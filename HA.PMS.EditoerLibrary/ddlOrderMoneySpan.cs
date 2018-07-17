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
    /// 订单金额价格段
    /// </summary>
    public class ddlOrderMoneySpan : DropDownList
    {
        public ddlOrderMoneySpan()
         {
             OrderMoneySpan objOrderMoneySpanBLL = new OrderMoneySpan();
             this.DataSource = objOrderMoneySpanBLL.GetByAll();
             this.DataTextField = "MoneyName";
             this.DataValueField = "MoneyValue";
             this.DataBind();
             this.Items.Add(new ListItem("请选择", "0-100000000"));
             this.SelectedIndex = this.Items.Count - 1;
         }
    }
}
