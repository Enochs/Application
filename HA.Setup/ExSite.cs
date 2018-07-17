using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HA.Setup
{
    public partial class ExSite : Form
    {
        public ExSite()
        {
            InitializeComponent();
        }

        private void btnExt_Click(object sender, EventArgs e)
        {
            btnExt.Visible = false;
             
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\OnlineSystem\\" + "HA.PMS.OnlineSysytem.exe");
            
            Application.Exit();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {

        }
    }
}
