using MinecraftUpdateAndSync.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftUpdateAndSync.Core.Utilities;
using System.Diagnostics;

namespace MinecraftUpdateAndSync.Core.Services
{
    public class FileScanService
    {
        public enum ScanMode
        {
            Include,
            Exclude
        }

        /// <summary>
        /// Scans the specified directory and returns a dictionary of file snapshots, optionally including or excluding
        /// files based on applied rule directories and scan mode.
        /// </summary>
        /// <remarks>If scanMode is ScanMode.Exclude, files within the applied rule directories are
        /// omitted from the results. If scanMode is ScanMode.Include, only files within the applied rule directories
        /// are included. Directory paths are normalized for comparison. This method performs a recursive scan of all
        /// subdirectories.</remarks>
        /// <param name="directoryPath">The full path of the directory to scan. Must not be null or empty.</param>
        /// <param name="scanMode">Specifies whether to include or exclude files based on the applied rule directories. The default is
        /// ScanMode.Exclude.</param>
        /// <param name="progress">An optional IProgress object to report the progress of the scan. The progress is reported
        /// as a percentage of the total files processed.</param>
        /// <param name="cancellationToken">An optional CancellationToken to cancel the scan operation.</param>
        /// <param name="appliedRuleDirectories">An array of directory paths used to filter files during the scan. If null or empty, all files are included.
        /// Paths can be absolute or relative to the scanned directory.</param>
        /// <returns>A dictionary mapping each file's relative path to its corresponding FileSnapshot. The dictionary will be
        /// empty if no files match the criteria.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
        public static Dictionary<string, FileSnapshot> ScanDirectory(
            string directoryPath,
            ScanMode scanMode = ScanMode.Exclude,
            IProgress<int>? progress = null,
            CancellationToken? cancellationToken = null,
            string[]? appliedRuleDirectories = null)
        {
            var directoryFiles = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            var fileSnapshots = new Dictionary<string, FileSnapshot>();
            var normalizedAppliedRuleDirs =
                appliedRuleDirectories?
                    .Select(d =>
                    {
                        var relative =
                            Path.IsPathRooted(d)
                                ? PathHelper.GetRelativePath(directoryPath, d)
                                : d;

                        return relative
                            .Replace('\\', '/')
                            .TrimEnd('/') + "/";
                    })
                    .ToArray();
            var step = 1;
            if (directoryFiles.Length > 0)
            {
                step = Math.Max(1, directoryFiles.Length / 100);
            }
            var processedFiles = 0;
            var stopwatch = Stopwatch.StartNew();
            const int reportIntervalMs = 500;
            int lastReportedProgress = -1;

            foreach (var file in directoryFiles)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                var fileInfo = new FileInfo(file);
                var relativePath = PathHelper.GetRelativePath(directoryPath, fileInfo.FullName);
                var normalizedRelativePath = relativePath.Replace('\\', '/');
                var fileSnapshot = new FileSnapshot
                {
                    FullPath = file,
                    RelativePath = relativePath,
                    MTime = fileInfo.LastWriteTimeUtc,
                    Hash = "", // Hash calculation can be implemented here if needed
                    Size = fileInfo.Length
                };

                processedFiles++;

                if (progress != null &&
                    stopwatch.ElapsedMilliseconds >= reportIntervalMs)
                {
                    var currentProgress =
                        Math.Min(
                            (int)((double)processedFiles / directoryFiles.Length * 100),
                            100);
                    if (currentProgress != lastReportedProgress)
                    {
                        progress.Report(currentProgress);
                        lastReportedProgress = currentProgress;
                    }
                    stopwatch.Restart();
                }

                if (normalizedAppliedRuleDirs != null && 
                    (scanMode == ScanMode.Exclude ?
                 normalizedAppliedRuleDirs.Any(dir =>
                    normalizedRelativePath.StartsWith(
                        dir,
                        StringComparison.OrdinalIgnoreCase)) :
                 !normalizedAppliedRuleDirs.Any(dir =>
                    normalizedRelativePath.StartsWith(
                        dir,
                        StringComparison.OrdinalIgnoreCase))))
                    continue;
                fileSnapshots[relativePath] = fileSnapshot;
            }
            stopwatch.Stop();
            if (progress != null)
            {
                progress.Report(100);
            }
            LogHelper.LogInfo($"File scan completed in {stopwatch.ElapsedMilliseconds} ms.", LogHelper.LogDebugLevel.None);
            LogHelper.LogInfo($"Processed {processedFiles} files.", LogHelper.LogDebugLevel.Low);
            if (appliedRuleDirectories != null) 
            {
                LogHelper.LogInfo($"Applied rule directories: {string.Join(", ", appliedRuleDirectories)}", LogHelper.LogDebugLevel.Low);
            }
            LogHelper.LogInfo($"Scan mode: {scanMode}", LogHelper.LogDebugLevel.Low);
            return fileSnapshots;
        }

        public static Manifest GetLocalFileManifest(Dictionary<string, FileSnapshot> files, string version)
        {
            var manifest = new Manifest() { Version = "" };
            
            manifest.Files = files.Values.Select(fs => new ManifestFile
            {
                RelativePath = fs.RelativePath,
                MTime = fs.MTime,
                Hash = fs.Hash,
                Size = fs.Size
            }).ToList();
            manifest.Version = version;
            return manifest;
        }
    }
}

