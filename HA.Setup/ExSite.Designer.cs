namespace HA.Setup
{
    partial class ExSite
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
            this.btnExt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExt
            // 
            this.btnExt.Location = new System.Drawing.Point(497, 527);
            this.btnExt.Name = "btnExt";
            this.btnExt.Size = new System.Drawing.Size(75, 23);
            this.btnExt.TabIndex = 0;
            this.btnExt.Text = "安装";
            this.btnExt.UseVisualStyleBackColor = true;
            this.btnExt.Click += new System.EventHandler(this.btnExt_Click);
            // 
            // ExSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.btnExt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExSite";
            this.Text = "开始安装";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExt;
    }
}