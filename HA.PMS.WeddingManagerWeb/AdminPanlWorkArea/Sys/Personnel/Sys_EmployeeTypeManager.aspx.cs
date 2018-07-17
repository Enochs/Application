
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:员工类型管理页面
 History:修改日志
 
 Author:杨洋
 date:2013.3.13
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmployeeTypeManager : SystemPage
    {
        EmployeeType objEmployeeTypeBLL = new EmployeeType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化绑定数据源
                DataBinder();
            }
        }
        /// <summary>
        /// 绑定数据源到Repeater控件中
        /// </summary>
        private void DataBinder()
        {
            int startIndex = EmployeeTypePager.StartRecordIndex;
            int resourceCount = 0;

            var query = objEmployeeTypeBLL.GetByIndex(EmployeeTypePager.PageSize, EmployeeTypePager.CurrentPageIndex, out resourceCount);
            EmployeeTypePager.RecordCount = resourceCount;
            //加载员工类型信息
            rptEmployeeType.DataSource = query;
            rptEmployeeType.DataBind();

        
             
        }
        /// <summary>
        /// Repeater ItemCommand事件用于删除操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptEmployeeType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //通过CommandName判断当前操作是删除
            if (e.CommandName == "Delete")
            {
                int employeeTypeId = e.CommandArgument.ToString().ToInt32();

                Sys_EmployeeType sys_EmployeeType = new Sys_EmployeeType()
                {

                    EmployeeTypeID = employeeTypeId

                };

                objEmployeeTypeBLL.Delete(sys_EmployeeType);
                //删除之后重新绑定
                DataBinder();
                JavaScriptTools.AlertWindow("删除成功!",Page);
            }
        }

        protected void EmployeeTypePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}