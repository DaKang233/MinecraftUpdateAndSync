using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinecraftUpdateAndSync.Core.Utilities.Utilities;


namespace MinecraftUpdateAndSync.Core.Models
{
    public class FileSnapshot
    {
        public required string FullPath { get; set; }
        public required string RelativePath { get; set; }
        public DateTime MTime { get; set; }
        public required string Hash { get; set; }
        public long Size { get; set; }
    }
}

