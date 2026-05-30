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
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using MinecraftUpdateAndSync.Core.Models;
using MinecraftUpdateAndSync.Core.Utilities;
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
            LoadConfig();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            SaveConfig();
        }

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
            Log("配置文件已加载。");
        }

        private void SaveConfig()
        {
            var config = new Configuration.ConfigurationData();
            config.InstructionConfigPath = textBoxClientInstructionPath.Text;
            config.AutoUpdate = checkBoxIsAutoUpdated.Checked;
            config.GameDirectory = textBoxGameDirectory.Text;
            config.ServerAddress = textBoxServerAddress.Text;
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

        private async Task DownloadInstructionConfig()
        {
            var config_file = new HttpHelper.DownloadItem()
            {
                FileName = Configuration.INSTRUCTION_CONFIG_NAME,
                FileUri = new Uri(Configuration.INSTRUCTION_CONFIG_URL),
                SavePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration.INSTRUCTION_CONFIG_NAME)
            };
            await HttpHelper.DownloadListItemsAsync(new List<HttpHelper.DownloadItem> { config_file }, CancellationToken.None, new Progress<HttpHelper.DownloadProgressInfo>(progress =>
            {
                Log($"正在下载指令配置文件：{progress.CurrentFileName}，{progress.CurrentFilePercent}%");
            }));
        }

        private void buttonDownloadClientConfig_Click(object sender, EventArgs e)
        {

        }
    }
}

