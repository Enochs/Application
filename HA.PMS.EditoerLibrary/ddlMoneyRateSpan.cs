using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
    /// <summary>
    /// 订单利润率
    /// </summary>
    public class ddlMoneyRateSpan : DropDownList
    {
        public ddlMoneyRateSpan()
        {
            MoneyRateSpan objOrderMoneySpanBLL = new MoneyRateSpan();
            this.DataSource = objOrderMoneySpanBLL.GetByAll();
            this.DataTextField = "RateName";
            this.DataValueField = "RateValue";
            this.DataBind();
            this.Items.Add(new ListItem("请选择", "0-1"));
            this.SelectedIndex = this.Items.Count - 1;
        }
    }
}
