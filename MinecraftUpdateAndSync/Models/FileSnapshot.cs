using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinecraftUpdateAndSync.Utilities.Utilities;


namespace MinecraftUpdateAndSync.Models
{
    public class FileSnapshot
    {
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
        public DateTime MTime { get; set; }
        public string Hash { get; set; }
        public long Size { get; set; }
    }
}

