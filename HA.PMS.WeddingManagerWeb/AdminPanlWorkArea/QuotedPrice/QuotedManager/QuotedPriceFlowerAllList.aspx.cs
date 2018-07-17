using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedPriceFlowerAllList : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary>  
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Dispatching ObjDispatchingBLL = new Dispatching();

        BLLAssmblly.Flow.QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }

        }

        #region 页面状态显示


        /// <summary>
        /// 显示更新
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string ShowUpdate(object IsFirstCreate)
        {
            return Convert.ToBoolean(IsFirstCreate) ? string.Empty : "style='display:none'";
        }

        /// <summary>
        /// 隐藏提交审核
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideChecks(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            else
            {
                if (State.ToString().ToInt32() == 3)
                {
                    return string.Empty;

                }
                else
                {
                    return "style='display:none'";
                }
            }
        }

        /// <summary>
        /// 绑定样式
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string SetClass(object QuotedID)
        {

            var ObjQuotedModel = ObjDispatchingBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            if (ObjQuotedModel != null)
            {
                return string.Empty;
            }
            else
            {
                return "RemoveClass";
            }

        }



        /// <summary>
        /// 根据报价单获取执行单ID
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetDispatchingIDByQuotedID(string QuotedID)
        {

            var ObjQuotedModel = ObjDispatchingBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            if (ObjQuotedModel != null)
            {
                return ObjQuotedModel.DispatchingID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetOrderEmoney(object OrderID)
        {
            return ObjOrderBLL.GetByID(OrderID.ToString().ToInt32()).EarnestMoney.ToString();
        }


        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideCreate(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            if (State.ToString() == "False")
            {
                return string.Empty;

            }
            else
            {
                return "style='display:none'";
            }
        }


        #endregion

        private void BinderData()
        {
            List<PMSParameters> objParmList = new List<PMSParameters>();




            switch (Request["Typer"])
            {
                //花艺
                case "Flower":


                    objParmList.Add("FlowerCheckEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    objParmList.Add("FlowerCheck", false, NSqlTypes.Equal);
                    break;
                //核价
                case "Money":

                    objParmList.Add("MoneyCheckEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    objParmList.Add("MoneyCheck", false, NSqlTypes.Equal);
                    break;
                //采购
                case "SaleMoney":


                    objParmList.Add("SaleEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    break;
            }



            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);

            objParmList.Add("PartyDate", PartyDateRanger.Start + "," + PartyDateRanger.End, NSqlTypes.DateBetween);


            objParmList.Add("State", 0, NSqlTypes.Greaterthan);
            objParmList.Add("Expr1", false, NSqlTypes.Equal);

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.QuotedPrice().GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
        }

        /// <summary>
        /// 获取已付款
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetQuotedDispatchingFinishMoney(object CustomerID)
        {
            HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney ObjOrderEarnestMoneyBLL = new HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal FinishAmount = 0;

            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
            FinishAmount += EarnestMoney;

            //获得收款计划的东西
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);

            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }

            //定金
            var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            if (ObjEorder != null)
            {
                if (ObjEorder.EarnestMoney.HasValue)
                {
                    FinishAmount += ObjEorder.EarnestMoney.Value;
                }
            }
            return FinishAmount.ToString();

        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {

        }

        protected string GetTotalPurchasePrice(object QuotedID)
        {
            return ObjQuotedPriceItemsBLL.GetTotalPurchasePrice(Convert.ToInt32(QuotedID)).ToString("f2");
        }

        protected string GetTotalUnitPrice(object QuotedID)
        {
            return ObjQuotedPriceItemsBLL.GetTotalUnitPrice(Convert.ToInt32(QuotedID)).ToString("f2");
        }

        protected string GetPriceRate(object UnitPrice, object PurchasePrice)
        {
            decimal unitPrice = Convert.ToDecimal(UnitPrice);
            decimal purchasePrice = Convert.ToDecimal(PurchasePrice);
            if (purchasePrice != 0)
            {
                return (decimal.Divide(unitPrice, purchasePrice) * 100).ToString("0.00") + "%";
                //return (decimal.Divide(unitPrice - purchasePrice, purchasePrice) * 100).ToString("0.00") + "%";
            }
            return "-";
        }
    }
}