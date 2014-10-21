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
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // minRAMBox
            // 
            this.minRAMBox.Location = new System.Drawing.Point(96, 12);
            this.minRAMBox.Name = "minRAMBox";
            this.minRAMBox.Size = new System.Drawing.Size(203, 20);
            this.minRAMBox.TabIndex = 4;
            // 
            // maxRAMBox
            // 
            this.maxRAMBox.Location = new System.Drawing.Point(96, 38);
            this.maxRAMBox.Name = "maxRAMBox";
            this.maxRAMBox.Size = new System.Drawing.Size(203, 20);
            this.maxRAMBox.TabIndex = 5;
            // 
            // permGenBox
            // 
            this.permGenBox.Location = new System.Drawing.Point(96, 64);
            this.permGenBox.Name = "permGenBox";
            this.permGenBox.Size = new System.Drawing.Size(203, 20);
            this.permGenBox.TabIndex = 6;
            // 
            // serverNameBox
            // 
            this.serverNameBox.Location = new System.Drawing.Point(96, 90);
            this.serverNameBox.Name = "serverNameBox";
            this.serverNameBox.Size = new System.Drawing.Size(203, 20);
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
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(13, 133);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 12;
            this.ok.Text = "Ok";
            this.ok.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(224, 133);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 13;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 168);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
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
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
    }
}