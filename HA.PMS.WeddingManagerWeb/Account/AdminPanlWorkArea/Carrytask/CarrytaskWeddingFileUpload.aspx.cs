using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskWeddingFileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["FinishFloder"].Value = "WeddingPlanningFile";
            Session["PostServerUri"] = "/AdminPanlWorkArea/Control/FileServer.aspx?PlanningID=" + Request["PlanningID"] + "&Kind=" + Request["Kind"] + "&Typer=6";
        }

        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?PlanningID=" + Request["PlanningID"] + "&Kind=" + Request["Kind"] + "&Typer=6";
            base.OnInit(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
        }
    }
}