using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Master
{
    public partial class PopuMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["HAEmployeeID"] != null)
            {
                hideSubmitKey.Value = Request.Cookies["HAEmployeeID"].Value;
            }
        }
    }
}