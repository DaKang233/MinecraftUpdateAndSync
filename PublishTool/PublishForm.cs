using Microsoft.WindowsAPICodePack.Dialogs;
using MinecraftUpdateAndSync.Contracts;
using MinecraftUpdateAndSync.Models;
using MinecraftUpdateAndSync.PublishTool.Models;
using MinecraftUpdateAndSync.Services;
using MinecraftUpdateAndSync.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MinecraftUpdateAndSync.Utilities.LogHelper;

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
            /*
            if (MessageBox.Show("确定要退出吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                e.Cancel = true;
            }
            */
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Log("程序已退出。");
            SaveConfig();
        }

        private void FolderChooseDialog(string description, TextBox textBox)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Title = description;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    textBox.Text = dialog.FileName;
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

        private void LoadProtocol(Protocol protocol)
        {
            switch (protocol)
            {
                case Protocol.Http:
                    radioBtnHttpProtocol.Checked = true;
                    break;
                case Protocol.Https:
                    radioBtnHttpsProtocol.Checked = true;
                    break;
                case Protocol.Ftp:
                    radioBtnFtpProtocol.Checked = true;
                    break;
                case Protocol.Sftp:
                    radioBtnSftpProtocol.Checked = true;
                    break;
                default:
                    radioBtnHttpsProtocol.Checked = true;
                    break;
            }
        }

        private Protocol GetSelectedProtocol()
        {
            if (radioBtnHttpProtocol.Checked) return Protocol.Http;
            if (radioBtnHttpsProtocol.Checked) return Protocol.Https;
            if (radioBtnFtpProtocol.Checked) return Protocol.Ftp;
            if (radioBtnSftpProtocol.Checked) return Protocol.Sftp;
            return Protocol.Https; // 默认
        }

        private void LoadConfig()
        {
            try
            {
                var configPath = System.IO.Path.Combine(AppContext.BaseDirectory, "publish_config.json");
                if (System.IO.File.Exists(configPath))
                {
                    var config = Serializer.LoadFromFile<Models.AppConfiguration>(configPath);
                    var instructionConfig = config.Instruction ?? new UpdateInstruction();
                    if (config != null)
                    {
                        textBoxCurrentVersion.Text = config.CurrentVersion ?? "";
                        textBoxIgnoreDirectories.Text = string.Join(";", config.IgnoredDirectories);
                        textBoxLastManifestPath.Text = config.LastManifestSavePath ?? "";
                        textBoxManifestSaveDirectory.Text = config.ManifestSaveDirectory ?? "";
                        textBoxMinecraftDirectory.Text = config.MinecraftPath ?? "";
                        textBoxIncludeDirectories.Text = string.Join(";", config.IncludeDirectories); // 新增字段
                        LoadScanMode(config.ScanMode);

                        // 加载指导文件相关配置
                        textBoxFileServerRootPath.Text = config.FileServerRootPath ?? "";
                        textBoxInstructionConfigPath.Text = config.InstructionConfigPath ?? "";

                        textBoxInstructionVersion.Text = instructionConfig.Version ?? "";
                        textBoxPrefix.Text = instructionConfig.Prefix ?? "";
                        textBoxResourceRelativeDirectory.Text = instructionConfig.ResourceRelativeDirectory ?? "";
                        textBoxServerAddress.Text = instructionConfig.ServerAddress ?? "";
                        textBoxServerPort.Text = instructionConfig.ServerPort.ToString() ?? "";
                        checkBoxAllowDeletion.Checked = instructionConfig.AllowDelete;
                        LoadProtocol(instructionConfig.Protocol);
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
                    IncludeDirectories = textBoxIncludeDirectories.Text.Split(';'), // 新增字段
                    InstructionConfigPath = textBoxInstructionConfigPath.Text,
                    FileServerRootPath = textBoxFileServerRootPath.Text,
                    Instruction = new UpdateInstruction
                    {
                        Version = textBoxInstructionVersion.Text,
                        Prefix = textBoxPrefix.Text,
                        ResourceRelativeDirectory = textBoxResourceRelativeDirectory.Text,
                        ServerAddress = textBoxServerAddress.Text,
                        ServerPort = int.TryParse(textBoxServerPort.Text, out int port) ? port : 0,
                        AllowDelete = checkBoxAllowDeletion.Checked,
                        Protocol = GetSelectedProtocol()
                    }
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
            FileChooseDialog("选择上一个清单文件", textBoxLastManifestPath);
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
            if (string.IsNullOrEmpty(textBoxManifestSaveDirectory.Text)) 
            { Log("请指定 Manifest 保存目录。",LogLevel.Warning); return; };
            if (string.IsNullOrEmpty(textBoxMinecraftDirectory.Text)) 
            { Log("请指定 Minecraft 安装目录。", LogLevel.Warning); return; };
            if (string.IsNullOrEmpty(textBoxCurrentVersion.Text)) 
            { Log("请指定当前版本号。", LogLevel.Warning); return; };
            if (Version.TryParse(textBoxCurrentVersion.Text, out var version) == false) 
            { Log("版本号格式不正确，请输入有效的版本号。", LogLevel.Warning); return; }
            Log("正在生成清单文件...");
            try
            {
                buttonGenerateManifest.Enabled = false;
                var progress = new Progress<int>(p => { progressBar.Value = p; Log($"生成进度: {p}%"); labelPercent.Text = $"{p}%"; });
                await GenerateManifestAsync(progress);
                Log("清单文件已生成: " + System.IO.Path.Combine(textBoxManifestSaveDirectory.Text, $"manifest-{textBoxCurrentVersion.Text}.json"));
            }
            catch (Exception ex) { Log("清单文件生成时遇到错误:" + ex.Message); }
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

        private async Task GenerateManifestAsync(Progress<int> progress)
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

        private void buttonInstructionConfigPath_Click(object sender, EventArgs e)
        {
            FolderChooseDialog("选择指导文件配置路径", textBoxInstructionConfigPath);
        }

        private void buttonFileServerPathBrowse_Click(object sender, EventArgs e)
        {
            FolderChooseDialog("选择文件服务器路径", textBoxFileServerRootPath);
        }

        private void buttonResourceRelativeDirectoryBrowse_Click(object sender, EventArgs e)
        {
            FolderChooseDialog("选择资源相对目录", textBoxResourceRelativeDirectory);
        }

        private async void buttonGenerateInstruction_Click(object sender, EventArgs e)
        {
            var version = textBoxInstructionVersion.Text;
            var prefix = textBoxPrefix.Text;
            var resourceRelativeDirectory = textBoxResourceRelativeDirectory.Text;
            var serverAddress = textBoxServerAddress.Text;
            var serverPortText = textBoxServerPort.Text;
            var fileServerRootPath = textBoxFileServerRootPath.Text;
            var instructionConfigPath = textBoxInstructionConfigPath.Text;
            if (string.IsNullOrEmpty(version) || 
                string.IsNullOrEmpty(prefix) || 
                string.IsNullOrEmpty(resourceRelativeDirectory) || 
                string.IsNullOrEmpty(serverAddress) || 
                string.IsNullOrEmpty(serverPortText) || 
                string.IsNullOrEmpty(fileServerRootPath) ||
                string.IsNullOrEmpty(instructionConfigPath))
            {
                Log("请确保所有指导文件配置项都已填写。", LogLevel.Warning);
                return;
            }
            try
            {
                await GenerateInstructionAsync();
                Log($"指导文件已生成到目录下:{instructionConfigPath}", LogLevel.Info);
            }
            catch (Exception ex) { Log($"生成指导文件时出错:{ex.Message}", LogLevel.Error); }
        }

        private async Task GenerateInstructionAsync()
        {
            var rootPath = textBoxFileServerRootPath.Text;
            var resourceRelativeDirectory = textBoxResourceRelativeDirectory.Text;
            var version = textBoxInstructionVersion.Text;
            await Task.Run(() =>
            {
                var instructionConfig = new UpdateInstruction()
                {
                    Version = textBoxInstructionVersion.Text,
                    Prefix = textBoxPrefix.Text,
                    ResourceRelativeDirectory = PathHelper.GetNormalizedPath(PathHelper.GetRelativePath(rootPath,resourceRelativeDirectory)),
                    ServerAddress = textBoxServerAddress.Text,
                    ServerPort = int.TryParse(textBoxServerPort.Text, out int port) ? port : 0,
                    AllowDelete = checkBoxAllowDeletion.Checked,
                    Protocol = GetSelectedProtocol(),
                    ManifestPath = PathHelper.GetRelativePath(rootPath,textBoxManifestPath.Text)
                };
                if (!Directory.Exists(textBoxInstructionConfigPath.Text)) Directory.CreateDirectory(textBoxInstructionConfigPath.Text);
                Serializer.SaveToFile(System.IO.Path.Combine(textBoxInstructionConfigPath.Text,Configuration.INSTRUCTION_CONFIG_NAME), instructionConfig);
            });
        }

        private void buttonManifestDirectoryBrowse_Click(object sender, EventArgs e)
        {
            FileChooseDialog("选择清单文件保存目录", textBoxManifestPath);
        }
    }
}
