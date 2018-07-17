using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.EditoerLibrary;

//跟单派单
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class StarOrder : SystemPage
    {

        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        Employee objEmployeeBLL = new Employee();
        MissionManager ObjMissManagerBLL = new MissionManager();
        /// <summary>
        /// 邀约操作
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
                BinderEmployees();
            }
        }
        #endregion

        #region 人员下拉框绑定
        /// <summary>
        /// 人员绑定
        /// </summary>
        protected void BinderEmployees()
        {
            ddlEmployee.DataSource = objEmployeeBLL.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32()).Select(C => new { EmpLoyeeID = C.EmployeeID, EmployeeName = C.EmployeeName }).Distinct();

            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmpLoyeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Add(new ListItem(objEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName, objEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeID.ToString()));
            ddlEmployee.Items.Insert(0, new ListItem("", "0"));
            ddlEmployee.ClearSelection();
            ddlEmployee.Items.FindByValue("0").Selected = true;
            ddlCustomerStates.DataBinderOrder();

        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {

            var objParmList = new List<PMSParameters>();

            if (ddlCustomerStates.SelectedValue.ToInt32() >= 0)
            {
                objParmList.Add("State", ddlCustomerStates.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                objParmList.Add("State", ((int)CustomerStates.InviteSucess).ToString() + "," + ((int)CustomerStates.DidNotFollowOrder).ToString() + "," + ((int)CustomerStates.SucessOrder).ToString() + "," + ((int)CustomerStates.BeginFollowOrder).ToString() + ",200,201,202,203,205", NSqlTypes.IN);
            }

            //按跟单人姓名查询
            if (ddlEmployee.SelectedValue.ToInt32() > 0)
            {

                objParmList.Add("EmployeeID", ddlEmployee.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            //else
            //{
            //    objParmList.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            //}

            //按渠道名称查询
            if (DdlChannelName1.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Channel", DdlChannelName1.SelectedItem.Text, NSqlTypes.LIKE);
            }

            //按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //按新人姓名查询
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.LIKE);
            }
            //按各种时间查询
            if (ddlDateRanger.SelectedValue != "请选择")
            {
                if (DateRanger.IsNotBothEmpty)
                {
                    objParmList.Add(ddlDateRanger.SelectedValue, DateRanger.StartoEnd, NSqlTypes.DateBetween);
                }

            }
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "ComeDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
            //var query = ObjInvtieBLL.GetByWhereParameter(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out recordCount, parameters);
            //CtrPageIndex.RecordCount = recordCount;
            //repCustomer.DataBind(query);
        }
        #endregion

        #region 保存分派客户
        /// <summary>
        /// 保存所有分派改派已经成功预约的客户
        /// </summary>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            HiddenField ObjEmpLoyeeHide;
            HiddenField ObjCustomerHide;
            FL_Order ObjOrders = new FL_Order();
            Report ObjReportBLL = new Report();

            for (int i = 0; i < repCustomer.Items.Count; i++)
            {

                ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");

                if (ObjEmpLoyeeHide.Value.ToInt32() != 0)
                {

                    ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");

                    ObjOrders = ObjOrderBLL.GetbyCustomerID(ObjCustomerHide.Value.ToInt32());
                    if (ObjOrders == null)
                    {
                        ObjOrders = new FL_Order();

                        if (ObjOrders.CustomerID != null)
                        {
                            ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(ObjCustomersBLL.GetByID(ObjOrders.CustomerID).PartyDate.Value);
                        }
                        else
                        {
                            ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(DateTime.Now);
                        }
                        // ObjOrders.OrderCoder = ObjOrderBLL.GetOrderCoder(ObjCustomersBLL.GetByID(ObjOrders.CustomerID).PartyDate.Value.ToShortDateString());
                        ObjOrders.FollowSum = 0;
                        ObjOrders.CustomerID = ObjCustomerHide.Value.ToInt32();
                        ObjOrders.EmployeeID = ObjEmpLoyeeHide.Value.ToInt32();
                        ObjOrders.PlanComeDate = ObjInvtieBLL.GetByCustomerID(ObjCustomerHide.Value.ToInt32()).ComeDate;
                        ObjOrders.CreateDate = DateTime.Now;
                        ObjOrders.FlowCount = 0;
                        ObjOrderBLL.Insert(ObjOrders);
                        var ObjUpdateModel = ObjCustomersBLL.GetByID(ObjOrders.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;//未开始跟单
                        ObjCustomersBLL.Update(ObjUpdateModel);

                        ObjMissManagerBLL.WeddingMissionCreate(ObjOrders.CustomerID.Value, 1, (int)MissionTypes.Order, DateTime.Now, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjOrders.CustomerID.ToString() + "&OrderID=" + ObjOrders.OrderID + "&FlowOrder=1", MissionChannel.FL_TelemarketingManager, ObjOrders.EmployeeID.Value, ObjOrders.OrderID);
                        var OjbUpdateReportModel = ObjReportBLL.GetByCustomerID(ObjOrders.CustomerID.Value, ObjOrders.EmployeeID);
                        OjbUpdateReportModel.OrderEmployee = ObjOrders.EmployeeID;
                        ObjReportBLL.Update(OjbUpdateReportModel);
                        //ObjMissManagerBLL.WeddingMissionCreate(1, (int)MissionTypes.Order, DateTime.Now, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjOrders.CustomerID.ToString() + "&OrderID=" + ObjOrders.OrderID + "&FlowOrder=1", MissionChannel.FL_TelemarketingManager, ObjOrders.EmployeeID.Value, ObjOrders.OrderID);
                    }
                    else
                    {
                        var ObjUpdateModel = ObjCustomersBLL.GetByID(ObjOrders.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;//未开始跟单
                        ObjCustomersBLL.Update(ObjUpdateModel);
                        ObjOrders.EmployeeID = ObjEmpLoyeeHide.Value.ToInt32();
                        ObjOrders.FlowCount = 0;
                        ObjOrderBLL.Update(ObjOrders);
                        //CustomerID=" + CustomerID + "&OrderID=" + OrderID+"&FlowOrder=1
                        MissionDetailed ObjMissionDetauledBLL = new MissionDetailed();
                        ObjMissionDetauledBLL.UpdateforFlow((int)MissionTypes.Order, ObjOrders.OrderID, ObjOrders.EmployeeID);

                        var OjbUpdateReportModel = ObjReportBLL.GetByCustomerID(ObjOrders.CustomerID.Value, ObjOrders.EmployeeID);
                        OjbUpdateReportModel.OrderEmployee = ObjOrders.EmployeeID;
                        ObjReportBLL.Update(OjbUpdateReportModel);
                    }
                    //var ObjUpdateModel = ObjCustomersBLL.GetByID(ObjOrders.CustomerID);
                    //ObjUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;//未开始跟单

                    ///添加庆典任务到任务列表
                    //FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();
                    //ObjMissManagerBLL.WeddingMissionCreate(ObjOrders.CustomerID.Value, 1, (int)MissionTypes.Order, DateTime.Now, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjOrders.CustomerID, MissionChannel.StarOrder, ObjOrders.EmployeeID.Value, ObjOrders.OrderID);

                    //ObjCustomersBLL.Update(ObjUpdateModel);
                }
            }
            BinderData();
            JavaScriptTools.AlertWindowAndLocation("保存完毕", Request.Url.ToString(), Page);
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

        #region 派给自己
        /// <summary>
        /// 全部分派给自己
        /// </summary>
        protected void btnSvaeforme_Click(object sender, EventArgs e)
        {
            int EmployeeId = User.Identity.Name.ToInt32();
            SaveForm(EmployeeId);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 条件查询
        /// </summary> 
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 派给其他人
        /// <summary>
        /// 派给其他人
        /// </summary>V
        protected void btnSaveOther_Click(object sender, EventArgs e)
        {
            int EmployeeId = hideEmpLoyeeID.Value.ToInt32();
            SaveForm(EmployeeId);
        }
        #endregion

        #region 改派方法
        /// <summary>
        /// 传入跟单人ID
        /// </summary>
        /// <param name="EmployeeId">跟单人Id</param>
        private void SaveForm(int EmployeeId)
        {
            if (hideKeyList.Value != string.Empty)
            {
                var KeyArry = hideKeyList.Value.Trim(',').Split(',');
                FL_Order ObjOrders = new FL_Order();

                foreach (var ObjItem in KeyArry)
                {
                    FL_Customers ObjCustomersModel = new FL_Customers();

                    ObjCustomersModel = ObjCustomersBLL.GetByID(ObjItem.ToInt32());
                    ObjOrders = ObjOrderBLL.GetbyCustomerID(ObjItem.ToInt32());
                    if (ObjOrders == null)
                    {
                        ObjOrders = new FL_Order();
                        if (ObjOrders.CustomerID != null)
                        {
                            ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(ObjCustomersModel.PartyDate.Value);
                        }
                        else
                        {
                            ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(ObjCustomersModel.PartyDate.Value);
                        }
                        ObjOrders.FollowSum = 0;
                        ObjOrders.FlowCount = 0;
                        ObjOrders.CustomerID = ObjItem.ToInt32();
                        ObjOrders.EmployeeID = EmployeeId;
                        ObjOrders.PlanComeDate = ObjInvtieBLL.GetByCustomerID(ObjItem.ToInt32()).ComeDate;
                        ObjOrders.CreateDate = DateTime.Now;
                        ObjOrderBLL.Insert(ObjOrders);
                        var ObjUpdateModel = ObjCustomersBLL.GetByID(ObjOrders.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;//未开始跟单
                        ObjCustomersBLL.Update(ObjUpdateModel);
                        //添加庆典任务到任务列表


                        ObjMissManagerBLL.WeddingMissionCreate(ObjOrders.CustomerID.Value, 1, (int)MissionTypes.Order, ObjOrders.PlanComeDate.Value, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjOrders.CustomerID, MissionChannel.StarOrder, ObjOrders.EmployeeID.Value, ObjOrders.OrderID);
                    }
                    else
                    {
                        ObjOrders.EmployeeID = EmployeeId;
                        ObjOrders.FlowCount = 0;
                        ObjOrderBLL.Update(ObjOrders);
                        MissionDetailed ObjMissionDetauledBLL = new MissionDetailed();
                        ObjMissionDetauledBLL.UpdateforFlow((int)MissionTypes.Order, ObjOrders.OrderID, ObjOrders.EmployeeID);

                    }

                    var ObjUpdateIntvieModel = ObjInvtieBLL.GetByCustomerID(ObjItem.ToInt32());
                    if (ObjUpdateIntvieModel != null)
                    {
                        ObjUpdateIntvieModel.OrderEmpLoyeeID = ObjOrders.EmployeeID;
                        ObjInvtieBLL.Update(ObjUpdateIntvieModel);
                    }
                }
                BinderData();
                JavaScriptTools.AlertWindow("保存完毕", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("请选择", Page);
            }
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
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
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