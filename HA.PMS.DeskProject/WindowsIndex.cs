using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HA.PMS.DeskProject
{
    public partial class WindowsIndex : Form
    {

        public delegate void AutoUpdate(string Address, string SN, string Path, ProgressBar ObjControl,Label lblMessage);


        public WindowsIndex()
        {
            // this.WindowState = FormWindowState.Maximized;

            InitializeComponent();



        }

        #region MyRegion


        /// <summary>
        /// 开始运行B/S系统
        /// </summary>
        private void StarSystem(string CupID, string HDID, string SN)
        {

            //this.webBrowser1.Url = new Uri("http://localhost:52542/Account/Login.aspx");
        }

        /// <summary>
        /// 验证客户端电脑是否合法有效
        /// </summary>
        /// <returns></returns>
        private string ClientChecking()
        {
            string CPUID = string.Empty;//cpu序列号  
            string HDID = string.Empty;
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                CPUID = FindComputerCPU_ID();

            }

            //获取硬盘ID  

            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDID = (string)mo.Properties["Model"].Value;

            }

            return HDID;
        }


        /// <summary>
        /// 查找CPUID
        /// </summary>
        /// <returns></returns>
        public static string FindComputerCPU_ID()
        {
            ManagementScope ms = new ManagementScope("root\\cimv2");
            ms.Connect();
            ManagementObjectSearcher sysinfo = new ManagementObjectSearcher(ms, new SelectQuery("Win32_Processor"));
            string cpuId = "";
            foreach (ManagementObject sys in sysinfo.Get())
            {
                cpuId = sys["ProcessorId"].ToString();
            }
            return cpuId;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void WindowsIndex_Resize(object sender, EventArgs e)
        {
            //this.webBrowser1.Width = this.Width;
            //this.webBrowser1.Height = this.Height;
        }

        private void WindowsIndex_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        #endregion
        /// <summary>
        ///升级程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            LineSince ObjLineSince = new LineSince();

            button1.Visible = false;
            System.IO.StreamReader ObjReader = new System.IO.StreamReader(Application.StartupPath + "\\Service.txt");
            string Address = ObjReader.ReadToEnd();
            ObjReader.Close();
            string CpuID = FindComputerCPU_ID();
            string HDID = ClientChecking();

            System.IO.StreamReader ObjSnReader = new System.IO.StreamReader(Application.StartupPath + "\\SN.bat");
            string SN = ObjSnReader.ReadToEnd().Trim();
            ObjSnReader.Close();

            //没有则默认为1.0版本
            if (!System.IO.File.Exists(Application.StartupPath + "\\Vision.bat"))
            {

                System.IO.StreamWriter ObjWrite = new System.IO.StreamWriter(Application.StartupPath + "\\Vision.bat");
                ObjWrite.WriteLine("1");
                ObjWrite.Close();
            }


            AutoUpdate Update = ObjLineSince.AutoUpdate;
            Update.BeginInvoke(Address, SN + "," + CpuID + "," + HDID, Application.StartupPath + "\\", this.progressBar1, label1, null, null);




            //AlterDataBase.sql

        }
    }
}
