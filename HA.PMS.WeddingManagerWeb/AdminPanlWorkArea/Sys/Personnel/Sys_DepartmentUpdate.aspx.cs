
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:部门修改页面
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
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_DepartmentUpdate : SystemPage
    {
        Department objDepartmentBLL = new Department();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //部门ID
                int departId =Request.QueryString["deparId"].ToInt32();
                //部门父级ID
                if (Request["parent"] != null)
                {
                    hideDepartmetnKey.Value = Request["parent"];
                    txtUpdepartmetnname.Text = objDepartmentBLL.GetByID(Request["parent"].ToInt32()).DepartmentName;
                }
                
                //通过传递进来的ID查询某个部门对应的实体类用于界面内容相关信息的显示
                Sys_Department department = objDepartmentBLL.GetByID(departId);
                txtDepartName.Text = department.DepartmentName;
                #region  把部门信息绑定数据源

                txtDepartName.Text = objDepartmentBLL.GetByID(departId).DepartmentName;
                //ddlDepartment.DataSource = objDepartmentBLL.GetByAll();
                //ddlDepartment.DataTextField = "DepartmentName";
                //ddlDepartment.DataValueField = "DepartmentID";
                //ddlDepartment.DataBind();
                ////如果此时parentId 为0的话，就代表要修改的部门是总的父级部门，就不再进行选择
                //if (parentId!=0)
                //{
                //    //通过父级部门ID提供修改之前的部门信息为第一选项
                //    ddlDepartment.Items.FindByValue(parentId.ToString()).Selected = true;
                //}
               
                #endregion

            }
        }
        /// <summary>
        /// 部门修改提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //获取部门ID
            int departId =Request.QueryString["deparId"].ToInt32();
            Sys_Department sys_department = objDepartmentBLL.GetByID(departId);
            sys_department.DepartmentID = departId;
            sys_department.DepartmentName = txtDepartName.Text;
            sys_department.DataSource = hiddDataSource.Value.ToInt32();
            sys_department.Parent =hideDepartmetnKey.Value.ToInt32();
            //提交修改信息
            int result = objDepartmentBLL.Update(sys_department);
            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndReaload("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindowAndReaload("修改失败,请重新尝试", this.Page);

            }
        }
    }
}