using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class ServerFileUpLoad : System.Web.UI.UserControl
    {
        public string GuiKey = "";
        public string listShowType = "";
        public string PostServerUri=string.Empty;

        /// <summary>
        /// 提交地址
        /// </summary>
        public string PostServer
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GuiKey = Guid.NewGuid().ToString();
            listShowType = System.Configuration.ConfigurationManager.AppSettings["ListShowType"];
           // PostServerUri = PostServer+Session["PostServerUri"].ToString()+"&Need=Change";
             
        }
    }
}