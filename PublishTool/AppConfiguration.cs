using MinecraftUpdateAndSync.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdateAndSync.PublishTool.Models
{
    class AppConfiguration
    {
        // 生成 Manifest 文件配置项
        public string MinecraftPath { get; set; } = string.Empty;
        public string[] IgnoredDirectories { get; set; }
        public string LastManifestSavePath { get; set; }
        public string ManifestSaveDirectory { get; set; }
        public string CurrentVersion { get; set; } = string.Empty;
        public FileScanService.ScanMode ScanMode { get; set; } = FileScanService.ScanMode.Exclude;
        public string[] IncludeDirectories { get; set; }

        // 指令文件配置项
        public Contracts.UpdateInstruction Instruction { get; set; }
        public string InstructionConfigPath { get; set; }
        public string FileServerRootPath { get; set; }
    }
}

