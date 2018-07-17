
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.8
 Description:套系价格段修改页面
 History:修改日志

 Author:杨洋
 Date:2013.4.8
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
    public partial class FD_CelebrationPackagePriceSpanUpdate : SystemPage
    {
        CelebrationPackagePriceSpan objCelebrationPackagePriceSpanBLL = new CelebrationPackagePriceSpan();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int SpanID = Request.QueryString["SpanID"].ToInt32();
                FD_CelebrationPackagePriceSpan span = objCelebrationPackagePriceSpanBLL.GetByID(SpanID);
                txtSpan.Text = span.SpanPrice;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SpanID = Request.QueryString["SpanID"].ToInt32();
            FD_CelebrationPackagePriceSpan span = objCelebrationPackagePriceSpanBLL.GetByID(SpanID);

            span.SpanPrice = txtSpan.Text;
            int result = objCelebrationPackagePriceSpanBLL.Update(span);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
    }
}