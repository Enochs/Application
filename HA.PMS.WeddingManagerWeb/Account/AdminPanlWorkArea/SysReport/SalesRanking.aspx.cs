using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using System.IO;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport
{
    public partial class SalesRanking : SystemPage
    {

        Employee ObjEmployeeBLL = new Employee();
        Report ObjReportBLL = new Report();
        CostSum ObjCostSumBLL = new CostSum();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        DateTime Star = DateTime.Now.AddDays(-DateTime.Now.DayOfYear);
        DateTime End = DateTime.Now.AddDays(1);

        #region 销售排行榜 实体类

        /// <summary>
        /// 销售排名报表
        /// </summary>
        public class EmployeeReport
        {

            public int EmployeeID { get; set; }
            //回款 现金流
            public decimal SumReturnMoney
            { get; set; }


            public decimal SumQuotedMoney
            { get; set; }
            //员工姓名
            public string EmployeeName
            { get; set; }

            //客源量
            public int SourceCount
            { get; set; }

            //入客量
            public int InSourceCount
            { get; set; }

            //转换率
            public string InSourceRate
            { get; set; }



            public decimal InSourceRateCount
            { get; set; }


            //到店客户量
            public int ComeOrderCount { get; set; }

            /// <summary>
            /// 平均消费金额
            /// </summary>
            public decimal AvgQuotedMoney
            {
                get;
                set;
            }
            //跟单客户量
            public int CustomerCount
            {

                get;
                set;
            }


            //本月新签订单
            public int NewOrderByMonth
            {
                get;
                set;
            }

            //签单率
            public string TurnoveRate
            {
                get;
                set;
            }

            //完工额
            public decimal FinisMoneySum
            {
                get;
                set;
            }

            //完工额
            public int FinishCount
            {
                get;
                set;
            }

            //成本
            public decimal Cost
            {
                get;
                set;
            }

            //毛利率
            public string Gross { get; set; }

            //毛利润
            public decimal MineMoney { get; set; }

            //转换率基数 排行作用
            public decimal TurnoveRateCount { get; set; }
            //毛利率基数   排行 作用
            public decimal GrossCount { get; set; }

        }
        #endregion


        List<EmployeeReport> ReportList = new List<EmployeeReport>();

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary> 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //按婚期查询
                //if (DateRanger.IsNotBothEmpty == false)
                //{
                //    DateRanger.StartText = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString().ToString();
                //    DateRanger.EndText = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString().ToString();
                //}
                ddlDepartment.BinderDepartment();
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            GetAllReport();
            this.repdataList.DataSource = ReportList.OrderByDescending(C => C.SumReturnMoney);
            this.repdataList.DataBind();
        }
        #endregion

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        #region 导出
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btn_Export_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SaleRanking.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();

            GetAllReport();
            int index = 0;
            foreach (var ObjDataItem in ReportList)
            {
                index++;
                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + index + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.EmployeeName + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SumReturnMoney + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ComeOrderCount + "</Data></Cell>\r\n"); //
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.NewOrderByMonth + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.TurnoveRate + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.FinisMoneySum + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.FinishCount + "</Data></Cell>\r\n");//
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.AvgQuotedMoney + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Cost + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.MineMoney + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Gross + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + "" + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");
            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }
        #endregion

        #region 数据绑定到集合中 GetAllReport
        public void GetAllReport()
        {
            List<PMSParameters> ObjParmList = new List<PMSParameters>();

            if (DateRanger.IsNotBothEmpty)
            {
                string[] times = DateRanger.StartoEnd.Split(',');
                Star = times[0].ToDateTime().ToString("yyyy-MM-dd 00:00:00.000").ToDateTime();
                End = times[1].ToDateTime().ToString("yyyy-MM-dd 23:59:59.999").ToDateTime();
            }
            else
            {
                Star = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString().ToDateTime();
                End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString().ToDateTime();
            }
            if (ddlDepartment.SelectedValue.ToInt32() > 0)
            {
                List<Sys_Employee> EmployeeList = ObjEmployeeBLL.GetByDepartmetnID(ddlDepartment.SelectedValue.ToInt32());
                GetReports(EmployeeList, ObjParmList);
            }
            else
            {
                List<Sys_Employee> EmployeeList = ObjEmployeeBLL.GetByAll();
                GetReports(EmployeeList, ObjParmList);
            }
        }
        #endregion

        #region 点击排序
        /// <summary>
        /// 排序
        /// </summary>
        protected void lbtnSumReturnMoney_Click(object sender, EventArgs e)
        {
            GetAllReport();
            LinkButton lbtnSource = (sender as LinkButton);
            switch (lbtnSource.CommandName)
            {
                case "SumReturnMoney":
                    ReportList = ReportList.OrderByDescending(C => C.SumReturnMoney).ToList();
                    break;
                case "ComeOrderCount":
                    ReportList = ReportList.OrderByDescending(C => C.ComeOrderCount).ToList();
                    break;
                case "SourceCount":
                    ReportList = ReportList.OrderByDescending(C => C.SourceCount).ToList();
                    break;
                case "InSourceCount":
                    ReportList = ReportList.OrderByDescending(C => C.InSourceCount).ToList();
                    break;
                case "InSourceRate":
                    ReportList = ReportList.OrderByDescending(C => C.InSourceRateCount).ToList();
                    break;
                case "NewOrderByMonth":
                    ReportList = ReportList.OrderByDescending(C => C.NewOrderByMonth).ToList();
                    break;
                case "TurnoveRate":
                    ReportList = ReportList.OrderByDescending(C => C.TurnoveRateCount).ToList();
                    break;
                case "FinisMoneySum":
                    ReportList = ReportList.OrderByDescending(C => C.FinisMoneySum).ToList();
                    break;
                case "FinishCount":
                    ReportList = ReportList.OrderByDescending(C => C.FinishCount).ToList();
                    break;
                case "AvgQuotedMoney":
                    ReportList = ReportList.OrderByDescending(C => C.AvgQuotedMoney).ToList();
                    break;
                case "Cost":
                    ReportList = ReportList.OrderByDescending(C => C.Cost).ToList();
                    break;
                case "MineMoney":
                    ReportList = ReportList.OrderByDescending(C => C.MineMoney).ToList();
                    break;
                case "Gross":
                    ReportList = ReportList.OrderByDescending(C => C.GrossCount).ToList();
                    break;
            }
            repdataList.DataBind(ReportList);
        }
        #endregion

        #region 合计
        public void GetTotalSum(List<EmployeeReport> ReportList)
        {
            lblSumReturnMoney.Text = ReportList.Sum(C => C.SumReturnMoney).ToString();           //现金流
            lblComeOrderCount.Text = ReportList.Sum(C => C.ComeOrderCount).ToString();          //新客户
            lblNewOrderByMonth.Text = ReportList.Sum(C => C.NewOrderByMonth).ToString();        //新订单
            if (ReportList.Sum(C => C.ComeOrderCount).ToString().ToDecimal() > 0)               //成交率
            {
                lblTurnoveRate.Text = (ReportList.Sum(C => C.NewOrderByMonth).ToString().ToDecimal() / ReportList.Sum(C => C.ComeOrderCount).ToString().ToDecimal()).ToString("0.00%");
            }

            lblSourceCount.Text = ReportList.Sum(C => C.SourceCount).ToString();            //客源量
            lblInSourceCount.Text = ReportList.Sum(C => C.InSourceCount).ToString();        //入客量
            if (ReportList.Sum(C => C.SourceCount).ToString().ToDecimal() > 0)              //转换率
            {
                lblInSourceRate.Text = (ReportList.Sum(C => C.InSourceCount).ToString().ToDecimal() / ReportList.Sum(C => C.SourceCount).ToString().ToDecimal()).ToString("0.00%");
            }

            lblFinisMoneySum.Text = ReportList.Sum(C => C.FinisMoneySum).ToString();        //执行额
            lblFinishCount.Text = ReportList.Sum(C => C.FinishCount).ToString();            //执行量
            if (ReportList.Sum(C => C.FinishCount).ToString().ToDecimal() > 0)              //平均消费
            {
                lblAvgQuotedMoney.Text = (ReportList.Sum(C => C.FinisMoneySum).ToString().ToDecimal() / ReportList.Sum(C => C.FinishCount).ToString().ToDecimal()).ToString("f2");
            }
            lblCostSum.Text = ReportList.Sum(C => C.Cost).ToString();                   //成本
            lblMineMoney.Text = ReportList.Sum(C => C.MineMoney).ToString();            //毛利
            if (ReportList.Sum(C => C.FinisMoneySum).ToString().ToDecimal() > 0)        //毛利率 
            {
                lblGross.Text = (ReportList.Sum(C => C.MineMoney).ToString().ToDecimal() / ReportList.Sum(C => C.FinisMoneySum).ToString().ToDecimal()).ToString("0.00%");
            }
        }
        #endregion

        #region 获取统计
        public void GetReports(List<Sys_Employee> EmployeeList, List<PMSParameters> ObjParmList)
        {
            foreach (var ObjEmployee in EmployeeList)
            {
                EmployeeReport ObjReport = new EmployeeReport();
                ObjParmList.Add("CreateDate", Star + "," + End, NSqlTypes.DateBetween);
                if (ObjEmployee.EmployeeID != 1)
                {
                    ObjReport.EmployeeID = ObjEmployee.EmployeeID;
                    ObjReport.EmployeeName = ObjEmployee.EmployeeName;      //姓名
                    if (ObjEmployee.EmployeeName == "all")
                    {
                    }
                    ObjReport.SumReturnMoney = ObjReportBLL.GetCollectionsPlanByEmployee(Star, End, ObjEmployee.EmployeeID);    //现金流
                    ObjReport.SourceCount = ObjReportBLL.GetCustomerSumByEmployee(ObjEmployee.EmployeeID, Star, End);       //客源量
                    ObjReport.InSourceCount = ObjReportBLL.GetCustomerComeSumByEmployee(ObjEmployee.EmployeeID, Star, End);       //入客量(转化量)
                    ObjReport.ComeOrderCount = ObjReportBLL.GetComeOrderCountByDate(Star, End, ObjEmployee.EmployeeID);     //新客户(到店量)
                    if (ObjReport.SourceCount.ToString().ToDecimal() > 0)
                    {
                        ObjReport.InSourceRateCount = (ObjReport.InSourceCount.ToString().ToDecimal() / ObjReport.SourceCount.ToString().ToDecimal()).ToString("0.00").ToDecimal();         //成交率基数
                        ObjReport.InSourceRate = (ObjReport.InSourceCount.ToString().ToDecimal() / ObjReport.SourceCount.ToString().ToDecimal()).ToString("0.00%");                         //转换率
                    }
                    else
                    {
                        ObjReport.InSourceRate = "0.00%";
                    }

                    ObjReport.NewOrderByMonth = ObjEmployeeBLL.GetQuotedEmployeeCountByDate(Star, End, ObjEmployee.EmployeeID);     //新订单

                    if (ObjReport.ComeOrderCount.ToString().ToDecimal() > 0)
                    {
                        ObjReport.TurnoveRateCount = (ObjReport.NewOrderByMonth.ToString().ToDecimal() / ObjReport.ComeOrderCount.ToString().ToDecimal()).ToString("0.00").ToDecimal();  //成交率基数
                        ObjReport.TurnoveRate = (ObjReport.NewOrderByMonth.ToString().ToDecimal() / ObjReport.ComeOrderCount.ToString().ToDecimal()).ToString("0.00%");  //成交率
                    }
                    else
                    {
                        ObjReport.TurnoveRate = "0.00%";
                    }

                    ObjReport.FinisMoneySum = ObjQuotedPriceBLL.GetFinishAmountByDate(Star, End, ObjEmployee.EmployeeID).ToDecimal();   //执行额
                    ObjReport.FinishCount = ObjQuotedPriceBLL.GetFinishCountByDate(Star, End, ObjEmployee.EmployeeID);       //执行量
                    if (ObjReport.FinishCount > 0)
                    {
                        ObjReport.AvgQuotedMoney = (ObjReport.FinisMoneySum.ToString().ToDecimal() / ObjReport.FinishCount.ToString().ToDecimal()).ToString("0.00").ToDecimal();       //平均消费
                    }
                    else
                    {
                        ObjReport.AvgQuotedMoney = "0.00".ToDecimal();
                    }
                    ObjReport.Cost = ObjCostSumBLL.GetCostSumByDate(Star, End, ObjEmployee.EmployeeID).ToDecimal();     //总成本

                    ObjReport.MineMoney = ObjReport.FinisMoneySum - ObjReport.Cost;     //毛利
                    if (ObjReport.FinisMoneySum.ToString().ToDecimal() > 0)
                    {
                        ObjReport.GrossCount = (ObjReport.MineMoney.ToString().ToDecimal() / ObjReport.FinisMoneySum.ToString().ToDecimal()).ToString("0.00").ToDecimal();  //毛利率基数
                        ObjReport.Gross = (ObjReport.MineMoney.ToString().ToDecimal() / ObjReport.FinisMoneySum.ToString().ToDecimal()).ToString("0.00%");     //毛利率
                    }
                    else
                    {
                        ObjReport.Gross = "0.00%";
                    }

                    ReportList.Add(ObjReport);
                    GetTotalSum(ReportList);

                    #region 注释
                    //List<Sys_Employee> EmployeeList = new List<Sys_Employee>();
                    //List<Sys_Employee> EmployeeLists = new List<Sys_Employee>();


                    //List<PMSParameters> ObjParList = new List<PMSParameters>();
                    //List<PMSParameters> ObjParmList = new List<PMSParameters>();        //QuotedEmpoyee  签单人
                    //List<PMSParameters> ObjParmLists = new List<PMSParameters>();       //OrderEmployee 策划人
                    //if (DateRanger.IsNotBothEmpty)
                    //{
                    //    string[] times = DateRanger.StartoEnd.Split(',');
                    //    Star = times[0].ToDateTime().ToShortDateString().ToDateTime();
                    //    End = times[1].ToDateTime().ToShortDateString().ToDateTime();
                    //}

                    //ObjParList.Add("CreateDate", Star.ToShortDateString() + "," + End.ToShortDateString(), NSqlTypes.DateBetween);
                    //ObjParmLists.Add("CreateDate", Star.ToShortDateString() + "," + End.ToShortDateString(), NSqlTypes.DateBetween);


                    //List<Sys_Employee> EmployeeList = ObjEmployeeBLL.GetByAll();


                    //ObjParList.Add(DateRanger.IsNotBothEmpty, "Partydate", Star + "," + End, NSqlTypes.DateBetween);
                    //ObjParmLists.Add(DateRanger.IsNotBothEmpty, "Partydate", Star + "," + End, NSqlTypes.DateBetween);

                    //var ReportResualtList = ObjReportBLL.GetReportByEmployee(1, ObjParList, "CreateDate");  //签单人集合
                    //var ReportResualtLists = ObjReportBLL.GetReportByEmployee(1, ObjParmLists, "CreateDate");   //策划人集合

                    //ObjReport.SumQuotedMoney = ReportResualtList.Where(C => C.QuotedEmployee == ObjEmployee.EmployeeID && C.QuotedMoney != null).ToList().Sum(C => C.QuotedMoney.Value);
                    //ObjReport.Cost = ObjReportBLL.GetCostByEmployee(ObjEmployee.EmployeeID, DateTime.Now.Year, 13);
                    //ObjReport.CustomerCount = ReportResualtLists.Where(C => C.OrderEmployee == ObjEmployee.EmployeeID).Count();   //入客量
                    //ObjReport.NewOrderByMonth = ReportResualtList.Where(C => C.QuotedEmployee == ObjEmployee.EmployeeID && C.QuotedDateSucessDate >= DateTime.Now.AddDays(-DateTime.Now.Day) && C.QuotedDateSucessDate <= DateTime.Now.AddDays(1)).Count();
                    //ObjReport.FinisMoneySum = ReportResualtList.Where(C => C.Partydate < DateTime.Now && C.QuotedEmployee == ObjEmployee.EmployeeID && C.QuotedMoney != null).ToList().Sum(C => C.QuotedMoney.Value);
                    #endregion
                }
            }
        }
        #endregion

    }
}