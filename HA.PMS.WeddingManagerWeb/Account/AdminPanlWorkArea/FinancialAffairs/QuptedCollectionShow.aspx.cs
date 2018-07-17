using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class QuptedCollectionShow : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.repDataKist.DataSource = ObjQuotedCollectionsPlanBLL.GetByOrderID(Request["OrderID"].ToInt32());
            this.repDataKist.DataBind();

            GetOverFinishMoney(Request["OrderID"].ToInt32());

        }




        /// <summary>
        /// 获取余款数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public void GetOverFinishMoney(object OrderID)
        {
            string AllMoney = string.Empty;
            string EarnestMoney = string.Empty; ;

            AllMoney = ObjQuotedPriceBLL.GetByOrderId(OrderID.ToString().ToInt32()).FinishAmount.ToString();
            EarnestMoney = ObjQuotedPriceBLL.GetByOrderId(OrderID.ToString().ToInt32()).EarnestMoney.ToString();
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID.ToString().ToInt32());
            decimal FinishAmount = 0;
            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }
            var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(OrderID.ToString().ToInt32());
            if (ObjEorder != null)
            {
                if (ObjEorder.EarnestMoney != null)
                {
                    FinishAmount += ObjEorder.EarnestMoney.Value;
                }
            }
            if (EarnestMoney != null)
            {
                FinishAmount += EarnestMoney.ToString().ToDecimal();
            }

            if (AllMoney != null)
            {

                lblyukuan.Text = (AllMoney.ToString().ToDecimal() - FinishAmount).ToString();
            }
            else
            {
                lblyukuan.Text = string.Empty;
            }
        }
    }
}