/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:创建部门页面
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
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_DepartmentCreate : SystemPage
    {
        Department ObjDepartmentBLL = new Department();

        Channel ObjChannelBLL = new Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Paretnt"] != null)
                {
                    hideDepartmetnKey.Value = Request["Paretnt"];
                    txtUpdepartmetnname.Text = ObjDepartmentBLL.GetByID(Request["Paretnt"].ToInt32()).DepartmentName;
                    hiddDataSource.Value = ObjDepartmentBLL.GetByID(Request["Paretnt"].ToInt32()).DataSource + string.Empty;
                }

                ////绑定部门信息到DropDownList中用于用户选择
                //ddlDepart.DataSource = ObjDepartmentBLL.GetByAll();
                //ddlDepart.DataTextField = "DepartmentName";
                //ddlDepart.DataValueField = "DepartmentID";

                //ddlDepart.DataBind();
                //ddlDepart.Items.Add(new ListItem("请选择", "0"));
                //ddlDepart.SelectedIndex = ddlDepart.Items.Count - 1;
            }
        }


        /// <summary>
        /// 获取平级SortOrder
        /// </summary>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        private string GetSortOrder(string SortOrder, int SourceCount)
        {
            return SortOrder + string.Empty + SourceCount;
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {
            string SortOrder = string.Empty;
            if (hideDepartmetnKey.Value.ToInt32() == 0)         //创建顶级部门
            {
                SortOrder = ObjDepartmentBLL.GetFirstSortOrder(0, 0);
                CreateHandle(1);
            }
            else                //创建下级部门
            {
                SortOrder = ObjDepartmentBLL.GetFirstSortOrder(-1, hideDepartmetnKey.Value.ToInt32());
                CreateHandle(2);
            }
            //创建部门信息，填入数据库中
            int result = ObjDepartmentBLL.Insert(new DataAssmblly.Sys_Department()
            {
                DepartmentName = txtDeparName.Text,
                Parent = hideDepartmetnKey.Value.ToInt32(), //ddlDepart.SelectedValue.ToInt32(),
                IsDelete = false,
                createTime = DateTime.Now,
                SortOrder = SortOrder,
                ItemLevel = ObjDepartmentBLL.GetItemLevel(hideDepartmetnKey.Value.ToInt32()),
                DepartmentManager = 0,
                EmployeeID = User.Identity.Name.ToInt32(),
                DataSource = hiddDataSource.Value.ToInt32()
            });


            //部门添加之后标识
            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndReaload("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindowAndReaload("添加失败,请重新尝试", this.Page);

            }
        }


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int Type)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            if (Type == 1)      //创建顶级部门
            {
                HandleModel.HandleContent = "系统设置-添加部门,部门名称:" + txtDeparName.Text.Trim().ToString();
            }
            else
            {
                HandleModel.HandleContent = "系统设置-添加部门,部门名称:" + txtDeparName.Text.Trim().ToString() + "上级部门:" + txtUpdepartmetnname.Text.Trim().ToString();
            }

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