/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:员工工作修改页面
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
    public partial class SysEmployeeJobUpdate :SystemPage
    {
        EmployeeJobI objEmployeeJobBLL = new EmployeeJobI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //通过传递的工作ID
                 int jobId = Request.QueryString["jobId"].ToInt32();
                 //查询对应Name
                 Sys_EmployeeJob sys_employeeJob = objEmployeeJobBLL.GetByID(jobId);
                 txtJobName.Text = sys_employeeJob.Jobname;

            }
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            int jobId =Request.QueryString["jobId"].ToInt32();
             //查询对应要修改的实体对象
            Sys_EmployeeJob sys_employeeJob = objEmployeeJobBLL.GetByID(jobId);
            sys_employeeJob.Jobname = txtJobName.Text;
            int result= objEmployeeJobBLL.Update(sys_employeeJob);
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