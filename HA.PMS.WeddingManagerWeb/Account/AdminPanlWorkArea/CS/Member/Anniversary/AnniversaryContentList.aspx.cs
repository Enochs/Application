using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary
{
    public partial class AnniversaryContentList : SystemPage
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
            //int startIndex = CtrPageIndex.StartRecordIndex;
            //int SourceCount = 0;
            //List<ObjectParameter> paramList = new List<ObjectParameter>();

            ////新人姓名
            //paramList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride_LIKE", txtBride.Text.Trim());
            ////新人手机
            //paramList.Add(!string.IsNullOrWhiteSpace(txtBrideCellPhone.Text), "BrideCellPhone_LIKE", txtBrideCellPhone.Text.Trim());
            ////酒店
            //paramList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop_LIKE", ddlHotel.SelectedItem.Text.Trim());
            ////婚期
            //paramList.Add(QueryDateRanger.IsNotBothEmpty && ddlDateType.SelectedValue.Equals("1"), "PartyDate_between", QueryDateRanger.Start, QueryDateRanger.End);
            ////生日
            //paramList.Add(QueryDateRanger.IsNotBothEmpty && ddlDateType.SelectedValue.Equals("2"), "BrideBirthday", QueryDateRanger.Start, QueryDateRanger.End);

            //var query = new BLLAssmblly.CS.Member().GetMemberListByIndexAndParas(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, paramList);
            //CtrPageIndex.RecordCount = SourceCount;
            //repCustomer.DataBind(query);
        }
    }
}