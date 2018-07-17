using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class LoseOrder : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        LoseContent ObjLoseContentBLL = new LoseContent();

        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 日报表(每日)
        /// </summary>
        WorkReport ObjWorkReportBLL = new WorkReport();

        #region 页面加载
        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDataBind();
                BinderData();
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
                    if (ContentKey.ToString() == "-3")
                    {
                        return "渠道信息无效";
                    }
                    else
                    {
                        return "未选择流失原因";
                    }
                }
            }
            else
            {
                return "未选择流失原因";
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的需要派单的 
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add("State", (int)CustomerStates.Lose + ",10", NSqlTypes.IN);
            MyManager.GetEmployeePar(objParmList);
            //按流失愿意查询
            if (ddlLoseContent.SelectedValue.ToInt32() != 0)
            {
                objParmList.Add("ConteenID", ddlLoseContent.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间
            string dateStr = "2100-1-1";
            DateTime endTime = dateStr.ToDateTime();

            if (!string.IsNullOrEmpty(txtStarTime.Text))
            {
                startTime = txtStarTime.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text))
            {
                endTime = txtEndTime.Text.ToDateTime();
            }


            if (ddlType.SelectedValue != "-1")
            {
                //按流失时间查询
                string dateType = "LastFollowDate";

                if (ddlType.SelectedValue == "1")
                {
                    //按婚期查询
                    dateType = "PartyDate";

                }
                objParmList.Add(dateType, startTime + "," + endTime, NSqlTypes.DateBetween);
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
                objParmList.Add("Groom", txtContactMan.Text, NSqlTypes.ORLike);
            }


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "LastFollowDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

        }
        #endregion

        #region 根据客户ID获取订单ID
        /// <summary>
        /// 根据客户ID获取订单ID
        /// </summary>
        public string GetOrderIDByCustomers(object e)
        {
            var ObjCustomer = ObjOrderBLL.GetbyCustomerID(e.ToString().ToInt32());
            if (ObjCustomer != null)
            {
                return ObjCustomer.OrderID.ToString();
            }
            else
            {
                return string.Empty;
            }
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

        #region 查询
        /// <summary>
        /// 查询统计结果
        /// </summary>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion}

        #region 恢复跟单
        /// <summary>
        /// 恢复跟单
        /// </summary>
        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int CustomerID = Convert.ToInt32(e.CommandArgument.ToString());

            FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            switch (e.CommandName)
            {
                case "RecoOrder":
                    DateTime LoseDate = (e.Item.FindControl("lblLoseDate") as Label).Text.Trim().ToDateTime();
                    var OrderModels = ObjOrderBLL.GetbyCustomerID(CustomerID);
                    //var Model = ObjWorkReportBLL.GetEntityByTimeCustomerID(OrderModels.EmployeeID.ToString().ToInt32(), LoseDate);
                    //if (Model == null)
                    //{
                    //    CreateWorkReport(LoseDate);

                    //    var WorkModel = ObjWorkReportBLL.GetEntityByTimeCustomerID(OrderModels.EmployeeID.ToString().ToInt32(), LoseDate);
                    //    WorkModel.LoseOrderNum -= 1;       //生成时间当天的流失量减1 电销量不用手动加1  沟通之后 日报表中会自动增加
                    //    ObjWorkReportBLL.Update(WorkModel);
                    //}
                    //else
                    //{
                    //    Model.LoseOrderNum -= 1;       //生成时间当天的流失量减1 电销量不用手动加1  沟通之后 日报表中会自动增加
                    //    ObjWorkReportBLL.Update(Model);
                    //}


                    ObjCustomersUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;
                    ObjCustomersUpdateModel.IsRescover = 1;
                    ObjCustomersBLL.Update(ObjCustomersUpdateModel);
                    ////JavaScriptTools.AlertWindowAndLocation("已将新人恢复到跟单", "FollowOrderDetails.aspx?CustomerID=" + CustomerID, Page);
                    ////JavaScriptTools.ResponseScript("alert('已将新人恢复到跟单,请在【跟单】中重新跟单');parent.window.location.href = parent.window.location.href", Page);
                    //FL_Order OrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
                    //OrderModel.IsRecovery = 1;
                    //ObjOrderBLL.Update(OrderModel);

                    //操作日志
                    CreateHandle(ObjCustomersUpdateModel);
                    JavaScriptTools.AlertWindow("已将新人恢复到跟单,请在【跟单】中重新跟单", Page);
                    BinderData();
                    break;
                default: break;

            }
        }
        #endregion

        #region 下拉框绑定  流失原因
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        public void ddlDataBind()
        {
            ddlLoseContent.Width = 75;
            LoseContent objLoseContentBLL = new LoseContent();
            ddlLoseContent.DataSource = objLoseContentBLL.GetByType(2);
            ddlLoseContent.DataTextField = "Title";
            ddlLoseContent.DataValueField = "ContentID";
            ddlLoseContent.DataBind();

            ddlLoseContent.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlLoseContent.Items.FindByText("请选择").Selected = true;
        }
        #endregion

        #region 新增日报表
        /// <summary>
        /// 新增/修改
        /// </summary>
        public void CreateWorkReport(DateTime LoseDate)
        {
            DateTime Start = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime End = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            int Years = LoseDate.Year;
            int Month = LoseDate.Month;
            int Day = LoseDate.Day;

            sys_WorkReport workReport = new sys_WorkReport();
            var EmployeeList = ObjEmployeeBLL.GetByAll().ToList();

            foreach (var item in EmployeeList)
            {
                int EmployeeID = item.EmployeeID;
                if (EmployeeID != 1)
                {
                    if (ObjWorkReportBLL.GetEntityByTimes(EmployeeID, Years, Month, Day) == null)     //该员工的当天汇总不存在 就新增
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
                        workReport.CreateDate = LoseDate.ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示 这样方便查询
                        workReport.Year = LoseDate.Year;
                        workReport.Month = LoseDate.Month;
                        workReport.Day = LoseDate.Day;
                        workReport.Remark = string.Empty;

                        ObjWorkReportBLL.Insert(workReport);
                    }
                    else
                    {
                        workReport = ObjWorkReportBLL.GetEntityByTimes(EmployeeID, Years, Month, Day);
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
                        workReport.CreateDate = LoseDate.ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示 这样方便查询
                        workReport.Year = LoseDate.Year;
                        workReport.Month = LoseDate.Month;
                        workReport.Day = LoseDate.Day;
                        workReport.Remark = string.Empty;

                        ObjWorkReportBLL.Update(workReport);
                    }
                }
            }
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(FL_Customers ObjCustomersUpdateModel)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            string Customername = ObjCustomersUpdateModel.Bride.ToString() == "" ? ObjCustomersUpdateModel.Groom.ToString() : ObjCustomersUpdateModel.Bride.ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();

            HandleModel.HandleContent = "销售跟单,客户姓名:" + Customername + ",恢复跟单";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 2;     //销售跟单
            ObjHandleBLL.Insert(HandleModel);
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