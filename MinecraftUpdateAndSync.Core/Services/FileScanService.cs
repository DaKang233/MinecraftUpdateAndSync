using MinecraftUpdateAndSync.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftUpdateAndSync.Core.Utilities;

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
        /// <param name="appliedRuleDirectories">An array of directory paths used to filter files during the scan. If null or empty, all files are included.
        /// Paths can be absolute or relative to the scanned directory.</param>
        /// <returns>A dictionary mapping each file's relative path to its corresponding FileSnapshot. The dictionary will be
        /// empty if no files match the criteria.</returns>
        public static Dictionary<string, FileSnapshot> ScanDirectory(
            string directoryPath,
            ScanMode scanMode = ScanMode.Exclude,
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
            foreach (var file in directoryFiles)
            {
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
