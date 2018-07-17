using System;
using System.Collections.Generic;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit
{
    public partial class QuotedPriceMissionList : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["QuotedID"].ToInt32() > 0)
                {
                    new MissionDetailed().UpdateforFlow((int)MissionTypes.Quoted, Request["QuotedID"].ToInt32(), User.Identity.Name.ToInt32());
                }
                BinderData(sender, e);
            }
        }

        /// <summary>
        /// 获取预算
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderMoney(object CustomerID)
        {
            FL_Order fl_Order = new Order().GetbyCustomerID((CustomerID + string.Empty).ToInt32());
            return object.ReferenceEquals(fl_Order, null) ? string.Empty : fl_Order.EarnestMoney + string.Empty;
        }

        /// <summary>
        /// 获取定金
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetPartyBudget(object CustomerID)
        {
            FL_Customers fL_Customers = new Customers().GetByID((CustomerID + string.Empty).ToInt32());
            return object.ReferenceEquals(fL_Customers, null) ? string.Empty : fL_Customers.PartyBudget.ToString();
        }

        /// <summary>
        /// 获取邀约负责人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderEmployee(object CustomerID)
        {
            FL_Order fl_Order = new Order().GetbyCustomerID((CustomerID + string.Empty).ToInt32());
            if (!object.ReferenceEquals(fl_Order, null))
            {
                Sys_Employee sys_Employee = new Employee().GetByID(fl_Order.EmployeeID);
                return object.ReferenceEquals(sys_Employee, null) ? string.Empty : sys_Employee.EmployeeName;
            }
            return string.Empty;
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> objParmList = new List<PMSParameters>();
            int RequestTyper = Request["ManagerTyper"].ToInt32();
            
            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
 
            objParmList.Add("PartyDate", PartyDateRanger.Start + "," + PartyDateRanger.End, NSqlTypes.DateBetween);
            if (ddlHotel.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);
            }
            objParmList.Add("Expr1", false, NSqlTypes.Equal);

            objParmList.Add("FinishOver", null, NSqlTypes.IsNull);
            switch (RequestTyper)
            { 
                case 1://销售
                    objParmList.Add("EmpLoyeeID",User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    break;
                case 2://婚礼管家
                    objParmList.Add("MissionManagerEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    break;
                case 3:
                    break;
                default:
                    break;
            }


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.QuotedPrice().GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
 
        }
    }
}