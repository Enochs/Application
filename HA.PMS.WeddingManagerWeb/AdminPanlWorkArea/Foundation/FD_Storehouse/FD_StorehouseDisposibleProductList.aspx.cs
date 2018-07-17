using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using System.Text;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseDisposibleProductList : HA.PMS.Pages.SystemPage
    {
        StorehouseSourceProduct storehouseSourceProductBLL = new StorehouseSourceProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        protected void BinderData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": MainPager.CurrentPageIndex = 1; break;
                }
            }

            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(!string.IsNullOrEmpty(txtProductName.Text), "SourceProductName_LIKE", txtProductName.Text.Trim());
            parameters.Add(ddlCategory.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategory.SelectedValue.ToInt32());
            parameters.Add(ddlProject.SelectedValue.ToInt32() > 0, "ProductProject", ddlProject.SelectedValue.ToInt32());
            parameters.Add(CreateDateRanger.IsNotBothEmpty, "PutStoreDate_between", CreateDateRanger.Start, CreateDateRanger.End);

            int resourceCount = 0;
            //源数据
            var dataSource = storehouseSourceProductBLL.GetStorehouseDisposibleProducts(
                MainPager.PageSize,
                MainPager.CurrentPageIndex,
                out resourceCount,
                C => C.PutStoreDate,
                false,
                parameters);

            rptMain.DataBind(dataSource);
            MainPager.RecordCount = resourceCount;


            StringBuilder purchasePrice = new StringBuilder();
            var disposibleProducts = storehouseSourceProductBLL.GetStorehouseDisposibleProductsByYear(ddlChooseYear.SelectedValue.ToInt32());
            for (int i = 1; i <= 12; i++)
            {
                decimal purchasePriceSum = 0;
                DateTime dateOfMonth = new DateTime(ddlChooseYear.SelectedValue.ToInt32(), i, 1);

                var dataOfMonth = disposibleProducts.Where(C => C.PutStoreDate >= dateOfMonth && C.PutStoreDate < dateOfMonth.AddMonths(1));
                foreach (var C in dataOfMonth)
                {
                    purchasePriceSum += Convert.ToDecimal(C.PurchasePrice) * Convert.ToInt32(C.SourceCount);
                }
                purchasePrice.AppendFormat("{0},", purchasePriceSum);
            }

            //成本总价
            ViewState["KL_PurchasePrice"] = purchasePrice.ToString().TrimEnd(',');
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProject.Items.Clear();
            ddlProject.ParentID = ddlCategory.SelectedValue.ToInt32();
            ddlProject.BinderByparent();
            ddlProject.ClearSelection();
            ddlProject.Items.Insert(0, new ListItem("", "-1"));
            ddlProject.Items.FindByValue("-1").Selected = true;
        }

        protected void ddlCategory_Init(object sender, EventArgs e)
        {
            ddlCategory.Items.Insert(0, new ListItem("", "-1"));
            ddlCategory.ClearSelection();
            ddlCategory.Items.FindByValue("-1").Selected = true;
            ddlProject.Items.Clear();
        }

        protected int GetStoreCount(object productid)
        {
            return storehouseSourceProductBLL.GetStoreCount(Convert.ToInt32(productid));
        }

        protected int GetLeaveCount(object productid)
        {
            return storehouseSourceProductBLL.GetLeaveCount(Convert.ToInt32(productid));
        }

        protected int GetUsedTimes(object productid)
        {
            return storehouseSourceProductBLL.GetUsedTimes(Convert.ToInt32(productid));
        }

        protected string GetLastUsedDate(object productid)
        {
            return storehouseSourceProductBLL.GetLastUsedDate(Convert.ToInt32(productid));
        }

        protected int GetAvailableCount(object productid)
        {
            return storehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), DateTime.Now.Date);
        }

    }
}