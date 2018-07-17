using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class MessageBoardPage1 : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.MessageBoardforEmpLoyee.CreateEmpLoyeeID = User.Identity.Name.ToInt32();
            this.MessageBoardforEmpLoyee.EmpLoyeeID = Request["EmpLoyeeID"].ToInt32();

        }
    }
}