namespace HA.PMS.SnManager
{
    partial class SnIndex
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GrewSnList = new System.Windows.Forms.DataGridView();
            this.SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CpuID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HDID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCreatefileList = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GrewSnList)).BeginInit();
            this.SuspendLayout();
            // 
            // GrewSnList
            // 
            this.GrewSnList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrewSnList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SN,
            this.Customer,
            this.CpuID,
            this.HDID});
            this.GrewSnList.Location = new System.Drawing.Point(2, 1);
            this.GrewSnList.Name = "GrewSnList";
            this.GrewSnList.RowTemplate.Height = 23;
            this.GrewSnList.Size = new System.Drawing.Size(1256, 496);
            this.GrewSnList.TabIndex = 0;
            // 
            // SN
            // 
            this.SN.DataPropertyName = "Sn";
            this.SN.HeaderText = "序列号";
            this.SN.Name = "SN";
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "Customer";
            this.Customer.HeaderText = "客户";
            this.Customer.Name = "Customer";
            // 
            // CpuID
            // 
            this.CpuID.DataPropertyName = "CpuID";
            this.CpuID.HeaderText = "cpu号";
            this.CpuID.Name = "CpuID";
            // 
            // HDID
            // 
            this.HDID.DataPropertyName = "DiskID";
            this.HDID.HeaderText = "硬盘号";
            this.HDID.Name = "HDID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加序列号";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(100, 524);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "刷新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCreatefileList
            // 
            this.btnCreatefileList.Location = new System.Drawing.Point(390, 524);
            this.btnCreatefileList.Name = "btnCreatefileList";
            this.btnCreatefileList.Size = new System.Drawing.Size(75, 23);
            this.btnCreatefileList.TabIndex = 3;
            this.btnCreatefileList.Text = "生成更新文件";
            this.btnCreatefileList.UseVisualStyleBackColor = true;
            this.btnCreatefileList.Click += new System.EventHandler(this.btnCreatefileList_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(272, 526);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 4;
            // 
            // SnIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 596);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCreatefileList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GrewSnList);
            this.Name = "SnIndex";
            this.Text = "序列号管理";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GrewSnList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GrewSnList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn CpuID;
        private System.Windows.Forms.DataGridViewTextBoxColumn HDID;
        private System.Windows.Forms.Button btnCreatefileList;
        private System.Windows.Forms.TextBox textBox1;
    }
}

