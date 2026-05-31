using MinecraftUpdateAndSync.Core.Contracts;
using MinecraftUpdateAndSync.Core.Models;
using MinecraftUpdateAndSync.Core.Services;
using MinecraftUpdateAndSync.Core.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinecraftUpdateAndSync.Core.Utilities.HttpHelper;

namespace MinecraftUpdateAndSync.Core.Services
{
    public class DownloadService
    {
        
        public static List<HttpHelper.DownloadItem> ConstructDownloadItems(
            List<CompareService.CompareResult> compareResults,
            string minecraftDirectory,
            UpdateInstruction updateInstruction)
        {
            var downloadItems = new List<HttpHelper.DownloadItem>();
            var serverResourceBaseUrl = string.Empty;
            if (updateInstruction.Prefix != null)
            {
                // 构造服务器资源的基础 URL: protocol://server_address:port/prefix/resource_relative_directory
                serverResourceBaseUrl = $"{UpdateInstructionMethod.GetProtocolString(updateInstruction.Protocol)}://" +
                    $"{updateInstruction.ServerAddress}:{updateInstruction.ServerPort}/" +
                    $"{updateInstruction.Prefix}/{updateInstruction.ResourceRelativeDirectory?.TrimStart('/')}";
            }
            else 
            {
                // 构造服务器资源的基础 URL: protocol://server_address:port/resource_relative_directory
                serverResourceBaseUrl = $"{UpdateInstructionMethod.GetProtocolString(updateInstruction.Protocol)}://" +
                    $"{updateInstruction.ServerAddress}:{updateInstruction.ServerPort}/" +
                    $"{updateInstruction.ResourceRelativeDirectory?.TrimStart('/')}";
            }
            foreach (var item in compareResults)
            {
                var action = item.Action;
                var relativePath = item.RelativePath;
                var savePath = Path.Combine(minecraftDirectory, relativePath);
                if (action == SyncAction.Download || action == SyncAction.Replace)
                {
                    // 构造下载项
                    Uri uri = new(serverResourceBaseUrl + relativePath);

                    downloadItems.Add(new HttpHelper.DownloadItem()
                    {
                        FileUri = uri,
                        FileName = Path.GetFileName(relativePath),
                        SavePath = savePath
                    });
                }
            }
            return downloadItems;
        }

        public static async Task<List<Manifest>> GetRemoteManifestFiles(UpdateInstruction updateInstruction)
        {
            var serverManifest = string.Empty;
            if (!string.IsNullOrEmpty(updateInstruction.Prefix))
                serverManifest = $"{UpdateInstructionMethod.GetProtocolString(updateInstruction.Protocol)}://" +
                    $"{updateInstruction.ServerAddress}:{updateInstruction.ServerPort}/" +
                    $"{updateInstruction.Prefix}/{updateInstruction.ManifestPath?.TrimStart('/')}";
            else serverManifest = $"{UpdateInstructionMethod.GetProtocolString(updateInstruction.Protocol)}://" +
                    $"{updateInstruction.ServerAddress}:{updateInstruction.ServerPort}/" +
                    $"{updateInstruction.ManifestPath?.TrimStart('/')}";
            var manifestString = await HttpHelper.GetRemoteTextAsync(serverManifest);
            var deletedManifestString = string.Empty;
            try { deletedManifestString = await HttpHelper.GetRemoteTextAsync(serverManifest.Replace("manifest", "manifest-deleted")); }
            catch { }
            var manifests = new List<Manifest>
            {
                Serializer.DeserializeFromString<Manifest>(manifestString),
            };
            if (!string.IsNullOrEmpty(deletedManifestString))
                manifests.Add(Serializer.DeserializeFromString<Manifest>(deletedManifestString));
            return manifests;
        }

        /// <summary>
        /// Asynchronously downloads multiple files concurrently with aggregated progress reporting,
        /// download speed calculation, cancellation support, and per-file state tracking.
        /// </summary>
        /// <param name="downloadItems">
        /// The collection of files to download.
        /// Each <see cref="DownloadItem"/> contains the source URL, target save path,
        /// and metadata required for the download operation.
        /// </param>
        /// <param name="cancellationToken">
        /// A token used to cancel the entire download session.
        /// When cancellation is requested:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// Pending downloads will stop waiting for execution.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Active downloads will attempt to terminate gracefully.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// File states will be marked as <see cref="DownloadState.Cancelled"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="progressReporter">
        /// Optional progress reporter used to receive real-time aggregated download status.
        /// Reports are emitted periodically (approximately every 200 milliseconds)
        /// during the download session.
        ///
        /// The reported <see cref="DownloadSessionProgress"/> includes:
        /// <list type="bullet">
        /// <item>
        /// <description>Total file count.</description>
        /// </item>
        /// <item>
        /// <description>Completed and failed file counts.</description>
        /// </item>
        /// <item>
        /// <description>Total downloaded bytes.</description>
        /// </item>
        /// <item>
        /// <description>Total download size.</description>
        /// </item>
        /// <item>
        /// <description>Combined download speed of all active tasks.</description>
        /// </item>
        /// <item>
        /// <description>Per-file progress information and current state.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="maxConcurrency">
        /// The maximum number of files allowed to download simultaneously.
        /// Default value is <c>3</c>.
        ///
        /// Internally, concurrency is controlled using
        /// <see cref="SemaphoreSlim"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous multi-file download operation.
        /// The task completes when:
        /// <list type="bullet">
        /// <item>
        /// <description>All downloads finish successfully.</description>
        /// </item>
        /// <item>
        /// <description>Some downloads fail but all tasks have completed.</description>
        /// </item>
        /// <item>
        /// <description>The operation is cancelled.</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Features provided by this method:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// Concurrent multi-file downloading with configurable parallelism.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Aggregated session-level progress tracking.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Real-time per-file speed calculation using moving average sampling.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Thread-safe shared state updates via
        /// <see cref="Interlocked"/>,
        /// <see cref="Volatile"/>,
        /// and <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Periodic UI progress reporting using
        /// <see cref="PeriodicTimer"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Graceful handling of cancellation and download failures.
        /// </description>
        /// </item>
        /// </list>
        ///
        /// Notes:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// File sizes are pre-fetched before download begins
        /// in order to calculate total session progress.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Download speed is estimated using short interval sampling
        /// and averaged over recent samples to reduce fluctuation.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Progress callbacks may occur on background threads,
        /// depending on the implementation of <see cref="IProgress{T}"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public static async Task DownloadFilesAsync(
            List<DownloadItem> downloadItems,
            CancellationToken cancellationToken,
            IProgress<DownloadSessionProgress>? progressReporter = null,
            int maxConcurrency = 3)
        {
            int totalFiles = downloadItems.Count;

            int completedFiles = 0;

            long totalBytesDownloaded = 0;

            long totalBytes = 0;

            foreach (var item in downloadItems)
            {
                totalBytes += await GetContentLengthAsync(
                    item.FileUri.ToString());
            }

            var fileReports =
                new ConcurrentDictionary<string, DownloadFileProgress>();

            foreach (var item in downloadItems)
            {
                fileReports[item.FileName] =
                    new DownloadFileProgress
                    {
                        FileName = item.FileName,
                        Url = item.FileUri.ToString(),
                        SavePath = item.SavePath,
                        State = DownloadState.Pending
                    };
            }

            var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = new List<Task>();

            using (var timer =
                new PeriodicTimer(TimeSpan.FromMilliseconds(200)))
            {
                // UI汇报任务
                var reportingTask = Task.Run(async () =>
                {
                    try
                    {
                        while (await timer.WaitForNextTickAsync(
                            cancellationToken))
                        {
                            progressReporter?.Report(
                                new DownloadSessionProgress
                                {
                                    TotalFiles = totalFiles,

                                    CompletedFiles =
                                        Volatile.Read(ref completedFiles),

                                    FailedFiles =
                                        fileReports.Values.Count(x =>
                                            x.State ==
                                            DownloadState.Failed),

                                    TotalDownloadedBytes =
                                        Volatile.Read(
                                            ref totalBytesDownloaded),

                                    TotalBytes = totalBytes,

                                    TotalSpeedBytesPerSec =
                                        fileReports.Values.Sum(x =>
                                            x.SpeedBytesPerSec),

                                    IsCompleted = false,

                                    Files =
                                        fileReports.Values.ToList()
                                });
                        }
                    }
                    catch (OperationCanceledException)
                    {
                    }
                });

                foreach (var item in downloadItems)
                {
                    await semaphore.WaitAsync(cancellationToken);

                    tasks.Add(Task.Run(async () =>
                    {
                        var fileName = item.FileName;

                        var url = item.FileUri.ToString();

                        var savePath = item.SavePath;

                        var report = fileReports[fileName];

                        long lastBytes = 0;

                        long lastSampleBytes = 0;

                        const long sampleIntervalMs = 500;

                        var speedSamples = new Queue<double>();

                        var sw = Stopwatch.StartNew();

                        try
                        {
                            report.State = DownloadState.Downloading;

                            long fileTotalBytes =
                                await GetContentLengthAsync(url);

                            report.TotalBytes = fileTotalBytes;

                            var fileProgress =
                                new Progress<long>(bytesDownloaded =>
                                {
                                    var deltaBytes =
                                        bytesDownloaded - lastBytes;

                                    if (deltaBytes <= 0)
                                        return;

                                    lastBytes = bytesDownloaded;

                                    Interlocked.Add(
                                        ref totalBytesDownloaded,
                                        deltaBytes);

                                    report.DownloadedBytes =
                                        bytesDownloaded;

                                    double speed = 0;

                                    var elapsedMs =
                                        sw.ElapsedMilliseconds;

                                    if (elapsedMs >= sampleIntervalMs)
                                    {
                                        var sampleDelta =
                                            bytesDownloaded -
                                            lastSampleBytes;

                                        if (sampleDelta > 0)
                                        {
                                            speed =
                                                sampleDelta * 1000.0 /
                                                elapsedMs;

                                            if (speedSamples.Count >= 4)
                                                speedSamples.Dequeue();

                                            speedSamples.Enqueue(speed);
                                        }

                                        lastSampleBytes =
                                            bytesDownloaded;

                                        sw.Restart();
                                    }

                                    if (speed == 0 &&
                                        speedSamples.Count > 0)
                                    {
                                        speed =
                                            speedSamples.Average();
                                    }

                                    report.SpeedBytesPerSec =
                                        speed;
                                });

                            await DownloadFileAsyncWithByteProgress(
                                url,
                                savePath,
                                cancellationToken,
                                fileProgress);

                            report.DownloadedBytes =
                                report.TotalBytes;

                            report.SpeedBytesPerSec = 0;

                            report.State =
                                DownloadState.Completed;

                            Interlocked.Increment(
                                ref completedFiles);
                        }
                        catch (OperationCanceledException)
                        {
                            report.State =
                                DownloadState.Cancelled;

                            report.SpeedBytesPerSec = 0;
                        }
                        catch (Exception ex)
                        {
                            report.State =
                                DownloadState.Failed;

                            report.ErrorMessage =
                                ex.Message;

                            report.SpeedBytesPerSec = 0;
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }, cancellationToken));
                }

                await Task.WhenAll(tasks);

                progressReporter?.Report(
                    new DownloadSessionProgress
                    {
                        TotalFiles = totalFiles,

                        CompletedFiles =
                            Volatile.Read(ref completedFiles),

                        FailedFiles =
                            fileReports.Values.Count(x =>
                                x.State ==
                                DownloadState.Failed),

                        TotalDownloadedBytes =
                            Volatile.Read(
                                ref totalBytesDownloaded),

                        TotalBytes = totalBytes,

                        TotalSpeedBytesPerSec = 0,

                        IsCompleted = true,

                        Files =
                            fileReports.Values.ToList()
                    });
            }
        }
    }
}

