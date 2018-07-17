using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.EditoerLibrary
{
    public class btnReload : System.Web.UI.WebControls.Button
    {

        public btnReload()
        {
            this.Click += btnReload_Click;
            this.CssClass = "btn btn-primary";
            this.Text = "重置条件";
            this.Visible = true;
        }
      
        private void btnReload_Click(object sender, EventArgs e)
        {
            this.Page.Response.Redirect(this.Page.Request.Url.ToString());
        }
       
    }
}
