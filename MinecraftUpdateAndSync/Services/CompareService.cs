using MinecraftUpdateAndSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftUpdateAndSync.Utilities;

namespace MinecraftUpdateAndSync.Services
{
    public class CompareService
    {
        public class CompareResult
        {
            public string RelativePath { get; set; }
            public FileStatus Status { get; set; }
        }

        public static FileStatus CompareHash(string hash1, string hash2)
        {
            if (string.IsNullOrEmpty(hash1) || string.IsNullOrEmpty(hash2))
            {
                System.Console.WriteLine($"[WARNING] One or both hashes are null or empty: {hash1}, {hash2}.");
                throw new ArgumentNullException("Hash values cannot be null or empty.");
            }
            if (hash1.Equals(hash2, StringComparison.OrdinalIgnoreCase))
            {
                return FileStatus.Same;
            }
            return FileStatus.Different;
        }

        public static FileStatus CompareMTime(DateTime mTime1, DateTime mTime2)
        {
            if (mTime1.CompareTo(mTime2) == 0)
            {
                return FileStatus.Same;
            }
            return FileStatus.Different;
        }

        // We assume that the file name is the same, so there is no need to compare the file name.
        public static CompareResult CompareFile(FileSnapshot localFile, ManifestFile remoteFile)
        {
            if (localFile == null || remoteFile == null)
            {
                Console.WriteLine($"[WARNING] One or both file snapshots or manifest files are null.");
                throw new ArgumentNullException("File snapshot or manifest file cannot be null.");
            }
            var result = new CompareResult
            {
                RelativePath = remoteFile.RelativePath,
            };
            if (string.IsNullOrEmpty(localFile.FullPath) || localFile.MTime == null || localFile.Size == 0)
            {
                throw new ArgumentException("Local file snapshot is incomplete.");
            }
            if (string.IsNullOrEmpty(remoteFile.RelativePath) || string.IsNullOrEmpty(remoteFile.Hash) || remoteFile.MTime == null)
            {
                throw new ArgumentException("Manifest file is incomplete.");
            }
            // compare file hash only for MVP
            if (string.IsNullOrEmpty(localFile.Hash))
            {
                localFile.Hash = HashHelper.ComputeFileHash(localFile.FullPath);
            }
            if (localFile.Hash.Equals(remoteFile.Hash, StringComparison.OrdinalIgnoreCase))
            {
                result.Status = FileStatus.Same;
            }
            else result.Status = FileStatus.Different;
            return result;
        }

        public static List<CompareResult> CompareManifest(
            Manifest manifest,
            Dictionary<string, FileSnapshot> localFiles)
        {

        }
    }
}
