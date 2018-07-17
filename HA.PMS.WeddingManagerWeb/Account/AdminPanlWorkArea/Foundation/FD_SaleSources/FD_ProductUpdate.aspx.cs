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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_ProductUpdate : SystemPage
    {
        Productcs objProductBLL = new Productcs();
        Category objCategoryBLL = new Category();
        Supplier objSupplierBLL = new Supplier();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int SourceProductId = Request.QueryString["ProductID"].ToInt32();

                FD_Product fd_Storehouse = objProductBLL.GetByID(SourceProductId);



                ListItem ddlCategoryInit = ddlCategory.Items.FindByValue(fd_Storehouse.CategoryID + string.Empty);
                if (ddlCategoryInit != null)
                {
                    ddlCategoryInit.Selected = true;
                }
                DataDropDownList();
                ListItem ddlProjectInit = ddlProject.Items.FindByValue(fd_Storehouse.ProductProject + string.Empty);
                if (ddlProjectInit != null)
                {
                    ddlProjectInit.Selected = true;
                }
                txtRemark.Text = fd_Storehouse.Remark;

                txtPurchasePrice.Text = fd_Storehouse.ProductPrice + string.Empty;
                txtSaleOrice.Text = fd_Storehouse.SalePrice + string.Empty;
                txtUnit.Text = fd_Storehouse.Unit + string.Empty;
                txtSpecifications.Text = fd_Storehouse.Specifications;
                txtExplain.Text = fd_Storehouse.Explain;
                txtSourceProductName.Text = fd_Storehouse.ProductName;

                hideSuppID.Value = fd_Storehouse.SupplierID.ToString();
                txtSuppName.Value = new Supplier().GetByID(fd_Storehouse.SupplierID).Name;
            }
        }
        protected void DataDropDownList()
        {
            ddlProject.ParentID = ddlCategory.SelectedValue.ToInt32();
            ddlProject.BinderByparent();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            int SourceProductId = Request.QueryString["ProductID"].ToInt32();
            FD_Product fd_Storehouse = objProductBLL.GetByID(SourceProductId);

            if (Request["IsSupplierShow"] == "1")
            {
                if (hideSuppID.Value.ToInt32() > 0)
                {
                    fd_Storehouse.SupplierID = hideSuppID.Value.ToInt32();
                }
                else
                {
                    JavaScriptTools.AlertWindow("请选择供应商！", Page); return;
                }

            }

            fd_Storehouse.IsDelete = false;

            fd_Storehouse.ProductName = txtSourceProductName.Text;


            fd_Storehouse.CategoryID = ddlCategory.SelectedValue.ToInt32();
            fd_Storehouse.ProductProject = ddlProject.SelectedValue.ToInt32();
            fd_Storehouse.Specifications = txtSpecifications.Text;

            fd_Storehouse.ProductPrice = txtPurchasePrice.Text.ToDecimal();
            fd_Storehouse.SalePrice = txtSaleOrice.Text.ToDecimal();
            fd_Storehouse.Explain = txtExplain.Text;
            fd_Storehouse.Unit = txtUnit.Text;
            fd_Storehouse.Remark = txtRemark.Text;
            string savaPath = "~/Files/Supplier/";
            string fileExt = "";

            bool isOk = true;
            if (flieUp.HasFile)
            {

                fileExt = System.IO.Path.GetExtension(flieUp.FileName).ToLower();
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    fd_Storehouse.Data = savaPath + DateTime.Now.ToFileTimeUtc() + fileExt;
                    flieUp.SaveAs(Server.MapPath(fd_Storehouse.Data));
                }
                else
                {
                    isOk = false;
                    JavaScriptTools.AlertWindow("请你上传的图片只能是jpeg png jpg gif bmp", this.Page);

                }


            }
            if (isOk)
            {
                int result = objProductBLL.Update(fd_Storehouse);
                //根据返回判断添加的状态
                if (result > 0)
                {
                    //JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
                    JavaScriptTools.RegisterJsCodeSource("alert('修改成功！');parent.window.location.reload();parent.$.fancybox.close(1)", this.Page);
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

                }
            }
        }
    }
}