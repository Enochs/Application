/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:员工类型修改页面
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
    public partial class Sys_EmployeeTypeUpdate :SystemPage
    {
        EmployeeType objEmployeeTypeBLL = new EmployeeType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int employeeTypeId = Request.QueryString["employeeTypeId"].ToInt32();
                Sys_EmployeeType sys_employeeType = objEmployeeTypeBLL.GetByID(employeeTypeId);
                txtEmployeeType.Text = sys_employeeType.Type;
            }
        }
        /// <summary>
        /// 员工类型修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEmployeeType_Click(object sender, EventArgs e)
        {
            int employeeTypeId =  Request.QueryString["employeeTypeId"].ToInt32();
            Sys_EmployeeType sys_employeeType = objEmployeeTypeBLL.GetByID(employeeTypeId);
            sys_employeeType.Type = txtEmployeeType.Text;
            //修改之后提交
            int result = objEmployeeTypeBLL.Update(sys_employeeType);
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