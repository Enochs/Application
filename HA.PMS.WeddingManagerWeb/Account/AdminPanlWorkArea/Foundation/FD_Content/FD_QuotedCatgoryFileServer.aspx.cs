using HA.PMS.ToolsLibrary;
using System;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgoryFileServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["FinishFloder"].Value = "QuotedCatgoryFile";
            Session["PostServerUri"] = "/AdminPanlWorkArea/Control/FileServer.aspx?QcKey=" + Request["QcKey"] + "&Kind=7&Typer=7";
        }

        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?QcKey=" + Request["QcKey"] + "&Kind=7&Typer=7";
            base.OnInit(e);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
        }
    }
}