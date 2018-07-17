using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderProjectfileList : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedBLL = new BLLAssmblly.Flow.QuotedPrice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {
            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            //ObjParList.Add(new ObjectParameter("State_NumOr", ((int)CustomerStates.DidNotFollowOrder).ToString() + "," + ((int)CustomerStates.BeginFollowOrder).ToString() + ",200,201,202,203"));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            ObjParList.Add(new ObjectParameter("IsDelete", false));
            ObjParList.Add(new ObjectParameter("FileCheck", false));
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetOrderCustomerByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, ObjParList);
            CtrPageIndex.RecordCount = SourceCount;

            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }

        /// <summary>
        /// 获取订单总金额
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetQuotedAggregateAmount(object OrderID)
        {
            if (OrderID != null)
            {
                var ObjQuotedModel = ObjQuotedBLL.GetByOrderId(OrderID.ToString().ToInt32());
                if (ObjQuotedModel != null)
                {
                    return ObjQuotedModel.AggregateAmount + string.Empty;
                }
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }




        /// <summary>
        /// 获取订单总金额
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetQuotedEmpLoyee(object OrderID)
        {
            if (OrderID != null)
            {
                var ObjQuotedModel = ObjQuotedBLL.GetByOrderId(OrderID.ToString().ToInt32());
                if (ObjQuotedModel != null)
                {
                    return GetEmployeeName(ObjQuotedModel.EmpLoyeeID) + string.Empty;
                }
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 算出订单总金额 
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumOrderMoneyByCustomerId(List<View_GetOrderCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }
        /// <summary>
        /// 算出定金总额
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumOrderEarnestMoneyByCustomerId(List<View_GetOrderCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetEarnestMoneyByCustomerID(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }


        /// <summary>
        /// 根据客户ID获取订单ID
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public string GetOrderIDByCustomers(object e)
        {
            var ObjCustomer = ObjOrderBLL.GetbyCustomerID(e.ToString().ToInt32());
            if (ObjCustomer != null)
            {
                return ObjCustomer.OrderID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();

        }


        /// <summary>
        /// 分页绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void ddlChooseYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}