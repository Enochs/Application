using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPricefileDownload :SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
            this.repfilelist.DataSource=ObjQuotedPriceBLL.GetQuotedPricefileByQuotedID(Request["QuotedID"].ToInt32(), 2);
            this.repfilelist.DataBind();
        }
    }
}