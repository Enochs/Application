using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class QuotedPriceWorkPanel : SystemPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //int employeeId = Convert.ToInt32(User.Identity.Name);
            //HiddenField hfUrl = Master.FindControl("hfUrl") as HiddenField;
            //hfUrl.Value = EncodeBase64("/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=");

            ////获取当前登陆人的ID，进行查找的信息
            //List<CA_MyGoalTarget> currentList = objMyGoalTargetBLL.GetByAll()
            //    .Where(C => C.CreateEmployeeId == employeeId).ToList();
            

            //DateTime dt = DateTime.Now;
            //DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);   //本 季度初 
            //DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);   //本 季度末 
            //startQuarter = new DateTime(startQuarter.Year, startQuarter.Month, startQuarter.Day);
            //endQuarter = new DateTime(endQuarter.Year, endQuarter.Month, endQuarter.Day, 23, 59, 59);
            //Literal ltlMonth = Master.FindControl("ltlMonth") as Literal;
            //Literal ltlQuarter = Master.FindControl("ltlQuarter") as Literal;
            //Literal ltlYear = Master.FindControl("ltlYear") as Literal;
            //Literal ltlMonthRate = Master.FindControl("ltlMonthRate") as Literal;
            //Literal ltlQuarterRate = Master.FindControl("ltlQuarterRate") as Literal;
            //Literal ltlYearRate = Master.FindControl("ltlYearRate") as Literal;
            //Literal ltlTargetNames = Master.FindControl("ltlTargetNames") as Literal;
 
            //var queryTarget = objTargetTypeBLL.GetByEmployeeId(objEmployeeBLL.GetTopManangerByEmployeeId(employeeId));
            //var objTarger = queryTarget.FirstOrDefault();
            ////if (objTarger != null)
            ////{
            ////    ltlTargetNames.Text = objTarger.Goal;
            ////}
            //List<ObjectParameter> objParameterList = new List<ObjectParameter>();
            //objParameterList.Add(new ObjectParameter("IsDispatching", 0));

            //objParameterList.Add(new ObjectParameter("EmpLoyeeID", employeeId));

            //var objResult = objQuotedPriceBLL.GetCustomerQuotedParameter(objParameterList);

            ////当月实际完成的工作任务
            //var currentValidateMonth = objResult.Where(C => C.PartyDate.Value.Month == dt.Month);
            ////当季度实际完成的工作任务
            //var currentValidateQuarter = objResult.Where(C => C.PartyDate.Value >= startQuarter && C.PartyDate <= endQuarter);
            ////当年实际完成的工作任务
            //var currentValidateYear = objResult.Where(C => C.PartyDate.Value.Year == dt.Year);
           
            //ltlMonth.Text = currentValidateMonth.Sum(C => C.AggregateAmount) + string.Empty;

            //ltlQuarter.Text = currentValidateQuarter.Sum(C => C.AggregateAmount) + string.Empty;

            //ltlYear.Text = currentValidateYear.Sum(C => C.AggregateAmount) + string.Empty;

            //if (currentList.Count > 0)
            //{
            //    var currentMonth = currentList.Where(C => C.CreateTime.Value.Month == dt.Month && C.CreateTime.Value.Year == dt.Year).FirstOrDefault();
            //    //月份
            //    if (currentMonth != null)
            //    {
            //        ltlMonthRate.Text = GetDoubleFormat((double)(ltlMonth.Text.ToInt32() / currentMonth.TargetValue.Value));
            //    }

            //    //季度
            //    var currentQuarter = currentList.Where(C => C.CreateTime.Value >= startQuarter
            //        && C.CreateTime.Value <= endQuarter);
            //    if (currentQuarter.Count() > 0)
            //    {
            //        ltlQuarterRate.Text = GetDoubleFormat((double)(ltlQuarter.Text.ToInt32() /
            //            currentQuarter.Sum(C => C.TargetValue).Value));
            //    }
            //    //本年
            //    var currentYear = currentList.Where(C => C.CreateTime.Value.Year == DateTime.Now.Year);
            //    if (currentYear.Count() > 0)
            //    {
            //        ltlYearRate.Text = GetDoubleFormat((double)(ltlYear.Text.ToInt32() /
            //            currentYear.Sum(C => C.TargetValue).Value));
            //    }

            //    ltlMonthRate.Text = currentMonth == null ? "0 %" : ltlMonthRate.Text;
            //    ltlQuarterRate.Text = currentQuarter == null ? "0 %" : ltlQuarterRate.Text;
            //    ltlYearRate.Text = currentYear == null ? "0 %" : ltlYearRate.Text;
            //}
        }

        private decimal GetAggregateAmountBy(List<View_GetOrderCustomers> source)
        {
            decimal sum = 0;
            foreach (var item in source)
            {
                string result = GetAggregateAmount(item.CustomerID);
                if (result == "")
                {
                    sum += 0;
                }
                else
                {
                    sum += Convert.ToDecimal(result);
                }

            }
            return sum;
        }
    }
}