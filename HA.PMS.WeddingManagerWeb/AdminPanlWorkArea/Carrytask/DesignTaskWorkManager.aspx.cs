using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class DesignTaskWorkManager : SystemPage
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
            List<PMSParameters> paramsList = new List<PMSParameters>();
            //paramsList.Add("UseSate", 206, NSqlTypes.NotEquals);
            paramsList.Add("DesignEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            paramsList.Add("IsUse", true, NSqlTypes.Bit);

            //新人姓名
            CstmNameSelector.AppandTo(paramsList);
            //婚期
            paramsList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            //酒店
            paramsList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);


            //数据页面列表绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new Dispatching().GetDispatchingPageByWhere(paramsList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount));
            CtrPageIndex.RecordCount = SourceCount;
        }
    }
}