using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class SaleTargetManager : SystemPage
    {
        SaleTarget objSaleTargetBLL = new SaleTarget();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder() 
        {

            int startIndex = SaleTargetPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objSaleTargetBLL.GetByIndex(SaleTargetPager.PageSize, SaleTargetPager.CurrentPageIndex, out resourceCount);
            SaleTargetPager.RecordCount = resourceCount;

            rptSaleTarget.DataSource = query;
            rptSaleTarget.DataBind();
        
        }
        protected void SaleTargetPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}