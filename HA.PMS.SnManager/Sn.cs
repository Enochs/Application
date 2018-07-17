using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HA.PMS.SnManager
{
    public partial class Sn : Form
    {
        public Sn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("保存成功!");

            Sys_SndatabaseEntities Objentity = new Sys_SndatabaseEntities();
            Objentity.Table_SnManager.Add(new Table_SnManager()
            {
                Sn = txtSN.Text,
                CreateDate = DateTime.Now,
                CreateTime = DateTime.Now,
                IsClose = false,
                FirstCreate = false,
                Customer = txtCustomerName.Text,
                OverDate=DateTime.Parse(txtOverTimer.Text)

            });

            Objentity.SaveChanges();
            this.Close();
        }

        private void Sn_Load(object sender, EventArgs e)
        {
            txtSN.Text = Guid.NewGuid().ToString().ToUpper();

      
        }
    }
}
