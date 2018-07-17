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
    public partial class CommandByCustomerManager : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        Department objDepartmentBLL = new Department();

        Dispatching objDispatchingBLL = new Dispatching();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Employee objEmployeeBLL = new Employee();
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
        Complain objComplainBLL = new Complain();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {
            string formatStr = "<td>{0}</td>";

            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            var GetWhereParList = new List<ObjectParameter>();

            //投诉参数
            var GetWhereSuccessParList = new List<ObjectParameter>();
            #region
            //所有投诉记录
            var allCompalin = objComplainBLL.GetByAll();

            //根据当前选择时间返回对应投诉内容
            var currentComplain = allCompalin.Where(C => C.ComplainDate >= chooseDateStar && C.ComplainDate <= chooseDateEnd);
            //对应处理人员的ID
            var currentEmployeeList = currentComplain.Select(C => C.ComplainEmployeeId.Value);

            var resultEmployeeId = new List<int>();
            if (ddlDepartment.Items.Count > 0)
            {
                if (ddlDepartment.SelectedItem.Text != "请选择")
                {

                    var emplistAll = objEmployeeBLL.GetByALLDepartmetnID(ddlDepartment.SelectedValue.ToInt32())
                     .Select(C => C.EmployeeID);
                    resultEmployeeId = (from m in emplistAll select m).Intersect((from m in currentEmployeeList select m)).ToList();
                        
                }
            }
            //获取根据查询条件之后的执行中的订单
            var resultList = new List<CS_Complain>();
            foreach (var item in currentComplain)
            {
                var current = resultEmployeeId.Where(C => C == item.ComplainEmployeeId.Value).FirstOrDefault();
                if (current != 0)
                {
                    resultList.Add(item);
                }
            }
            StringBuilder sbComplain = new StringBuilder();

            #endregion

            #region 客户记录
            var customerAll = objCustomersBLL.GetByAll();
            //当年的客户
            var currentCustomer = customerAll.Where(C => C.State >= 6 && (C.PartyDate >= chooseDateStar
                && C.PartyDate <= chooseDateEnd));

            //上年的客户
            var preCustomer = customerAll.Where(C => C.PartyDate >= chooseDateStar.AddYears(-1)
                && C.PartyDate <= chooseDateEnd.AddYears(-1)).ToList();

            //投诉率
            StringBuilder sbComplainRate = new StringBuilder();
            #endregion


            #region 满意度
            var degreeAll=objDegreeOfSatisfactionBLL.GetByAll();
            //DofDate
            //当年的满意度
            var currentDegree = degreeAll.Where(C=>C.DofDate >= chooseDateStar
              && C.DofDate <= chooseDateEnd);
            //上年的满意度
            var preDegree = degreeAll.Where(C => C.DofDate >= chooseDateStar.AddYears(-1)
                && C.DofDate <= chooseDateEnd.AddYears(-1)).ToList();
            StringBuilder sbDegree = new StringBuilder();
            //满意度率
            StringBuilder sbDegreeRate = new StringBuilder();
            #endregion

            #region 回访
            var allReturnVisit = objCustomerReturnVisitBLL.GetByAll();
            var currentReturn = allReturnVisit.Where(C => C.ReturnDate >= chooseDateStar
             && C.ReturnDate <= chooseDateEnd).ToList();

            var preReturn = allReturnVisit.Where(C => C.ReturnDate >= chooseDateStar.AddYears(-1)
                && C.ReturnDate <= chooseDateEnd.AddYears(-1)).ToList();
            StringBuilder sbReturn = new StringBuilder();
            //回访率
            StringBuilder sbReturnRate = new StringBuilder();

            //回访满意率
            StringBuilder sbReturnDegreeRate = new StringBuilder();

            #endregion
            for (int i = 1; i <= 12; i++)
            {
                //单月份的投诉
                var singerComplain = currentComplain.Where(C => C.ComplainDate.Value.Month == i);
                //这个月的客户
                var singerCustomer = currentCustomer.Where(C => C.PartyDate.Value.Month == i).ToList();
                var singerDegree = currentDegree.Where(C => C.DofDate.Value.Month == i);
                List<int> singerDegreeCustomerID = singerDegree.Select(C => C.CustomerID.Value).ToList();
                var singerReturn = currentReturn.Where(C => C.ReturnDate.Month == i).ToList();
                //回访和满意度的 客户ID交集
                var differenceCustomer = (from m in singerDegreeCustomerID select m).ToList()
                    .Intersect(from m in singerReturn select m.CustomerId).ToList();

                sbReturnDegreeRate.AppendFormat(formatStr,GetDecimal(differenceCustomer.Count,singerCustomer.Count()));

                sbComplain.AppendFormat(formatStr, singerComplain.Count());
                sbComplainRate.AppendFormat(formatStr, GetDecimal(singerComplain.Count(), singerCustomer.Count()));
                sbDegree.AppendFormat(formatStr, 0);//singerDegree.Sum(C=>C.SumDof));
                sbDegreeRate.AppendFormat(formatStr,GetDecimal( singerDegree.Count(),singerCustomer.Count()));
                sbReturn.AppendFormat(formatStr, singerReturn.Count());

                sbReturnRate.AppendFormat(formatStr, GetDecimal(singerReturn.Count(), singerCustomer.Count()));
            }
            #region 投诉年份统计
            //当年所有的统计
            sbComplain.AppendFormat(formatStr, currentComplain.Count());

            sbComplainRate.AppendFormat(formatStr, GetDecimal(currentComplain.Count(), currentCustomer.Count()));
            sbDegree.AppendFormat(formatStr, 0);
            sbDegreeRate.AppendFormat(formatStr, GetDecimal(currentDegree.Count(), currentCustomer.Count()));
            sbReturn.AppendFormat(formatStr, currentReturn.Count());
            sbReturnRate.AppendFormat(formatStr, GetDecimal(currentReturn.Count(), currentCustomer.Count()));
            //当期满意度客户ID交集
             List<int> currentDegreeCustomerID = currentDegree.Select(C => C.CustomerID.Value).ToList();
             var differenceCustomers = (from m in currentDegreeCustomerID select m).ToList()
                     .Intersect(from m in currentReturn select m.CustomerId).ToList();

             sbReturnDegreeRate.AppendFormat(formatStr, GetDecimal(differenceCustomers.Count, currentCustomer.Count()));

            //上年统计数 chooseDateStar.AddYears(-1) + "," + chooseDateEnd.AddYears(-1);
            var preComplain = allCompalin.Where(C => C.ComplainDate >= chooseDateStar.AddYears(-1)
                && C.ComplainDate <= chooseDateEnd.AddYears(-1)).ToList();

            sbComplain.AppendFormat(formatStr, preComplain.Count);
            sbComplainRate.AppendFormat(formatStr, GetDecimal(preComplain.Count, preCustomer.Count));
            sbDegree.AppendFormat(formatStr,0);
            sbDegreeRate.AppendFormat(formatStr, GetDecimal(preDegree.Count(), preCustomer.Count()));
          
            sbReturn.AppendFormat(formatStr, preReturn.Count());
            sbReturnRate.AppendFormat(formatStr, GetDecimal(preReturn.Count(), preCustomer.Count()));

            //上年满意度客户ID交集
            List<int> preDegreeCustomerID = preDegree.Select(C => C.CustomerID.Value).ToList();

            var differencePreCustomers = (from m in preDegreeCustomerID select m).ToList()
                    .Intersect(from m in preReturn select m.CustomerId).ToList();
            sbReturnDegreeRate.AppendFormat(formatStr, GetDecimal(differencePreCustomers.Count, preCustomer.Count()));
            //所有统计数
            sbComplain.AppendFormat(formatStr, allCompalin.Count);
            ViewState["sbComplain"] = sbComplain.ToString();

            sbComplainRate.AppendFormat(formatStr, GetDecimal(allCompalin.Count, customerAll.Count));

            ViewState["sbComplainRate"] = sbComplainRate.ToString();


            sbDegree.AppendFormat(formatStr, 0);

            ViewState["sbDegree"] = sbDegree.ToString();

            sbDegreeRate.AppendFormat(formatStr, GetDecimal(degreeAll.Count(), customerAll.Count()));

            ViewState["sbDegreeRate"] = sbDegreeRate.ToString();

            sbReturn.AppendFormat(formatStr, allReturnVisit.Count());

            ViewState["sbReturn"] = sbReturn.ToString();

            sbReturnRate.AppendFormat(formatStr, GetDecimal(allReturnVisit.Count(), customerAll.Count()));

            ViewState["sbReturnRate"] = sbReturnRate.ToString();

            //所有满意度客户ID交集
            List<int> allDegreeCustomerID = degreeAll.Select(C => C.CustomerID.Value).ToList();

            var differenceAllCustomers = (from m in allDegreeCustomerID select m).ToList()
                    .Intersect(from m in allReturnVisit select m.CustomerId).ToList();
            sbReturnDegreeRate.AppendFormat(formatStr, GetDecimal(differenceAllCustomers.Count, customerAll.Count()));

            ViewState["sbReturnDegreeRate"] = sbReturnDegreeRate.ToString();
            #endregion
        }
        protected decimal GetDecimal(int number1, int number2)
        {
            if (number2 == 0 || number1 == 0)
            {
                return 0;
            }

            return Math.Round(Convert.ToDecimal(number1 / number2), 2);
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}