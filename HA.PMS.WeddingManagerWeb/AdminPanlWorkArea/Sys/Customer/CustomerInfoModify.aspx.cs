using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer
{
    public partial class CustomerInfoModify : SystemPage
    {
        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderCustomer(Request.QueryString["CustomerID"].ToInt32());
            }
        }
        #endregion


        #region 页面加载方法 传入客户ID
        private void BinderCustomer(int CustomerID)
        {
            BLLAssmblly.Flow.Customers customerBLL = new BLLAssmblly.Flow.Customers();
            HA.PMS.DataAssmblly.FL_Customers fL_Customers = customerBLL.GetByID(CustomerID);

            txtGroom.Text = fL_Customers.Groom;
            try
            {
                txtGroomBirthday.Text = fL_Customers.GroomBirthday.Value.CompareTo(DateTime.Parse("1949-10-2")) > 0 ? fL_Customers.GroomBirthday.Value.ToShortDateString() : string.Empty;
            }
            catch
            {

            }
            txtGroomCellPhone.Text = fL_Customers.GroomCellPhone;
            txtGroomEmail.Text = fL_Customers.GroomEmail;
            txtGroomJob.Text = fL_Customers.GroomJob;
            txtGroomQQ.Text = (fL_Customers.GroomQQ + string.Empty).TrimStart('0');
            txtGroomJobCompany.Text = fL_Customers.GroomJobCompany;
            txtGroomtelPhone.Text = fL_Customers.GroomtelPhone;
            txtGroomWeiBo.Text = fL_Customers.GroomWeiBo;
            txtGroomteWeixin.Text = fL_Customers.GroomteWeixin;
            txtGroomIDCard.Text = fL_Customers.GroomIdCard;
            rdoIsVIP.Items.FindByValue(fL_Customers.IsVip == true ? "1" : "0").Selected = true;

            txtBride.Text = fL_Customers.Bride;
            try
            {
                txtBrideBirthday.Text = fL_Customers.BrideBirthday.Value.CompareTo(DateTime.Parse("1949-10-2")) > 0 ? fL_Customers.BrideBirthday.Value.ToShortDateString() : string.Empty;
            }
            catch (Exception)
            {


            }

            txtBrideCellPhone.Text = fL_Customers.BrideCellPhone;
            txtBrideJob.Text = fL_Customers.BrideJob;
            txtBrideQQ.Text = (fL_Customers.BrideQQ + string.Empty).TrimStart('0');
            txtBrideJobCompany.Text = fL_Customers.BrideJobCompany;
            txtBridePhone.Text = fL_Customers.BridePhone;
            txtBrideWeiBo.Text = fL_Customers.BrideWeiBo;
            txtBrideWeiXin.Text = fL_Customers.BrideWeiXin;
            txtBrideIDCard.Text = fL_Customers.BrideIdCard;

            txtOperator.Text = fL_Customers.Operator;
            txtOperatorBrithday.Text = fL_Customers.OperatorBrithday;
            txtOperatorPhone.Text = fL_Customers.OperatorPhone;
            txtOperatorEmail.Text = fL_Customers.OperatorEmail;
            txtOperatorRelationship.Text = fL_Customers.OperatorRelationship;
            txtOperatorQQ.Text = (fL_Customers.OperatorQQ + string.Empty).TrimStart('0');
            txtOperatorJob.Text = fL_Customers.OperatorCompany;
            txtOperatorPhone.Text = fL_Customers.OperatorPhone;
            txtOperatorWeiBo.Text = fL_Customers.OperatorWeiBo;
            txtOperatorWeiXin.Text = fL_Customers.OperatorWeiXin;
            txtOperatorIDCard.Text = fL_Customers.OperatorIDCard;

            ddlApplyType.SelectedValue = fL_Customers.ApplyType == null ? "0" : fL_Customers.ApplyType.ToString();



            //txtChannel.Text = fL_Customers.Channel;
            //txtChannelType.Text = GetChannelTypeName(fL_Customers.ChannelType);


            Report ObjReportBLL = new Report();
            var ObjReportModel = ObjReportBLL.GetByCustomerID(fL_Customers.CustomerID, User.Identity.Name.ToInt32());
            switch (ObjReportModel.ContactType)
            {
                case 0:
                    rdoCustomertype.Items[1].Selected = true;
                    break;
                case 1:
                    rdoCustomertype.Items[0].Selected = true;
                    break;
                case 2:
                    rdoCustomertype.Items[2].Selected = true;
                    break;
            }



            lblCreateEmployee.Text = GetEmployeeName(ObjReportBLL.GetByCustomerID(fL_Customers.CustomerID, User.Identity.Name.ToInt32()).CreateEmployee.Value); //fL_Customers.Recorder.HasValue ? GetEmployeeName(fL_Customers.Recorder.Value) : string.Empty;
            lblCreateDate.Text = fL_Customers.RecorderDate.ToString();
            txtCreateDate.Text = fL_Customers.RecorderDate.ToString();

            txtPartyDay.Text = fL_Customers.PartyDate.HasValue ? fL_Customers.PartyDate.Value.ToShortDateString() : string.Empty;
            ddlTimerSpan.ClearSelection();
            if (!object.ReferenceEquals(ddlTimerSpan.Items.FindByText(fL_Customers.TimeSpans), null))
            {
                ddlTimerSpan.Items.FindByText(fL_Customers.TimeSpans).Selected = true;
            }

            ddlHotel.ClearSelection();
            if (!object.ReferenceEquals(ddlHotel.Items.FindByText(fL_Customers.Wineshop), null))
            {
                ddlHotel.Items.FindByText(fL_Customers.Wineshop).Selected = true;
            }
            txtTablenumber.Text = fL_Customers.TableNumber + string.Empty;

            txtBanquetSales.Text = fL_Customers.BanquetSales;
            txtBanquetSalesPhone.Text = fL_Customers.BanquetSalesPhone;

            //txtCustomersType.Text = fL_Customers.CustomersType;
            string CustomerType = fL_Customers.CustomersType != null && fL_Customers.CustomersType != "" ? fL_Customers.CustomersType.ToString() : "";
            if (CustomerType != "")
            {
                ddlCustomersType.Items.FindByText(CustomerType).Selected = true;
            }
            //txtBanquetTypes.Text = fL_Customers.BanquetTypes;

            txtPartyBudget.Text = fL_Customers.PartyBudget + string.Empty;
            //txtConvenientIinvitationTime.Text = fL_Customers.ConvenientIinvitationTime;



            var ObjItem = ddlChannelType.Items.FindByValue(fL_Customers.ChannelType.ToString());
            if (ObjItem != null)
            {
                ddlChannelType.ClearSelection();
                ObjItem.Selected = true;
                ddlReferee.BinderbyChannel(ddlChannelName.SelectedValue.ToInt32());
            }


            ddlChannelName.Items.Add(new ListItem("手动录入", "手动录入"));
            var ObjChannelItem = ddlChannelName.Items.FindByText(fL_Customers.Channel);

            if (ObjChannelItem != null)
            {
                ddlChannelName.ClearSelection();
                ObjChannelItem.Selected = true;
                ddlReferee.BinderbyChannel(ObjChannelItem.Value.ToInt32());
                if (ObjChannelItem.Text == "手动录入")
                {
                    txtReffer.Visible = true;
                    ddlReferee.Visible = false;
                }
                else
                {
                    ddlReferee.Visible = true;
                }

            }
            else
            {
                ddlReferee.Visible = true;
            }



            txtReffer.Text = fL_Customers.Referee;
            var RefereeModel = ddlReferee.Items.FindByText(fL_Customers.Referee);
            if (RefereeModel != null)
            {

                ddlReferee.ClearSelection();
                RefereeModel.Selected = true;
            }
            txtReceptionStandards.Text = fL_Customers.ReceptionStandards;

            txtOther.Text = fL_Customers.Other;
            txtRebates.Text = fL_Customers.Rebates;

            //其他信息
            txtMemorableGift.Text = fL_Customers.MemorableGift;
            txtWeddingServices.Text = fL_Customers.WeddingServices;
            txtLikeFilm.Text = fL_Customers.LikeFilm;
            txtLikeMusic.Text = fL_Customers.LikeMusic;
            txtLikeColor.Text = fL_Customers.LikeColor;

            txtMostwanttogo.Text = fL_Customers.Mostwanttogo;
            txtLikeNumber.Text = fL_Customers.LikeNumber;
            txtLikePerson.Text = fL_Customers.LikePerson;
            txtImportantProcess.Text = fL_Customers.ImportantProcess;
            txtWeddingSponsors.Text = fL_Customers.WeddingSponsors;

            txtFormMarriage.Text = fL_Customers.FormMarriage;
            txtNoTaboos.Text = fL_Customers.NoTaboos;
            txtPreparationsWedding.Text = fL_Customers.PreparationsWedding;
            txtGuestsStructure.Text = fL_Customers.GuestsStructure;
            txtDesiredAppearance.Text = fL_Customers.DesiredAppearance;

            txtExpectedAtmosphere.Text = fL_Customers.ExpectedAtmosphere;
            txtParentsWish.Text = fL_Customers.ParentsWish;
            txtHobbies.Text = fL_Customers.Hobbies;

            //20140521新添字段
            txtBrideHobby.Text = fL_Customers.BrideHobby;
            txtGroomHobby.Text = fL_Customers.GroomHobby;
            txtIntentStyle.Text = fL_Customers.IntentStyle;
            txtGroomHobby.Text = fL_Customers.GroomHobby;
            txtKonwSite.Text = fL_Customers.KonwSite;
            txtHowKonw.Text = fL_Customers.HowKonw;
            txtEldersrequirements.Text = fL_Customers.Eldersrequirements;
            txtBlindDateSite.Text = fL_Customers.BlindDateSite;

        }
        #endregion

        #region 修改方法  传入客户ID
        private void UpdateCustomer(int CustomerID)
        {
            SysConfig ObjConfigBLL = new SysConfig();
            var ConfigModel = ObjConfigBLL.GetByName("PhoneRepeat");

            if (ConfigModel.IsClose == false)           //停用  不能重复
            {
                if (txtBrideCellPhone.Text.Trim().ToString() == txtGroomCellPhone.Text.Trim().ToString())
                {
                    JavaScriptTools.AlertWindow("新郎、新娘手机号不能相同，请更改后输入", Page);
                    return;
                }
            }

            BLLAssmblly.Flow.Customers customerBLL = new BLLAssmblly.Flow.Customers();
            HA.PMS.DataAssmblly.FL_Customers fL_Customers = customerBLL.GetByID(CustomerID);

            fL_Customers.Groom = txtGroom.Text.Trim();
            fL_Customers.GroomBirthday = txtGroomBirthday.Text.ToDateTime(DateTime.Parse("1753-1-1"));
            fL_Customers.GroomCellPhone = txtGroomCellPhone.Text.Trim();
            fL_Customers.GroomEmail = txtGroomEmail.Text.Trim();
            fL_Customers.GroomJob = txtGroomJob.Text.Trim();
            fL_Customers.GroomQQ = txtGroomQQ.Text;
            fL_Customers.GroomJobCompany = txtGroomJobCompany.Text.Trim();
            fL_Customers.GroomtelPhone = txtGroomtelPhone.Text.Trim();
            fL_Customers.GroomWeiBo = txtGroomWeiBo.Text.Trim();
            fL_Customers.GroomteWeixin = txtGroomteWeixin.Text.Trim();
            fL_Customers.GroomIdCard = txtGroomIDCard.Text.Trim();

            fL_Customers.Bride = txtBride.Text.Trim();
            fL_Customers.BrideBirthday = txtBrideBirthday.Text.ToDateTime(DateTime.Parse("1753-1-1"));
            fL_Customers.BrideCellPhone = txtBrideCellPhone.Text.Trim();
            fL_Customers.BrideEmail = txtBrideEmail.Text.Trim();
            fL_Customers.BrideJob = txtBrideJob.Text.Trim();
            fL_Customers.BrideQQ = txtBrideQQ.Text;
            fL_Customers.BrideJobCompany = txtBrideJobCompany.Text.Trim();
            fL_Customers.BridePhone = txtBridePhone.Text.Trim();
            fL_Customers.BrideWeiBo = txtBrideWeiBo.Text.Trim();
            fL_Customers.BrideWeiXin = txtBrideWeiXin.Text.Trim();
            fL_Customers.BrideIdCard = txtBrideIDCard.Text.Trim();

            fL_Customers.Operator = txtOperator.Text.Trim();
            fL_Customers.OperatorBrithday = txtOperatorBrithday.Text.Trim();
            fL_Customers.OperatorPhone = txtOperatorPhone.Text.Trim();
            fL_Customers.OperatorEmail = txtOperatorEmail.Text.Trim();
            fL_Customers.OperatorRelationship = txtOperatorRelationship.Text.Trim();
            fL_Customers.OperatorQQ = txtOperatorQQ.Text.Trim();
            fL_Customers.OperatorCompany = txtOperatorJob.Text.Trim();
            fL_Customers.OperatorPhone = txtOperatorPhone.Text.Trim();
            fL_Customers.OperatorWeiBo = txtOperatorWeiBo.Text.Trim();
            fL_Customers.OperatorWeiXin = txtOperatorWeiXin.Text.Trim();
            fL_Customers.OperatorIDCard = txtOperatorIDCard.Text.Trim();


            fL_Customers.Channel = ddlChannelName.SelectedItem.Text;
            fL_Customers.ChannelType = ddlChannelType.SelectedValue.ToInt32();
            if (ddlChannelName.SelectedItem.Text == "手动录入")
            {
                fL_Customers.Referee = ddlReferee.SelectedItem.Text;
            }
            else
            {
                if (ddlReferee.Items.Count > 0)
                {
                    fL_Customers.Referee = ddlReferee.SelectedItem.Text;
                }
            }

            fL_Customers.RecorderDate = txtCreateDate.Text.ToDateTime();
            fL_Customers.PartyDate = txtPartyDay.Text.ToDateTime();
            fL_Customers.TimeSpans = ddlTimerSpan.SelectedItem.Text;

            fL_Customers.Wineshop = ddlHotel.SelectedItem.Text;
            fL_Customers.TableNumber = txtTablenumber.Text.ToInt32();

            fL_Customers.BanquetSales = txtBanquetSales.Text.Trim();
            fL_Customers.BanquetSalesPhone = txtBanquetSalesPhone.Text.Trim();

            fL_Customers.CustomersType = ddlCustomersType.SelectedItem.Text.Trim().ToString();
            fL_Customers.PartyBudget = txtPartyBudget.Text.ToDecimal();


            fL_Customers.ReceptionStandards = txtReceptionStandards.Text.Trim();

            fL_Customers.Other = txtOther.Text.Trim();
            fL_Customers.Rebates = txtRebates.Text.Trim();
            fL_Customers.IsVip = rdoIsVIP.SelectedValue.ToInt32() == 0 ? false : true;

            //其他信息
            fL_Customers.MemorableGift = txtMemorableGift.Text.Trim();
            fL_Customers.WeddingServices = txtWeddingServices.Text.Trim();
            fL_Customers.LikeFilm = txtLikeFilm.Text.Trim();
            fL_Customers.LikeMusic = txtLikeMusic.Text.Trim();
            fL_Customers.LikeColor = txtLikeColor.Text.Trim();

            fL_Customers.Mostwanttogo = txtMostwanttogo.Text.Trim();
            fL_Customers.LikeNumber = txtLikeNumber.Text.Trim();
            fL_Customers.LikePerson = txtLikePerson.Text.Trim();
            fL_Customers.ImportantProcess = txtImportantProcess.Text.Trim();
            fL_Customers.WeddingSponsors = txtWeddingSponsors.Text.Trim();

            fL_Customers.FormMarriage = txtFormMarriage.Text.Trim();
            fL_Customers.NoTaboos = txtNoTaboos.Text.Trim();
            fL_Customers.PreparationsWedding = txtPreparationsWedding.Text.Trim();
            fL_Customers.GuestsStructure = txtGuestsStructure.Text.Trim();
            fL_Customers.DesiredAppearance = txtDesiredAppearance.Text.Trim();

            fL_Customers.ExpectedAtmosphere = txtExpectedAtmosphere.Text.Trim();
            fL_Customers.ParentsWish = txtParentsWish.Text.Trim();
            fL_Customers.Hobbies = txtHobbies.Text.Trim();


            //20140521新添字段
            fL_Customers.BrideHobby = txtBrideHobby.Text;
            fL_Customers.GroomHobby = txtGroomHobby.Text;
            fL_Customers.IntentStyle = txtIntentStyle.Text;
            fL_Customers.GroomHobby = txtGroomHobby.Text;
            fL_Customers.KonwSite = txtKonwSite.Text;
            fL_Customers.HowKonw = txtHowKonw.Text;
            fL_Customers.Eldersrequirements = txtEldersrequirements.Text;
            fL_Customers.BlindDateSite = txtBlindDateSite.Text;
            fL_Customers.ApplyType = ddlApplyType.SelectedValue.ToInt32();


            Report ObjReportBLL = new Report();
            var ObjUpdateReportModel = ObjReportBLL.GetByCustomerID(fL_Customers.CustomerID, User.Identity.Name.ToInt32());
            switch (rdoCustomertype.Text)
            {
                case "新郎":
                    ObjUpdateReportModel.ContactMan = fL_Customers.Groom;
                    ObjUpdateReportModel.ContactPhone = fL_Customers.GroomCellPhone;
                    ObjUpdateReportModel.ContactType = 0;
                    break;
                case "新娘":
                    ObjUpdateReportModel.ContactMan = fL_Customers.Bride;
                    ObjUpdateReportModel.ContactPhone = fL_Customers.BrideCellPhone;
                    ObjUpdateReportModel.ContactType = 1;
                    break;
                case "经办人":
                    ObjUpdateReportModel.ContactMan = fL_Customers.Operator;
                    ObjUpdateReportModel.ContactPhone = fL_Customers.OperatorPhone;
                    ObjUpdateReportModel.ContactType = 2;
                    break;
            }
            ObjUpdateReportModel.Partydate = txtPartyDay.Text.ToDateTime();
            ObjReportBLL.Update(ObjUpdateReportModel);
            customerBLL.Update(fL_Customers);

            if (Request["Type"] == "QuotedPrice")
            {
                JavaScriptTools.AlertWindowAndLocation("修改成功!", "/AdminPanlWorkArea/QuotedPrice/QuotedPriceList.aspx?NeedPopu=1", Page);
            }
            else
            {
                JavaScriptTools.AlertWindowAndLocation("修改成功!", "CustomerInfoModifyManger.aspx", Page);
            }
        }
        #endregion

        #region 根据渠道类型ID获取渠道类型名称
        private string GetChannelTypeName(int ChannelTypeId)
        {
            HA.PMS.DataAssmblly.FD_ChannelType channelType = new BLLAssmblly.FD.ChannelType().GetByID(ChannelTypeId);
            return object.ReferenceEquals(channelType, null) ? string.Empty : channelType.ChannelTypeName;
        }
        #endregion

        #region 根据EmployeeID获取员工名称
        private string GetEmployeeName(int EmployeeID)
        {
            HA.PMS.DataAssmblly.Sys_Employee sys_Employee = new HA.PMS.BLLAssmblly.Sys.Employee().GetByID(EmployeeID);
            if (sys_Employee != null)
            {
                return sys_Employee.EmployeeName;
            }
            return string.Empty;
            //return object.ReferenceEquals(sys_Employee, null) ? sys_Employee.EmployeeName : string.Empty;
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 修改完成之后
        /// </summary> 
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            CreateHandle();
            UpdateCustomer(Request.QueryString["CustomerID"].ToInt32());
        }
        #endregion

        #region 渠道选择   类型
        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelName.BindByParent(ddlChannelType.SelectedValue.ToInt32());
        }
        #endregion

        #region 渠道选择  名称
        protected void ddlChannelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChannelName.SelectedItem.Text == "手动录入")
            {

                ddlReferee.Visible = false;
                ddlReferee.Items.Clear();
                txtReffer.Visible = true;
            }
            else
            {
                ddlReferee.BinderbyChannel(ddlChannelName.SelectedValue.ToInt32());
                txtReffer.Visible = false;
                ddlReferee.Visible = true;
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
            string CustomerName = txtBride.Text.Trim().ToString() == string.Empty ? txtGroom.Text.Trim().ToString() : txtBride.Text.Trim().ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "客户入口,修改客户信息:" + CustomerName;
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