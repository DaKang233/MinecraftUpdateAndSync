using MinecraftUpdateAndSync.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace MinecraftUpdateAndSync.Core.Models
{
    public class Configuration
    {
        private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        private static ConfigurationData? _currentConfig;
        public const string INSTRUCTION_CONFIG_NAME = "instruction_config.json";
        public const string INSTRUCTION_CONFIG_URL = "https://furina.dakang233.com:8443/www/minecraft/instruction/"+INSTRUCTION_CONFIG_NAME;

        public static ConfigurationData Current
        {
            get
            {
                if (_currentConfig == null)
                {
                    _currentConfig = LoadConfiguration();
                }
                return _currentConfig;
            }
        }

        // 配置结构
        public class ConfigurationData
        {
            public string GameDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),".minecraft");
            public bool AutoUpdate { get; set; } = false;
            public string? InstructionConfigPath { get; set; }
            public string ServerAddress { get; set; } = "https://mc.dakang233.com";
        }

        public static ConfigurationData LoadConfiguration(IProgress<LogHelper.ProcessMessage>? progress = null)
        {
            if (!File.Exists(ConfigPath))
            {
                _currentConfig = new ConfigurationData();
                if (progress != null) LogHelper.Report(progress, "配置文件不存在，创建默认配置。", LogHelper.LogLevel.Warning);
                SaveConfiguration(_currentConfig);
                return _currentConfig;
            }

            try
            {
                string json = File.ReadAllText(ConfigPath);
                _currentConfig = JsonConvert.DeserializeObject<ConfigurationData>(json);
                return _currentConfig ?? throw new Exception("配置文件内容无效，无法解析为ConfigurationData对象。");
            }
            catch (Exception ex) 
            {
                if (progress != null) LogHelper.Report(progress, $"配置文件解析失败，使用默认配置。错误信息：{ex.Message}", LogHelper.LogLevel.Error);
                // 出错则使用默认配置
                _currentConfig = new ConfigurationData();
                SaveConfiguration(_currentConfig);
                return _currentConfig;
            }
        }

        public static void SaveConfiguration(ConfigurationData config)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(ConfigPath, json);
        }
    }
}
