using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.CS;
using System.IO;
using System.Text;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class SedimentationUpdate : SystemPage
    {
        FileDetails objFileDetailsBLL = new FileDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int FileDetailsId = Request.QueryString["FileDetailsId"].ToInt32();
                Sys_FileDetails fd = objFileDetailsBLL.GetByID(FileDetailsId);
                if (File.Exists(fd.FileDetailsPath))
                {
                    ltlContent.Text = File.ReadAllText(fd.FileDetailsPath, Encoding.Default);
                }
            }
        }
    }
}