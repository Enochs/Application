using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class Donotinvite : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomersBLL = new Customers();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }

        }

        #region 渠道类型选择 绑定渠道名称
        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlreferrr.Items.Clear();
            ddlChannelname.Items.Clear();
            if (ddlChanneltype.SelectedValue.ToInt32() == -1)
            {
                ListItem currentList = ddlChannelname.Items.FindByValue("0");
                if (currentList != null)
                {
                    currentList.Selected = true;
                }
            }
            else
            {
                ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
            }

            //ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion

        #region 选择渠道 绑定联系人
        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
        protected void ddlChannelname_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlChannelname.SelectedValue.ToInt32() == 0)
            {
                ddlreferrr.Items.Clear();
            }
            else
            {
                ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
            }
            // ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void BinderData()
        {
            //List<ObjectParameter> parameters = new List<ObjectParameter>();
            //parameters.Add(new System.Data.Objects.ObjectParameter("State", (int)CustomerStates.DidNotInvite));
            //parameters.Add(new System.Data.Objects.ObjectParameter("IsDelete", false));

            List<System.Data.Objects.ObjectParameter> parameters = new List<System.Data.Objects.ObjectParameter>();

            var objParmList = new List<PMSParameters>();

            //渠道类型
            if (ddlChanneltype.SelectedItem != null)
            {
                if (ddlChanneltype.SelectedItem.Text != "无")
                {

                    objParmList.Add("ChannelType", ddlChanneltype.SelectedValue.ToString().ToInt32(), NSqlTypes.Equal);
                }
            }

            //渠道
            if (ddlChannelname.SelectedItem != null)
            {
                if (ddlChannelname.SelectedItem.Text != "请选择")
                {
                    objParmList.Add("Channel", ddlChannelname.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                }
            }

            //按照推荐查询
            if (ddlreferrr.Items.Count != 0)
            {
                if (string.IsNullOrEmpty(ddlreferrr.SelectedItem.Text) || ddlreferrr.SelectedItem.Text != "请选择")
                {
                    objParmList.Add("Referee", ddlreferrr.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                }
            }


            //按责任人查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                objParmList.Add("InviteEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            }
            else
            {
                objParmList.Add("InviteEmployee", MyManager.SelectedValue, NSqlTypes.Equal);
            }


            //未邀约 状态
            objParmList.Add("State", ((int)CustomerStates.DidNotInvite), NSqlTypes.Equal);
            objParmList.Add("IsDelete", false, NSqlTypes.Bit);
            //按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //按新人姓名查询
            CstmNameSelector.AppandTo(objParmList);
            //if (txtContactMan.Text != string.Empty)
            //{
            //    objParmList.Add("ContactMan", txtContactMan.Text, NSqlTypes.LIKE);
            //}


            int resourceCounts = 0;
            var query = ObjTelemarketingBLL.GetByWhereParameter1(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out resourceCounts);
            CtrPageIndex.RecordCount = resourceCounts;
            repTelemarketingManager.DataBind(query);
        }
        #endregion

        #region 保存全部
        /// <summary>
        /// 保存全部设置好沟通时间的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// 任务主体操作
            /// </summary>
            MissionManager ObjMissManagerBLL = new MissionManager();
            HtmlInputText ObjTxtDate;
            HiddenField ObjHideKey;
            FL_Invite ObjInvte = new FL_Invite();

            ///邀约沟通记录操作
            InvtieContent ObjInvtieContentBLL = new InvtieContent();
            Telemarketing ObjTelemarketingBLL = new Telemarketing();
            Employee ObjEmployeeBLL = new Employee();
            MissionDetailed ObjMissionBLL = new MissionDetailed();
            for (int i = 0; i < repTelemarketingManager.Items.Count; i++)
            {
                ObjTxtDate = (HtmlInputText)repTelemarketingManager.Items[i].FindControl("txtSetDateTime");
                ObjHideKey = (HiddenField)repTelemarketingManager.Items[i].FindControl("HideKey");
                if (ObjTxtDate.Value.Trim() != string.Empty)
                {
                    ////邀约记录
                    //FL_InvtieContent ObjInvtieContentModel = new FL_InvtieContent();
                    ObjInvte.CustomerID = ObjHideKey.Value.ToInt32();
                    var ObjTelModel = ObjTelemarketingBLL.GetByCustomersId(ObjInvte.CustomerID).First();
                    //ObjInvte.CommunicationTime = ObjTxtDate.Value.ToDateTime();
                    //ObjInvte.CommunicationContent = string.Empty;
                    ObjInvte.EmpLoyeeID = ObjTelModel.EmployeeID;
                    ObjInvte.CreateEmployee = User.Identity.Name.ToInt32();
                    ObjInvte.CreateEmployeeName = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                    ObjInvte.LastFollowDate = ObjTxtDate.Value.ToDateTime();
                    ObjInvte.FllowCount = 0;
                    ObjInvte.ComeDate = DateTime.Now;
                    ObjInvte.CreateDate = DateTime.Now;
                    var ObjCustomer = ObjCustomersBLL.GetByID(ObjInvte.CustomerID);
                    ObjCustomer.State = (int)CustomerStates.DoInvite;
                    ObjCustomersBLL.Update(ObjCustomer);
                    ObjInvtieBLL.Insert(ObjInvte);
                    ObjMissManagerBLL.WeddingMissionCreate(ObjInvte.CustomerID, 1, (int)MissionTypes.Invite, ObjInvte.LastFollowDate.Value, ObjInvte.EmpLoyeeID.Value, "?CustomerID=" + ObjInvte.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, ObjInvte.EmpLoyeeID.Value, ObjInvte.InviteID);
                    // ObjMissionBLL.UpdateforFlow((int)MissionTypes.Tel, ObjTelModel.MarkeID);


                    //修改统计目录
                    Report ObjReportBLL = new Report();
                    SS_Report ObjReportModel = new SS_Report();
                    ObjReportModel = ObjReportBLL.GetByCustomerID(ObjInvte.CustomerID, ObjInvte.EmpLoyeeID.Value);
                    ObjReportModel.InviteEmployee = User.Identity.Name.ToInt32();
                    ObjReportModel.InviteCreateDate = DateTime.Now;
                    ObjReportModel.OrderCreateDate = DateTime.Now;
                    ObjReportModel.State = ObjCustomer.State;
                    ObjReportBLL.Update(ObjReportModel);
                    //开始记录首次沟通记录
                    //ObjInvtieContentModel.InviteID = ObjInvte.InviteID;
                    //ObjInvtieContentModel.EmpLoyeeID = ObjInvte.EmpLoyeeID;
                    //ObjInvtieContentModel.CommunicationTime = ObjTxtDate.Value.ToDateTime();
                    //ObjInvtieContentModel.CommunicationContent = "电话交谈";
                    //ObjInvtieContentModel.CreateDate = DateTime.Now;
                    //ObjInvtieContentModel.CustomerID = ObjInvte.CustomerID;
                    //ObjInvtieContentModel.LoseContent = string.Empty;
                    //ObjInvtieContentModel.State = "初谈";
                    //ObjInvtieContentBLL.Insert(ObjInvtieContentModel);
                }
            }
            JavaScriptTools.AlertWindow("保存完毕", Page);
            BinderData();
        }
        #endregion

        #region 分页 点击查询

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion

        #region Repeater绑定事件
        /// <summary>
        /// 绑定
        /// </summary>
        protected void repTelemarketingManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            /// <summary>
            /// 任务主体操作
            /// </summary>
            MissionManager ObjMissManagerBLL = new MissionManager();

            FL_Invite ObjInvte = new FL_Invite();

            ///邀约沟通记录操作
            InvtieContent ObjInvtieContentBLL = new InvtieContent();
            Telemarketing ObjTelemarketingBLL = new Telemarketing();
            Employee ObjEmployeeBLL = new Employee();
            MissionDetailed ObjMissionBLL = new MissionDetailed();
            int CusTomerID = e.CommandArgument.ToString().ToInt32();
            if (ObjInvtieBLL.GetByCustomerID(CusTomerID) != null)
            {
                var ObjUpdateModel = ObjCustomersBLL.GetByID(CusTomerID);
                ObjUpdateModel.State = (int)CustomerStates.DoInvite;
                ObjCustomersBLL.Update(ObjUpdateModel);
            }
            else
            {  ////邀约记录
                //FL_InvtieContent ObjInvtieContentModel = new FL_InvtieContent();
                ObjInvte.CustomerID = CusTomerID;
                var ObjTelModel = ObjTelemarketingBLL.GetByCustomersId(ObjInvte.CustomerID).First();
                //ObjInvte.CommunicationTime = ObjTxtDate.Value.ToDateTime();
                //ObjInvte.CommunicationContent = string.Empty;
                ObjInvte.EmpLoyeeID = ObjTelModel.EmployeeID;
                ObjInvte.CreateEmployee = User.Identity.Name.ToInt32();
                ObjInvte.CreateEmployeeName = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                ObjInvte.LastFollowDate = DateTime.Now;
                ObjInvte.FllowCount = 0;
                ObjInvte.ComeDate = DateTime.Now;
                ObjInvte.CreateDate = DateTime.Now;
                var ObjCustomer = ObjCustomersBLL.GetByID(ObjInvte.CustomerID);
                ObjCustomer.State = (int)CustomerStates.DoInvite;
                ObjCustomersBLL.Update(ObjCustomer);
                ObjInvtieBLL.Insert(ObjInvte);
                ObjMissManagerBLL.WeddingMissionCreate(ObjInvte.CustomerID, 1, (int)MissionTypes.Invite, ObjInvte.LastFollowDate.Value, ObjInvte.EmpLoyeeID.Value, "?CustomerID=" + ObjInvte.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, ObjInvte.EmpLoyeeID.Value, ObjInvte.InviteID);
                // ObjMissionBLL.UpdateforFlow((int)MissionTypes.Tel, ObjTelModel.MarkeID);


                Report ObjReportBLL = new Report();
                SS_Report ObjReportModel = new SS_Report();
                ObjReportModel = ObjReportBLL.GetByCustomerID(ObjInvte.CustomerID, ObjInvte.EmpLoyeeID.Value);
                ObjReportModel.InviteEmployee = User.Identity.Name.ToInt32();
                ObjReportModel.InviteCreateDate = DateTime.Now;
                ObjReportModel.State = ObjCustomer.State;
                ObjReportBLL.Update(ObjReportModel);

            }

            Response.Redirect("/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CusTomerID);
        }
        #endregion

        #region 会员标志是否显示   绑定事件
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repTelemarketingManager_ItemDataBound(object sender, RepeaterItemEventArgs e)
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