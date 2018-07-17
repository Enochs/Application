using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class SelectCategoryProject : SystemPage
    {
        Category objCategoryBLL = new Category();
        AllProducts objAllProductsBLL = new AllProducts();

        QuotedCatgory ObjQuotedCategoryBLL = new QuotedCatgory();
        string Type = "";

        /// <summary>
        /// 产品
        /// </summary>
        StorehouseSourceProduct ObjStorehouseSourceProductBLL = new StorehouseSourceProduct();

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type = Request["Type"].ToString();
            if (!IsPostBack)
            {
                if (Type == "Store")
                {
                    SelectEmployee.Visible = true;
                    divSelectQuotedCategory.Visible = false;
                    DataLeftTreeBinder();
                }
                else if (Type == "Quoted")
                {
                    SelectEmployee.Visible = false;
                    divSelectQuotedCategory.Visible = true;
                    QuotedDataBinder();
                }
            }
        }
        #endregion

        #region 数据绑定 库房
        /// <summary>
        /// 数据绑定 库房
        /// </summary>
        protected void DataLeftTreeBinder()
        {
            var query = objCategoryBLL.GetByAll();
            List<FD_Category> parentList = query.Where(C => C.ParentID == 0).ToList();
            for (int i = 0; i < parentList.Count; i++)
            {
                TreeNode singerNode = new TreeNode();
                singerNode.Text = parentList[i].CategoryName;
                singerNode.Value = parentList[i].CategoryID + string.Empty;
                //singerNode.NavigateUrl = this.Request.Url + "&CategoryID=" + parentList[i].CategoryID;
                if (!parentList[i].CategoryName.Contains("待分配产品"))
                {
                    treeCategory.Nodes.Add(singerNode);
                }
            }
        }
        #endregion

        #region 数据绑定 报价单
        /// <summary>
        /// 报价单 数据绑定
        /// </summary>
        public void QuotedDataBinder()
        {
            var query = ObjQuotedCategoryBLL.GetByAll();
            List<FD_QuotedCatgory> parentList = query.Where(C => C.Parent == 0).ToList();
            for (int i = 0; i < parentList.Count; i++)
            {
                TreeNode singerNode = new TreeNode();
                singerNode.Text = parentList[i].Title;
                singerNode.Value = parentList[i].QCKey + string.Empty;
                //singerNode.NavigateUrl = this.Request.Url + "&CategoryID=" + parentList[i].CategoryID;
                if (!parentList[i].Title.Contains("待分配产品"))
                {
                    treeQuotedCategory.Nodes.Add(singerNode);
                }
            }
        }
        #endregion

        #region 树形的选择变化事件
        /// <summary>
        /// 选择变化
        /// </summary>
        protected void treeCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            int parentCategoryId = this.treeCategory.SelectedNode.Value.ToInt32();
            var childQuery = objCategoryBLL.GetByAll().Where(C => C.ParentID == parentCategoryId).ToList();
            if (childQuery.Count != 0)
            {
                rptProjectList.DataSource = childQuery;
                rptProjectList.DataBind();
            }
        }
        #endregion

        #region 分配库房之后的确定事件
        /// <summary>
        /// 点击确定时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {

            Button btnConfrim = (sender as Button);
            //分配到所有产品
            string rdo = Request["rdo"];

            int Keys = Request.QueryString["Keys"].ToInt32();
            int childParent = rdo.ToInt32();

            //获取HouseID
            Storehouse ObjStorehouseBLL = new Storehouse();
            int HouseID = ObjStorehouseBLL.GetKeyByManager(User.Identity.Name.ToInt32());

            if (btnConfrim.ID == "btnConfrim" || btnConfrim.ID == "btnSaveSelect")
            {
                FD_Category parentCategory = objCategoryBLL.GetByID(childParent);
                int parentParent = parentCategory.ParentID;
                FD_AllProducts fdEdit = objAllProductsBLL.GetByID(Keys);
                fdEdit.ProductCategory = parentParent;
                fdEdit.ProjectCategory = childParent;
                objAllProductsBLL.Update(fdEdit);

                //加入库房
                //获取在列表界面选择的产品
                FD_AllProducts allProducts = objAllProductsBLL.GetByID(Keys);

                FD_StorehouseSourceProduct ObjStorehouseSourceProductInsertModel = new FD_StorehouseSourceProduct();

                ObjStorehouseSourceProductInsertModel.SourceCount = allProducts.Count;
                ObjStorehouseSourceProductInsertModel.Data = allProducts.Data;
                ObjStorehouseSourceProductInsertModel.IsDelete = false;

                ObjStorehouseSourceProductInsertModel.ProductCategory = parentParent;
                ObjStorehouseSourceProductInsertModel.ProductProject = childParent;

                ObjStorehouseSourceProductInsertModel.PurchasePrice = fdEdit.PurchasePrice;
                ObjStorehouseSourceProductInsertModel.SaleOrice = allProducts.SalePrice;
                ObjStorehouseSourceProductInsertModel.Specifications = allProducts.Explain;
                ObjStorehouseSourceProductInsertModel.SourceProductName = allProducts.ProductName;
                ObjStorehouseSourceProductInsertModel.StorehouseID = HouseID;/*库房ID*/

                ObjStorehouseSourceProductInsertModel.Unit = allProducts.Unit;
                ObjStorehouseSourceProductInsertModel.Remark = allProducts.Remark;

                ObjStorehouseSourceProductBLL.Insert(ObjStorehouseSourceProductInsertModel);

                //由于插入产品时会自动插入到所有产品，所以需要删除在原来所有产品中新购入的产品
                objAllProductsBLL.Delete(Keys);

                JavaScriptTools.AlertAndCloseJumpfancybox("/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_ProductTobeDistributed.aspx", this.Page);
            }
            else if (btnConfrim.ID == "btnConfirmQuoted" || btnConfrim.ID == "btnConfirmQuoteds")
            {
                FD_QuotedCatgory parentCategory = ObjQuotedCategoryBLL.GetByID(childParent);
                int parentParent = parentCategory.Parent.ToString().ToInt32();
                FD_AllProducts fdEdit = objAllProductsBLL.GetByID(Keys);
                fdEdit.ProductCategory = parentParent;
                fdEdit.ProjectCategory = childParent;
                objAllProductsBLL.Update(fdEdit);
                //加入报价单
                FD_StorehouseSourceProduct fd_Storehouse = new FD_StorehouseSourceProduct();
                FD_AllProducts allProducts = objAllProductsBLL.GetByID(Keys);
                FD_StorehouseSourceProduct ObjStorehouseSourceProductInsertModel = new FD_StorehouseSourceProduct();
                ObjStorehouseSourceProductInsertModel.SourceCount = allProducts.Count;

                ObjStorehouseSourceProductInsertModel.Data = allProducts.Data;
                ObjStorehouseSourceProductInsertModel.IsDelete = false;

                ObjStorehouseSourceProductInsertModel.ProductCategory = parentParent;
                ObjStorehouseSourceProductInsertModel.ProductProject = childParent;

                ObjStorehouseSourceProductInsertModel.PurchasePrice = fdEdit.PurchasePrice;
                ObjStorehouseSourceProductInsertModel.SaleOrice = allProducts.SalePrice;
                ObjStorehouseSourceProductInsertModel.Specifications = allProducts.Explain;
                ObjStorehouseSourceProductInsertModel.SourceProductName = allProducts.ProductName;
                ObjStorehouseSourceProductInsertModel.StorehouseID = HouseID;/*库房ID*/

                string ClassType = string.Empty;            //获取产品属性
                foreach (ListItem Objitem in CheckBoxList1.Items)
                {
                    if (Objitem.Selected)
                    {
                        ClassType += Objitem.Value + ",";
                    }
                }
                ObjStorehouseSourceProductInsertModel.Unit += "VirtualProduct" + "①" + ClassType.TrimEnd(',');
                ObjStorehouseSourceProductInsertModel.Remark = allProducts.Remark;

                ObjStorehouseSourceProductInsertModel.ProductState = "编辑";
                ObjStorehouseSourceProductBLL.Insert(ObjStorehouseSourceProductInsertModel);

                //由于插入产品时会自动插入到所有产品，所以需要删除在原来所有产品中新购入的产品
                objAllProductsBLL.Delete(Keys);
                JavaScriptTools.AlertAndCloseJumpfancybox("/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_ProductTobeDistributed.aspx", this.Page);
            }

        }
        #endregion

        #region 树形的选择变化 报价单
        /// <summary>
        /// 选择变化事件
        /// </summary>
        protected void treeQuotedCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            int parentCategoryId = this.treeQuotedCategory.SelectedNode.Value.ToInt32();
            var childQuery = ObjQuotedCategoryBLL.GetByAll().Where(C => C.Parent == parentCategoryId).ToList();
            if (childQuery.Count != 0)
            {
                RepQuotedCategory.DataSource = childQuery;
                RepQuotedCategory.DataBind();
            }
        }
        #endregion



    }
}