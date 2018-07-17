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
    public partial class FD_CelebrationPackageDetails : SystemPage
    {
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
        CelebrationPackageStyle objCelebrationPackageStyleBLL = new CelebrationPackageStyle();
        CelebrationPackagePriceSpan objCelebrationPackagePriceSpan = new CelebrationPackagePriceSpan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }
        protected void DataDropDownList()
        {
            ListItem firstChoose = new ListItem("请选择", "0");
            ddlPackageStyle.DataSource = objCelebrationPackageStyleBLL.GetByAll();
            ddlPackageStyle.DataTextField = "StyleName";
            ddlPackageStyle.DataValueField = "StyleId";
            ddlPackageStyle.DataBind();
            ddlPackageStyle.Items.Add(firstChoose);
            ddlPackageStyle.SelectedIndex = ddlPackageStyle.Items.Count - 1;

            ddlPriceSpan.DataSource = objCelebrationPackagePriceSpan.GetByAll();
            ddlPriceSpan.DataTextField = "SpanPrice";
            ddlPriceSpan.DataValueField = "SpanID";
            ddlPriceSpan.DataBind();
            ddlPriceSpan.Items.Add(firstChoose);

            ddlPriceSpan.SelectedIndex = ddlPriceSpan.Items.Count - 1;

        }

        protected string GetPackage(object source)
        {
            int skipId = (source + string.Empty).ToInt32();
            HA.PMS.DataAssmblly.FD_CelebrationPackageStyle style = objCelebrationPackageStyleBLL.GetByID(skipId);
            if (style != null)
            {
                return style.StyleName;
            }
            return "";
        }
        protected void DataBinder()
        {

            HA.PMS.DataAssmblly.FD_CelebrationPackage fdPackage = new DataAssmblly.FD_CelebrationPackage();
            fdPackage.PackageSkip = ddlPackageStyle.SelectedValue.ToInt32();
            fdPackage.SpanID = ddlPriceSpan.SelectedValue.ToInt32();
            List<ObjectParameter> ObjectParameterList = new List<ObjectParameter>();
            if (ddlPackageStyle.SelectedItem.Text != "请选择")
            {
                ObjectParameterList.Add(new ObjectParameter("PackageSkip", fdPackage.PackageSkip));
            }
            if (ddlPriceSpan.SelectedItem.Text != "请选择")
            {
                ObjectParameterList.Add(new ObjectParameter("SpanID", fdPackage.SpanID));
            }

            #region 分页页码
            int startIndex = PackagePager.StartRecordIndex;
            int resourceCount = 0;

            var query = objCelebrationPackageBLL.GetPackageDataByParameter(ObjectParameterList.ToArray(),
                PackagePager.PageSize, PackagePager.CurrentPageIndex, out resourceCount);

            PackagePager.RecordCount = resourceCount;

            rptPackage.DataSource = query;
            rptPackage.DataBind();



            #endregion
        }

        protected void PackagePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
    }

}
