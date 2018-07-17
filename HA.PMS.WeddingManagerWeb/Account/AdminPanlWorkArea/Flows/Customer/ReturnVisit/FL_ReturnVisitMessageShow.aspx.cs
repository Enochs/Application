using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit
{
    public partial class FL_ReturnVisitMessageShow : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
                repItemList.DataSource=objCustomerReturnVisitBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
                repItemList.DataBind();
              
            }
        }


    }
}