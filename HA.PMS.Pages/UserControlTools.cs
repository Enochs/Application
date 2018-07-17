using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
namespace HA.PMS.Pages
{
    public class UserControlTools:System.Web.UI.UserControl
    {
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSubString(string source)
        {
            return source.Substring(0, source.ToString().LastIndexOf(','));
        }
        protected string GetDoubleFormat2(double number)
        {
            return Math.Round(number, 2) * 100 + string.Empty;


        }
        /// <summary>
        /// 根据客户ID返回定金
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetEarnestMoneyByCustomerID(object source)
        {
            Order objOrderBLL = new Order();
            FL_Order ordes = objOrderBLL.GetbyCustomerID((int)source);
            if (ordes != null)
            {
                return ordes.EarnestMoney + string.Empty;
            }
            return "";

        }
        /// <summary>
        /// 返回总金额
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetAggregateAmount(object source)
        {
            QuotedPrice objQuotedPriceBLL = new QuotedPrice();
            int CustomerID = (source + string.Empty).ToInt32();
            FL_QuotedPrice quotedPrice = objQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (quotedPrice != null)
            {
                return quotedPrice.AggregateAmount + string.Empty;
            }
            return "";
        }
    }
}
