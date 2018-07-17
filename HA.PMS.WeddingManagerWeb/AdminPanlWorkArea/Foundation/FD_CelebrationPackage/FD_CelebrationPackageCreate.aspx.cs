using System;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    public partial class FD_CelebrationPackageCreate : HA.PMS.Pages.SystemPage
    {
        private string packageTopImageDir = "~/Files/CelebrationPackage/CelebrationPackageTop/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!System.IO.Directory.Exists(Server.MapPath(packageTopImageDir)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(packageTopImageDir));
                }
                BinderData(sender, e);
            }
        }


        protected void BinderData(object sender, EventArgs e)
        {
            ddlPackageStyle.DataSource = new BLLAssmblly.FD.CelebrationPackageStyle().GetByAll();
            ddlPackageStyle.DataTextField = "StyleName";
            ddlPackageStyle.DataValueField = "StyleId";
            ddlPackageStyle.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!flCelePackage.HasFile)
            {
                JavaScriptTools.AlertWindow("请上传封面图片", Page);
                return;
            }

            string filePath = packageTopImageDir + DateTime.Now.ToFileTimeUtc() + System.IO.Path.GetExtension(flCelePackage.FileName).ToLower();

            try
            {
                flCelePackage.SaveAs(Server.MapPath(filePath));
            }
            catch
            {
                JavaScriptTools.AlertWindow("上传封面图片失败，请重试", Page);
                return;
            }

            HA.PMS.DataAssmblly.FD_CelebrationPackage fD_CelebrationPackage = new DataAssmblly.FD_CelebrationPackage();
            fD_CelebrationPackage.PackageTitle = txtPackageTitle.Text;
            fD_CelebrationPackage.PackagePrice = txtPackagePrice.Text.ToDecimal();
            fD_CelebrationPackage.PackagePreferentiaPrice = txtPackagePreferentiaPrice.Text.ToDecimal();
            fD_CelebrationPackage.PackageDirections = txtPackageDirections.Text;
            fD_CelebrationPackage.PackageDetails = txtPackageDetails.Text;
            fD_CelebrationPackage.PackageSkip = ddlPackageStyle.SelectedValue.ToInt32();
            fD_CelebrationPackage.PackageDate = DateTime.Now;
            fD_CelebrationPackage.PackageType = 0;
            fD_CelebrationPackage.IsDelete = false;
            fD_CelebrationPackage.PackageImgTop = filePath;
            fD_CelebrationPackage.CreateEmployee = User.Identity.Name.ToInt32();
            fD_CelebrationPackage.PackageSum = txtSum.Text.ToInt32();
            int packageID = new BLLAssmblly.FD.CelebrationPackage().Insert(fD_CelebrationPackage);


            #region 创建套系报价单
            FL_QuotedPrice fL_QuotedPrice = new FL_QuotedPrice();
            fL_QuotedPrice.CustomerID = -1;
            fL_QuotedPrice.OrderID = -1;
            fL_QuotedPrice.CategoryName = "套系报价单";
            fL_QuotedPrice.EmpLoyeeID = User.Identity.Name.ToInt32();
            fL_QuotedPrice.IsChecks = false;
            fL_QuotedPrice.IsDelete = true;
            fL_QuotedPrice.CreateDate = DateTime.Now;
            fL_QuotedPrice.IsFirstCreate = false;
            fL_QuotedPrice.OrderCoder = "套系预制报价单";
            fL_QuotedPrice.ParentQuotedID = 0;
            fL_QuotedPrice.QuotedKind = fD_CelebrationPackage.PackageID;
            fL_QuotedPrice.QuotedType = 2;
            fL_QuotedPrice.QuotedTitle = txtPackageTitle.Text;
            fL_QuotedPrice.QuotedTitle = "套系预制报价单";
            fL_QuotedPrice.RealAmount = 0;
            fL_QuotedPrice.FinishAmount = 0;
            fL_QuotedPrice.HaveFile = true;
            fL_QuotedPrice.IsDispatching = 99;
            fL_QuotedPrice.Dispatching = -99;
            fL_QuotedPrice.StarDispatching = true;
            fL_QuotedPrice.PakegTyper = string.Empty;
            new BLLAssmblly.Flow.QuotedPrice().Insert(fL_QuotedPrice);
            #endregion

            if (packageID > 0)
            {
                Response.Redirect(string.Concat("~/AdminPanlWorkArea/Foundation/FD_CelebrationPackage/FD_CelebrationPackageUpdate.aspx?PackageID=", packageID), true);
            }
            else
            {
                JavaScriptTools.AlertWindow("保存失败，请重试", Page);
            }
        }
    }
}