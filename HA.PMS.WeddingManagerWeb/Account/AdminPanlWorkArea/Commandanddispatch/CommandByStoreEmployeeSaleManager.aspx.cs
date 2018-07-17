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
    public partial class CommandByStoreEmployeeSaleManager : SystemPage
    {
        //Customers objCustomersBLL = new Customers();
        //Department objDepartmentBLL = new Department();
        //SaleSources objSaleSourcesBLL = new SaleSources();
        //MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        //TargetType objTargetTypeBLL = new TargetType();

        //Order objOrderBLL = new Order();
        //HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        //Dispatching objDispatchingBLL = new Dispatching();
        //HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        //Employee objEmployeeBLL = new Employee();
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {

        //        DataTableHead();
        //        GetTdStr();
        //        LoadTableBody();
        //    }
        //}
        ///// <summary>
        ///// 绑定主体数据
        ///// </summary>
        //protected void LoadTableBody()
        //{

        //    ViewState["successOrder"] = GetTargetDataByTargetName("成功预定数");
        //    ViewState["OrderRate"] = GetTargetDataByTargetName("预订率");
        //    GetFlinish();

        //}
        ///// <summary>
        ///// 加载空白表格单元格
        ///// </summary>
        ///// <returns></returns>
        //protected void GetTdStr()
        //{
        //    int employeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();
        //    StringBuilder sbTds = new StringBuilder();
        //    for (int i = 0; i < employeeCount + 3; i++)
        //    {
        //        sbTds.Append("<td></td>");
        //    }
        //    ViewState["sbTds"] = sbTds.ToString();

        //}
        ///// <summary>
        ///// 根据当前下拉框选择的时间生成对应的相关参数
        ///// </summary>
        ///// <returns></returns>
        //protected string[] GetParameterDateTime()
        //{
        //    string[] chooseDateStr = new string[4];
        //    if (ddlChooseYear.Items.Count == 0)
        //    {
        //        chooseDateStr[0] = (DateTime.Now.Year + "-1-1");

        //        chooseDateStr[1] = DateTime.Now.Year + "-12-31";
        //    }
        //    else
        //    {
        //        for (int i = 0; i < ddlChooseYear.SelectedValue.Split(',').Count(); i++)
        //        {
        //            chooseDateStr[i] = ddlChooseYear.SelectedValue.Split(',')[i];
        //        }

        //    }
        //    DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
        //    DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
        //    if (ddlChooseMonth.SelectedValue != "0")
        //    {
        //        //加入月份时间
        //        chooseDateStar = chooseDateStar.AddMonths(ddlChooseMonth.SelectedValue.ToInt32() - 1);

        //        int year = chooseDateStar.Year;
        //        int month = chooseDateStar.Month;

        //        chooseDateEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        //        chooseDateStr[0] = chooseDateStar + string.Empty;
        //        chooseDateStr[1] = chooseDateEnd + string.Empty;
        //    }
        //    //上年
        //    chooseDateStr[2] = chooseDateStar.Year - 1 + "-1-1";
        //    chooseDateStr[3] = chooseDateStar.Year - 1 + "-12-31";
        //    return chooseDateStr;
        //}
        ///// <summary>
        ///// 根据目标名称返回对应的 时间和部门的 目标集合
        ///// </summary>
        ///// <param name="targetName"></param>
        ///// <returns></returns>
        //protected string GetTargetDataByTargetName(string targetName)
        //{
        //    string[] chooseDateSt = GetParameterDateTime();
        //    List<ObjectParameter> objListParameter = new List<ObjectParameter>();
        //    objListParameter.Add(new ObjectParameter("Goal", targetName));

        //    objListParameter.Add(new ObjectParameter("CreateTime_between", chooseDateSt[0].ToDateTime() + "," + chooseDateSt[1].ToDateTime()));


        //    //按照 targetName 返回对应所有的计划目标
        //    var currentYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());

        //    StringBuilder sb = new StringBuilder();
        //    //当前部门员工的个数
        //    int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


        //    for (int i = 0; i < currentDepartEmployeeCount; i++)
        //    {

        //        HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
        //        int currentEmployeeId = hfEmployee.Value.ToInt32();
        //        //查询当前员工的目标值
        //        var singerQuery = currentYearQuery.Where(C => C.CreateEmployeeId.Value == currentEmployeeId).ToList();
        //        decimal tagetValue = 0;
        //        if (singerQuery != null)
        //        {
        //            tagetValue = singerQuery.Sum(C => C.TargetValue.Value);
        //        }
        //        sb.AppendFormat("<td>{0}</td>", tagetValue);
        //    }



        //    //当年合计
        //    sb.AppendFormat("<td>{0}</td>", currentYearQuery.Sum(C => C.TargetValue.Value));

        //    //上年合计
        //    objListParameter[1].Value = chooseDateSt[2].ToDateTime() + ","
        //        + chooseDateSt[3].ToDateTime();
        //    var preYearQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
        //    if (preYearQuery != null)
        //    {
        //        sb.AppendFormat("<td>{0}</td>", preYearQuery.Sum(C => C.TargetValue.Value));
        //    }
        //    else
        //    {
        //        sb.AppendFormat("<td>{0}</td>", 0);
        //    }
        //    //历史累计
        //    //移除时间参数，查询对应的部门的所有目标
        //    objListParameter.RemoveAt(1);
        //    var queryAll = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
        //    if (queryAll != null)
        //    {
        //        sb.AppendFormat("<td>{0}</td>", queryAll.Sum(C => C.TargetValue.Value));
        //    }
        //    else
        //    {
        //        sb.AppendFormat("<td>{0}</td>", 0);
        //    }
        //    return sb.ToString();

        //}
        ///// <summary>
        ///// 返回对应的数据
        ///// </summary>
        //protected void GetFlinish()
        //{

        //    string[] chooseDateSt = GetParameterDateTime();
        //    var GetWhereParList = new List<ObjectParameter>();
        //    GetWhereParList.Add(new ObjectParameter("State_Greaterthan", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.Sucess + string.Empty));
        //    //总邀约参数
        //    var GetWhereSuccessParList = new List<ObjectParameter>();
        //    GetWhereSuccessParList.Add(new ObjectParameter("State_NumOr", (int)CustomerStates.InviteSucess + "," + (int)CustomerStates.Sucess + string.Empty));
 
        //    #region 总邀约成功 数据
        //    //获取不含年份所有成功数 
        //    var DataList = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereSuccessParList);

        //    //用户当前选择的年份
        //    var chooseYear = DataList.Where(C => C.CreateDate >= chooseDateSt[0].ToDateTime() && C.CreateDate <= chooseDateSt[1].ToDateTime());
        //    StringBuilder sbInviteSuccess = new StringBuilder();
        //    #endregion
        //    //成功预订数


        //    var OrderObj = objOrderBLL.GetCustomerOrderEmployeeParameter(GetWhereParList.ToArray());
        //    StringBuilder sbOrderSuccess = new StringBuilder();
        //    var chooseYearOrder = OrderObj.Where(C => C.PlanComeDate >= chooseDateSt[0].ToDateTime() && C.PlanComeDate <= chooseDateSt[1].ToDateTime());

        //    int SourceCount = 0;
        //    var sourceAll = objOrderBLL.GetOrderCustomerByIndex(0, 0, out SourceCount, GetWhereParList);

        //    var chooseCustomers = sourceAll.Where(C => C.PlanComeDate != null && (C.PlanComeDate >= chooseDateSt[0].ToDateTime()
        //       && C.PlanComeDate <= chooseDateSt[1].ToDateTime()));
        //    //预订率
        //    StringBuilder orderRate = new StringBuilder();
        //    //订单金额
        //    StringBuilder orderAggregateSuccess = new StringBuilder();
        //    //
        //    //定金
        //    StringBuilder sbEarnest = new StringBuilder();

        //    //实际到店数
        //    StringBuilder validateCount = new StringBuilder();

        //    //当前部门员工的个数
        //    int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


        //    for (int i = 0; i < currentDepartEmployeeCount; i++)
        //    {

        //        HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
        //        int currentEmployeeId = hfEmployee.Value.ToInt32();

        //        //成功预订数
        //        var singerOrder = chooseYearOrder.Where(C => C.EmployeeID.Value == currentEmployeeId);
        //        if (singerOrder != null)
        //        {
        //            sbOrderSuccess.AppendFormat("<td>{0}</td>", singerOrder.Count());
        //            orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(singerOrder.ToList()));

        //            sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(singerOrder.ToList()));
        //        }
        //        else
        //        {
        //            sbOrderSuccess.AppendFormat("<td>{0}</td>", 0);
        //            orderAggregateSuccess.AppendFormat("<td>{0}</td>", 0);
        //            sbEarnest.AppendFormat("<td>{0}</td>", 0);
        //        }
        //        //预订率
        //        //实际到店数量
        //        int validCount = chooseCustomers
        //          .Where(C => C.EmployeeID.Value == currentEmployeeId && (C.State >= 8 && C.State <= 13)).Count();
        //        validateCount.AppendFormat("<td>{0}</td>", validCount);
        //        //接待直接到店的人数
        //        int toStore = chooseCustomers
        //            .Where(C => C.EmployeeID.Value == currentEmployeeId && C.ChannelType == 0).Count();
        //        if (validCount == 0 && toStore == 0)
        //        {
        //            orderRate.AppendFormat("<td>{0}</td>", 0);
        //        }
        //        else
        //        {
        //            orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)singerOrder.Count() / (validCount + toStore), 2));
        //        }
        //        //邀约成功数
        //        var singerQuery = chooseYear.Where(C => C.CreateEmployee.Value == currentEmployeeId);
        //        if (singerQuery != null)
        //        {
        //            sbInviteSuccess.AppendFormat("<td>{0}</td>", singerQuery.Count());
        //        }
        //        else
        //        {
        //            sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
        //        }

        //    }
        //    #region 邀约成功量年份统计
        //    //邀约成功量当年合计
        //    sbInviteSuccess.AppendFormat("<td>{0}</td>", chooseYear.Count());
        //    //上年邀约成功量统计
        //    var preYearSuccess = DataList.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
        //        && C.CreateDate <= chooseDateSt[3].ToDateTime());
        //    if (preYearSuccess != null)
        //    {
        //        sbInviteSuccess.AppendFormat("<td>{0}</td>", preYearSuccess.Count());
        //    }
        //    else
        //    {
        //        sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
        //    }
        //    //所有历史统计
        //    sbInviteSuccess.AppendFormat("<td>{0}</td>", DataList.Count);

        //    ViewState["sbInviteSuccess"] = sbInviteSuccess.ToString();
        //    #endregion


        //    #region 预订率年份统计
        //    //当前年份成功预订数
        //    var currentYearCount = chooseYearOrder.Count();

        //    //当前年份接待直接到店数
        //    var validateToStoreCount = chooseCustomers.Where(C => C.State >= 8 && C.State <= 13).Count();
        //    validateCount.AppendFormat("<td>{0}</td>", validateToStoreCount);
        //    //直接到店人数
        //    var toStoreCount = chooseCustomers.Where(C => C.ChannelType == 0).Count();
        //    orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)currentYearCount / (validateToStoreCount + toStoreCount), 2));

        //    //上年成功统计预订数
        //    var preYearRateOrder = OrderObj.Where(C => C.PlanComeDate >= chooseDateSt[2].ToDateTime()
        //       && C.PlanComeDate <= chooseDateSt[3].ToDateTime());

        //    //上年年份接待直接到店数
        //    var validatePreToStoreCount = chooseCustomers.Where(C => C.State >= 8 && C.PlanComeDate >= chooseDateSt[2].ToDateTime()
        //       && C.PlanComeDate <=  chooseDateSt[3].ToDateTime()).Count();

        //    //上年到店人数
        //    var toPreStoreCount = chooseCustomers.Where(C => C.ChannelType == 0 && C.PlanComeDate >= chooseDateSt[2].ToDateTime()
        //       && C.PlanComeDate <= chooseDateSt[3].ToDateTime()).Count();

        //    if (preYearRateOrder.Count() != 0)
        //    {

        //        orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)preYearRateOrder.Count() / (validatePreToStoreCount + toPreStoreCount), 2));
        //    }
        //    else
        //    {
        //        orderRate.AppendFormat("<td>{0}</td>", 0);
        //    }
        //    validateCount.AppendFormat("<td>{0}</td>", validatePreToStoreCount);
        //    //所有成功统计
        //    //所有成功预订数
        //    var allOrderCount = OrderObj.Count();
        //    //所有接待到店数
        //    var validateAllToStoreCount = sourceAll.Where(C => C.State >= 8 && C.State <= 13).Count();

        //    //所有直接到店人数
        //    var toStoreAllCount = sourceAll.Where(C => C.ChannelType == 0).Count();
        //    validateCount.AppendFormat("<td>{0}</td>", validateAllToStoreCount);
        //    orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)allOrderCount / (validateAllToStoreCount + toStoreAllCount), 2));
        //    ViewState["orderRate"] = orderRate.ToString();
        //    ViewState["validateCount"] = validateCount.ToString();
        //    #endregion
        //    #region 成功预订年份统计
        //    sbOrderSuccess.AppendFormat("<td>{0}</td>", chooseYearOrder.Count());
        //    orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(chooseYearOrder.ToList()));

        //    sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(chooseYearOrder.ToList()));
        //    //上年成功统计
        //    var preYearOrder = OrderObj.Where(C => C.PlanComeDate >= chooseDateSt[2].ToDateTime()
        //       && C.PlanComeDate <= chooseDateSt[3].ToDateTime());
        //    orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(preYearOrder.ToList()));

        //    sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(preYearOrder.ToList()));
        //    if (preYearOrder != null)
        //    {
        //        sbOrderSuccess.AppendFormat("<td>{0}</td>", preYearOrder.Count());
        //    }
        //    else
        //    {
        //        sbOrderSuccess.AppendFormat("<td>{0}</td>", 0);
        //    }
        //    //所有成功统计
        //    sbOrderSuccess.AppendFormat("<td>{0}</td>", OrderObj.Count());
        //    orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(OrderObj.ToList()));
        //    ViewState["sbEarnest"] = sbEarnest.ToString();
        //    ViewState["orderAggregateSuccess"] = orderAggregateSuccess.ToString();
        //    ViewState["sbOrderSuccess"] = sbOrderSuccess.ToString();
        //    #endregion

        //}
      
        ///// <summary>
        ///// 算出定金
        ///// </summary>
        ///// <param name="currentCustomer"></param>
        ///// <returns></returns>
        //protected string GetSumEarnestMoneyByCustomerLIst(List<FLCustomerOrderEmployee> currentCustomer)
        //{
        //    decimal currentSumMoney = 0;
        //    foreach (var item in currentCustomer)
        //    {
        //        currentSumMoney += GetEarnestMoneyByCustomerID(item.CustomerID).ToDecimal();
        //    }
        //    return currentSumMoney + string.Empty;
        //}
        ///// <summary>
        ///// 算出订单总金额 
        ///// </summary>
        ///// <param name="currentCustomer"></param>
        ///// <returns></returns>
        //protected string GetSumOrderMoneyByCustomerId(List<FLCustomerOrderEmployee> currentCustomer)
        //{
        //    decimal currentSumMoney = 0;
        //    foreach (var item in currentCustomer)
        //    {
        //        currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
        //    }
        //    return currentSumMoney + string.Empty;
        //}
        ///// <summary>
        ///// 绑定头部数据
        ///// </summary>
        //protected void DataTableHead()
        //{
        //    Sys_Department depart = objDepartmentBLL.GetByAll().Where(C => C.DepartmentName == "销售部").FirstOrDefault();

        //    List<Sys_Employee> empList = new List<Sys_Employee>();
        //    if (depart != null)
        //    {
        //        ViewState["depart"] = depart.DepartmentID;
        //        empList = objEmployeeBLL.GetByALLDepartmetnID(depart.DepartmentID);
        //        ViewState["employeeCount"] = empList.Count;
        //        rptEmployee.DataSource = empList;
        //        rptEmployee.DataBind();
        //    }

        //}

        //protected void btnQuery_Click(object sender, EventArgs e)
        //{
        //    LoadTableBody();
        //}

    }
}