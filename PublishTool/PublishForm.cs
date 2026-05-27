using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinecraftUpdateAndSync.Models;
using MinecraftUpdateAndSync.Utilities;

namespace MinecraftUpdateAndSync.PublishTool
{
    public partial class PublishForm : Form
    {
        public PublishForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadConfig();
        }

        private enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        private void Log(string message, LogLevel level = LogLevel.Info)
        {
            switch (level)
            {
                case LogLevel.Info:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {message}\n");
                    break;
                case LogLevel.Warning:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [WARNING] {message}\n");
                    break;
                case LogLevel.Error:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] {message}\n");
                    break;
            }
        }

        private void LoadConfig()
        {
            try
            {
                var configPath = System.IO.Path.Combine(AppContext.BaseDirectory, "publish_config.json");
                if (System.IO.File.Exists(configPath))
                {
                    var configText = System.IO.File.ReadAllText(configPath);
                    var config = Serializer.LoadFromFile<Models.AppConfiguration>(configText);
                    textBoxCurrentVersion.Text = config.CurrentVersion;
                    textBoxIgnoreDirectories.Text = string.Join(";", config.IgnoredDirectories);
                    textBoxLastManifestPath.Text = config.LastManifestSaveDirectory;
                    textBoxManifestSavePath.Text = config.ManifestSaveDirectory;
                    textBoxLastVersion.Text = config.LastManifestSaveVersion;
                    textBoxMinecraftDirectory.Text = config.MinecraftPath;
                }
                else Log("未找到配置文件 publish_config.json，请确保它与程序在同一目录下。", LogLevel.Warning);
            }
            catch (Exception ex) { Log($"加载配置文件时发生错误: {ex.Message}", LogLevel.Error); }
        }

        private void SaveConfig()
        {
            var configPath = System.IO.Path.Combine(AppContext.BaseDirectory, "publish_config.json");
            var config = new Models.AppConfiguration
            {
                CurrentVersion = textBoxCurrentVersion.Text,
                IgnoredDirectories = textBoxIgnoreDirectories.Text.Split(';'),
                LastManifestSaveDirectory = textBoxLastManifestPath.Text,
                ManifestSaveDirectory = textBoxManifestSavePath.Text,
                LastManifestSaveVersion = textBoxLastVersion.Text,
                MinecraftPath = textBoxMinecraftDirectory.Text
            };
            Serializer.SaveToFile(configPath,config);
        }

        private void buttonReloadConfig_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Log("配置文件已保存。");
        }
    }
}
