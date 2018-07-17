using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmployeeUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["FinishFloder"].Value = "PersonFile";
            Session["PostServerUri"] = "/AdminPanlWorkArea/Control/FileServer.aspx?EmployeeID=" + Request["EmployeeID"].ToInt32() + "&Typer=11";
        }

        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?EmployeeID=" + Request["EmployeeID"].ToInt32() + "&Typer=11";
            base.OnInit(e);
        }


        /// <summary>
        /// 保存图片 刷新报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
        }
    }
}