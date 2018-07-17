
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class RenewInvite : SystemPage
    {
        //电话邀约
        HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();

        //流失客户
        Customers ObjCustomersBLL = new Customers();

        //流失原因
        LoseContent ObjLoseContentBLL = new LoseContent();

        //员工
        Employee ObjEmployeeBLL = new Employee();

        //记录表
        Report ObjReportBLL = new Report();             //记录表

        ///每日报表
        WorkReport ObjWorkBLL = new WorkReport();

        //电话营销 功能类
        HA.PMS.BLLAssmblly.Flow.Telemarketing ObjTelemarketingsBLL = new BLLAssmblly.Flow.Telemarketing();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load加载时绑定数据源
                DataBinder();
            }
        }
        #endregion

        #region 获取流失原因
        /// <summary>
        /// 获取流失原因
        /// </summary>
        public string GetLoseContent(object ContentKey)
        {
            if (ContentKey != null)
            {

                var Objmodel = ObjLoseContentBLL.GetByID(ContentKey.ToString().ToInt32());

                if (Objmodel != null)
                {
                    return Objmodel.Title;
                }
                else
                {
                    return "具体原因不明";
                }
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void DataBinder()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            var objParmList = new List<PMSParameters>();

            int SourceCount = 0;
            //是否按照责任人查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                MyManager.GetEmployeePar(objParmList, "InviteEmployee");
            }
            else
            {
                objParmList.Add("InviteEmployee", MyManager.SelectedValue, NSqlTypes.Equal);
            }

            objParmList.Add("State", "29,7", NSqlTypes.IN);

            //按流失时间查询
            if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                objParmList.Add("LastFollowDate", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime(), NSqlTypes.DateBetween);
            }

            //按渠道名称查询
            if (ddlChannelname.SelectedValue.ToInt32() != 0 && ddlChannelname.SelectedItem != null)
            {
                objParmList.Add("Channel", ddlChannelname.SelectedItem.Text, NSqlTypes.DateBetween);

            }

            //按流失原因查询
            if (ddlLoseContent.SelectedValue.ToInt32() != 0)
            {
                objParmList.Add("ContentID", ddlLoseContent.SelectedValue.ToInt32(), NSqlTypes.Equal);

            }


            //按渠道类型查询
            if (ddlChanneltype.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("ChannelType", ddlChanneltype.SelectedValue.ToInt32(), NSqlTypes.Equal);

            }

            //按新人姓名查询
            if (txtName.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtName.Text, NSqlTypes.LIKE);
                objParmList.Add("Bride", txtName.Text + ",", NSqlTypes.ORLike);
                objParmList.Add("Groom", txtName.Text, NSqlTypes.ORLike);
            }

            //按联系电话查询
            if (txtCellphone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellphone.Text, NSqlTypes.LIKE);

            }


            var DataList = ObjInviteBLL.GetByWhereParameter(objParmList, "LastFollowDate", CtrPager.PageSize, CtrPager.CurrentPageIndex, out SourceCount);
            //CtrPageIndex.RecordCount = SourceCount;
            //repTelemarketingManager.DataSource = DataList;
            //repTelemarketingManager.DataBind();

            CtrPager.RecordCount = SourceCount;
            RptTelemarketing.DataBind(DataList);

            ddlChannelname.Items.Clear();

            //repTelemarketingManager.DataSource = ObjCustomersBLL.GetInviteCustomerByStateIndex();
            //repTelemarketingManager.DataBind();

        }
        #endregion

        #region 绑定完成事件 会员标志
        /// <summary>
        /// 绑定事件
        /// </summary>
        protected void RepTelemarketing_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            ////对应的视图对象
            //View_GetTelmarketingCustomers tel;
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    LinkButton lkbtnAssign = e.Item.FindControl("lkbtnAssign") as LinkButton;
            //    LinkButton lkbtnReassignment = e.Item.FindControl("lkbtnReassignment") as LinkButton;
            //    tel = (View_GetTelmarketingCustomers)e.Item.DataItem;

            //    //lkbtnAssign  lkbtnReassignment
            //    //如果当前行的State大于1的话就代表已经被分派过了，相反就代表是录入 新录入 没有被分派过
            //    if (tel.State > 1)
            //    {
            //        //分派隐藏
            //        lkbtnAssign.Visible = false;
            //        lkbtnReassignment.OnClientClick = "ShowWindowsPopu(" + tel.MarkeID + ",this,1)";

            //    }
            //    else
            //    {
            //        //改派隐藏
            //        lkbtnReassignment.Visible = false;
            //        lkbtnAssign.OnClientClick = "ShowWindowsPopu(" + tel.MarkeID + ",this,0)";

            //    }

            //}
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 渠道类型选择变化事件
        /// <summary>
        /// 渠道类型选择
        /// </summary>    
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelname.Items.Clear();
            ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询数据
        /// </summary>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 恢复邀约
        /// <summary>
        /// 恢复邀约
        /// </summary> 
        protected void repTelemarketingManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //WorkReport ObjWorkBLL = new WorkReport();
            ////获取流失时间 lblInviteEmployee
            //int CustomerID = e.CommandArgument.ToString().ToInt32();
            //var InviteModel = ObjInviteBLL.GetByCustomerID(CustomerID);
            //var Model = ObjWorkBLL.GetEntityByTimeCustomerID(InviteModel.EmpLoyeeID.ToString().ToInt32(), InviteModel.LastFollowDate.ToString().ToDateTime());
            //if (Model == null)
            //{
            //    CreateWorkReports();
            //}
            //Model.LoseInviteNum -= 1;       //生成时间当天的流失量减1 电销量不用手动加1  沟通之后 日报表中会自动增加
            //ObjWorkBLL.Update(Model);


            //FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            //ObjCustomersUpdateModel.State = (int)CustomerStates.DoInvite;//邀约中
            //ObjCustomersBLL.Update(ObjCustomersUpdateModel);
            //JavaScriptTools.AlertWindowAndLocation("已将新人恢复到邀约中", "InviteCommunicationContent.aspx?CustomerID=" + CustomerID, Page);

        }

        protected void RptTelemarketing_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //查出最大的批次
            int maxSortOrder = ObjTelemarketingsBLL.GetMaxSortOrder();
            //结果集
            int result = 0;
            int CustomerID = e.CommandArgument.ToString().ToInt32();
            int EmpLoyeeID = (e.Item.FindControl("hideEmpLoyeeID") as HiddenField).Value.ToString().ToInt32();
            if (EmpLoyeeID.ToString() == "0")
            {
                string EmployeeName = (e.Item.FindControl("txtEmpLoyee") as TextBox).Text.ToString();
                var EmployeeModel = ObjEmployeeBLL.GetByName(EmployeeName);
                EmpLoyeeID = EmployeeModel.EmployeeID;
            }

            FL_Telemarketing fl = ObjTelemarketingsBLL.GetByCustomerID(CustomerID);
            fl.SortOrder = maxSortOrder;
            fl.EmployeeID = EmpLoyeeID;
            result = ObjTelemarketingsBLL.Update(fl);


            //修改邀约 改派时间
            var ObjInviteModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);
            ObjInviteModel.ChangeDate = DateTime.Now;
            ObjInviteModel.EmpLoyeeID = EmpLoyeeID;
            ObjInviteBLL.Update(ObjInviteModel);


            var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);       //邀约
            if (ObjUpdateModel != null)
            {
                ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                ObjInviteBLL.Update(ObjUpdateModel);


                SS_Report ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;

                ObjReportBLL.Update(ObjReportModel);
            }


            FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            ObjCustomersUpdateModel.State = (int)CustomerStates.DoInvite;//邀约中
            ObjCustomersBLL.Update(ObjCustomersUpdateModel);

            DataBinder();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }
        #endregion

        #region 派给自己
        /// <summary>
        /// 批量保存 派给自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    int CustomerID = lkbtnAssign.Value.ToInt32();
                    ReInvite(CustomerID);                   //恢复邀约

                    FL_Telemarketing fl = ObjTelemarketingsBLL.GetByCustomerID(CustomerID);
                    int EmpLoyeeID = User.Identity.Name.ToInt32();
                    fl.SortOrder = maxSortOrder;
                    fl.EmployeeID = EmpLoyeeID;
                    result = ObjTelemarketingsBLL.Update(fl);


                    //修改邀约 改派时间
                    var ObjInviteModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);
                    ObjInviteModel.ChangeDate = DateTime.Now;
                    ObjInviteModel.EmpLoyeeID = EmpLoyeeID;
                    ObjInviteBLL.Update(ObjInviteModel);

                    var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);               //邀约表
                    if (ObjUpdateModel != null)
                    {
                        ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                        ObjInviteBLL.Update(ObjUpdateModel);


                        SS_Report ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                        ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;

                        ObjReportBLL.Update(ObjReportModel);
                    }
                }
            }
            DataBinder();

            JavaScriptTools.AlertWindow("保存成功", Page);
        }
        #endregion

        #region 派给其他人(批量)
        /// <summary>
        /// 批量派给其他人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    int CustomerID = lkbtnAssign.Value.ToInt32();
                    ReInvite(CustomerID);               //恢复邀约

                    FL_Telemarketing fl = ObjTelemarketingsBLL.GetByCustomerID(CustomerID);
                    int EmpLoyeeID = hideEmpLoyeeID.Value.ToInt32();
                    fl.SortOrder = maxSortOrder;
                    fl.EmployeeID = EmpLoyeeID;
                    result = ObjTelemarketingsBLL.Update(fl);

                    //修改邀约 改派时间
                    var ObjInviteModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);
                    ObjInviteModel.ChangeDate = DateTime.Now;
                    ObjInviteModel.EmpLoyeeID = EmpLoyeeID;
                    ObjInviteBLL.Update(ObjInviteModel);

                    var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fl.CustomerID);       //邀约
                    if (ObjUpdateModel != null)
                    {
                        ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                        ObjInviteBLL.Update(ObjUpdateModel);

                        Report ObjReportBLL = new Report();                     //记录表(Report)
                        SS_Report ObjReportModel = new SS_Report();
                        ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                        ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;


                        ObjReportBLL.Update(ObjReportModel);
                    }
                }
            }
            DataBinder();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }
        #endregion

        #region 恢复邀约方法
        /// <summary>
        /// 恢复邀约
        /// </summary>
        public void ReInvite(int CustomerID)
        {
            FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            ObjCustomersUpdateModel.State = (int)CustomerStates.DoInvite;//邀约中
            ObjCustomersBLL.Update(ObjCustomersUpdateModel);
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(FL_Customers ObjCustomersUpdateModel)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            string Customername = ObjCustomersUpdateModel.Bride.ToString() != "" ? ObjCustomersUpdateModel.Bride.ToString() : ObjCustomersUpdateModel.Groom.ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "电话邀约,客户姓名:" + Customername + ",恢复邀约";
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 1;     //电话邀约
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 新增日报表（当天）
        /// <summary>
        /// 新增/修改
        /// </summary>
        public void CreateWorkReports()
        {
            DateTime Start = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime End = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            int Years = Start.Year;
            int Month = Start.Month;
            int Day = Start.Day;

            sys_WorkReport workReport = new sys_WorkReport();
            var EmployeeList = ObjEmployeeBLL.GetByAll();

            foreach (var item in EmployeeList)
            {
                int EmployeeID = item.EmployeeID;
                if (EmployeeID != 1)
                {
                    if (ObjWorkBLL.GetEntityByTimes(EmployeeID, Years, Month, Day) == null)     //该员工的当天汇总不存在 就新增
                    {
                        workReport = new sys_WorkReport();
                        workReport.EmployeeID = item.EmployeeID;                                                            //公司内部员工
                        workReport.InviteSumTotal = ObjCustomersBLL.GetInviteSumTotal(EmployeeID, Start, End);               //潜在客户(邀约)
                        workReport.CreateNum = ObjCustomersBLL.GetNumByToday(EmployeeID, Start, End);                        //新录入
                        workReport.InviteNum = ObjCustomersBLL.GetInviteNumByToday(EmployeeID, Start, End);                  //电销量
                        workReport.LoseInviteNum = ObjCustomersBLL.GetInviteNumByToday(EmployeeID, Start, End, 7);           //流失  (邀约中)
                        workReport.InviteSuccessNum = ObjCustomersBLL.GetInviteNumByToday(EmployeeID, Start, End, 6);        //邀约成功量 
                        workReport.OrderSumTotal = ObjCustomersBLL.GetOrderSumTotal(EmployeeID, Start, End);                 //潜在客户(跟单)
                        workReport.OrderNum = ObjCustomersBLL.GetOrderNumByToday(EmployeeID, Start, End);                    //跟单量
                        workReport.LoseOrderNum = ObjCustomersBLL.GetOrderNumByToday(EmployeeID, Start, End, 10);            //流失量(跟单)
                        workReport.OrderSuccessNum = ObjCustomersBLL.GetOrderNumByToday(EmployeeID, Start, End, 13);         //成功预定
                        workReport.QuotedCheckNum = ObjCustomersBLL.GetQuotedNumByToday(EmployeeID, Start, End);             //获取已签约量
                        workReport.FinishAmount = ObjCustomersBLL.GetFinishAmountByToday(EmployeeID, Start, End);            //获取现金流
                        workReport.OrderAmount = ObjCustomersBLL.GetOrderAmountByToday(EmployeeID, Start, End);              //获取订单金额
                        workReport.CreateDate = DateTime.Now.ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示 这样方便查询
                        workReport.Year = Years;
                        workReport.Month = Month;
                        workReport.Day = Day;
                        workReport.Remark = string.Empty;

                        ObjWorkBLL.Insert(workReport);
                    }
                    else
                    {
                        workReport = ObjWorkBLL.GetEntityByTimes(EmployeeID, Years, Month, Day);
                        workReport.EmployeeID = item.EmployeeID;                                                            //公司内部员工
                        workReport.InviteSumTotal = ObjCustomersBLL.GetInviteSumTotal(EmployeeID, Start, End);               //潜在客户(邀约)
                        workReport.CreateNum = ObjCustomersBLL.GetNumByToday(EmployeeID, Start, End);                        //新录入
                        workReport.InviteNum = ObjCustomersBLL.GetInviteNumByToday(EmployeeID, Start, End);                  //电销量
                        workReport.LoseInviteNum = ObjCustomersBLL.GetInviteNumByToday(EmployeeID, Start, End, 7);           //流失  (邀约中)
                        workReport.InviteSuccessNum = ObjCustomersBLL.GetInviteNumByToday(EmployeeID, Start, End, 6);        //邀约成功量 
                        workReport.OrderSumTotal = ObjCustomersBLL.GetOrderSumTotal(EmployeeID, Start, End);                 //潜在客户(跟单)
                        workReport.OrderNum = ObjCustomersBLL.GetOrderNumByToday(EmployeeID, Start, End);                    //跟单量
                        workReport.LoseOrderNum = ObjCustomersBLL.GetOrderNumByToday(EmployeeID, Start, End, 10);            //流失量(跟单)
                        workReport.OrderSuccessNum = ObjCustomersBLL.GetOrderNumByToday(EmployeeID, Start, End, 13);         //成功预定
                        workReport.QuotedCheckNum = ObjCustomersBLL.GetQuotedNumByToday(EmployeeID, Start, End);         //获取已签约量
                        workReport.FinishAmount = ObjCustomersBLL.GetFinishAmountByToday(EmployeeID, Start, End);            //获取现金流
                        workReport.OrderAmount = ObjCustomersBLL.GetOrderAmountByToday(EmployeeID, Start, End);              //获取订单金额
                        workReport.CreateDate = DateTime.Now.ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示 这样方便查询
                        workReport.Year = Years;
                        workReport.Month = Month;
                        workReport.Day = Day;
                        workReport.Remark = string.Empty;

                        ObjWorkBLL.Update(workReport);
                    }
                }
            }
        }
        #endregion



    }
}