using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.SysTarget;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class FollowOrderDetails : SystemPage
    {

        //策划报价
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //收款记录
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();

        //沟通记录(跟单)
        OrderDetails ObjOrderDetailsBLL = new OrderDetails();


        //客户
        Customers ObjCustomerBLL = new Customers();

        //订单
        Order ObjOrderBLL = new Order();

        //数据统计
        Report ObjReportBLL = new Report();

        //内部员工
        Employee ObjEmployeeBLL = new Employee();

        //头部报表
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Target ObjTargetBLL = new Target();

        DetailsQuotedPrice ObjDetailsBLL = new DetailsQuotedPrice();
        int OrderID = 0;

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["OnlyView"] != null)
            {
                divonlyviewhide.Visible = false;
                //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OnlyViewScript", "$('input,textarea,select').attr('disabled','disabled');", true);

            }

            if (Request["OrderID"].ToInt32() > 0)
            {
                OrderID = Request["OrderID"].ToInt32();
                MissionDetailed ObjMissionDetauledBLL = new MissionDetailed();
                ObjMissionDetauledBLL.UpdateforFlow((int)MissionTypes.Order, OrderID, User.Identity.Name.ToInt32());

                //divQuotedPanel.Visible = false;
                //divInviteContent.Visible = false;
                repContenList.Visible = false;
                RepQuoteContentList.Visible = false;
            }
            else
            {
                //如果是查看信息，并且没有跟单，就显示邀约信息。
                if (Request["OnlyView"] != null && ObjOrderBLL.GetbyCustomerID(Request["CustomerID"].ToInt32()) == null)
                {
                    Response.Redirect("~/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + Request["CustomerID"] + "&OnlyView=1");
                    return;
                }
                OrderID = ObjOrderBLL.GetbyCustomerID(Request["CustomerID"].ToInt32()).OrderID;
                MissionDetailed ObjMissionDetauledBLL = new MissionDetailed();
                ObjMissionDetauledBLL.UpdateforFlow((int)MissionTypes.Order, OrderID, User.Identity.Name.ToInt32());
            }

            int CustomerID = Request["CustomerID"].ToInt32();
            var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (Model == null)          //策划报价为空就隐藏 
            {
                divQuotedPanel.Visible = false;
            }
            else if (Model != null)   //策划报价不为空 可以做策划沟通
            {
                divQuotedPanel.Visible = true;
            }


            if (!IsPostBack)
            {
                DDLDataBinder();
                BinderData(sender, e);
                hideEmpLoyeeID.Value = User.Identity.Name;
                txtEmpLoyee.Text = GetEmployeeName(User.Identity.Name.ToInt32());

                ddlLoseContent.BinderByType(2);

                if (Request["Sucess"] != null)
                {
                    btnSaveChange.Visible = false;
                    hiddIsSucess.Value = "1";
                    ddlLoseContent.Enabled = false;
                    ddlHotel.Enabled = false;
                    txtOther.Enabled = false;

                }

                if (Request.QueryString["FlowOrder"] == null)
                {
                    var ObjOrder = ObjOrderBLL.GetbyCustomerID(Request["CustomerID"].ToInt32());
                    if (ObjOrder.EmployeeID == User.Identity.Name.ToInt32())
                    {
                        FL_Customers fL_Customers = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
                        if (fL_Customers.State == ((int)CustomerStates.DidNotFollowOrder) || fL_Customers.State == ((int)CustomerStates.BeginFollowOrder) || fL_Customers.State == 200 || fL_Customers.State == 201 || fL_Customers.State == 202 || fL_Customers.State == 203 || fL_Customers.State == 205)
                        {
                            hiddIsSucess.Value = "99999";
                        }
                    }
                    else
                    {
                        btnSaveChange.Visible = false;
                        hiddIsSucess.Value = "0";
                        ddlLoseContent.Enabled = false;
                        ddlHotel.Enabled = false;
                        txtMoney.Enabled = false;
                        btnSaveMoney.Visible = false;
                        rdoStateList.Visible = false;
                        txtMostwanttogo.Enabled = false;
                        txtExperience.Enabled = false;
                        txtMemorable.Enabled = false;
                        txtTheProposal.Enabled = false;
                        txtAspirations.Enabled = false;
                        txtWatchingExperience.Enabled = false;
                        txtOther.Enabled = false;
                        txtFlowContent.Visible = false;
                        ddlTimerSpan.Enabled = false;
                    }
                }

                InvtieContent ObjInvtieContentBLL = new InvtieContent();
                var query = ObjInvtieContentBLL.GetByCustomerID(Request["CustomerID"].ToInt32());

                this.repContenList.DataSource = query;
                this.repContenList.DataBind();

                var DataList = ObjDetailsBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
                this.RepQuoteContentList.DataSource = DataList;
                this.RepQuoteContentList.DataBind();

            }
        }
        #endregion

        #region 绑定数据方法
        protected void BinderData(object sender, EventArgs e)
        {
            int SourceCount = 0;
            var query = ObjOrderDetailsBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            this.repFollowMessage.DataSource = query;
            this.repFollowMessage.DataBind();
            FL_OrderDetails orderDetails = query.FirstOrDefault();
            if (orderDetails != null)
            {
                //存入该用户操作人信息
                ViewState["OperEmployeeId"] = orderDetails.CreateEmpLoyee;
            }
            else
            {
                ViewState["OperEmployeeId"] = 0;
            }

            //this.repFollowMessage.DataSource = ObjOrderDetailsBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            //this.repFollowMessage.DataBind();
            if (Request["OrderID"].ToInt32() > 0)
            {
                var ObjOrder = ObjOrderBLL.GetByID(OrderID);
                if (ObjOrder != null)
                {
                    txtMoney.Text = Convert.ToDecimal(ObjOrder.EarnestMoney).Equals(0) ? string.Empty : ObjOrder.EarnestMoney.ToString();

                    if (txtMoney.Text.Trim().ToInt32() > 0)
                    {
                        txtMoney.Enabled = false;
                        btnSaveMoney.Enabled = false;
                    }
                }
            }
            FL_Customers fL_Customers = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            if (fL_Customers == null)
            {
                return;
            }
            txtGroom.Text = fL_Customers.Groom;
            txtBride.Text = fL_Customers.Bride;

            txtGroomBirthday.Text = fL_Customers.GroomBirthday == null ? "" : fL_Customers.GroomBirthday.ToString();
            txtBrideBirthday.Text = fL_Customers.BrideBirthday == null ? "" : fL_Customers.BrideBirthday.ToString();



            lblChannel.Text = fL_Customers.Channel;
            lblChannelType.Text = GetChannelTypeName(fL_Customers.ChannelType);
            lblCreateDate.Text = fL_Customers.RecorderDate.ToString();
            lblCreateEmpLoyee.Text = GetEmployeeName(fL_Customers.Recorder);
            lblReffer.Text = fL_Customers.Referee;

            txtGroomEmail.Text = fL_Customers.GroomEmail;
            txtBrideEmail.Text = fL_Customers.BrideEmail;
            txtGroomtelPhone.Text = fL_Customers.GroomtelPhone;
            txtBridePhone.Text = fL_Customers.BridePhone;
            txtGroomteWeixin.Text = fL_Customers.GroomteWeixin;
            txtBrideWeiXin.Text = fL_Customers.BrideWeiXin;
            txtGroomWeiBo.Text = fL_Customers.GroomWeiBo;
            txtBrideWeiBo.Text = fL_Customers.BrideWeiBo;
            txtGroomQQ.Text = fL_Customers.GroomQQ == "0" ? string.Empty : fL_Customers.GroomQQ;
            txtBrideQQ.Text = fL_Customers.BrideQQ == "0" ? string.Empty : fL_Customers.BrideQQ;
            txtPartyBudget.Text = fL_Customers.PartyBudget == 0 ? string.Empty : fL_Customers.PartyBudget.ToString();
            txtOperator.Text = fL_Customers.Operator;
            txtOperatorPhone.Text = fL_Customers.OperatorPhone;
            txtOperatorEmail.Text = fL_Customers.OperatorEmail;
            txtOperatorWeiXin.Text = fL_Customers.OperatorWeiXin;
            txtOperatorWeiBo.Text = fL_Customers.OperatorWeiBo;
            txtOperatorQQ.Text = fL_Customers.OperatorQQ == "0" ? string.Empty : fL_Customers.OperatorQQ;
            txtOperatorRelationship.Text = fL_Customers.OperatorRelationship;

            txtGroomCellPhone.Text = fL_Customers.GroomCellPhone;
            txtBrideCellPhone.Text = fL_Customers.BrideCellPhone;
            txtGroomtelPhone.Text = fL_Customers.GroomtelPhone;
            txtBridePhone.Text = fL_Customers.BridePhone;
            txtGroomIDCard.Text = fL_Customers.GroomIdCard;
            txtBrideIDCard.Text = fL_Customers.BrideIdCard;

            txtGroomJob.Text = fL_Customers.GroomJob;
            txtBrideJob.Text = fL_Customers.BrideJob;
            txtCustomersType.Text = fL_Customers.CustomersType;
            txtGroomJobCompany.Text = fL_Customers.GroomJobCompany;
            txtBrideJobCompany.Text = fL_Customers.BrideJobCompany;
            txtBanquetTypes.Text = fL_Customers.BanquetTypes;
            txtWeddingSponsors.Text = fL_Customers.WeddingSponsors;
            txtPartyDay.Text = fL_Customers.PartyDate.HasValue && (fL_Customers.PartyDate.Value.Date.CompareTo(DateTime.Parse("2000-01-01")) > 0) ? fL_Customers.PartyDate.Value.ToString("yyyy/MM/dd") : string.Empty;

            txtBanquetSales.Text = fL_Customers.BanquetSales;
            //txtRecorder.Text = fL_Customers.Recorder;

            txtGuestsStructure.Text = fL_Customers.GuestsStructure;
            txtPreparationsWedding.Text = fL_Customers.PreparationsWedding;
            txtLikeNumber.Text = fL_Customers.LikeNumber;
            txtLikeFilm.Text = fL_Customers.LikeFilm;
            txtLikeMusic.Text = fL_Customers.LikeMusic;
            txtLikePerson.Text = fL_Customers.LikePerson;
            // txtVoidLink.Text = fL_Customers.VoidLink;
            txtMemorable.Text = fL_Customers.Memorable;
            txtTheProposal.Text = fL_Customers.TheProposal;
            txtMemorableGift.Text = fL_Customers.MemorableGift;
            txtWatchingExperience.Text = fL_Customers.WatchingExperience;
            txtParentsWish.Text = fL_Customers.ParentsWish;
            txtOther.Text = fL_Customers.Other;
            txtConvenientIinvitationTime.Text = fL_Customers.ConvenientIinvitationTime;

            //fL_Customers.TableNumber = txtTablenumber.Text.ToInt32();

            //新加字段
            txtFirePoint.Text = fL_Customers.FirePoint;
            txtMostwanttogo.Text = fL_Customers.Mostwanttogo;
            if (fL_Customers.OperatorTelPhone != null)
            {
                txtOperatorTelPhone.Text = fL_Customers.OperatorTelPhone.Trim();
            }
            txtOperatorBrithday.Text = fL_Customers.OperatorBrithday;
            txtBanquetSalesPhone.Text = fL_Customers.BanquetSalesPhone;
            txtCustomerFlowContent.Text = fL_Customers.FlowContent;
            txtReceptionStandards.Text = fL_Customers.ReceptionStandards;
            txtOperatorJob.Text = fL_Customers.OperatorCompany;
            //  txtCustomerStatus.Text = fL_Customers.CustomerStatus;
            txtTablenumber.Text = fL_Customers.TableNumber.ToString();
            txtBanquetSales.Text = fL_Customers.BanquetSales;
            txtBanquetTypes.Text = fL_Customers.BanquetTypes;
            txtFormMarriage.Text = fL_Customers.FormMarriage;
            txtFormMarriage.Text = fL_Customers.WeddingServices;
            txtExperience.Text = fL_Customers.Experience;
            txtAspirations.Text = fL_Customers.Aspirations;
            txtNoTaboos.Text = fL_Customers.NoTaboos;
            txtImportantProcess.Text = fL_Customers.ImportantProcess;
            txtDesiredAppearance.Text = fL_Customers.DesiredAppearance;
            txtExpectedAtmosphere.Text = fL_Customers.ExpectedAtmosphere;
            txtReceptionStandards.Text = fL_Customers.ReceptionStandards;
            txtLikeColor.Text = fL_Customers.LikeColor;
            txtHobbies.Text = fL_Customers.Hobbies;
            txtWeddingServices.Text = fL_Customers.WeddingServices;


            //20140521新添字段
            txtBrideHobby.Text = fL_Customers.BrideHobby;
            txtGroomHobby.Text = fL_Customers.GroomHobby;
            txtIntentStyle.Text = fL_Customers.IntentStyle;
            txtGroomHobby.Text = fL_Customers.GroomHobby;
            txtKonwSite.Text = fL_Customers.KonwSite;
            txtHowKonw.Text = fL_Customers.HowKonw;
            txtEldersrequirements.Text = fL_Customers.Eldersrequirements;
            txtBlindDateSite.Text = fL_Customers.BlindDateSite;

            if (ddlTimerSpan.Items.FindByText(fL_Customers.TimeSpans) != null)
            {
                ddlTimerSpan.Items.FindByText(fL_Customers.TimeSpans).Selected = true;
            }
            if (fL_Customers.Wineshop != "")
            {

                var FocucuseItem = ddlHotel.Items.FindByText(fL_Customers.Wineshop);
                if (FocucuseItem != null)
                {

                    ddlHotel.Items.FindByText(fL_Customers.Wineshop).Selected = true;
                }
            }

        }
        #endregion

        public void DDLDataBinder()
        {
            this.ddlPackgeName.DataSource = ObjQuotedPriceBLL.GetByType(2).Where(C => C.IsDelete == false).ToList();
            this.ddlPackgeName.DataTextField = "QuotedTitle";
            this.ddlPackgeName.DataValueField = "QuotedID";
            this.ddlPackgeName.DataBind();
            ddlPackgeName.Items.Insert(0, new ListItem { Text = "请选择", Value = "0" });
        }

        #region 点击保存
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (rdoStateList.SelectedItem == null)
            {
                JavaScriptTools.AlertWindow("请选择沟通后的状态", Page);
                return;
            }

            FL_OrderDetails ObjOrderDetailModel = new FL_OrderDetails();
            ObjOrderDetailModel.CreateDate = DateTime.Now;
            decimal TransactionProba = 0;
            ObjOrderDetailModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
            ObjOrderDetailModel.CustomerID = Request["CustomerID"].ToInt32();
            ObjOrderDetailModel.FollowContent = this.txtFlowContent.Text;
            ObjOrderDetailModel.FollowDate = DateTime.Now.ToString().ToDateTime();
            ObjOrderDetailModel.SortOrder = 1;

            ObjOrderDetailModel.StateName = rdoStateList.SelectedItem.Text;
            ObjOrderDetailModel.OrderID = OrderID;
            if (rdoStateList.SelectedValue.ToString() == "202" || rdoStateList.SelectedValue.ToString() == "203")
            {
                ObjOrderDetailModel.NextPlanDate = txtNextFollowDate.Text.ToDateTime();
            }
            else
            {
                ObjOrderDetailModel.NextPlanDate = txtAgainDate.Text.ToDateTime();
            }

            
            SS_Report ObjReportModel = new SS_Report();
            ObjReportModel = ObjReportBLL.GetByCustomerID(Request["CustomerID"].ToInt32(), User.Identity.Name.ToInt32());


            if (ObjReportModel.ContactType == 0)
            {
                ObjReportModel.ContactMan = txtGroom.Text;
            }

            if (ObjReportModel.ContactType == 1)
            {
                ObjReportModel.ContactMan = txtBride.Text;

            }
            if (ObjReportModel.ContactType == 2)
            {
                ObjReportModel.ContactMan = txtOperator.Text;
            }


            if (txtPartyDay.Text != string.Empty)
            {
                ObjReportModel.Partydate = txtPartyDay.Text.ToDateTime();
            }
            ObjReportBLL.Update(ObjReportModel);

            //操作日志
            CreateHandle();

            if (rdoStateList.SelectedItem.Text == "流失")
            {
                ObjOrderDetailModel.FollowContent = txtLoseContent.Text;
                ObjOrderDetailModel.LoseContent = txtLoseContent.Text;
                ObjOrderDetailModel.LoseDate = DateTime.Now;
                ObjOrderDetailsBLL.Insert(ObjOrderDetailModel);

                var ObjOrderModel = ObjOrderBLL.GetByID(OrderID);
                var ObjCustomer = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());

                ObjOrderModel.ConteenID = ddlLoseContent.SelectedValue.ToInt32();
                if (ObjCustomer != null)
                {
                    //修改新人状态以及流失原因
                    ObjCustomer.LoseBeforeState = ObjCustomer.State;        //保存该客户流失之前的状态    
                    ObjCustomer.State = rdoStateList.SelectedValue.ToInt32();
                    ObjCustomerBLL.Update(ObjCustomer);

                    ObjOrderModel.LoseDate = DateTime.Now;
                    ObjOrderModel.NextFlowDate = txtAgainDate.Text.ToDateTime();
                    ObjOrderBLL.Update(ObjOrderModel);
                    JavaScriptTools.AlertWindowAndLocation("保存成功！", "LoseOrder.aspx?Needpopu=1", this.Page);
                    return;
                    //JavaScriptTools.AlertWindowAndLocation("修改成功!", Request.Url.ToString(), Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("保存失败！", this.Page);
                }

            }



            //计算成交率
            if (txtStateName.Text != string.Empty)
            {
                switch (txtStateName.Text)
                {
                    case "建立信任":
                        TransactionProba = 25;
                        break;
                    case "找到燃烧点":
                        TransactionProba = 35;
                        break;
                    case "优选":
                        TransactionProba = 50;
                        break;
                    case "确定":
                        TransactionProba = 75;
                        break;
                }
            }
            else
            {
                //找到燃烧点
                if (txtPartyBudget.Text.ToDecimal() > 0 && txtFormMarriage.Text != string.Empty && txtLikeColor.Text != string.Empty && txtExpectedAtmosphere.Text != string.Empty
                    && txtHobbies.Text != string.Empty && txtNoTaboos.Text != string.Empty && txtWeddingServices.Text != string.Empty && txtImportantProcess.Text != string.Empty
                    && txtDesiredAppearance.Text != string.Empty)
                {
                    TransactionProba = 35;
                }
            }



            if (txtFirePoint.Text != string.Empty && rdoStateList.SelectedValue != "202" && rdoStateList.SelectedValue != "204")
            {
                TransactionProba = 35;
            }

            int result = ObjOrderDetailsBLL.Insert(ObjOrderDetailModel);

            if (result > 0)
            {

                var ObjOrder = ObjOrderBLL.GetByID(OrderID);

                if (rdoStateList.SelectedValue.ToString() == "202" || rdoStateList.SelectedValue.ToString() == "203")
                {
                    ObjOrder.NextFlowDate = txtNextFollowDate.Text.ToDateTime();
                }
                else
                {
                    ObjOrder.NextFlowDate = txtAgainDate.Text.ToDateTime();
                }
                ObjOrder.LastFollowDate = DateTime.Now;
                ObjOrder.ProjectDate = DateTime.Now; //txtPreliminarySchemeDate.Text.ToDateTime();
                ObjOrder.StateName = rdoStateList.SelectedItem.Text;
                ObjOrder.TransactionProba = TransactionProba;
                ObjOrder.FlowCount = Convert.ToInt32(ObjOrder.FlowCount) + 1;
                ObjOrderBLL.Update(ObjOrder);

                ///跟踪次数大于等于3次 开始报警
                if (ObjOrder.FlowCount >= 3)
                {
                    WarningMessage ObjWarningMessageBLL = new WarningMessage();
                    FL_WarningMessage ObjWareMessage = new FL_WarningMessage();
                    Employee ObjEmployeeBLL = new Employee();
                    ObjWareMessage.CreateDate = DateTime.Now;
                    ObjWareMessage.EmpLoyeeID = ObjOrder.EmployeeID;
                    ObjWareMessage.IsLook = false;
                    ObjWareMessage.MessAgeTitle = "跟单次数超过三次";
                    ObjWareMessage.ResualtAddress = "/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=" + ObjOrder.CustomerID;
                    ObjWareMessage.managerEmployee = ObjEmployeeBLL.GetMineCheckEmployeeID(ObjOrder.EmployeeID);
                    ObjWareMessage.FinishKey = ObjOrder.OrderID;
                    ObjWareMessage.State = 3;
                    ObjWareMessage.CustomerID = ObjOrder.CustomerID;
                    ObjWarningMessageBLL.Insert(ObjWareMessage);
                }

                UpdateCustomer(ObjOrderDetailModel.DetailID);
                if (rdoTypes.SelectedItem.Text == "定制")
                {
                }
                else if (rdoTypes.SelectedItem.Text == "套餐")
                {
                }

                if (TransactionProba > 0)
                {
                    if (rdoStateList.SelectedValue != "13" && rdoStateList.SelectedValue != "29" && rdoStateList.SelectedValue != "10")
                    {
                        MissionManager ObjMissManagerBLL = new MissionManager();

                        ObjMissManagerBLL.WeddingMissionCreate(ObjOrder.CustomerID.Value, 1, (int)MissionTypes.Order, txtAgainDate.Text.Trim().ToDateTime(), ObjOrder.EmployeeID.Value, "?CustomerID=" + ObjOrder.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, ObjOrder.EmployeeID.Value, ObjOrder.OrderID);
                    }
                }
            }

            Response.Redirect("FollowUpOrder.aspx?NeedPopu=1");
            BinderData(sender, e);
        }
        #endregion

        #region 修改新人信息  确保新人信息全面
        private void UpdateCustomer(int DetailID)
        {
            int CustomerID = Request.QueryString["CustomerID"].ToInt32();
            FL_Customers fL_Customers = ObjCustomerBLL.GetByID(CustomerID);


            #region 该代码片段是属于新人必填信息
            fL_Customers.Groom = txtGroom.Text;

            if (txtGroomBirthday.Text != string.Empty)
            {
                fL_Customers.GroomBirthday = txtGroomBirthday.Text.ToDateTime();
            }
            fL_Customers.Bride = txtBride.Text;

            if (txtBrideBirthday.Text != string.Empty)
            {
                fL_Customers.BrideBirthday = txtBrideBirthday.Text.ToDateTime();
            }
            fL_Customers.GroomCellPhone = txtGroomCellPhone.Text;
            fL_Customers.BrideCellPhone = txtBrideCellPhone.Text;
            fL_Customers.Operator = txtOperator.Text;

            fL_Customers.OperatorPhone = txtOperatorPhone.Text;
            fL_Customers.FormMarriage = txtFormMarriage.Text;
            fL_Customers.LikeColor = txtLikeColor.Text;
            fL_Customers.ExpectedAtmosphere = txtExpectedAtmosphere.Text;
            fL_Customers.Hobbies = txtHobbies.Text;
            fL_Customers.NoTaboos = txtNoTaboos.Text;
            fL_Customers.WeddingServices = txtWeddingServices.Text;
            fL_Customers.ImportantProcess = txtImportantProcess.Text;
            fL_Customers.Experience = txtExperience.Text;
            fL_Customers.DesiredAppearance = txtDesiredAppearance.Text;

            fL_Customers.PartyDate = txtPartyDay.Text.ToDateTime(DateTime.Parse("1949-10-1"));

            //经办人
            fL_Customers.Operator = txtOperator.Text;
            fL_Customers.OperatorPhone = txtOperatorPhone.Text;
            fL_Customers.OperatorPhone = txtOperatorPhone.Text;
            fL_Customers.OperatorEmail = txtOperatorEmail.Text;
            fL_Customers.OperatorWeiBo = txtOperatorWeiBo.Text;
            fL_Customers.OperatorWeiXin = txtOperatorWeiXin.Text;
            fL_Customers.OperatorQQ = txtOperatorQQ.Text;
            fL_Customers.OperatorRelationship = txtOperatorRelationship.Text;
            #endregion

            fL_Customers.GroomEmail = txtGroomEmail.Text;
            fL_Customers.BrideEmail = txtBrideEmail.Text;
            fL_Customers.GroomtelPhone = txtGroomtelPhone.Text;
            fL_Customers.BridePhone = txtBridePhone.Text;
            fL_Customers.GroomteWeixin = txtGroomteWeixin.Text;
            fL_Customers.BrideWeiXin = txtBrideWeiXin.Text;
            fL_Customers.GroomWeiBo = txtGroomWeiBo.Text;
            fL_Customers.BrideWeiBo = txtBrideWeiBo.Text;
            fL_Customers.GroomQQ = txtGroomQQ.Text;
            fL_Customers.BrideQQ = txtBrideQQ.Text;


            fL_Customers.IsLose = false;

            fL_Customers.GroomJob = txtGroomJob.Text;
            fL_Customers.BrideJob = txtBrideJob.Text;
            fL_Customers.CustomersType = txtCustomersType.Text;
            fL_Customers.GroomJobCompany = txtGroomJobCompany.Text;
            fL_Customers.BrideJobCompany = txtBrideJobCompany.Text;
            fL_Customers.BanquetTypes = txtBanquetTypes.Text;
            fL_Customers.WeddingSponsors = txtWeddingSponsors.Text;
            fL_Customers.ConvenientIinvitationTime = txtConvenientIinvitationTime.Text;

            fL_Customers.BanquetSales = txtBanquetSales.Text;
            ////   fL_Customers.Recorder = txtRecorder.Text;

            fL_Customers.GuestsStructure = txtGuestsStructure.Text;
            fL_Customers.PreparationsWedding = txtPreparationsWedding.Text;
            fL_Customers.LikeNumber = txtLikeNumber.Text;
            fL_Customers.LikeFilm = txtLikeFilm.Text;
            fL_Customers.LikeMusic = txtLikeMusic.Text;
            fL_Customers.LikePerson = txtLikePerson.Text;
            // fL_Customers.VoidLink = txtVoidLink.Text;
            fL_Customers.Memorable = txtMemorable.Text;
            fL_Customers.TheProposal = txtTheProposal.Text;
            fL_Customers.MemorableGift = txtMemorableGift.Text;
            fL_Customers.WatchingExperience = txtWatchingExperience.Text;
            fL_Customers.ParentsWish = txtParentsWish.Text;
            fL_Customers.Other = txtOther.Text;
            fL_Customers.PartyBudget = txtPartyBudget.Text.ToDecimal();
            fL_Customers.EmployeeSelfAnalysis = "找到燃烧点";
            fL_Customers.AnalysisManager = "找到燃烧点";

            fL_Customers.Wineshop = ddlHotel.SelectedItem.Text;
            fL_Customers.TableNumber = txtTablenumber.Text.ToInt32();

            //新加字段
            fL_Customers.FirePoint = txtFirePoint.Text;
            fL_Customers.Mostwanttogo = txtMostwanttogo.Text;
            fL_Customers.OperatorTelPhone = txtOperatorTelPhone.Text;
            fL_Customers.OperatorBrithday = txtOperatorBrithday.Text;
            fL_Customers.BanquetSalesPhone = txtBanquetSalesPhone.Text;
            fL_Customers.FlowContent = txtCustomerFlowContent.Text;
            fL_Customers.ReceptionStandards = txtReceptionStandards.Text;
            fL_Customers.Aspirations = txtAspirations.Text;
            fL_Customers.OperatorCompany = txtOperatorJob.Text;
            fL_Customers.State = rdoStateList.SelectedValue.ToInt32();

            fL_Customers.TimeSpans = ddlTimerSpan.SelectedItem.Text;


            //20140521新添字段
            fL_Customers.BrideHobby = txtBrideHobby.Text;
            fL_Customers.GroomHobby = txtGroomHobby.Text;
            fL_Customers.IntentStyle = txtIntentStyle.Text;
            fL_Customers.GroomHobby = txtGroomHobby.Text;
            fL_Customers.KonwSite = txtKonwSite.Text;
            fL_Customers.HowKonw = txtHowKonw.Text;
            fL_Customers.Eldersrequirements = txtEldersrequirements.Text;
            fL_Customers.BlindDateSite = txtBlindDateSite.Text;

            if (rdoStateList.Visible == true)
            {
                if (fL_Customers.State == 204)
                {
                    fL_Customers.State = (int)CustomerStates.SucessOrder;

                    Report ObjReportBLL = new Report();
                    SS_Report ObjReportModel = new SS_Report();
                    ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());
                    ObjReportModel.State = fL_Customers.State;
                    ObjReportModel.OrderSucessDate = DateTime.Now;
                    ObjReportModel.Emoney = txtMoney.Text.ToDecimal();

                    if (ObjReportModel.ContactType == 0)
                    {
                        ObjReportModel.ContactMan = txtGroom.Text;
                    }

                    if (ObjReportModel.ContactType == 1)
                    {
                        ObjReportModel.ContactMan = txtBride.Text;

                    }
                    if (ObjReportModel.ContactType == 2)
                    {
                        ObjReportModel.ContactMan = txtOperator.Text;
                    }

                    ObjReportModel.QuotedCreateDate = DateTime.Now;
                    ObjReportModel.QuotedEmployee = hideEmpLoyeeID.Value.ToInt32();
                    if (txtPartyDay.Text != string.Empty)
                    {
                        ObjReportModel.Partydate = txtPartyDay.Text.ToDateTime();
                    }
                    ObjReportBLL.Update(ObjReportModel);

                }
                else
                {
                    if (txtFirePoint.Text != string.Empty && rdoStateList.SelectedValue != "202" && rdoStateList.SelectedValue != "204")
                    {
                        var ObjCustomer = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
                        ObjCustomer.State = (int)CustomerStates.FirePoint;
                        ObjCustomerBLL.Update(ObjCustomer);
                    }
                }
            }


            int result = ObjCustomerBLL.Update(fL_Customers);
            //根据返回判断添加的状态
            if (result > 0)
            {
                btnSaveMoney_Click(fL_Customers.State.Value);
            }
        }
        #endregion

        #region 输入订金之后 点击确定按钮
        protected void btnSaveMoney_Click(int State)
        {
            OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
            Order ObjOrderBLL = new Order();
            var ObjOrder = ObjOrderBLL.GetByID(OrderID);
            ObjOrder.NextFlowDate = txtAgainDate.Text.ToDateTime();
            ObjOrder.LastFollowDate = DateTime.Now;
            if (txtMoney.Text != string.Empty)
            {

                ObjOrder.EarnestMoney = txtMoney.Text.ToDecimal();
                ObjOrder.EarnestFinish = false;
                ObjOrderBLL.Update(ObjOrder);
            }
            //如果有下级权限就加入跟单
            if (State == 204 || State == (int)CustomerStates.SucessOrder)
            {
                UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
                if (ObjUserJurisdictionBLL.CheckByClassType("QuotedPriceWorkPanel", User.Identity.Name.ToInt32()))
                {
                    HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
                    ///录入报价单基础
                    FL_QuotedPrice ObjQuotedPriceModel = new FL_QuotedPrice();

                    var ObjReportModel = ObjReportBLL.GetByCustomerID(Request["CustomerID"].ToInt32(), User.Identity.Name.ToInt32());

                    ObjReportModel.QuotedCreateDate = DateTime.Now;
                    ObjReportBLL.Update(ObjReportModel);


                    if (ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()) == null)
                    {
                        ObjQuotedPriceModel.CustomerID = Request["CustomerID"].ToInt32();
                        ObjQuotedPriceModel.IsDelete = false;
                        ObjQuotedPriceModel.OrderID = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).OrderID;// Request["OrderID"].ToInt32();
                        ObjQuotedPriceModel.CategoryName = "开始制作报价单";
                        ObjQuotedPriceModel.EmpLoyeeID = hideEmpLoyeeID.Value.ToInt32();

                        ObjQuotedPriceModel.IsChecks = false;
                        ObjQuotedPriceModel.CreateDate = DateTime.Now;
                        ObjQuotedPriceModel.IsDispatching = 0;
                        ObjQuotedPriceModel.EarnestMoney = 0;
                        ObjQuotedPriceModel.FinishAmount = 0;
                        ObjQuotedPriceModel.IsFirstCreate = false;
                        ObjQuotedPriceModel.PakegName = rdoTypes.SelectedItem.Text.ToString();
                        ObjQuotedPriceModel.PakegTyper = ddlPackgeName.Text.Trim().ToString();
                        ObjQuotedPriceModel.OrderCoder = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).OrderCoder;
                        ObjQuotedPriceModel.ParentQuotedID = 0;
                        ObjQuotedPriceModel.NextFlowDate = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).LastFollowDate;
                        ObjQuotedPriceModel.StarDispatching = false;
                        ObjQuotedPriceModel.DesignerState = 0;
                        var ObjQuteKey = ObjQuotedPriceBLL.Insert(ObjQuotedPriceModel);
                        var ObjUpdateModel = ObjCustomerBLL.GetByID(ObjQuotedPriceModel.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.SucessOrder;//开始制作报价单
                        ObjCustomerBLL.Update(ObjUpdateModel);


                        MissionManager ObjMissManagerBLL = new MissionManager();

                        //ObjMissManagerBLL.WeddingMissionCreate(ObjOrder.CustomerID.Value, 1, (int)MissionTypes.Quoted, DateTime.Now, ObjOrder.EmployeeID.Value, "?QuotedID=" + ObjQuotedPriceModel.QuotedID + "&OrderID=" + ObjOrder.OrderID + "&FlowOrder=1&CustomerID=" + ObjQuotedPriceModel.CustomerID + "&PartyDate=" + ObjCustomerBLL.GetByID(ObjQuotedPriceModel.CustomerID).PartyDate.ToString(), "QuotedPriceWorkPanel", ObjQuotedPriceModel.EmpLoyeeID.Value, ObjQuotedPriceModel.QuotedID);
                        //ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.Value, 1, (int)MissionTypes.Quoted, DateTime.Now, User.Identity.Name.ToInt32(), "?QuotedID=" + ObjQuotedPriceModel.QuotedID + "&OrderID=" + ObjQuotedPriceModel.OrderID + "&FlowOrder=1&CustomerID=" + ObjQuotedPriceModel.CustomerID, "QuotedPriceWorkPanel", ObjQuotedPriceModel.EmpLoyeeID.Value, ObjQuotedPriceModel.QuotedID);


                        ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.Value, 1, (int)MissionTypes.QuotedPlan, DateTime.Now, User.Identity.Name.ToInt32(), "?QuotedID=" + ObjQuotedPriceModel.QuotedID + "&OrderID=" + ObjQuotedPriceModel.OrderID + "&FlowOrder=1&CustomerID=" + ObjQuotedPriceModel.CustomerID, "QuotedPriceWorkPanel", ObjQuotedPriceModel.EmpLoyeeID.Value, ObjQuotedPriceModel.QuotedID);

                        ObjQuotedCollectionsPlanBLL.Insert(new DataAssmblly.FL_QuotedCollectionsPlan()
                        {
                            OrderID = Request["OrderID"].ToInt32(),
                            QuotedID = ObjQuotedPriceModel.QuotedID,
                            RealityAmount = txtRealityAmount.Text.ToDecimal(),
                            CollectionTime = DateTime.Now,
                            CreateEmpLoyee = User.Identity.Name.ToInt32(),
                            FinancialEmployee = User.Identity.Name.ToInt32(),
                            AmountEmployee = User.Identity.Name.ToInt32(),
                            CreateDate = DateTime.Now,
                            Amountmoney = txtRealityAmount.Text.ToDecimal(),
                            RowLock = false,
                            Node = "定金",
                            MoneyType = rdoMoneytypes.SelectedItem.Text,
                            State = 0
                        });

                        GetRealitySumMoney();
                    }

                }

                Response.Redirect("SucessOrder.aspx?NeedPopu=1");
            }
        }
        #endregion

        protected void btnStar_Click(object sender, EventArgs e)
        {
            //var ObjCustomer = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            //ObjCustomer.State = (int)CustomerStates.BeginFollowOrder;
            //ObjCustomerBLL.Update(ObjCustomer);
            //Response.Redirect(Request.Url.ToString().Replace("&State=None", ""));
        }

        #region 获取现金流 头部显示

        public void GetRealitySumMoney()
        {
            int EmployeeId = User.Identity.Name.ToInt32();
            Sys_Employee employee = ObjEmployeeBLL.GetByID(EmployeeId);

            FL_FinishTargetSum FinishTargetSum = new FL_FinishTargetSum();
            FL_Target target = new FL_Target();

            var FinishTargetList = ObjFinishTargetSumBLL.GetByAll();
            var TargetList = ObjTargetBLL.GetByAll();

            var QuotedCollectionPlanList = ObjQuotedCollectionsPlanBLL.GetByOrderID(Request["OrderID"].ToInt32());

            bool isExists = false;
            List<FL_FinishTargetSum> FinishTargetSumList = new List<FL_FinishTargetSum>();
            foreach (var item in FinishTargetList)
            {
                if (item.EmployeeID == EmployeeId && item.TargetTitle == "现金流" && item.Year == DateTime.Now.Year)      //判断改用户 现金流是否存在 存在则返回True
                {
                    FinishTargetSumList.Add(item);
                    isExists = true;
                }
            }

            if (isExists == true)       //该用户现金流已经存在  只需修改
            {
                foreach (var item in FinishTargetSumList)
                {
                    if (item.TargetTitle == "现金流" && item.Year == DateTime.Now.Year)
                    {
                        FinishTargetSum = ObjFinishTargetSumBLL.GetByID(item.FinishKey);
                        GetUpdateQuotePriceByMonth(FinishTargetSum, EmployeeId);

                        ObjFinishTargetSumBLL.Update(FinishTargetSum);
                    }
                }
            }




            if (isExists == false)      //该用户现金流不存在  需要新增
            {
                foreach (var item in TargetList)
                {
                    if (item.TargetTitle == "现金流")
                    {
                        FinishTargetSum = new FL_FinishTargetSum();
                        GetAddQuotePriceByMonth(FinishTargetSum, EmployeeId);

                        FinishTargetSum.TargetID = item.TargetID;
                        FinishTargetSum.TargetTitle = item.TargetTitle;
                        FinishTargetSum.DepartmentID = employee.DepartmentID;
                        FinishTargetSum.EmployeeID = EmployeeId;
                        FinishTargetSum.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        FinishTargetSum.Year = DateTime.Now.Year;
                        FinishTargetSum.CreateEmployeeID = EmployeeId;
                        FinishTargetSum.LastYearCompletionrate = 0;
                        FinishTargetSum.FinishSum = 0;
                        FinishTargetSum.OveryearRate = 0;
                        FinishTargetSum.OverYearFinishSum = 0;
                        FinishTargetSum.LastYearFinishSum = 0;
                        FinishTargetSum.Unite = "个";
                        FinishTargetSum.IsActive = false;

                        ObjFinishTargetSumBLL.Insert(FinishTargetSum);
                    }
                }
            }

            //添加现金流  已经被添加的数据状态就变为1
            int OrderId = Request["OrderID"].ToInt32();
            List<FL_QuotedCollectionsPlan> QCPList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderId);
            foreach (var item in QCPList)
            {
                item.State = 1;
                ObjQuotedCollectionsPlanBLL.Update(item);
            }



        }
        #endregion

        #region 新增现金流

        public void GetAddQuotePriceByMonth(FL_FinishTargetSum FinishTargetSum, int EmployeeId)
        {
            int OrderID = Request["OrderID"].ToInt32();
            FinishTargetSum.MonthPlan1 = 0;
            FinishTargetSum.MonthFinsh1 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 1);
            FinishTargetSum.MonthPlan2 = 0;
            FinishTargetSum.MonthFinish2 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 2);
            FinishTargetSum.MonthPlan3 = 0;
            FinishTargetSum.MonthFinish3 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 3);
            FinishTargetSum.MonthPlan4 = 0;
            FinishTargetSum.MonthFinish4 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 4);
            FinishTargetSum.MonthPlan5 = 0;
            FinishTargetSum.MonthFinish5 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 5);
            FinishTargetSum.MonthPlan6 = 0;
            FinishTargetSum.MonthFinish6 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 6);
            FinishTargetSum.MonthPlan7 = 0;
            FinishTargetSum.MonthFinish7 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 7);
            FinishTargetSum.MonthPlan8 = 0;
            FinishTargetSum.MonthFinish8 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 8);
            FinishTargetSum.MonthPlan9 = 0; ;
            FinishTargetSum.MonthFinish9 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 9);
            FinishTargetSum.MonthPlan10 = 0; ;
            FinishTargetSum.MonthFinish10 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 10);
            FinishTargetSum.MonthPlan11 = 0; ;
            FinishTargetSum.MonthFinish11 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 11);
            FinishTargetSum.MonthPlan12 = 0; ;
            FinishTargetSum.MonthFinish12 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 12);
            FinishTargetSum.Completionrate = 0;
            FinishTargetSum.PlanSum = FinishTargetSum.MonthPlan1 + FinishTargetSum.MonthPlan2 + FinishTargetSum.MonthPlan3 + FinishTargetSum.MonthPlan4 + FinishTargetSum.MonthPlan5 + FinishTargetSum.MonthPlan6 + FinishTargetSum.MonthPlan7 + FinishTargetSum.MonthPlan8 + FinishTargetSum.MonthPlan9 + FinishTargetSum.MonthPlan10 + FinishTargetSum.MonthPlan11 + FinishTargetSum.MonthPlan12;
        }
        #endregion

        #region 修改现金流

        public void GetUpdateQuotePriceByMonth(FL_FinishTargetSum FinishTargetSum, int EmployeeId)
        {
            int OrderID = Request["OrderID"].ToInt32();
            FinishTargetSum.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            FinishTargetSum.MonthFinsh1 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 1);
            FinishTargetSum.MonthFinish2 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 2);
            FinishTargetSum.MonthFinish3 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 3);
            FinishTargetSum.MonthFinish4 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 4);
            FinishTargetSum.MonthFinish5 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 5);
            FinishTargetSum.MonthFinish6 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 6);
            FinishTargetSum.MonthFinish7 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 7);
            FinishTargetSum.MonthFinish8 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 8);
            FinishTargetSum.MonthFinish9 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 9);
            FinishTargetSum.MonthFinish10 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 10);
            FinishTargetSum.MonthFinish11 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 11);
            FinishTargetSum.MonthFinish12 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 12);
            FinishTargetSum.Completionrate = 0;
        }
        #endregion

        #region 点击保存  策划报价沟通记录
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSaveConfirm_Click(object sender, EventArgs e)
        {

            FL_DetailsQuotedPrice DetailsModel = new FL_DetailsQuotedPrice();
            DetailsModel.CustomerID = Request["CustomerID"].ToInt32();
            DetailsModel.QuotedID = ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).QuotedID;
            DetailsModel.QuotedCreateDate = DateTime.Now.ToShortDateString().ToDateTime();
            DetailsModel.QuotedContent = txtQuotedContent.Text.Trim().ToString();
            DetailsModel.CreateEmployee = User.Identity.Name.ToInt32();
            DetailsModel.NextFollowDate = txtNextFollowDates.Text.ToString().ToDateTime();

            int result = ObjDetailsBLL.Insert(DetailsModel);

            if (result > 0)
            {
                JavaScriptTools.AlertWindow("保存成功", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("保存失败,请稍候再试...", Page);
            }

        }
        #endregion

        #region 是否隐藏
        /// <summary>
        /// 隐藏
        /// </summary>
        public string IsVisible()
        {
            int CustomerID = Request["CustomerID"].ToInt32();
            var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (Request["OrderID"].ToInt32() > 0)           //填写跟单记录里  不能填写策划报价的沟通记录
            {
                return "style='display:none;'";
            }
            if (Model == null)          //为null 说明还未到 策划报价  就不能填写沟通记录(策划报价)
            {
                return "style='display:none;'";
            }
            return "";
        }
        #endregion

        #region 跟单事件
        /// <summary>
        /// 时间显示  下次沟通时间还是提案时间
        /// </summary>
        protected void repFollowMessage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTime = e.Item.FindControl("lblOrderTimes") as Label;
            int DetailsID = (e.Item.FindControl("HideDetailsID") as HiddenField).Value.ToInt32();
            var Model = ObjOrderDetailsBLL.GetByID(DetailsID);
            if (Model.StateName == "确定")
            {
                lblTime.Text = "提案时间";
            }
            else
            {
                lblTime.Text = "下次沟通时间";
            }


        }
        #endregion


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            string Customername = txtBride.Text.Trim().ToString() != "" ? txtBride.Text.Trim().ToString() : txtGroom.Text.Trim().ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();
            if (rdoStateList.SelectedItem.Text == "确定")
            {
                HandleModel.HandleContent = "销售跟单,客户姓名:" + Customername + ",跟单状态:" + rdoStateList.SelectedItem.Text + ",定金收款:" + txtRealityAmount.Text.Trim().ToString();
            }
            else
            {
                HandleModel.HandleContent = "销售跟单,客户姓名:" + Customername + ",跟单状态:" + rdoStateList.SelectedItem.Text;
            }
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 2;     //销售跟单
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

    }
}