using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseProductUpdate : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
                FD_StorehouseSourceProduct fd_Storehouse = objStorehouseSourceProductBLL.GetByID(SourceProductId);

                ListItem ddlCategoryInit=null;
                try
                {
                    ddlCategoryInit = ddlCategory.Items.FindByValue(fd_Storehouse.ProductCategory + string.Empty);
                }
                catch
                { }
                if (ddlCategoryInit != null)
                {
                    ddlCategoryInit.Selected = true;
                }

                DataDropDownList();
                ListItem ddlProjectInit = null;
                try
                {
                    ddlProjectInit = ddlCategory.Items.FindByValue(fd_Storehouse.ProductCategory + string.Empty);
                }
                catch
                { }
              
                if (ddlProjectInit != null)
                {
                    ddlProjectInit.Selected = true;
                }

                txtPurchasePrice.Text = fd_Storehouse.PurchasePrice + string.Empty;
                txtSaleOrice.Text = fd_Storehouse.SaleOrice + string.Empty;
                txtRemark.Text = fd_Storehouse.Remark;
                //txtSourceCount.Text = fd_Storehouse.SourceCount + string.Empty;
                txtUnit.Text = fd_Storehouse.Unit + string.Empty;
                txtSpecifications.Text = fd_Storehouse.Specifications;
                txtPutStoreDates.Text = fd_Storehouse.PutStoreDate + string.Empty;
                txtSourceProductName.Text = fd_Storehouse.SourceProductName;
                txtPosi.Text = fd_Storehouse.Position;


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
            int SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
            FD_StorehouseSourceProduct fd_Storehouse = objStorehouseSourceProductBLL.GetByID(SourceProductId);
            fd_Storehouse.IsDelete = false;

            fd_Storehouse.SourceProductName = txtSourceProductName.Text;

            fd_Storehouse.PutStoreDate = txtPutStoreDates.Text.ToDateTime();
            fd_Storehouse.ProductCategory = ddlCategory.SelectedValue.ToInt32();
            fd_Storehouse.ProductProject = ddlProject.SelectedValue.ToInt32();
            fd_Storehouse.Specifications = txtSpecifications.Text;
            fd_Storehouse.Remark = txtRemark.Text;
            fd_Storehouse.PurchasePrice = txtPurchasePrice.Text.ToDecimal();
            fd_Storehouse.SaleOrice = txtSaleOrice.Text.ToDecimal();
            if (ddlDisposible.SelectedValue.ToInt32() == 1)
            {
                fd_Storehouse.IsDisposible = true;/*一次性*/
            }
            else
            {
                fd_Storehouse.IsDisposible = false;
            }
           //fd_Storehouse.SourceCount = txtSourceCount.Text.ToInt32();
            fd_Storehouse.Unit = txtUnit.Text;
            fd_Storehouse.Position = txtPosi.Text;
            string savaPath = "~/Files/Storehouse/";
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
                
                int result = objStorehouseSourceProductBLL.Update(fd_Storehouse);
                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.ResponseScript("alert('修改成功');parent.$.fancybox.close(1);$(parent.window.document.getElementById('ContentPlaceHolder1_btnbinderdata')).click();", Page);
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);
                }
            }
            
        }
    }
}