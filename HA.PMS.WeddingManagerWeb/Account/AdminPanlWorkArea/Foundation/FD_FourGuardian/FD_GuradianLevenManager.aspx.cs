
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚人员等级管理页面
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
    public partial class FD_GuradianLevenManager : SystemPage
    {
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
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
            int startIndex = LevenPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objGuradianLevenBLL.GetByIndex(LevenPager.PageSize, LevenPager.CurrentPageIndex, out resourceCount);
            LevenPager.RecordCount = resourceCount;
            rptLeven.DataSource = query;
            rptLeven.DataBind();
            #endregion
            
        }
        
        protected void rptLeven_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int LevenId = e.CommandArgument.ToString().ToInt32();
                FD_GuradianLeven fD_GuradianLeven = new FD_GuradianLeven()
                { 
                    LevenId = LevenId
                };
                objGuradianLevenBLL.Delete(fD_GuradianLeven);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void LevenPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}