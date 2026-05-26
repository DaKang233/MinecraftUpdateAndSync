using Newtonsoft.Json; // 推荐使用 Newtonsoft.Json（.NET Framework兼容性好）
using System;
using System.IO;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace MinecraftUpdateAndSync.Models
{
    public class Configuration
    {
        private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        private static ConfigurationData _currentConfig;

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
            public string GameDirectory { get; set; } = @"C:\Minecraft";
            public bool AutoUpdate { get; set; } = true;
            public string LastVersion { get; set; } = "1.0.0";
        }

        public static ConfigurationData LoadConfiguration()
        {
            if (!File.Exists(ConfigPath))
            {
                _currentConfig = new ConfigurationData();
                SaveConfiguration(_currentConfig);
                return _currentConfig;
            }

            try
            {
                string json = File.ReadAllText(ConfigPath);
                _currentConfig = JsonConvert.DeserializeObject<ConfigurationData>(json);
                return _currentConfig;
            }
            catch
            {
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