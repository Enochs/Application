using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using System.Linq;
using System.Text;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseDisposibleProductOutList : HA.PMS.Pages.SystemPage
    {
        BLLAssmblly.Flow.Customers ObjCustomersBLL = new BLLAssmblly.Flow.Customers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Web.UI.WebControls.ListItem listItem = ddlChooseYear.Items.FindByValue(DateTime.Now.Year.ToString());
                if (listItem != null)
                {
                    ddlChooseYear.ClearSelection();
                    listItem.Selected = true;
                }
                BinderData(sender, e);
            }
        }

        protected void BinderData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": PagerMain.CurrentPageIndex = 1; break;
                }
            }

            List<System.Data.Objects.ObjectParameter> parameters = new List<System.Data.Objects.ObjectParameter>();
            parameters.Add(!string.IsNullOrWhiteSpace(txtProductName.Text), "ProductName_LIKE", txtProductName.Text.Trim());

            int totalCount;
            //数据源
            var dataSource = new HA.PMS.BLLAssmblly.Flow.ProductforDispatching().GetDispatchingSHDisposibleProductsByPartyDate(
                PagerMain.PageSize,
                PagerMain.CurrentPageIndex,
                out totalCount,
                C => C.ProeuctKey,
                false,
                parameters,
                PartyDateRanger.Start, PartyDateRanger.End);

            RptMain.DataSource = dataSource;
            RptMain.DataBind();
            PagerMain.RecordCount = totalCount;

            StringBuilder unitPrice = new StringBuilder();
            for (int i = 1; i <= 12; i++)
            {
                decimal unitPriceSum = 0;

                var dataOfMonth = new BLLAssmblly.Flow.ProductforDispatching().GetDispatchingSHDisposibleProductsByCustomerIDs(ObjCustomersBLL.GetCustomerIDsByMonthOfYear(Convert.ToInt32(ddlChooseYear.SelectedValue), i));
                foreach (var C in dataOfMonth)
                {
                    unitPriceSum += Convert.ToDecimal(C.UnitPrice) * C.Quantity;
                }
                unitPrice.AppendFormat("{0},", unitPriceSum);
            }
            //销售总价
            ViewState["KL_UnitPrice"] = unitPrice.ToString().TrimEnd(',');
             
        }

        protected System.DateTime? GetPartyDate(object customerID)
        {
            return new HA.PMS.BLLAssmblly.Flow.Customers().GetPartyDate(customerID);
        }

        protected System.DateTime? GetPutStoreDate(object productid)
        {
            return new BLLAssmblly.FD.StorehouseSourceProduct().GetPutStoreDate(Convert.ToInt32(productid));
        }
    }
}