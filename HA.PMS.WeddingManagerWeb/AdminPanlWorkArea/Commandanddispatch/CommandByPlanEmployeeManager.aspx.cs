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
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.BLLAssmblly.CS;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch
{
    public partial class CommandByPlanEmployeeManager : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
   
      
        Dispatching objDispatchingBLL = new Dispatching();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        Employee objEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTableHead();
                GetTdStr();
                LoadTableBody();
            }
        }
        /// <summary>
        /// 绑定主体数据
        /// </summary>
        protected void LoadTableBody()
        {
            ViewState["currentOrderMoneyTarget"] = GetTargetDataByTargetName("当期执行订单总额");
            ViewState["rateTarget"] = GetTargetDataByTargetName("毛利率");
            ViewState["newOrderMoneyTarget"] = GetTargetDataByTargetName("当期新增订单总额");
            GetFlinish();
   
        }
        /// <summary>
        /// 加载空白表格单元格
        /// </summary>
        /// <returns></returns>
        protected void GetTdStr()
        {
            int employeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();
            StringBuilder sbTds = new StringBuilder();
            for (int i = 0; i < employeeCount + 3; i++)
            {
                sbTds.Append("<td></td>");
            }
            ViewState["sbTds"] = sbTds.ToString();

        }
        /// <summary>
        /// 根据目标名称返回对应的 时间和部门的 目标集合
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        protected string GetTargetDataByTargetName(string targetName)
        {
            string[] chooseDateSt = GetParameterDateTime();
            List<ObjectParameter> objListParameter = new List<ObjectParameter>();
            objListParameter.Add(new ObjectParameter("Goal", targetName));

            objListParameter.Add(new ObjectParameter("CreateTime_between", chooseDateSt[0].ToDateTime() + "," + chooseDateSt[1].ToDateTime()));


            //按照 targetName 返回对应所有的计划目标
            var currentYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());

            StringBuilder sb = new StringBuilder();
            //当前部门员工的个数
            int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


            for (int i = 0; i < currentDepartEmployeeCount; i++)
            {

                HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
                int currentEmployeeId = hfEmployee.Value.ToInt32();
                //查询当前员工的目标值
                var singerQuery = currentYearQuery.Where(C => C.CreateEmployeeId.Value == currentEmployeeId).ToList();
                decimal tagetValue = 0;
                if (singerQuery != null)
                {
                    tagetValue = singerQuery.Sum(C => C.TargetValue.Value);
                }
                sb.AppendFormat("<td>{0}</td>", tagetValue);
            }



            //当年合计
            sb.AppendFormat("<td>{0}</td>", currentYearQuery.Sum(C => C.TargetValue.Value));

            //上年合计
            objListParameter[1].Value = chooseDateSt[2].ToDateTime() + ","
                + chooseDateSt[3].ToDateTime();
            var preYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
            if (preYearQuery != null)
            {
                sb.AppendFormat("<td>{0}</td>", preYearQuery.Sum(C => C.TargetValue.Value));
            }
            else
            {
                sb.AppendFormat("<td>{0}</td>", 0);
            }
            //历史累计
            //移除时间参数，查询对应的部门的所有目标
            objListParameter.RemoveAt(1);
            var queryAll = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
            if (queryAll != null)
            {
                sb.AppendFormat("<td>{0}</td>", queryAll.Sum(C => C.TargetValue.Value));
            }
            else
            {
                sb.AppendFormat("<td>{0}</td>", 0);
            }
            return sb.ToString();

        }
        /// <summary>
        /// 绑定头部数据
        /// </summary>
        protected void DataTableHead()
        {
            Sys_Department depart = objDepartmentBLL.GetByAll().Where(C => C.DepartmentName == "策划部").FirstOrDefault();

            List<Sys_Employee> empList = new List<Sys_Employee>();
            if (depart != null)
            {
                ViewState["depart"] = depart.DepartmentID;
                empList = objEmployeeBLL.GetByALLDepartmetnID(depart.DepartmentID);
                ViewState["employeeCount"] = empList.Count;
                rptEmployee.DataSource = empList;
                rptEmployee.DataBind();
            }

        }

        protected void GetFlinish()
        {
            string[] chooseDateSt = GetParameterDateTime();

            List<ObjectParameter> ObjParameter = new List<ObjectParameter>();
            ObjParameter.Add(new ObjectParameter("CreateDate_between", chooseDateSt[0].ToDateTime() + "," + chooseDateSt[1].ToDateTime()));
            #region 执行订单数
            var dispatchAll = objDispatchingBLL.GetByAll().Where(C => C.Isover == false);
            //当前时间段的信息
            var currentDispatch = dispatchAll.Where(C => C.CreateDate >= chooseDateSt[0].ToDateTime() && C.CreateDate <= chooseDateSt[1].ToDateTime()).ToList();
            //获取对应的员工ID
            var currentEmployee = currentDispatch.Select(C => C.EmployeeID.Value).ToList();
            var resultEmployeeId = new List<int>();
           


            //获取根据查询条件之后的执行中的订单
            var resultList = new List<FL_Dispatching>();
            foreach (var item in currentDispatch)
            {
                var current = resultEmployeeId.Where(C => C == item.EmployeeID.Value).FirstOrDefault();
                if (current != 0)
                {
                    resultList.Add(item);
                }
            }
            StringBuilder sbDispatching = new StringBuilder();
            #endregion


            #region 当期新增订单金额
            var quotedPriceAll = objQuotedPriceBLL.GetCustomerQuotedParameter(new List<ObjectParameter>());
            var currentQuotedPrice = quotedPriceAll.Where(C => C.PartyDate >= chooseDateSt[0].ToDateTime() && C.PartyDate <= chooseDateSt[1].ToDateTime()).ToList();
            StringBuilder sbQuoted = new StringBuilder();
            #endregion


            //当前部门员工的个数
            int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


            for (int i = 0; i < currentDepartEmployeeCount; i++)
            {

                HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
                int currentEmployeeId = hfEmployee.Value.ToInt32();

                //执行订单数
                var singerDispatch = resultList.Where(C => C.EmployeeID.Value == currentEmployeeId);
                int monthDispatchCount = singerDispatch.Count();
                if (singerDispatch.Count() > 0)
                {
                    sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(singerDispatch.ToList()));
                }
                else
                {
                    sbDispatching.AppendFormat("<td>{0}</td>", 0);
                }
                //新增订单金额
                var singerOrder = currentQuotedPrice.Where(C => C.EmpLoyeeID.Value == currentEmployeeId);
                if (singerOrder != null)
                {
                    sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(singerOrder.ToList()));
                }
                else
                {
                    sbQuoted.AppendFormat("<td>{0}</td>", 0);
                }
            }

            #region 执行订单数年份统计
            //当年统计数
            if (resultList != null)
            {
                sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(resultList.ToList()));
            }
            else
            {
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //上年统计数 chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preYearDispatch = currentDispatch.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
                && C.CreateDate <= chooseDateSt[3].ToDateTime()).ToList();
            if (preYearDispatch != null)
            {
                sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(preYearDispatch.ToList()));
            }
            else
            {
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计数
            sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(dispatchAll.ToList()));
            ViewState["sbDispatching"] = sbDispatching.ToString();

            #endregion

            #region 新增订单年份统计
            //当年统计数
            if (currentQuotedPrice != null)
            {
                sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(currentQuotedPrice.ToList()));
            }
            else
            {
                sbQuoted.AppendFormat("<td>{0}</td>", 0);
            }
            //上年统计数 chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preQuoted = quotedPriceAll.Where(C => C.PartyDate >= chooseDateSt[2].ToDateTime()
                && C.PartyDate <= chooseDateSt[3].ToDateTime()).ToList();
            if (preQuoted != null)
            {
                sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(preQuoted.ToList()));
            }
            else
            {
                sbQuoted.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计数
            sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(quotedPriceAll.ToList()));
            ViewState["sbQuoted"] = sbQuoted.ToString();
            #endregion
        }
        /// <summary>
        /// 算出新增订单总金额 
        /// </summary>-
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumNewOrderMoneyByCustomerId(List<View_CustomerQuoted> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }

        /// <summary>
        /// 算出订单总金额 
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumOrderMoneyByCustomerId(List<FL_Dispatching> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }
        /// <summary>
        /// 根据当前下拉框选择的时间生成对应的相关参数
        /// </summary>
        /// <returns></returns>
        protected string[] GetParameterDateTime()
        {
            string[] chooseDateStr = new string[4];
            if (ddlChooseYear.Items.Count == 0)
            {
                chooseDateStr[0] = (DateTime.Now.Year + "-1-1");

                chooseDateStr[1] = DateTime.Now.Year + "-12-31";
            }
            else
            {
                for (int i = 0; i < ddlChooseYear.SelectedValue.Split(',').Count(); i++)
                {
                    chooseDateStr[i] = ddlChooseYear.SelectedValue.Split(',')[i];
                }

            }
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            if (ddlChooseMonth.SelectedValue != "0")
            {
                //加入月份时间
                chooseDateStar = chooseDateStar.AddMonths(ddlChooseMonth.SelectedValue.ToInt32() - 1);

                int year = chooseDateStar.Year;
                int month = chooseDateStar.Month;

                chooseDateEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                chooseDateStr[0] = chooseDateStar + string.Empty;
                chooseDateStr[1] = chooseDateEnd + string.Empty;
            }
            //上年
            chooseDateStr[2] = chooseDateStar.Year - 1 + "-1-1";
            chooseDateStr[3] = chooseDateStar.Year - 1 + "-12-31";
            return chooseDateStr;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            LoadTableBody();
        }
    }
}