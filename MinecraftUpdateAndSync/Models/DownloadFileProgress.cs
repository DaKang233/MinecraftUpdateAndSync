using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Models
{
    public class DownloadFileProgress
    {
        public string FileName { get; set; }

        public string Url { get; set; }

        public string SavePath { get; set; }

        public DownloadState State { get; set; }

        public long DownloadedBytes { get; set; }

        public long TotalBytes { get; set; }

        public double SpeedBytesPerSec { get; set; }

        public string ErrorMessage { get; set; }

        public double ProgressPercent =>
            TotalBytes <= 0
                ? 0
                : DownloadedBytes * 100.0 / TotalBytes;
    }
}
