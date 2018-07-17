using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderMessAge :SystemPage
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
           this.repOrderMessage.DataSource= ObjOrderMessageBLL.GetByOrderID(Request["OrderID"].ToInt32());
           this.repOrderMessage.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            FL_OrderMessage ObjOrderMessageModel = new FL_OrderMessage();
            ObjOrderMessageModel.Message = txtContent.Text;
            ObjOrderMessageModel.Title = txtTitle.Text;
            ObjOrderMessageModel.OrderID=Request["OrderID"].ToInt32();
            ObjOrderMessageModel.CreateEmployeeID = User.Identity.Name.ToInt32();
            ObjOrderMessageBLL.Insert(ObjOrderMessageModel);
            JavaScriptTools.AlertAndClosefancybox("辅导意见添加成功！",Page);
        }
    }
}