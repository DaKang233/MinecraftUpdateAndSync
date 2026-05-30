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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishForm));
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
            this.groupBoxManifestGeneration = new System.Windows.Forms.GroupBox();
            this.groupBoxInstructionGeneration = new System.Windows.Forms.GroupBox();
            this.buttonInstructionConfigPath = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxInstructionConfigPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxInstructionVersion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.buttonGenerateInstruction = new System.Windows.Forms.Button();
            this.checkBoxAllowDeletion = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.groupBoxProtocol = new System.Windows.Forms.GroupBox();
            this.radioBtnHttpProtocol = new System.Windows.Forms.RadioButton();
            this.radioBtnSftpProtocol = new System.Windows.Forms.RadioButton();
            this.radioBtnHttpsProtocol = new System.Windows.Forms.RadioButton();
            this.radioBtnFtpProtocol = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxResourceRelativeDirectory = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxFileServerRootPath = new System.Windows.Forms.TextBox();
            this.buttonFileServerPathBrowse = new System.Windows.Forms.Button();
            this.buttonResourceRelativeDirectoryBrowse = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxManifestPath = new System.Windows.Forms.TextBox();
            this.buttonManifestPath = new System.Windows.Forms.Button();
            this.groupBoxManifestGeneration.SuspendLayout();
            this.groupBoxInstructionGeneration.SuspendLayout();
            this.groupBoxProtocol.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReloadConfig
            // 
            this.buttonReloadConfig.Location = new System.Drawing.Point(13, 10);
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
            this.buttonSaveConfig.Location = new System.Drawing.Point(121, 10);
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
            this.textBoxManifestSaveDirectory.Location = new System.Drawing.Point(7, 108);
            this.textBoxManifestSaveDirectory.Name = "textBoxManifestSaveDirectory";
            this.textBoxManifestSaveDirectory.Size = new System.Drawing.Size(323, 26);
            this.textBoxManifestSaveDirectory.TabIndex = 2;
            // 
            // labelManifestSavePath
            // 
            this.labelManifestSavePath.AutoSize = true;
            this.labelManifestSavePath.Location = new System.Drawing.Point(6, 85);
            this.labelManifestSavePath.Name = "labelManifestSavePath";
            this.labelManifestSavePath.Size = new System.Drawing.Size(156, 20);
            this.labelManifestSavePath.TabIndex = 3;
            this.labelManifestSavePath.Text = "Manifest 文件保存路径";
            // 
            // buttonBrowseManifestSavePath
            // 
            this.buttonBrowseManifestSavePath.Location = new System.Drawing.Point(337, 104);
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
            this.buttonGenerateManifest.Location = new System.Drawing.Point(7, 27);
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
            this.buttonBrowseMinecraftDirectory.Location = new System.Drawing.Point(337, 156);
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
            this.labelMinecraftDirectory.Location = new System.Drawing.Point(6, 137);
            this.labelMinecraftDirectory.Name = "labelMinecraftDirectory";
            this.labelMinecraftDirectory.Size = new System.Drawing.Size(152, 20);
            this.labelMinecraftDirectory.TabIndex = 7;
            this.labelMinecraftDirectory.Text = "扫描的 Minecraft 目录";
            // 
            // textBoxMinecraftDirectory
            // 
            this.textBoxMinecraftDirectory.Location = new System.Drawing.Point(7, 160);
            this.textBoxMinecraftDirectory.Name = "textBoxMinecraftDirectory";
            this.textBoxMinecraftDirectory.Size = new System.Drawing.Size(323, 26);
            this.textBoxMinecraftDirectory.TabIndex = 6;
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(732, 42);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(65, 20);
            this.labelLog.TabIndex = 9;
            this.labelLog.Text = "日志输出";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(736, 65);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(404, 456);
            this.richTextBoxLog.TabIndex = 10;
            this.richTextBoxLog.Text = "";
            // 
            // buttonBrowseLastManifestPath
            // 
            this.buttonBrowseLastManifestPath.Location = new System.Drawing.Point(337, 208);
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
            this.label1.Location = new System.Drawing.Point(6, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "上一次 Manifest 文件的路径";
            // 
            // textBoxLastManifestPath
            // 
            this.textBoxLastManifestPath.Location = new System.Drawing.Point(7, 212);
            this.textBoxLastManifestPath.Name = "textBoxLastManifestPath";
            this.textBoxLastManifestPath.Size = new System.Drawing.Size(323, 26);
            this.textBoxLastManifestPath.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "当前版本";
            // 
            // textBoxCurrentVersion
            // 
            this.textBoxCurrentVersion.Location = new System.Drawing.Point(10, 264);
            this.textBoxCurrentVersion.Name = "textBoxCurrentVersion";
            this.textBoxCurrentVersion.Size = new System.Drawing.Size(100, 26);
            this.textBoxCurrentVersion.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "忽略的目录列表（以 ; 分隔）";
            // 
            // textBoxIgnoreDirectories
            // 
            this.textBoxIgnoreDirectories.Location = new System.Drawing.Point(7, 316);
            this.textBoxIgnoreDirectories.Name = "textBoxIgnoreDirectories";
            this.textBoxIgnoreDirectories.Size = new System.Drawing.Size(323, 26);
            this.textBoxIgnoreDirectories.TabIndex = 18;
            // 
            // buttonIgnoreDirectories
            // 
            this.buttonIgnoreDirectories.Location = new System.Drawing.Point(337, 312);
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
            this.radioBtnIncludeMode.Location = new System.Drawing.Point(10, 430);
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
            this.radioBtnExcludeMode.Location = new System.Drawing.Point(10, 400);
            this.radioBtnExcludeMode.Name = "radioBtnExcludeMode";
            this.radioBtnExcludeMode.Size = new System.Drawing.Size(83, 24);
            this.radioBtnExcludeMode.TabIndex = 22;
            this.radioBtnExcludeMode.TabStop = true;
            this.radioBtnExcludeMode.Text = "排除模式";
            this.radioBtnExcludeMode.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseIncludeDirectories
            // 
            this.buttonBrowseIncludeDirectories.Location = new System.Drawing.Point(337, 364);
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
            this.label5.Location = new System.Drawing.Point(6, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "包含的目录列表（以 ; 分隔）";
            // 
            // textBoxIncludeDirectories
            // 
            this.textBoxIncludeDirectories.Location = new System.Drawing.Point(7, 368);
            this.textBoxIncludeDirectories.Name = "textBoxIncludeDirectories";
            this.textBoxIncludeDirectories.Size = new System.Drawing.Size(323, 26);
            this.textBoxIncludeDirectories.TabIndex = 23;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(115, 430);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(296, 23);
            this.progressBar.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 402);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "计算进度:";
            // 
            // labelPercent
            // 
            this.labelPercent.Location = new System.Drawing.Point(337, 404);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(74, 20);
            this.labelPercent.TabIndex = 28;
            this.labelPercent.Text = "0%";
            this.labelPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBoxManifestGeneration
            // 
            this.groupBoxManifestGeneration.Controls.Add(this.labelPercent);
            this.groupBoxManifestGeneration.Controls.Add(this.label3);
            this.groupBoxManifestGeneration.Controls.Add(this.progressBar);
            this.groupBoxManifestGeneration.Controls.Add(this.buttonBrowseIncludeDirectories);
            this.groupBoxManifestGeneration.Controls.Add(this.label5);
            this.groupBoxManifestGeneration.Controls.Add(this.textBoxIncludeDirectories);
            this.groupBoxManifestGeneration.Controls.Add(this.radioBtnExcludeMode);
            this.groupBoxManifestGeneration.Controls.Add(this.radioBtnIncludeMode);
            this.groupBoxManifestGeneration.Controls.Add(this.buttonIgnoreDirectories);
            this.groupBoxManifestGeneration.Controls.Add(this.label4);
            this.groupBoxManifestGeneration.Controls.Add(this.textBoxIgnoreDirectories);
            this.groupBoxManifestGeneration.Controls.Add(this.label2);
            this.groupBoxManifestGeneration.Controls.Add(this.textBoxCurrentVersion);
            this.groupBoxManifestGeneration.Controls.Add(this.buttonBrowseLastManifestPath);
            this.groupBoxManifestGeneration.Controls.Add(this.label1);
            this.groupBoxManifestGeneration.Controls.Add(this.textBoxLastManifestPath);
            this.groupBoxManifestGeneration.Controls.Add(this.buttonBrowseMinecraftDirectory);
            this.groupBoxManifestGeneration.Controls.Add(this.labelMinecraftDirectory);
            this.groupBoxManifestGeneration.Controls.Add(this.textBoxMinecraftDirectory);
            this.groupBoxManifestGeneration.Controls.Add(this.buttonGenerateManifest);
            this.groupBoxManifestGeneration.Controls.Add(this.buttonBrowseManifestSavePath);
            this.groupBoxManifestGeneration.Controls.Add(this.labelManifestSavePath);
            this.groupBoxManifestGeneration.Controls.Add(this.textBoxManifestSaveDirectory);
            this.groupBoxManifestGeneration.Location = new System.Drawing.Point(12, 56);
            this.groupBoxManifestGeneration.Name = "groupBoxManifestGeneration";
            this.groupBoxManifestGeneration.Size = new System.Drawing.Size(421, 465);
            this.groupBoxManifestGeneration.TabIndex = 29;
            this.groupBoxManifestGeneration.TabStop = false;
            this.groupBoxManifestGeneration.Text = "Manifest 生成";
            // 
            // groupBoxInstructionGeneration
            // 
            this.groupBoxInstructionGeneration.Controls.Add(this.buttonManifestPath);
            this.groupBoxInstructionGeneration.Controls.Add(this.label13);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxManifestPath);
            this.groupBoxInstructionGeneration.Controls.Add(this.buttonResourceRelativeDirectoryBrowse);
            this.groupBoxInstructionGeneration.Controls.Add(this.buttonFileServerPathBrowse);
            this.groupBoxInstructionGeneration.Controls.Add(this.buttonInstructionConfigPath);
            this.groupBoxInstructionGeneration.Controls.Add(this.label11);
            this.groupBoxInstructionGeneration.Controls.Add(this.label12);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxFileServerRootPath);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxInstructionConfigPath);
            this.groupBoxInstructionGeneration.Controls.Add(this.label10);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxInstructionVersion);
            this.groupBoxInstructionGeneration.Controls.Add(this.label9);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxServerAddress);
            this.groupBoxInstructionGeneration.Controls.Add(this.checkBoxAllowDeletion);
            this.groupBoxInstructionGeneration.Controls.Add(this.label8);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxServerPort);
            this.groupBoxInstructionGeneration.Controls.Add(this.groupBoxProtocol);
            this.groupBoxInstructionGeneration.Controls.Add(this.label7);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxPrefix);
            this.groupBoxInstructionGeneration.Controls.Add(this.label6);
            this.groupBoxInstructionGeneration.Controls.Add(this.textBoxResourceRelativeDirectory);
            this.groupBoxInstructionGeneration.Location = new System.Drawing.Point(439, 56);
            this.groupBoxInstructionGeneration.Name = "groupBoxInstructionGeneration";
            this.groupBoxInstructionGeneration.Size = new System.Drawing.Size(291, 465);
            this.groupBoxInstructionGeneration.TabIndex = 30;
            this.groupBoxInstructionGeneration.TabStop = false;
            this.groupBoxInstructionGeneration.Text = "指导文件生成";
            // 
            // buttonInstructionConfigPath
            // 
            this.buttonInstructionConfigPath.Location = new System.Drawing.Point(207, 89);
            this.buttonInstructionConfigPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonInstructionConfigPath.Name = "buttonInstructionConfigPath";
            this.buttonInstructionConfigPath.Size = new System.Drawing.Size(74, 35);
            this.buttonInstructionConfigPath.TabIndex = 29;
            this.buttonInstructionConfigPath.Text = "浏览...";
            this.buttonInstructionConfigPath.UseVisualStyleBackColor = true;
            this.buttonInstructionConfigPath.Click += new System.EventHandler(this.buttonInstructionConfigPath_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 20);
            this.label11.TabIndex = 44;
            this.label11.Text = "指导文件保存路径";
            // 
            // textBoxInstructionConfigPath
            // 
            this.textBoxInstructionConfigPath.Location = new System.Drawing.Point(7, 93);
            this.textBoxInstructionConfigPath.Name = "textBoxInstructionConfigPath";
            this.textBoxInstructionConfigPath.Size = new System.Drawing.Size(197, 26);
            this.textBoxInstructionConfigPath.TabIndex = 43;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(244, 20);
            this.label10.TabIndex = 30;
            this.label10.Text = "匹配的 Manifest 版本 / 指导文件版本";
            // 
            // textBoxInstructionVersion
            // 
            this.textBoxInstructionVersion.Location = new System.Drawing.Point(7, 411);
            this.textBoxInstructionVersion.Name = "textBoxInstructionVersion";
            this.textBoxInstructionVersion.Size = new System.Drawing.Size(274, 26);
            this.textBoxInstructionVersion.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 336);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 42;
            this.label9.Text = "预设服务器地址";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(7, 359);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(274, 26);
            this.textBoxServerAddress.TabIndex = 41;
            // 
            // buttonGenerateInstruction
            // 
            this.buttonGenerateInstruction.Location = new System.Drawing.Point(439, 10);
            this.buttonGenerateInstruction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGenerateInstruction.Name = "buttonGenerateInstruction";
            this.buttonGenerateInstruction.Size = new System.Drawing.Size(122, 38);
            this.buttonGenerateInstruction.TabIndex = 29;
            this.buttonGenerateInstruction.Text = "生成指导文件";
            this.buttonGenerateInstruction.UseVisualStyleBackColor = true;
            this.buttonGenerateInstruction.Click += new System.EventHandler(this.buttonGenerateInstruction_Click);
            // 
            // checkBoxAllowDeletion
            // 
            this.checkBoxAllowDeletion.AutoSize = true;
            this.checkBoxAllowDeletion.Location = new System.Drawing.Point(10, 438);
            this.checkBoxAllowDeletion.Name = "checkBoxAllowDeletion";
            this.checkBoxAllowDeletion.Size = new System.Drawing.Size(182, 24);
            this.checkBoxAllowDeletion.TabIndex = 40;
            this.checkBoxAllowDeletion.Text = "允许客户端执行删除操作";
            this.checkBoxAllowDeletion.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 20);
            this.label8.TabIndex = 39;
            this.label8.Text = "服务器端口(整数)";
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Location = new System.Drawing.Point(7, 307);
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(122, 26);
            this.textBoxServerPort.TabIndex = 38;
            // 
            // groupBoxProtocol
            // 
            this.groupBoxProtocol.Controls.Add(this.radioBtnHttpProtocol);
            this.groupBoxProtocol.Controls.Add(this.radioBtnSftpProtocol);
            this.groupBoxProtocol.Controls.Add(this.radioBtnHttpsProtocol);
            this.groupBoxProtocol.Controls.Add(this.radioBtnFtpProtocol);
            this.groupBoxProtocol.Location = new System.Drawing.Point(135, 232);
            this.groupBoxProtocol.Name = "groupBoxProtocol";
            this.groupBoxProtocol.Size = new System.Drawing.Size(150, 101);
            this.groupBoxProtocol.TabIndex = 37;
            this.groupBoxProtocol.TabStop = false;
            this.groupBoxProtocol.Text = "协议";
            // 
            // radioBtnHttpProtocol
            // 
            this.radioBtnHttpProtocol.AutoSize = true;
            this.radioBtnHttpProtocol.Location = new System.Drawing.Point(6, 25);
            this.radioBtnHttpProtocol.Name = "radioBtnHttpProtocol";
            this.radioBtnHttpProtocol.Size = new System.Drawing.Size(63, 24);
            this.radioBtnHttpProtocol.TabIndex = 33;
            this.radioBtnHttpProtocol.TabStop = true;
            this.radioBtnHttpProtocol.Text = "HTTP";
            this.radioBtnHttpProtocol.UseVisualStyleBackColor = true;
            // 
            // radioBtnSftpProtocol
            // 
            this.radioBtnSftpProtocol.AutoSize = true;
            this.radioBtnSftpProtocol.Location = new System.Drawing.Point(75, 55);
            this.radioBtnSftpProtocol.Name = "radioBtnSftpProtocol";
            this.radioBtnSftpProtocol.Size = new System.Drawing.Size(59, 24);
            this.radioBtnSftpProtocol.TabIndex = 36;
            this.radioBtnSftpProtocol.TabStop = true;
            this.radioBtnSftpProtocol.Text = "SFTP";
            this.radioBtnSftpProtocol.UseVisualStyleBackColor = true;
            // 
            // radioBtnHttpsProtocol
            // 
            this.radioBtnHttpsProtocol.AutoSize = true;
            this.radioBtnHttpsProtocol.Location = new System.Drawing.Point(75, 25);
            this.radioBtnHttpsProtocol.Name = "radioBtnHttpsProtocol";
            this.radioBtnHttpsProtocol.Size = new System.Drawing.Size(71, 24);
            this.radioBtnHttpsProtocol.TabIndex = 34;
            this.radioBtnHttpsProtocol.TabStop = true;
            this.radioBtnHttpsProtocol.Text = "HTTPS";
            this.radioBtnHttpsProtocol.UseVisualStyleBackColor = true;
            // 
            // radioBtnFtpProtocol
            // 
            this.radioBtnFtpProtocol.AutoSize = true;
            this.radioBtnFtpProtocol.Location = new System.Drawing.Point(6, 55);
            this.radioBtnFtpProtocol.Name = "radioBtnFtpProtocol";
            this.radioBtnFtpProtocol.Size = new System.Drawing.Size(51, 24);
            this.radioBtnFtpProtocol.TabIndex = 35;
            this.radioBtnFtpProtocol.TabStop = true;
            this.radioBtnFtpProtocol.Text = "FTP";
            this.radioBtnFtpProtocol.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "前缀";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(7, 255);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(122, 26);
            this.textBoxPrefix.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(205, 20);
            this.label6.TabIndex = 30;
            this.label6.Text = "客户端拉取服务器资源目录路径";
            // 
            // textBoxResourceRelativeDirectory
            // 
            this.textBoxResourceRelativeDirectory.Location = new System.Drawing.Point(7, 145);
            this.textBoxResourceRelativeDirectory.Name = "textBoxResourceRelativeDirectory";
            this.textBoxResourceRelativeDirectory.Size = new System.Drawing.Size(197, 26);
            this.textBoxResourceRelativeDirectory.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 20);
            this.label12.TabIndex = 46;
            this.label12.Text = "文件服务器根目录";
            // 
            // textBoxFileServerRootPath
            // 
            this.textBoxFileServerRootPath.Location = new System.Drawing.Point(7, 197);
            this.textBoxFileServerRootPath.Name = "textBoxFileServerRootPath";
            this.textBoxFileServerRootPath.Size = new System.Drawing.Size(197, 26);
            this.textBoxFileServerRootPath.TabIndex = 45;
            // 
            // buttonFileServerPathBrowse
            // 
            this.buttonFileServerPathBrowse.Location = new System.Drawing.Point(207, 193);
            this.buttonFileServerPathBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFileServerPathBrowse.Name = "buttonFileServerPathBrowse";
            this.buttonFileServerPathBrowse.Size = new System.Drawing.Size(74, 35);
            this.buttonFileServerPathBrowse.TabIndex = 47;
            this.buttonFileServerPathBrowse.Text = "浏览...";
            this.buttonFileServerPathBrowse.UseVisualStyleBackColor = true;
            this.buttonFileServerPathBrowse.Click += new System.EventHandler(this.buttonFileServerPathBrowse_Click);
            // 
            // buttonResourceRelativeDirectoryBrowse
            // 
            this.buttonResourceRelativeDirectoryBrowse.Location = new System.Drawing.Point(207, 141);
            this.buttonResourceRelativeDirectoryBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonResourceRelativeDirectoryBrowse.Name = "buttonResourceRelativeDirectoryBrowse";
            this.buttonResourceRelativeDirectoryBrowse.Size = new System.Drawing.Size(74, 35);
            this.buttonResourceRelativeDirectoryBrowse.TabIndex = 48;
            this.buttonResourceRelativeDirectoryBrowse.Text = "浏览...";
            this.buttonResourceRelativeDirectoryBrowse.UseVisualStyleBackColor = true;
            this.buttonResourceRelativeDirectoryBrowse.Click += new System.EventHandler(this.buttonResourceRelativeDirectoryBrowse_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 20);
            this.label13.TabIndex = 50;
            this.label13.Text = "Manifest 文件路径";
            // 
            // textBoxManifestPath
            // 
            this.textBoxManifestPath.Location = new System.Drawing.Point(7, 41);
            this.textBoxManifestPath.Name = "textBoxManifestPath";
            this.textBoxManifestPath.Size = new System.Drawing.Size(197, 26);
            this.textBoxManifestPath.TabIndex = 49;
            // 
            // buttonManifestPath
            // 
            this.buttonManifestPath.Location = new System.Drawing.Point(207, 37);
            this.buttonManifestPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonManifestPath.Name = "buttonManifestPath";
            this.buttonManifestPath.Size = new System.Drawing.Size(74, 35);
            this.buttonManifestPath.TabIndex = 51;
            this.buttonManifestPath.Text = "浏览...";
            this.buttonManifestPath.UseVisualStyleBackColor = true;
            this.buttonManifestPath.Click += new System.EventHandler(this.buttonManifestDirectoryBrowse_Click);
            // 
            // PublishForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 530);
            this.Controls.Add(this.groupBoxInstructionGeneration);
            this.Controls.Add(this.groupBoxManifestGeneration);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.buttonSaveConfig);
            this.Controls.Add(this.buttonReloadConfig);
            this.Controls.Add(this.buttonGenerateInstruction);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PublishForm";
            this.Text = "发布工具";
            this.groupBoxManifestGeneration.ResumeLayout(false);
            this.groupBoxManifestGeneration.PerformLayout();
            this.groupBoxInstructionGeneration.ResumeLayout(false);
            this.groupBoxInstructionGeneration.PerformLayout();
            this.groupBoxProtocol.ResumeLayout(false);
            this.groupBoxProtocol.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxManifestGeneration;
        private System.Windows.Forms.GroupBox groupBoxInstructionGeneration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxResourceRelativeDirectory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.GroupBox groupBoxProtocol;
        private System.Windows.Forms.RadioButton radioBtnHttpProtocol;
        private System.Windows.Forms.RadioButton radioBtnSftpProtocol;
        private System.Windows.Forms.RadioButton radioBtnHttpsProtocol;
        private System.Windows.Forms.RadioButton radioBtnFtpProtocol;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.CheckBox checkBoxAllowDeletion;
        private System.Windows.Forms.Button buttonGenerateInstruction;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxInstructionVersion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxInstructionConfigPath;
        private System.Windows.Forms.Button buttonInstructionConfigPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxFileServerRootPath;
        private System.Windows.Forms.Button buttonFileServerPathBrowse;
        private System.Windows.Forms.Button buttonResourceRelativeDirectoryBrowse;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxManifestPath;
        private System.Windows.Forms.Button buttonManifestPath;
    }
}

