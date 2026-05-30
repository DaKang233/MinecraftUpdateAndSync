using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Core.Models
{
    public enum DownloadState
    {
        Pending,
        Downloading,
        Completed,
        Failed,
        Cancelled
    }
}

