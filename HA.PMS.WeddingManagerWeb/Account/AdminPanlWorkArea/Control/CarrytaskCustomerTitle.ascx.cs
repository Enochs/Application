using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CarrytaskCustomerTitle : System.Web.UI.UserControl
    {
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();


        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        /// <summary>
        /// 初始化绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CustomerID <= 0)
            {
                CustomerID = Request["CustomerID"].ToInt32();
            }

            var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (ObjCustomerModel != null)
            {
                var ObjOrderModel = ObjCustomersBLL.GetByID(CustomerID);
                lblCoder.Text = GetOrderCoder(CustomerID);
                lblHotel.Text = ObjCustomerModel.Wineshop;
                lblPartyDate.Text = ObjCustomerModel.PartyDate.Value.ToShortDateString();
                lblPhone.Text = ObjCustomerModel.BrideCellPhone;

                lblGroomname.Text = ObjCustomerModel.Groom;
                lblGroomPhone.Text = ObjCustomerModel.GroomCellPhone;

                lblTimerSpan.Text = ObjCustomerModel.TimeSpans;

                lblCustomerName.Text = ObjCustomerModel.Bride;


                //lblyukuan.Text = GetOverFinishMoney(ObjCustomerModel.CustomerID);
                var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);

                //lblTyper.Text = QuotedModel.PakegTyper;
                //lblPakegName.Text = QuotedModel.PakegName;
            }
        }



        /// <summary>
        /// 获取余款数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetOverFinishMoney(int CustomerID)
        {
            OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
            /// <summary>
            /// 收款计划
            /// </summary>

            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();

            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
            decimal AllMoney = QuotedModel.FinishAmount.Value;
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);
            decimal FinishAmount = 0;
            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }
            var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            if (ObjEorder.EarnestMoney != null)
            {
                FinishAmount += ObjEorder.EarnestMoney.Value;
            }

            if (AllMoney != null)
            {

                return (AllMoney.ToString().ToDecimal() - FinishAmount).ToString();
            }
            else
            {
                return "";
            }
        }

        public string GetOrderCoder(object source)
        {
            int CustomerID = source.ToString().ToInt32();
            var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (ObjOrderModel != null)
            {
                if (ObjOrderModel.OrderCoder != null)
                {
                    return ObjOrderModel.OrderCoder.ToString();
                }
                else if (ObjOrderModel.OrderCoder == null)
                {
                    ObjOrderModel.OrderCoder = DateTime.Now.ToString("yyyyMMddHHmmss");
                    ObjOrderBLL.Update(ObjOrderModel);
                    return ObjOrderModel.OrderCoder.ToString();
                }
            }
            return "";
        }
    }
}