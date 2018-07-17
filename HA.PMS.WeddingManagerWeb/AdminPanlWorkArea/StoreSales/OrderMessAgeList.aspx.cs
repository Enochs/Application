using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderMessAgeList : PopuPage
    {
        OrderMessage ObjOrderMessageBLL = new OrderMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.repOrderMessage.DataSource = ObjOrderMessageBLL.GetByOrderID(Request["OrderID"].ToInt32());
            this.repOrderMessage.DataBind();
        }

    }
}