using HA.PMS.Pages;
using System;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class InviteAdminPanel : SystemPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //int employeeId = Convert.ToInt32(User.Identity.Name);

            ////Sys_Employee currentLoginPerson= objEmployeeBLL.GetByID(employeeId);

            ////var objTargetType=objTargetTypeBLL.GetByAll()
            ////    .FirstOrDefault(C => C.DepartmentId == currentLoginPerson.DepartmentID);
            ////////获取当前登陆人所在部门的质量指标
            //////var rateQuery= objRateValueBLL.GetByAll().Where(C =>C.TargetTypeId==objTargetType.TargetTypeId
            //////    && C.CreateTime.Value.Year == DateTime.Now.Year);
            //HiddenField hfUrl = Master.FindControl("hfUrl") as HiddenField;
            //hfUrl.Value =EncodeBase64( "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=");


            ////获取当前登陆人的ID，进行查找的信息
            //List<CA_MyGoalTarget> currentList = objMyGoalTargetBLL.GetByAll()
            //    .Where(C => C.CreateEmployeeId == employeeId).ToList();


            //Literal ltlTargetNames = Master.FindControl("ltlTargetNames") as Literal;

            //var queryTarget = objTargetTypeBLL.GetByEmployeeId(objEmployeeBLL.GetTopManangerByEmployeeId(employeeId));
            //var objTarger = queryTarget.FirstOrDefault();
            ////if (objTarger != null)
            ////{
            ////    ltlTargetNames.Text = objTarger.Goal;
            ////}

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
            //var GetWhereParList = new List<ObjectParameter>();
            //GetWhereParList.Add(new System.Data.Objects.ObjectParameter("EmployeeID", employeeId));
            //GetWhereParList.Add(new ObjectParameter("State_Greaterthan", (int)CustomerStates.InviteSucess + "," + (int)CustomerStates.Sucess + string.Empty));

            
            ////查询当前自己渠道信息。
            //var allCustomers = ObjInvtieBLL.GetInviteCustomerByStateIndex(GetWhereParList);

            //int monthCount = allCustomers.Where(C => C.RecorderDate.Value.Month == dt.Month && C.RecorderDate.Value.Year == dt.Year).Count();
            //ltlMonth.Text =Convert.ToInt32(  monthCount) + "个";
            //int quarterCount = allCustomers.Where(C => C.RecorderDate.Value >= startQuarter && C.RecorderDate.Value <= endQuarter).Count();
            //ltlQuarter.Text =Convert.ToInt32(  quarterCount) + "个";
            //int yearCount = allCustomers.Where(C => C.RecorderDate.Value.Year == dt.Year).Count();
            //ltlYear.Text = Convert.ToInt32( yearCount) + "个";
            //if (currentList.Count > 0)
            //{
            //    var currentMonth = currentList.Where(C => C.CreateTime.Value.Month == dt.Month && C.CreateTime.Value.Year == dt.Year).FirstOrDefault();
            //    //月份
            //    if (currentMonth != null)
            //    {
            //        ltlMonthRate.Text = GetDoubleFormat((double)(monthCount / currentMonth.TargetValue.Value));
            //    }

            //    //季度
            //    var currentQuarter = currentList.Where(C => C.CreateTime.Value >= startQuarter
            //        && C.CreateTime.Value <= endQuarter);
            //    if (currentQuarter.Count() > 0)
            //    {
            //        ltlQuarterRate.Text = GetDoubleFormat((double)(quarterCount /
            //            currentQuarter.Sum(C => C.TargetValue).Value));
            //    }
            //    //本年
            //    var currentYear = currentList.Where(C => C.CreateTime.Value.Year == DateTime.Now.Year);
            //    if (currentYear.Count() > 0)
            //    {
            //        ltlYearRate.Text = GetDoubleFormat((double)(yearCount /
            //            currentYear.Sum(C => C.TargetValue).Value));
            //    }
            //    ltlMonthRate.Text = currentMonth == null ? "0 %" : ltlMonthRate.Text;
            //    ltlQuarterRate.Text = currentQuarter == null ? "0 %" : ltlQuarterRate.Text;
            //    ltlYearRate.Text = currentYear == null ? "0 %" : ltlYearRate.Text;
            //}
        }
    }
}