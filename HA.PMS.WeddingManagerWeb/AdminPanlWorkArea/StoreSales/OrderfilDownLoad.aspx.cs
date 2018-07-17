using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.BLLAssmblly.Flow;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderfilDownLoad : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Orderfile ObjOrderfileBLL = new Orderfile();
                this.repfileList.DataSource = ObjOrderfileBLL.GetByOrderID(Request["OrderID"].ToInt32());
                this.repfileList.DataBind();
            }
        }
    }
}