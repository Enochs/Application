using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSale
{
    public partial class QuotedResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string RequstPage = Request["EmployeeType"];
            switch (RequstPage)
            { 
                    //进入本人负责的报价转销售
                case "1":
                    Response.Redirect("/QuotedPriceWorkPanel.aspx?SaleEmployee=" + User.Identity.Name);
                    break;
                    //进入本人的报价转销售制作报价单
                case "2":
                    Response.Redirect("QuotedPriceListCreateEdit.aspx?SaleEmployee=" + User.Identity.Name);
                    break;
                    //转入正常流程
                case "3":
                    Response.Redirect("/QuotedPriceWorkPanel.aspx?EmployeID=" + User.Identity.Name);
                    break;
                default:
                    Response.Redirect("/AdminPanlWorkArea/QuotedPriceWorkPanel.aspx?SaleEmployee=" + User.Identity.Name);
                    break;

            }
        }
    }
}