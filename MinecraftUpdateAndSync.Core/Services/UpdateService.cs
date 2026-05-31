using MinecraftUpdateAndSync.Core.Contracts;
using MinecraftUpdateAndSync.Core.Models;
using MinecraftUpdateAndSync.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MinecraftUpdateAndSync.Core.Utilities.LogHelper;

namespace MinecraftUpdateAndSync.Core.Services
{
    public class UpdateService
    {
        public class UpdateClientOptions
        {
            public required UpdateInstruction UpdateInstruction { get; set; }

            public required Dictionary<string, FileSnapshot> LocalFiles { get; set; }

            public required string MinecraftDirectory { get; set; }

            public CancellationToken CancellationToken { get; set; }

            public required IProgress<DownloadSessionProgress>
                DownloadProgress
            { get; set; }

            public required IProgress<ProcessMessage>
                LogProgress
            { get; set; }

            public int ParallelCount { get; set; }
        }

        public async static Task UpdateClient(UpdateClientOptions updateClientOptions)
        {
            // 1. 下载最新的文件清单
            // 2. 对比文件清单，找出需要更新的文件列表
            // 3. 构造下载任务队列
            // 4. 开始下载文件
            // 5. 删除旧的文件（如果有删除清单且允许删除）

            var updateInstruction = updateClientOptions.UpdateInstruction;
            var localFiles = updateClientOptions.LocalFiles;
            var downloadProgress = updateClientOptions.DownloadProgress;
            var progress = updateClientOptions.LogProgress;
            var minecraftDirectory = updateClientOptions.MinecraftDirectory;
            var cancellationToken = updateClientOptions.CancellationToken;
            var parallelCount = updateClientOptions.ParallelCount;

            LogHelper.Report(progress, "下载最新的文件清单...", LogHelper.LogLevel.Info);
            var manifestList = await DownloadService.GetRemoteManifestFiles(updateInstruction);
            var manifest = manifestList[0];
            Manifest? deleteManifest = null;
            if (manifestList.Count > 1)
            {
                deleteManifest = manifestList[1];
            }

            LogHelper.Report(progress, "下载最新的文件清单完成。", LogHelper.LogLevel.Info);

            LogHelper.Report(progress, "对比文件清单...", LogHelper.LogLevel.Info);
            var compareResults = CompareService.CompareManifestToDictLocalFiles(manifest, localFiles);
            LogHelper.Report(progress, "对比文件清单完成。", LogHelper.LogLevel.Info);

            LogHelper.Report(progress, "构造下载任务队列...", LogHelper.LogLevel.Info);
            var downloadItems = DownloadService.ConstructDownloadItems(compareResults, minecraftDirectory, updateInstruction);
            LogHelper.Report(progress, $"构造下载任务队列完成，共构造了 {downloadItems.Count} 个下载任务。", LogHelper.LogLevel.Info);
            LogHelper.Report(progress, "开始下载文件...", LogHelper.LogLevel.Info);
            await DownloadService.DownloadFilesAsync(downloadItems, cancellationToken, downloadProgress, parallelCount);
            LogHelper.Report(progress, "下载文件完成。", LogHelper.LogLevel.Info);
            if (deleteManifest != null && updateInstruction.AllowDelete)
            {
                LogHelper.Report(progress, "删除旧的文件...", LogHelper.LogLevel.Info);
                await Task.Run(() => { DeleteFiles(deleteManifest, minecraftDirectory, progress); });
                LogHelper.Report(progress, "删除旧的文件完成。", LogHelper.LogLevel.Info);
            }
            LogHelper.Report(progress, "更新完成(未进行校验)。", LogHelper.LogLevel.Info);
        }

        private static void DeleteFiles(Manifest deleteManifest, string minecraftDirectory, IProgress<LogHelper.ProcessMessage> progress)
        {
            foreach (var file in deleteManifest.Files)
            {
                var fullPath = Path.Combine(minecraftDirectory, file.RelativePath);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        File.Delete(fullPath);
                        LogHelper.Report(progress, $"已删除文件: {file.RelativePath}", LogHelper.LogLevel.Info);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Report(progress, $"删除文件失败: {file.RelativePath}. 错误信息: {ex.Message}", LogHelper.LogLevel.Error);
                    }
                }
            }
        }
    }
}

