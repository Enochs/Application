using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_CategoryParentUpdate : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        Category objCategoryBLL = new Category();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CategoryId = Request.QueryString["CategoryId"].ToInt32();
                FD_Category fd_Category = objCategoryBLL.GetByID(CategoryId);
                txtCategoryName.Text = fd_Category.CategoryName;
                if (fd_Category.Productproperty == 0)
                {
                    rdoPerson.Checked = true;
                }
                else
                {
                    rdoProduct.Checked = true;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            // 返回一个顶级的最后一个子节点的sortOrder， 
            int CategoryId = Request.QueryString["CategoryId"].ToInt32();
            FD_Category fd_Category = objCategoryBLL.GetByID(CategoryId);

            fd_Category.CategoryName = txtCategoryName.Text;


            if (rdoProduct.Checked)
            {
                fd_Category.Productproperty = 1;
            }
            else
            {
                fd_Category.Productproperty = 0;
            }
            int result = objCategoryBLL.Update(fd_Category);
            //自动添加到库房产品里面
            FD_StorehouseSourceProduct fd_Storehouse = objStorehouseSourceProductBLL.GetByStorehouseSourceID(result);
            if (fd_Storehouse != null)
            {
                fd_Storehouse.IsDelete = false;
                fd_Storehouse.ProductCategory = result;
                //注意以后如果进行 修改或者删除的操作，
                //可以根据 当前在这里添加的库房ID字段 区别在库房界面直接添加的库房产品
                fd_Storehouse.ProductState = "编辑";
                fd_Storehouse.ProductProject = result;
                fd_Storehouse.SourceProductName = txtCategoryName.Text;
                objStorehouseSourceProductBLL.Update(fd_Storehouse);
            }
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("操作成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("操作失败,请重新尝试", this.Page);

            }
        }
    }
}