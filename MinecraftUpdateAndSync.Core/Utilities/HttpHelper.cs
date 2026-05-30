using MinecraftUpdateAndSync.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Utilities
{
    /// <summary>
    /// HTTPS请求工具类
    /// </summary>
    public class HttpHelper
    {
        // 全局HttpClient（避免频繁创建释放）
        private static readonly HttpClient _httpClient;

        static HttpHelper()
        {
            _httpClient = new HttpClient(/*handler*/)
            {
                Timeout = TimeSpan.FromMinutes(5) // 下载超时时间
            };
        }

        /// <summary>
        /// 异步获取远程文本文件（如download.txt）
        /// </summary>
        public static async Task<string> GetRemoteTextAsync(string url)
        {
            try
            {
                using (var response = await _httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode(); // 抛出HTTP错误（4xx/5xx）
                    return await response.Content.ReadAsStringAsync(); // 获取文本内容
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"网络请求失败：{ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"请求超时：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 异步下载文件到指定路径（带进度回调）
        /// </summary>
        /// <param name="url">文件的URL地址</param>
        /// <param name="savePath">文件的保存路径</param>
        /// <param name="progress">进度回调接口，用于报告下载进度百分比</param>
        public static async Task DownloadFileAsync(
            string url,
            string savePath,
            CancellationToken cancellationToken,
            IProgress<int>? progress = null)
        {
            HttpResponseMessage? response = null;
            CancellationTokenRegistration ctr = default;

            try
            {
                response = await _httpClient.GetAsync(
                    url,
                    HttpCompletionOption.ResponseHeadersRead
                );

                response.EnsureSuccessStatusCode();

                // 注册取消回调：一旦取消，立刻 Dispose response
                ctr = cancellationToken.Register(() =>
                {
                    try { response.Dispose(); } catch { }
                });

                var totalBytes = response.Content.Headers.ContentLength ?? 0;
                var downloadedBytes = 0L;
                var buffer = new byte[8192];

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = File.Create(savePath))
                {
                    int bytesRead;
                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        // 主动检查取消
                        cancellationToken.ThrowIfCancellationRequested();

                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        downloadedBytes += bytesRead;

                        if (totalBytes > 0 && progress != null)
                        {
                            int percent = (int)(downloadedBytes * 100.0 / totalBytes);
                            progress.Report(percent);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                if (File.Exists(savePath))
                    File.Delete(savePath);

                throw; // 非常重要：向上传递“取消”
            }
            catch (ObjectDisposedException)
            {
                // HttpResponse 被 Dispose，通常意味着取消
                if (File.Exists(savePath))
                    File.Delete(savePath);

                throw new OperationCanceledException(cancellationToken);
            }
            catch (Exception ex)
            {
                if (File.Exists(savePath))
                    File.Delete(savePath);

                throw new Exception($"下载文件失败：{ex.Message}", ex);
            }
            finally
            {
                ctr.Dispose();
                response?.Dispose();
            }
        }

        /// <summary>
        /// 异步下载文件，并实时报告已下载的字节数
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="filePath">本地保存路径</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="progress">用于报告已下载的字节数</param>
        public static async Task DownloadFileAsyncWithByteProgress(
            string url,
            string filePath,
            CancellationToken cancellationToken,
            IProgress<long>? progress = null)
        {
            CancellationTokenRegistration ctr = default;
            ArgumentNullException.ThrowIfNull(filePath);
            try
            {
                var response = await _httpClient.GetAsync(
                    url,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);
                response.EnsureSuccessStatusCode();

                ctr = cancellationToken.Register(() =>
                {
                    try { response?.Dispose(); } catch { }
                });

                var buffer = new byte[8192];
                long bytesDownloaded = 0;

                // 确保在创建 FileStream 之前目标目录存在，避免 Path.GetDirectoryName 返回 null 引发警告/异常
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);
                else throw new ArgumentException("无效的文件路径，无法获取目录信息。", nameof(filePath));

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                {
                    int bytesRead;
                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                        bytesDownloaded += bytesRead;

                        // 报告已下载的字节数
                        progress?.Report(bytesDownloaded);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                if (File.Exists(filePath)) File.Delete(filePath);
                throw new OperationCanceledException(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                if (File.Exists(filePath)) File.Delete(filePath);
                throw;
            }
            catch (Exception ex) { throw new Exception("下载文件失败：", ex); }
            finally
            {
                ctr.Dispose();
            }
        }

        public class DownloadItem
        {
            public required Uri FileUri { get; set; }
            public required string FileName { get; set; }
            public required string SavePath { get; set; }
        }

        public class DownloadListResult
        {
            public required List<DownloadItem> Items { get; set; }
            public required List<string> Skipped { get; set; }
        }

        /// <summary>
        /// 生成包含下载项和被跳过项的下载列表，基于指定的协议、服务器、端口、前缀和相对路径集合。
        /// </summary>
        /// <remarks>如果 downloadRelativePaths 中的某个路径无效或无法生成有效的下载项，则该路径会被添加到结果的 Skipped
        /// 列表中。方法不会尝试访问远程服务器，仅根据输入参数生成下载项信息。</remarks>
        /// <param name="protocol">用于构建基础下载 URL 的协议名称（如 "http" 或 "https"）。</param>
        /// <param name="webServer">Web 服务器的主机名或 IP 地址。不能为空。</param>
        /// <param name="webPort">Web 服务器的端口号，作为字符串传递。</param>
        /// <param name="prefix">可选的 URL 路径前缀，将添加到每个下载项的相对路径前。</param>
        /// <param name="downloadRelativePaths">要下载的文件的相对路径集合。不能为空，集合中的每个元素将用于生成下载项。</param>
        /// <param name="savePath">文件保存到本地的目标目录路径。不能为空。</param>
        /// <returns>一个 DownloadListResult 对象，包含成功生成的下载项列表和被跳过的路径列表。</returns>
        /// <exception cref="ArgumentNullException">当 webServer、savePath 或 downloadRelativePaths 为 null 或空时抛出。</exception>
        /// <exception cref="ArgumentException">当基础 URL 格式无效时抛出。</exception>
        public static DownloadListResult GetDownloadList(
            string protocol,
            string webServer,
            string webPort,
            string prefix,
            List<string> downloadRelativePaths,
            string savePath)
        {
            // 参数验证
            if (string.IsNullOrWhiteSpace(webServer))
                throw new ArgumentNullException(nameof(webServer), "Web服务器地址不能为空");
            if (string.IsNullOrWhiteSpace(savePath))
                throw new ArgumentNullException(nameof(savePath), "保存路径不能为空");
            if (downloadRelativePaths == null)
                throw new ArgumentNullException(nameof(downloadRelativePaths), "下载路径列表不能为空");

            var downloadItems = new List<DownloadItem>();
            var skipped = new List<string>();
            Uri baseUri;
            try
            {
                baseUri = new Uri($"{protocol}://{webServer}:{webPort}/");
            }
            catch (UriFormatException ex) { throw new ArgumentException("Base URL 格式错误：", ex); }

            var normalizedPrefix = (prefix ?? string.Empty).Trim().Trim('/');
            foreach (var path in downloadRelativePaths)
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    skipped.Add(path);
                    continue;
                }
                var p = path.Trim();

                try
                {
                    Uri? finalUri;
                    if (Uri.TryCreate(p, UriKind.Absolute, out finalUri))
                    {
                        // 已经是完整 URL
                        finalUri = new Uri(p);
                    }
                    else
                    {
                        var rel = p.TrimStart('/');
                        // 拼接前缀和相对路径（如果前缀为空则直接使用相对路径）
                        var combined = string.IsNullOrEmpty(normalizedPrefix) ? rel : $"{normalizedPrefix}/{rel}";
                        finalUri = new Uri(baseUri, combined);
                    }

                    var fileName = Path.GetFileName(finalUri.LocalPath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        skipped.Add(p);
                        continue;
                    }

                    var savePathSingle = Path.Combine(savePath, fileName);
                    downloadItems.Add(new DownloadItem
                    {
                        FileUri = finalUri,
                        FileName = fileName,
                        SavePath = savePathSingle
                    });
                }
                catch
                {
                    skipped.Add(p);
                }
            }
            DownloadListResult result = new DownloadListResult()
            {
                Items = downloadItems,
                Skipped = skipped,
            };
            return result;
        }

        /// <summary>
        /// Represents progress information for a multi-file download operation, including overall and per-file status.
        /// </summary>
        /// <remarks>Use this class to track the number of files to be downloaded, the number completed,
        /// the name of the current file, and the progress percentage of the current file. The class also indicates
        /// whether the entire download operation has completed.</remarks>
        public class DownloadProgressInfo
        {
            public int TotalFiles { get; }
            public int CompletedFiles { get; }

            public string CurrentFileName { get; }
            public int CurrentFilePercent { get; } // 0-100

            public bool IsCompleted { get; }

            public DownloadProgressInfo(
                int totalFiles,
                int completedFiles,
                string currentFileName,
                int currentFilePercent,
                bool isCompleted)
            {
                TotalFiles = totalFiles;
                CompletedFiles = completedFiles;
                CurrentFileName = currentFileName;
                CurrentFilePercent = currentFilePercent;
                IsCompleted = isCompleted;
            }
        }

        /// <summary>
        /// 下载指定列表里的文件，按百分比报告进度
        /// </summary>
        /// <param name="downloadItems">下载列表</param>
        /// <param name="cancellationToken">取消Token</param>
        /// <param name="progressReporter">进度报告（百分比）</param>
        /// <returns></returns>
        public static async Task DownloadListItemsAsync(
            List<DownloadItem> downloadItems,
            CancellationToken cancellationToken,
            IProgress<DownloadProgressInfo> progressReporter)
        {
            int totalFiles = downloadItems.Count;
            int downloadedFiles = 0;

            var semaphore = new SemaphoreSlim(3);
            var tasks = new List<Task>();

            foreach (var item in downloadItems)
            {
                await semaphore.WaitAsync(cancellationToken);

                tasks.Add(Task.Run(async () =>
                {
                    var uri = item.FileUri;
                    var fileName = item.FileName;
                    var savePath = item.SavePath;

                    try
                    {
                        var fileProgress = new Progress<int>(percent =>
                        {
                            progressReporter?.Report(
                                new DownloadProgressInfo(
                                    totalFiles,
                                    downloadedFiles,
                                    fileName,
                                    percent,
                                    false));
                        });

                        await HttpHelper.DownloadFileAsync(
                            uri.ToString(),
                            savePath,
                            cancellationToken,
                            fileProgress);

                        int completed = Interlocked.Increment(ref downloadedFiles);

                        progressReporter?.Report(
                            new DownloadProgressInfo(
                                totalFiles,
                                completed,
                                fileName,
                                100,
                                completed == totalFiles));
                    }
                    catch (OperationCanceledException)
                    {
                        progressReporter?.Report(
                            new DownloadProgressInfo(
                                totalFiles,
                                downloadedFiles,
                                fileName,
                                0,
                                false));
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cancellationToken));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 下载进度信息（按字节数）
        /// </summary>
        public class DownloadByteProgressInfo
        {
            public int TotalFiles { get; }
            public int CompletedFiles { get; }

            public string CurrentFileName { get; }

            public long CurrentFileBytesDownloaded { get; }
            public long CurrentFileTotalBytes { get; }

            public long TotalBytesDownloaded { get; }
            public long TotalBytes { get; }

            public double CurrentFileSpeedBytesPerSec { get; }

            public bool IsCompleted { get; }

            /// <summary>
            /// 下载进度信息（按字节数）
            /// </summary>
            /// <param name="totalFiles">要下载的总文件数</param>
            /// <param name="completedFiles">已下载的文件数</param>
            /// <param name="currentFileName">当前下载的文件名</param>
            /// <param name="currentFileBytesDownloaded">正在下载的文件的已下载字节</param>
            /// <param name="currentFileTotalBytes">正在下载的文件的总字节</param>
            /// <param name="totalBytesDownloaded">已下载的总字节</param>
            /// <param name="totalBytes">要下载的总字节</param>
            /// <param name="currentFileSpeedBytesPerSec">下载速率</param>
            /// <param name="isCompleted">是否已全部下载完成</param>
            public DownloadByteProgressInfo(
                int totalFiles,
                int completedFiles,
                string currentFileName,
                long currentFileBytesDownloaded,
                long currentFileTotalBytes,
                long totalBytesDownloaded,
                long totalBytes,
                double currentFileSpeedBytesPerSec,
                bool isCompleted)
            {
                TotalFiles = totalFiles;
                CompletedFiles = completedFiles;
                CurrentFileName = currentFileName;
                CurrentFileBytesDownloaded = currentFileBytesDownloaded;
                CurrentFileTotalBytes = currentFileTotalBytes;
                TotalBytesDownloaded = totalBytesDownloaded;
                TotalBytes = totalBytes;
                CurrentFileSpeedBytesPerSec = currentFileSpeedBytesPerSec;
                IsCompleted = isCompleted;
            }
        }

        /// <summary>
        /// Asynchronously downloads a collection of files, reporting aggregate and per-file byte progress updates
        /// during the operation.
        /// </summary>
        /// <remarks>Progress updates include the total number of files, the number of completed files,
        /// the current file's progress, total bytes downloaded, and estimated download speed. The method limits
        /// concurrent downloads to three at a time. If cancellation is requested, incomplete downloads are stopped as
        /// soon as possible.</remarks>
        /// <param name="downloadItems">A list of DownloadItem objects representing the files to download. Each item specifies the source URI,
        /// destination path, and file name.</param>
        /// <param name="cancellationToken">A token that can be used to request cancellation of the download operation.</param>
        /// <param name="progressReporter">An optional progress reporter that receives DownloadByteProgressInfo updates for overall and per-file
        /// download progress, including bytes downloaded and estimated speed.</param>
        /// <returns>A task that represents the asynchronous download operation. The task completes when all downloads have
        /// finished or the operation is canceled.</returns>
        public static async Task DownloadListItemsWithByteProgressAsync(
            List<DownloadItem> downloadItems,
            CancellationToken cancellationToken,
            IProgress<DownloadByteProgressInfo> progressReporter)
        {
            int totalFiles = downloadItems.Count;
            int completedFiles = 0;

            long totalBytes = 0;
            foreach (var item in downloadItems)
                totalBytes += await GetContentLengthAsync(item.FileUri.ToString());

            long totalBytesDownloaded = 0;
            var semaphore = new SemaphoreSlim(3);
            var tasks = new List<Task>();

            foreach (var item in downloadItems)
            {
                await semaphore.WaitAsync(cancellationToken);

                tasks.Add(Task.Run(async () =>
                {
                    var fileName = item.FileName;
                    var url = item.FileUri.ToString();
                    var savePath = item.SavePath;

                    long fileTotalBytes = await GetContentLengthAsync(url);
                    long lastBytes = 0;
                    long lastSampleBytes = 0;
                    const long sampleIntervalMs = 500;
                    var speedSamples = new Queue<decimal>(4);
                    var sw = Stopwatch.StartNew();

                    try
                    {
                        var fileProgress = new Progress<long>(bytesDownloaded =>
                        {
                            var deltaBytes = bytesDownloaded - lastBytes;
                            if (deltaBytes <= 0)
                                return;

                            lastBytes = bytesDownloaded;
                            var currentTotalBytesDownloaded = Interlocked.Add(ref totalBytesDownloaded, deltaBytes);

                            decimal speed = 0m;
                            var elapsedMs = sw.ElapsedMilliseconds;
                            if (elapsedMs >= sampleIntervalMs)
                            {
                                var sampleDelta = bytesDownloaded - lastSampleBytes;
                                if (sampleDelta > 0)
                                {
                                    speed = sampleDelta * 1000m / elapsedMs;
                                    if (speedSamples.Count == 4)
                                        speedSamples.Dequeue();
                                    speedSamples.Enqueue(speed);
                                }

                                lastSampleBytes = bytesDownloaded;
                                sw.Restart();
                            }

                            if (speed == 0m && speedSamples.Count > 0)
                            {
                                decimal sum = 0m;
                                foreach (var sample in speedSamples) sum += sample;
                                speed = sum / speedSamples.Count;
                            }

                            progressReporter?.Report(
                                new DownloadByteProgressInfo(
                                    totalFiles,
                                    Volatile.Read(ref completedFiles),
                                    fileName,
                                    bytesDownloaded,
                                    fileTotalBytes,
                                    currentTotalBytesDownloaded,
                                    totalBytes,
                                    (double)speed,
                                    false));
                        });

                        await DownloadFileAsyncWithByteProgress(url, savePath, cancellationToken, fileProgress);

                        if (fileTotalBytes > 0 && lastBytes < fileTotalBytes)
                        {
                            Interlocked.Add(ref totalBytesDownloaded, fileTotalBytes - lastBytes);
                        }

                        int finished = Interlocked.Increment(ref completedFiles);
                        progressReporter?.Report(
                            new DownloadByteProgressInfo(
                                totalFiles,
                                finished,
                                fileName,
                                fileTotalBytes,
                                fileTotalBytes,
                                Interlocked.Read(ref totalBytesDownloaded),
                                totalBytes,
                                0,
                                finished == totalFiles));
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cancellationToken));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 获取远程文件的 Content-Length（字节数），若无法获取则返回 0
        /// </summary>
        /// <param name="url">远程文件URL</param>
        /// <returns>文件字节数，获取失败返回0</returns>
        private static async Task<long> GetContentLengthAsync(string url)
        {
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Head, url))
                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength.HasValue)
                        return response.Content.Headers.ContentLength.Value;
                }

                using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    return response.Content.Headers.ContentLength ?? 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        // 根据字节大小返回合适的单位值（KB, MB, GB等）
        public static (double value, string unit) BytesToUnit(long byteCount)
        {
            // 如果字节数小于 1024，直接返回字节
            if (byteCount < 1024)
                return (byteCount, "B");

            // 定义单位数组
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

            // 计算合适的单位索引
            int unitIndex = 0;
            double size = byteCount;

            while (size >= 1024 && unitIndex < units.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            // 返回保留两位小数的结果
            return (Math.Round(size, 2), units[unitIndex]);
        }

        public static string GetBytesUnitString(long byteCount)
        {
            var (value, unit) = BytesToUnit(byteCount);
            return $"{value:F2} {unit}";
        }

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

