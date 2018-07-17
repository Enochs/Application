
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:套系产品选取页面
 History:修改日志

 Author:杨洋
 date:2013.3.21
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    public partial class FD_CelebrationProductCreate : SystemPage
    {
        CelebrationPackageMakeQuotedPrice objCelebrationPackageMakeQuotedPriceBLL = new CelebrationPackageMakeQuotedPrice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                DataBinder();
            }
        }
        protected void DataBinder() 
        {
              //int sourceCount=0;
              //rptCelebrationPackageMakeQuotedPrice.DataSource = objCelebrationPackageMakeQuotedPriceBLL.
              //GetFD_ProductStorehouseByIndex(10, 1, out sourceCount);
              //rptCelebrationPackageMakeQuotedPrice.DataBind();
        
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string productIds = "";
            for (int i = 0; i < rptCelebrationPackageMakeQuotedPrice.Items.Count; i++)
            {
                CheckBox chk = rptCelebrationPackageMakeQuotedPrice.Items[i]
                               .FindControl("chkChoose") as CheckBox;
                if (chk.Checked)
                {
                    productIds += chk.ToolTip + ",";
                }
            }
         
            productIds=productIds.Substring(0, productIds.Length - 1);
            string cidFirst = Request.QueryString["cidFirst"];
            string cidSecond = Request.QueryString["cidSecond"];

            JavaScriptTools.RegisterJsCodeSource("alert('选择成功');parent.$.fancybox.close(1);parent.window.location.href ="
            + "'FD_CelebrationPackageMakeQuotedPrice.aspx?productIds=" +
            productIds + "&cidFirst=" + cidFirst + "&cidSecond=" + cidSecond + "';", this.Page);
         
            
           
        }
    }
}