using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.PublishTool.Models
{
    class PublishTask
    {
        public string Name { get; set; }

        public string OutputPath { get; set; }

        public string Mode { get; set; }

        public string Type { get; set; }

        public string Version { get; set; }

        public string VersionFile { get; set; }
    }
    class ConfigurationModel
    {
        public string MinecraftPath { get; set; }

        public string LogPath { get; set; }

        public string[] IgnoredDirectories { get; set; }

        public string[] IgnoredFiles { get; set; }

        public List<PublishTask> Tasks { get; set; }
    }
}
