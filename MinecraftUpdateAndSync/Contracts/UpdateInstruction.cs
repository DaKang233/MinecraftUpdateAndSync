using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.Contracts
{
    public enum Protocol
    {
        Http,
        Https,
        Ftp,
        Sftp
    }
    public class UpdateInstruction
    {
        public bool AllowDelete { get; set; }
        public string ManifestPath { get; set; }
        public string Prefix { get; set; }
        public Protocol Protocol { get; set; }
        public string ResourceRelativeDirectory { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string Version { get; set; }
    }
}
