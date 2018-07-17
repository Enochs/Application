using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using System.Text;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountTableManager
{
    public partial class StoreSaleTable : UserControlTools
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
        Order objOrderBLL = new Order();
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CA_TargetType taget = objTargetTypeBLL.GetTargetTypeByTargetName("成功预定数");

                if (taget.DepartmentId.HasValue)
                {
                    DataDropDownList(taget.DepartmentId.Value);
                }
                DataLoad();
            }
        }
        /// <summary>
        /// 加载对应的数据
        /// </summary>
        protected void DataLoad()
        {
            ViewState["successOrder"] = GetTargetDataByTargetName("成功预定数");
            ViewState["OrderRate"] = GetTargetDataByTargetName("预订率");
            GetFlinish();
        }
        /// <summary>
        /// 返回对应的数据
        /// </summary>
        protected void GetFlinish()
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
        
            //部门主管ID
            int employeeManagerId = 0;

            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {
                    Sys_Department objDepartment = objDepartmentBLL.GetByID(ddlDepartment.SelectedValue.ToInt32());
                    if (objDepartment != null)
                    {
                        employeeManagerId = objDepartment.DepartmentManager.Value;
                    }

                }
            }


            List<ObjectParameter> objOrderParameter = new List<ObjectParameter>();
            objOrderParameter.Add(new ObjectParameter("EmpLoyeeID", employeeManagerId));
            //所有根据部门成功信息查询集合
            var objOrderAll = objOrderBLL.GetOrderCustomerByIndex(objOrderParameter);
            //当年
            var objCurrentOrder = objOrderAll.Where(C => C.CreateDate >= chooseDateStar
                && C.CreateDate <= chooseDateEnd);
            //上年
            var objPreOrder = objOrderAll.Where(C => C.CreateDate >= chooseDateStar.AddYears(-1)
                && C.CreateDate <= chooseDateEnd.AddYears(-1));

            #region 总邀约成功 数据

            //用户当前选择的年份

            StringBuilder sbInviteSuccess = new StringBuilder();
            #endregion
            //成功预订数
            var successOrder = objCurrentOrder.Where(C => C.State >= 13 && C.State <= 23);
            //邀约到店数
            var BeginFollowOrder = objCurrentOrder.Where(C => C.State == 9);
            //实际到店数量
            var validateOrder = objCurrentOrder.Where(C => C.State >= 8 && C.State <= 13);



            //成功预订数
            StringBuilder sbOrderSuccess = new StringBuilder();
            int SourceCount = 0;
            var sourceAll = objOrderBLL.GetOrderCustomerByIndex(0, 0, out SourceCount, objOrderParameter);
            var chooseCustomers = sourceAll.Where(C => C.PlanComeDate != null && (C.PlanComeDate >= chooseDateStar
               && C.PlanComeDate <= chooseDateEnd));
            //预订率
            StringBuilder orderRate = new StringBuilder();
            //订单金额
            StringBuilder orderAggregateSuccess = new StringBuilder();
            //
            //定金
            StringBuilder sbEarnest = new StringBuilder();

            //实际到店数
            StringBuilder validateCount = new StringBuilder();
            for (int i = 1; i <= 12; i++)
            {
                //成功预订数
                var singerOrder = successOrder.Where(C => C.CreateDate.Value.Month == i);
                if (singerOrder != null)
                {
                    sbOrderSuccess.AppendFormat("<td>{0}</td>", singerOrder.Count());
                    orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(singerOrder.ToList()));

                    sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(singerOrder.ToList()));
                }
                else
                {
                    sbOrderSuccess.AppendFormat("<td>{0}</td>", 0);
                    orderAggregateSuccess.AppendFormat("<td>{0}</td>", 0);
                    sbEarnest.AppendFormat("<td>{0}</td>", 0);
                }
                //预订率
                //实际到店数量
                int validCount = validateOrder.Count();
                validateCount.AppendFormat("<td>{0}</td>", validCount);
                //接待直接到店的人数
                int toStore = chooseCustomers
                    .Where(C => C.RecorderDate.Value.Month == i && C.ChannelType == 0).Count();
                if (validCount == 0 && toStore == 0)
                {
                    orderRate.AppendFormat("<td>{0}</td>", 0);
                }
                else
                {
                    orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)singerOrder.Count() / (validCount + toStore), 2));
                }
                //邀约成功数
                var singerQuery = BeginFollowOrder.Where(C => C.CreateDate.Value.Month == i);
                if (singerQuery != null)
                {
                    sbInviteSuccess.AppendFormat("<td>{0}</td>", singerQuery.Count());
                }
                else
                {
                    sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
                }

            }
            #region 邀约成功量年份统计
            //邀约成功量当年合计
            sbInviteSuccess.AppendFormat("<td>{0}</td>", BeginFollowOrder.Count());
            //上年邀约成功量统计
            var preYearSuccess = objPreOrder.Where(C => C.State == 9);
            if (preYearSuccess != null)
            {
                sbInviteSuccess.AppendFormat("<td>{0}</td>", preYearSuccess.Count());
            }
            else
            {
                sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbInviteSuccess.AppendFormat("<td>{0}</td>", objOrderAll.Where(C => C.State == 9).Count());

            ViewState["sbInviteSuccess"] = sbInviteSuccess.ToString();
            #endregion


            #region 预订率年份统计
            //当前年份成功预订数
            var currentYearCount = successOrder.Count();

            //当前年份接待直接到店数
            var validateToStoreCount = chooseCustomers.Where(C => C.State >= 8 && C.State <= 13).Count();
            validateCount.AppendFormat("<td>{0}</td>", validateToStoreCount);
            //直接到店人数
            var toStoreCount = chooseCustomers.Where(C => C.ChannelType == 0).Count();
            orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)currentYearCount / (validateToStoreCount + toStoreCount), 2));

            //上年成功统计预订数
            var preYearRateOrder = objPreOrder.Where(C => C.State > 13 && C.State <= 23);


            //上年年份接待直接到店数
            var validatePreToStoreCount = objPreOrder.Where(C => C.State >= 8 && C.State <= 13).Count();

            //上年到店人数
            var toPreStoreCount = chooseCustomers.Where(C => C.ChannelType == 0 && C.PlanComeDate >= chooseDateStar.AddYears(-1)
               && C.PlanComeDate <= chooseDateEnd.AddYears(-1)).Count();

            if (preYearRateOrder.Count() != 0)
            {

                orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)preYearRateOrder.Count() / (validatePreToStoreCount + toPreStoreCount), 2));
            }
            else
            {
                orderRate.AppendFormat("<td>{0}</td>", 0);
            }
            validateCount.AppendFormat("<td>{0}</td>", validatePreToStoreCount);
            //所有成功统计
            //所有成功预订数
            var allOrderCount = objOrderAll.Where(C => C.State >= 13 && C.State <= 23).Count();
            //所有接待到店数
            var validateAllToStoreCount = objOrderAll.Where(C => C.State >= 8 && C.State <= 13).Count();

            //所有直接到店人数
            var toStoreAllCount = objOrderAll.Where(C => C.ChannelType == 0).Count();

            validateCount.AppendFormat("<td>{0}</td>", validateAllToStoreCount);

            if ( (validateAllToStoreCount + toStoreAllCount)==0)
            {
                orderRate.AppendFormat("<td>{0}</td>", 0);
            }
            else
            {
                orderRate.AppendFormat("<td>{0}</td>", Math.Round((double)allOrderCount / (validateAllToStoreCount + toStoreAllCount), 2));
             
            }
        
            ViewState["orderRate"] = orderRate.ToString();
            ViewState["validateCount"] = validateCount.ToString();
            #endregion
            #region 成功预订年份统计
            sbOrderSuccess.AppendFormat("<td>{0}</td>", successOrder.Count());
            orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(objCurrentOrder.ToList()));

            sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(objCurrentOrder.ToList()));
            //上年成功统计
            var preYearOrder = objPreOrder.Where(C => C.State >= 13 && C.State <= 23);
            orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(preYearOrder.ToList()));

            sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(preYearOrder.ToList()));
            if (preYearOrder != null)
            {
                sbOrderSuccess.AppendFormat("<td>{0}</td>", preYearOrder.Count());
            }
            else
            {
                sbOrderSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            //所有成功统计
            sbOrderSuccess.AppendFormat("<td>{0}</td>", objOrderAll.Where(C => C.State >= 13 && C.State <= 23).Count());
            orderAggregateSuccess.AppendFormat("<td>{0}</td>", GetSumOrderMoneyByCustomerId(objOrderAll.ToList()));

            sbEarnest.AppendFormat("<td>{0}</td>", GetSumEarnestMoneyByCustomerLIst(objOrderAll.ToList()));

            ViewState["sbEarnest"] = sbEarnest.ToString();
            ViewState["orderAggregateSuccess"] = orderAggregateSuccess.ToString();
            ViewState["sbOrderSuccess"] = sbOrderSuccess.ToString();
            #endregion

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
        /// <summary>
        /// 算出定金
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumEarnestMoneyByCustomerLIst(List<View_GetOrderCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetEarnestMoneyByCustomerID(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }
        /// <summary>
        /// 算出订单总金额 
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumOrderMoneyByCustomerId(List<View_GetOrderCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}