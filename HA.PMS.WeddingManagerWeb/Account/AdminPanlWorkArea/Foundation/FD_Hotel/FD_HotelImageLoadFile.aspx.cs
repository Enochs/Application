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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class FD_HotelImageLoadFile : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //HandleSaveImg.aspx
               string urlPar = Request.QueryString.GetKey(0);
               int urlParId = Request.QueryString[urlPar].ToInt32();
               //指定在哪个页面保存数据库
               string targetUrlPar = Request.QueryString["toOperPage"];

               ServerFileUpLoad.PostServer = string.Format("{2}.aspx?{0}={1}", urlPar, urlParId, DecodeBase64(targetUrlPar));
               
            }
        }

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
        }
    }
}