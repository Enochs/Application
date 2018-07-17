/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:取件修改页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
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
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_TakeDiskUpdate : SystemPage
    {
        TakeDisk objTakeDiskBLL = new TakeDisk();
        Customers objCustomersBLL = new Customers();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                int TakeID = Request.QueryString["TakeID"].ToInt32();
                CS_TakeDisk cS_TakeDisk = objTakeDiskBLL.GetByID(TakeID);

                txtNoticeTime.Text = cS_TakeDisk.NoticeTime + string.Empty;
                txtrealityTime.Text = cS_TakeDisk.realityTime + string.Empty;
                txtTakePlanTime.Text = cS_TakeDisk.TakePlanTime + string.Empty;
                txtConsigneeTime.Text = cS_TakeDisk.ConsigneeTime + string.Empty;
                ddlCustomer.Items.FindByValue(cS_TakeDisk.CustomerID + string.Empty).Selected = true;

 
            }
        }

        protected void DataBinder()
        {
            ddlCustomer.DataSource = objCustomersBLL.GetByAll();
            ddlCustomer.DataTextField = "Groom";
            ddlCustomer.DataValueField = "CustomerID";
            ddlCustomer.DataBind();
 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int TakeID = Request.QueryString["TakeID"].ToInt32();
            CS_TakeDisk cS_TakeDisk = objTakeDiskBLL.GetByID(TakeID);

            cS_TakeDisk.NoticeTime = txtNoticeTime.Text.ToDateTime();
            cS_TakeDisk.realityTime = txtrealityTime.Text.ToDateTime();
            cS_TakeDisk.TakePlanTime = txtTakePlanTime.Text.ToDateTime();
            cS_TakeDisk.ConsigneeTime = txtConsigneeTime.Text.ToDateTime();
           
            cS_TakeDisk.CustomerID = ddlCustomer.SelectedValue.ToInt32();
   


            int result = objTakeDiskBLL.Update(cS_TakeDisk);
            //根据返回判断添加的状态
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