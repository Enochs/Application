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
    public partial class CommandByMoneyManager : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
         TargetType objTargetTypeBLL = new TargetType();
        Dispatching objDispatchingBLL = new Dispatching();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Employee objEmployeeBLL = new Employee();
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
        Complain objComplainBLL = new Complain();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!IsPostBack)
                {
                    CA_TargetType taget = objTargetTypeBLL.GetTargetTypeByTargetName("当期执行订单总额");
                    if (taget.DepartmentId.HasValue)
                    {
                        DataDropDownList(taget.DepartmentId.Value);

                    }
                    DataLoad();
                }
            }
        }
            
        protected void GetFlinish()
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();

            List<ObjectParameter> ObjParameter = new List<ObjectParameter>();
            ObjParameter.Add(new ObjectParameter("CreateDate_between", chooseDateStar + "," + chooseDateEnd));
            #region 执行订单数
            var dispatchAll = objDispatchingBLL.GetByAll().Where(C => C.Isover == false);
            //当前时间段的信息
            var currentDispatch = dispatchAll.Where(C => C.CreateDate >= chooseDateStar && C.CreateDate <= chooseDateEnd).ToList();
            //获取对应的员工ID
            var currentEmployee = currentDispatch.Select(C => C.EmployeeID.Value).ToList();


            //流水
            StringBuilder sbWater = new StringBuilder();
            //应收款
            StringBuilder sbRealAmount = new StringBuilder();

            var resultEmployeeId = new List<int>();
           
            
            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {
                    var emplistAll = objEmployeeBLL.GetByALLDepartmetnID(ddlDepartment.SelectedValue.ToInt32())
                        .Select(C => C.EmployeeID);
                    resultEmployeeId = (from m in emplistAll select m).
                        Intersect((from m in currentEmployee select m)).ToList();
                }
                else
                {  //如果没有选择部门进行查询就默认为只含年份的查询
                    resultEmployeeId = currentEmployee;
                }
            }
            

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
            var currentQuotedPrice = quotedPriceAll.Where(C => C.PartyDate >= chooseDateStar && C.PartyDate <= chooseDateEnd).ToList();
            StringBuilder sbQuoted = new StringBuilder();
            #endregion

           
            for (int i = 1; i <= 12; i++)
            {
                //执行订单数
                var singerDispatch = resultList.Where(C => C.CreateDate.Value.Month == i);
                int monthDispatchCount = singerDispatch.Count();
                if (singerDispatch.Count() > 0)
                {    sbWater.AppendFormat("<td>{0}</td>",GetSumEarnestMoneyByCustomerId(singerDispatch.ToList()));
                    sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(singerDispatch.ToList()));
                }
                else
                {
                    sbWater.AppendFormat("<td>{0}</td>",0);
                    sbDispatching.AppendFormat("<td>{0}</td>", 0);
                }

                //新增订单金额
                var singerOrder = currentQuotedPrice.Where(C => C.PartyDate.Value.Month == i);
                if (singerOrder != null)
                {
                    sbRealAmount.AppendFormat("<td>{0}</td>", GetRealAmountByQuotedID(singerOrder.ToList()));
                    sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(singerOrder.ToList()));
                }
                else
                {
                    sbRealAmount.AppendFormat("<td>{0}</td>", 0);
                    sbQuoted.AppendFormat("<td>{0}</td>", 0);
                }
            }
            #region 执行订单数年份统计
            //当年统计数
            if (resultList != null)
            {
                sbWater.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerId(resultList.ToList()));
                sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(resultList.ToList()));
            }
            else
            {
                sbWater.AppendFormat("<td>{0}</td>", 0);
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //上年统计数 chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preYearDispatch = currentDispatch.Where(C => C.CreateDate >= chooseDateStar.AddYears(-1)
                && C.CreateDate <= chooseDateEnd.AddYears(-1)).ToList();
            if (preYearDispatch != null)
            {
                sbWater.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(preYearDispatch.ToList()));
                sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(preYearDispatch.ToList()));
            }
            else
            {
                sbWater.AppendFormat("<td>{0}</td>", 0);
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计数
            sbDispatching.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(dispatchAll.ToList()));
            ViewState["sbDispatching"] = sbDispatching.ToString();
            sbWater.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(dispatchAll.ToList()));
            ViewState["sbWater"] = sbWater.ToString();
            #endregion

            #region 新增订单年份统计
            //当年统计数
            if (currentQuotedPrice != null)
            {
                sbRealAmount.AppendFormat("<td>{0}</td>", GetRealAmountByQuotedID(currentQuotedPrice.ToList()));
                sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(currentQuotedPrice.ToList()));
            }
            else
            {
                sbRealAmount.AppendFormat("<td>{0}</td>",0);
                sbQuoted.AppendFormat("<td>{0}</td>", 0);
            }
            //上年统计数 chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preQuoted = quotedPriceAll.Where(C => C.PartyDate >= chooseDateStar.AddYears(-1)
                && C.PartyDate <= chooseDateEnd.AddYears(-1)).ToList();
            if (preQuoted != null)
            {
                sbRealAmount.AppendFormat("<td>{0}</td>", GetRealAmountByQuotedID(preQuoted.ToList()));
                sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(preQuoted.ToList()));
            }
            else
            {
                sbRealAmount.AppendFormat("<td>{0}</td>", 0);
                sbQuoted.AppendFormat("<td>{0}</td>", 0);
            }
            sbRealAmount.AppendFormat("<td>{0}</td>", GetRealAmountByQuotedID(quotedPriceAll.ToList()));
            ViewState["sbRealAmount"] = sbRealAmount.ToString();
            //所有统计数
            sbQuoted.AppendFormat("<td>{0}</td>", GetSumNewOrderMoneyByCustomerId(quotedPriceAll.ToList()));
            ViewState["sbQuoted"] = sbQuoted.ToString();
            #endregion
        }
        
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        public string GetRealAmountByQuotedID(List<View_CustomerQuoted> currentCustomer) 
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                if (item.RealAmount.HasValue)
                {
                    currentSumMoney += item.RealAmount.Value;
                }
                else
                {
                    currentSumMoney += 0;
                }
               
            }
            return currentSumMoney + string.Empty;
            
        }
        /// <summary>
        ///  流水金额
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumEarnestMoneyByCustomerId(List<FL_Dispatching> currentCustomer)
        {

            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetEarnestMoneyByCustomerID(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
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
        /// 加载对应的数据
        /// </summary>
        protected void DataLoad()
        {
            ViewState["TargetOrderMoney"] = GetTargetDataByTargetName("当期执行订单总额");
            
            GetFlinish();
        }
        /// <summary>
        /// 根据目标名称返回对应的 时间和部门的 目标集合
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        protected string GetTargetDataByTargetName(string targetName)
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            List<ObjectParameter> objListParameter = new List<ObjectParameter>();
            objListParameter.Add(new ObjectParameter("Goal", targetName));
            objListParameter.Add(new ObjectParameter("CreateTime_between", chooseDateStar + "," + chooseDateEnd));
            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {
                    objListParameter.Add(new ObjectParameter("DepartmentId", ddlDepartment.SelectedValue.ToInt32()));
                }
            }

            //按照 targetName 返回对应所有的计划目标
            var currentYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());

            StringBuilder sb = new StringBuilder();
            //12个月份，每一个月份
            for (int i = 1; i <= 12; i++)
            {
                var singerQuery = currentYearQuery.Where(C => C.CreateTime.Value.Month == i).FirstOrDefault();
                decimal tagetValue = 0;
                if (singerQuery != null)
                {
                    tagetValue = singerQuery.TargetValue.Value;
                }
                sb.AppendFormat("<td>{0}</td>", tagetValue);
            }
            //当年合计
            sb.AppendFormat("<td>{0}</td>", currentYearQuery.Sum(C => C.TargetValue.Value));

            //上年合计
            objListParameter[1].Value = chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
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

        protected void DataDropDownList(int parentId)
        {
            ddlDepartment.DataSource = objDepartmentBLL.GetbyChildenByDepartmetnID(parentId).Where(C => C.DepartmentID != parentId);
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlDepartment.Items.FindByText("请选择").Selected = true;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }
    }
}