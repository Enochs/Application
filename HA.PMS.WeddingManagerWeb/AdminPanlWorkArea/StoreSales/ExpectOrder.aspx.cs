using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.Web.UI.HtmlControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class ExpectOrder : SystemPage
    {
        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 部门
        /// </summary>
        Department ObjDepartmentBLL = new Department();

        Employee ObjEmployeeBLL = new Employee();

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary> 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                BinderData();
                var ObjEmpLoyeeModel = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());

                if (ObjDepartmentBLL.GetByID(ObjEmpLoyeeModel.DepartmentID).DepartmentManager == ObjEmpLoyeeModel.EmployeeID)
                {
                    hideeIsManager.Value = "0";
                }
                else
                {
                    hideeIsManager.Value = "1";
                }
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add("State", (int)CustomerStates.DidNotFollowOrder + "," + (int)CustomerStates.InviteSucess, NSqlTypes.IN);

            this.MyManager.GetEmployeePar(objParmList);


            //按各种时间查询
            if (ddlDateRanger.SelectedValue != "请选择")
            {
                if (DateRanger.IsNotBothEmpty)
                {
                    objParmList.Add(ddlDateRanger.SelectedValue, DateRanger.StartoEnd, NSqlTypes.DateBetween);
                }
            }
            //按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }

            //按新人姓名(新娘)查询
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.LIKE);
            }

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;



            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();


        }
        #endregion

        #region 分页
        /// <summary>
        /// 上一页/下一页
        /// </summary>  
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 开始查询
        /// </summary>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 确认到店
        /// <summary>
        /// 确认到店
        /// </summary>
        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var now = DateTime.Now;
            var OrderModel = ObjOrderBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            OrderModel.ComeDate = now;
            OrderModel.OrderState = 0;          //0代表经过邀约到店的
            ObjOrderBLL.Update(OrderModel);

            var CustomerModel = ObjCustomerBLL.GetByID(OrderModel.CustomerID);
            CustomerModel.State = (int)CustomerStates.BeginFollowOrder;
            CustomerModel.IsReturn = false;
            ObjCustomerBLL.Update(CustomerModel);


            Report ObjReportBLL = new Report();
            var ObjReportModel = ObjReportBLL.GetByCustomerID(OrderModel.CustomerID.Value, User.Identity.Name.ToInt32());
            ObjReportModel.OrderCreateDate = now;
            ObjReportModel.OrderEmployee = User.Identity.Name.ToInt32();
            ObjReportModel.State = (int)CustomerStates.BeginFollowOrder;

            ObjReportBLL.Update(ObjReportModel);

            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();
            FL_Invite m_invite = ObjInviteBLL.GetByCustomerID(ObjReportModel.CustomerID);
            m_invite.ComeDate = DateTime.Now;

            ObjInviteBLL.Update(m_invite);

            Response.Redirect("FollowUpOrder.aspx");
            //Response.Redirect("FollowOrderDetails.aspx?CustomerID=" + CustomerModel.CustomerID + "&OrderID=" + OrderModel.OrderID + "&FlowOrder=1&State=None");
        }
        #endregion

        #region 会员标志显示
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomerBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion
    }
}