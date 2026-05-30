using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Models
{
    public class DownloadSessionProgress
    {
        public int TotalFiles { get; set; }

        public int CompletedFiles { get; set; }

        public int FailedFiles { get; set; }

        public long TotalDownloadedBytes { get; set; }

        public long TotalBytes { get; set; }

        public double TotalSpeedBytesPerSec { get; set; }

        public bool IsCompleted { get; set; }

        public IReadOnlyList<DownloadFileProgress> Files { get; set; }

        public double TotalProgressPercent =>
            TotalBytes <= 0
                ? 0
                : TotalDownloadedBytes * 100.0 / TotalBytes;
    }
}
