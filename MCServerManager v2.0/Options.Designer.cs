namespace MCServerManager_v2._0
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.minRAMBox = new System.Windows.Forms.TextBox();
            this.maxRAMBox = new System.Windows.Forms.TextBox();
            this.permGenBox = new System.Windows.Forms.TextBox();
            this.serverNameBox = new System.Windows.Forms.TextBox();
            this.minRAMLabel = new System.Windows.Forms.Label();
            this.maxRAMLabel = new System.Windows.Forms.Label();
            this.permGenLabel = new System.Windows.Forms.Label();
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.jvmArgsBox = new System.Windows.Forms.TextBox();
            this.jvmArgsLabel = new System.Windows.Forms.Label();
            this.backupFrequencyBox = new System.Windows.Forms.TextBox();
            this.backupFrequencyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // minRAMBox
            // 
            this.minRAMBox.Location = new System.Drawing.Point(96, 12);
            this.minRAMBox.Name = "minRAMBox";
            this.minRAMBox.Size = new System.Drawing.Size(252, 20);
            this.minRAMBox.TabIndex = 4;
            // 
            // maxRAMBox
            // 
            this.maxRAMBox.Location = new System.Drawing.Point(96, 38);
            this.maxRAMBox.Name = "maxRAMBox";
            this.maxRAMBox.Size = new System.Drawing.Size(252, 20);
            this.maxRAMBox.TabIndex = 5;
            // 
            // permGenBox
            // 
            this.permGenBox.Location = new System.Drawing.Point(96, 64);
            this.permGenBox.Name = "permGenBox";
            this.permGenBox.Size = new System.Drawing.Size(252, 20);
            this.permGenBox.TabIndex = 6;
            // 
            // serverNameBox
            // 
            this.serverNameBox.Location = new System.Drawing.Point(96, 90);
            this.serverNameBox.Name = "serverNameBox";
            this.serverNameBox.Size = new System.Drawing.Size(252, 20);
            this.serverNameBox.TabIndex = 7;
            // 
            // minRAMLabel
            // 
            this.minRAMLabel.AutoSize = true;
            this.minRAMLabel.Location = new System.Drawing.Point(12, 15);
            this.minRAMLabel.Name = "minRAMLabel";
            this.minRAMLabel.Size = new System.Drawing.Size(75, 13);
            this.minRAMLabel.TabIndex = 8;
            this.minRAMLabel.Text = "Minimum RAM";
            // 
            // maxRAMLabel
            // 
            this.maxRAMLabel.AutoSize = true;
            this.maxRAMLabel.Location = new System.Drawing.Point(12, 41);
            this.maxRAMLabel.Name = "maxRAMLabel";
            this.maxRAMLabel.Size = new System.Drawing.Size(78, 13);
            this.maxRAMLabel.TabIndex = 9;
            this.maxRAMLabel.Text = "Maximum RAM";
            // 
            // permGenLabel
            // 
            this.permGenLabel.AutoSize = true;
            this.permGenLabel.Location = new System.Drawing.Point(12, 67);
            this.permGenLabel.Name = "permGenLabel";
            this.permGenLabel.Size = new System.Drawing.Size(72, 13);
            this.permGenLabel.TabIndex = 10;
            this.permGenLabel.Text = "Max Permgen";
            // 
            // serverNameLabel
            // 
            this.serverNameLabel.AutoSize = true;
            this.serverNameLabel.Location = new System.Drawing.Point(12, 93);
            this.serverNameLabel.Name = "serverNameLabel";
            this.serverNameLabel.Size = new System.Drawing.Size(65, 13);
            this.serverNameLabel.TabIndex = 11;
            this.serverNameLabel.Text = "Jarfile Name";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Location = new System.Drawing.Point(12, 175);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(266, 175);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancel_Click);
            // 
            // jvmArgsBox
            // 
            this.jvmArgsBox.Location = new System.Drawing.Point(145, 117);
            this.jvmArgsBox.Name = "jvmArgsBox";
            this.jvmArgsBox.Size = new System.Drawing.Size(203, 20);
            this.jvmArgsBox.TabIndex = 14;
            // 
            // jvmArgsLabel
            // 
            this.jvmArgsLabel.AutoSize = true;
            this.jvmArgsLabel.Location = new System.Drawing.Point(12, 120);
            this.jvmArgsLabel.Name = "jvmArgsLabel";
            this.jvmArgsLabel.Size = new System.Drawing.Size(119, 13);
            this.jvmArgsLabel.TabIndex = 15;
            this.jvmArgsLabel.Text = "Custom JVM Arguments";
            // 
            // backupFrequencyBox
            // 
            this.backupFrequencyBox.Location = new System.Drawing.Point(145, 144);
            this.backupFrequencyBox.Name = "backupFrequencyBox";
            this.backupFrequencyBox.Size = new System.Drawing.Size(203, 20);
            this.backupFrequencyBox.TabIndex = 16;
            // 
            // backupFrequencyLabel
            // 
            this.backupFrequencyLabel.AutoSize = true;
            this.backupFrequencyLabel.Location = new System.Drawing.Point(12, 147);
            this.backupFrequencyLabel.Name = "backupFrequencyLabel";
            this.backupFrequencyLabel.Size = new System.Drawing.Size(127, 13);
            this.backupFrequencyLabel.TabIndex = 17;
            this.backupFrequencyLabel.Text = "Backup Frequency (mins)";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 210);
            this.Controls.Add(this.backupFrequencyLabel);
            this.Controls.Add(this.backupFrequencyBox);
            this.Controls.Add(this.jvmArgsLabel);
            this.Controls.Add(this.jvmArgsBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.serverNameLabel);
            this.Controls.Add(this.permGenLabel);
            this.Controls.Add(this.maxRAMLabel);
            this.Controls.Add(this.minRAMLabel);
            this.Controls.Add(this.serverNameBox);
            this.Controls.Add(this.permGenBox);
            this.Controls.Add(this.maxRAMBox);
            this.Controls.Add(this.minRAMBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox minRAMBox;
        private System.Windows.Forms.TextBox maxRAMBox;
        private System.Windows.Forms.TextBox permGenBox;
        private System.Windows.Forms.TextBox serverNameBox;
        private System.Windows.Forms.Label minRAMLabel;
        private System.Windows.Forms.Label maxRAMLabel;
        private System.Windows.Forms.Label permGenLabel;
        private System.Windows.Forms.Label serverNameLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox jvmArgsBox;
        private System.Windows.Forms.Label jvmArgsLabel;
        private System.Windows.Forms.TextBox backupFrequencyBox;
        private System.Windows.Forms.Label backupFrequencyLabel;
    }
}