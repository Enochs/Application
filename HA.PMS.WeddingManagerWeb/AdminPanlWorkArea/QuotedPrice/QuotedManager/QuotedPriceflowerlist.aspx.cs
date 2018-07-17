using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedPriceflowerlist : SystemPage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }

        }

        #region 页面状态显示




        /// <summary>
        /// 获取订单显示状态
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetState(object QuotedID)
        {
            //var ObjModel = ObjQuotedPriceBLL.GetByID(QuotedID.ToString().ToInt32());
            string ReturnValue = string.Empty;
            //if (ObjModel.FlowerCheck != null)
            //{
            //    if (ObjModel.FlowerCheck.Value)
            //    {
            //        ReturnValue = "花艺已核价";
            //    }
            //}

            //if (ObjModel.FlowerCheck != null)
            //{
            //    if (ObjModel.FlowerCheck.Value)
            //    {
            //        ReturnValue = "核价部已核价";
            //    }
            //}


            //if (ObjModel.FlowerCheck != null)
            //{
            //    if (ObjModel.FlowerCheck.Value)
            //    {
            //        ReturnValue = "采购已核价";
            //    }
            //}

            return ReturnValue;
        }

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
            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add("ParentQuotedID", 0);
            paramsList.Add("Expr1", false);
            paramsList.Add("State_NumGreaterthan", 0);



            switch (Request["Typer"])
            {
                //花艺
                case "Flower":
                    paramsList.Add("FlowerCheckEmployee", User.Identity.Name.ToInt32());

                    if (Request["State"].ToInt32() == 1)
                    {
                        paramsList.Add("FlowerCheck", false);
                    }

                    if (Request["State"].ToInt32() == 2)
                    {
                        paramsList.Add("FlowerCheck", true);
                    }
                    break;
                //核价
                case "Money":
                    paramsList.Add("MoneyCheckEmployee", User.Identity.Name.ToInt32());
                    if (Request["State"].ToInt32() == 1)
                    {
                        paramsList.Add("MoneyCheck", false);
                    }

                    if (Request["State"].ToInt32() == 2)
                    {
                        paramsList.Add("MoneyCheck", true);
                    }

                    break;
                //采购
                case "SaleMoney":
                    paramsList.Add("BuyCheckEmployee", User.Identity.Name.ToInt32());
                    if (Request["State"].ToInt32() == 1)
                    {
                        paramsList.Add("BuyCheck", false);
                    }

                    if (Request["State"].ToInt32() == 2)
                    {
                        paramsList.Add("BuyCheck", true);
                    }
                    break;
            }

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var query = new BLLAssmblly.Flow.QuotedPrice().GetCustomerQuotedByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, paramsList);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataBind(query);
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


    }
}