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
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;


namespace HA.PMS.WeddingManagerWeb.TheStage.Foundation
{
    public partial class FD_CelebrationPackageDetails2 : System.Web.UI.Page
    {
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
        CelebrationPackageStyle objCelebrationPackageStyleBLL = new CelebrationPackageStyle();
        CelebrationPackagePriceSpan objCelebrationPackagePriceSpan = new CelebrationPackagePriceSpan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder() 
        {
            int PackageType = Request.QueryString["PackageType"].ToInt32();
            var query = objCelebrationPackageBLL.GetPackageDataByParameter().Where(C => C.PackageType == PackageType && C.IsDelete == false).OrderByDescending(C => C.PackageDate);
            rptCelePackageTop.DataSource = query.Take(2);
            rptCelePackageTop.DataBind();
            rptCelePackageList.DataSource = query.Skip(2);
            rptCelePackageList.DataBind();
        }
    }
}