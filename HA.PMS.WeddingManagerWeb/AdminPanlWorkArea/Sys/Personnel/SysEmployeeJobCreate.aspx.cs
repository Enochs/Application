
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:创建工作信息页面
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
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class SysEmployeeJobCreate : SystemPage
    {
        EmployeeJobI objEmployeeJobBLL = new EmployeeJobI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnEmployee_Click(object sender, EventArgs e)
        {
            Sys_EmployeeJob sys_EmployeeJob = new Sys_EmployeeJob();
            sys_EmployeeJob.Jobname = txtJobName.Text;
            sys_EmployeeJob.createTime = DateTime.Now;
            sys_EmployeeJob.IsDelete = false;
            //默认是1
            sys_EmployeeJob.EmployeeId = User.Identity.Name.ToInt32();
            int result= objEmployeeJobBLL.Insert(sys_EmployeeJob);
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