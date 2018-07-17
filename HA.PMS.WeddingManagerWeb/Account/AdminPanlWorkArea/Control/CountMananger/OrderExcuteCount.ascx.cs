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
    public partial class OrderExcuteCount : UserControlTools
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateOrderSumMoneyKLine();
            CreateExcuteAssessKLine();

        }


        protected void CreateExcuteAssessKLine()
        { 
        
        }
        /// <summary>
        /// 创建订单总额K线图
        /// </summary>
        protected void CreateOrderSumMoneyKLine()
        {

             
            StringBuilder sbOrderCount = new StringBuilder();
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
                    sbOrderCount.AppendFormat("{0},", currentMonth.Count());

                }
                else
                {
                    sbOrderCount.AppendFormat("{0},", 0);

                }

            }
            ViewState["sbOrderCount"] = GetSubString(sbOrderCount.ToString());
        }
    }
}