using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using MinecraftUpdateAndSync.Models;
using MinecraftUpdateAndSync.PublishTool.Models;
using MinecraftUpdateAndSync.Services;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (MessageBox.Show("确定要退出吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Log("程序已退出。");
            SaveConfig();
        }

        private enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        private void FolderChooseDialog(string description, TextBox textBox)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = description;
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void FileChooseDialog(string description, TextBox textBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = description;
                openFileDialog.Filter = "JSON 文件 (*.json)|*.json|所有文件 (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void Log(
            string message, 
            LogLevel level = LogLevel.Info, 
            [CallerMemberName] string memberName = "", 
            [CallerFilePath] string filePath = "")
        {
            switch (level)
            {
                case LogLevel.Info:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {message}\n");
                    LogHelper.LogInfo(message, LogHelper.LogDebugLevel.None, memberName, filePath);
                    break;
                case LogLevel.Warning:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [WARNING] {message}\n");
                    LogHelper.LogWarning(message, LogHelper.LogDebugLevel.None, memberName, filePath);
                    break;
                case LogLevel.Error:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] {message}\n");
                    LogHelper.LogError(message, LogHelper.LogDebugLevel.None, memberName, filePath);
                    break;
            }
        }

        private void LoadScanMode(FileScanService.ScanMode mode)
        {
            switch (mode)
            {
                case FileScanService.ScanMode.Include:
                    radioBtnIncludeMode.Checked = true;
                    break;
                case FileScanService.ScanMode.Exclude:
                    radioBtnExcludeMode.Checked = true;
                    break;
                default:
                    radioBtnExcludeMode.Checked = true;
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
                    var config = Serializer.LoadFromFile<Models.AppConfiguration>(configPath);
                    if (config != null)
                    {
                        textBoxCurrentVersion.Text = config.CurrentVersion ?? "";
                        textBoxIgnoreDirectories.Text = string.Join(";", config.IgnoredDirectories);
                        textBoxLastManifestPath.Text = config.LastManifestSavePath ?? "";
                        textBoxManifestSaveDirectory.Text = config.ManifestSaveDirectory ?? "";
                        textBoxMinecraftDirectory.Text = config.MinecraftPath ?? "";
                        textBoxIncludeDirectories.Text = string.Join(";", config.IncludeDirectories); // 新增字段
                        LoadScanMode(config.ScanMode);
                        Log("配置文件已加载。");
                    }
                    else Log("配置文件 publish_config.json 格式不正确，无法加载。", LogLevel.Error);
                }
                else Log("未找到配置文件 publish_config.json，请确保它与程序在同一目录下。", LogLevel.Warning);
            }
            catch (Exception ex) { Log($"加载配置文件时发生错误: {ex.Message}", LogLevel.Error); }
        }

        private void SaveConfig()
        {
            try
            {
                var configPath = System.IO.Path.Combine(AppContext.BaseDirectory, "publish_config.json");
                var config = new Models.AppConfiguration
                {
                    CurrentVersion = textBoxCurrentVersion.Text,
                    IgnoredDirectories = textBoxIgnoreDirectories.Text.Split(';'),
                    LastManifestSavePath = textBoxLastManifestPath.Text,
                    ManifestSaveDirectory = textBoxManifestSaveDirectory.Text,
                    MinecraftPath = textBoxMinecraftDirectory.Text,
                    ScanMode = GetScanMode(),
                    IncludeDirectories = textBoxIncludeDirectories.Text.Split(';') // 新增字段
                };
                Serializer.SaveToFile(configPath, config);
                Log("配置文件已保存。");
            }
            catch (Exception ex)
            {
                Log($"保存配置文件时发生错误: {ex.Message}", LogLevel.Error);
            }
        }

        private void buttonReloadConfig_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void buttonBrowseManifestSavePath_Click(object sender, EventArgs e)
        {
            FolderChooseDialog("选择清单文件保存目录", textBoxManifestSaveDirectory);
        }

        private void buttonBrowseMinecraftDirectory_Click(object sender, EventArgs e)
        {
            FolderChooseDialog("选择Minecraft安装目录", textBoxMinecraftDirectory);
        }

        private void buttonBrowseLastManifestPath_Click(object sender, EventArgs e)
        {
            FileChooseDialog("选择上一个清单文件所在目录", textBoxLastManifestPath);
        }

        private void buttonIgnoreDirectories_Click(object sender, EventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Multiselect = true;
                dialog.Title = "选择要忽略的目录";
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var selectedPaths = dialog.FileNames;
                    textBoxIgnoreDirectories.Text = string.Join(";", selectedPaths);
                }
            }
        }

        private async void buttonGenerateManifest_Click(object sender, EventArgs e)
        {
            Log("正在生成清单文件...");
            try
            {
                buttonGenerateManifest.Enabled = false;
                var progress = new Progress<int>(p => { progressBar.Value = p; Log($"生成进度: {p}%"); labelPercent.Text = $"{p}%"; });
                await GenerateManifest(progress);
                Log("清单文件已生成: " + System.IO.Path.Combine(textBoxManifestSaveDirectory.Text, $"manifest-{textBoxCurrentVersion.Text}.json"));
            }
            catch (Exception ex) { Log("清单文件生成时遇到错误:"+ex.Message); }
            finally
            {
                buttonGenerateManifest.Enabled = true;
            }
        }

        private FileScanService.ScanMode GetScanMode()
        {
            if (radioBtnIncludeMode.Checked) return FileScanService.ScanMode.Include;
            if (radioBtnExcludeMode.Checked) return FileScanService.ScanMode.Exclude;
            return FileScanService.ScanMode.Exclude; // 默认
        }

        private async Task GenerateManifest(Progress<int> progress)
        {
            await Task.Run(() =>
            {
                ManifestGenerator.GenerateManifest(
                    textBoxMinecraftDirectory.Text,
                    textBoxManifestSaveDirectory.Text,
                    textBoxCurrentVersion.Text,
                    textBoxLastManifestPath.Text,
                    GetScanMode(),
                    progress,
                    GetScanMode() == FileScanService.ScanMode.Exclude ? textBoxIgnoreDirectories.Text.Split(';') : textBoxIncludeDirectories.Text.Split(';')
                );
            });
        }

        private void buttonBrowseIncludeDirectories_Click(object sender, EventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Multiselect = true;
                dialog.Title = "选择要包含的目录";
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var selectedPaths = dialog.FileNames;
                    textBoxIncludeDirectories.Text = string.Join(";", selectedPaths);
                }
            }
        }
    }
}
