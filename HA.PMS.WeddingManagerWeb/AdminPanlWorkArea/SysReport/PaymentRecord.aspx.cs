using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport
{
    public partial class PaymentRecord : SystemPage
    {
        /// <summary>
        /// 支付记录
        /// </summary>
        StatementPayFor ObjPayMentBLL = new StatementPayFor();

        int CustomerID = 0;

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerID = Request["CustomerID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BinderData()
        {
            var DataList = ObjPayMentBLL.GetByCustomerID(CustomerID).OrderBy(C => C.SupplierName);
            RepPaymentRecord.DataBind(DataList);
        }
        #endregion

        #region 点击排序
        /// <summary>
        /// 根据表头排序
        /// </summary> 
        protected void lbtnSupplier_Click(object sender, EventArgs e)
        {
            LinkButton btnSort = sender as LinkButton;
            switch (btnSort.ID)
            {
                case "lbtnSupplier":
                    var DataList = ObjPayMentBLL.GetByCustomerID(CustomerID).OrderBy(C => C.SupplierName);
                    RepPaymentRecord.DataBind(DataList);
                    break;
                case "lbtnPayMoney":
                    var DataList1 = ObjPayMentBLL.GetByCustomerID(CustomerID).OrderBy(C => C.PayMoney);
                    RepPaymentRecord.DataBind(DataList1);
                    break;
                case "lbtnCreateDate":
                    var DataList2 = ObjPayMentBLL.GetByCustomerID(CustomerID).OrderBy(C => C.CreateDate);
                    RepPaymentRecord.DataBind(DataList2);
                    break;
            }
        }
        #endregion
    }
}