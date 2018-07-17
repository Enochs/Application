using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class ProductListPrintView : HA.PMS.Pages.SystemPage
    {
        int DispatchingID, EmployeeID, CustomerID, Typer = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            Typer = Request["Typer"].ToInt32();

            if (!IsPostBack)
            {
                switch (Typer)
                {
                    case 1: BinderSupplierData(); break;//供应商
                    case 2: BinderWareHouseData(); break;//库房
                    case 3: BinderNewAddData(); break;//新购入产品
                    default:break;
                }
            }
        }

        private void BinderWareHouseData()
        {
            var query = new BLLAssmblly.Flow.ProductforDispatching().GetProductByType(DispatchingID, 2, EmployeeID, 1);
            decimal wareSumPrice = 0;
            query.ForEach(C => { if (!object.ReferenceEquals(C.PurchasePrice, null)) { wareSumPrice += C.Subtotal.Value; } });
            lblProductSumPrice.Text = wareSumPrice.ToString();
            txtMoney.Text = GetPlanCostBySupplyName("库房折旧", DispatchingID, 3);
            repProductList.DataBind(query);
        }

        private void BinderSupplierData()
        {
        }

        private void BinderNewAddData()
        {
            HA.PMS.BLLAssmblly.Flow.ProductforDispatching ObjProductforDispatchingBLL = new HA.PMS.BLLAssmblly.Flow.ProductforDispatching();
            var query = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 3, EmployeeID, 1);
            query.AddRange(ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 3, EmployeeID, 0));
            decimal wareSumPrice = 0;
            query.ForEach(C => { if (!object.ReferenceEquals(C.PurchasePrice, null)) { wareSumPrice += C.Subtotal.Value; } });
            lblProductSumPrice.Text = wareSumPrice.ToString();
            repProductList.DataBind(query);
        }

        public string GetPlanCostBySupplyName(string SupplilyName, int DispatchingID, int RowType)
        {
            HA.PMS.BLLAssmblly.Flow.OrderfinalCost ObjOrderfinalCost = new HA.PMS.BLLAssmblly.Flow.OrderfinalCost();
            var ObjModel = ObjOrderfinalCost.GetBySupplilyName(SupplilyName, DispatchingID, RowType);
            return object.ReferenceEquals(ObjModel, null) ? string.Empty : ObjModel.PlannedExpenditure.ToString();
        }
    }
}