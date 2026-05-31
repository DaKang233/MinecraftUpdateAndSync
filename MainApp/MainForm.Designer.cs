namespace MinecraftUpdateAndSync
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBoxGameDirectory = new TextBox();
            labelGameDirectory = new Label();
            buttonBrowseGameDirectory = new Button();
            buttonSaveConfig = new Button();
            buttonReloadConfig = new Button();
            richTextBoxLog = new RichTextBox();
            labelLog = new Label();
            buttonScanLocalFiles = new Button();
            label3 = new Label();
            progressBarScan = new ProgressBar();
            labelScanPercent = new Label();
            labelDownloadPercent = new Label();
            label2 = new Label();
            progressBarDownload = new ProgressBar();
            labelDownloadSpeed = new Label();
            buttonUpdate = new Button();
            labelPercentValidation = new Label();
            label4 = new Label();
            progressBarValidation = new ProgressBar();
            buttonValidation = new Button();
            label5 = new Label();
            textBoxServerAddress = new TextBox();
            buttonOneClickAction = new Button();
            buttonDownloadClientConfig = new Button();
            checkBoxIsAutoUpdated = new CheckBox();
            buttonBrowseClientInstruction = new Button();
            textBoxClientInstructionPath = new TextBox();
            label6 = new Label();
            buttonCancel = new Button();
            buttonLocalFileManifest = new Button();
            label1 = new Label();
            textBoxLocalFileManifest = new TextBox();
            label7 = new Label();
            textBoxDownloadParallelCounts = new TextBox();
            SuspendLayout();
            // 
            // textBoxGameDirectory
            // 
            textBoxGameDirectory.Location = new Point(13, 165);
            textBoxGameDirectory.Margin = new Padding(4, 5, 4, 5);
            textBoxGameDirectory.Name = "textBoxGameDirectory";
            textBoxGameDirectory.Size = new Size(211, 26);
            textBoxGameDirectory.TabIndex = 0;
            // 
            // labelGameDirectory
            // 
            labelGameDirectory.AutoSize = true;
            labelGameDirectory.Location = new Point(10, 140);
            labelGameDirectory.Margin = new Padding(4, 0, 4, 0);
            labelGameDirectory.Name = "labelGameDirectory";
            labelGameDirectory.Size = new Size(120, 20);
            labelGameDirectory.TabIndex = 1;
            labelGameDirectory.Text = "Minecraft 目录：";
            // 
            // buttonBrowseGameDirectory
            // 
            buttonBrowseGameDirectory.Location = new Point(232, 159);
            buttonBrowseGameDirectory.Margin = new Padding(4, 5, 4, 5);
            buttonBrowseGameDirectory.Name = "buttonBrowseGameDirectory";
            buttonBrowseGameDirectory.Size = new Size(56, 38);
            buttonBrowseGameDirectory.TabIndex = 2;
            buttonBrowseGameDirectory.Text = "浏览...";
            buttonBrowseGameDirectory.UseVisualStyleBackColor = true;
            buttonBrowseGameDirectory.Click += buttonBrowseGameDirectory_Click;
            // 
            // buttonSaveConfig
            // 
            buttonSaveConfig.Location = new Point(384, 14);
            buttonSaveConfig.Margin = new Padding(4, 5, 4, 5);
            buttonSaveConfig.Name = "buttonSaveConfig";
            buttonSaveConfig.Size = new Size(100, 38);
            buttonSaveConfig.TabIndex = 7;
            buttonSaveConfig.Text = "保存配置";
            buttonSaveConfig.UseVisualStyleBackColor = true;
            buttonSaveConfig.Click += buttonSaveConfig_Click;
            // 
            // buttonReloadConfig
            // 
            buttonReloadConfig.Location = new Point(276, 14);
            buttonReloadConfig.Margin = new Padding(4, 5, 4, 5);
            buttonReloadConfig.Name = "buttonReloadConfig";
            buttonReloadConfig.Size = new Size(100, 38);
            buttonReloadConfig.TabIndex = 6;
            buttonReloadConfig.Text = "重载配置";
            buttonReloadConfig.UseVisualStyleBackColor = true;
            buttonReloadConfig.Click += buttonReloadConfig_Click;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.Location = new Point(565, 36);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.Size = new Size(404, 420);
            richTextBoxLog.TabIndex = 12;
            richTextBoxLog.Text = "";
            // 
            // labelLog
            // 
            labelLog.AutoSize = true;
            labelLog.Location = new Point(561, 9);
            labelLog.Name = "labelLog";
            labelLog.Size = new Size(65, 20);
            labelLog.TabIndex = 11;
            labelLog.Text = "日志输出";
            // 
            // buttonScanLocalFiles
            // 
            buttonScanLocalFiles.Location = new Point(10, 257);
            buttonScanLocalFiles.Margin = new Padding(4, 5, 4, 5);
            buttonScanLocalFiles.Name = "buttonScanLocalFiles";
            buttonScanLocalFiles.Size = new Size(106, 38);
            buttonScanLocalFiles.TabIndex = 13;
            buttonScanLocalFiles.Text = "扫描本地文件";
            buttonScanLocalFiles.UseVisualStyleBackColor = true;
            buttonScanLocalFiles.Click += buttonScanLocalFiles_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 300);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 29;
            label3.Text = "扫描进度:";
            // 
            // progressBarScan
            // 
            progressBarScan.Location = new Point(14, 322);
            progressBarScan.Name = "progressBarScan";
            progressBarScan.Size = new Size(530, 23);
            progressBarScan.TabIndex = 28;
            // 
            // labelScanPercent
            // 
            labelScanPercent.Location = new Point(470, 300);
            labelScanPercent.Name = "labelScanPercent";
            labelScanPercent.Size = new Size(74, 20);
            labelScanPercent.TabIndex = 30;
            labelScanPercent.Text = "0%";
            labelScanPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // labelDownloadPercent
            // 
            labelDownloadPercent.Location = new Point(470, 353);
            labelDownloadPercent.Name = "labelDownloadPercent";
            labelDownloadPercent.Size = new Size(74, 20);
            labelDownloadPercent.TabIndex = 33;
            labelDownloadPercent.Text = "0%";
            labelDownloadPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 353);
            label2.Name = "label2";
            label2.Size = new Size(68, 20);
            label2.TabIndex = 32;
            label2.Text = "下载进度:";
            // 
            // progressBarDownload
            // 
            progressBarDownload.Location = new Point(14, 376);
            progressBarDownload.Name = "progressBarDownload";
            progressBarDownload.Size = new Size(530, 23);
            progressBarDownload.TabIndex = 31;
            // 
            // labelDownloadSpeed
            // 
            labelDownloadSpeed.AutoSize = true;
            labelDownloadSpeed.Location = new Point(84, 353);
            labelDownloadSpeed.Name = "labelDownloadSpeed";
            labelDownloadSpeed.Size = new Size(25, 20);
            labelDownloadSpeed.TabIndex = 34;
            labelDownloadSpeed.Text = "    ";
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(124, 257);
            buttonUpdate.Margin = new Padding(4, 5, 4, 5);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(100, 38);
            buttonUpdate.TabIndex = 35;
            buttonUpdate.Text = "更新游戏";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // labelPercentValidation
            // 
            labelPercentValidation.Location = new Point(471, 411);
            labelPercentValidation.Name = "labelPercentValidation";
            labelPercentValidation.Size = new Size(74, 20);
            labelPercentValidation.TabIndex = 38;
            labelPercentValidation.Text = "0%";
            labelPercentValidation.TextAlign = ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 411);
            label4.Name = "label4";
            label4.Size = new Size(68, 20);
            label4.TabIndex = 37;
            label4.Text = "验证进度:";
            // 
            // progressBarValidation
            // 
            progressBarValidation.Location = new Point(15, 433);
            progressBarValidation.Name = "progressBarValidation";
            progressBarValidation.Size = new Size(530, 23);
            progressBarValidation.TabIndex = 36;
            // 
            // buttonValidation
            // 
            buttonValidation.Location = new Point(232, 257);
            buttonValidation.Margin = new Padding(4, 5, 4, 5);
            buttonValidation.Name = "buttonValidation";
            buttonValidation.Size = new Size(100, 38);
            buttonValidation.TabIndex = 39;
            buttonValidation.Text = "校验完整性";
            buttonValidation.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 196);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(93, 20);
            label5.TabIndex = 41;
            label5.Text = "服务器地址：";
            // 
            // textBoxServerAddress
            // 
            textBoxServerAddress.Location = new Point(12, 221);
            textBoxServerAddress.Margin = new Padding(4, 5, 4, 5);
            textBoxServerAddress.Name = "textBoxServerAddress";
            textBoxServerAddress.Size = new Size(530, 26);
            textBoxServerAddress.TabIndex = 40;
            // 
            // buttonOneClickAction
            // 
            buttonOneClickAction.Location = new Point(13, 14);
            buttonOneClickAction.Margin = new Padding(4, 5, 4, 5);
            buttonOneClickAction.Name = "buttonOneClickAction";
            buttonOneClickAction.Size = new Size(100, 38);
            buttonOneClickAction.TabIndex = 42;
            buttonOneClickAction.Text = "一键更新";
            buttonOneClickAction.UseVisualStyleBackColor = true;
            // 
            // buttonDownloadClientConfig
            // 
            buttonDownloadClientConfig.Location = new Point(121, 14);
            buttonDownloadClientConfig.Margin = new Padding(4, 5, 4, 5);
            buttonDownloadClientConfig.Name = "buttonDownloadClientConfig";
            buttonDownloadClientConfig.Size = new Size(147, 38);
            buttonDownloadClientConfig.TabIndex = 46;
            buttonDownloadClientConfig.Text = "下载客户端配置文件";
            buttonDownloadClientConfig.UseVisualStyleBackColor = true;
            buttonDownloadClientConfig.Click += buttonDownloadClientConfig_Click;
            // 
            // checkBoxIsAutoUpdated
            // 
            checkBoxIsAutoUpdated.AutoSize = true;
            checkBoxIsAutoUpdated.Location = new Point(18, 57);
            checkBoxIsAutoUpdated.Name = "checkBoxIsAutoUpdated";
            checkBoxIsAutoUpdated.Size = new Size(84, 24);
            checkBoxIsAutoUpdated.TabIndex = 47;
            checkBoxIsAutoUpdated.Text = "自动更新";
            checkBoxIsAutoUpdated.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseClientInstruction
            // 
            buttonBrowseClientInstruction.Location = new Point(232, 103);
            buttonBrowseClientInstruction.Margin = new Padding(4, 5, 4, 5);
            buttonBrowseClientInstruction.Name = "buttonBrowseClientInstruction";
            buttonBrowseClientInstruction.Size = new Size(56, 38);
            buttonBrowseClientInstruction.TabIndex = 45;
            buttonBrowseClientInstruction.Text = "浏览...";
            buttonBrowseClientInstruction.UseVisualStyleBackColor = true;
            buttonBrowseClientInstruction.Click += buttonBrowseClientInstruction_Click;
            // 
            // textBoxClientInstructionPath
            // 
            textBoxClientInstructionPath.Location = new Point(15, 109);
            textBoxClientInstructionPath.Margin = new Padding(4, 5, 4, 5);
            textBoxClientInstructionPath.Name = "textBoxClientInstructionPath";
            textBoxClientInstructionPath.Size = new Size(209, 26);
            textBoxClientInstructionPath.TabIndex = 43;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 84);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(107, 20);
            label6.TabIndex = 44;
            label6.Text = "客户端指令文件";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(430, 257);
            buttonCancel.Margin = new Padding(4, 5, 4, 5);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(115, 38);
            buttonCancel.TabIndex = 48;
            buttonCancel.Text = "终止当前操作";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonLocalFileManifest
            // 
            buttonLocalFileManifest.Location = new Point(489, 103);
            buttonLocalFileManifest.Margin = new Padding(4, 5, 4, 5);
            buttonLocalFileManifest.Name = "buttonLocalFileManifest";
            buttonLocalFileManifest.Size = new Size(56, 38);
            buttonLocalFileManifest.TabIndex = 51;
            buttonLocalFileManifest.Text = "浏览...";
            buttonLocalFileManifest.UseVisualStyleBackColor = true;
            buttonLocalFileManifest.Click += buttonLocalFileManifest_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(300, 84);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(201, 20);
            label1.TabIndex = 50;
            label1.Text = "本地清单文件(输入完整文件名)";
            // 
            // textBoxLocalFileManifest
            // 
            textBoxLocalFileManifest.Location = new Point(300, 109);
            textBoxLocalFileManifest.Margin = new Padding(4, 5, 4, 5);
            textBoxLocalFileManifest.Name = "textBoxLocalFileManifest";
            textBoxLocalFileManifest.Size = new Size(181, 26);
            textBoxLocalFileManifest.TabIndex = 49;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(300, 140);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(125, 20);
            label7.TabIndex = 53;
            label7.Text = "下载并发数(默认3)";
            // 
            // textBoxDownloadParallelCounts
            // 
            textBoxDownloadParallelCounts.Location = new Point(300, 165);
            textBoxDownloadParallelCounts.Margin = new Padding(4, 5, 4, 5);
            textBoxDownloadParallelCounts.Name = "textBoxDownloadParallelCounts";
            textBoxDownloadParallelCounts.Size = new Size(79, 26);
            textBoxDownloadParallelCounts.TabIndex = 52;
            textBoxDownloadParallelCounts.TextChanged += textBoxDownloadParallelCounts_TextChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(981, 468);
            Controls.Add(label7);
            Controls.Add(textBoxDownloadParallelCounts);
            Controls.Add(buttonLocalFileManifest);
            Controls.Add(label1);
            Controls.Add(textBoxLocalFileManifest);
            Controls.Add(buttonCancel);
            Controls.Add(checkBoxIsAutoUpdated);
            Controls.Add(buttonDownloadClientConfig);
            Controls.Add(buttonBrowseClientInstruction);
            Controls.Add(label6);
            Controls.Add(textBoxClientInstructionPath);
            Controls.Add(buttonOneClickAction);
            Controls.Add(label5);
            Controls.Add(textBoxServerAddress);
            Controls.Add(buttonValidation);
            Controls.Add(labelPercentValidation);
            Controls.Add(label4);
            Controls.Add(progressBarValidation);
            Controls.Add(buttonUpdate);
            Controls.Add(labelDownloadSpeed);
            Controls.Add(labelDownloadPercent);
            Controls.Add(label2);
            Controls.Add(progressBarDownload);
            Controls.Add(labelScanPercent);
            Controls.Add(label3);
            Controls.Add(progressBarScan);
            Controls.Add(buttonScanLocalFiles);
            Controls.Add(richTextBoxLog);
            Controls.Add(labelLog);
            Controls.Add(buttonSaveConfig);
            Controls.Add(buttonReloadConfig);
            Controls.Add(buttonBrowseGameDirectory);
            Controls.Add(labelGameDirectory);
            Controls.Add(textBoxGameDirectory);
            Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Minecraft 更新器";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGameDirectory;
        private System.Windows.Forms.Label labelGameDirectory;
        private System.Windows.Forms.Button buttonBrowseGameDirectory;
        private System.Windows.Forms.Button buttonSaveConfig;
        private System.Windows.Forms.Button buttonReloadConfig;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Button buttonScanLocalFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBarScan;
        private System.Windows.Forms.Label labelScanPercent;
        private System.Windows.Forms.Label labelDownloadPercent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Label labelDownloadSpeed;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label labelPercentValidation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBarValidation;
        private System.Windows.Forms.Button buttonValidation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Button buttonOneClickAction;
        private System.Windows.Forms.Button buttonDownloadClientConfig;
        private System.Windows.Forms.CheckBox checkBoxIsAutoUpdated;
        private System.Windows.Forms.Button buttonBrowseClientInstruction;
        private System.Windows.Forms.TextBox textBoxClientInstructionPath;
        private System.Windows.Forms.Label label6;
        private Button buttonCancel;
        private Button buttonLocalFileManifest;
        private Label label1;
        private TextBox textBoxLocalFileManifest;
        private Label label7;
        private TextBox textBoxDownloadParallelCounts;
    }
}


