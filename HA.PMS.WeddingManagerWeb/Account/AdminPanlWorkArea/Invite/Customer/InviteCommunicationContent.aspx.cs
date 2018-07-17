
/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.17
 Description:沟通记录创建展示
 **/
//
using HA.PMS.Pages;
using System;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;
using System.Web.UI.WebControls;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class InviteCommunicationContent : SystemPage
    {
        //如果有下级权限就加入跟单
        UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();

        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
        /// <summary>
        /// 邀请主体
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();

        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 更新客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();


        /// <summary>
        /// 更新部门操作
        /// </summary>
        Department ObjDepartmentBLL = new Department();


        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 邀约沟通记录主体
        /// </summary>
        InvtieContent ObjInvtieContentBLL = new InvtieContent();

        #region 页面加载事件


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["OnlyView"] != null)
            {
                plhonlyviewhide.Visible = false;
            }

            if (!IsPostBack)
            {
                hideEmpLoyeeID.Value = User.Identity.Name;
                txtEmpLoyee.Text = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                if (ObjUserJurisdictionBLL.CheckByClassType("ExpectOrder", User.Identity.Name.ToInt32()))
                {


                    // hideDepartmetnKey.Value = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).DepartmentID.ToString();
                }

                // txtCommunicationTime.Text = DateTime.Now.ToShortDateString();
                BinderData();
                ddlLoseContent1.BinderByType(1);


                if (Request["NeedMessAre"] != null)
                {
                    JavaScriptTools.AlertWindow("保存成功!", Page);
                }
                #region 杨洋新添加提供搜索框跳转到当前页面


                int customersId = Request["CustomerID"].ToInt32();
                FL_Customers singerCustomers = ObjCustomersBLL.GetByID(customersId);
                if (singerCustomers.State >= 9)
                {
                    EnabledTextBox(false);      //false  禁用
                }
                else
                {
                    EnabledTextBox(true);       //true  启用
                }



                #endregion
            }
        }
        #endregion

        #region 禁用  启用控件
        protected void EnabledTextBox(bool isValidate)
        {
            foreach (var item in this.plhonlyviewhide.Controls)
            {
                if (item is TextBox)
                {
                    TextBox itemTxt = item as TextBox;
                    itemTxt.Enabled = isValidate;
                }
                if (item is Button)
                {
                    Button itemBtn = item as Button;
                    itemBtn.Enabled = isValidate;
                }
            }
        }
        #endregion

        #region 获取沟通次数
        /// <summary>
        /// 获取沟通次序数
        /// </summary>
        /// <returns></returns>
        public int GetPageSkip(object Index)
        {

            return (CtrPageIndex.RecordCount - CtrPageIndex.RecordCount / (CtrPageIndex.CurrentPageIndex)) + Index.ToString().ToInt32();
        }
        #endregion


        #region 绑定数据方法 BinderData

        /// <summary>
        /// 绑定数据 
        /// </summary>
        private void BinderData()
        {
            int SourceCount = 0;
            //var GetWhereParList = new List<ObjectParameter>();


            //GetWhereParList.Add(new ObjectParameter("CustomerID", Request["CustomerID"].ToInt32()));
            //var query = ObjInvtieContentBLL.GetByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, GetWhereParList);
            var query = ObjInvtieContentBLL.GetByCustomerID(Request["CustomerID"].ToInt32());

            FL_InvtieContent messageInvite = query.FirstOrDefault();
            if (messageInvite != null)
            {
                //存入该用户操作人信息
                ViewState["OperEmployeeId"] = messageInvite.EmpLoyeeID;
            }
            else
            {
                ViewState["OperEmployeeId"] = 0;
            }

            CtrPageIndex.RecordCount = SourceCount;
            this.repContenList.DataSource = query;
            this.repContenList.DataBind();
            //绑定新人资料
            var ObjCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            var ObjIntiveModel = ObjInviteBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            if (ObjIntiveModel == null)
            {
                btnSaveDate.Visible = false;
                return;
            }
            if (Request["Sucess"] != null || ObjIntiveModel.EmpLoyeeID != User.Identity.Name.ToInt32())
            {
                //btnSaveDate.Visible = false;
                //hiddIsSucess.Value = "1";
                //ddlLoseContent1.Enabled = false;
                //ddlInviteSate.Enabled = false;
                //txtOther.Enabled = false;
                //txtContent1.Enabled = false;
            }

            if (Request["OrderID"].ToInt32() > 0)
            {
                btnSaveDate.Visible = true;
                hiddIsSucess.Value = "0";
                ddlLoseContent1.Enabled = true;
                ddlInviteSate.Enabled = true;
                txtOther.Enabled = true;
                txtContent1.Enabled = true;
            }
            txtBride.Text = ObjCustomerModel.Bride;
            txtBrideCellPhone.Text = ObjCustomerModel.BrideCellPhone;
            txtBrideEmail.Text = ObjCustomerModel.BrideEmail;
            txtBrideQQ.Text = ObjCustomerModel.BrideQQ == "0" ? string.Empty : ObjCustomerModel.BrideQQ;
            txtBrideWeiXin.Text = ObjCustomerModel.BrideWeiXin;
            txtGroom.Text = ObjCustomerModel.Groom;
            txtGroomCellPhone.Text = ObjCustomerModel.GroomCellPhone;
            txtGroomEmail.Text = ObjCustomerModel.GroomEmail;
            txtGroomQQ.Text = ObjCustomerModel.GroomQQ == "0" ? string.Empty : ObjCustomerModel.GroomQQ;
            txtGroomteWeixin.Text = ObjCustomerModel.GroomteWeixin;
            txtOperator.Text = ObjCustomerModel.Operator;
            txtOperatorPhone.Text = ObjCustomerModel.OperatorPhone;
            txtOperatorEmail.Text = ObjCustomerModel.OperatorEmail;
            txtOperatorWeiXin.Text = ObjCustomerModel.OperatorWeiXin;
            txtOperatorQQ.Text = ObjCustomerModel.OperatorQQ == "0" ? string.Empty : ObjCustomerModel.OperatorQQ;
            txtGroomWeibo.Text = ObjCustomerModel.GroomWeiBo;
            txtOperatorWeibo.Text = ObjCustomerModel.OperatorWeiBo;
            txtBrideWeibo.Text = ObjCustomerModel.BrideWeiBo;

            txtOther.Text = ObjCustomerModel.Other;
            txtPartyDay.Text = ObjCustomerModel.PartyDate.HasValue && (ObjCustomerModel.PartyDate.Value.Date.CompareTo(DateTime.Parse("2000-01-01")) > 0) ? ObjCustomerModel.PartyDate.Value.ToString("yyyy/MM/dd") : string.Empty;
            ddlTimerSpan.Items.FindByText(ObjCustomerModel.TimeSpans).Selected = true;

            if (ddlHotel.Items.FindByText(ObjCustomerModel.Wineshop) != null)
            {
                ddlHotel.Items.FindByText(ObjCustomerModel.Wineshop).Selected = true;
            }
            lblChannel.Text = ObjCustomerModel.Channel;
            lblChannelType.Text = GetChannelTypeName(ObjCustomerModel.ChannelType);

            Telemarketing ObjTelemarketingsBLL = new BLLAssmblly.Flow.Telemarketing();
            Report ObjReportBLL = new Report();

            //var TelModel = ObjTelemarketingsBLL.GetByCustomersId(ObjCustomerModel.CustomerID);
            var TelModel = ObjReportBLL.GetByCustomerID(ObjCustomerModel.CustomerID, User.Identity.Name.ToInt32());
            if (TelModel != null)
            {
                var ObjIntiveModels = ObjInviteBLL.GetByCustomerID(ObjCustomerModel.CustomerID);
                if (ObjIntiveModels != null)
                {
                    lblCreateEmpLoyee.Text = GetEmployeeName(ObjIntiveModels.CreateEmployee);
                }
                else
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }

            }
            lblCreateDate.Text = GetShortDateString(ObjCustomersBLL.GetByID(ObjCustomerModel.CustomerID).RecorderDate);

            lblReffer.Text = ObjCustomerModel.Referee;

        }

        #endregion

        #region 点击保存
        /// <summary>
        /// 保存沟通记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            if (ddlInviteSate.SelectedValue.ToInt32() == 29 || ddlInviteSate.SelectedValue.ToInt32() == 7)
            {
                FL_Customers ObjCustoerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
                ObjCustoerModel.LoseBeforeState = ObjCustoerModel.State;        //保存该客户流失之前的状态
                ObjCustomersBLL.Update(ObjCustoerModel);


                if (txtLoseContent.Text == string.Empty)
                {
                    JavaScriptTools.AlertWindow("请填写流失原因说明！", Page);
                    return;
                }
            }

            //选择的主管IDddlLoseContent1

            //当前登录员工ID

            int CustomerID = Request["CustomerID"].ToInt32();
            DateTime CommunicationTime = txtCommunicationTime.Text.ToDateTime();
            DateTime ComeDate = txtComeDate.Text.ToDateTime();
            DateTime PlanDate = txtPlandate.Text.ToDateTime();

            //新人基本信息
            FL_Customers ObjCustomersModel = ObjCustomersBLL.GetByID(CustomerID);
            Report ObjReportBLL = new Report();
            SS_Report ObjReportModel = new SS_Report();
            ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());
            ObjReportModel.State = ObjCustomersModel.State;

            if (ObjReportModel.ContactType == 0)
            {
                if (txtGroom.Text.Trim() == string.Empty)
                {
                    JavaScriptTools.AlertWindow("新郎为主要联系人，新郎姓名不能为空", Page);
                    return;
                }
                ObjReportModel.ContactMan = txtGroom.Text;
            }

            if (ObjReportModel.ContactType == 1)
            {
                if (txtBride.Text.Trim() == string.Empty)
                {
                    JavaScriptTools.AlertWindow("新娘为主要联系人，新娘姓名不能为空", Page);
                    return;
                }
                ObjReportModel.ContactMan = txtBride.Text;

            }
            if (ObjReportModel.ContactType == 2)
            {

                if (txtOperator.Text.Trim() == string.Empty)
                {
                    JavaScriptTools.AlertWindow("经办人为主要联系人，经办人姓名不能为空", Page);
                    return;
                }
                ObjReportModel.ContactMan = txtOperator.Text;
            }

            #region 邀约信息
            FL_Invite ObjInviteModel = ObjInviteBLL.GetByCustomerID(CustomerID);
            ObjInviteModel.FllowCount = Convert.ToInt32(ObjInviteModel.FllowCount) + 1;
            ObjInviteModel.AgainDate = PlanDate;
            ObjInviteModel.UpdateDate = DateTime.Now;
            ObjInviteModel.ComeDate = ComeDate;
            ObjInviteModel.LastFollowDate = CommunicationTime;
            ObjInviteModel.ToDepartMent = ObjEmployeeBLL.GetByID(hideEmpLoyeeID.Value.ToInt32()).DepartmentID;
            ObjInviteModel.ToEmpLoyee = hideEmpLoyeeID.Value.ToInt32();
            ObjInviteModel.ContentID = ddlLoseContent1.SelectedValue.ToInt32();

            ObjInviteBLL.Update(ObjInviteModel);
            #endregion

            #region 邀约沟通详细
            FL_InvtieContent ObjInvtieContentModel = new FL_InvtieContent();
            ObjInvtieContentModel.CustomerID = CustomerID;
            ObjInvtieContentModel.CommunicationTime = CommunicationTime.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            ObjInvtieContentModel.CommunicationContent = txtContent1.Text;
            ObjInvtieContentModel.CreateDate = DateTime.Now;
            ObjInvtieContentModel.State = ddlInviteSate.SelectedItem.Text;
            ObjInvtieContentModel.EmpLoyeeID = ObjInviteModel.EmpLoyeeID;
            ObjInvtieContentModel.InviteID = ObjInviteModel.InviteID;
            ObjInvtieContentModel.SortOrder = CtrPageIndex.RecordCount + 1;
            ObjInvtieContentModel.LoseContent = txtLoseContent.Text;
            ObjInvtieContentModel.NextPlanDate = txtPlandate.Text.ToString().ToDateTime();

            ObjInvtieContentBLL.Insert(ObjInvtieContentModel);
            #endregion

            #region 新人基本信息

            if (Request["OrderID"].ToInt32() <= 0)
            {

                ObjCustomersModel.State = ddlInviteSate.SelectedValue.ToInt32();
            }
            ObjCustomersModel.Bride = txtBride.Text;
            ObjCustomersModel.BrideCellPhone = txtBrideCellPhone.Text;
            ObjCustomersModel.BrideEmail = txtBrideEmail.Text;
            ObjCustomersModel.BrideQQ = txtBrideQQ.Text;
            ObjCustomersModel.BrideWeiXin = txtBrideWeiXin.Text;
            ObjCustomersModel.Groom = txtGroom.Text;
            ObjCustomersModel.GroomCellPhone = txtGroomCellPhone.Text;
            ObjCustomersModel.GroomEmail = txtGroomEmail.Text;
            ObjCustomersModel.GroomWeiBo = txtGroomWeibo.Text;
            ObjCustomersModel.BrideWeiBo = txtBrideWeibo.Text;
            ObjCustomersModel.GroomQQ = txtGroomQQ.Text;
            ObjCustomersModel.GroomteWeixin = txtGroomteWeixin.Text;
            ObjCustomersModel.Operator = txtOperator.Text;
            ObjCustomersModel.OperatorPhone = txtOperatorPhone.Text;
            ObjCustomersModel.OperatorPhone = txtOperatorPhone.Text;
            ObjCustomersModel.OperatorEmail = txtOperatorEmail.Text;
            ObjCustomersModel.OperatorWeiBo = txtOperatorWeibo.Text;
            ObjCustomersModel.OperatorWeiXin = txtOperatorWeiXin.Text;
            ObjCustomersModel.OperatorQQ = txtOperatorQQ.Text;
            ObjCustomersModel.Other = txtOther.Text;
            ObjCustomersModel.PartyDate = txtPartyDay.Text.ToDateTime(DateTime.Parse("1949-10-1"));
            ObjCustomersModel.TimeSpans = ddlTimerSpan.SelectedItem.Text;
            ObjCustomersModel.Wineshop = ddlHotel.SelectedItem.Text;

            ObjCustomersBLL.Update(ObjCustomersModel);
            #endregion


            //任务消息
            ObjMissionDetailedBLL.UpdateforFlow((int)MissionTypes.Invite, ObjInviteModel.InviteID, hideEmpLoyeeID.Value.ToInt32());

            #region 跟单基础
            FL_Order ObjOrders = new FL_Order();
            var Model = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (Model == null)
            {
                ObjOrders.CustomerID = CustomerID;
                ObjOrders.FollowSum = 0;
                ObjOrders.EmployeeID = hideEmpLoyeeID.Value.ToInt32();
                ObjOrders.PlanComeDate = ComeDate;
                ObjOrders.CreateDate = DateTime.Now;
                ObjOrders.FlowCount = 0;
                ObjOrders.EarnestFinish = false;
                ObjOrders.EarnestMoney = 0;
                ObjOrders.OrderCoder = ObjOrderBLL.ComputeOrderCoder(ObjInviteModel.CustomerID);
            }

            #endregion

            //操作日志
            CreateHandle();

            switch (ddlInviteSate.SelectedValue.ToInt32())
            {
                //约定再沟通
                case 5:
                    ObjCustomersModel.State = 5;
                    ObjCustomersBLL.Update(ObjCustomersModel);
                    JavaScriptTools.AlertWindowAndLocation("保存成功!", "OngoingInvite.aspx?NeedPopu=1", Page);
                    break;

                //邀约成功
                case 6:
                    if (Request["OrderID"].ToInt32() == 0)
                    {
                        //1.修改新人状态：未开始跟单
                        ObjCustomersModel.State = 6;

                        ObjCustomersBLL.Update(ObjCustomersModel);
                        //2.插入跟单基础
                        ObjOrderBLL.Insert(ObjOrders);
                        //3.任务消息
                        new MissionManager().WeddingMissionCreate(ObjCustomersModel.CustomerID, 1, (int)MissionTypes.Order, ObjOrders.PlanComeDate.Value, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjCustomersModel.CustomerID + "&OrderID=" + ObjOrders.OrderID + "&FlowOrder=1", MissionChannel.FL_TelemarketingManager, ObjOrders.EmployeeID.Value, ObjOrders.OrderID);

                        ObjReportModel.InviteSucessDate = DateTime.Now;
                        ObjReportBLL.Update(ObjReportModel);
                        JavaScriptTools.AlertWindowAndLocation("保存成功!", "SucessInvite.aspx?NeedPopu=1", Page);
                        //Response.Redirect("SucessInvite.aspx?NeedPopu=1");
                    }

                    break;

                //流失
                case 7:
                    if (ObjReportModel != null)
                    {
                        if (ddlLoseContent1.SelectedValue.ToInt32() == -3)
                        {
                            ObjReportModel.ISeffective = false;

                            ObjReportBLL.Update(ObjReportModel);
                        }
                    }
                    ObjCustomersModel.State = 7;

                    ObjCustomersBLL.Update(ObjCustomersModel);

                    ObjReportModel.InviteLoseDate = DateTime.Now;
                    ObjReportBLL.Update(ObjReportModel);
                    JavaScriptTools.AlertWindowAndLocation("保存成功!", "LoseInvite.aspx?NeedPopu=1", Page);
                    break;

                //无效信息
                case 300: break;
                default: JavaScriptTools.AlertWindowAndGoLocationHref("无效的选项!", Page); break;
            }

        }
        #endregion

        #region 翻页
        /// <summary>
        /// 绑定沟通记录
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            string CustomerName = txtBride.Text.Trim().ToString() == string.Empty ? txtGroom.Text.Trim().ToString() : txtBride.Text.Trim().ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "电话邀约,客户姓名:" + CustomerName + ",修改状态：" + ddlInviteSate.SelectedItem.Text + "(邀约)";
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 1;     //电话邀约
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}