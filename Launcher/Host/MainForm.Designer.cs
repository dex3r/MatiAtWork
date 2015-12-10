namespace Host
{
    partial class MainForm
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
            this.lbFoundClients = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbScanRange = new System.Windows.Forms.TextBox();
            this.bScan = new System.Windows.Forms.Button();
            this.bLaunch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbAppPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bBrowse = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTargetDirectory = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.cbIncludeLocalhost = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFoundClients
            // 
            this.lbFoundClients.FormattingEnabled = true;
            this.lbFoundClients.Location = new System.Drawing.Point(18, 142);
            this.lbFoundClients.Name = "lbFoundClients";
            this.lbFoundClients.Size = new System.Drawing.Size(244, 290);
            this.lbFoundClients.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connected clients:";
            // 
            // bRefresh
            // 
            this.bRefresh.Location = new System.Drawing.Point(187, 438);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(75, 23);
            this.bRefresh.TabIndex = 6;
            this.bRefresh.Text = "Refresh";
            this.bRefresh.UseVisualStyleBackColor = true;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Scan range:";
            // 
            // tbScanRange
            // 
            this.tbScanRange.Location = new System.Drawing.Point(15, 25);
            this.tbScanRange.Name = "tbScanRange";
            this.tbScanRange.Size = new System.Drawing.Size(143, 20);
            this.tbScanRange.TabIndex = 3;
            this.tbScanRange.Text = "127.0.0.1";
            // 
            // bScan
            // 
            this.bScan.Location = new System.Drawing.Point(187, 111);
            this.bScan.Name = "bScan";
            this.bScan.Size = new System.Drawing.Size(75, 23);
            this.bScan.TabIndex = 2;
            this.bScan.Text = "Scan";
            this.bScan.UseVisualStyleBackColor = true;
            this.bScan.Click += new System.EventHandler(this.bScan_Click);
            // 
            // bLaunch
            // 
            this.bLaunch.Location = new System.Drawing.Point(454, 143);
            this.bLaunch.Name = "bLaunch";
            this.bLaunch.Size = new System.Drawing.Size(75, 23);
            this.bLaunch.TabIndex = 8;
            this.bLaunch.Text = "Launch app";
            this.bLaunch.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(454, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Terminate";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tbAppPath
            // 
            this.tbAppPath.Location = new System.Drawing.Point(285, 117);
            this.tbAppPath.Name = "tbAppPath";
            this.tbAppPath.Size = new System.Drawing.Size(244, 20);
            this.tbAppPath.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Application to launch:";
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(288, 51);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 12;
            this.bBrowse.Text = "Browse....";
            this.bBrowse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Directory to send:";
            // 
            // tbTargetDirectory
            // 
            this.tbTargetDirectory.Location = new System.Drawing.Point(285, 25);
            this.tbTargetDirectory.Name = "tbTargetDirectory";
            this.tbTargetDirectory.Size = new System.Drawing.Size(244, 20);
            this.tbTargetDirectory.TabIndex = 13;
            this.tbTargetDirectory.Text = "C:\\Users\\DeX3r\\Documents\\MaWData\\DirToSend";
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(457, 51);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 15;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Max threads:";
            // 
            // nudMaxThreads
            // 
            this.nudMaxThreads.Location = new System.Drawing.Point(18, 87);
            this.nudMaxThreads.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxThreads.Name = "nudMaxThreads";
            this.nudMaxThreads.Size = new System.Drawing.Size(120, 20);
            this.nudMaxThreads.TabIndex = 17;
            this.nudMaxThreads.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // cbIncludeLocalhost
            // 
            this.cbIncludeLocalhost.AutoSize = true;
            this.cbIncludeLocalhost.Location = new System.Drawing.Point(18, 51);
            this.cbIncludeLocalhost.Name = "cbIncludeLocalhost";
            this.cbIncludeLocalhost.Size = new System.Drawing.Size(106, 17);
            this.cbIncludeLocalhost.TabIndex = 18;
            this.cbIncludeLocalhost.Text = "Include localhost";
            this.cbIncludeLocalhost.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 467);
            this.Controls.Add(this.cbIncludeLocalhost);
            this.Controls.Add(this.nudMaxThreads);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbTargetDirectory);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbAppPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bLaunch);
            this.Controls.Add(this.bRefresh);
            this.Controls.Add(this.bScan);
            this.Controls.Add(this.lbFoundClients);
            this.Controls.Add(this.tbScanRange);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Mati@Work Launcher host";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFoundClients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbScanRange;
        private System.Windows.Forms.Button bScan;
        private System.Windows.Forms.Button bLaunch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbAppPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTargetDirectory;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMaxThreads;
        private System.Windows.Forms.CheckBox cbIncludeLocalhost;
    }
}

