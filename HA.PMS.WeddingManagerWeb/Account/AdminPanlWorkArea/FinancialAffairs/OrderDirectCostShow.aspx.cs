using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class OrderDirectCostShow : SystemPage
    {
        int DispatchingID = 0;
        int EmployeeID = 0;
        int CustomerID = 0;
        int OrderID = 0;

        CostSum ObjCostSumBLL = new CostSum();
        /// <summary>
        /// 报价单审核
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Designclass ObjDesignClassBLL = new Designclass();

        Employee ObjEmployeeBLL = new Employee();
        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();
        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();


        ///订单成本明细
        OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();
        Cost ObjCostBLL = new Cost();
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            if (!IsPostBack)
            {
                var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);

                var QuotedModel = ObjQuotedPriceBLL.GetOnlyFirstByOrderID(OrderID);
                if (QuotedModel != null)
                {
                    hideTotal.Value = QuotedModel.FinishAmount.ToString();
                    lblTotalAmount.Text = QuotedModel.FinishAmount.ToString();
                    if (hideTotal.Value == "")
                    {
                        hideTotal.Value = "1";

                    }
                }
                BinderData();

                var ObjUpdateModel = ObjCostBLL.GetByOrderID(OrderID);

                //txtProfitMargin.Text = ObjUpdateModel.ProfitMargin + string.Empty;

            }
        }

        private void BinderData()
        {
            int CustomerID = Request["CustomerID"].ToInt32();

            var DataList = ObjCostSumBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());

            var ObjDataList = ObjOrderfinalCostBLL.GetByCustomerID(CustomerID);

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5));  //执行团队 4

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1));        //供应商 1

            rptStore.DataBind(DataList.Where(C => C.RowType == 2));        //库房 2

            //repSaleMoney.DataBind(DataList.Where(C => C.RowType == 6));  // 酒店佣金 顾问提成等 四项

            repBuyCost.DataBind(DataList.Where(C => C.RowType == 7));  //采购物料

            repFlowerCost.DataBind(DataList.Where(C => C.RowType == 8));  //花艺单

            repOther.DataBind(DataList.Where(C => C.RowType == 9));   //其他

            List<FL_Designclass> List = ObjDesignClassBLL.GetByCustomerId(Request["CustomerID"].ToInt32());
            lblCost.Text = (DataList.Sum(C => C.ActualSumTotal).ToString().ToDecimal() + List.Sum(C => C.ActualSumTotal).ToString().ToDecimal()).ToString();
            lblProfit.Text = (lblTotalAmount.Text.ToDecimal() - lblCost.Text.ToDecimal()).ToString();
            if (lblTotalAmount.Text.ToDecimal() > 0)
            {
                lblProfitMargin.Text = (lblProfit.Text.ToDecimal() / lblCost.Text.ToDecimal()).ToString("0.00%").ToString();
            }

        }
    }
}