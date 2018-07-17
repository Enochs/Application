using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPricefileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.ServerFileUpLoad.PostServer = "";
            if (Request["QuotedID"] != null)
            {
                Response.Cookies["FinishFloder"].Value = "QuotedProjectFile";
            }

            if (Request["DetailedID"] != null)
            {
                Response.Cookies["FinishFloder"].Value = "MissionFile";
            }
        }


        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (Request["QuotedID"] != null)
            {
                this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?QuotedID=" + Request["QuotedID"] + "&Kind=0&Typer=2";
            }

            if (Request["DetailedID"] != null)
            {
                this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?DetailedID=" + Request["DetailedID"] + "&MissionID=" + Request["MissionID"] + "&Kind=0&Typer=3&FinishType=" + Request["IsFinish"];
            }


            base.OnInit(e);
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (Request["DetailedID"] != null)
            {
                if (Request["IsFinish"] == "1")
                {
                    if (Request["Kind"] == "0")
                    {
                        JavaScriptTools.AlertAndClosefancybox("保存完毕！", Page);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
                    }
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("保存完毕！", Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
            }
        }
    }
}