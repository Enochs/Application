using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit
{
    public partial class FL_ReturnVisitManagerUpdate : SystemPage
    {
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
        InviteReturnState ObjInviteStateBLL = new InviteReturnState();

        VisitState ObjStateBLL = new VisitState();

        HA.PMS.BLLAssmblly.Flow.Customers ObjCustomerBLL = new HA.PMS.BLLAssmblly.Flow.Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLDataBind();

                var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());

                if (Model != null)
                {
                    if (Model.ReasonPerson == null)
                    {
                        lblReturnPerson.Text = GetEmployeeName(User.Identity.Name.ToInt32());
                    }
                    else
                    {
                        lblReturnPerson.Text = GetEmployeeName(Model.ReasonPerson);
                    }

                    if (Model.ReasonsDate == null)
                    {
                        lblReturnDate.Text = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        lblReturnDate.Text = Model.ReasonsDate.ToString().ToDateTime().ToShortDateString();
                    }
                }

                lblReturnDate.Text = DateTime.Now.ToShortDateString();
                txtEmpLoyee.Text = GetEmployeeName(GetOrderEmployee());

                this.repItemList.DataBind(objCustomerReturnVisitBLL.GetReturnItemByall());
                var DataList = ObjInviteStateBLL.GetByAll();
                if (DataList.Count == 0)
                {
                    btnSaveReturn.Enabled = false;
                }

            }
        }

        #region 单选框绑定
        /// <summary>
        /// 绑定
        /// </summary>
        public void DDLDataBind()
        {
            var DataList = ObjStateBLL.GetByAll();
            rdoState.DataValueField = "ID";
            rdoState.DataTextField = "StatenName";
            rdoState.DataBind(DataList);

            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            if (Model != null)
            {
                if (Model.VisisState != null)       //已经回访过
                {
                    rdoState.Items.FindByValue(Model.VisisState.ToString()).Selected = true;
                }
                else            //未回访 就默认一个
                {
                    rdoState.Items[0].Selected = true;
                }
            }

        }

        #endregion


        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveReturn_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < repItemList.Items.Count; i++)
            {
                objCustomerReturnVisitBLL.Insert(new FL_CustomerReturnVisit()
                {
                    StateItem = (repItemList.Items[i].FindControl("lblTitle") as Label).Text,
                    ReturnDate = DateTime.Now,
                    Source = (repItemList.Items[i].FindControl("rdoState") as RadioButtonList).SelectedItem.Text,
                    SourceNode = (repItemList.Items[i].FindControl("txtSourceNode") as TextBox).Text,
                    ReturnSource = (repItemList.Items[i].FindControl("txtReturnSource") as TextBox).Text,
                    CustomerId = Request["CustomerID"].ToInt32()
                });
            }

            //添加下次到店日期
            FL_CustomerReturnVisit CReturnVisit = new FL_CustomerReturnVisit();
            CReturnVisit.StateItem = lblNextComeDate.Text.ToString();
            CReturnVisit.ReturnDate = DateTime.Now;
            CReturnVisit.Source = txtDate.Text.ToString() == "" ? "" : txtDate.Text.ToString();
            CReturnVisit.SourceNode = txtDateSource.Text.ToString();
            CReturnVisit.ReturnSource = txtDateReturnSource.Text.ToString();
            CReturnVisit.CustomerId = Request["CustomerID"].ToInt32();
            objCustomerReturnVisitBLL.Insert(CReturnVisit);

            //添加建议
            FL_CustomerReturnVisit ObjReturnVisitModel = new FL_CustomerReturnVisit();

            ObjReturnVisitModel.StateItem = lblSuggest.Text.ToString();
            ObjReturnVisitModel.ReturnDate = DateTime.Now;
            ObjReturnVisitModel.Source = txtSuggest.Text.ToString();
            ObjReturnVisitModel.SourceNode = txtSuggestSource.Text.ToString();
            ObjReturnVisitModel.ReturnSource = txtSuggestReturnSource.Text.ToString();
            ObjReturnVisitModel.CustomerId = Request["CustomerID"].ToInt32();

            objCustomerReturnVisitBLL.Insert(ObjReturnVisitModel);

            var ObjUpdateModel = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            ObjUpdateModel.IsReturn = true;
            ObjUpdateModel.ReasonsDate = DateTime.Now;
            ObjUpdateModel.Reasons = txtSuggest.Text.Trim().ToString();
            ObjUpdateModel.ReasonPerson = User.Identity.Name.ToInt32();
            ObjCustomerBLL.Update(ObjUpdateModel);

            //修改状态(跟单/流失/改派)
            ChangeState();
            //添加操作日志
            CreateHandle();
            JavaScriptTools.AlertWindowAndLocation("记录完毕!", "FL_CustomerReturnVisitManagerNot.aspx?NeedPopu=1", Page);
        }

        #endregion

        protected void repItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RadioButtonList rdoState = e.Item.FindControl("rdoState") as RadioButtonList;
            rdoState.DataSource = ObjInviteStateBLL.GetByAll();
            rdoState.DataBind();
        }

        #region 点击派单
        /// <summary>
        /// 单独保存跟单人
        /// </summary>
        protected void lbtnChange_Click(object sender, EventArgs e)
        {
            int EmployeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
            int CustomerID = Request["CustomerID"].ToString().ToInt32();

            Report ObjReportBLL = new Report();
            var ReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());
            if (ReportModel != null)
            {
                ReportModel.OrderEmployee = EmployeeID;
                ObjReportBLL.Update(ReportModel);
            }

            Order ObjOrderBLL = new Order();
            var OrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (OrderModel != null)
            {
                OrderModel.EmployeeID = EmployeeID;
                int result = ObjOrderBLL.Update(OrderModel);
                if (result > 0)
                {
                    ChangeState();
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("修改失败,请稍候再试...", Page);
                }
            }


        }
        #endregion

        #region 获取跟单人
        public string GetOrderEmployee()
        {
            Order ObjOrderBLL = new Order();
            int CustomerID = Request["CustomerID"].ToString().ToInt32();
            var OrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (OrderModel != null)
            {
                int EmployeeID = OrderModel.EmployeeID.ToString().ToInt32();
                return EmployeeID.ToString();
            }
            return "";
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            Customers ObjCustomerBLL = new Customers();
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());

            HandleModel.HandleContent = "新人回访-完成回访,客户姓名:" + Model.Bride + "/" + Model.Groom + ",状态：" + rdoState.SelectedItem.Text + ",跟单人" + txtEmpLoyee.Text.Trim().ToString();


            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 11;     //新人回访
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 修改状态  （跟单/流失/改派）
        public void ChangeState()
        {
            var ObjCustomerModel = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            ObjCustomerModel.VisisState = rdoState.SelectedValue.ToInt32();
            ObjCustomerBLL.Update(ObjCustomerModel);
        }
        #endregion
    }
}