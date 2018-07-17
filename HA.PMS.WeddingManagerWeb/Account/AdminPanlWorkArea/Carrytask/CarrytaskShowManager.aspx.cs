using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskShowManager :SystemPage
    {
        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

        Customers ObjCustomersBLL = new Customers();


        /// <summary>
        /// 报价单基础表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //客户ID
        int CustomersID = 0;
        //坤ID
        int OrderID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            OrderID = Request["QuotedID"].ToInt32();
            CustomersID = Request["CustomerID"].ToInt32();
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            var DataSource = ObjProductforDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32(), 1);
            this.reptabstitle.DataSource = DataSource;
            this.reptabstitle.DataBind();
            this.reptabContent.DataSource = DataSource;
            this.reptabContent.DataBind();

            var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomersID);
            lblCoder.Text = "订单号";
            lblHotel.Text = ObjCustomerModel.Wineshop;
            lblPartyDate.Text = ObjCustomerModel.PartyDate.Value.ToShortDateString();
            lblPhone.Text = ObjCustomerModel.GroomCellPhone;
            lblpinpai.Text = "品牌";
            lblTimerSpan.Text = ObjCustomerModel.TimeSpans;
            lblTyper.Text = "风格";


        }
    }
}