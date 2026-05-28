using MinecraftUpdateAndSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftUpdateAndSync.Utilities;

namespace MinecraftUpdateAndSync.Services
{
    public class FileScanService
    {
        /// <summary>
        /// 扫描指定目录，并返回一个字典，其中包含所找到的所有文件的文件快照，
        /// 排除那些位于指定忽略目录中的文件。
        /// </summary>
        /// <remarks>位于任何指定忽略目录或其子目录中的文件将从结果中排除。
        /// 目录匹配不区分大小写，并使用标准化后的路径，路径中的斜线为正斜杠。</remarks>
        /// <param name="directoryPath">要扫描文件的目录的完整路径。不能为 null 或空。</param>
        /// <param name="ignoreDirectories">一个目录路径数组，这些路径相对于根目录，将在扫描中被忽略。
        /// 如果为 null，则不忽略任何目录。目录路径比较不区分大小写，应使用正斜杠。</param>
        /// <returns>一个字典，将每个文件的相对路径映射到其对应的文件快照。
        /// 如果没有找到任何文件，则该字典为空。</returns>
        public static Dictionary<string, FileSnapshot> ScanDirectory(string directoryPath, string[] ignoreDirectories = null)
        {
            var directoryFiles = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            var fileSnapshots = new Dictionary<string, FileSnapshot>();
            foreach (var file in directoryFiles)
            {
                var fileInfo = new FileInfo(file);
                var relativePath = PathHelper.GetRelativePath(directoryPath, fileInfo.FullName);
                var normalizedRelativePath = relativePath.Replace('\\', '/');
                var normalizedIgnoreDirs =
                    ignoreDirectories?
                        .Select(d => d.Replace('\\', '/').TrimEnd('/') + "/")
                        .ToArray();
                var fileSnapshot = new FileSnapshot
                {
                    FullPath = file,
                    RelativePath = relativePath,
                    MTime = fileInfo.LastWriteTimeUtc,
                    // Hash = ComputeFileHash(file), // Implement this method to compute the file hash if needed
                    Size = fileInfo.Length
                };
                if (normalizedIgnoreDirs != null &&
                    normalizedIgnoreDirs.Any(dir =>
                        normalizedRelativePath.StartsWith(
                            dir,
                            StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }
                fileSnapshots[relativePath] = fileSnapshot;
            }
            return fileSnapshots;
        }

        public static Manifest GetLocalFileManifest(Dictionary<string, FileSnapshot> files, string version)
        {
            var manifest = new Manifest();
            
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
