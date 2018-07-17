using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HA.PMS.WeddingManagerWeb;
using HA.PMS.ToolsLibrary;
using System.Management;

namespace HA.PMS.WeddingManagerWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            //if (!IOTools.DeleteSystem(Server.MapPath("/")))
            //{
            //    Response.End();
            //}


        }


        /// <summary>
        /// 验证客户端电脑是否合法有效
        /// </summary>
        /// <returns></returns>
        private string GetHDID()
        {

            //获取硬盘ID  
            string HDID = string.Empty;
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

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }



        /// <summary>
        /// 提交时发生
        /// </summary>
        /// <param name="sernder"></param>
        /// <param name="e"></param>
        void Application_BeginRequest(object sernder,EventArgs e)
        { 
        
        
        }

        void Application_Error(object sender, EventArgs e)
        {

            //string Message = "";

            //Exception ex = Server.GetLastError();

            //Message = "发生错误的网页:{0}错误讯息:{1}堆叠内容:{2}";

            //Message = String.Format(Message, Request.Path + Environment.NewLine,

            //    ex.GetBaseException().Message + Environment.NewLine,

            //    Environment.NewLine + ex.StackTrace);



            //////写入事件捡视器,方法一    

            ////System.Diagnostics.EventLog.WriteEntry("WebAppError", Message,

            ////    System.Diagnostics.EventLogEntryType.Error);



            ////写入文字档,方法二    
            //string Path=Server.MapPath(string.Format("SysTemLog\\{0}.txt",DateTime.Now.ToString("yyyyMMdd")));

            //if (!System.IO.File.Exists(Path))
            //{
            //    System.IO.File.Create(Path);
            //}
            //System.IO.File.AppendAllText(Path, Message);



            ////寄出Email,方法三    

            ////此方法请参考System.Net.Mail.MailMessage    



            ////清除Error    

            //Server.ClearError();



            //Response.Write("系统错误,请联络技术支持人员!");


        }
    }
}
