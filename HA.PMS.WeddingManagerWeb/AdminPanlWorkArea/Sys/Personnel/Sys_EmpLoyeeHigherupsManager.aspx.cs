
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:查看上级管理页面
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
    public partial class Sys_EmpLoyeeHigherupsManager :SystemPage
    {
        EmpLoyeeHigherups objEmpLoyeeHigherupsBLL = new EmpLoyeeHigherups();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化绑定数据
                DataBinder();
            }
        }
        /// <summary>
        /// 用于绑定数据源
        /// </summary>
        private void DataBinder()
        {
            //加载信息Repeater
          

            int startIndex = EmpLoyeeHigherupsPager.StartRecordIndex;
            int resourceCount = 0;

            var query = objEmpLoyeeHigherupsBLL.GetByIndex(EmpLoyeeHigherupsPager.PageSize, EmpLoyeeHigherupsPager.CurrentPageIndex, out resourceCount);
            EmpLoyeeHigherupsPager.RecordCount = resourceCount;
            //加载部门信息
            rptEmpLoyeeHigherups.DataSource = query;
            rptEmpLoyeeHigherups.DataBind();
        }
        /// <summary>
        /// ItemCommand的删除操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptEmpLoyeeHigherups_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //通过CommandName来区分
            if (e.CommandName == "Delete")
            {
                int UpKey = e.CommandArgument.ToString().ToInt32();
                 Sys_EmpLoyeeHigherups sys_EmpLoyeeHigherups=new Sys_EmpLoyeeHigherups ()
                 {
                     UpKey = UpKey
                 };

                //进行删除操作
                 objEmpLoyeeHigherupsBLL.Delete(sys_EmpLoyeeHigherups);
                 //删除之后重新绑定
                 DataBinder();
            }
        }

        protected void EmpLoyeeHigherupsPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

    }
}