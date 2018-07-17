/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.24
 Description:投诉意见详细页面
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
    public partial class CS_ComplainDetails : SystemPage
    {
        Complain objComlainBLL = new Complain();
        Customers objCustomersBLL = new Customers();
        Employee objEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ComplainID = Request.QueryString["ComplainID"].ToInt32();
                CS_Complain cs_Complain = objComlainBLL.GetByID(ComplainID);
                ltlComplainRemark.Text = cs_Complain.ComplainRemark; 
                ltlEmployee.Text = objEmployeeBLL.GetByID(cs_Complain.ComplainEmployeeId).EmployeeName;
                ltlComplainDate.Text =GetDateStr( cs_Complain.ComplainDate) + string.Empty;
                ltlComplainContent.Text = cs_Complain.ComplainContent;
                ltlCustomerID.Text = objCustomersBLL.GetByID(cs_Complain.CustomerID).Bride;
                ltlReturnDate.Text = GetDateStr(cs_Complain.ReturnDate) + string.Empty;
                ltlReturnContent.Text = cs_Complain.ReturnContent;
            }
        }
    }
}