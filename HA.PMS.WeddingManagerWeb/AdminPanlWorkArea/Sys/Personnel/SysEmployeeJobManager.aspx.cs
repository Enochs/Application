/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:员工工作管理页面
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
    public partial class SysEmployeeJobManager :SystemPage
    {
        EmployeeJobI objEmployeeJobBLL = new EmployeeJobI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化加载数据源
                DataBinder();
            }

           
        }
        /// <summary>
        /// 绑定数据集
        /// </summary>
        protected void DataBinder() 
        {
            int startIndex = EmployeeJobPager.StartRecordIndex;
            int resourceCount = 0;

            var query = objEmployeeJobBLL.GetByIndex(EmployeeJobPager.PageSize, EmployeeJobPager.CurrentPageIndex, out resourceCount);
            EmployeeJobPager.RecordCount = resourceCount;
           
            rptJob.DataSource = query;
            rptJob.DataBind();

            
        
        }

        protected void rptJob_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int jobId = e.CommandArgument.ToString().ToInt32();
               
                Sys_EmployeeJob sys_EmployeeJob = new Sys_EmployeeJob()
                {
                     JobID=jobId

                };

                objEmployeeJobBLL.Delete(sys_EmployeeJob);
                DataBinder();
            }
        }

        protected void EmployeeJobPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}