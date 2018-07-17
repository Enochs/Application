
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:查看上级创建页面
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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmpLoyeeHigherupsCreate : SystemPage
    {
        EmpLoyeeHigherups objEmpLoyeeHigherupsBLL = new EmpLoyeeHigherups();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

            }
        }

        protected void btnEmployeeHigherups_Click(object sender, EventArgs e)
        {
             //创建对应的实体对象
            Sys_EmpLoyeeHigherups sys_EmpLoyeeHigherups = new Sys_EmpLoyeeHigherups();
            sys_EmpLoyeeHigherups.createTime = DateTime.Now;
            sys_EmpLoyeeHigherups.IsDelete = true;
            sys_EmpLoyeeHigherups.Type = txtType.Text.ToInt32();
            sys_EmpLoyeeHigherups.FunctionCode = txtCode.Text;
            sys_EmpLoyeeHigherups.EmployeeID = User.Identity.Name.ToInt32();
            //保存到数据库中
            int result = objEmpLoyeeHigherupsBLL.Insert(sys_EmpLoyeeHigherups);
            //根据添加信息之后
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