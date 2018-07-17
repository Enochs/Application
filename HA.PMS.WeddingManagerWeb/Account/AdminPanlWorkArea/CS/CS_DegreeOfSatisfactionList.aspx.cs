using System;
using System.Collections.Generic;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.CS;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_DegreeOfSatisfactionList : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        /// <summary>
        /// 获取到店时间
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DateTime? GetComeDate(object CustomerID)
        {
           DataAssmblly.FL_Invite ObjInviteModel = new BLLAssmblly.Flow.Invite().GetByCustomerID((CustomerID + string.Empty).ToInt32());
            return object.ReferenceEquals(ObjInviteModel, null) ? null : ObjInviteModel.ComeDate;
        }

        /// <summary>
        /// 获取预定时间
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public DateTime? GetOrderDate(object CustomerID)
        {
            DataAssmblly.FL_Order ObjOrderModel = new BLLAssmblly.Flow.Order().GetbyCustomerID(CustomerID.ToString().ToInt32());
            return object.ReferenceEquals(ObjOrderModel, null) ? null : ObjOrderModel.LastFollowDate;
        }

        protected void BinderData(object sender, EventArgs e)
        {

            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add("CdState", 2);
            //新人姓名
          //  CstmNameSelector.AppandTo(paramsList);
            //婚期
            paramsList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate_between", PartyDateRanger.Start, PartyDateRanger.End);
            //酒店
            paramsList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop_LIKE", ddlHotel.SelectedItem.Text.Trim());

            List<int> customerIDs = new List<int>();
            List<int> employeeIDs = new List<int>() { User.Identity.Name.ToInt32() };

            switch (Request["Typer"].ToInt32())
            {
                //邀约.责任人
                case 1: customerIDs = new BLLAssmblly.Flow.Invite().Where(C => employeeIDs.Contains(C.CreateEmployee.Value)).Select(C => C.CustomerID).Distinct().ToList(); break;
                //跟单.责任人
                case 2: customerIDs = new BLLAssmblly.Flow.Invite().Where(C => employeeIDs.Contains(C.EmpLoyeeID.Value)).Select(C => C.CustomerID).Distinct().ToList(); break;
                //策划报价.责任人
                case 3: customerIDs = new BLLAssmblly.Flow.Order().Where(C => employeeIDs.Contains(C.EmployeeID.Value)).Select(C => C.CustomerID.Value).Distinct().ToList(); break;
                //订单.责任人
                case 4: customerIDs = new BLLAssmblly.Flow.QuotedPrice().GetByAll().Where(C => employeeIDs.Contains(C.EmpLoyeeID.Value)).Select(C => C.CustomerID.Value).Distinct().ToList(); break;
            }

            int startIndex = DegreePager.StartRecordIndex;
            int resourceCount = 0;
            var query = new DegreeOfSatisfaction().GetDefrreSatisaction(DegreePager.PageSize, DegreePager.CurrentPageIndex, out resourceCount, paramsList.ToArray(), C => customerIDs.Contains(C.CustomerID));
            DegreePager.RecordCount = resourceCount;
            rptDegree.DataBind(query);
        }
    }
}