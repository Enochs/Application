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
    public partial class CommandbySaleSourceEmployeeManager : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        //返利
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();

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
        /// 加载空白表格单元格
        /// </summary>
        /// <returns></returns>
        protected void GetTdStr() 
        {
            int employeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();
            StringBuilder sbTds = new StringBuilder();
            for (int i = 0; i < employeeCount+3; i++)
            {
                sbTds.Append("<td></td>");
            }
            ViewState["sbTds"] = sbTds.ToString();
        
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

        protected void GetFlinish()
        {
            string[] chooseDateSt = GetParameterDateTime();

            List<ObjectParameter> ObjParameter = new List<ObjectParameter>();
            ObjParameter.Add(new ObjectParameter("CreateDate_between", chooseDateSt[0].ToDateTime() + "," + chooseDateSt[1].ToDateTime()));

            //ObjParameter.Add(new ObjectParameter("DepartmentId",Convert.ToInt32(ViewState["depart"])));
        
            

            //客户量
            var query = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray());
            StringBuilder sbCustomersCount = new StringBuilder();
            //有效量
            var queryValid = query.Where(C => C.State != 300);
            StringBuilder sbValide = new StringBuilder();
            //邀约成功量
            var querySuccess = query.Where(C => C.State >= 6);
            StringBuilder sbSuccess = new StringBuilder();
            //成交量
            var queryClinchaDeal = query.Where(C => C.State >= 17);
            StringBuilder sbClinchaDeal = new StringBuilder();

            //订单金额
            StringBuilder sbAggregateAmount = new StringBuilder();
            //返利
            StringBuilder sbPay = new StringBuilder();
            List<ObjectParameter> ObjPayParameterList = new List<ObjectParameter>();
            ObjectParameter dates = new ObjectParameter("PayDate_between", chooseDateSt[1].ToDateTime() + "," + chooseDateSt[1].ToDateTime());
            ObjPayParameterList.Add(dates);
            ObjPayParameterList.Add(new ObjectParameter("IsFinish", true));
            var payNeed = objPayNeedRabateBLL.GetByParaandIndex(ObjPayParameterList.ToArray());
            //当前部门员工的个数
            int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


            for (int i = 0; i < currentDepartEmployeeCount; i++)
            {

                HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
                int currentEmployeeId = hfEmployee.Value.ToInt32();

                //客户量 （含订单金额）
                var customers = query.Where(C => C.CreateEmpLoyee.Value == currentEmployeeId);
                if (customers != null)
                {

                    sbCustomersCount.AppendFormat("<td>{0}</td>", customers.Count());
                    //订单金额
                    sbAggregateAmount.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(customers.ToList()));
                }
                else
                {
                    sbCustomersCount.AppendFormat("<td>{0}</td>", 0);
                    //订单金额
                    sbAggregateAmount.AppendFormat("<td>{0}</td>", 0);
                }
                //有效信息
                var customersValide = queryValid.Where(C => C.CreateEmpLoyee.Value == currentEmployeeId);
                if (customersValide != null)
                {
                    sbValide.AppendFormat("<td>{0}</td>", customersValide.Count());
                }
                else
                {
                    sbValide.AppendFormat("<td>{0}</td>", 0);
                }
                //成功量
                var customersSuccess = querySuccess.Where(C => C.CreateEmpLoyee.Value == currentEmployeeId);
                if (customersSuccess != null)
                {
                    sbSuccess.AppendFormat("<td>{0}</td>", customersSuccess.Count());
                }
                else
                {
                    sbSuccess.AppendFormat("<td>{0}</td>", 0);
                }
                //成交量
                var customersClinchaDeal = queryClinchaDeal.Where(C => C.CreateEmpLoyee.Value == currentEmployeeId);
                if (customersClinchaDeal != null)
                {
                    sbClinchaDeal.AppendFormat("<td>{0}</td>", customersClinchaDeal.Count());
                }
                else
                {
                    sbClinchaDeal.AppendFormat("<td>{0}</td>", 0);
                }
                //返利
                var customersPayNeed = payNeed.Where(C => C.CustomerID.Value== currentEmployeeId);
                if (customersPayNeed != null)
                {

                    sbPay.AppendFormat("<td>{0}</td>", GetPaySumMoney(customersPayNeed.ToList()));
                }
                else
                {
                    sbPay.AppendFormat("<td>{0}</td>", 0);
                }

            }

            #region 客户量年份统计
            //客户量当年合计
            sbCustomersCount.AppendFormat("<td>{0}</td>", query.Count);

            //客户量上年合计
            ObjParameter[0].Value = chooseDateSt[2].ToDateTime() + "," + chooseDateSt[3].ToDateTime();
            var preYearQuery = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray());
            if (preYearQuery != null)
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", preYearQuery.Count);
            }
            else
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", 0);
            }
            //客户量历史累计
            //移除时间参数，查询对应的部门的所有目标
            ObjParameter.RemoveAt(0);
            var queryAll = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray());
            if (queryAll != null)
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", queryAll.Count);
            }
            else
            {
                sbCustomersCount.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["Flinish"] = sbCustomersCount.ToString();
            #endregion

            #region 有效量年份统计
            //当前有客户有效量合计
            sbValide.AppendFormat("<td>{0}</td>", queryValid.Count());
            //客户量上年合计
            var preValidate = preYearQuery.Where(C => C.State != 300);
            if (preValidate != null)
            {
                sbValide.AppendFormat("<td>{0}</td>", preValidate.Count());
            }
            else
            {
                sbValide.AppendFormat("<td>{0}</td>", 0);
            }
            //客户量历史累计
            var queryValidateAll = queryAll.Where(C => C.State != 300);
            if (queryValidateAll != null)
            {
                sbValide.AppendFormat("<td>{0}</td>", queryValidateAll.Count());
            }
            else
            {
                sbValide.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbValide"] = sbValide.ToString();
            #endregion

            #region 成功量的年份统计
            //当前成功量合计
            sbSuccess.AppendFormat("<td>{0}</td>", querySuccess.Count());
            //成功量上年合计
            var preSuccess = preYearQuery.Where(C => C.State >= 6);
            if (preSuccess != null)
            {
                sbSuccess.AppendFormat("<td>{0}</td>", preSuccess.Count());
            }
            else
            {
                sbSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            //成功量历史累计
            var querySuccessAll = queryAll.Where(C => C.State >= 6);
            if (querySuccessAll != null)
            {
                sbSuccess.AppendFormat("<td>{0}</td>", querySuccessAll.Count());
            }
            else
            {
                sbSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbSuccess"] = sbSuccess.ToString();
            #endregion

            #region 成交量年份统计
            //成交量当年合计
            sbClinchaDeal.AppendFormat("<td>{0}</td>", queryClinchaDeal.Count());

            //成交量上年合计

            var preClinchaDeal = preYearQuery.Where(C => C.State >= 17);
            if (preClinchaDeal != null)
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", preClinchaDeal.Count());
            }
            else
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", 0);
            }
            //成交量历史累计
            var queryClinchaDealAll = queryAll.Where(C => C.State >= 17);
            if (queryClinchaDealAll != null)
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", queryClinchaDealAll.Count());
            }
            else
            {
                sbClinchaDeal.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbClinchaDeal"] = sbClinchaDeal.ToString();
            #endregion

            #region 订单金额
            sbAggregateAmount.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(query.ToList()));

            //订单金额上年合计

            var preAggregateAmount = GetSumOrderMoneyByCustomerId(preYearQuery.ToList());
            sbAggregateAmount.AppendFormat("<td>{0}</td>", preAggregateAmount);
            //订单金额累计
            var queryAggregateAmountAll = GetSumOrderMoneyByCustomerId(queryAll.ToList());
            sbAggregateAmount.AppendFormat("<td>{0}</td>", queryAggregateAmountAll);
            ViewState["sbAggregateAmount"] = sbAggregateAmount.ToString();

            #endregion


            #region 返利年份统计
            //返利当年合计
            sbPay.AppendFormat("<td>{0}</td>", GetPaySumMoney(payNeed.ToList()));
            //返利上年统计
            ObjPayParameterList[0].Value = chooseDateSt[2].ToDateTime() + "," + chooseDateSt[3].ToDateTime();
            var prePayQuery = objPayNeedRabateBLL.GetByParaandIndex(ObjPayParameterList.ToArray());
            if (prePayQuery != null)
            {
                sbPay.AppendFormat("<td>{0}</td>", GetPaySumMoney(prePayQuery.ToList()));
            }
            else
            {
                sbPay.AppendFormat("<td>{0}</td>", 0);
            }
            //返利历史累计
            ObjPayParameterList.RemoveAt(0);
            var payAll = objPayNeedRabateBLL.GetByParaandIndex(ObjPayParameterList.ToArray());
            if (payAll != null)
            {
                sbPay.AppendFormat("<td>{0}</td>", GetPaySumMoney(payAll.ToList()));
            }
            else
            {
                sbPay.AppendFormat("<td>{0}</td>", 0);
            }
            ViewState["sbPay"] = sbPay.ToString();
            #endregion
        }
        /// <summary>
        /// 返回返利金额
        /// </summary>
        /// <param name="allPayMoney"></param>
        /// <returns></returns>
        protected string GetPaySumMoney(List<FD_PayNeedRabate> allPayMoney)
        {
            decimal allPaySumMoney = 0;
            foreach (var item in allPayMoney)
            {

                if (item.PayMoney.HasValue)
                {
                    allPaySumMoney += item.PayMoney.Value;
                }

            }
            return allPaySumMoney + string.Empty;
        }
        /// <summary>
        /// 算出订单总金额 
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumOrderMoneyByCustomerId(List<View_GetTelmarketingCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }

        /// <summary>
        /// 绑定头部数据
        /// </summary>
        protected void DataTableHead()
        {
            Sys_Department depart = objDepartmentBLL.GetByAll().Where(C => C.DepartmentName == "渠道部").FirstOrDefault();
            
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

        /// <summary>
        /// 绑定主体数据
        /// </summary>
        protected void LoadTableBody()
        {
            ViewState["customerCount"] = GetTargetDataByTargetName("有效信息数");
            ViewState["validateRate"] = GetTargetDataByTargetName("客源有效率");
            GetFlinish();
            //  string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            // DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            // DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();

        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            LoadTableBody();
        }
    }
}