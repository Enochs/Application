using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using System.Text;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium
{
    public partial class DynamicIndex : SystemPage
    {
        Order objOrderBLL = new Order();

        Report ObjReportBLL = new Report();
        /// <summary>
        /// 派工操作
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();

        /// <summary>
        /// 满意度
        /// </summary>
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();

        Report ObjRportBLL = new Report();

        CostSum ObjCostSumBLL = new CostSum();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        int Year = DateTime.Now.Year;
        int EmployeeId = 0;

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlChooseYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
                DataKChartBinder(sender, e);
            }
        }
        #endregion

        #region 绘制K线图
        /// <summary>
        /// 绘制K线图
        /// </summary>
        protected void DataKChartBinder(object sender, EventArgs e)
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();

            //订单金额
            StringBuilder sbFinishAmount = new StringBuilder();
            //现金流
            StringBuilder sbRealityAmount = new StringBuilder();
            //订单执行总数
            StringBuilder sbOrderCount = new StringBuilder();
            //新订单
            StringBuilder sbNewOrderCount = new StringBuilder();


            #region 统计图表
            for (int i = 1; i <= 12; i++)
            {

                //执行订单金额 == 完工额
                sbFinishAmount.AppendFormat("{0},", GetCustomFinishSumMoneyByMonth(i));

                //现金流
                sbRealityAmount.AppendFormat("{0},", BinderReturnMoney(i));

                //执行订单总数 == 完工量
                sbOrderCount.AppendFormat("{0},", GetSucessCustomerCountByYearMonth(i));

                //新订单 == 签单量
                sbNewOrderCount.AppendFormat("{0},", GetSucessCustomerByMonth(i));

            }

            //订单总数
            ViewState["sbOrderCount"] = GetSubString(sbOrderCount.ToString());
            //新订单
            ViewState["sbNewOrderCount"] = GetSubString(sbNewOrderCount.ToString());
            //订单金额
            ViewState["sbFinishAmount"] = GetSubString(sbFinishAmount.ToString());
            //现金流
            ViewState["sbRealityAmount"] = GetSubString(sbRealityAmount.ToString());

            #endregion

        }
        #endregion


        #region table 表格绑定
        /// <summary>
        /// 绑定现金流
        /// </summary>
        /// <param name="Month"></param>
        public string BinderReturnMoney(int Month)
        {
            return ObjReportBLL.GetCollectionsPlanByMonth(ddlChooseYear.SelectedItem.Text.ToInt32(), Month).ToString("0");
        }


        /// <summary>
        /// 绑定当月客户入客量(到店)
        /// </summary>
        /// <param name="Month"></param>
        public string GetNewCustomerByMonth(int Month)
        {
            return ObjReportBLL.GetNewCustomerByMonth(ddlChooseYear.SelectedItem.Text.ToInt32(), Month, EmployeeId).ToString();
        }


        /// <summary>
        /// 绑定当月客户签单量(第一次收款时间在当月)
        /// </summary>
        /// <param name="Month"></param>
        public string GetSucessCustomerByMonth(int Month)
        {
            return ObjReportBLL.GetSucessCustomerByMonth(ddlChooseYear.SelectedItem.Text.ToInt32(), Month, EmployeeId).ToString();
        }

        /// <summary>
        /// 成交率
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetTurnoverRateByMonth(int Month)
        {
            if (GetNewCustomerByMonth(Month).ToDecimal() > 0)
            {
                return Convert.ToDecimal(GetSucessCustomerByMonth(Month).ToDecimal() / GetNewCustomerByMonth(Month).ToDecimal()).ToString("0.00%");
            }
            else
            {
                return "0.00%";
            }
        }

        /// <summary>
        /// 完工额/执行额
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetCustomFinishSumMoneyByMonth(int Month)
        {
            return ObjReportBLL.GetCustomFinishSumMoneyByMonth(ddlChooseYear.SelectedItem.Text.ToInt32(), Month, EmployeeId).ToString("0");
        }


        /// <summary>
        /// 绑定当月完工量/执行量
        /// </summary>
        /// <param name="Month"></param>
        public int GetSucessCustomerCountByYearMonth(int month)
        {
            return ObjReportBLL.GetSucessCustomerCountByYearMonth(ddlChooseYear.SelectedItem.Text.ToInt32(), month, EmployeeId);
        }

        /// <summary>
        /// 获取当月平均消费
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GeAvgtQuotedMoneyByMonth(int month)
        {
            //return Convert.ToDecimal(ObjReportBLL.GeAvgtQuotedMoneyByMonth(Year, month, EmployeeId)).ToString("0");
            if (GetSucessCustomerCountByYearMonth(month).ToString().ToDecimal() > 0)
            {
                return Convert.ToDecimal(GetCustomFinishSumMoneyByMonth(month).ToDecimal() / GetSucessCustomerCountByYearMonth(month)).ToString("0");
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 获取成本
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetCostMoneyByMonth(int Month)
        {
            return ObjReportBLL.GetCostByEmployee(EmployeeId, ddlChooseYear.SelectedItem.Text.ToInt32(), Month).ToString();
        }

        /// <summary>
        /// 获取毛利
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetProfitByMonth(int Month)
        {
            return (GetCustomFinishSumMoneyByMonth(Month).ToDecimal() - GetCostMoneyByMonth(Month).ToDecimal()).ToString("0");
        }
        /// <summary>
        /// 获取毛利率
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetProfitRateByMonth(int Month)
        {
            if (GetCustomFinishSumMoneyByMonth(Month).ToDecimal() > 0)
            {
                return ((GetCustomFinishSumMoneyByMonth(Month).ToDecimal() - GetCostMoneyByMonth(Month).ToDecimal()).ToString().ToDecimal() / GetCustomFinishSumMoneyByMonth(Month).ToDecimal()).ToString("0.00%");
            }
            else
            {
                return "0.00%";
            }
        }


        #endregion
    }
}