namespace HA.PMS.EasyMessage
{
    partial class EasyMessAgebox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyMessAgebox));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabNewMission = new System.Windows.Forms.TabPage();
            this.lstNewMission = new System.Windows.Forms.ListBox();
            this.tabNewMessage = new System.Windows.Forms.TabPage();
            this.lstNewMessage = new System.Windows.Forms.ListBox();
            this.EasyMessagenotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerMessage = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabNewMission.SuspendLayout();
            this.tabNewMessage.SuspendLayout();
            this.NotifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabNewMission);
            this.tabControl1.Controls.Add(this.tabNewMessage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1099, 514);
            this.tabControl1.TabIndex = 0;
            // 
            // tabNewMission
            // 
            this.tabNewMission.BackColor = System.Drawing.Color.Silver;
            this.tabNewMission.Controls.Add(this.lstNewMission);
            this.tabNewMission.Location = new System.Drawing.Point(4, 22);
            this.tabNewMission.Margin = new System.Windows.Forms.Padding(0);
            this.tabNewMission.Name = "tabNewMission";
            this.tabNewMission.Size = new System.Drawing.Size(1091, 488);
            this.tabNewMission.TabIndex = 0;
            this.tabNewMission.Text = "新任务";
            // 
            // lstNewMission
            // 
            this.lstNewMission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstNewMission.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lstNewMission.ForeColor = System.Drawing.Color.Black;
            this.lstNewMission.FormattingEnabled = true;
            this.lstNewMission.ItemHeight = 21;
            this.lstNewMission.Location = new System.Drawing.Point(0, 0);
            this.lstNewMission.Margin = new System.Windows.Forms.Padding(0);
            this.lstNewMission.Name = "lstNewMission";
            this.lstNewMission.Size = new System.Drawing.Size(1091, 487);
            this.lstNewMission.TabIndex = 0;
            this.lstNewMission.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstNewMission_MouseDoubleClick);
            // 
            // tabNewMessage
            // 
            this.tabNewMessage.BackColor = System.Drawing.Color.SlateGray;
            this.tabNewMessage.Controls.Add(this.lstNewMessage);
            this.tabNewMessage.Location = new System.Drawing.Point(4, 22);
            this.tabNewMessage.Margin = new System.Windows.Forms.Padding(0);
            this.tabNewMessage.Name = "tabNewMessage";
            this.tabNewMessage.Size = new System.Drawing.Size(1091, 488);
            this.tabNewMessage.TabIndex = 1;
            this.tabNewMessage.Text = "新消息";
            // 
            // lstNewMessage
            // 
            this.lstNewMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstNewMessage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lstNewMessage.ForeColor = System.Drawing.Color.Black;
            this.lstNewMessage.FormattingEnabled = true;
            this.lstNewMessage.ItemHeight = 21;
            this.lstNewMessage.Location = new System.Drawing.Point(0, 0);
            this.lstNewMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lstNewMessage.Name = "lstNewMessage";
            this.lstNewMessage.Size = new System.Drawing.Size(1091, 487);
            this.lstNewMessage.TabIndex = 0;
            this.lstNewMessage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstNewMessage_MouseDoubleClick);
            // 
            // EasyMessagenotifyIcon
            // 
            this.EasyMessagenotifyIcon.ContextMenuStrip = this.NotifyMenu;
            this.EasyMessagenotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("EasyMessagenotifyIcon.Icon")));
            this.EasyMessagenotifyIcon.Visible = true;
            this.EasyMessagenotifyIcon.DoubleClick += new System.EventHandler(this.EasyMessagenotifyIcon_DoubleClick);
            // 
            // NotifyMenu
            // 
            this.NotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.NotifyMenu.Name = "NotifyMenu";
            this.NotifyMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitMenuItem.Text = "退出";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // TimerMessage
            // 
            this.TimerMessage.Interval = 3000;
            this.TimerMessage.Tick += new System.EventHandler(this.TimerMessage_Tick);
            // 
            // EasyMessAgebox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1099, 514);
            this.Controls.Add(this.tabControl1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EasyMessAgebox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WBMS消息提示器";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EasyMessAgebox_FormClosing);
            this.Load += new System.EventHandler(this.EasyMessAgebox_Load);
            this.SizeChanged += new System.EventHandler(this.EasyMessAgebox_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabNewMission.ResumeLayout(false);
            this.tabNewMessage.ResumeLayout(false);
            this.NotifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNewMission;
        private System.Windows.Forms.TabPage tabNewMessage;
        private System.Windows.Forms.ListBox lstNewMessage;
        private System.Windows.Forms.NotifyIcon EasyMessagenotifyIcon;
        private System.Windows.Forms.Timer TimerMessage;
        private System.Windows.Forms.ContextMenuStrip NotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ListBox lstNewMission;

    }
}