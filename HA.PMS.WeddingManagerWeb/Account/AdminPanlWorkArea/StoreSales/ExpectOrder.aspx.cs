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



        /// <summary>
        /// 保存计划沟通时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            HtmlInputText ObjtxtDate;
            HiddenField ObjCustomerHide;
            FL_Order ObjOrders = new FL_Order();
            for (int i = 0; i < repCustomer.Items.Count; i++)
            {

                //ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");

                ObjtxtDate = (HtmlInputText)repCustomer.Items[i].FindControl("txtDateTime");
                if (ObjtxtDate.Value.Trim() != string.Empty)
                {
                    ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");
                    var UpdateModel = ObjOrderBLL.GetbyCustomerID(ObjCustomerHide.Value.ToInt32());
                    //UpdateModel.NextFlowDate = ObjtxtDate.Value.ToDateTime();
                    //UpdateModel.LastFollowDate = ObjtxtDate.Value.ToDateTime();
                    UpdateModel.PlanComeDate = ObjtxtDate.Value.ToDateTime();
                    UpdateModel.NextFlowDate = ObjtxtDate.Value.ToDateTime();
                    ObjOrderBLL.Update(UpdateModel);

                    var ObjUpdateModel = ObjCustomerBLL.GetByID(UpdateModel.CustomerID);
                    ObjUpdateModel.State = (int)CustomerStates.BeginFollowOrder;//开始跟单
                    ObjCustomerBLL.Update(ObjUpdateModel);
                    MissionDetailed ObjMissionDetauledBLL = new MissionDetailed();
                    ObjMissionDetauledBLL.UpdateforFlow((int)MissionTypes.Order, ObjOrders.OrderID, ObjOrders.EmployeeID);


                    //修改统计目录
                    Report ObjReportBLL = new Report();
                    SS_Report ObjReportModel = new SS_Report();
                    ObjReportModel = ObjReportBLL.GetByCustomerID(UpdateModel.CustomerID.Value, UpdateModel.EmployeeID.Value);
                    ObjReportModel.OrderEmployee = User.Identity.Name.ToInt32();
                    ObjReportModel.OrderCreateDate = DateTime.Now;
                    ObjReportModel.State = ObjUpdateModel.State;
                    ObjReportBLL.Update(ObjReportModel);
                }
                //ObjOrders.OrderCoder = Guid.NewGuid().ToString().Substring(0, 5);
                //ObjOrders.FollowSum = 0;
                //ObjOrders.CustomerID = ObjCustomerHide.Value.ToInt32();
                //ObjOrders.EmployeeID = ObjEmpLoyeeHide.Value.ToInt32();
                //ObjOrderBLL.Insert(ObjOrders);
                //var ObjUpdateModel = ObjCustomerBLL.GetByID(ObjOrders.CustomerID);
                //ObjUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;//未开始跟单
                //ObjCustomerBLL.Update(ObjUpdateModel);
            }
            BinderData();

        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 开始查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnserchforemployee_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var OrderModel = ObjOrderBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            var CustomerModel = ObjCustomerBLL.GetByID(OrderModel.CustomerID);
            CustomerModel.State = (int)CustomerStates.BeginFollowOrder;
            CustomerModel.IsReturn = false;
            ObjCustomerBLL.Update(CustomerModel);


            Report ObjReportBLL = new Report();
            var ObjReportModel = ObjReportBLL.GetByCustomerID(OrderModel.CustomerID.Value, User.Identity.Name.ToInt32());
            ObjReportModel.OrderCreateDate = DateTime.Now;
            ObjReportModel.OrderEmployee = User.Identity.Name.ToInt32();
            ObjReportModel.State = (int)CustomerStates.BeginFollowOrder;

            ObjReportBLL.Update(ObjReportModel);



            Response.Redirect("FollowOrderDetails.aspx?CustomerID=" + CustomerModel.CustomerID + "&OrderID=" + OrderModel.OrderID + "&FlowOrder=1&State=None");
        }

        protected void btnReload1_Click(object sender, EventArgs e)
        {

        }
    }
}