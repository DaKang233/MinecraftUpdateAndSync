using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using MinecraftUpdateAndSync.Core.Contracts;
using MinecraftUpdateAndSync.Core.Models;
using MinecraftUpdateAndSync.Core.Services;
using MinecraftUpdateAndSync.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MinecraftUpdateAndSync.Core.Utilities.LogHelper;

namespace MinecraftUpdateAndSync
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            buttonCancel.Enabled = false;
            LoadConfig();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            SaveConfig();
            LogHelper.LogInfo("应用程序已关闭。", LogHelper.LogDebugLevel.None);
            LogHelper.LogInfo("配置文件已保存。", LogHelper.LogDebugLevel.None);
        }

        /// <summary>
        /// 全局取消操作的令牌源。
        /// </summary>
        CancellationTokenSource? GlobalCTS;

        private void Log(
            string message,
            LogLevel level = LogLevel.Info,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            bool AppendLogOnly = false)
        {
            switch (level)
            {
                case LogLevel.Info:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {message}\n");
                    if (!AppendLogOnly) LogHelper.LogInfo(message, LogHelper.LogDebugLevel.None, memberName, filePath);
                    break;
                case LogLevel.Warning:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [WARNING] {message}\n");
                    if (!AppendLogOnly) LogHelper.LogWarning(message, LogHelper.LogDebugLevel.None, memberName, filePath);
                    break;
                case LogLevel.Error:
                    richTextBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] {message}\n");
                    if (!AppendLogOnly) LogHelper.LogError(message, LogHelper.LogDebugLevel.None, memberName, filePath);
                    break;
            }
        }

        private void BrowseDirectory(string desc, TextBox textBox)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Title = desc;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    textBox.Text = dialog.FileName;
                }
            }
        }

        private void BrowseFile(string desc, TextBox textBox, string filter, string filterDesc)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = false;
                dialog.Title = desc;
                dialog.Filters.Add(new CommonFileDialogFilter(filterDesc, filter));
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    textBox.Text = dialog.FileName;
                }
            }
        }

        private void buttonBrowseGameDirectory_Click(object sender, EventArgs e)
        {
            BrowseDirectory("选择 Minecraft 游戏目录", textBoxGameDirectory);
        }

        /// <summary>
        /// 释放全局取消令牌源，并重置相关按钮状态，禁用取消按钮。
        /// </summary>
        /// <param name="button">要恢复启用的按钮控件。</param>
        private void DisposeCTS(Button button)
        {
            GlobalCTS?.Dispose();
            GlobalCTS = null;
            button.Enabled = true;
            buttonCancel.Enabled = false;
        }

        private void LoadConfig()
        {
            var progress = new Progress<ProcessMessage>(message =>
            {
                Log(message.Message, message.Level, message.MemberName, message.FilePath, true);
            });
            var config = Configuration.LoadConfiguration(progress);
            textBoxClientInstructionPath.Text = config.InstructionConfigPath;
            checkBoxIsAutoUpdated.Checked = config.AutoUpdate;
            textBoxGameDirectory.Text = config.GameDirectory;
            textBoxServerAddress.Text = config.ServerAddress;
            textBoxLocalFileManifest.Text = config.LocalManifestPath;
            Log("配置文件已加载。");
        }

        private void SaveConfig()
        {
            var config = new Configuration.ConfigurationData();
            config.InstructionConfigPath = textBoxClientInstructionPath.Text;
            config.AutoUpdate = checkBoxIsAutoUpdated.Checked;
            config.GameDirectory = textBoxGameDirectory.Text;
            config.ServerAddress = textBoxServerAddress.Text;
            config.LocalManifestPath = textBoxLocalFileManifest.Text;
            Configuration.SaveConfiguration(config);
            Log("配置文件已保存。");
        }

        private void buttonReloadConfig_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();
            }
            catch (Exception ex)
            {
                Log($"保存配置文件时出错：{ex.Message}", LogLevel.Error);
            }
        }

        private void buttonBrowseClientInstruction_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON 文件 (*.json)|*.json";
                openFileDialog.Title = "选择客户端指令文件";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxClientInstructionPath.Text = openFileDialog.FileName;
                }
            }
        }

        private async Task DownloadInstructionConfig(CancellationToken cancellationToken, string filePath)
        {
            await HttpHelper.DownloadFileAsync(
                Configuration.INSTRUCTION_CONFIG_URL,
                filePath,
                cancellationToken,
                new Progress<int>(progress =>
                {
                    Log($"下载客户端指令配置文件：{progress:F2}%");
                }));
        }

        private async void buttonDownloadClientConfig_Click(object sender, EventArgs e)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = System.IO.Path.Combine(currentDirectory, Configuration.INSTRUCTION_CONFIG_NAME);
            GlobalCTS = new CancellationTokenSource();
            buttonDownloadClientConfig.Enabled = false;
            buttonCancel.Enabled = true;
            try
            {
                await DownloadInstructionConfig(GlobalCTS.Token, filePath);
                Log("下载客户端指令配置文件已完成。");
                textBoxClientInstructionPath.Text = filePath;
            }
            catch (OperationCanceledException)
            {
                Log("下载客户端指令配置文件已取消。", LogLevel.Warning);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                        Log("已删除未完成的客户端指令配置文件。");
                    }
                    catch (Exception ex)
                    {
                        Log($"删除未完成的客户端指令配置文件时出错：{ex.Message}", LogLevel.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"下载客户端指令配置文件时出错：{ex.Message}", LogLevel.Error);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                        Log($"已删除未完成的客户端指令配置 {filePath}。");
                    }
                    catch (Exception deleteEx)
                    {
                        Log($"删除未完成的客户端指令配置文件时出错：{deleteEx.Message}", LogLevel.Error);
                    }
                }
            }
            finally { DisposeCTS(buttonDownloadClientConfig); }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (GlobalCTS == null)
            {
                Log("没有正在进行的操作需要取消。", LogLevel.Warning);
                return;
            }
            var result = MessageBox.Show("确定要取消当前操作吗？", "确认取消", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                GlobalCTS?.Cancel();
                buttonCancel.Enabled = false;
            }
        }

        private void buttonLocalFileManifest_Click(object sender, EventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.Title = "选择本地文件清单保存目录";
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var savePath = System.IO.Path.Combine(dialog.FileName, Configuration.LOCAL_FILE_MANIFEST_NAME);
                    textBoxLocalFileManifest.Text = savePath;
                }
            }
        }

        private async Task<Dictionary<string, FileSnapshot>> ScanLocalFiles(
            string directoryPath,
            Progress<int> progress,
            CancellationToken cancellationToken)
        {
            var scannedFiles = new Dictionary<string, FileSnapshot>();
            await Task.Run(() =>
            {
                scannedFiles = FileScanService.ScanDirectory(directoryPath, FileScanService.ScanMode.Exclude, progress, cancellationToken);
            });
            return scannedFiles;
        }

        private async void buttonScanLocalFiles_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxGameDirectory.Text) || !System.IO.Directory.Exists(textBoxGameDirectory.Text))
            {
                Log("请先选择有效的游戏目录。", LogLevel.Warning);
                return;
            }
            if (string.IsNullOrEmpty(textBoxLocalFileManifest.Text) || !textBoxLocalFileManifest.Text.EndsWith(".json"))
            {
                Log("请先输入本地文件清单的有效保存路径。", LogLevel.Warning);
                return;
            }

            progressBarScan.Value = 0;
            buttonScanLocalFiles.Enabled = false;
            buttonCancel.Enabled = true;
            GlobalCTS = new CancellationTokenSource();
            var cancellationToken = GlobalCTS.Token;
            labelScanPercent.Text = "0%";

            var progress = new Progress<int>(percent =>
            {
                Log($"扫描文件中... {percent:F2}%");
                progressBarScan.Value = percent;
                labelScanPercent.Text = $"{percent:F2}%";
            });
            try
            {
                var scannedFiles = await ScanLocalFiles(textBoxGameDirectory.Text, progress, cancellationToken);
                Serializer.SaveToFile(textBoxLocalFileManifest.Text, scannedFiles);
                Log($"扫描完成，共扫描到 {scannedFiles.Count} 个文件。文件清单已保存到 {textBoxLocalFileManifest.Text}");
                progressBarScan.Value = 100;
                labelScanPercent.Text = "100%";
            }
            catch (OperationCanceledException)
            {
                Log("扫描文件已取消。", LogLevel.Warning);
            }
            catch (Exception ex)
            {
                Log($"扫描文件时出错：{ex.Message}", LogLevel.Error);
            }
            finally
            {
                DisposeCTS(buttonScanLocalFiles);
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (GlobalCTS != null)
            {
                Log("已有更新任务正在运行。",LogLevel.Warning);
                return;
            }
            try
            {
                var updateInstruction = Serializer.LoadFromFile<UpdateInstruction>(textBoxClientInstructionPath.Text);
                var localFiles = Serializer.LoadFromFile<Dictionary<string, FileSnapshot>>(textBoxLocalFileManifest.Text);
                var minecraftDirectory = textBoxGameDirectory.Text;
                if (!int.TryParse(textBoxDownloadParallelCounts.Text, out var parallelCount))
                {
                    parallelCount = 3;
                }
                GlobalCTS = new CancellationTokenSource();
                buttonUpdate.Enabled = false;
                buttonCancel.Enabled = true;
                var cancellationToken = GlobalCTS.Token;
                progressBarDownload.Value = 0;
                labelDownloadPercent.Text = "0%";

                var progressLog = new Progress<ProcessMessage>(message =>
                {
                    Log(message.Message, message.Level, message.MemberName, message.FilePath, true);
                });

                var downloadProgress = new Progress<DownloadSessionProgress>(sessionProgress =>
                {
                    Log($"下载进度：{sessionProgress.TotalProgressPercent:F2}%");
                    progressBarDownload.Value = (int)Math.Min(Math.Max(0, Math.Round(sessionProgress.TotalProgressPercent)), 100);
                });
                var updateClentOptions = new UpdateService.UpdateClientOptions
                {
                    UpdateInstruction = updateInstruction,
                    LocalFiles = localFiles,
                    MinecraftDirectory = minecraftDirectory,
                    CancellationToken = cancellationToken,
                    DownloadProgress = downloadProgress,
                    LogProgress = progressLog,
                    ParallelCount = parallelCount
                };

                await UpdateService.UpdateClient(updateClentOptions);
            }
            catch (Exception ex)
            {
                Log($"更新客户端时出错：{ex.Message}", LogLevel.Error);
            }
            finally { DisposeCTS(buttonUpdate); }
        }

        private void textBoxDownloadParallelCounts_TextChanged(object sender, EventArgs e)
        {
            var parallelCounts = 3; // 默认值
            if (string.IsNullOrEmpty(textBoxDownloadParallelCounts.Text))
            {
                return; // 允许空输入，用户可能正在编辑
            }
            if (!int.TryParse(textBoxDownloadParallelCounts.Text, out parallelCounts) || parallelCounts <= 0)
            {
                Log("下载并行数量必须是一个正整数。", LogLevel.Warning);
                textBoxDownloadParallelCounts.Text = "3"; // 恢复默认值
                return;
            }
            // 不能超过20
            if (int.TryParse(textBoxDownloadParallelCounts.Text, out parallelCounts) && parallelCounts > 20)
            {
                Log("下载并行数量不能超过20。", LogLevel.Warning);
                textBoxDownloadParallelCounts.Text = "3"; // 恢复默认值
                return;
            }
            parallelCounts = Math.Min(parallelCounts, 20); // 确保不超过20
            Log($"下载并行数量已设置为 {parallelCounts}。");
        }
    }
}

