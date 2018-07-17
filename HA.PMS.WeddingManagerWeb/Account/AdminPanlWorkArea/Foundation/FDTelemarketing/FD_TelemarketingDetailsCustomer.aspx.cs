using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.FD;
using System.Text;
using System.IO;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{
    public partial class FD_TelemarketingDetailsCustomer : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        ChannelType objChannelTypeBLL = new ChannelType();
        SaleSources objSaleSourcesBLL = new SaleSources();
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();
        Employee ObjEmployeeBLL = new Employee();
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        HA.PMS.BLLAssmblly.Report.Report ObjReportBLL = new BLLAssmblly.Report.Report();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        string OrderByName = "CreateDate";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlChannelName1.Items.Clear();
                ddlState.Items.Insert(0, new ListItem { Text = "未流失", Value = "560" });
                BinderData();
            }
        }

        protected void BinderData()
        {
            #region 查询参数
            var objParmList = new List<PMSParameters>();

            if (ddlState.SelectedItem != null)
            {
                if (ddlState.SelectedItem.Text == "未流失")
                {
                    objParmList.Add(ddlState.SelectedItem.Value.ToInt32() > 0, "State", "7,10,20,29", NSqlTypes.NotIN);
                }
                else
                {
                    objParmList.Add(ddlState.SelectedItem.Value.ToInt32() > 0, "State", ddlState.SelectedItem.Value.ToInt32());
                }

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

            if (ddltimerType.SelectedValue == "3" && DateRanger.IsNotBothEmpty)         //最后沟通时间
            {
                objParmList.Add("AgainDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "AgainDate";
            }

            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(objParmList, "CreateEmployee");         //录入人
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
            ltlCurrentCustomerCount.Text = TelemarketingPager.RecordCount + string.Empty;
            ltlCurrentInviteSuccess.Text = SSResualtList.Count(C => C.State > 3 && C.InviteSucessDate != null).ToString();
            ltlCurrentClinchaDeal.Text = SSResualtList.Count(C => C.OrderSucessDate != null && C.InviteSucessDate != null).ToString();
            if (ltlCurrentCustomerCount.Text.ToDecimal() > 0)
            {
                ltlCurrentInviteSuccessRate.Text = (ltlCurrentInviteSuccess.Text.ToDecimal() / ltlCurrentCustomerCount.Text.ToDecimal()).ToString("0.00%");
                ltlClinchaDealRate.Text = (ltlCurrentClinchaDeal.Text.ToDecimal() / ltlCurrentCustomerCount.Text.ToDecimal()).ToString("0.00%");
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            TelemarketingPager.CurrentPageIndex = 1;
            BinderData();
        }

        protected void TelemarketingPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

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



        protected void ddlChooseYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnDownLoadReport_Click(object sender, EventArgs e)
        {


        }
    }
}