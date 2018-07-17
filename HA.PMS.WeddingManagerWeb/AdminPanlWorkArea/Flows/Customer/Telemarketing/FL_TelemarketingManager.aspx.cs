using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.Telemarketing
{
    public partial class FL_TelemarketingManager : HA.PMS.Pages.SystemPage
    {
        Customers ObjCustomers = new Customers();
        Employee objEmployeeBLL = new Employee();
        Customers objCustomersBLL = new Customers();
        SaleSources objSaleSourcesBLL = new SaleSources();
        //任务计划统一时间
        DateTime StarDate = DateTime.Now;

        MissionManager ObjMissManagerBLL = new MissionManager();
        /// <summary>
        /// 渠道类型
        /// </summary>
        ChannelType objChannelTypeBLL = new ChannelType();
        /// <summary>
        /// 任务详情
        /// </summary>
        MissionDetailed ObjMissionDetailedBLl = new BLLAssmblly.Flow.MissionDetailed();

        HA.PMS.BLLAssmblly.Flow.Telemarketing ObjTelemarketingBLL = new BLLAssmblly.Flow.Telemarketing();

        //电话营销 功能类
        HA.PMS.BLLAssmblly.Flow.Telemarketing ObjTelemarketingsBLL = new BLLAssmblly.Flow.Telemarketing();

        /// <summary>
        /// 邀约操作
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                BinderData();


                HiddenField hfUrl = Master.FindControl("hfUrl") as HiddenField;
                hfUrl.Value = EncodeBase64("/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=");
            }
        }
        #endregion

        #region 获取邀约人姓名 根据客户ID
        /// <summary>
        /// 获取邀约人
        /// </summary> 
        public string GetIntiveEmployeeName(object CustomerID)
        {
            if (CustomerID != null)
            {
                return ObjInviteBLL.GetIntiveEmployeeName(CustomerID.ToString().ToInt32());
            }
            else
            {
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
        }
        #endregion

        #region 渠道类型 选择变化
        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelName.Items.Clear();
            if (ddlChannelType.SelectedValue.ToInt32() == -1)
            {

                ListItem currentList = ddlChannelName.Items.FindByValue("0");
                if (currentList != null)
                {
                    currentList.Selected = true;
                }
            }
            else
            {
                ddlChannelName.BindByParent(ddlChannelType.SelectedValue.ToInt32());
                ddlChannelName.Items.Add(new ListItem("自己收集", "自己收集"));
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();

            //按渠道类型查询
            if (ddlChannelType.SelectedItem.Text != "无")
            {
                ObjParameterList.Add("ChannelType", ddlChannelType.SelectedItem.Value);
            }
            //按渠道名称查询
            if (ddlChannelName.SelectedItem != null)
            {
                //fL_GetTelmarketingCustomers.Channel = ddlChannelName.SelectedItem.Text;
                //fL_GetTelmarketingCustomers.ChannelType = ddlChannelType.SelectedValue.ToInt32();
                //添加查询参数
                if (ddlChannelName.SelectedItem.Text != "请选择")
                {
                    ObjParameterList.Add("Channel", ddlChannelName.SelectedItem.Text, NSqlTypes.StringEquals);
                }
            }

            //按状态查询
            if (ddlCustomersState.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add(ddlCustomersState.SelectedValue.ToInt32() > 0, "State", ddlCustomersState.SelectedValue.ToInt32());
            }
            else
            {
                ObjParameterList.Add("State", (int)CustomerStates.DidNotInvite + "," + (int)CustomerStates.DoInvite + "," + (int)CustomerStates.InviteSucess, NSqlTypes.IN);
            }

            //按邀约人查询
            if (ddlMyManagerEmployee.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add("EmployeeID", ddlMyManagerEmployee.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                int EmployeeID = User.Identity.Name.ToInt32();

                ObjParameterList.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);

            }

            //按新人姓名查询
            if (txtBrideName.Text != string.Empty)
            {
                ObjParameterList.Add("ContactMan", txtBrideName.Text, NSqlTypes.LIKE);
            }



            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                ObjParameterList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按录入时间查询
            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                ObjParameterList.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按录入人查询 
            if (ddlCreateEmployee.SelectedItem.Value.ToInt32() > 0)
            {
                ObjParameterList.Add("CreateEmpLoyee", ddlCreateEmployee.SelectedItem.Value.ToInt32(), NSqlTypes.Equal);
            }

            int resourceCount = 0;
            var query = ObjTelemarketingBLL.GetByWhereParameter(ObjParameterList, "CreateDate", TelemarketingPager.PageSize, TelemarketingPager.CurrentPageIndex, out resourceCount);
            TelemarketingPager.RecordCount = resourceCount;

            RptTelemarketing.DataSource = query;
            RptTelemarketing.DataBind();

            //  employeeChoose.Visible = true;
        }
        #endregion

        #region 下拉框绑定 员工
        /// <summary>
        /// 绑定所有下拉框选项
        /// </summary>
        protected void DataDropDownList()
        {
            #region
            ////渠道名称绑定
            //var query = objCustomersBLL.GetByAll();
            //ddlChannelName.DataSource = query.Distinct(new FL_CustomersChannelComparer())
            //    .Select(C => C.Channel);
            //ddlChannelName.DataBind();

            //ddlChannelName.Items.Add(new ListItem("请选择", "0"));
            //ddlChannelName.SelectedIndex = ddlChannelName.Items.Count - 1;


            ////渠道类型绑定
            //ddlChannelType.DataSource = objChannelTypeBLL.GetByAll()
            //    .Where(C => !string.IsNullOrEmpty(C.ChannelTypeName));
            //ddlChannelType.DataTextField = "ChannelTypeName";
            //ddlChannelType.DataValueField = "ChannelTypeId";
            //ddlChannelType.DataBind();
            //ddlChannelType.Items.Add(new ListItem("请选择", "0"));
            //ddlChannelType.SelectedIndex = ddlChannelType.Items.Count - 1;
            #endregion
            ////渠道联系人绑定
            #region
            //var querySaleSources = objSaleSourcesBLL.GetByAll();
            //foreach (var item in querySaleSources)
            //{
            //    if (!string.IsNullOrEmpty(item.Tactcontacts1))
            //    {
            //        ddlTactcontacts.Items.Add(new ListItem(item.Tactcontacts1, "1"));
            //    }
            //    if (!string.IsNullOrEmpty(item.Tactcontacts2))
            //    {
            //        ddlTactcontacts.Items.Add(new ListItem(item.Tactcontacts2, "1"));
            //    }
            //    if (!string.IsNullOrEmpty(item.Tactcontacts3))
            //    {
            //        ddlTactcontacts.Items.Add(new ListItem(item.Tactcontacts3, "1"));
            //    }
            //}
            //ddlTactcontacts.Items.Add(new ListItem("请选择", "0"));
            //ddlTactcontacts.SelectedIndex = ddlTactcontacts.Items.Count - 1;
            //ddlTactcontacts.DataBind();
            #endregion
            //员工绑定
            ddlEmployee.DataSource = objEmployeeBLL.GetByAll();
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();

            ddlCustomersState.DataBinders();
        }
        #endregion

        #region 批量 隐藏、无用
        /// <summary>
        /// 批量改派
        /// </summary>    
        protected void btnBatchAssign_Click(object sender, EventArgs e)
        {
            chkAll.Checked = true;
            //更改Repeater中所有的复选框被选中
            for (int i = 0; i < RptTelemarketing.Items.Count; i++)
            {
                CheckBox chk = RptTelemarketing.Items[i].FindControl("chkSinger") as CheckBox;
                if (chk != null)
                {
                    chk.Checked = true;
                }

            }
            //  employeeChoose.Visible = true;
        }
        #endregion

        #region 单独改派  会员标志
        /// <summary>
        /// 单独改派
        /// </summary> 
        protected void RepTelemarketing_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HidesCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }

            //对应的视图对象
            View_GetTelmarketingCustomers tel;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lkbtnAssign = e.Item.FindControl("lkbtnAssign") as LinkButton;
                LinkButton lkbtnReassignment = e.Item.FindControl("lkbtnReassignment") as LinkButton;
                tel = (View_GetTelmarketingCustomers)e.Item.DataItem;

                //lkbtnAssign  lkbtnReassignment
                //如果当前行的State大于1的话就代表已经被分派过了，相反就代表是录入 新录入 没有被分派过
                if (tel.State > 1)
                {
                    //分派隐藏
                    lkbtnAssign.Visible = false;
                    lkbtnReassignment.OnClientClick = "ShowWindowsPopu(" + tel.MarkeID + ",this,1)";

                }
                else
                {
                    //改派隐藏
                    lkbtnReassignment.Visible = false;
                    lkbtnAssign.OnClientClick = "ShowWindowsPopu(" + tel.MarkeID + ",this,0)";

                }

            }


        }
        #endregion

        #region 修改方法
        /// <summary>
        /// 保存修改方法
        /// </summary>
        protected void SaveUpdate(FL_Telemarketing fL_Telemarketing)
        {
            FL_Telemarketing fl = ObjTelemarketingsBLL.GetByID(fL_Telemarketing.MarkeID);

            fl.EmployeeID = fL_Telemarketing.EmployeeID;
            fl.SortOrder = fL_Telemarketing.SortOrder;
            //fl.CreateDate = fL_Telemarketing.CreateDate;
            fl.CreateEmpLoyee = fL_Telemarketing.CreateEmpLoyee;
            int result = ObjTelemarketingsBLL.Update(fl);


            if (result > 0)
            {
                JavaScriptTools.AlertWindow("操作成功", this.Page);
                ///添加庆典任务到任务列表
                //FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();
                //ObjMissManagerBLL.WeddingMissionCreate(1, (int)MissionTypes.Tel, StarDate, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), fL_Telemarketing.MarkeID);
                BinderData();
            }
            else
            {
                JavaScriptTools.AlertWindow("操作失败,请重新尝试", this.Page);

            }
        }
        #endregion

        #region 保存事件  隐藏、无用
        /// <summary>
        /// 保存事件
        /// </summary>
        protected void btnSaveUpdate_Click(object sender, EventArgs e)
        {

            //查出最大的批次
            int maxSortOrder = ObjTelemarketingsBLL.GetMaxSortOrder();
            //结果集
            FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();

            for (int i = 0; i < RptTelemarketing.Items.Count; i++)
            {
                //循环当前行
                var currentItem = RptTelemarketing.Items[i];
                CheckBox chkSinger = currentItem.FindControl("chkSinger") as CheckBox;
                if (chkSinger.Checked)
                {
                    LinkButton lkbtnAssign = currentItem.FindControl("lkbtnAssign") as LinkButton;
                    int MarkeID = lkbtnAssign.CommandArgument.ToInt32();
                    FL_Telemarketing fL_Telemarketing = new FL_Telemarketing();
                    int EmpLoyeeID = User.Identity.Name.ToInt32();
                    fL_Telemarketing.EmployeeID = EmpLoyeeID;
                    fL_Telemarketing.MarkeID = MarkeID;
                    fL_Telemarketing.SortOrder = maxSortOrder + 1;
                    fL_Telemarketing.ChangeDate = DateTime.Now;
                    //创建人是他自己
                    fL_Telemarketing.CreateEmpLoyee = EmpLoyeeID;
                    //修改记录
                    SaveUpdate(fL_Telemarketing);
                    // ObjMissManagerBLL.WeddingMissionCreate("电话营销任务", "电话营销", StarDate, fL_Telemarketing.EmployeeID.Value, "?CustomerID=" + fL_Telemarketing.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager);
                    //ObjMissManagerBLL.WeddingMissionCreate(1, (int)MissionTypes.Tel, StarDate, fL_Telemarketing.EmployeeID.Value, "?CustomerID=" + fL_Telemarketing.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), fL_Telemarketing.MarkeID);
                    UpdateCustomersState(fL_Telemarketing.CustomerID);

                    //修改邀约 改派时间
                    var ObjInviteModel = ObjInviteBLL.GetByCustomerID(fL_Telemarketing.CustomerID);
                    ObjInviteModel.ChangeDate = DateTime.Now;
                    ObjInviteBLL.Update(ObjInviteModel);

                    var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fL_Telemarketing.CustomerID);
                    if (ObjUpdateModel != null)
                    {
                        ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                        ObjInviteBLL.Update(ObjUpdateModel);

                        Report ObjReportBLL = new Report();
                        SS_Report ObjReportModel = new SS_Report();
                        ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                        ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;
                        ObjReportModel.DutyEmployee = ObjUpdateModel.EmpLoyeeID.Value;

                        ObjReportBLL.Update(ObjReportModel);
                    }
                }
            }
        }
        #endregion

        #region 改为相应的状态 邀约/未邀约
        /// <summary>
        /// 修改客户信息为未邀约
        /// </summary>
        private void UpdateCustomersState(int? CustomerID)
        {

            //修改客户基础信息表记录
            var ObjCustomersModel = ObjCustomers.GetByID(CustomerID);
            if (ObjCustomersModel.State <= 3)
            {
                ObjCustomersModel.State = (int)CustomerStates.DidNotInvite;
                ObjCustomers.Update(ObjCustomersModel);
            }
            else
            {
                ObjCustomersModel.State = (int)CustomerStates.DoInvite;
                ObjCustomers.Update(ObjCustomersModel);
            }

        }

        #endregion

        #region 批量操作  隐藏、无用
        /// <summary>
        /// 批量操作
        /// </summary>
        protected void lkbtnBatch_Click(object sender, EventArgs e)
        {
            //查出最大的批次
            int maxSortOrder = ObjTelemarketingsBLL.GetMaxSortOrder();
            //结果集
            int result = 0;
            for (int i = 0; i < RptTelemarketing.Items.Count; i++)
            {
                //循环当前行
                var currentItem = RptTelemarketing.Items[i];
                CheckBox chkSinger = currentItem.FindControl("chkSinger") as CheckBox;
                if (chkSinger.Checked)
                {
                    LinkButton lkbtnAssign = currentItem.FindControl("lkbtnAssign") as LinkButton;
                    int MarkeID = lkbtnAssign.CommandArgument.ToInt32();
                    FL_Telemarketing fl = ObjTelemarketingsBLL.GetByID(MarkeID);
                    int EmpLoyeeID = User.Identity.Name.ToInt32();
                    fl.CreateEmpLoyee = EmpLoyeeID;
                    fl.SortOrder = maxSortOrder;
                    fl.EmployeeID = ddlEmployee.SelectedValue.ToInt32();
                    //fl.CreateDate = DateTime.Now;
                    result = ObjTelemarketingsBLL.Update(fl);
                    //ObjMissManagerBLL.WeddingMissionCreate("电话营销任务", "电话营销", StarDate, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager);
                    //ObjMissManagerBLL.WeddingMissionCreate(1, (int)MissionTypes.Tel, StarDate, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), MarkeID);

                    UpdateCustomersState(fl.CustomerID);

                    var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);
                    if (ObjUpdateModel != null)
                    {
                        ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                        ObjInviteBLL.Update(ObjUpdateModel);

                        Report ObjReportBLL = new Report();
                        SS_Report ObjReportModel = new SS_Report();
                        ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                        ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;


                        ObjReportBLL.Update(ObjReportModel);
                    }
                }
            }

            if (result > 0)
            {
                JavaScriptTools.AlertWindow("操作成功", this.Page);
                BinderData();
                chkAll.Checked = false;
            }
            else
            {
                JavaScriptTools.AlertWindow("操作失败,请重新尝试", this.Page);

            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询操作
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页操作
        /// </summary>
        protected void TelemarketingPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 派给自己 批量
        /// <summary>
        /// 批量保存 派给自己
        /// </summary>
        protected void btnMine_Click(object sender, EventArgs e)
        {  //查出最大的批次
            int maxSortOrder = ObjTelemarketingsBLL.GetMaxSortOrder();
            //结果集
            int result = 0;
            for (int i = 0; i < RptTelemarketing.Items.Count; i++)
            {
                //循环当前行
                var currentItem = RptTelemarketing.Items[i];
                CheckBox chkSinger = currentItem.FindControl("chkSinger") as CheckBox;
                if (chkSinger.Checked)
                {
                    HiddenField lkbtnAssign = currentItem.FindControl("hideCustomerID") as HiddenField;
                    int MarkeID = lkbtnAssign.Value.ToInt32();
                    FL_Telemarketing fl = ObjTelemarketingsBLL.GetByID(MarkeID);
                    int EmpLoyeeID = User.Identity.Name.ToInt32();
                    fl.SortOrder = maxSortOrder;
                    fl.EmployeeID = EmpLoyeeID;
                    //fl.CreateDate = DateTime.Now;
                    result = ObjTelemarketingsBLL.Update(fl);

                    //ObjMissManagerBLL.WeddingMissionCreate("电话营销任务", "电话营销", StarDate, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager);
                    //  ObjMissManagerBLL.WeddingMissionCreate(fl.CustomerID, 1, (int)MissionTypes.Order, DateTime.Now, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID, MissionChannel.StarOrder, fl.EmployeeID.Value, fl.MarkeID);
                    UpdateCustomersState(fl.CustomerID);

                    var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);
                    if (ObjUpdateModel != null)
                    {
                        ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                        ObjInviteBLL.Update(ObjUpdateModel);

                        Report ObjReportBLL = new Report();
                        SS_Report ObjReportModel = new SS_Report();
                        ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                        ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;


                        ObjReportBLL.Update(ObjReportModel);
                    }
                }
            }
            BinderData();

            JavaScriptTools.AlertWindow("保存成功", Page);
        }
        #endregion

        #region 派给其他人 批量
        /// <summary>
        /// 批量派给其他人
        /// </summary>
        protected void btnOther_Click(object sender, EventArgs e)
        {
            //查出最大的批次
            int maxSortOrder = ObjTelemarketingsBLL.GetMaxSortOrder();
            //结果集
            int result = 0;
            for (int i = 0; i < RptTelemarketing.Items.Count; i++)
            {
                //循环当前行
                var currentItem = RptTelemarketing.Items[i];
                CheckBox chkSinger = currentItem.FindControl("chkSinger") as CheckBox;
                if (chkSinger.Checked)
                {
                    HiddenField lkbtnAssign = currentItem.FindControl("hideCustomerID") as HiddenField;
                    int MarkeID = lkbtnAssign.Value.ToInt32();
                    FL_Telemarketing fl = ObjTelemarketingsBLL.GetByID(MarkeID);
                    int EmpLoyeeID = hideEmpLoyeeID.Value.ToInt32();
                    fl.SortOrder = maxSortOrder;
                    fl.EmployeeID = EmpLoyeeID;
                    //fl.CreateDate = DateTime.Now;
                    result = ObjTelemarketingsBLL.Update(fl);

                    //  //ObjMissManagerBLL.WeddingMissionCreate("电话营销任务", "电话营销", StarDate, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager);
                    //ObjMissManagerBLL.WeddingMissionCreate(1, (int)MissionTypes.Tel, StarDate, fl.EmployeeID.Value, "?CustomerID=" + fl.CustomerID.ToString(), MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), MarkeID);
                    UpdateCustomersState(fl.CustomerID);

                    var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);
                    if (ObjUpdateModel != null)
                    {
                        ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                        ObjInviteBLL.Update(ObjUpdateModel);

                        Report ObjReportBLL = new Report();
                        SS_Report ObjReportModel = new SS_Report();
                        ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                        ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;


                        ObjReportBLL.Update(ObjReportModel);
                    }
                }
            }
            BinderData();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }
        #endregion
    }
}