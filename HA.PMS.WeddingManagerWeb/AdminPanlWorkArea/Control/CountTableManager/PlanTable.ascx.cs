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
    public partial class PlanTable : UserControlTools
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();

        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();

        Employee objEmployeeBLL = new Employee();
        Dispatching objDispatchingBLL = new Dispatching();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
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
        protected void DataDropDownList(int parentId)
        {
            ddlDepartment.DataSource = objDepartmentBLL.GetbyChildenByDepartmetnID(parentId)
                .Where(C => C.DepartmentID != parentId);
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlDepartment.Items.FindByText("请选择").Selected = true;
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

        /// <summary>
        /// 加载对应的数据
        /// </summary>
        protected void DataLoad()
        {
            ViewState["currentOrderMoneyTarget"] = GetTargetDataByTargetName("当期执行订单总额");


            ViewState["rateTarget"] = GetTargetDataByTargetName("毛利率");
            ViewState["newOrderMoneyTarget"] = GetTargetDataByTargetName("当期新增订单总额");
            GetFlinish();
        }

        protected void GetFlinish()
        {
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
           //部门主管
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

            #region 执行订单总金额
            int resource = 0;
            List<ObjectParameter> objParameterDisList = new List<ObjectParameter>();

            objParameterDisList.Add(new ObjectParameter("isover", false));
         

            objParameterDisList.Add(new ObjectParameter("EmpLoyeeID", employeeManagerId));
            //所有执行订单总金额
            var objExcuteResult = objDispatchingBLL.GetDispatchingByWhere(0, 1, out resource, 0, objParameterDisList);
            //当年执行订单总金额
            var objCurrentExcuteResult = objExcuteResult.Where(C => C.PartyDate >= chooseDateStar && C.PartyDate <= chooseDateEnd);
            //上年执行订单总金额
            var objPreExcuteResult = objExcuteResult.Where(C => C.PartyDate >= chooseDateStar.AddYears(-1) && C.PartyDate <= chooseDateEnd.AddYears(-1));
            StringBuilder sbDispatching = new StringBuilder();
           
            #endregion


            #region 当期新增订单金额
            List<ObjectParameter> objParameterList = new List<ObjectParameter>();
            objParameterList.Add(new ObjectParameter("IsDispatching", 0));
          
            objParameterList.Add(new ObjectParameter("EmpLoyeeID", employeeManagerId));
            StringBuilder sbQuoted = new StringBuilder();
            //所有新增订单金额
            var objNewOrderResult = objQuotedPriceBLL.GetCustomerQuotedParameter(objParameterList);
            //当前新增订单
            var objCurrentNewOrder = objNewOrderResult.Where(C => C.PartyDate >= chooseDateStar && C.PartyDate <= chooseDateEnd);
            //上年新增订单
            var objPreNewOrder = objNewOrderResult.Where(C => C.PartyDate >= chooseDateStar.AddYears(-1) && C.PartyDate <= chooseDateEnd.AddYears(-1));
            #endregion


            for (int i = 1; i <= 12; i++)
            {
                //执行订单数
                var singerDispatch = objCurrentExcuteResult.Where(C => C.PartyDate.Value.Month == i);
                int monthDispatchCount = singerDispatch.Count();
                if (singerDispatch.Count() > 0)
                {
                    sbDispatching.AppendFormat("<td>{0}</td>", singerDispatch.Sum(C => C.AggregateAmount));
                }
                else
                {
                    sbDispatching.AppendFormat("<td>{0}</td>", 0);
                }
                //新增订单金额
                var singerOrder = objCurrentNewOrder.Where(C => C.PartyDate.Value.Month == i);
                if (singerOrder != null)
                {
                    sbQuoted.AppendFormat("<td>{0}</td>", singerOrder.Sum(C => C.AggregateAmount));
                }
                else
                {
                    sbQuoted.AppendFormat("<td>{0}</td>", 0);
                }
            }

            #region 执行订单数年份统计
            //当年统计数
            if (objCurrentExcuteResult != null)
            {
                sbDispatching.AppendFormat("<td>{0}</td>",objCurrentExcuteResult.Sum(C => C.AggregateAmount));
            }
            else
            {
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //上年统计数 chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);

            if (objPreExcuteResult != null)
            {
                sbDispatching.AppendFormat("<td>{0}</td>", objPreExcuteResult.Sum(C => C.AggregateAmount));
            }
            else
            {
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计数
            sbDispatching.AppendFormat("<td>{0}</td>", objExcuteResult.Sum(C => C.AggregateAmount));
            ViewState["sbDispatching"] = sbDispatching.ToString();

            #endregion

            #region 新增订单年份统计
            //当年统计数
            if (objCurrentNewOrder != null)
            {
                sbQuoted.AppendFormat("<td>{0}</td>", objCurrentNewOrder.Sum(C => C.AggregateAmount));
            }
            else
            {
                sbQuoted.AppendFormat("<td>{0}</td>", 0);
            }
           

            if (objPreNewOrder != null)
            {
                sbQuoted.AppendFormat("<td>{0}</td>", objPreNewOrder.Sum(C => C.AggregateAmount));
            }
            else
            {
                sbQuoted.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计数
            sbQuoted.AppendFormat("<td>{0}</td>", objNewOrderResult.Sum(C => C.AggregateAmount));
            ViewState["sbQuoted"] = sbQuoted.ToString();
            #endregion
        }
       

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}