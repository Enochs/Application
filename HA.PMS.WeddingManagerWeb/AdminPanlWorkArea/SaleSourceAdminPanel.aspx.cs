using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class SaleSourceAdminPanel : SystemPage
    {

        Employee objEmployeeBLL = new Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
            //int employeeId = Convert.ToInt32(User.Identity.Name);


            ////获取当前登陆人的ID，进行查找的信息
            //List<CA_MyGoalTarget> currentList= objMyGoalTargetBLL.GetByAll()
            //    .Where(C => C.CreateEmployeeId == employeeId).ToList();

            ////搜索框路径
            //HiddenField hfUrl = Master.FindControl("hfUrl") as HiddenField;
            //hfUrl.Value =EncodeBase64( "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=");

            ////
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
            //List<ObjectParameter> ObjParameter = new List<ObjectParameter>();
            //ObjParameter.Add(new ObjectParameter("EmpLoyeeID", employeeId));
            ////查询当前自己渠道信息。
            //var allCustomers = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(ObjParameter.ToArray()).Where(C=>C.State==1);
            //Literal ltlTargetNames = Master.FindControl("ltlTargetNames") as Literal;

            //var queryTarget = objTargetTypeBLL.GetByEmployeeId(objEmployeeBLL.GetTopManangerByEmployeeId(employeeId));
            //var objTarger = queryTarget.FirstOrDefault();
            //if (objTarger != null)
            //{
            //    ltlTargetNames.Text = objTarger.Goal;
            //}

            //int monthCount = allCustomers.Where(C => C.RecorderDate.Value.Month == dt.Month && C.RecorderDate.Value.Year == dt.Year).Count();
            //ltlMonth.Text = Convert.ToInt32( monthCount) + "个";
            //int quarterCount = allCustomers.Where(C => C.RecorderDate.Value >= startQuarter && C.RecorderDate.Value <= endQuarter).Count();
            //ltlQuarter.Text = Convert.ToInt32( quarterCount) + "个";
            //int yearCount = allCustomers.Where(C => C.RecorderDate.Value.Year == dt.Year).Count();
            //ltlYear.Text = Convert.ToInt32( yearCount )+ "个";
            //if (currentList.Count>0)
            //{
            //    var currentMonth= currentList.Where(C => C.CreateTime.Value.Month == dt.Month&&C.CreateTime.Value.Year==dt.Year).FirstOrDefault();
            //    //月份
            //    if (currentMonth!=null)
            //    {
            //        ltlMonthRate.Text = GetDoubleFormat((double)(monthCount / currentMonth.TargetValue.Value));
            //    }

            //    //季度
            //    var currentQuarter = currentList.Where(C => C.CreateTime.Value >= startQuarter 
            //        && C.CreateTime.Value<=endQuarter);
            //    if (currentQuarter.Count() > 0)
            //    {
            //        ltlQuarterRate.Text = GetDoubleFormat((double)(quarterCount / 
            //            currentQuarter.Sum(C=>C.TargetValue).Value));
            //    }
            //    //本年
            //    var currentYear = currentList.Where(C => C.CreateTime.Value.Year == DateTime.Now.Year);
            //    if (currentYear.Count() > 0)
            //    {
            //        ltlYearRate.Text = GetDoubleFormat((double)(yearCount /
            //            currentYear.Sum(C=>C.TargetValue).Value));
            //    }
            //    ltlMonthRate.Text = currentMonth == null ? "0 %" : ltlMonthRate.Text;
            //    ltlQuarterRate.Text = currentQuarter == null ? "0 %" : ltlQuarterRate.Text;
            //    ltlYearRate.Text = currentYear == null ? "0 %" : ltlYearRate.Text;
            //}
        }

        public void BinderData()
        {
            UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
            var DataList = ObjUserJurisdictionBLL.GetEmPloyeeChannel(User.Identity.Name.ToInt32(), Request["ChannelID"].ToInt32()).Where(C => C.IsMenu == true && C.IsClose == false).OrderBy(C => C.OrderCode);
            var onlyModel = new View_EmpLoyeeChannel();
            if (DataList.Count() > 10)
            {
                onlyModel = DataList.First();
            }
            //不让“渠道管理”和“渠道批量转移”在水平导航选项卡中
            var result = new List<View_EmpLoyeeChannel>();
            if (onlyModel.ChannelID != 0)
            {
                result.Add(onlyModel);
            }
            else
            {
                if (DataList != null && DataList.Count() > 0)
                {
                    foreach (var Item in DataList)
                    {
                        //if (!Item.ChannelAddress.Contains("FD_SaleSourcesManager.aspx") &&!Item.ChannelAddress.Contains("FD_SaleSourcesEmployeeManager"))
                        //{
                        result.Add(Item);
                        // }
                    }
                }
            }
            if (DataList.Count() > 1)
            {
                Navgator.DataSource = result;
                Navgator.DataBind();
                Iframe1.Src = result[0].ChannelAddress + "?Needpopu=1";
            }
            else
            {
                if (result != null)
                {
                    Response.Redirect(result[0].ChannelAddress);
                    Iframe1.Src = result[0].ChannelAddress + "?Needpopu=1";
                }
            }
        }

        /// <summary>
        /// 指示指定员工是否有模块 ID 为 ChannelID 并且没有启用的模块。
        /// </summary>
        /// <param name="EmployeeID">员工 ID。</param>
        /// <param name="ChannelID">模块 ID。</param>
        /// <returns></returns>
        protected bool IsChannelUsing(int EmployeeID, int ChannelID)
        {
            UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
            var ObjUserJurisdictionModel = ObjUserJurisdictionBLL.GetByEmpLoyee(EmployeeID).Where(C => C.ChannelID == ChannelID);
            if (ObjUserJurisdictionModel.Count() > 0)
            {
                if (ObjUserJurisdictionModel.FirstOrDefault().IsClose != null)
                {
                    return !ObjUserJurisdictionModel.FirstOrDefault().IsClose.Value;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 指示指定员工是否有模块地址为 ChannelAddress 并且没有启用的模块。
        /// </summary>
        /// <param name="EmployeeID">员工 ID。</param>
        /// <param name="ChannelID">模块地址。</param>
        /// <returns></returns>
        protected bool IsChannelUsing(int EmployeeID, string ChannelAddress)
        {
            int? ChannelID = GetChannelIDByChannelAddress(ChannelAddress);
            if (ChannelID.HasValue)
            {
                return IsChannelUsing(EmployeeID, ChannelID.Value);
            }
            return false;
        }

        /// <summary>
        /// 返回模块地址为 ChannelAddress 的 模块ID。不存在则返回 null。
        /// </summary>
        /// <param name="ChannelAddress">模块地址。</param>
        /// <returns>模块 ID</returns>
        protected int? GetChannelIDByChannelAddress(string ChannelAddress)
        {
            Channel ObjCannnelBLL = new Channel();
            var CannnelModel = ObjCannnelBLL.GetByAll().Where(C => C.ChannelAddress.Contains(ChannelAddress));
            if (CannnelModel.Count() > 0)
            {
                return CannnelModel.FirstOrDefault().ChannelID;
            }
            else
            {
                return null;
            }
        }


    }
}