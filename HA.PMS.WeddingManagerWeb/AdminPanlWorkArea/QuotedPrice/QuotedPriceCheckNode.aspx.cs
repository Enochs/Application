using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceCheckNode : System.Web.UI.Page
    {
        int QuotedID = 0;
        /// <summary>
        /// 报价单主表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QuotedID = Request["QuotedID"].ToInt32();
                var ObjQuotedModel=ObjQuotedPriceBLL.GetByID(QuotedID);
                lblCheckState.Text= ObjQuotedModel.ChecksTitle;
                lblCheckNode.Text = ObjQuotedModel.ChecksContent;

            }
        }
    }
}