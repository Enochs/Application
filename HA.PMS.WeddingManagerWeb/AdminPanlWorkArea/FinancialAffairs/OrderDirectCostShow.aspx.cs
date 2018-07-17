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
/*
 * 
 时间 ： 2015/11/2
 姓名 ： 吴鹏
 * 
 * */

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
        /// 调度
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();


        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BinderData()
        {
            //所有产品成本
            var DataList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);

            //策划表
            var QuotedModel = ObjQuotedPriceBLL.GetOnlyFirstByOrderID(OrderID);


            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7));  //内部人员 5  四大金刚 4  手动添加  7

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 8 || C.RowType == 10).OrderBy(C => C.CostSumId));        //物料 1      库房 2      新购买 3    系统默认添加的设计师 工程主管 6   手动添加 8  设计单 10

            repOtherCost.DataBind(DataList.Where(C => C.RowType == 9 || C.RowType == 11));              //其他   9.手动添加

            repSaleCost.DataBind(DataList.Where(C => C.RowType == 12));                                 //销售成本


            //订单总金额
            lblTotalAmount.Text = QuotedModel.FinishAmount.ToString();

            //订单成本
            lblCost.Text = (DataList.Sum(C => C.ActualSumTotal).ToString().ToDecimal()).ToString();

            //利润
            lblProfit.Text = (lblTotalAmount.Text.ToDecimal() - lblCost.Text.ToDecimal()).ToString();

            //利润率
            if (lblTotalAmount.Text.ToDecimal() > 0)
            {
                lblProfitMargin.Text = (lblProfit.Text.ToDecimal() / lblCost.Text.ToDecimal()).ToString("0.00%").ToString();
            }
        }
        #endregion
    }
}