using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace HA.PMS.EasyMessage
{
    public partial class EasyMessAgebox : Form
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        private Int32 MessageCount { get; set; }
        private Int32 MissionCount { get; set; }

        private List<WbmsMessage> Messages = new List<WbmsMessage>();
        private List<WbmsMission> Missions = new List<WbmsMission>();

        private Service service = new Service();

        public sealed class WbmsMessage
        {
            public string Title { get; set; }
            public string ID { get; set; }
        }

        public sealed class WbmsMission
        {
            public string DetailedID { get; set; }
            public string MissionName { get; set; }
        }

        private string serverAddress = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAddress"].ConnectionString;

        public EasyMessAgebox()
        {
            InitializeComponent();
            //this.SuspendLayout();
            //this.lstNewMessage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            //this.lstNewMessage.DrawItem += new DrawItemEventHandler(this.DrawItemHandler);
            //this.lstNewMessage.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.MeasureItemHandler);
            //this.ResumeLayout(false);
        }

        //private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        //{
        //    e.ItemHeight = 20;
        //}

        //private void DrawItemHandler(object sender, DrawItemEventArgs e)
        //{
        //    if (this.Messages.Count > 0 && e.Index > 0)
        //    {
        //        e.DrawBackground();
        //        e.DrawFocusRectangle();
        //        e.Graphics.DrawString(this.Messages[e.Index].Title, new Font("微软雅黑", 12, FontStyle.Regular), new SolidBrush(Color.Black), e.Bounds);
        //    }
        //}

        private void BinderData(object sender, EventArgs e)
        {
            //绑定消息
            int messageCount;
            this.Messages = new List<WbmsMessage>();
            this.service.GetMessageService(this.LoginName, 1, out messageCount).ForEach(C => this.Messages.Add(new WbmsMessage { Title = C.Split(',').First(), ID = C.Split(',').Skip(1).Take(1).FirstOrDefault() }));
            this.MessageCount = messageCount;

            this.lstNewMessage.DisplayMember = "Title";
            this.lstNewMessage.ValueMember = "ID";
            this.lstNewMessage.DataSource = this.Messages;

            //绑定任务
            int missionCount;
            this.Missions = new List<WbmsMission>();
            this.service.GetMissionService(this.LoginName, 1, out missionCount).ForEach(C => this.Missions.Add(new WbmsMission { MissionName = C.Split(',').First(), DetailedID = C.Split(',').Skip(1).Take(1).FirstOrDefault() }));
            this.MissionCount = missionCount;

            this.lstNewMission.DisplayMember = "MissionName";
            this.lstNewMission.ValueMember = "DetailedID";
            this.lstNewMission.DataSource = this.Missions;
        }

        private void TimerMessage_Tick(object sender, EventArgs e)
        {
            int messageCount = 0, missionCount = 0;
            List<string> message = this.service.GetMessageService(this.LoginName, 1, out messageCount);
            List<string> mission = this.service.GetMissionService(this.LoginName, 1, out missionCount);

            if (this.MessageCount != messageCount || this.MissionCount != missionCount)
            {
                this.WindowState = FormWindowState.Normal;
                this.MessageCount = messageCount;
                this.MissionCount = missionCount;
                this.BinderData(sender, e);
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void EasyMessAgebox_Load(object sender, EventArgs e)
        {
            this.BinderData(sender, e);
            this.TimerMessage.Start();
        }

        private void EasyMessAgebox_SizeChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    this.ShowInTaskbar = false;
                    this.EasyMessagenotifyIcon.Visible = true;
                    break;
            }
        }

        private void EasyMessAgebox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.EasyMessagenotifyIcon.Visible = true;
            e.Cancel = true;
        }

        private void EasyMessagenotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    this.ShowInTaskbar = true;
                    this.WindowState = FormWindowState.Normal;
                    break;
            }
        }

        private void lstNewMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenUrl(string.Format("{0}Account.aspx?txtLoginName={1}&txtpassword={2}", this.serverAddress, this.LoginName, this.Password));
            System.Threading.Thread.Sleep(3000);
            OpenUrl(string.Format("{0}AdminPanlWorkArea/Flows/Mission/FL_MessageforEmployeeShow.aspx?NeedPopu=1", this.serverAddress));
            this.WindowState = FormWindowState.Minimized;
        }

        private void lstNewMission_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenUrl(string.Format("{0}Account.aspx?txtLoginName={1}&txtpassword={2}", this.serverAddress, this.LoginName, this.Password));
            System.Threading.Thread.Sleep(3000);
            OpenUrl(string.Format("{0}AdminPanlWorkArea/Flows/Mission/MissionDispose.aspx?DetailedID={1}", this.serverAddress, this.lstNewMission.SelectedValue));
            this.WindowState = FormWindowState.Minimized;
        }

        private void OpenUrl(string url)
        {
            using (Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\"))
            {
                string name = Convert.ToString(registryKey.GetValue(string.Empty)).Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(name, url)) { }
            }
        }
    }
}