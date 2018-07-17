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
    public partial class CommandByInviteEmployeeManager : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Order objOrderBLL = new Order();
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
        protected void GetFlinish()
        {
            string[] chooseDateSt = GetParameterDateTime();


            var GetWhereParList = new List<ObjectParameter>();
            GetWhereParList.Add(new ObjectParameter("State_NumOr", (int)CustomerStates.InviteSucess + "," + string.Empty));
            //客源量 参数集合
            var customerList = new List<ObjectParameter>();
           
            // 订单总数

            int SourceCount = 0;
            var orderAll = objOrderBLL.GetOrderCustomerByIndex(0, 0, out SourceCount, GetWhereParList);


            StringBuilder sbOrder = new StringBuilder();
            var chooseYearOrder = orderAll.Where(C => C.PlanComeDate >= chooseDateSt[0].ToDateTime()
                && C.PlanComeDate <= chooseDateSt[1].ToDateTime());

            //有效信息
            var valideAll = orderAll.Where(C => C.State != 300);
            StringBuilder sbValid = new StringBuilder();
            var chooseYearValid = valideAll.Where(C => C.PlanComeDate >= chooseDateSt[0].ToDateTime() 
                && C.PlanComeDate <= chooseDateSt[1].ToDateTime());


            //获取不含年份所有成功数 
            var DataList = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);

            //用户当前选择的年份
            var chooseYear = DataList.Where(C => C.CreateDate >= chooseDateSt[0].ToDateTime()
                && C.CreateDate <= chooseDateSt[1].ToDateTime());
            StringBuilder sbInviteSuccess = new StringBuilder();

            //邀约中
            GetWhereParList[0] = new ObjectParameter("State", (int)CustomerStates.DoInvite);
            var OngoingInvite = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);
            var chooseOngoingYear = OngoingInvite.Where(C => C.CreateDate >= chooseDateSt[1].ToDateTime()
                && C.CreateDate <= chooseDateSt[1].ToDateTime());
            StringBuilder sbOngoing = new StringBuilder();
            //流失  (int)CustomerStates.Lose)
            GetWhereParList[0] = new ObjectParameter("State", (int)CustomerStates.Lose);
            var LoseInvite = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);
            var chooseLoseYear = LoseInvite.Where(C => C.CreateDate >= chooseDateSt[0].ToDateTime()
                && C.CreateDate <= chooseDateSt[1].ToDateTime());
            StringBuilder sbLose = new StringBuilder();
            //未邀约

            GetWhereParList[0] = new ObjectParameter("State", (int)CustomerStates.DidNotInvite);
            var NotInvite = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);
            var chooseNotYear = NotInvite.Where(C => C.CreateDate >= chooseDateSt[1].ToDateTime() 
                && C.CreateDate <= chooseDateSt[1].ToDateTime());
            StringBuilder sbNot = new StringBuilder();

            //客源量所有数据不包含 年份的参数
            var queryCustomer = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(customerList.ToArray());
            var chooseCustomerYear = queryCustomer.Where(C => C.CreateDate >= chooseDateSt[0].ToDateTime()
                && C.CreateDate <= chooseDateSt[1].ToDateTime());
            StringBuilder sbCustomer = new StringBuilder();
            //当前部门员工的个数
            int currentDepartEmployeeCount = (ViewState["employeeCount"] + string.Empty).ToInt32();


            for (int i = 0; i < currentDepartEmployeeCount; i++)
            {
                HiddenField hfEmployee = rptEmployee.Items[i].FindControl("hfEmployee") as HiddenField;
                int currentEmployeeId = hfEmployee.Value.ToInt32();

                //未邀约
                var singerNot = chooseNotYear.Where(C =>C.CreateEmployee.Value==currentEmployeeId);
                if (singerNot != null)
                {
                    sbNot.AppendFormat("<td>{0}</td>", singerNot.Count());
                }
                else
                {
                    sbNot.AppendFormat("<td>{0}</td>", 0);
                }

                //流失
                var singerLose = chooseLoseYear.Where(C => C.CreateEmployee.Value == currentEmployeeId);
                if (singerLose != null)
                {
                    sbLose.AppendFormat("<td>{0}</td>", singerLose.Count());
                }
                else
                {
                    sbLose.AppendFormat("<td>{0}</td>", 0);
                }

                //正在邀约
                var singerOngoing = chooseOngoingYear.Where(C => C.CreateEmployee.Value == currentEmployeeId);
                if (singerOngoing != null)
                {
                    sbOngoing.AppendFormat("<td>{0}</td>", singerOngoing.Count());
                }
                else
                {
                    sbOngoing.AppendFormat("<td>{0}</td>", 0);
                }

                //有效量
                var singerValid = chooseYearValid.Where(C => C.EmployeeID.Value == currentEmployeeId);
                if (singerValid != null)
                {

                    sbValid.AppendFormat("<td>{0}</td>", singerValid.Count());
                }
                else
                {
                    sbValid.AppendFormat("<td>{0}</td>", 0);
                }
                //邀约成功数
                var singerQuery = chooseYear.Where(C => C.CreateEmployee.Value == currentEmployeeId);
                if (singerQuery != null)
                {
                    sbInviteSuccess.AppendFormat("<td>{0}</td>", singerQuery.Count());
                }
                else
                {
                    sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
                }
                //客源量
                var singerCustomers = chooseCustomerYear.Where(C => C.EmployeeID.Value == currentEmployeeId);
                if (singerCustomers != null)
                {
                    sbCustomer.AppendFormat("<td>{0}</td>", singerCustomers.Count());
                }
                else
                {
                    sbCustomer.AppendFormat("<td>{0}</td>", 0);
                }
                //订单
                var singerOrder = chooseYearOrder.Where(C => C.EmployeeID.Value == currentEmployeeId);
                if (singerOrder != null)
                {
                    sbOrder.AppendFormat("<td>{0}</td>", singerOrder.Count());
                }
                else
                {
                    sbOrder.AppendFormat("<td>{0}</td>", 0);
                }
            }

            #region  未邀约年份统计
            sbNot.AppendFormat("<td>{0}</td>", chooseNotYear.Count());

            //上年未邀约统计
            var preYearNot = NotInvite.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
               && C.CreateDate <= chooseDateSt[3].ToDateTime());
            if (preYearNot != null)
            {
                sbNot.AppendFormat("<td>{0}</td>", preYearNot.Count());
            }
            else
            {
                sbNot.AppendFormat("<td>{0}</td>", 0);
            }
            //所有未邀约统计
            sbNot.AppendFormat("<td>{0}</td>", NotInvite.Count());
            ViewState["sbNot"] = sbNot.ToString();

            #endregion

            #region   流失年份统计
            sbLose.AppendFormat("<td>{0}</td>", chooseLoseYear.Count());

            //上年流失统计
            var preYearLose = LoseInvite.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
               && C.CreateDate <= chooseDateSt[3].ToDateTime());
            if (preYearLose != null)
            {
                sbLose.AppendFormat("<td>{0}</td>", preYearLose.Count());
            }
            else
            {
                sbLose.AppendFormat("<td>{0}</td>", 0);
            }
            //所有流失统计
            sbLose.AppendFormat("<td>{0}</td>", LoseInvite.Count());
            ViewState["sbLose"] = sbLose.ToString();

            #endregion

            #region 未邀约年份统计
            sbOngoing.AppendFormat("<td>{0}</td>", chooseOngoingYear.Count());

            //上年未邀约统计
            var preYearOngoing = OngoingInvite.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
               && C.CreateDate <= chooseDateSt[3].ToDateTime());
            if (preYearOngoing != null)
            {
                sbOngoing.AppendFormat("<td>{0}</td>", preYearOngoing.Count());
            }
            else
            {
                sbOngoing.AppendFormat("<td>{0}</td>", 0);
            }
            //所有未邀约统计
            sbOngoing.AppendFormat("<td>{0}</td>", OngoingInvite.Count());
            ViewState["sbOngoing"] = sbOngoing.ToString();


            #endregion
            #region 有效量年份统计
            //有效量当年合计
            sbValid.AppendFormat("<td>{0}</td>", chooseYearValid.Count());

            //上年有效量统计
            var preYearValid = valideAll.Where(C => C.PlanComeDate >= chooseDateSt[2].ToDateTime()
               && C.PlanComeDate <= chooseDateSt[3].ToDateTime());
            if (preYearValid != null)
            {
                sbValid.AppendFormat("<td>{0}</td>", preYearValid.Count());
            }
            else
            {
                sbValid.AppendFormat("<td>{0}</td>", 0);
            }
            //所有有效量统计
            sbValid.AppendFormat("<td>{0}</td>", valideAll.Count());
            ViewState["sbValid"] = sbValid.ToString();


            #endregion
            #region 订单年份统计
            //订单当年合计
            sbOrder.AppendFormat("<td>{0}</td>", chooseYearOrder.Count());
            //上年订单统计
            var preYearOrder = orderAll.Where(C => C.PlanComeDate >= chooseDateSt[2].ToDateTime()
               && C.PlanComeDate <= chooseDateSt[3].ToDateTime());
            if (preYearOrder != null)
            {
                sbOrder.AppendFormat("<td>{0}</td>", preYearOrder.Count());
            }
            else
            {
                sbOrder.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbOrder.AppendFormat("<td>{0}</td>", orderAll.Count);
            ViewState["sbOrder"] = sbOrder.ToString();

            #endregion
            #region 邀约成功量年份统计
            //邀约成功量当年合计
            sbInviteSuccess.AppendFormat("<td>{0}</td>", chooseYear.Count());
            //上年邀约成功量统计
            var preYearSuccess = DataList.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
                && C.CreateDate <= chooseDateSt[3].ToDateTime());
            if (preYearSuccess != null)
            {
                sbInviteSuccess.AppendFormat("<td>{0}</td>", preYearSuccess.Count());
            }
            else
            {
                sbInviteSuccess.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbInviteSuccess.AppendFormat("<td>{0}</td>", DataList.Count);

            ViewState["sbInviteSuccess"] = sbInviteSuccess.ToString();
            #endregion

            #region 客源量年份统计
            //客源量当年合计
            sbCustomer.AppendFormat("<td>{0}</td>", chooseCustomerYear.Count());
            //上年客源量统计
            var preYearCustomer = queryCustomer.Where(C => C.CreateDate >= chooseDateSt[2].ToDateTime()
                && C.CreateDate <= chooseDateSt[3].ToDateTime());
            if (preYearCustomer != null)
            {
                sbCustomer.AppendFormat("<td>{0}</td>", preYearCustomer.Count());
            }
            else
            {
                sbCustomer.AppendFormat("<td>{0}</td>", 0);
            }
            //所有历史统计
            sbCustomer.AppendFormat("<td>{0}</td>", queryCustomer.Count);

            ViewState["sbCustomer"] = sbCustomer.ToString();
            #endregion
        }
        /// <summary>
        /// 绑定主体数据
        /// </summary>
        protected void LoadTableBody()
        {
            ViewState["TargetInviteSuccess"] = GetTargetDataByTargetName("邀约成功数");
            ViewState["TargetInviteRate"] = GetTargetDataByTargetName("邀约成功率");
            GetFlinish();
        

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
            Sys_Department depart = objDepartmentBLL.GetByAll().Where(C => C.DepartmentName == "邀约部").FirstOrDefault();

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