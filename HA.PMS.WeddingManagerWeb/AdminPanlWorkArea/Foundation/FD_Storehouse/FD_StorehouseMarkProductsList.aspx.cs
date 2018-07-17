using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseMarkProductsList : HA.PMS.Pages.SystemPage
    {
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
                    case "btnquery": StorePager.CurrentPageIndex = 1; break;
                }
            }

            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            paramsList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate_between", PartyDateRanger.Start, PartyDateRanger.End);

            int startIndex = StorePager.StartRecordIndex;
            int sourceCount = 0;
            rptStorehouse.DataBind(new StorehouseSourceProduct().GetbyMarkParameterDistinctByCustomerID(paramsList.ToArray(), StorePager.PageSize, StorePager.CurrentPageIndex, out sourceCount));
            StorePager.RecordCount = sourceCount;
        }
    }
}