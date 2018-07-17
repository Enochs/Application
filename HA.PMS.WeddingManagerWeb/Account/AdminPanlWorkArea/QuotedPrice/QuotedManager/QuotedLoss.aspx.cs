using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Emnus;
using System.Web.UI.WebControls;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedLoss : SystemPage
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

        /// <summary>
        /// 收款
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
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

            if (OrderID != null)
            {
                var Model = ObjOrderBLL.GetByID(OrderID.ToString().ToInt32());
                if (Model != null)
                {
                    return Model.EarnestMoney.ToString();
                }
            }
            return "0";
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

            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);

            //按新人姓名查询
            CstmNameSelector.AppandTo(objParmList);

            //按联系电话查询
            objParmList.Add(txtContactPhone.Text != string.Empty, "ContactPhone", txtContactPhone.Text, NSqlTypes.StringEquals);

            //按责任人查询
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(objParmList);
            }
            //按婚期查询
            objParmList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            //按签单日期查询
            objParmList.Add(CreateDateRanger.IsNotBothEmpty, "CreateDate", CreateDateRanger.StartoEnd, NSqlTypes.DateBetween);

            objParmList.Add("Expr1", false, NSqlTypes.Equal);
            //按状态查询
            objParmList.Add("State", 0, NSqlTypes.Greaterthan);

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
            BinderData();
        }

        protected void repCustomer_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int CustomerID = (e.Item.FindControl("hideCustomerHide") as HiddenField).Value.ToInt32();
            var CustomerModel = ObjCustomerBLL.GetByID(CustomerID);     //保留客户流失之前的状态
            CustomerModel.LoseBeforeState = CustomerModel.State;
            ObjCustomerBLL.Update(CustomerModel);

            int QuotedID = e.CommandArgument.ToString().ToInt32();
            var ObjUpdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            var ObjCustomerModel = ObjCustomerBLL.GetByID(ObjUpdateModel.CustomerID);
            ObjCustomerModel.State = (int)CustomerStates.Lose;
            ObjUpdateModel.IsDelete = true;

            ObjCustomerBLL.Update(ObjCustomerModel);
            ObjQuotedPriceBLL.Update(ObjUpdateModel);
            BinderData();
            JavaScriptTools.AlertWindow("操作成功!", Page);
        }


        #region 获取该订单的退款金额
        /// <summary>
        /// 获取退款金额
        /// </summary>
        public string GetBackMoney(object Source)        //获取退款金额
        {
            int OrderID = Source.ToString().ToInt32();
            var DataList = ObjQuotedCollectionsPlanBLL.GetByNodeOrder(OrderID, "退款");
            Decimal BackMoney = DataList.Sum(C => C.RealityAmount).ToString().ToDecimal();
            return BackMoney.ToString().Replace("-", "");
        }
        #endregion

    }
}