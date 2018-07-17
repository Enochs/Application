using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using System.Text;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger
{
    public partial class InviteListCount : UserControlTools
    {
        Telemarketing objTelemarketingBLL = new Telemarketing();
        HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();


        /// <summary>
        /// 控件初始化绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlChooseYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
                var objTelemarkResult = GetPublicDataByParameter();
                CreateCustomerCountKLine();
                CreateValidateCountKLine();
                CreateValidRateKLine(objTelemarkResult);
                CreateOrderSumMoneyKLine();
                var objInviteResult = GetPublicInviteDataByParameter();

                CreateSuccessCountKLine();

                CreateInviteIngCountKLine();
                CreateInviteLoseCountKLine(objInviteResult);
                CreateInviteNotCountKLine(objInviteResult);

                //未邀约率
                ViewState["dontRate"] = CreateInviteStateRateKLine(3, objTelemarkResult, objInviteResult);
                ViewState["inviteRate"] = CreateInviteStateRateKLine(5, objTelemarkResult, objInviteResult);
                ViewState["inviteSuccessRate"] = CreateInviteStateRateKLine(6, objTelemarkResult, objInviteResult);
                ViewState["loseRate"] = CreateInviteStateRateKLine(23, objTelemarkResult, objInviteResult);



            }
        }


        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        protected List<ObjectParameter> GetParameter()
        {
            int employeeId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            List<ObjectParameter> objParameterList = new List<ObjectParameter>();

            //string[] chooseDateStr = ddlChooseYear.SelectedValue.Split(',');
            //DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            //DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            //objParameterList.Add(new ObjectParameter("CreateDate_between", chooseDateStar + "," + chooseDateEnd));

            objParameterList.Add(new ObjectParameter("EmpLoyeeID", employeeId));
            return objParameterList;
        }

        /// <summary>
        /// 创建客源量K线图
        /// </summary>
        protected void CreateCustomerCountKLine()
        {
            //客源量
            StringBuilder countCustomers = new StringBuilder();

            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();

            for (int i = 1; i <= 12; i++)
            {
                var currentMonth = ObjInviteBLL.GetInviteSumByDatetime(GetParameter(), ddlChooseYear.SelectedItem.Text.ToInt32(), i, 1);
                //if (currentMonth != null)
                //{
                countCustomers.AppendFormat("{0},", currentMonth);
                //}
                //else
                //{
                //    countCustomers.AppendFormat("{0},", 0);
                //}
            }
            ViewState["countCustomers"] = GetSubString(countCustomers.ToString());
        }


        /// <summary>
        /// 创建有效量K线图
        /// </summary>
        protected void CreateValidateCountKLine()
        {
            //有效量
            StringBuilder ObjSource = new StringBuilder();
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();


            for (int i = 1; i <= 12; i++)
            {
                var MonthSource = ObjInviteBLL.GetInviteSumByDatetime(GetParameter(), ddlChooseYear.SelectedItem.Text.ToInt32(), i, 2);
                ObjSource.AppendFormat("{0},", MonthSource);

            }
            ViewState["validCounts"] = GetSubString(ObjSource.ToString());

        }




        /// <summary>
        /// 创建邀约中量K线图
        /// </summary>
        protected void CreateInviteIngCountKLine()
        {
            //邀约中
            StringBuilder ObjSource = new StringBuilder();
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();


            for (int i = 1; i <= 12; i++)
            {
                var MonthSource = ObjInviteBLL.GetInviteSumByDatetime(GetParameter(), ddlChooseYear.SelectedItem.Text.ToInt32(), i, 3);
                ObjSource.AppendFormat("{0},", MonthSource);

            }
            ViewState["inviteCounts"] = GetSubString(ObjSource.ToString());
        }



        /// <summary>
        /// 创建邀约成功量K线图
        /// </summary>
        protected void CreateSuccessCountKLine()
        {
            //邀约中
            StringBuilder ObjSource = new StringBuilder();
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();


            for (int i = 1; i <= 12; i++)
            {
                var MonthSource = ObjInviteBLL.GetInviteSumByDatetime(GetParameter(), ddlChooseYear.SelectedItem.Text.ToInt32(), i, 4);
                ObjSource.AppendFormat("{0},", MonthSource);

            }
            ViewState["inviteSuccessCounts"] = GetSubString(ObjSource.ToString());
        }



        /// <summary>
        /// 创建流失量K线图
        /// </summary>
        protected void CreateInviteLoseCountKLine(List<View_GetInviteCustomers> objInviteResult)
        {

            //邀约中
            StringBuilder ObjSource = new StringBuilder();
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();


            for (int i = 1; i <= 12; i++)
            {
                var MonthSource = ObjInviteBLL.GetInviteSumByDatetime(GetParameter(),ddlChooseYear.SelectedItem.Text.ToInt32(), i, 5);
                ObjSource.AppendFormat("{0},", MonthSource);

            }
            ViewState["loseCounts"] = GetSubString(ObjSource.ToString());
        }




        /// <summary>
        /// 创建未邀约K线图
        /// </summary>
        protected void CreateInviteNotCountKLine(List<View_GetInviteCustomers> objInviteResult)
        {

            StringBuilder ObjSource = new StringBuilder();
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();


            for (int i = 1; i <= 12; i++)
            {
                var MonthSource = ObjInviteBLL.GetInviteSumByDatetime(GetParameter(),ddlChooseYear.SelectedItem.Text.ToInt32(), i, 6);
                ObjSource.AppendFormat("{0},", MonthSource);

            }
            ViewState["dontCounts"] = GetSubString(ObjSource.ToString());
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
        /// 创建有效率K线图
        /// </summary>
        protected void CreateValidRateKLine(List<View_GetTelmarketingCustomers> objTelemarkResult)
        {
            //有效率
            StringBuilder ValidRate = new StringBuilder();


            var objTelemarkValidateResult = objTelemarkResult.Where(C => C.State >3&&C.State!=29);
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

        protected List<View_GetInviteCustomers> GetPublicInviteDataByParameter()
        {
            var objParameterList = GetParameter();
            // objParameterList.Add(new ObjectParameter("ChannelType_NumGreaterthan",0));
            var objInviteResult = objInviteBLL.GetInviteCustomerByStateIndex(objParameterList);
            return objInviteResult;
        }








        /// <summary>
        /// 根据邀约状态值返回对应比率K线图
        /// </summary>
        /// <param name="objTelemarkResult"></param>
        /// <param name="objInviteResult"></param>
        protected string CreateInviteStateRateKLine(int stateValue, List<View_GetTelmarketingCustomers> objTelemarkResult, List<View_GetInviteCustomers> objInviteResult)
        {
            //邀约成功率
            StringBuilder inviteLoseRate = new StringBuilder();


            var objSuccessResult = objTelemarkResult.Where(C => C.State == stateValue);
            for (int i = 1; i <= 12; i++)
            {
                //当前客户量
                var currentMonth = objTelemarkResult.Where(C => C.CreateDate.Value.Month == i);

                var currentIngMonth = objSuccessResult.Where(C => C.CreateDate.Value.Month == i);
                int successCount = currentIngMonth.Count();
                int customerCount = currentMonth.Count();
                if (successCount == 0 && customerCount == 0)
                {
                    //有效率
                    inviteLoseRate.AppendFormat("{0},", 0);
                }
                else
                {

                    inviteLoseRate.AppendFormat("{0},", GetDoubleFormat2((double)successCount / customerCount));
                }
            }

            return GetSubString(inviteLoseRate.ToString());

        }




        /// <summary>
        /// 创建订单总额K线图
        /// </summary>
        protected void CreateOrderSumMoneyKLine()
        {
            StringBuilder ObjSource = new StringBuilder();

            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();

            int employeeId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            for (int i = 1; i <= 12; i++)
            {
                var StateParmeter = new List<ObjectParameter>();
 
                StateParmeter.Add(new ObjectParameter("EmpLoyeeID", employeeId));
                StateParmeter.Add(new ObjectParameter("CreateDate_between", GetMonthStaeEndDate(ddlChooseYear.SelectedItem.Text.ToInt32(),i)));
                ObjSource.AppendFormat("{0},", ObjInviteBLL.GetInviteSumTotal(StateParmeter, 2));


            }
            ViewState["OrderSumMoney"] = GetSubString(ObjSource.ToString());
        }

        public string GetMonthStaeEndDate(int Year, int Month)
        {
            return Year+"-"+Month+"-1"+","+Year+"-"+Month+"-"+DateTime.DaysInMonth(Year, Month);
        }

    }
}