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
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using System.Text;
using System.Web.Providers.Entities;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger
{
    public partial class TelemarketingCustomersNumberIndexCount : UserControlTools
    {
        Telemarketing objTelemarketingBLL = new Telemarketing();
        HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();
        PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        protected List<ObjectParameter> GetParameter()
        {
            int EmpLoyeeID =Request.Cookies["HAEmployeeID"].Value.ToInt32();
            List<ObjectParameter> objParameterList = new List<ObjectParameter>();

            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();

            //创建时间
            objParameterList.Add(new ObjectParameter("CreateDate_between", chooseDateStar + "," + chooseDateEnd));


            //上级看下级
            objParameterList.Add(new ObjectParameter("EmpLoyeeID", EmpLoyeeID));
            return objParameterList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem listItem = ddlChooseYear.Items.FindByText(DateTime.Now.Year + "年");
                if (listItem != null)
                {
                    ddlChooseYear.ClearSelection();
                    listItem.Selected = true;
                }

                BinderData();
            }
        }

        protected void BinderData()
        {
            var query = GetPublicDataByParameter();

            CreatePayMoneyMoneyKLine();

            CreateOrderSumMoneyKLine();
            CreateClinchaDealRateKLine(query);
            CreateInviteSuccessRateKLine(query);
            CreateValidRateKLine(query);
            CreateClinchaDealCountKLine(query);
            CreateSuccessCountKLine();
            CreateCustomerCountKLine(query);
            CreateValidateCountKLine(query);
        }

        /// <summary>
        /// 返回公共数据
        /// </summary>
        /// <returns></returns>
        protected List<View_GetTelmarketingCustomers> GetPublicDataByParameter()
        {
            return objTelemarketingBLL.GetTelmarketingCustomersByParameter(GetParameter().ToArray());

        }

        /// <summary>
        /// 创建返利K线图
        /// </summary>
        protected void CreatePayMoneyMoneyKLine()
        {
            //返利总额 
            StringBuilder PayMoneyMoney = new StringBuilder();
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            var objParameterList = GetParameter();
            objParameterList[0] = new ObjectParameter("PartyDay_between", chooseDateStar + "," + chooseDateEnd);
            int resourceCount = 0;
            var objResult = objPayNeedRabateBLL.GetByParaandIndex(objParameterList.ToArray(), 0, 1, out resourceCount);
            for (int i = 1; i <= 12; i++)
            {
                var currentMonth = objResult.Where(C => C.PartyDay.Value.Month == i);
                if (currentMonth != null)
                {
                    PayMoneyMoney.AppendFormat("{0},", currentMonth.Sum(C => C.PayMoney));
                }
                else
                {
                    PayMoneyMoney.AppendFormat("{0},", 0);
                }
            }
            ViewState["PayMoneyMoney"] = GetSubString(PayMoneyMoney.ToString());
        }

        /// <summary>
        /// 创建订单总额K线图
        /// </summary>
        protected void CreateOrderSumMoneyKLine()
        {

            //订单总额
            StringBuilder OrderSumMoney = new StringBuilder();
            int employeeId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            List<ObjectParameter> objParameterList = new List<ObjectParameter>();
            objParameterList.Add(new ObjectParameter("EmpLoyeeID", employeeId));
            string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            objParameterList.Add(new ObjectParameter("PartyDate_between", chooseDateStr + "," + chooseDateEnd));
            var objResult = objQuotedPriceBLL.GetCustomerQuotedParameter(objParameterList);
            for (int i = 1; i <= 12; i++)
            {

                var currentMonth = objResult.Where(C => C.PartyDate.Value.Month == i);
                if (currentMonth != null)
                {
                    OrderSumMoney.AppendFormat("{0},", currentMonth.Sum(C => C.AggregateAmount));

                }
                else
                {
                    OrderSumMoney.AppendFormat("{0},", 0);

                }

            }
            ViewState["OrderSumMoney"] = GetSubString(OrderSumMoney.ToString());
        }

        /// <summary>
        /// 创建成功率K线图
        /// </summary>
        protected void CreateClinchaDealRateKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //成功率
            StringBuilder ClinchaDealRate = new StringBuilder();
            //客源量
            
            var objParameterList = GetParameter();
            objParameterList.Add(new ObjectParameter("State_between", 13 + "," + 23));

            var objClinchaDealResult = objTelemarkResult.Where(C => C.State >= 13 && C.State <= 23);
            for (int i = 1; i <= 12; i++)
            {
                //当前客户量
                var currentMonth = objTelemarkResult.Where(C => C.CreateDate.Value.Month == i);

                var currentSuccessMonth = objClinchaDealResult.Where(C => C.CreateDate.Value.Month == i);
                int ClinchaDealCount = currentSuccessMonth.Count();
                int customerCount = currentMonth.Count();
                if (ClinchaDealCount == 0 && customerCount == 0)
                {
                    //有效率
                    ClinchaDealRate.AppendFormat("{0},", 0);
                }
                else
                {

                    ClinchaDealRate.AppendFormat("{0},", GetDoubleFormat2((double)ClinchaDealCount / customerCount));
                }
            }

            ViewState["ClinchaDealRate"] = GetSubString(ClinchaDealRate.ToString());

        }

        /// <summary>
        /// 创建邀约成功率K线图
        /// </summary>
        protected void CreateInviteSuccessRateKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //邀约成功率
            StringBuilder inviteSuccessRate = new StringBuilder();
       
          
            var objSuccessResult = objInviteBLL.GetInviteCustomerByStateIndex(GetParameter());
            for (int i = 1; i <= 12; i++)
            {
                //当前客户量
                var currentMonth = objTelemarkResult.Where(C => C.CreateDate.Value.Month == i);

                var currentSuccessMonth = objSuccessResult.Where(C => C.CreateDate.Value.Month == i);
                int successCount = currentSuccessMonth.Count();
                int customerCount = currentMonth.Count();
                if (successCount == 0 && customerCount == 0)
                {
                    //有效率
                    inviteSuccessRate.AppendFormat("{0},", 0);
                }
                else
                {

                    inviteSuccessRate.AppendFormat("{0},", GetDoubleFormat2((double)successCount / customerCount));
                }
            }

            ViewState["inviteSuccessRate"] = GetSubString(inviteSuccessRate.ToString());
        }

        /// <summary>
        /// 创建有效率K线图
        /// </summary>
        protected void CreateValidRateKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //有效率
            StringBuilder ValidRate = new StringBuilder();
          
 
            var objTelemarkValidateResult = objTelemarkResult.Where(C => C.State >= 2 && C.State <= 6);
            for (int i = 1; i <= 12; i++)
            {
                //当前客户量
                var currentMonth = objTelemarkResult.Where(C => C.CreateDate.Value.Month == i);

                var currentValidateMonth = objTelemarkValidateResult.Where(C => C.CreateDate.Value.Month == i);
                int validCount = currentValidateMonth.Count();
                int customerCount = currentMonth.Count();
                if (validCount == 0 && customerCount == 0)
                {
                    //有效率
                    ValidRate.AppendFormat("{0},", 0);
                }
                else
                {
                    //有效率
                    ValidRate.AppendFormat("{0},", GetDoubleFormat2((double)validCount / customerCount));
                }
            }

            ViewState["ValidRate"] = GetSubString(ValidRate.ToString());

        }

        /// <summary>
        /// 创建成交量K线图
        /// </summary>
        protected void CreateClinchaDealCountKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //成交量
            StringBuilder ClinchaDealCounts = new StringBuilder();



            var objClinchaDealResult = objTelemarkResult.Where(C => C.State >= 13 && C.State <= 23);

            for (int i = 1; i <= 12; i++)
            {
                var currentMonth = objClinchaDealResult.Where(C => C.CreateDate.Value.Month == i);


                if (currentMonth != null)
                {
                    ClinchaDealCounts.AppendFormat("{0},", currentMonth.Count());
                }
                else
                {
                    ClinchaDealCounts.AppendFormat("{0},", currentMonth.Count());
                }
            }

            ViewState["ClinchaDealCounts"] = GetSubString(ClinchaDealCounts.ToString());
        }

        /// <summary>
        /// 创建邀约成功量K线图
        /// </summary>
        protected void CreateSuccessCountKLine()
        {
            //邀约成功量
            StringBuilder inviteSuccessCounts = new StringBuilder();
            var objParameterList = GetParameter();
            // objParameterList.Add(new ObjectParameter("ChannelType_NumGreaterthan",0));
            var objSuccessResult = objInviteBLL.GetInviteCustomerByStateIndex(objParameterList).Where(C=>C.State==6);
            for (int i = 1; i <= 12; i++)
            {
                var currentMonth = objSuccessResult.Where(C => C.CreateDate.Value.Month == i);
                if (currentMonth != null)
                {
                    inviteSuccessCounts.AppendFormat("{0},", currentMonth.Count());
                }
                else
                {
                    inviteSuccessCounts.AppendFormat("{0},", currentMonth.Count());
                }
            }
            ViewState["inviteSuccessCounts"] = GetSubString(inviteSuccessCounts.ToString());
        }

        /// <summary>
        /// 创建有效量K线图
        /// </summary>
        protected void CreateValidateCountKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //有效量
            StringBuilder validCounts = new StringBuilder();
         
            var objTelemarkValidateResult = objTelemarkResult.Where(C=>C.State>=2&&C.State<=6);
            for (int i = 1; i <= 12; i++)
            {
                var currentMonth = objTelemarkResult.Where(C => C.CreateDate.Value.Month == i);
                if (currentMonth != null)
                {
                    validCounts.AppendFormat("{0},", currentMonth.Count());
                }
                else
                {
                    validCounts.AppendFormat("{0},", 0);
                }
            }
            ViewState["validCounts"] = GetSubString(validCounts.ToString());

        }

        /// <summary>
        /// 创建客源量K线图
        /// </summary>
        protected void CreateCustomerCountKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //客源量
            StringBuilder countCustomers = new StringBuilder();
          


            for (int i = 1; i <= 12; i++)
            {
                var currentMonth = objTelemarkResult.Where(C => C.CreateDate.Value.Month == i);
                if (currentMonth != null)
                {
                    countCustomers.AppendFormat("{0},", currentMonth.Count());
                }
                else
                {
                    countCustomers.AppendFormat("{0},", 0);
                }
            }
            ViewState["countCustomers"] = GetSubString(countCustomers.ToString());
        }
       
        protected void ddlChooseYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}