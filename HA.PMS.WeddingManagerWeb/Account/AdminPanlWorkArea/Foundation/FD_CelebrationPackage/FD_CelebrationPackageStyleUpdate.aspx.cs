
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.8
 Description:套系风格修改页面
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
    public partial class FD_CelebrationPackageStyleUpdate : SystemPage
    {
        CelebrationPackageStyle objCelebrationPackageStyleBLL = new CelebrationPackageStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int StyleId = Request.QueryString["StyleId"].ToInt32();
                FD_CelebrationPackageStyle style = objCelebrationPackageStyleBLL.GetByID(StyleId);
                txtExplain.Text = style.StyleExplain;
                txtStyleName.Text = style.StyleName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int StyleId = Request.QueryString["StyleId"].ToInt32();
            FD_CelebrationPackageStyle style = objCelebrationPackageStyleBLL.GetByID(StyleId);

            style.StyleExplain =txtExplain.Text;
            style.StyleName = txtStyleName.Text;


            int result = objCelebrationPackageStyleBLL.Update(style);
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