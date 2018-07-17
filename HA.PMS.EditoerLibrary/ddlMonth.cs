using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
    public class ddlMonth:DropDownList
    {
        public ddlMonth() 
        {
            ListItem currentMonth = new ListItem("请选择", "0");

            this.Items.Add(currentMonth);
            for (int i = 1; i <= 12; i++)
            {
                currentMonth = new ListItem(i + "月", i + string.Empty);
                this.Items.Add(currentMonth);
            }
        }
    }
}
