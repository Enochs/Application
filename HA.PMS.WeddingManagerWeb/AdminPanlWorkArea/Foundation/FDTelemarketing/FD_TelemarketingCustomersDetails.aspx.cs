/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.2
 Description:客户明细界面
 History:修改日志

 Author:杨洋
 Date:2013.4.2
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.FD;
using System.Text;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{
    public partial class FD_TelemarketingCustomersDetails : SystemPage
    {
        /// <summary>
        /// 客户
        /// </summary>
        Customers objCustomersBLL = new Customers();

        /// <summary>
        /// 渠道类型
        /// </summary>
        ChannelType objChannelTypeBLL = new ChannelType();

        /// <summary>
        /// 渠道
        /// </summary>
        SaleSources objSaleSourcesBLL = new SaleSources();
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();

        /// <summary>
        /// 员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 电话营销
        /// </summary>
        Telemarketing ObjTelemarketingBLL = new Telemarketing();

        /// <summary>
        /// 系统各种合计
        /// </summary>
        HA.PMS.BLLAssmblly.Report.Report ObjReportBLL = new BLLAssmblly.Report.Report();

        /// <summary>
        /// 收款
        /// </summary>
        QuotedCollectionsPlan ObjQuotedCollectionPlanBLL = new QuotedCollectionsPlan();

        /// <summary>
        /// 报价单
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        string OrderByName = "CreateDate";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlChannelName1.Items.Clear();
                BinderData();
            }
        }

        protected void BinderData()
        {
            #region 查询参数
            var objParmList = new List<PMSParameters>();

            //状态
            if (ddlState.SelectedItem != null)
            {
                objParmList.Add(ddlState.SelectedItem.Value.ToInt32() > 0, "State", ddlState.SelectedItem.Value.ToInt32());

            }

            //渠道类型
            if (ddlChanneType.SelectedItem != null)
            {
                objParmList.Add(ddlChanneType.SelectedItem.Value.ToInt32() > 0, "ChannelType", ddlChanneType.SelectedItem.Value.ToInt32());

            }

            //渠道名称
            if (DdlChannelName1.SelectedItem != null)
            {
                objParmList.Add(DdlChannelName1.SelectedItem.Value.ToInt32() > 0, "Channel", DdlChannelName1.SelectedItem.Text, NSqlTypes.StringEquals);

            }
            //邀约类型
            if (ddlApplyType.SelectedItem != null)
            {
                objParmList.Add(ddlApplyType.SelectedItem.Value.ToInt32() > 0, "ApplyType", ddlApplyType.SelectedItem.Value.ToInt32());
            }


            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)         //录入时间
            {
                objParmList.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "CreateDate";
            }


            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)         //婚期
            {
                objParmList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "PartyDate";
            }

            if (ddltimerType.SelectedValue == "2" && DateRanger.IsNotBothEmpty)         //签单时间
            {
                objParmList.Add("OrderCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "OrderCreateDate";
            }

            if (ddltimerType.SelectedValue == "3" && DateRanger.IsNotBothEmpty)         //成功预定时间
            {
                objParmList.Add("QuotedCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "QuotedCreateDate";
            }


            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(objParmList, "CreateEmployee");         //录入人
            }
            else
            {
                objParmList.Add("CreateEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);         //录入人
            }

            if (MyManager1.SelectedValue.ToInt32() > 0)
            {
                MyManager1.GetEmployeePar(objParmList, "InviteEmployee");         //电销
            }

            if (txtBrideName.Text != string.Empty)          //联系人
            {
                objParmList.Add("ContactMan", txtBrideName.Text, NSqlTypes.LIKE);
            }

            if (txtCellPhone.Text != string.Empty)          //联系电话
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.LIKE);
            }

            if (txtFollowCount.Text != string.Empty)        //沟通次数
            {
                objParmList.Add("FllowCount", txtFollowCount.Text, NSqlTypes.Equal);
            }



            #endregion

            int resourceCount = 0;
            var query = ObjTelemarketingBLL.GetByWhereParameter1(objParmList, OrderByName, TelemarketingPager.PageSize, TelemarketingPager.CurrentPageIndex, out resourceCount);
            TelemarketingPager.RecordCount = resourceCount;
            rptTelemarketingManager.DataBind(query);


            #region 绑定统计项目


            var SSResualtList = ObjReportBLL.GetReportByWhereClause(objParmList, "CreateDate");
            ltlCurrentCustomerCount.Text = TelemarketingPager.RecordCount + string.Empty;           //客源量
            ltlCurrentInviteSuccess.Text = SSResualtList.Count(C => C.State > 3 && C.InviteSucessDate != null).ToString();      //邀约成功量
            ltlCurrentLoseCount.Text = SSResualtList.Count(C => C.State == 7 || C.State == 10 || C.State == 20 || C.State == 29).ToString();
            ltlCurrentClinchaDeal.Text = SSResualtList.Count(C => C.OrderSucessDate != null).ToString();                //成交量(确认订单)
            if (ltlCurrentCustomerCount.Text.ToDecimal() > 0)
            {
                ltlCurrentInviteSuccessRate.Text = (ltlCurrentInviteSuccess.Text.ToDecimal() / ltlCurrentCustomerCount.Text.ToDecimal()).ToString("0.00%");     //邀约成功率
            }

            if (ltlCurrentInviteSuccess.Text.ToDecimal() > 0)
            {
                ltlClinchaDealRate.Text = (ltlCurrentClinchaDeal.Text.ToDecimal() / ltlCurrentInviteSuccess.Text.ToDecimal()).ToString("0.00%");            //成交率
            }

            ltlCurrentOrderSumMoney.Text = SSResualtList.Where(C => C.QuotedDateSucessDate != null).ToList().Sum(C => C.QuotedMoney).ToString();
            #endregion

        }


        /// <summary>
        /// 算出订单总金额 
        /// </summary>
        /// <param name="currentCustomer"></param>
        /// <returns></returns>
        protected string GetSumOrderMoneyByCustomerId(List<View_GetTelmarketingCustomers> currentCustomer)
        {
            decimal currentSumMoney = 0;
            foreach (var item in currentCustomer)
            {
                currentSumMoney += GetAggregateAmount(item.CustomerID).ToDecimal();
            }
            return currentSumMoney + string.Empty;
        }

        /// <summary>
        /// 获取钱
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetMoney(object Key)
        {
            PayNeedRabate ObjPayNeedRabateBLL = new PayNeedRabate();
            var ObjModel = ObjPayNeedRabateBLL.GetByCustomersID(Key.ToString().ToInt32());
            if (ObjModel == null)
            {
                return string.Empty;
            }
            else
            {
                return ObjModel.PayMoney + string.Empty;
            }

        }

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            TelemarketingPager.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页  上一页/下一页
        /// </summary>
        protected void TelemarketingPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        public string GetChannelNode(object Key, string Type)
        {
            if (Type == "0")
            {
                if (Key != null)
                {
                    var singer = objSaleSourcesBLL.GetByName(Key.ToString());
                    if (singer != null)
                    {
                        return singer.ProlongationDate.ToShortDateString();
                    }
                    else
                    {
                        return GetDateStr(DateTime.Now);
                    }
                }
                else
                {
                    return string.Empty;
                }


            }
            else
            {
                Employee ObjEmployeeBLL = new Employee();
                if (Key != null)
                {
                    var singerSale = objSaleSourcesBLL.GetByName(Key.ToString());
                    if (singerSale != null)
                    {
                        var singerEmp = ObjEmployeeBLL.GetByID(singerSale.ProlongationEmployee);
                        if (singerEmp != null)
                        {
                            return singerEmp.EmployeeName;
                        }
                        else
                        {
                            return string.Empty;
                        }

                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #region 渠道类型 选择变化时间
        /// <summary>
        /// 选择渠道类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlChanneType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlChannelName1.Items.Clear();
            if (ddlChanneType.SelectedValue.ToInt32() == -1)
            {
                ListItem currentList = DdlChannelName1.Items.FindByValue("0");
                if (currentList != null)
                {
                    currentList.Selected = true;
                }
            }
            else
            {
                DdlChannelName1.BindByParent(ddlChanneType.SelectedValue.ToInt32());
            }
        }
        #endregion

        #region 年份选择变化事件
        /// <summary>
        /// 选择变化
        /// </summary>
        protected void ddlChooseYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 导出报表
        /// <summary>
        /// 导出
        /// </summary>
        protected void btnDownLoadReport_Click(object sender, EventArgs e)
        {


        }
        #endregion

        #region 获取收款
        /// <summary>
        /// 获取收款金额
        /// </summary>
        public string GetFinishAmount(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = objQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                var DataList = ObjQuotedCollectionPlanBLL.GetByOrderID(Model.OrderID);
                decimal FinishAmount = 0;
                foreach (var ObjItem in DataList)
                {
                    FinishAmount += ObjItem.RealityAmount.ToString().ToDecimal();
                }
                return FinishAmount.ToString();
            }
            return "0";
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志
        /// </summary> 
        protected void rptTelemarketingManager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
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