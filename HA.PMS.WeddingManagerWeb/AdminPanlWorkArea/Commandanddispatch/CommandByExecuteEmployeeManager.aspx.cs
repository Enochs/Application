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
    public partial class CommandByExecuteEmployeeManager : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();

        Dispatching objDispatchingBLL = new Dispatching();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Employee objEmployeeBLL = new Employee();

        Complain objComplainBLL = new Complain();
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
            ViewState["ExecuOrderTarget"] = GetTargetDataByTargetName("执行订单数");

          
            ViewState["DegreeOfSatisfactionTarget"] = GetTargetDataByTargetName("满意度");
            ViewState["ComplainTarget"] = GetTargetDataByTargetName("投诉率");
            GetFlinish();


        }
        protected void GetFlinish()
        {
            string[] chooseDateSt = GetParameterDateTime();

            List<ObjectParameter> ObjParameter = new List<ObjectParameter>();
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

            #region 满意度

            //获取与执行表中相关联的客户ID
            var degreeCustomer = GetDegreeOfSatisfactionByDispatchList(resultList);


            StringBuilder sbDegree = new StringBuilder();

            #endregion

            #region 投诉
            var complainCustomer = GetComplainByList(resultList);
            StringBuilder sbComplain = new StringBuilder();

            #endregion
            //当前部门员工的个数
            int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


            for (int i = 0; i < currentDepartEmployeeCount; i++)
            {
                HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
                int currentEmployeeId = hfEmployee.Value.ToInt32();

                //执行订单数
                var singerDispatch = resultList.Where(C => C.EmployeeID == currentEmployeeId);
                int monthDispatchCount = singerDispatch.Count();
                if (singerDispatch.Count() > 0)
                {
                    sbDispatching.AppendFormat("<td>{0}</td>", monthDispatchCount);
                }
                else
                {
                    sbDispatching.AppendFormat("<td>{0}</td>", 0);
                }

                //满意度
                var singerDegree = degreeCustomer.Where(C => C.CustomerID.Value== currentEmployeeId);
                if (singerDegree != null)
                {
                    sbDegree.AppendFormat("<td>{0}</td>", singerDegree.Count());
                }
                else
                {
                    sbDegree.AppendFormat("<td>{0}</td>", 0);
                }

                //投诉
                var singerComplain = complainCustomer.Where(C => C.ComplainEmployeeId.Value == currentEmployeeId);
                if (singerComplain != null)
                {
                    if (monthDispatchCount == 0)
                    {
                        sbComplain.AppendFormat("<td>{0}</td>", 0);
                    }
                    else
                    {
                        sbComplain.AppendFormat("<td>{0}</td>", Math.Round(
                           Convert.ToDecimal(singerComplain.Count() / monthDispatchCount), 2));
                    }

                }
                else
                {
                    sbComplain.AppendFormat("<td>{0}</td>", 0);
                }
            }

            #region 执行订单数年份统计
            //当年统计数
            if (resultList != null)
            {
                sbDispatching.AppendFormat("<td>{0}</td>", resultList.Count());
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
                sbDispatching.AppendFormat("<td>{0}</td>", preYearDispatch.Count());
            }
            else
            {
                sbDispatching.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计数
            sbDispatching.AppendFormat("<td>{0}</td>", dispatchAll.Count());
            ViewState["sbDispatching"] = sbDispatching.ToString();

            #endregion

            #region 投诉率年份调查
            //当年统计数
            if (resultList.Count() == 0)
            {
                sbComplain.AppendFormat("<td>{0}</td>", 0);
            }
            else
            {
                sbComplain.AppendFormat("<td>{0}</td>", Math.Round(
                      Convert.ToDecimal(complainCustomer.Count() / resultList.Count()), 2));
            }


            //上年统计
            var preComplainList = GetComplainByList(preYearDispatch);
            if (preComplainList.Count != 0)
            {
                sbComplain.AppendFormat("<td>{0}</td>", Math.Round(
                        Convert.ToDecimal(preComplainList.Count() / preYearDispatch.Count()), 2));
            }
            else
            {
                sbComplain.AppendFormat("<td>{0}</td>", 0);
            }
            //所有统计
            var allComplain = GetComplainByList(dispatchAll.ToList()).Count;
            if (allComplain != 0)
            {
                sbComplain.AppendFormat("<td>{0}</td>", Math.Round(
                        Convert.ToDecimal(allComplain / dispatchAll.Count()), 2));
            }
            else
            {
                sbComplain.AppendFormat("<td>{0}</td>", 0);
            }

            ViewState["sbComplain"] = sbComplain.ToString();
            #endregion
            #region 满意度年份调查
            //当年满意度
            sbDegree.AppendFormat("<td>{0}</td>", degreeCustomer.Count);
            //上年满意度
            var preDegreeList = GetDegreeOfSatisfactionByDispatchList(preYearDispatch);

            if (preDegreeList != null)
            {
                sbDegree.AppendFormat("<td>{0}</td>", preDegreeList.Count);
            }
            else
            {
                sbDegree.AppendFormat("<td>{0}</td>", 0);
            }
            //所有满意度
            var allDegree = GetDegreeOfSatisfactionByDispatchList(dispatchAll.ToList());
            if (allDegree != null)
            {
                sbDegree.AppendFormat("<td>{0}</td>", allDegree.Count);
            }
            else
            {
                sbDegree.AppendFormat("<td>{0}</td>", 0);
            }

            ViewState["sbDegree"] = sbDegree.ToString();
            #endregion

        }
        /// <summary>
        /// 根据执行数据 关联满意度中客户ID，返回最终相匹配的客户
        /// </summary>
        /// <param name="resultList"></param>
        /// <returns></returns>
        protected List<CS_DegreeOfSatisfaction> GetDegreeOfSatisfactionByDispatchList(List<FL_Dispatching> resultList)
        {
            var allDegree = objDegreeOfSatisfactionBLL.GetByAll().ToList();
            //获取与执行表中相关联的客户ID
            var degreeCustomer = new List<CS_DegreeOfSatisfaction>();
            foreach (var item in allDegree)
            {
                var singer = resultList.Where(C => C.CustomerID == item.CustomerID).FirstOrDefault();
                if (singer != null)
                {
                    degreeCustomer.Add(item);
                }
            }
            return degreeCustomer;

        }
        /// <summary>
        /// 根据执行数据 关联投诉中客户ID，返回最终相匹配的客户
        /// </summary>
        /// <param name="resultList"></param>
        /// <returns></returns>
        protected List<CS_Complain> GetComplainByList(List<FL_Dispatching> resultList)
        {

            var allComplain = objComplainBLL.GetByAll().ToList();
            //获取与执行表中相关联的客户ID
            var complainCustomer = new List<CS_Complain>();
            foreach (var item in allComplain)
            {
                var singer = resultList.Where(C => C.CustomerID == item.CustomerID).FirstOrDefault();
                if (singer != null)
                {
                    complainCustomer.Add(item);
                }
            }
            return complainCustomer;

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
        /// 绑定头部数据
        /// </summary>
        protected void DataTableHead()
        {
            Sys_Department depart = objDepartmentBLL.GetByAll().Where(C => C.DepartmentName == "执行部").FirstOrDefault();

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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            LoadTableBody();
        }

    }
}