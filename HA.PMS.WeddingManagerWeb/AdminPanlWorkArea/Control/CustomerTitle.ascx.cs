using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CustomerTitle : System.Web.UI.UserControl
    {
        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();

        Order ObjOrderBLL = new Order();
        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            if (!IsPostBack)
            {
                BinderCuseomerDate();
            }

        }


        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            lblCoder.Text = ObjOrderModel.OrderCoder;
            lblCustomerName.Text = ObjCustomerModel.Bride;
            lblHotel.Text = ObjCustomerModel.Wineshop;
            lblPartyDate.Text = ObjCustomerModel.PartyDate.Value.ToShortDateString();
            lblPhone.Text = ObjCustomerModel.GroomCellPhone;
            //lblTyper.Text = "套系";
            lblTimerSpan.Text = ObjCustomerModel.TimeSpans;

         

        }
    }
}