using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class SupplierProductList : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender,e);
            }
        }
        
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            List<FL_ProductforDispatching> query = new ProductforDispatching().GetProductsByDispatchingIDAndRowType(Request["DispatchingID"].ToInt32(), 1).Where(C => C.SupplierID == Request["SupplierID"].ToInt32()).ToList();
            repProductList.DataBind(query);
            lblPriceSum.Text = query.Where(C => C.Subtotal.HasValue).Sum(C => C.Subtotal.Value).ToString("f2");
        }
    }
}