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
            this.textBoxGameDirectory = new System.Windows.Forms.TextBox();
            this.labelGameDirectory = new System.Windows.Forms.Label();
            this.buttonBrowseGameDirectory = new System.Windows.Forms.Button();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.buttonReloadConfig = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.labelLog = new System.Windows.Forms.Label();
            this.buttonScanLocalFiles = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBarScan = new System.Windows.Forms.ProgressBar();
            this.labelScanPercent = new System.Windows.Forms.Label();
            this.labelDownloadPercent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.labelDownloadSpeed = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.labelPercentValidation = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBarValidation = new System.Windows.Forms.ProgressBar();
            this.buttonValidation = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.buttonOneClickAction = new System.Windows.Forms.Button();
            this.buttonDownloadClientConfig = new System.Windows.Forms.Button();
            this.checkBoxIsAutoUpdated = new System.Windows.Forms.CheckBox();
            this.buttonBrowseClientInstruction = new System.Windows.Forms.Button();
            this.textBoxClientInstructionPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxGameDirectory
            // 
            this.textBoxGameDirectory.Location = new System.Drawing.Point(13, 165);
            this.textBoxGameDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxGameDirectory.Name = "textBoxGameDirectory";
            this.textBoxGameDirectory.Size = new System.Drawing.Size(422, 26);
            this.textBoxGameDirectory.TabIndex = 0;
            // 
            // labelGameDirectory
            // 
            this.labelGameDirectory.AutoSize = true;
            this.labelGameDirectory.Location = new System.Drawing.Point(10, 140);
            this.labelGameDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGameDirectory.Name = "labelGameDirectory";
            this.labelGameDirectory.Size = new System.Drawing.Size(120, 20);
            this.labelGameDirectory.TabIndex = 1;
            this.labelGameDirectory.Text = "Minecraft 目录：";
            // 
            // buttonBrowseGameDirectory
            // 
            this.buttonBrowseGameDirectory.Location = new System.Drawing.Point(443, 159);
            this.buttonBrowseGameDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseGameDirectory.Name = "buttonBrowseGameDirectory";
            this.buttonBrowseGameDirectory.Size = new System.Drawing.Size(100, 38);
            this.buttonBrowseGameDirectory.TabIndex = 2;
            this.buttonBrowseGameDirectory.Text = "浏览...";
            this.buttonBrowseGameDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseGameDirectory.Click += new System.EventHandler(this.buttonBrowseGameDirectory_Click);
            // 
            // buttonSaveConfig
            // 
            this.buttonSaveConfig.Location = new System.Drawing.Point(384, 14);
            this.buttonSaveConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.Size = new System.Drawing.Size(100, 38);
            this.buttonSaveConfig.TabIndex = 7;
            this.buttonSaveConfig.Text = "保存配置";
            this.buttonSaveConfig.UseVisualStyleBackColor = true;
            this.buttonSaveConfig.Click += new System.EventHandler(this.buttonSaveConfig_Click);
            // 
            // buttonReloadConfig
            // 
            this.buttonReloadConfig.Location = new System.Drawing.Point(276, 14);
            this.buttonReloadConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonReloadConfig.Name = "buttonReloadConfig";
            this.buttonReloadConfig.Size = new System.Drawing.Size(100, 38);
            this.buttonReloadConfig.TabIndex = 6;
            this.buttonReloadConfig.Text = "重载配置";
            this.buttonReloadConfig.UseVisualStyleBackColor = true;
            this.buttonReloadConfig.Click += new System.EventHandler(this.buttonReloadConfig_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(565, 36);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(404, 420);
            this.richTextBoxLog.TabIndex = 12;
            this.richTextBoxLog.Text = "";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(561, 9);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(65, 20);
            this.labelLog.TabIndex = 11;
            this.labelLog.Text = "日志输出";
            // 
            // buttonScanLocalFiles
            // 
            this.buttonScanLocalFiles.Location = new System.Drawing.Point(10, 257);
            this.buttonScanLocalFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonScanLocalFiles.Name = "buttonScanLocalFiles";
            this.buttonScanLocalFiles.Size = new System.Drawing.Size(106, 38);
            this.buttonScanLocalFiles.TabIndex = 13;
            this.buttonScanLocalFiles.Text = "扫描本地文件";
            this.buttonScanLocalFiles.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "扫描进度:";
            // 
            // progressBarScan
            // 
            this.progressBarScan.Location = new System.Drawing.Point(14, 322);
            this.progressBarScan.Name = "progressBarScan";
            this.progressBarScan.Size = new System.Drawing.Size(530, 23);
            this.progressBarScan.TabIndex = 28;
            // 
            // labelScanPercent
            // 
            this.labelScanPercent.Location = new System.Drawing.Point(470, 300);
            this.labelScanPercent.Name = "labelScanPercent";
            this.labelScanPercent.Size = new System.Drawing.Size(74, 20);
            this.labelScanPercent.TabIndex = 30;
            this.labelScanPercent.Text = "0%";
            this.labelScanPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDownloadPercent
            // 
            this.labelDownloadPercent.Location = new System.Drawing.Point(470, 353);
            this.labelDownloadPercent.Name = "labelDownloadPercent";
            this.labelDownloadPercent.Size = new System.Drawing.Size(74, 20);
            this.labelDownloadPercent.TabIndex = 33;
            this.labelDownloadPercent.Text = "0%";
            this.labelDownloadPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "下载进度:";
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(14, 376);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(530, 23);
            this.progressBarDownload.TabIndex = 31;
            // 
            // labelDownloadSpeed
            // 
            this.labelDownloadSpeed.AutoSize = true;
            this.labelDownloadSpeed.Location = new System.Drawing.Point(84, 353);
            this.labelDownloadSpeed.Name = "labelDownloadSpeed";
            this.labelDownloadSpeed.Size = new System.Drawing.Size(25, 20);
            this.labelDownloadSpeed.TabIndex = 34;
            this.labelDownloadSpeed.Text = "    ";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(124, 257);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(100, 38);
            this.buttonUpdate.TabIndex = 35;
            this.buttonUpdate.Text = "更新游戏";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // labelPercentValidation
            // 
            this.labelPercentValidation.Location = new System.Drawing.Point(471, 411);
            this.labelPercentValidation.Name = "labelPercentValidation";
            this.labelPercentValidation.Size = new System.Drawing.Size(74, 20);
            this.labelPercentValidation.TabIndex = 38;
            this.labelPercentValidation.Text = "0%";
            this.labelPercentValidation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 37;
            this.label4.Text = "验证进度:";
            // 
            // progressBarValidation
            // 
            this.progressBarValidation.Location = new System.Drawing.Point(15, 433);
            this.progressBarValidation.Name = "progressBarValidation";
            this.progressBarValidation.Size = new System.Drawing.Size(530, 23);
            this.progressBarValidation.TabIndex = 36;
            // 
            // buttonValidation
            // 
            this.buttonValidation.Location = new System.Drawing.Point(232, 257);
            this.buttonValidation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonValidation.Name = "buttonValidation";
            this.buttonValidation.Size = new System.Drawing.Size(100, 38);
            this.buttonValidation.TabIndex = 39;
            this.buttonValidation.Text = "校验完整性";
            this.buttonValidation.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 196);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 41;
            this.label5.Text = "服务器地址：";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(12, 221);
            this.textBoxServerAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(530, 26);
            this.textBoxServerAddress.TabIndex = 40;
            // 
            // buttonOneClickAction
            // 
            this.buttonOneClickAction.Location = new System.Drawing.Point(13, 14);
            this.buttonOneClickAction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOneClickAction.Name = "buttonOneClickAction";
            this.buttonOneClickAction.Size = new System.Drawing.Size(100, 38);
            this.buttonOneClickAction.TabIndex = 42;
            this.buttonOneClickAction.Text = "一键更新";
            this.buttonOneClickAction.UseVisualStyleBackColor = true;
            // 
            // buttonDownloadClientConfig
            // 
            this.buttonDownloadClientConfig.Location = new System.Drawing.Point(121, 14);
            this.buttonDownloadClientConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDownloadClientConfig.Name = "buttonDownloadClientConfig";
            this.buttonDownloadClientConfig.Size = new System.Drawing.Size(147, 38);
            this.buttonDownloadClientConfig.TabIndex = 46;
            this.buttonDownloadClientConfig.Text = "下载客户端配置文件";
            this.buttonDownloadClientConfig.UseVisualStyleBackColor = true;
            this.buttonDownloadClientConfig.Click += new System.EventHandler(this.buttonDownloadClientConfig_Click);
            // 
            // checkBoxIsAutoUpdated
            // 
            this.checkBoxIsAutoUpdated.AutoSize = true;
            this.checkBoxIsAutoUpdated.Location = new System.Drawing.Point(18, 57);
            this.checkBoxIsAutoUpdated.Name = "checkBoxIsAutoUpdated";
            this.checkBoxIsAutoUpdated.Size = new System.Drawing.Size(84, 24);
            this.checkBoxIsAutoUpdated.TabIndex = 47;
            this.checkBoxIsAutoUpdated.Text = "自动更新";
            this.checkBoxIsAutoUpdated.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseClientInstruction
            // 
            this.buttonBrowseClientInstruction.Location = new System.Drawing.Point(443, 103);
            this.buttonBrowseClientInstruction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseClientInstruction.Name = "buttonBrowseClientInstruction";
            this.buttonBrowseClientInstruction.Size = new System.Drawing.Size(100, 38);
            this.buttonBrowseClientInstruction.TabIndex = 45;
            this.buttonBrowseClientInstruction.Text = "浏览...";
            this.buttonBrowseClientInstruction.UseVisualStyleBackColor = true;
            this.buttonBrowseClientInstruction.Click += new System.EventHandler(this.buttonBrowseClientInstruction_Click);
            // 
            // textBoxClientInstructionPath
            // 
            this.textBoxClientInstructionPath.Location = new System.Drawing.Point(15, 109);
            this.textBoxClientInstructionPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxClientInstructionPath.Name = "textBoxClientInstructionPath";
            this.textBoxClientInstructionPath.Size = new System.Drawing.Size(422, 26);
            this.textBoxClientInstructionPath.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 84);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.TabIndex = 44;
            this.label6.Text = "客户端指令文件";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 468);
            this.Controls.Add(this.checkBoxIsAutoUpdated);
            this.Controls.Add(this.buttonDownloadClientConfig);
            this.Controls.Add(this.buttonBrowseClientInstruction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxClientInstructionPath);
            this.Controls.Add(this.buttonOneClickAction);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxServerAddress);
            this.Controls.Add(this.buttonValidation);
            this.Controls.Add(this.labelPercentValidation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBarValidation);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.labelDownloadSpeed);
            this.Controls.Add(this.labelDownloadPercent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.labelScanPercent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBarScan);
            this.Controls.Add(this.buttonScanLocalFiles);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.buttonSaveConfig);
            this.Controls.Add(this.buttonReloadConfig);
            this.Controls.Add(this.buttonBrowseGameDirectory);
            this.Controls.Add(this.labelGameDirectory);
            this.Controls.Add(this.textBoxGameDirectory);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Minecraft 更新器";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}


