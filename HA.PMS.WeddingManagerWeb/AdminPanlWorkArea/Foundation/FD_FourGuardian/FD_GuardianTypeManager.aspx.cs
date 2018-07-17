
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚类型管理页面
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
    public partial class FD_GuardianTypeManager : SystemPage
    {
        GuardianType objGuardianTypeBLL = new GuardianType();
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
            int startIndex = TypePager.StartRecordIndex;
            int resourceCount = 0;
            var query = objGuardianTypeBLL.GetByIndex(TypePager.PageSize, TypePager.CurrentPageIndex, out resourceCount);
            TypePager.RecordCount = resourceCount;
            rptType.DataSource = query;
            rptType.DataBind();
            #endregion

            rptType.DataSource = objGuardianTypeBLL.GetByAll();
            rptType.DataBind();
        }

        protected void rptType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int TypeId = e.CommandArgument.ToString().ToInt32();
                HA.PMS.DataAssmblly.FD_GuardianType fD_GuardianType = 
                    new HA.PMS.DataAssmblly.FD_GuardianType()
                {
                     TypeId = TypeId
                };
                objGuardianTypeBLL.Delete(fD_GuardianType);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void TypePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}