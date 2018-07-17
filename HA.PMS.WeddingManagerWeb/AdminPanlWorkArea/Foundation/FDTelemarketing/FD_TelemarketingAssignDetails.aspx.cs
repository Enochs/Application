using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{
    public partial class FD_TelemarketingAssignDetails : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder(sender, e);
            }
        }

        protected void DataBinder(object sender, EventArgs e)
        {
            List<System.Data.Objects.ObjectParameter> parameters = new List<System.Data.Objects.ObjectParameter>();
            parameters.Add("EmpLoyeeID", User.Identity.Name.ToInt32());
            parameters.Add(new ObjectParameter("SerchKeypar", "CreateEmpLoyee"));
            parameters.Add("IsDelete", false);
            parameters.Add(ddlChanneType.SelectedValue.ToInt32() > 0, "ChannelType", ddlChanneType.SelectedValue.ToInt32());
            parameters.Add(ddlChanneName.SelectedValue.ToInt32() > 0, "Channel", ddlChanneName.SelectedItem.Text);
            parameters.Add(!string.IsNullOrEmpty(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            parameters.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate_between", PartyDateRanger.Start, PartyDateRanger.End);
           
            int resourceCount; ;
            var query = new HA.PMS.BLLAssmblly.Flow.Telemarketing().GetTelmarketingCustomersByParameter(TelemarketingPager.PageSize, TelemarketingPager.CurrentPageIndex, out resourceCount, parameters.ToArray());
            TelemarketingPager.RecordCount = resourceCount;
            rptTelemarketingManager.DataBind(query);

            ddlChanneType.DataSource = new HA.PMS.BLLAssmblly.FD.ChannelType().GetByAll();
            ddlChanneType.DataTextField = "ChannelTypeName";
            ddlChanneType.DataValueField = "ChannelTypeId";
            ddlChanneType.DataBind();
            ddlChanneType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlChanneType.ClearSelection();
            ddlChanneType.Items.FindByValue("0").Selected = true;

        }

        protected void TelemarketingPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder(sender, e);
        }

        protected void ddlChanneType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChanneName.BindByParent(ddlChanneType.SelectedValue.ToInt32());
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            TelemarketingPager.CurrentPageIndex = 1;
            DataBinder(sender, e);
        }
    }
}