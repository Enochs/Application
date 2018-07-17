

/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:员工类型创建页面
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
    public partial class Sys_EmployeeTypeCreate :SystemPage
    {
        EmployeeType objEmployeeBLL = new EmployeeType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
               
            }
        }

        /// <summary>
        /// 员工类型添加操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEmployeeType_Click(object sender, EventArgs e)
        {
            Sys_EmployeeType sys_Employee = new Sys_EmployeeType();
            sys_Employee.Type = txtEmployeeType.Text;
            //默认
            sys_Employee.EmployeeId = User.Identity.Name.ToInt32();
            sys_Employee.CreateTime = DateTime.Now;
            sys_Employee.IsDelete = false;
            //保存到数据库
            int result= objEmployeeBLL.Insert(sys_Employee);
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