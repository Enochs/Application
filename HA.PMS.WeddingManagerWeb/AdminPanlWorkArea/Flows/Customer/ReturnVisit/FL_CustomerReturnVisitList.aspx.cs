using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit
{
    public partial class FL_CustomerReturnVisitList : HA.PMS.Pages.SystemPage
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
            var objParmList = new List<PMSParameters>();

            objParmList.Add("IsReturn", true, NSqlTypes.Bit);

            //新人姓名
            CstmNameSelector.AppandTo(objParmList);
            //婚期

            objParmList.Add("PartyDate", PartyDateRanger.Start.ToString() + "," + PartyDateRanger.End.ToString(), NSqlTypes.DateBetween);

            //酒店

            objParmList.Add("Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);


            int startIndex = ReturnPager.StartRecordIndex;
            int resourceCount = 0;
            var query = new CustomerReturnVisit().GetByWhereParameter(objParmList, "PartyDate", ReturnPager.PageSize, ReturnPager.CurrentPageIndex, out resourceCount);
            ReturnPager.RecordCount = resourceCount;
            rptReturn.DataBind(query);
        }
    }
}