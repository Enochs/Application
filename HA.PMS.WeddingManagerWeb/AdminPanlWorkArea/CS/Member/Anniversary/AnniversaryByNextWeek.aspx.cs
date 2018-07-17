using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.FD;
 

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary
{
    public partial class AnniversaryByNextWeek : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        protected CS_Member GetMember(object CustomerID)
        {
            return new BLLAssmblly.CS.Member().GetByCustomerID(CustomerID.ToString().ToInt32());
        }

        protected void BinderData(object sender, EventArgs e)
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<ObjectParameter> paramList = new List<ObjectParameter>();

            //新人姓名
            paramList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            //新人手机
            paramList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "BrideCellPhone_LIKE", txtBrideCellPhone.Text.Trim());
            //酒店
            paramList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop_LIKE", ddlHotel.SelectedItem.Text.Trim());

            var query = new Dispatching().GetCsCustomersByPartyDateRangeIgnoreYear(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, User.Identity.Name.ToInt32(), paramList, NextMonday, NextSunday);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataBind(query);
        }
    }
}