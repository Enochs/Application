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
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class MyComplain : SystemPage
    {
        Complain objComplainBLL = new Complain();
        Customers objCustomersBLL = new Customers();
        HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
        Order objOrderBLL = new Order();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataBinder();
            }
        }
        /// <summary>
        /// 根据客户ID返回策划师名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetQuotedEmpLoyeeNameByCustomers(object source)
        {
            int customerId = (source + string.Empty).ToInt32();
            FL_Order queryOrder = objOrderBLL.GetbyCustomerID(customerId);
            if (queryOrder != null)
            {
                return GetQuotedEmpLoyeeName(queryOrder.OrderID);
            }
            return string.Empty;

        }
        
        /// <summary>
        /// 订单时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetLastFollowDateByCustomerId(object source)
        {
            int customerId = (source + string.Empty).ToInt32();
            FL_Order queryOrder = objOrderBLL.GetbyCustomerID(customerId);
            if (queryOrder != null)
            {

                return GetDateStr(queryOrder.LastFollowDate);
            }
            return string.Empty;

        }


        /// <summary>
        /// 返回策划师
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetProgrammerByCustomerID(object source)
        {
            int customerId = (source + string.Empty).ToInt32();
            return GetPlannerNameByCustomersId(customerId);

        }




        /// <summary>
        /// 返回销售跟单人
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetAdviserByCustomerID(object source)
        {
            int customerId = (source + string.Empty).ToInt32();
            FL_Invite invites = objInviteBLL.GetByCustomerID(customerId);
            if (invites != null)
            {
                return GetEmployeeName(invites.OrderEmpLoyeeID);
            }
            return string.Empty;
        }

        /// <summary>
        /// 相关的数据列表绑定，以及查询条件
        /// </summary>
        protected void DataBinder()
        {
            ComplainCustomer cCustomer = new ComplainCustomer();
            cCustomer.Expr2 = false;

            cCustomer.Groom = txtCustomrer.Text.Trim();


            ////这里设置为处理人ID为0就代表该投诉没有被处理
            ////  cCustomer.ComplainEmployeeId = 0;
            //cCustomer.Wineshop = ddlHotel.SelectedItem.Text;

            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            ObjParameterList.Add(new ObjectParameter("Expr2", cCustomer.Expr2));
            ObjParameterList.Add(new ObjectParameter("ComplainEmployeeId",User.Identity.Name.ToInt32()));
            


            cCustomer.GroomCellPhone = txtCelPhone.Text;
            if (!string.IsNullOrEmpty(txtCelPhone.Text))
            {
                ObjParameterList.Add(new ObjectParameter("BrideCellPhone",
                     cCustomer.GroomCellPhone.Trim()));
            }
            //姓名
            if (!string.IsNullOrEmpty(txtCustomrer.Text))
            {
                ObjParameterList.Add(new ObjectParameter("Bride_LIKE",
                     cCustomer.Groom.Trim()));
            }
            ////酒店
            //if (ddlHotel.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("Wineshop", cCustomer.Wineshop));
            //}


            //开始时间
            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间
            string endDateStr = "2100-1-1";
            DateTime endTime = endDateStr.ToDateTime();
            if (!string.IsNullOrEmpty(txtPartyDateStar.Text))
            {
                startTime = txtPartyDateStar.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtPartyDateEnd.Text))
            {
                endTime = txtPartyDateEnd.Text.ToDateTime();
            }
            ObjParameterList.Add(new ObjectParameter("PartyDate_between", startTime + "," + endTime));

            #region 分页页码
            int startIndex = ComplainPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objComplainBLL.GetbyParameter(ObjParameterList.ToArray(), ComplainPager.PageSize, ComplainPager.CurrentPageIndex, false, out resourceCount);
            ComplainPager.RecordCount = resourceCount;

            rptComplain.DataSource = query;
            rptComplain.DataBind();


            #endregion

        }



        protected void rptComplain_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int ComplainID = e.CommandArgument.ToString().ToInt32();

                CS_Complain cs_Complain = new CS_Complain();
                cs_Complain.ComplainID = ComplainID;
                objComplainBLL.Delete(cs_Complain);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void ComplainPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}