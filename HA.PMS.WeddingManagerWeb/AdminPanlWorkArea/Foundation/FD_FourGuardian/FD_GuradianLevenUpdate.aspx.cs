
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚人员等级修改页面
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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class FD_GuradianLevenUpdate : SystemPage
    {
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int LevenId = Request.QueryString["LevenId"].ToInt32();
                FD_GuradianLeven fD_GuradianLeven = objGuradianLevenBLL.GetByID(LevenId);
                txtLevenName.Text = fD_GuradianLeven.LevenName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int LevenId = Request.QueryString["LevenId"].ToInt32();
            FD_GuradianLeven fD_GuradianLeven = objGuradianLevenBLL.GetByID(LevenId);
            fD_GuradianLeven.LevenName = txtLevenName.Text;
            fD_GuradianLeven.IsDelete = false;
            int result = objGuradianLevenBLL.Update(fD_GuradianLeven);
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