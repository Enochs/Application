using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignImageUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["FinishFloder"].Value = "DesignFile";
            Session["PostServerUri"] = "/AdminPanlWorkArea/Control/FileServer.aspx?DesignClassID=" + Request["DesignClassID"].ToInt32() + "&OrderID=" + Request["OrderID"].ToInt32() + "&Type=" + Request["Type"].ToInt32() + "&Typer=8";
        }

        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?DesignClassID=" + Request["DesignClassID"].ToInt32() + "&OrderID=" + Request["OrderID"].ToInt32() + "&Type=" + Request["Type"].ToInt32() + "&Typer=8";
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