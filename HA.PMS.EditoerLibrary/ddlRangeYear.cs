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
    /// <summary>
    /// 范围年份选择下拉框
    /// </summary>
    public class ddlRangeYear : DropDownList
    {
 
        public ddlRangeYear()
        {
            for (int i = 0; i < 30; i++)
            {
                this.Items.Add(new ListItem((1990 + i).ToString(), (1990 + i).ToString()));
            }

            
            this.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;
      
        }
    }
}
