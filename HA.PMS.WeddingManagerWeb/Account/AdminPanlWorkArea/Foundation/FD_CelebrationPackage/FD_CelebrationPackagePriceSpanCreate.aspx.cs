
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.8
 Description:套系价格段添加页面
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
    public partial class FD_CelebrationPackagePriceSpanCreate : SystemPage
    {
        CelebrationPackagePriceSpan objCelebrationPackagePriceSpanBLL = new CelebrationPackagePriceSpan();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FD_CelebrationPackagePriceSpan span = new FD_CelebrationPackagePriceSpan();
            span.SpanPrice = txtSpan.Text;
            span.IsDelete = false;
            int result = objCelebrationPackagePriceSpanBLL.Insert(span);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }
        }
    }
}