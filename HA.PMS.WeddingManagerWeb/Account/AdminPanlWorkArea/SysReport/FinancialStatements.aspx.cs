using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport
{
    public partial class FinancialStatements : SystemPage
    {
        Report ObjReportBLL = new Report();
        int EmployeeId = 0;
        int Year = DateTime.Now.Year;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binder();
            }
        }

        private void Binder()
        {
            EmployeeId = MyManager.SelectedValue.ToInt32();
            Year = ys_year.Text.ToInt32();
        }

        /// <summary>
        /// 绑定现金流
        /// </summary>
        /// <param name="Month"></param>
        public string BinderReturnMoney(int Month)
        {
            return ObjReportBLL.GetCollectionsPlanByMonth(Year, Month).ToString();
        }


        /// <summary>
        /// 绑定当月客户量
        /// </summary>
        /// <param name="Month"></param>
        public string GetNewCustomerByMonth(int Month)
        {
            return ObjReportBLL.GetNewCustomerByMonth(Year, Month, EmployeeId).ToString();
        }


        /// <summary>
        /// 绑定当月客户量
        /// </summary>
        /// <param name="Month"></param>
        public string GetSucessCustomerByMonth(int Month)
        {
            return ObjReportBLL.GetSucessCustomerByMonth(Year, Month, EmployeeId).ToString();
        }

        /// <summary>
        /// 成交率
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetTurnoverRateByMonth(int Month)
        {
            return ObjReportBLL.GetTurnoverRateByMonth(Year, Month, EmployeeId).ToString();
        }

        /// <summary>
        /// 完工额
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetCustomFinishSumMoneyByMonth(int Month)
        {
            return ObjReportBLL.GetCustomFinishSumMoneyByMonth(Year, Month, EmployeeId).ToString();
        }


        /// <summary>
        /// 绑定当月签单客户量
        /// </summary>
        /// <param name="Month"></param>
        public int GetSucessCustomerCountByYearMonth(int month)
        {
            return ObjReportBLL.GetSucessCustomerCountByYearMonth(Year, month, EmployeeId);
        }

        /// <summary>
        /// 获取当月平均消费
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GeAvgtQuotedMoneyByMonth(int month)
        {
            return Convert.ToDecimal(ObjReportBLL.GeAvgtQuotedMoneyByMonth(Year, month, EmployeeId)).ToString("f2");
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            Binder();
        }

    }
}