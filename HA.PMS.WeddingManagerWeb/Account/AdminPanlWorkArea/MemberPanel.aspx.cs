using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class MemberPanel : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField hfUrl = Master.FindControl("hfUrl") as HiddenField;
            hfUrl.Value = EncodeBase64("/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=");
        }
    }
}