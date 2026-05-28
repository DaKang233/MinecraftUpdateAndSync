using MinecraftUpdateAndSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftUpdateAndSync.Utilities;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MinecraftUpdateAndSync.Services
{
    public class CompareService
    {
        public class CompareResult
        {
            public string RelativePath { get; set; }
            public FileState State { get; set; }
            public SyncAction Action { get; set; }
        }

        private static FileState CompareHash(string hash1, string hash2)
        {
            if (string.IsNullOrEmpty(hash1))
            {
                LogHelper.LogWarning($"Hash1 is null.", LogHelper.LogDebugLevel.Low);
                return FileState.InvalidLocal;
            }
            if (string.IsNullOrEmpty(hash2))
            {
                LogHelper.LogWarning($"Hash2 is null.", LogHelper.LogDebugLevel.Low);
                return FileState.InvalidRemote;
            }
            if (hash1.Equals(hash2, StringComparison.OrdinalIgnoreCase))
            {
                return FileState.Same;
            }
            return FileState.Different;
        }

        // Temporay not used, as we will compare file hash for MVP. We can consider to use file mtime for quick compare in the future, and only compare file hash when mtime is different.
        private static FileState CompareMTime(DateTime mTime1, DateTime mTime2)
        {
            if (mTime1.CompareTo(mTime2) == 0)
            {
                return FileState.Same;
            }
            return FileState.Different;
        }

        // We assume that the file name is the same, so there is no need to compare the file name.
        // Action will be handled in the other functions.
        public static CompareResult CompareFile(FileSnapshot localFile, ManifestFile remoteFile)
        {
            if (localFile == null || remoteFile == null)
            {
                LogHelper.LogWarning($"One or both file snapshots are null.", LogHelper.LogDebugLevel.None);
                return null;
            }
            var result = new CompareResult
            {
                RelativePath = remoteFile.RelativePath,
            };
            if (string.IsNullOrEmpty(localFile.FullPath) || !System.IO.File.Exists(localFile.FullPath))
            {
                LogHelper.LogError($"Local file is incomplete.", LogHelper.LogDebugLevel.None);
                result.State = FileState.InvalidLocal;
                return result;
            }
            if (string.IsNullOrEmpty(remoteFile.RelativePath) || string.IsNullOrEmpty(remoteFile.Hash) || remoteFile?.MTime == null)
            {
                LogHelper.LogError($"Remote file is incomplete.", LogHelper.LogDebugLevel.None);
                result.State = FileState.InvalidRemote;
                return result;
            }
            // compare file hash only for MVP; we can consider to compare file mtime for quick compare in the future, and only compare file hash when mtime is different.
            if (string.IsNullOrEmpty(localFile.Hash) && HashHelper.TryComputeFileHash(localFile.FullPath, out string hash1))
            {
                localFile.Hash = hash1;     // Cache the computed hash to avoid recomputing it in the future if needed.
                LogHelper.LogInfo($"Local file hash is computed: \nFile path: {localFile.FullPath}\nHash: {localFile.Hash}", LogHelper.LogDebugLevel.High);
            }
            result.State = CompareHash(localFile.Hash, remoteFile.Hash);
            LogHelper.LogInfo($"Local file hash compared with remote file hash: \n" +
                $"Local file path: {localFile.FullPath}\n" +
                $"Remote file path: {remoteFile.RelativePath}\n" +
                $"Local file hash: {localFile.Hash}\n" +
                $"Remote file hash: {remoteFile.Hash}\n" +
                $"Result: {result.State}", LogHelper.LogDebugLevel.High);
            return result;
        }

        private static SyncAction GetSyncAction(FileState state)
        {
            switch (state)
            {
                case FileState.Same:
                    return SyncAction.None;
                case FileState.MissingLocal:
                    return SyncAction.Download;
                case FileState.MissingRemote:
                    return SyncAction.Delete;
                case FileState.Different:
                    return SyncAction.Replace;
                case FileState.InvalidLocal:
                    return SyncAction.Replace;
                case FileState.Ignored:
                    return SyncAction.None;
                case FileState.InvalidRemote:
                    return SyncAction.Ignore;
                default:
                    return SyncAction.None;
            }
        }

        public static List<CompareResult> CompareManifestToDictLocalFiles(
            Manifest manifest,
            Dictionary<string, FileSnapshot> localFiles)
        {
            var compareResults = new List<CompareResult>();
            foreach (var remoteFile in manifest.Files)
            {
                if (localFiles.TryGetValue(remoteFile.RelativePath, out FileSnapshot localFile))
                {
                    var result = CompareFile(localFile, remoteFile);
                    if (result != null)
                    {
                        result.Action = GetSyncAction(result.State);
                        compareResults.Add(result);
                    }
                    else
                    {
                        LogHelper.LogError($"Compare file result is null for file, skipping: {remoteFile.RelativePath}", LogHelper.LogDebugLevel.None);
                    }
                }
                else
                {
                    // Local file is missing.
                    var result = new CompareResult
                    {
                        RelativePath = remoteFile.RelativePath,
                        State = FileState.MissingLocal,
                        Action = GetSyncAction(FileState.MissingLocal)
                    };
                    compareResults.Add(result);
                }
            }
            return compareResults;
        }

        public static List<CompareResult> CompareDeletedManifestToDictLocalFiles(
            Manifest deletedManifest,
            Dictionary<string, FileSnapshot> localFiles)
        {
            var compareResults = new List<CompareResult>();
            foreach (var remoteFile in deletedManifest.Files)
            {
                if (localFiles.TryGetValue(remoteFile.RelativePath, out FileSnapshot localFile))
                {
                    // Local file exists but it should be deleted.
                    var result = new CompareResult
                    {
                        RelativePath = remoteFile.RelativePath,
                        State = FileState.MissingRemote,
                        Action = GetSyncAction(FileState.MissingRemote)
                    };
                    compareResults.Add(result);
                }
                else
                {
                    // Local file is missing, which is expected for deleted files, so we can ignore this case.
                    LogHelper.LogInfo($"Local file is already missing for deleted file: {remoteFile.RelativePath}", LogHelper.LogDebugLevel.Low);
                }
            }
            return compareResults;
        }

        /// <summary>
        /// Compares two manifests and determines the differences between their files, including added, removed, and
        /// changed files.
        /// </summary>
        /// <remarks>The comparison treats the manifest with the higher version as the source of truth.
        /// The results indicate which files are unchanged, need to be replaced, downloaded, or deleted to synchronize
        /// the manifests.</remarks>
        /// <param name="manifest1">The first manifest to compare. Represents one version of the file set.</param>
        /// <param name="manifest2">The second manifest to compare. Represents the other version of the file set.</param>
        /// <returns>A list of compare results indicating the state and required synchronization action for each file. Returns
        /// null if the manifest versions are equal.</returns>
        public static List<CompareResult> CompareManifests(Manifest manifest1, Manifest manifest2)
        {
            var compareResults = new List<CompareResult>();
            var manifest1Version = manifest1.Version;
            var manifest2Version = manifest2.Version;
            Dictionary<string, ManifestFile> oldManifestDict = null;
            Dictionary<string, ManifestFile> newManifestDict = null;
            // newer as the target (manifest2 as target)
            if (Manifest.CompareVersions(manifest1Version, manifest2Version)) // manifest1Version is greater than manifest2Version
            {
                oldManifestDict = manifest2.Files.ToDictionary(f => f.RelativePath, f => f);
                newManifestDict = manifest1.Files.ToDictionary(f => f.RelativePath, f => f);
            }
            else if (Manifest.CompareVersions(manifest2Version, manifest1Version)) // manifest2Version is greater than manifest1Version
            {
                oldManifestDict = manifest1.Files.ToDictionary(f => f.RelativePath, f => f);
                newManifestDict = manifest2.Files.ToDictionary(f => f.RelativePath, f => f);
            }
            else // manifest1Version is equal to manifest2Version
            {
                return new List<CompareResult>();
            }
            
            foreach (var newFile in newManifestDict.Values)
            {
                var result = new CompareResult();
                if (oldManifestDict.TryGetValue(newFile.RelativePath, out ManifestFile manifest2File))
                {
                    // The file exists in both manifests, compare the file hash to see if the file is changed.
                    if (CompareHash(newFile.Hash, manifest2File.Hash) == FileState.Same)
                    {
                        result.RelativePath = newFile.RelativePath;
                        result.State = FileState.Same;
                        result.Action = GetSyncAction(result.State);
                    }
                    else
                    {
                        result.RelativePath = newFile.RelativePath;
                        result.State = FileState.Different;
                        result.Action = GetSyncAction(result.State); // The file is different, we need to replace the old file with the new file.
                    }
                }
                else
                {
                    // The file is missing in local manifest
                    result.RelativePath = newFile.RelativePath;
                    result.State = FileState.MissingLocal;
                    result.Action = GetSyncAction(result.State);
                }
                compareResults.Add(result);
            }
            foreach (var oldFile in oldManifestDict.Values)
            {
                if (!newManifestDict.TryGetValue(oldFile.RelativePath, out ManifestFile manifest2File))
                {
                    // The file is missing in remote manifest, which means the file is deleted in the newer manifest, we need to delete the local file.
                    var result = new CompareResult
                    {
                        RelativePath = oldFile.RelativePath,
                        State = FileState.MissingRemote,
                        Action = GetSyncAction(FileState.MissingRemote)
                    };
                    compareResults.Add(result);
                }
            }
            return compareResults;
        }
    }
}
