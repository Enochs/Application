using System;
using System.Collections.Generic;
using System.Linq;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesCreateManager : HA.PMS.Pages.SystemPage
    {
        SaleSources objSaleSourcesBLL = new SaleSources();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();

        protected bool IsSaleSourcePrivateOpening = false;   //指示自己录入的渠道只有自己和主管可以看见功能是否处于开启状态

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        protected void DataDropDownList()
        {
            //清空选项
            ddlChannelName.Items.Clear();
            ddlChannelName.ClearSelection();

            //频道权限
            IsSaleSourcePrivateOpening = new SysConfig().IsSaleSourcePrivateOpening(User.Identity.Name.ToInt32(), false);

            //如果开启了频道限制功能，上级可以看下级所有，自己可以看。
            if (IsSaleSourcePrivateOpening)
            {
                ddlChannelName.BindSubordinateByParent(ddlChannelType.SelectedValue.ToInt32(), User.Identity.Name.ToInt32());
            }
            else
            {
                ddlChannelName.BindByParent(ddlChannelType.SelectedValue.ToInt32());
            }
        }

        protected void BinderData(object sender, EventArgs e)
        {
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            this.MyManager1.GetEmployeePar(ObjParameterList);
            ObjParameterList.Add(new ObjectParameter("SerchKeypar", "CreateEmpLoyee"));

            //录入时间
            ObjParameterList.Add(DateRanger.IsNotBothEmpty, "CreateDate_between", DateRanger.Start, DateRanger.End);

 


            ObjParameterList.Add(new ObjectParameter("IsDelete", false));

            //新人信息
            int resourceCount = 0;
            var query = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(CustomersPager.PageSize, CustomersPager.CurrentPageIndex, out resourceCount, ObjParameterList.ToArray());
            CustomersPager.RecordCount = resourceCount;
            rptCustomer.DataSource = query.OrderByDescending(C => C.CreateDate);
            rptCustomer.DataBind();

            //渠道类型
            ddlChannelType.DataSource = new ChannelType().GetByAll();
            ddlChannelType.DataTextField = "ChannelTypeName";
            ddlChannelType.DataValueField = "ChannelTypeId";
            ddlChannelType.DataBind();

            //渠道名称
            DataDropDownList();
        }

        protected string GetChannelIDByName(object ChannelName)
        {
            DataAssmblly.FD_SaleSources fD_SaleSources = objSaleSourcesBLL.GetByName(Convert.ToString(ChannelName));
            return fD_SaleSources != null ? fD_SaleSources.SourceID.ToString() : "0";
        }

        /// <summary>
        /// 添加新人
        /// </summary>
        /// <returns></returns>
        protected int AddCustomer()
        {
            #region 新人基本信息
            FL_Customers fl_Coustomers = new FL_Customers();
            fl_Coustomers.Bride = txtGroom.Text;
            fl_Coustomers.BrideBirthday = new DateTime(1949, 10, 1);
            fl_Coustomers.Groom = string.Empty;
            fl_Coustomers.GroomBirthday = new DateTime(1949, 10, 1);
            if (DdlReferee1.Items.Count > 0)
            {
                fl_Coustomers.Referee = DdlReferee1.SelectedItem.Text;
            }
            fl_Coustomers.BrideCellPhone = txtGroomCellPhone.Text;
            fl_Coustomers.GroomCellPhone = string.Empty;
            fl_Coustomers.Operator = string.Empty;
            fl_Coustomers.OperatorRelationship = string.Empty;
            fl_Coustomers.OperatorPhone = string.Empty;
            fl_Coustomers.PartyBudget = 0;
            fl_Coustomers.IsLose = false;
            fl_Coustomers.BrideQQ = txtQQ.Text;
            fl_Coustomers.PartyDate = txtPartyDate.Text.ToDateTime(DateTime.Parse("1949-10-01"));
            fl_Coustomers.ChannelType = ddlChannelType.SelectedValue.ToInt32();
            if (ddlChannelName.SelectedValue != "0")
            {
                fl_Coustomers.Channel = ddlChannelName.SelectedItem.Text;
            }
            fl_Coustomers.LikeColor = string.Empty;
            fl_Coustomers.ExpectedAtmosphere = string.Empty;
            fl_Coustomers.Hobbies = string.Empty;
            fl_Coustomers.NoTaboos = string.Empty;
            fl_Coustomers.WeddingServices = string.Empty;
            fl_Coustomers.ImportantProcess = string.Empty;
            fl_Coustomers.Experience = string.Empty;
            fl_Coustomers.DesiredAppearance = string.Empty;
            fl_Coustomers.Wineshop = ddlHotel.SelectedItem.Text;
            fl_Coustomers.State = (int)CustomerStates.New;
            fl_Coustomers.CustomerStatus = CustomerState.GetEnumDescription(CustomerStates.New);
            fl_Coustomers.FormMarriage = string.Empty;
            fl_Coustomers.IsDelete = false;
            fl_Coustomers.TimeSpans = ddlTimerSpan.SelectedItem.Text;
            fl_Coustomers.Other = txtOther.Text;
            fl_Coustomers.Recorder = User.Identity.Name.ToInt32();
            fl_Coustomers.RecorderDate = DateTime.Now;
            if (new UserJurisdiction().CheckByClassType("InviteAdminPanel", User.Identity.Name.ToInt32()))
            {
                fl_Coustomers.State = (int)CustomerStates.DidNotInvite;
            } 
            #endregion

            int CustomerID = new Customers().Insert(fl_Coustomers);

            if (CustomerID > 0 && ddlChannelName.SelectedValue != "0")
            {
                #region 返利录入
                FD_PayNeedRabate ObjPayNeedRabateModel = new FD_PayNeedRabate();
                SaleSources ObjSaleSourcesBLL = new SaleSources();
                var ObjSalesalceModel = ObjSaleSourcesBLL.GetByName(fl_Coustomers.Channel);
                if (ObjSalesalceModel != null)
                {
                    if (ObjSalesalceModel.NeedRebate.Value)
                    {
                        ObjPayNeedRabateModel.CustomerID = fl_Coustomers.CustomerID;
                        ObjPayNeedRabateModel.PartyDay = fl_Coustomers.PartyDate;
                        ObjPayNeedRabateModel.IsFinish = false;
                        ObjPayNeedRabateModel.IsDelete = false;
                        ObjPayNeedRabateModel.State = fl_Coustomers.State;
                        ObjPayNeedRabateModel.ChannelTypeId = fl_Coustomers.ChannelType;
                        ObjPayNeedRabateModel.SourceID = ObjSalesalceModel.SourceID;
                        ObjPayNeedRabateModel.Paypolicy = ObjSalesalceModel.Rebatepolicy;
                        ObjPayNeedRabateModel.MoneyPerson = DdlReferee1.SelectedItem.Text;

                        ObjPayNeedRabateModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                        new PayNeedRabate().Insert(ObjPayNeedRabateModel);
                    }
                } 
                #endregion
            }
            return CustomerID;
        }

        /// <summary>
        /// 添加到电话销售
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        protected int AddToTelemarketing(int CustomerID)
        {
            if (CustomerID > 0)
            {
                int employeeId = User.Identity.Name.ToInt32();
                FL_Telemarketing telemarketing = new FL_Telemarketing();
                telemarketing.EmployeeID = employeeId;
                telemarketing.CreateEmpLoyee = employeeId;
                telemarketing.CustomerID = CustomerID;
                telemarketing.SortOrder = ObjTelemarketingBLL.GetMaxSortOrder() + 1; //获取最大批次量返回时加 一 更新批次量
                telemarketing.CreateDate = DateTime.Now;
                return ObjTelemarketingBLL.Insert(telemarketing);
            }
            return 0;
        }

        /// <summary>
        /// 创建统计目录
        /// </summary>
        /// <param name="CustomerID"></param>
        protected void AddToReport(int CustomerID)
        {
            SS_Report ObjReportModel = new SS_Report();
            ObjReportModel.Channel = ddlChannelName.Text;
            ObjReportModel.ChannelType = ddlChannelType.SelectedValue.ToInt32();
            ObjReportModel.CreateEmployee = User.Identity.Name.ToInt32();
            ObjReportModel.CustomerID = CustomerID;
            ObjReportModel.CreateDate = DateTime.Now;
            if (txtPartyDate.Text != string.Empty)
            {
                ObjReportModel.Partydate = txtPartyDate.Text.ToDateTime();
            }
            new Report().Insert(ObjReportModel);
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (string.IsNullOrWhiteSpace(txtGroom.Text))
            {
                JavaScriptTools.AlertWindow("新人姓名不能为空！", Page); return;
            }
            if (ddlChannelType.Items.Count <= 0 || ddlChannelName.SelectedValue == "0")
            {
                JavaScriptTools.AlertWindow("请完善渠道信息！", Page); return;
            }
            FL_Customers fL_Customers = new Customers().Where(C => C.BrideCellPhone == txtGroomCellPhone.Text).FirstOrDefault();
            if (fL_Customers != null)
            {
                JavaScriptTools.AlertWindow(string.Format("此客户已经存在，责任人：{0}，所在渠道:{1}", GetEmployeeName(fL_Customers.Recorder), fL_Customers.Channel), Page);
                return;
            } 
            #endregion

            //1.添加新人
            int CustomerID = AddCustomer();
            //2.添加到电话销售
            int MarkID = AddToTelemarketing(CustomerID);

            if (MarkID > 0)
            {
                //3.创建统计目录
                AddToReport(CustomerID);
                BinderData(sender, e);
                DdlReferee1.Items.Clear();
                ClearText();
                JavaScriptTools.AlertWindow("添加成功！", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("添加失败，请重试！", Page);
            
            }
        }

        protected void ClearText()
        {
            ddlChannelType.ClearSelection();
            ddlChannelName.ClearSelection();
            ddlHotel.ClearSelection();
            txtGroom.Text = string.Empty;
            txtGroomCellPhone.Text = string.Empty;
            txtPartyDate.Text = string.Empty;
            txtOther.Text = string.Empty;
            txtQQ.Text = string.Empty;
        }

        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList();
            DdlReferee1.Items.Clear();
        }

        protected void ddlChannelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlReferee1.BinderbyChannel(ddlChannelName.SelectedValue.ToInt32());
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            CustomersPager.CurrentPageIndex = 1;
            BinderData(sender, e);
        }
    }
}