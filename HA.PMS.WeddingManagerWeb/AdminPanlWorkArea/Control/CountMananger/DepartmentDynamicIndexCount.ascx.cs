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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger
{
    public partial class DepartmentDynamicIndexCount : UserControlTools
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();
        SaleSources objSaleSourcesBLL = new SaleSources();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        TargetType objTargetTypeBLL = new TargetType();
        Employee objEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int DepartmentID = Request.QueryString["DepartmentID"].ToInt32();
                DepartmentID = 77;
                LoadDepartData(DepartmentID);
            }
        }
        /// <summary>
        /// 绑定下属部门
        /// </summary>
        /// <param name="DepartmentID"></param>
        protected void LoadDepartData(int DepartmentID)
        {

            List<Sys_Department> depList = new List<Sys_Department>();

            ViewState["depart"] = DepartmentID;
            depList = objDepartmentBLL.GetbySublevelByDepartmetnID(DepartmentID);

            rptDepart.DataSource = depList;
            rptDepart.DataBind();
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
            string[] chooseDateStr = GetParameterDateTime();

            List<ObjectParameter> objListParameter = new List<ObjectParameter>();
            objListParameter.Add(new ObjectParameter("Goal", targetName));
            objListParameter.Add(new ObjectParameter("CreateTime_between", chooseDateStr[0].ToDateTime() + "," + chooseDateStr[1].ToDateTime()));

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
            objListParameter[1].Value = chooseDateStr[2].ToDateTime() + "," + chooseDateStr[3].ToDateTime();
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


            //上年
            chooseDateStr[2] = chooseDateStar.Year - 1 + "-1-1";
            chooseDateStr[3] = chooseDateStar.Year - 1 + "-12-31";
            return chooseDateStr;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }

        protected void rptDepart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Sys_Department currentDepart = e.Item.DataItem as Sys_Department;
            //部门ID
            int DepartmentID = currentDepart.DepartmentID;
            //当前部门的所有员工
            List<Sys_Employee> thisDepartPerson = objEmployeeBLL.GetByALLDepartmetnID(DepartmentID);


            string[] chooseDateStr = GetParameterDateTime();
            List<ObjectParameter> objListParameter = new List<ObjectParameter>();


            #region 目标计划区域
            var alllQuery = objMyGoalTargetBLL.GetbyParameter(objListParameter.ToArray());
            //当前时段的所有计划目标
            var currentYearQuery = alllQuery.Where(C => C.CreateTime >= chooseDateStr[0].ToDateTime()
            && C.CreateTime <= chooseDateStr[1].ToDateTime()).ToList();

            //当年结果集
            List<View_MyGoalTargetType> listResult = GetList(thisDepartPerson, currentYearQuery);

            #endregion

            StringBuilder sbTarget = new StringBuilder();
            //12个月份，每一个月份
            for (int i = 1; i <= 12; i++)
            {
                var singerQuery = listResult.Where(C => C.CreateTime.Value.Month == i);
                decimal tagetValue = 0;
                if (singerQuery != null)
                {
                    tagetValue = singerQuery.Sum(C => C.TargetValue.Value);
                }
                sbTarget.AppendFormat("<td>{0}</td>", tagetValue);
            }

            //当年合计
            sbTarget.AppendFormat("<td>{0}</td>", listResult.Sum(C => C.TargetValue.Value));

            //上年合计
            var preYearQuery = alllQuery.Where(C => C.CreateTime >= chooseDateStr[2].ToDateTime()
                 && C.CreateTime <= chooseDateStr[3].ToDateTime()).ToList();

            var preResult = GetList(thisDepartPerson, preYearQuery);

            if (preResult != null)
            {
                sbTarget.AppendFormat("<td>{0}</td>", preResult.Sum(C => C.TargetValue.Value));
            }
            else
            {
                sbTarget.AppendFormat("<td>{0}</td>", 0);
            }
            //历史累计
            //移除时间参数，查询对应的部门的所有目标

            var allResult = GetList(thisDepartPerson, alllQuery);
            if (alllQuery != null)
            {
                sbTarget.AppendFormat("<td>{0}</td>", allResult.Sum(C => C.TargetValue.Value));
            }
            else
            {
                sbTarget.AppendFormat("<td>{0}</td>", 0);
            }
            Literal ltlTargetContent = e.Item.FindControl("ltlTargetContent") as Literal;
            ltlTargetContent.Text = sbTarget.ToString();


        }




        /// <summary>
        /// 求出对应的对象交集
        /// </summary>
        /// <param name="thisDepartPerson"></param>
        /// <param name="currentYearQuery"></param>
        /// <returns></returns>
        protected List<View_MyGoalTargetType> GetList(List<Sys_Employee> thisDepartPerson, List<View_MyGoalTargetType> currentYearQuery)
        {
            List<View_MyGoalTargetType> listResult = new List<View_MyGoalTargetType>();
            foreach (var item in thisDepartPerson)
            {
                var singer = currentYearQuery.Where(C => C.CreateEmployeeId.Value == item.EmployeeID);

                if (singer != null)
                {
                    foreach (var Child in singer)
                    {
                        listResult.Add(Child);
                    }

                }
            }
            return listResult;
        }
    }

}