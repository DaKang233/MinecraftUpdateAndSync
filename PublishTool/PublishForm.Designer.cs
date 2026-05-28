namespace MinecraftUpdateAndSync.PublishTool
{
    partial class PublishForm
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
            this.buttonReloadConfig = new System.Windows.Forms.Button();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.textBoxManifestSaveDirectory = new System.Windows.Forms.TextBox();
            this.labelManifestSavePath = new System.Windows.Forms.Label();
            this.buttonBrowseManifestSavePath = new System.Windows.Forms.Button();
            this.buttonGenerateManifest = new System.Windows.Forms.Button();
            this.buttonBrowseMinecraftDirectory = new System.Windows.Forms.Button();
            this.labelMinecraftDirectory = new System.Windows.Forms.Label();
            this.textBoxMinecraftDirectory = new System.Windows.Forms.TextBox();
            this.labelLog = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.buttonBrowseLastManifestPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLastManifestPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCurrentVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIgnoreDirectories = new System.Windows.Forms.TextBox();
            this.buttonIgnoreDirectories = new System.Windows.Forms.Button();
            this.radioBtnIncludeMode = new System.Windows.Forms.RadioButton();
            this.radioBtnExcludeMode = new System.Windows.Forms.RadioButton();
            this.buttonBrowseIncludeDirectories = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxIncludeDirectories = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonReloadConfig
            // 
            this.buttonReloadConfig.Location = new System.Drawing.Point(13, 14);
            this.buttonReloadConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonReloadConfig.Name = "buttonReloadConfig";
            this.buttonReloadConfig.Size = new System.Drawing.Size(100, 38);
            this.buttonReloadConfig.TabIndex = 0;
            this.buttonReloadConfig.Text = "重载配置";
            this.buttonReloadConfig.UseVisualStyleBackColor = true;
            this.buttonReloadConfig.Click += new System.EventHandler(this.buttonReloadConfig_Click);
            // 
            // buttonSaveConfig
            // 
            this.buttonSaveConfig.Location = new System.Drawing.Point(121, 14);
            this.buttonSaveConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.Size = new System.Drawing.Size(100, 38);
            this.buttonSaveConfig.TabIndex = 1;
            this.buttonSaveConfig.Text = "保存配置";
            this.buttonSaveConfig.UseVisualStyleBackColor = true;
            this.buttonSaveConfig.Click += new System.EventHandler(this.buttonSaveConfig_Click);
            // 
            // textBoxManifestSaveDirectory
            // 
            this.textBoxManifestSaveDirectory.Location = new System.Drawing.Point(13, 95);
            this.textBoxManifestSaveDirectory.Name = "textBoxManifestSaveDirectory";
            this.textBoxManifestSaveDirectory.Size = new System.Drawing.Size(323, 26);
            this.textBoxManifestSaveDirectory.TabIndex = 2;
            // 
            // labelManifestSavePath
            // 
            this.labelManifestSavePath.AutoSize = true;
            this.labelManifestSavePath.Location = new System.Drawing.Point(12, 72);
            this.labelManifestSavePath.Name = "labelManifestSavePath";
            this.labelManifestSavePath.Size = new System.Drawing.Size(156, 20);
            this.labelManifestSavePath.TabIndex = 3;
            this.labelManifestSavePath.Text = "Manifest 文件保存路径";
            // 
            // buttonBrowseManifestSavePath
            // 
            this.buttonBrowseManifestSavePath.Location = new System.Drawing.Point(343, 91);
            this.buttonBrowseManifestSavePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseManifestSavePath.Name = "buttonBrowseManifestSavePath";
            this.buttonBrowseManifestSavePath.Size = new System.Drawing.Size(74, 35);
            this.buttonBrowseManifestSavePath.TabIndex = 4;
            this.buttonBrowseManifestSavePath.Text = "浏览...";
            this.buttonBrowseManifestSavePath.UseVisualStyleBackColor = true;
            this.buttonBrowseManifestSavePath.Click += new System.EventHandler(this.buttonBrowseManifestSavePath_Click);
            // 
            // buttonGenerateManifest
            // 
            this.buttonGenerateManifest.Location = new System.Drawing.Point(229, 14);
            this.buttonGenerateManifest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGenerateManifest.Name = "buttonGenerateManifest";
            this.buttonGenerateManifest.Size = new System.Drawing.Size(100, 38);
            this.buttonGenerateManifest.TabIndex = 5;
            this.buttonGenerateManifest.Text = "生成清单";
            this.buttonGenerateManifest.UseVisualStyleBackColor = true;
            this.buttonGenerateManifest.Click += new System.EventHandler(this.buttonGenerateManifest_Click);
            // 
            // buttonBrowseMinecraftDirectory
            // 
            this.buttonBrowseMinecraftDirectory.Location = new System.Drawing.Point(343, 143);
            this.buttonBrowseMinecraftDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseMinecraftDirectory.Name = "buttonBrowseMinecraftDirectory";
            this.buttonBrowseMinecraftDirectory.Size = new System.Drawing.Size(74, 35);
            this.buttonBrowseMinecraftDirectory.TabIndex = 8;
            this.buttonBrowseMinecraftDirectory.Text = "浏览...";
            this.buttonBrowseMinecraftDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseMinecraftDirectory.Click += new System.EventHandler(this.buttonBrowseMinecraftDirectory_Click);
            // 
            // labelMinecraftDirectory
            // 
            this.labelMinecraftDirectory.AutoSize = true;
            this.labelMinecraftDirectory.Location = new System.Drawing.Point(12, 124);
            this.labelMinecraftDirectory.Name = "labelMinecraftDirectory";
            this.labelMinecraftDirectory.Size = new System.Drawing.Size(152, 20);
            this.labelMinecraftDirectory.TabIndex = 7;
            this.labelMinecraftDirectory.Text = "扫描的 Minecraft 目录";
            // 
            // textBoxMinecraftDirectory
            // 
            this.textBoxMinecraftDirectory.Location = new System.Drawing.Point(13, 147);
            this.textBoxMinecraftDirectory.Name = "textBoxMinecraftDirectory";
            this.textBoxMinecraftDirectory.Size = new System.Drawing.Size(323, 26);
            this.textBoxMinecraftDirectory.TabIndex = 6;
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(435, 14);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(65, 20);
            this.labelLog.TabIndex = 9;
            this.labelLog.Text = "日志输出";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(439, 41);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(404, 409);
            this.richTextBoxLog.TabIndex = 10;
            this.richTextBoxLog.Text = "";
            // 
            // buttonBrowseLastManifestPath
            // 
            this.buttonBrowseLastManifestPath.Location = new System.Drawing.Point(343, 195);
            this.buttonBrowseLastManifestPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseLastManifestPath.Name = "buttonBrowseLastManifestPath";
            this.buttonBrowseLastManifestPath.Size = new System.Drawing.Size(74, 35);
            this.buttonBrowseLastManifestPath.TabIndex = 13;
            this.buttonBrowseLastManifestPath.Text = "浏览...";
            this.buttonBrowseLastManifestPath.UseVisualStyleBackColor = true;
            this.buttonBrowseLastManifestPath.Click += new System.EventHandler(this.buttonBrowseLastManifestPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "上一次 Manifest 文件的路径";
            // 
            // textBoxLastManifestPath
            // 
            this.textBoxLastManifestPath.Location = new System.Drawing.Point(13, 199);
            this.textBoxLastManifestPath.Name = "textBoxLastManifestPath";
            this.textBoxLastManifestPath.Size = new System.Drawing.Size(323, 26);
            this.textBoxLastManifestPath.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "当前版本";
            // 
            // textBoxCurrentVersion
            // 
            this.textBoxCurrentVersion.Location = new System.Drawing.Point(16, 251);
            this.textBoxCurrentVersion.Name = "textBoxCurrentVersion";
            this.textBoxCurrentVersion.Size = new System.Drawing.Size(100, 26);
            this.textBoxCurrentVersion.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "忽略的目录列表（以 ; 分隔）";
            // 
            // textBoxIgnoreDirectories
            // 
            this.textBoxIgnoreDirectories.Location = new System.Drawing.Point(13, 303);
            this.textBoxIgnoreDirectories.Name = "textBoxIgnoreDirectories";
            this.textBoxIgnoreDirectories.Size = new System.Drawing.Size(323, 26);
            this.textBoxIgnoreDirectories.TabIndex = 18;
            // 
            // buttonIgnoreDirectories
            // 
            this.buttonIgnoreDirectories.Location = new System.Drawing.Point(343, 299);
            this.buttonIgnoreDirectories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonIgnoreDirectories.Name = "buttonIgnoreDirectories";
            this.buttonIgnoreDirectories.Size = new System.Drawing.Size(74, 35);
            this.buttonIgnoreDirectories.TabIndex = 20;
            this.buttonIgnoreDirectories.Text = "浏览...";
            this.buttonIgnoreDirectories.UseVisualStyleBackColor = true;
            this.buttonIgnoreDirectories.Click += new System.EventHandler(this.buttonIgnoreDirectories_Click);
            // 
            // radioBtnIncludeMode
            // 
            this.radioBtnIncludeMode.AutoSize = true;
            this.radioBtnIncludeMode.Location = new System.Drawing.Point(16, 417);
            this.radioBtnIncludeMode.Name = "radioBtnIncludeMode";
            this.radioBtnIncludeMode.Size = new System.Drawing.Size(83, 24);
            this.radioBtnIncludeMode.TabIndex = 21;
            this.radioBtnIncludeMode.TabStop = true;
            this.radioBtnIncludeMode.Text = "包含模式";
            this.radioBtnIncludeMode.UseVisualStyleBackColor = true;
            // 
            // radioBtnExcludeMode
            // 
            this.radioBtnExcludeMode.AutoSize = true;
            this.radioBtnExcludeMode.Location = new System.Drawing.Point(16, 387);
            this.radioBtnExcludeMode.Name = "radioBtnExcludeMode";
            this.radioBtnExcludeMode.Size = new System.Drawing.Size(83, 24);
            this.radioBtnExcludeMode.TabIndex = 22;
            this.radioBtnExcludeMode.TabStop = true;
            this.radioBtnExcludeMode.Text = "排除模式";
            this.radioBtnExcludeMode.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseIncludeDirectories
            // 
            this.buttonBrowseIncludeDirectories.Location = new System.Drawing.Point(343, 351);
            this.buttonBrowseIncludeDirectories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBrowseIncludeDirectories.Name = "buttonBrowseIncludeDirectories";
            this.buttonBrowseIncludeDirectories.Size = new System.Drawing.Size(74, 35);
            this.buttonBrowseIncludeDirectories.TabIndex = 25;
            this.buttonBrowseIncludeDirectories.Text = "浏览...";
            this.buttonBrowseIncludeDirectories.UseVisualStyleBackColor = true;
            this.buttonBrowseIncludeDirectories.Click += new System.EventHandler(this.buttonBrowseIncludeDirectories_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "包含的目录列表（以 ; 分隔）";
            // 
            // textBoxIncludeDirectories
            // 
            this.textBoxIncludeDirectories.Location = new System.Drawing.Point(13, 355);
            this.textBoxIncludeDirectories.Name = "textBoxIncludeDirectories";
            this.textBoxIncludeDirectories.Size = new System.Drawing.Size(323, 26);
            this.textBoxIncludeDirectories.TabIndex = 23;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(121, 417);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(296, 23);
            this.progressBar.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "计算进度:";
            // 
            // labelPercent
            // 
            this.labelPercent.Location = new System.Drawing.Point(343, 391);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(74, 20);
            this.labelPercent.TabIndex = 28;
            this.labelPercent.Text = "0%";
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PublishForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 462);
            this.Controls.Add(this.labelPercent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonBrowseIncludeDirectories);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxIncludeDirectories);
            this.Controls.Add(this.radioBtnExcludeMode);
            this.Controls.Add(this.radioBtnIncludeMode);
            this.Controls.Add(this.buttonIgnoreDirectories);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxIgnoreDirectories);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCurrentVersion);
            this.Controls.Add(this.buttonBrowseLastManifestPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLastManifestPath);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.buttonBrowseMinecraftDirectory);
            this.Controls.Add(this.labelMinecraftDirectory);
            this.Controls.Add(this.textBoxMinecraftDirectory);
            this.Controls.Add(this.buttonGenerateManifest);
            this.Controls.Add(this.buttonBrowseManifestSavePath);
            this.Controls.Add(this.labelManifestSavePath);
            this.Controls.Add(this.textBoxManifestSaveDirectory);
            this.Controls.Add(this.buttonSaveConfig);
            this.Controls.Add(this.buttonReloadConfig);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PublishForm";
            this.Text = "发布工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReloadConfig;
        private System.Windows.Forms.Button buttonSaveConfig;
        private System.Windows.Forms.TextBox textBoxManifestSaveDirectory;
        private System.Windows.Forms.Label labelManifestSavePath;
        private System.Windows.Forms.Button buttonBrowseManifestSavePath;
        private System.Windows.Forms.Button buttonGenerateManifest;
        private System.Windows.Forms.Button buttonBrowseMinecraftDirectory;
        private System.Windows.Forms.Label labelMinecraftDirectory;
        private System.Windows.Forms.TextBox textBoxMinecraftDirectory;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Button buttonBrowseLastManifestPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLastManifestPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCurrentVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIgnoreDirectories;
        private System.Windows.Forms.Button buttonIgnoreDirectories;
        private System.Windows.Forms.RadioButton radioBtnIncludeMode;
        private System.Windows.Forms.RadioButton radioBtnExcludeMode;
        private System.Windows.Forms.Button buttonBrowseIncludeDirectories;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxIncludeDirectories;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPercent;
    }
}

