using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SupplierProductSignIn : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }

        }

        /// <summary>
        /// 根据产品 ID 返回所属供应商的名称
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        protected string GetSupplierName(object ProductID)
        {
            FD_Product fD_Product = new Productcs().GetByID(Convert.ToInt32(ProductID));
            if (fD_Product != null)
            {
                FD_Supplier fD_Supplier = new Supplier().GetByID(fD_Product.SupplierID);
                return fD_Supplier != null ? fD_Supplier.Name : string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BinderData(object sender, EventArgs e)
        {
            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add(!string.IsNullOrEmpty(txtProductName.Text), "ProductName_LIKE", txtProductName.Text.Trim());
            paramsList.Add(!string.IsNullOrEmpty(txtSupplierName.Text), "Name_LIKE", txtSupplierName.Text.Trim());


            int startIndex = StorePager.StartRecordIndex;
            int resourceCount = 0;

            int CategoryID = 0;
            Category ObjCategoryBLL = new Category();
            var ObjCategoryQueryModel = new BLLAssmblly.FD.Category().GetByAll().Where(C => C.CategoryName.Contains("待分配产品")).FirstOrDefault();
            if (ObjCategoryQueryModel == null)
            {
                CategoryID = ObjCategoryBLL.Insert(new FD_Category { CategoryName = "待分配产品", ParentID = 0, SortOrder = "9999", IsDelete = false });
            }
            else
            {
                CategoryID = ObjCategoryQueryModel.CategoryID;
            }
            var listResultContent = new Productcs().GetByIndexOfCategoryID(CategoryID, StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            StorePager.RecordCount = resourceCount;
            rptStorehouse.DataSource = listResultContent;
            rptStorehouse.DataBind();
        }

        protected void rptStorehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (new Productcs().Delete(new FD_Product() { ProductID = Convert.ToInt32(e.CommandArgument) }) > 0)
            {
                JavaScriptTools.AlertWindow("删除成功", this.Page);
                BinderData(source, e);
            }
            else
            {
                JavaScriptTools.AlertWindow("删除失败,请重新尝试", this.Page);
            }
        }
    }
}