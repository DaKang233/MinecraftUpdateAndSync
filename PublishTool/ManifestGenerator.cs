using MinecraftUpdateAndSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftUpdateAndSync.Services;
using MinecraftUpdateAndSync.Utilities;

namespace MinecraftUpdateAndSync.PublishTool
{
    internal class ManifestGenerator
    {

        public static void GenerateManifest(string directoryPath, string manifestSavePath, string version, string lastManifestFilePath = null, string[] ignoredDirectories = null)
        {
            Manifest manifest = new Manifest();
            Manifest deletedFilesManifest = new Manifest();
            manifest.Version = version;
            deletedFilesManifest.Version = version;
            Dictionary<string, FileSnapshot> scannedManifestFiles = FileScanService.ScanDirectory(directoryPath, ignoredDirectories);
            foreach (var file in scannedManifestFiles)
            {
                var fullPath = file.Value.FullPath;
                var relativePath = file.Key;
                if (HashHelper.TryComputeFileHash(fullPath, out string fileHash))
                {
                    file.Value.Hash = fileHash;
                }
                else { 
                    LogHelper.LogError($"Failed to compute hash for file: {fullPath}", LogHelper.LogDebugLevel.None);
                    continue;
                }
                scannedManifestFiles[relativePath].Hash = fileHash;
                manifest.Files.Add(new ManifestFile
                {
                    RelativePath = file.Key,
                    MTime = file.Value.MTime,
                    Hash = file.Value.Hash,
                    Size = file.Value.Size,
                });
            }
            if (!string.IsNullOrEmpty(lastManifestFilePath) && System.IO.File.Exists(lastManifestFilePath))
            {
                var lastManifest = Utilities.Serializer.LoadFromFile<Manifest>(lastManifestFilePath);
                foreach (var file in lastManifest.Files)
                {
                    // 如果上一个版本的 manifest 中存在这个文件，但在当前扫描的 manifest 中不存在，说明这个文件需要被删除
                    if (scannedManifestFiles.ContainsKey(file.RelativePath) == false)
                    {
                        deletedFilesManifest.Files.Add(new ManifestFile
                        {
                            RelativePath = file.RelativePath,
                            MTime = file.MTime,
                            Hash = file.Hash,
                            Size = file.Size,
                        });
                    }
                }
            }
            Utilities.Serializer.SaveToFile($"{manifestSavePath}\\manifest-{version}.json", manifest);
            if (deletedFilesManifest.Files.Count > 0)
            {
                Utilities.Serializer.SaveToFile($"{manifestSavePath}\\manifest-deleted-{version}.json", deletedFilesManifest);
            }
        }
    }
}
