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
            this.textBoxGameDirectory = new System.Windows.Forms.TextBox();
            this.labelGameDirectory = new System.Windows.Forms.Label();
            this.buttonBrowseGameDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxGameDirectory
            // 
            this.textBoxGameDirectory.Location = new System.Drawing.Point(26, 128);
            this.textBoxGameDirectory.Name = "textBoxGameDirectory";
            this.textBoxGameDirectory.Size = new System.Drawing.Size(168, 21);
            this.textBoxGameDirectory.TabIndex = 0;
            // 
            // labelGameDirectory
            // 
            this.labelGameDirectory.AutoSize = true;
            this.labelGameDirectory.Location = new System.Drawing.Point(24, 113);
            this.labelGameDirectory.Name = "labelGameDirectory";
            this.labelGameDirectory.Size = new System.Drawing.Size(101, 12);
            this.labelGameDirectory.TabIndex = 1;
            this.labelGameDirectory.Text = "Minecraft 目录：";
            // 
            // buttonBrowseGameDirectory
            // 
            this.buttonBrowseGameDirectory.Location = new System.Drawing.Point(200, 126);
            this.buttonBrowseGameDirectory.Name = "buttonBrowseGameDirectory";
            this.buttonBrowseGameDirectory.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseGameDirectory.TabIndex = 2;
            this.buttonBrowseGameDirectory.Text = "浏览...";
            this.buttonBrowseGameDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseGameDirectory.Click += new System.EventHandler(this.buttonBrowseGameDirectory_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonBrowseGameDirectory);
            this.Controls.Add(this.labelGameDirectory);
            this.Controls.Add(this.textBoxGameDirectory);
            this.Name = "MainForm";
            this.Text = "Minecraft 更新器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGameDirectory;
        private System.Windows.Forms.Label labelGameDirectory;
        private System.Windows.Forms.Button buttonBrowseGameDirectory;
    }
}

