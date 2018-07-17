/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.24
 Description:投诉意见修改页面
 History:修改日志

 Author:杨洋
 date:2013.3.24
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
    public partial class CS_ComplainUpdate : SystemPage
    {
        Complain objComplainBLL = new Complain();
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ComplainID = Request.QueryString["ComplainID"].ToInt32();
                CS_Complain cs_Complain = objComplainBLL.GetByID(ComplainID);
                txtComplainRemark.Text = cs_Complain.ComplainRemark;
                txtReturnContent.Text = cs_Complain.ReturnContent;
                ltlComplainDate.Text = cs_Complain.ComplainDate + string.Empty;
                ltlComplainContent.Text = cs_Complain.ComplainContent;
                ltlCustomers.Text = objCustomersBLL.GetByID(cs_Complain.CustomerID).Groom;
                
            }
        }

        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ComplainID = Request.QueryString["ComplainID"].ToInt32();
            CS_Complain cs_Complain = objComplainBLL.GetByID(ComplainID);
            cs_Complain.ComplainRemark = txtComplainRemark.Text;
            cs_Complain.ComplainEmployeeId = User.Identity.Name.ToInt32();
            cs_Complain.ReturnContent = txtReturnContent.Text;
            cs_Complain.ReturnDate = DateTime.Now;
            cs_Complain.IsDelete = false;

            int result = objComplainBLL.Update(cs_Complain);
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("处理成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("处理失败,请重新尝试", this.Page);

            }
        }
    }
}