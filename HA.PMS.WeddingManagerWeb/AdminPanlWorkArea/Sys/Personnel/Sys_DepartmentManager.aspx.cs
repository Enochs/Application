
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:部门管理页面
 History:修改日志
 
 Author:杨洋
 date:2013.3.12
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
using System.Data.Objects;
using HA.PMS.EditoerLibrary;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_DepartmentManager : SystemPage
    {
        Department ObjDepartmentBLL = new Department();

        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化加载部门信息
                DataBinder();
            }

        }
        /// <summary>
        /// 加载部门信息绑定Repeater控件
        /// </summary>
        private void DataBinder()
        {
            int startIndex = DepartmentPager.StartRecordIndex;
            int resourceCount = 0;

            var query = ObjDepartmentBLL.GetByAll();
            DepartmentPager.RecordCount = resourceCount;
            //加载部门信息
            rptDepartment.DataSource = query;
            rptDepartment.DataBind();


        }


        public string GetItemNbsp(object ItemLevel)
        {
            if (ItemLevel != null)
            {

                int Count = ItemLevel.ToString().ToInt32();
                string Nbsp = "";
                if (Count == 1)
                {
                    return string.Empty;
                }
                for (int i = 0; i < Count; i++)
                {
                    Nbsp += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                return Nbsp;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 通过ItemCommand事件进行删除操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int departmentId = e.CommandArgument.ToString().ToInt32();
                var EmployeeList = ObjEmployeeBLL.GetByDepartmetnID(departmentId);
                if (EmployeeList.Count == 0)
                {

                    //创建部门实体类
                    Sys_Department sys_Department = new Sys_Department()
                    {
                        DepartmentID = departmentId
                    };
                    //保存操作日志
                    CreateHandle(departmentId);
                    //删除部门
                    ObjDepartmentBLL.Delete(sys_Department);
                    //删除之后重新绑定数据源
                    DataBinder();
                }
                else
                {
                    JavaScriptTools.AlertWindow("此部门下还有员工存在，要删除部门请先将员工移除此部门！", Page);
                }

            }


            //保存部门负责人
            if (e.CommandName == "SaveChange")
            {
                var ObjUpdateModel = ObjDepartmentBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                HiddenField ObjddlEmployee = (HiddenField)e.Item.FindControl("hideEmpLoyeeID");
                if (ObjddlEmployee.Value != string.Empty)
                {
                    ObjUpdateModel.DepartmentManager = ObjddlEmployee.Value.ToInt32();
                    ObjDepartmentBLL.Update(ObjUpdateModel);
                }

                JavaScriptTools.AlertWindow("设置成功", Page);
            }
        }

        protected void DepartmentPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int DepartmentID)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            HandleModel.HandleContent = "系统设置-删除部门,部门名称:" + ObjDepartmentBLL.GetByID(DepartmentID).DepartmentName;

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 9;     //系统设置
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}