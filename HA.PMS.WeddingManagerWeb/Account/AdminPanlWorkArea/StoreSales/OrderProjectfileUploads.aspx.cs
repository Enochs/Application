using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderProjectfileUploads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies["FinishFloder"].Value = "OrderProjectFile";
        }



        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //if (Request["QuotedID"] != null)
            //{
            //    this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?QuotedID=" + Request["QuotedID"] + "&Kind=0&Typer=2";
            //}

            //if (Request["DetailedID"] != null)
            //{
                this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?OrderID=" + Request["OrderID"] + "&Kind=0&Typer=4";
            //}


            base.OnInit(e);
        }

    }
}