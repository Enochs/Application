
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:查看上级修改页面
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
    public partial class Sys_EmpLoyeeHigherupsUpdate :SystemPage
    {
        EmpLoyeeHigherups objEmpLoyeeHigherupsBLL = new EmpLoyeeHigherups();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int empLoyeeHigherupsId = Request.QueryString["empLoyeeHigherupsId"].ToInt32();
                //获取对应的实体对象用于提供修改之前的信息显示在页面上面
                Sys_EmpLoyeeHigherups sys_EmpLoyeeHigherups = objEmpLoyeeHigherupsBLL.GetByID(empLoyeeHigherupsId);
                txtCode.Text = sys_EmpLoyeeHigherups.FunctionCode;
                txtType.Text =sys_EmpLoyeeHigherups.Type.ToString();

            
            }
        }
        /// <summary>
        /// 修改提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEmployeeHigherups_Click(object sender, EventArgs e)
        {
            int empLoyeeHigherupsId = Request.QueryString["empLoyeeHigherupsId"].ToInt32();
            //提取要修改的实体对象用于修改操作
            Sys_EmpLoyeeHigherups sys_EmpLoyeeHigherups = objEmpLoyeeHigherupsBLL.GetByID(empLoyeeHigherupsId);
            sys_EmpLoyeeHigherups.Type = txtType.Text.ToInt32();
            sys_EmpLoyeeHigherups.FunctionCode = txtCode.Text;
             //保存到数据中
            int result = objEmpLoyeeHigherupsBLL.Update(sys_EmpLoyeeHigherups);
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