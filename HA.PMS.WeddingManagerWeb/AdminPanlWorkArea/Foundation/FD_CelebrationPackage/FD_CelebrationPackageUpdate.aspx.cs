using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    public partial class FD_CelebrationPackageUpdate : HA.PMS.Pages.SystemPage
    {
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
        CelebrationPackageImage objCelebrationPackageImageBLL = new CelebrationPackageImage();
        CelebrationPackageProduct objCelebrationPackageProductBLL = new CelebrationPackageProduct();
        //套系图片保存路径
        private string packagImageDir = "~/Files/CelebrationPackage/";
        //套系封面图片保存路径
        private string packageTopImageDir = "~/Files/CelebrationPackage/CelebrationPackageTop/";

        protected void Page_Load(object sender, EventArgs e)
        {
            //用于提示区别套系或者是经典案例
            ViewState["load"] = "'" + EncodeBase64("/AdminPanlWorkArea/Foundation/FD_CelebrationPackage/SaveCeleToDB") + "'";

            if (!IsPostBack)
            {
                if (!System.IO.Directory.Exists(Server.MapPath(packageTopImageDir)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(packageTopImageDir));
                }
                BinderData(sender, e);
            }
        }
        //套系基本信息
        protected void BinderPackageData(object sender, EventArgs e)
        {
            HA.PMS.DataAssmblly.FD_CelebrationPackage fD_CelebrationPackage = objCelebrationPackageBLL.GetByID(Request["PackageID"].ToInt32());
            if (fD_CelebrationPackage != null)
            {
                txtPackageTitle.Text = fD_CelebrationPackage.PackageTitle;
                txtPackagePrice.Text = Convert.ToString(fD_CelebrationPackage.PackagePrice);
                txtPackagePreferentiaPrice.Text = Convert.ToString(fD_CelebrationPackage.PackagePreferentiaPrice);
                txtPackageDirections.Text = fD_CelebrationPackage.PackageDirections;
                txtPackageDetails.Text = fD_CelebrationPackage.PackageDetails;
                txtSum.Text = fD_CelebrationPackage.PackageSum.ToString();
                ListItem packageStyle = ddlPackageStyle.Items.FindByValue(Convert.ToString(fD_CelebrationPackage.PackageSkip));
                if (packageStyle != null)
                {
                    ddlPackageStyle.ClearSelection();
                    packageStyle.Selected = true;
                }
            }
            else
            {
                JavaScriptTools.AlertWindowAndLocation("找不到该套系，该套系可能已被删除", "/AdminPanlWorkArea/Foundation/FD_CelebrationPackage/FD_CelebrationPackageManager.aspx", this.Page);
                Response.Flush();
            }
        }
        //套系风格
        protected void BinderPackageStyle(object sender, EventArgs e)
        {
            ddlPackageStyle.DataSource = new CelebrationPackageStyle().GetByAll();
            ddlPackageStyle.DataTextField = "StyleName";
            ddlPackageStyle.DataValueField = "StyleId";
            ddlPackageStyle.DataBind();
        }
        //套系图片
        protected void BinderPackageImage(object sender, EventArgs e)
        {
            var query = objCelebrationPackageImageBLL.GetByPackageID(Convert.ToInt32(Request["PackageID"]));
            if (query != null && query.Count > 0)
            {
                rptPackageImage.DataBind(query);
                btnReloadPackageImage.Visible = true;
                btnSavePackageImage.Visible = true;
            }
            else
            {
                btnReloadPackageImage.Visible = false;
                btnSavePackageImage.Visible = false;
            }
        }
        //套系搭档
        protected void BinderPackageProduct(object sender, EventArgs e)
        {
            var query = objCelebrationPackageProductBLL.GetByPackageID(Convert.ToInt32(Request["PackageID"]));
            if (query != null && query.Count > 0)
            {
                rptPackageProduct.DataBind(query);
                btnReloalPackageProduct.Visible = true;
                btnSavePackageProduct.Visible = true;
            }
            else
            {
                btnReloalPackageProduct.Visible = false;
                btnSavePackageProduct.Visible = false;
            }
        }

        protected void BinderData(object sender, EventArgs e)
        {
            BinderPackageStyle(sender, e);
            BinderPackageData(sender, e);
            BinderPackageImage(sender, e);
            BinderPackageProduct(sender, e);
        }

        protected void rptPackageImage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int imageId = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Save":
                    FD_CelebrationPackageImage fD_CelebrationPackageImage = objCelebrationPackageImageBLL.GetByID(imageId);
                    string message = ((TextBox)e.Item.FindControl("txtMessage")).Text;
                    fD_CelebrationPackageImage.Message = message.Length > 50 ? message.Substring(0, 50) : message;
                    FileUpload fileUpload = e.Item.FindControl("fpImg") as FileUpload;

                    string filePath = packagImageDir + DateTime.Now.ToFileTimeUtc() + System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                    if (fileUpload.HasFile)
                    {
                        try
                        {
                            fileUpload.SaveAs(Server.MapPath(filePath));
                        }
                        catch (Exception ex)
                        {
                            LinkButton lbtn = source as LinkButton;
                            if (lbtn != null && lbtn.ID != btnSavePackageImage.ID)
                            {
                                JavaScriptTools.AlertWindow("保存图片失败,请重试", Page);
                            }
                            objCelebrationPackageImageBLL.Update(fD_CelebrationPackageImage);
                            return;
                        }

                        fD_CelebrationPackageImage.ImageUrl = filePath;
                        string filename = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                        fD_CelebrationPackageImage.ImageName = filename.Length > 64 ? filename.Substring(0, 64) : filename;
                        Image img = (Image)e.Item.FindControl("img");
                        img.ImageUrl = filePath;
                    }
                    objCelebrationPackageImageBLL.Update(fD_CelebrationPackageImage);
                    break;
                case "Delete":
                    objCelebrationPackageImageBLL.Delete(new FD_CelebrationPackageImage { ImageId = imageId });
                    break;
                default: break;
            }
        }

        protected void rptPackageProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int PackageProductID = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Save":
                    FD_CelebrationPackageProduct fD_CelebrationPackageProduct = objCelebrationPackageProductBLL.GetByID(PackageProductID);
                    fD_CelebrationPackageProduct.PackagePrice = ((TextBox)e.Item.FindControl("txtPackagePrice")).Text.ToDecimal();
                    objCelebrationPackageProductBLL.Update(fD_CelebrationPackageProduct); break;
                case "Delete":
                    objCelebrationPackageProductBLL.Delete(new FD_CelebrationPackageProduct { PackageProductID = PackageProductID });
                    break;
                default: break;
            }
        }
        //保存所有套系图片
        protected void btnSavePackageImage_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptPackageImage.Items.Count; i++)
            {
                int imageId = Convert.ToInt32(((HiddenField)rptPackageImage.Items[i].FindControl("hideImageId")).Value);
                RepeaterCommandEventArgs args = new RepeaterCommandEventArgs(rptPackageImage.Items[i], btnSavePackageImage, new CommandEventArgs("Save", imageId));
                rptPackageImage_ItemCommand(btnSavePackageImage, args);
            }
            if (sender != null)
            {
                JavaScriptTools.AlertWindow("保存成功", Page);
            }
        }
        //保存所有套系搭档
        protected void btnSavePackageProduct_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptPackageProduct.Items.Count; i++)
            {
                int packageProductID = Convert.ToInt32(((HiddenField)rptPackageProduct.Items[i].FindControl("hidePackageProductID")).Value);
                RepeaterCommandEventArgs args = new RepeaterCommandEventArgs(rptPackageProduct.Items[i], btnSavePackageProduct, new CommandEventArgs("Save", packageProductID));
                rptPackageProduct_ItemCommand(btnSavePackageProduct, args);
            }
            if (sender != null)
            {
                JavaScriptTools.AlertWindow("保存成功", Page);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 套系基本信息
            int PackageID = Request["PackageID"].ToInt32();

            HA.PMS.DataAssmblly.FD_CelebrationPackage fD_CelebrationPackage = objCelebrationPackageBLL.GetByID(PackageID);
            fD_CelebrationPackage.PackageTitle = txtPackageTitle.Text;
            fD_CelebrationPackage.PackagePrice = txtPackagePrice.Text.ToDecimal();
            fD_CelebrationPackage.PackagePreferentiaPrice = txtPackagePreferentiaPrice.Text.ToDecimal();
            fD_CelebrationPackage.PackageDirections = txtPackageDirections.Text;
            fD_CelebrationPackage.PackageDetails = txtPackageDetails.Text;
            fD_CelebrationPackage.PackageSkip = ddlPackageStyle.SelectedValue.ToInt32();
            fD_CelebrationPackage.PackageSum = txtSum.Text.ToInt32();
            fD_CelebrationPackage.DesignEmployee = hideEmpLoyeeID.Value.ToInt32();

            HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
            var ObjUpdateModel = ObjQuotedPriceBLL.GetByKind(fD_CelebrationPackage.PackageID, 2);
            ObjUpdateModel.QuotedTitle = fD_CelebrationPackage.PackageTitle;
            ObjQuotedPriceBLL.Update(ObjUpdateModel);
            if (flCelePackage.HasFile)
            {
                string filePath = packageTopImageDir + DateTime.Now.ToFileTimeUtc() + System.IO.Path.GetExtension(flCelePackage.FileName).ToLower();
                if (!Directory.Exists(Server.MapPath(packageTopImageDir)))
                {
                    Directory.CreateDirectory(Server.MapPath(packageTopImageDir));
                }
                try
                {
                    flCelePackage.SaveAs(Server.MapPath(filePath));
                }
                catch
                {
                    JavaScriptTools.AlertWindow("保存封面图片失败，请重试", Page);
                    objCelebrationPackageBLL.Update(fD_CelebrationPackage);
                    return;
                }
                fD_CelebrationPackage.PackageImgTop = filePath;
            }
            objCelebrationPackageBLL.Update(fD_CelebrationPackage);
            #endregion

            //套系封面图片
            btnSavePackageImage_Click(null, e);

            //套系最佳拍档
            btnSavePackageProduct_Click(null, e);

            JavaScriptTools.AlertWindowAndLocation("保存成功", "/AdminPanlWorkArea/Foundation/FD_CelebrationPackage/FD_CelebrationPackageManager.aspx", this.Page);
        }
    }
}