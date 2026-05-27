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
    class AppConfiguration
    {
        public string MinecraftPath { get; set; }

        public string LogPath { get; set; }

        public string[] IgnoredDirectories { get; set; }
        public string LastManifestSaveDirectory { get; set; }
        public string ManifestSaveDirectory { get; set; }
        public string CurrentVersion { get; set; }
        public string LastManifestSaveVersion { get; set; }
    }
}
