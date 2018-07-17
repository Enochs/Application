using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class OrderSelector : System.Web.UI.UserControl
    {
        public bool IsAscending 
        {
            get
            {
                return hiddenStyle.Value == "0" ? true : false;
            }
            set
            {
                hiddenStyle.Value = value ? "0" : "1";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}