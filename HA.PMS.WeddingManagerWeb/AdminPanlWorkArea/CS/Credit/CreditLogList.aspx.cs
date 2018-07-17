using HA.PMS.BLLAssmblly.CS;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit
{
    public partial class CreditLogList : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reppointList.DataBind(new CreditLog().GetByCustomerID(Request["CustomerID"].ToInt32()));
            }
        }
    }
}