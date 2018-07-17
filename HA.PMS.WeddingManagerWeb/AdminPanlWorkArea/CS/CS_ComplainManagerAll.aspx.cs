/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.12
 Description:全部投诉
 History:修改日志

 Author:杨洋
 Date:2013.4.12
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_ComplainManagerAll : SystemPage
    {
        Complain objComplainBLL = new Complain();
        Customers objCustomersBLL = new Customers();
        Order objOrderBLL = new Order();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();

        HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }
        /// <summary>
        /// 如果已经处理的话，则不显示出来  处理投诉这项
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string IsShowExcuteComplainByContent(object source)
        {
            string returnContent = (source + string.Empty);
            if (returnContent != "")
            {
                return "style='display:none;'";
            }
            return string.Empty;
        }
        /// <summary>
        /// 查看处理结果
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string IsShowFindComplainByContent(object source)
        {
            string returnContent = (source + string.Empty);
            if (returnContent != "")
            {
                return string.Empty;

            }
            return "style='display:none;'";
        }
        /// <summary>
        /// 处理状态
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetComplainStateContent(object source)
        {
            string returnContent = (source + string.Empty);
            if (returnContent != "")
            {
                return "处理完毕";
            }
            return "处理中";
        }

        /// <summary>
        /// 返回婚礼顾问
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetAdviserName(object source)
        {
            Employee objEmployeeBLL = new Employee();
            int employeeId = (source + string.Empty).ToInt32();

            Sys_Employee emp = objEmployeeBLL.GetByID(employeeId);
            if (emp != null)
            {
                return emp.EmployeeName;
            }
            return "";
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

            bool? isExcute = null;
            ComplainCustomer cCustomer = new ComplainCustomer();
            cCustomer.Expr2 = false;
            cCustomer.Groom = txtCustomrer.Text.Trim();


            cCustomer.CustomerStatus = ddlCustomerState.SelectedItem.Text;

            //cCustomer.Wineshop = ddlHotel.SelectedItem.Text;

            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

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
            //    ObjParameterList.Add(new ObjectParameter("Wineshop",
            //        cCustomer.Wineshop));
            //}
            if (ddlCustomerState.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add(new ObjectParameter("State", ddlCustomerState.SelectedValue.ToInt32()));
            }
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
            var query = objComplainBLL.GetbyParameter(ObjParameterList.ToArray(),
                ComplainPager.PageSize, ComplainPager.CurrentPageIndex, isExcute, out resourceCount);
            ComplainPager.RecordCount = resourceCount;

            rptComplain.DataSource = query;
            rptComplain.DataBind();


            #endregion

        }
        /// <summary>
        /// 仰止重复客户ID
        /// </summary>
        public class QuotedPriceComparer : IEqualityComparer<FL_QuotedPrice>
        {

            public bool Equals(FL_QuotedPrice x, FL_QuotedPrice y)
            {


                if (Object.ReferenceEquals(x, y)) return true;


                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;


                return x.EmpLoyeeID == y.EmpLoyeeID;
            }



            public int GetHashCode(FL_QuotedPrice c)
            {

                if (Object.ReferenceEquals(c, null)) return 0;


                int hashCustomerID = c.EmpLoyeeID == null ? 0 : c.EmpLoyeeID.GetHashCode();


                //  int hashProductCode = c.Channel.GetHashCode();


                return hashCustomerID;
            }

        }

        /// <summary>
        /// 关于投诉方面的查询
        /// </summary>
        protected void DataComplainBinder()
        {


            //ComplainCustomer cCustomer = new ComplainCustomer();
            //cCustomer.Expr2 = false;
            //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            //cCustomer.Wineshop = ddlHotelSecond.SelectedItem.Text;
            //cCustomer.CustomerID = ddlPlanner.SelectedValue.ToInt32();
            //cCustomer.State = ddlCustomerState.SelectedValue.ToInt32();
            ////酒店
            //if (ddlHotelSecond.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("Wineshop",
            //        cCustomer.Wineshop));
            //}
            
            ////新人状态
            //if (ddlCustomerState.SelectedValue !="-1")
            //{
            //    ObjParameterList.Add(new ObjectParameter("State",
            //        cCustomer.State));
            //}


            ////两种查询时间

            //if (ddlDate.SelectedValue != "0")
            //{
            //    //开始时间
            //    DateTime startTime = new DateTime();
            //    //如果没有选择结束时间就默认是当前时间

            //    string endDateStr = "2100-1-1";
            //    DateTime endTime = endDateStr.ToDateTime();
            //    if (!string.IsNullOrEmpty(txtSecondStarDate.Text))
            //    {
            //        startTime = txtSecondStarDate.Text.ToDateTime();
            //    }
            //    if (!string.IsNullOrEmpty(txtSeondEndDate.Text))
            //    {
            //        endTime = txtSeondEndDate.Text.ToDateTime();
            //    }
            //    ObjParameterList.Add(new ObjectParameter(ddlDate.SelectedValue+"_between", startTime + "," + endTime));
            //}

            ////策划师
            //if (ddlPlanner.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("CustomerID",
            //        cCustomer.CustomerID));
            //}



            /////默认为全部查询
            //bool? isExcute = null;
           
            //if (ddlComplainState.SelectedItem.Text!="请选择")
            //{
            //    if (ddlComplainState.SelectedValue.ToInt32()==0)
            //    {
            //        isExcute = false;
            //    }
            //    else
            //    {
            //        isExcute = true;
            //    }
            //}
           
            #region 分页页码
            //int startIndex = ComplainPager.StartRecordIndex;
            //int resourceCount = 0;
            //var query = objComplainBLL.GetbyParameter(ObjParameterList.ToArray(),
            //    ComplainPager.PageSize, ComplainPager.CurrentPageIndex, isExcute, out resourceCount);
            //ComplainPager.RecordCount = resourceCount;

            //rptComplain.DataSource = query;
            //rptComplain.DataBind();


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
        /// <summary>
        /// 对应的下拉框数据绑定
        /// </summary>
        protected void DataDropDownList()
        {
            //ListItem listItemSinger = new ListItem("请选择", "0");
            ////ddlPlanner.Items.Add(listItemSinger);
            //var query = ObjQuotedPrice.GetByAll().Distinct(new QuotedPriceComparer());
            //foreach (var item in query)
            //{

            //    listItemSinger = new ListItem();
            //    listItemSinger.Text = GetEmployeeName(item.EmpLoyeeID);
            //    listItemSinger.Value = item.CustomerID + string.Empty;
            //    ddlPlanner.Items.Add(listItemSinger);
            //}

            //ddlPlanner.DataBind();


        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void ComplainPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        /// <summary>
        /// 第二排查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQueryResult_Click(object sender, EventArgs e)
        {
            DataComplainBinder();
        }
    }
}