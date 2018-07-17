
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.8
 Description:套系风格管理页面
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
    public partial class FD_CelebrationPackageStyleManager : SystemPage
    {
        CelebrationPackageStyle objCelebrationPackageStyleLL = new CelebrationPackageStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        protected void DataBinder() 
        {
            #region 分页页码
            int startIndex = StylePager.StartRecordIndex;
            int resourceCount = 0;
            var query = objCelebrationPackageStyleLL.GetByIndex(StylePager.PageSize, StylePager.CurrentPageIndex, out resourceCount);

            StylePager.RecordCount = resourceCount;

            rptStyle.DataSource = query;
            rptStyle.DataBind();


            #endregion

        }
        protected void StylePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptStyle_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int StyleId = e.CommandArgument.ToString().ToInt32();
                FD_CelebrationPackageStyle style = objCelebrationPackageStyleLL.GetByID(StyleId);
                objCelebrationPackageStyleLL.Delete(style);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }
    }
}