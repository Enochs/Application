using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using HHLWedding.BLLAssmbly;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class CustomerEntrance : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();

        Order ObjOrderBLL = new Order();

        Employee ObjEmployeeBLL = new Employee();

        HandleLog ObjHandleBLL = new HandleLog();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtOrderMessage.Text = GetEmployeeName(User.Identity.Name.ToInt32());

                hideOrderEmployee.Value = User.Identity.Name;
            }
        }

        #region 点击保存事件
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            SysConfig ObjConfigBLL = new SysConfig();
            var ConfigModel = ObjConfigBLL.GetByName("PhoneRepeat");
            Button btnSave = (sender as Button);
            btnSave.Enabled = false;
            var Model = ObjCustomersBLL.GetOnlyByPhone(txtCustomerPhone.Text.Trim().ToString());
            if (Model != null)          //这个电话已经存在
            {
                if (ConfigModel.IsClose == false)           //停用  不能重复
                {
                    JavaScriptTools.AlertWindow("客户已存在,请确认客户信息后再次输入", Page);
                    btnSave.Enabled = true;
                    return;
                }
            }
            if (ddlChannelType1.SelectedValue.ToInt32() <= 0)
            {
                JavaScriptTools.AlertWindow("请选择渠道类型", Page);
                btnSave.Enabled = true;
                return;
            }
            if (ddlChannelName2.SelectedValue.ToInt32() <= 0)
            {

                JavaScriptTools.AlertWindow("请选择渠道名称", Page);
                btnSave.Enabled = true;
                return;
            }

            if (txtCustomerPhone.Text != string.Empty)
            {
                int SoutceCounts = 0;
                string OrderByColumnName = "CustomerID";
                FL_Customers ObjCustomersModel = new FL_Customers();
                List<PMSParameters> ObjParList = new List<PMSParameters>();

                ObjParList.Add("BrideCellPhone", txtCustomerPhone.Text, NSqlTypes.StringEquals);
                ObjParList.Add("GroomCellPhone", txtCustomerPhone.Text, NSqlTypes.OR);
                ObjParList.Add("OperatorPhone", txtCustomerPhone.Text, NSqlTypes.OR);
                var SearchList = ObjCustomersBLL.GetDataByWhereParameter(ObjParList, OrderByColumnName, 10, 1, out SoutceCounts);


                if (ConfigModel.IsClose == true || (ConfigModel.IsClose == false && SearchList == null))            //可以重复/不能重复 但是新客户
                {
                    #region 新人基本信息
                    int State = 0;

                    FL_Customers fl_Coustomers = new FL_Customers();

                    fl_Coustomers.Referee = "直接到店";
                    fl_Coustomers.PartyBudget = 0;//婚礼预算
                    fl_Coustomers.IsLose = false;//是否流失

                    fl_Coustomers.ChannelType = 0;
                    fl_Coustomers.Channel = "自己到店";
                    fl_Coustomers.LikeColor = "";
                    fl_Coustomers.ExpectedAtmosphere = "";
                    fl_Coustomers.Hobbies = "";
                    fl_Coustomers.NoTaboos = "";
                    fl_Coustomers.WeddingServices = "";
                    fl_Coustomers.ImportantProcess = "";
                    fl_Coustomers.Experience = "";
                    fl_Coustomers.DesiredAppearance = "";
                    fl_Coustomers.Wineshop = ddlHotel1.SelectedItem.Text;

                    fl_Coustomers.CustomerStatus = CustomerState.GetEnumDescription(CustomerStates.New);
                    fl_Coustomers.FormMarriage = "";
                    fl_Coustomers.IsDelete = false;
                    fl_Coustomers.TimeSpans = txtTimerSpan.Text;
                    fl_Coustomers.Other = txtOther.Text.Trim().ToString();
                    fl_Coustomers.IsVip = rdoIsVIP.SelectedValue.ToInt32() == 0 ? false : true;
                    //记录人
                    fl_Coustomers.Recorder = User.Identity.Name.ToInt32();
                    //记录时间
                    fl_Coustomers.RecorderDate = DateTime.Now;
                    //邀约类型
                    fl_Coustomers.ApplyType = ddlApplyType.SelectedValue.ToInt32();
                    #endregion

                    Report ObjReportBLL = new Report();
                    SS_Report ObjReportModel = new SS_Report();
                    ObjReportModel.Emoney = 0;
                    ObjReportModel.QuotedMoney = 0;
                    ObjReportModel.FirstMoney = 0;

                    #region 联系人  新郎 还是新娘 或者经办人

                    switch (rdoCustomertype.Text)
                    {
                        case "新郎":
                            fl_Coustomers.Groom = txtCustomerName.Text;
                            fl_Coustomers.GroomCellPhone = txtCustomerPhone.Text;
                            ObjReportModel.ContactMan = fl_Coustomers.Groom;
                            ObjReportModel.ContactPhone = fl_Coustomers.GroomCellPhone;
                            ObjReportModel.ContactType = 0;
                            ObjReportModel.State = fl_Coustomers.State;
                            fl_Coustomers.GroomQQ = txtGroomQQ.Text;
                            fl_Coustomers.GroomEmail = txtGroomEmail.Text;
                            fl_Coustomers.Other = txtOther.Text;
                            fl_Coustomers.Rebates = txtRebates.Text;
                            fl_Coustomers.GroomteWeixin = txtGroomteWeixin.Text;
                            fl_Coustomers.GroomWeiBo = txtGroomWeibo.Text;
                            fl_Coustomers.GroomtelPhone = txtGroomtelPhone.Text;
                            fl_Coustomers.GroomBirthday = null;
                            fl_Coustomers.GroomJob = txtGroomJobCompany.Text;
                            fl_Coustomers.GroomJobCompany = txtGroomJobCompany.Text;

                            fl_Coustomers.Operator = string.Empty;
                            fl_Coustomers.OperatorPhone = string.Empty;
                            fl_Coustomers.OperatorRelationship = "";
                            fl_Coustomers.OperatorEmail = string.Empty;
                            fl_Coustomers.OperatorQQ = string.Empty;
                            fl_Coustomers.OperatorWeiBo = string.Empty;
                            fl_Coustomers.OperatorWeiXin = string.Empty;
                            fl_Coustomers.OperatorTelPhone = string.Empty;
                            fl_Coustomers.OperatorCompany = string.Empty;
                            fl_Coustomers.OperatorBrithday = string.Empty;

                            fl_Coustomers.Bride = string.Empty;
                            fl_Coustomers.BrideCellPhone = string.Empty;
                            fl_Coustomers.BrideWeiXin = string.Empty;
                            fl_Coustomers.BrideEmail = string.Empty;
                            fl_Coustomers.BrideQQ = string.Empty;
                            fl_Coustomers.BrideWeiBo = string.Empty;
                            fl_Coustomers.BridePhone = string.Empty;

                            fl_Coustomers.BrideJobCompany = string.Empty;
                            fl_Coustomers.BrideJob = string.Empty;
                            break;
                        case "新娘":

                            fl_Coustomers.Bride = txtCustomerName.Text;
                            fl_Coustomers.BrideCellPhone = txtCustomerPhone.Text;

                            ObjReportModel.ContactMan = fl_Coustomers.Bride;
                            ObjReportModel.ContactPhone = fl_Coustomers.BrideCellPhone;
                            ObjReportModel.ContactType = 1;

                            fl_Coustomers.BrideWeiXin = txtGroomteWeixin.Text;
                            fl_Coustomers.BrideEmail = txtGroomEmail.Text;
                            fl_Coustomers.BrideQQ = txtGroomQQ.Text;
                            fl_Coustomers.BrideWeiBo = txtGroomWeibo.Text;
                            fl_Coustomers.BridePhone = txtGroomtelPhone.Text;
                            if (txtGroomBirthday.Text != string.Empty)
                            {
                                fl_Coustomers.BrideBirthday = txtGroomBirthday.Text.ToDateTime();
                            }
                            fl_Coustomers.BrideJobCompany = txtGroomJobCompany.Text;
                            fl_Coustomers.BrideJob = txtGroomJob.Text;

                            fl_Coustomers.Groom = string.Empty;
                            fl_Coustomers.GroomQQ = string.Empty;
                            fl_Coustomers.GroomEmail = string.Empty;
                            fl_Coustomers.GroomBirthday = null;
                            fl_Coustomers.GroomteWeixin = string.Empty;
                            fl_Coustomers.GroomWeiBo = string.Empty;
                            fl_Coustomers.GroomtelPhone = string.Empty;
                            fl_Coustomers.GroomCellPhone = string.Empty;
                            fl_Coustomers.GroomJob = string.Empty;
                            fl_Coustomers.GroomJobCompany = string.Empty;
                            fl_Coustomers.Other = txtOther.Text;

                            fl_Coustomers.Operator = string.Empty;
                            fl_Coustomers.OperatorPhone = string.Empty;
                            fl_Coustomers.OperatorRelationship = "";
                            fl_Coustomers.OperatorEmail = string.Empty;
                            fl_Coustomers.OperatorQQ = string.Empty;
                            fl_Coustomers.OperatorWeiBo = string.Empty;
                            fl_Coustomers.OperatorWeiXin = string.Empty;
                            fl_Coustomers.OperatorTelPhone = string.Empty;
                            fl_Coustomers.OperatorCompany = string.Empty;
                            fl_Coustomers.OperatorBrithday = string.Empty;
                            break;
                        case "经办人":
                            fl_Coustomers.Operator = txtCustomerName.Text;
                            fl_Coustomers.OperatorPhone = txtCustomerPhone.Text;


                            ObjReportModel.ContactType = 2;
                            ObjReportModel.ContactMan = fl_Coustomers.Operator;
                            ObjReportModel.ContactPhone = fl_Coustomers.OperatorPhone;
                            fl_Coustomers.OperatorRelationship = "";
                            fl_Coustomers.OperatorEmail = txtGroomEmail.Text;
                            fl_Coustomers.OperatorQQ = txtGroomQQ.Text;
                            fl_Coustomers.OperatorWeiBo = txtGroomWeibo.Text;
                            fl_Coustomers.OperatorWeiXin = txtGroomteWeixin.Text;
                            fl_Coustomers.OperatorTelPhone = txtGroomtelPhone.Text;
                            fl_Coustomers.OperatorCompany = txtGroomJobCompany.Text;
                            fl_Coustomers.OperatorBrithday = null;
                            fl_Coustomers.GroomBirthday = null;
                            fl_Coustomers.GroomQQ = string.Empty;
                            fl_Coustomers.GroomEmail = string.Empty;
                            fl_Coustomers.Groom = string.Empty;
                            fl_Coustomers.GroomteWeixin = string.Empty;
                            fl_Coustomers.GroomWeiBo = string.Empty;
                            fl_Coustomers.GroomtelPhone = string.Empty;
                            fl_Coustomers.Other = txtOther.Text;

                            fl_Coustomers.GroomJob = string.Empty;
                            fl_Coustomers.GroomJobCompany = string.Empty;
                            fl_Coustomers.GroomCellPhone = string.Empty;
                            fl_Coustomers.Bride = string.Empty;
                            fl_Coustomers.BrideCellPhone = string.Empty;
                            fl_Coustomers.BrideWeiXin = string.Empty;
                            fl_Coustomers.BrideEmail = string.Empty;
                            fl_Coustomers.BrideQQ = string.Empty;
                            fl_Coustomers.BrideWeiBo = string.Empty;
                            fl_Coustomers.BridePhone = string.Empty;

                            fl_Coustomers.BrideJobCompany = string.Empty;
                            fl_Coustomers.BrideJob = string.Empty;

                            break;
                    }
                    #endregion

                    #region 返点 渠道 录入人

                    fl_Coustomers.TimeSpans = rdotimerSpan.Text;
                    if (txtCustomerPartydate.Text != string.Empty)
                    {
                        fl_Coustomers.PartyDate = txtCustomerPartydate.Text.ToDateTime();
                    }
                    else
                    {
                        fl_Coustomers.PartyDate = "1949-10-1".ToDateTime();
                    }

                    fl_Coustomers.State = (int)CustomerStates.BeginFollowOrder;
                    fl_Coustomers.Channel = ddlChannelName2.SelectedItem.Text;
                    try
                    {
                        fl_Coustomers.Channel = ddlChannelName2.SelectedItem.Text;
                        fl_Coustomers.ChannelType = ddlChannelType1.SelectedValue.ToInt32();


                    }
                    catch
                    {

                    }

                    if (ddlReferee1.SelectedValue.ToInt32() > 0)
                    {
                        fl_Coustomers.Referee = ddlReferee1.SelectedItem.Text;
                    }
                    else
                    {
                        fl_Coustomers.Referee = string.Empty;
                    }

                    fl_Coustomers.RecorderDate = DateTime.Now;
                    fl_Coustomers.CustomersType = ddlCustomersType.SelectedItem.Text;
                    var CustomerID = ObjCustomersBLL.Insert(fl_Coustomers);
                    var ObjustomerUpdateModel = ObjCustomersBLL.GetByID(CustomerID);

                    //返利 渠道 
                    FD_PayNeedRabate ObjPayNeedRabateModel = new FD_PayNeedRabate();
                    SaleSources ObjSaleSourcesBLL = new SaleSources();
                    var ObjSalesalceModel = ObjSaleSourcesBLL.GetByName(fl_Coustomers.Channel);
                    if (ObjSalesalceModel != null)
                    {
                        if (ObjSalesalceModel.NeedRebate.Value)
                        {
                            ObjPayNeedRabateModel.CustomerID = CustomerID;
                            ObjPayNeedRabateModel.PartyDay = fl_Coustomers.PartyDate;
                            ObjPayNeedRabateModel.IsFinish = false;
                            ObjPayNeedRabateModel.IsDelete = false;
                            ObjPayNeedRabateModel.State = fl_Coustomers.State;
                            ObjPayNeedRabateModel.ChannelTypeId = fl_Coustomers.ChannelType;
                            ObjPayNeedRabateModel.SourceID = ObjSalesalceModel.SourceID;
                            ObjPayNeedRabateModel.Paypolicy = ObjSalesalceModel.Rebatepolicy;
                            ObjPayNeedRabateModel.MoneyPerson = ddlReferee1.SelectedItem.Text;

                            ObjPayNeedRabateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                            new PayNeedRabate().Insert(ObjPayNeedRabateModel);
                        }
                    }


                    #endregion

                    #region 渠道数据
                    //渠道数据

                    Telemarketing ObjTelemarketingBLL = new Telemarketing();
                    int employeeId = User.Identity.Name.ToInt32();
                    FL_Telemarketing telemarketing = new FL_Telemarketing();
                    telemarketing.EmployeeID = employeeId;
                    telemarketing.CreateEmpLoyee = employeeId;
                    telemarketing.CustomerID = CustomerID;
                    telemarketing.SortOrder = ObjTelemarketingBLL.GetMaxSortOrder() + 1; //获取最大批次量返回时加 一 更新批次量
                    telemarketing.CreateDate = DateTime.Now;
                    ObjTelemarketingBLL.Insert(telemarketing);
                    #endregion

                    #region 电话邀约
                    if (ddlState.Text == "电话邀约" || ddlState.Text == "未邀约")
                    {
                        int employeeid = User.Identity.Name.ToInt32();
                        FL_Invite ObjInvte = new FL_Invite();
                        ObjInvte.CustomerID = fl_Coustomers.CustomerID;

                        ObjInvte.AgainDate = DateTime.Now;
                        ObjInvte.ComeDate = DateTime.Now;
                        ObjInvte.EmpLoyeeID = employeeid;
                        ObjInvte.CreateEmployee = employeeid;
                        ObjInvte.CreateEmployeeName = GetEmployeeName(employeeid);
                        ObjInvte.CreateDate = DateTime.Now;
                        ObjInvte.LastFollowDate = DateTime.Now;
                        ObjInvte.ToEmpLoyee = User.Identity.Name.ToInt32();
                        ObjInvte.ToDepartMent = new Employee().GetDepartmentID(employeeid);
                        ObjInvte.FllowCount = 0;
                        ObjInvte.ContentID = 0;
                        ObjInvte.OrderEmpLoyeeID = employeeid;
                        ObjInvte.UpdateDate = DateTime.Now;

                        new BLLAssmblly.Flow.Invite().Insert(ObjInvte);

                        if (ddlState.Text == "未邀约")
                        {
                            ObjustomerUpdateModel.State = (int)CustomerStates.DidNotInvite;

                            ObjReportModel.CreateDate = DateTime.Now;
                            ObjReportModel.InviteEmployee = ObjInvte.EmpLoyeeID;

                        }
                        else
                        {
                            ObjustomerUpdateModel.State = (int)CustomerStates.DoInvite; //邀约中

                            ObjReportModel.InviteCreateDate = DateTime.Now;
                            ObjReportModel.InviteEmployee = ObjInvte.EmpLoyeeID;
                        }
                    }

                    #endregion

                    #region 确认到店

                    if (ddlState.Text == "确认到店")
                    {
                        FL_Order ObjOrders = new FL_Order();
                        ObjOrders.CustomerID = fl_Coustomers.CustomerID;
                        ObjOrders.OrderCoder = ObjOrderBLL.ComputeOrderCoder(fl_Coustomers.CustomerID);
                        ObjOrders.FollowSum = 0;
                        ObjOrders.FlowCount = 0;
                        ObjOrders.ProjectDate = DateTime.Now;
                        ObjOrders.ComeDate = DateTime.Now;
                        ObjOrders.CreateDate = DateTime.Now;
                        ObjOrders.EmployeeID = hideOrderEmployee.Value.ToInt32();
                        ObjOrders.EarnestFinish = false;
                        ObjOrderBLL.Insert(ObjOrders);

                        Customers ObjCustomerBLL = new Customers();
                        var ObjUpdateModel = ObjCustomerBLL.GetByID(ObjOrders.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.BeginFollowOrder;//确认到店
                        ObjCustomerBLL.Update(ObjUpdateModel);

                        MissionManager ObjMissManagerBLL = new MissionManager();
                        ObjMissManagerBLL.WeddingMissionCreate(ObjOrders.CustomerID.Value, 1, (int)MissionTypes.Order, DateTime.Now, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjOrders.CustomerID.ToString() + "&OrderID=" + ObjOrders.OrderID + "&FlowOrder=1", MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), ObjOrders.OrderID);

                        ObjReportModel.OrderCreateDate = DateTime.Now;
                        ObjReportModel.InviteSucessDate = DateTime.Now;
                        ObjReportModel.OrderEmployee = ObjOrders.EmployeeID;
                    }
                    #endregion

                    #region 统计 report


                    //进入统计系统
                    ObjReportModel.CreateEmployee = User.Identity.Name.ToInt32();
                    ObjReportModel.DutyEmployee = User.Identity.Name.ToInt32();
                    ObjReportModel.CustomerID = CustomerID;
                    ObjReportModel.CreateDate = DateTime.Now;


                    if (txtCustomerPartydate.Text != string.Empty)
                    {
                        ObjReportModel.Partydate = txtCustomerPartydate.Text.ToDateTime();
                    }
                    else
                    {
                        ObjReportModel.Partydate = "1949-10-1".ToDateTime();
                    }
                    ObjReportModel.TargetState = 0;
                    ObjReportModel.IsFinish = false;
                    if (ObjReportBLL.GetByCustomerID(CustomerID) == null)
                    {
                        ObjReportBLL.Insert(ObjReportModel);
                    }
                    #endregion

                    ObjustomerUpdateModel.Policy = ObjSalesalceModel.Rebatepolicy;
                    ObjCustomersBLL.Update(ObjustomerUpdateModel);

                    CreateHandle();     //操作日志

                    if (ddlState.SelectedItem.Text == "电话邀约" && btnSave.ID == "btnSaveAll")
                    {
                        JavaScriptTools.AlertWindowAndLocation("保存成功！", "Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CustomerID, Page);
                    }
                    else if (ddlState.SelectedItem.Text == "确认到店" && btnSave.ID == "btnSaveAll")
                    {
                        if (ObjOrderBLL.GetbyCustomerID(CustomerID).EmployeeID != User.Identity.Name.ToInt32())
                        {
                            JavaScriptTools.AlertWindow("保存成功,但你不是该客户的婚礼顾问,不能填写沟通记录", this.Page.Request.Url.ToString(), Page);
                        }
                        else
                        {
                            JavaScriptTools.AlertWindowAndLocation("保存成功！", "StoreSales/FollowOrderDetails.aspx?CustomerID=" + CustomerID + "&OrderID=" + ObjOrderBLL.GetbyCustomerID(CustomerID).OrderID + "&FlowOrder=1&State=None", Page);
                        }
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("保存成功！", Page);
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("此客户资料已经存在", Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("新娘姓名，电话不能为空！", Page);
            }
            btnSave.Enabled = true;
        }
        #endregion

        protected void ddlChannelType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelName2.Items.Clear();
            ddlChannelName2.BindByParent(ddlChannelType1.SelectedValue.ToInt32());
        }

        protected void ddlChannelName2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReferee1.BinderbyChannel(ddlChannelName2.SelectedValue.ToInt32());

            ddlReferee1.Visible = true;
            txtRef.Visible = false;
        }

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "客户入口,新建客户" + txtCustomerName.Text.Trim().ToString() + ",状态：" + ddlState.SelectedItem.Text.Trim().ToString();
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 6;     //客户入口
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}