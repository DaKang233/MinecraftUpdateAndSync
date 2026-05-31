using MinecraftUpdateAndSync.Core.Models;
using MinecraftUpdateAndSync.Core.Services;
using MinecraftUpdateAndSync.Core.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Services
{
    public class ManifestGenerator
    {
        /// <summary>
        /// Generates a manifest file for the specified directory, including file metadata and version information, and
        /// saves it to the given path. Optionally, identifies deleted files by comparing with a previous manifest.
        /// </summary>
        /// <remarks>If the version string is invalid, the method logs an error and does not generate a
        /// manifest. When a previous manifest is provided, a separate manifest listing deleted files is also generated.
        /// Files for which the hash cannot be computed are logged as errors.</remarks>
        /// <param name="directoryPath">The path to the directory to scan for files to include in the manifest.</param>
        /// <param name="manifestSavePath">The directory path where the generated manifest file will be saved.</param>
        /// <param name="version">The version string to associate with the generated manifest. Must be a valid version format.</param>
        /// <param name="lastManifestFilePath">The file path to a previous manifest for detecting deleted files. If null or not provided, deleted files are
        /// not tracked.</param>
        /// <param name="scanMode">Specifies the scan mode to use when scanning the directory. Determines whether to include or exclude files
        /// based on rules.</param>
        /// <param name="progress">An optional progress reporter to track the progress of the file scan and hash computation.</param>
        /// <param name="appliedRuleDirectories">An array of directory paths containing rule definitions to apply during the scan. Can be null if no rules
        /// are applied.</param>
        public static void GenerateManifest(
            string directoryPath,
            string manifestSavePath,
            string version,
            string? lastManifestFilePath = null,
            FileScanService.ScanMode scanMode = FileScanService.ScanMode.Exclude,
            IProgress<int>? progress = null,
            string[]? appliedRuleDirectories = null)
        {
            if (!Version.TryParse(version, out var normalizedVersion))
            {
                LogHelper.LogError($"Invalid version format: {version}. Please provide a valid version string.", LogHelper.LogDebugLevel.None);
                return;
            }

            var manifest = new Manifest { Version = normalizedVersion.ToString() };
            var deletedFilesManifest = new Manifest { Version = normalizedVersion.ToString() };
            var scannedFiles = FileScanService.ScanDirectory(directoryPath, scanMode, null, null, appliedRuleDirectories);

            // 使用线程安全集合收集结果
            var manifestFilesBag = new ConcurrentBag<ManifestFile>();
            var failedFiles = new ConcurrentDictionary<string, FileSnapshot>();

            // 控制并行度，防止 IO 或 CPU 过载
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            var processedFiles = 0;
            var totalFiles = scannedFiles.Count;
            var step = totalFiles / 100;
            if (step == 0) step = 1; // 防止除以零
            Parallel.ForEach(scannedFiles, parallelOptions, kvp =>
            {
                var relativePath = kvp.Key;
                var fileSnapshot = kvp.Value;
                var fullPath = fileSnapshot.FullPath;

                if (HashHelper.TryComputeFileHash(fullPath, out string hash))
                {
                    fileSnapshot.Hash = hash;
                    manifestFilesBag.Add(new ManifestFile
                    {
                        RelativePath = relativePath,
                        MTime = fileSnapshot.MTime,
                        Hash = hash,
                        Size = fileSnapshot.Size
                    });
                }
                else
                {
                    failedFiles[relativePath] = fileSnapshot;
                }
                if (progress != null && totalFiles > 0)
                {
                    Interlocked.Increment(ref processedFiles);
                    if (processedFiles % step == 0 || processedFiles == totalFiles) // 处理了总文件的百分之一数量的文件时更新一次进度
                    {
                        progress.Report(Math.Max(Math.Min((int)(((double)manifestFilesBag.Count + failedFiles.Count) / scannedFiles.Count * 100), 100), 0));
                    }
                }
            });

            progress?.Report(100); // 确保在完成后报告 100% 进度
            // 合并结果
            manifest.Files.AddRange(manifestFilesBag);
            manifest.Files = manifest.Files
                .OrderBy(f => f.RelativePath, StringComparer.OrdinalIgnoreCase)
                .ToList();

            // 处理删除文件
            if (!string.IsNullOrEmpty(lastManifestFilePath) && File.Exists(lastManifestFilePath))
            {
                var lastManifest = Serializer.LoadFromFile<Manifest>(lastManifestFilePath);
                foreach (var file in lastManifest.Files)
                {
                    if (!scannedFiles.ContainsKey(file.RelativePath))
                    {
                        deletedFilesManifest.Files.Add(new ManifestFile
                        {
                            RelativePath = file.RelativePath,
                            MTime = file.MTime,
                            Hash = file.Hash,
                            Size = file.Size
                        });
                    }
                }
            }

            // 保存 manifest
            Serializer.SaveToFile(Path.Combine(manifestSavePath, $"manifest-{version}.json"), manifest);
            if (deletedFilesManifest.Files.Count > 0)
            {
                Serializer.SaveToFile(Path.Combine(manifestSavePath, $"manifest-deleted-{version}.json"), deletedFilesManifest);
            }

            // 输出失败文件
            if (!failedFiles.IsEmpty)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Failed to compute hash for the following files:");
                var failedFilesOrdered = failedFiles.Values
                    .OrderBy(f => f.RelativePath, StringComparer.OrdinalIgnoreCase)
                    .ToList();
                foreach (var kvp in failedFilesOrdered)
                    sb.AppendLine(kvp.FullPath);
                LogHelper.LogError(sb.ToString(), LogHelper.LogDebugLevel.None);
            }
        }
    }
}

