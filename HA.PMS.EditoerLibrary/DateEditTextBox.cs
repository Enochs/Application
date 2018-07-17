using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.EditoerLibrary
{

    public class DateEditTextBox:System.Web.UI.WebControls.TextBox
    {
        public DateEditTextBox()
        {
            this.CssClass = "DataTextEditoer"; 
            
            //this.Text = DateTime.Now.ToShortDateString();
            //string Script = " $('.DataTextEditoer').datepicker({ dateFormat: 'yy-mm-dd ' });";

            //JavaScriptTools.ResponseScript(Script, this.Page);
            
        }
    }
}
