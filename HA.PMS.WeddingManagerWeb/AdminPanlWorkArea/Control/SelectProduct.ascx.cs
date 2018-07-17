using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using System.Web.UI.HtmlControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectProduct : System.Web.UI.UserControl
    {
        /// <summary>
        /// 产品
        /// </summary>
        AllProducts ObjProductBLL = new AllProducts();

        /// <summary>
        /// 库房
        /// </summary>
        Storehouse ObjStorehouseBLL = new Storehouse();

        /// <summary>
        /// 报价单类别
        /// </summary>
        Category ObjCategoryBLL = new Category();

        /// <summary>
        /// 供应商
        /// </summary>
        Supplier ObjSupplierBLL = new Supplier();



        QuotedCatgory ObjQCategoryBLL = new QuotedCatgory();
        /// <summary>
        /// 系统配置
        /// </summary>
        SysConfig ObjConfigBLL = new SysConfig();

        BLLAssmblly.FD.StorehouseSourceProduct StorehouseSourceProductBLL = new StorehouseSourceProduct();

        public string GetUseCountByTimerSpan(object ProductID, string TimerSpan)
        {

            //return "0";
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            return ObjProductforDispatchingBLL.UseforCustomerorTimerSpan(ProductID.ToString().ToInt32(), Request["PartyDate"].ToDateTime(), TimerSpan).ToString();
        }


        public string SetStyleforSystemControlKey(string Key, int Type)
        {
            if (Request["SU"] == null)
            {
                return ObjConfigBLL.SetStyleforSystemControlKey(Key, Type);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var TreeSource = ObjCategoryBLL.GetByAll();
                BinderByParent(0, ObjQCategoryBLL.GetByAll(), null);



                // BinderQCatgoryByParent(0, ObjQCategoryBLL.GetByAll(), null, this.TreeSaleItem);
                BinderData();

                if (Request["SupplierName"] != "库房" && Request["SupplierName"] != "" && Request["SupplierName"] != null)
                {
                    btnSupp.Text = Request["SupplierName"];
                }
                else
                {
                    btnSupp.Text = "供应商";
                }
            }
        }



        /// <summary>
        /// 根据ID获取库房名称
        ///
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetHouseNameByID(object Key)
        {
            if (Key != null)
            {
                return ObjStorehouseBLL.GetByID(Key.ToString().ToInt32()).HouseName;
            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 绑定类型下的产品
        /// </summary>
        private void BinderData()
        {

            this.ddlSupply.DataSource = ObjSupplierBLL.GetByAll();
            this.ddlSupply.DataTextField = "Name";
            this.ddlSupply.DataValueField = "SupplierID";
            this.ddlSupply.DataBind();

            this.ddlSupply.Items.Add(new ListItem("库房", "库房"));
            this.ddlSupply.Items.Add(new ListItem("新购买", "新购买"));
            this.ddlSupply.Items[this.ddlSupply.Items.Count - 1].Selected = true;

            /////当有指定供应商的时候就指定某个供应商
            //if (Request["SupplierName"] != "库房" && Request["SupplierName"] != "")
            //{
            //    this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.Nodes[0].Value.ToInt32()).Where(C => C.SupplierName == Request["SupplierName"].ToString());
            //}
            //else
            //{
            //    this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.Nodes[0].Value.ToInt32());
            //}
            //this.repProductByCatogryList.DataBind();


            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByVType(9, treeCatogryList.Nodes[0].Value.ToInt32());
            this.repProductByCatogryforWarehouseList.DataBind();



            //this.repSuppProductList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.Nodes[0].Value.ToInt32());

            //this.repSuppProductList.DataBind();

        }




        /// <summary>
        /// 新购入产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateDate_Click(object sender, EventArgs e)
        {
            FD_AllProducts ObjAllProductsModel = new FD_AllProducts();
            QuotedCatgory ObjCategoryBLL = new QuotedCatgory();
            var ObjCategoryModel = ObjCategoryBLL.GetByID(Request["CategoryID"].ToInt32());
            ObjAllProductsModel.ProductName = txtProductName.Text;
            ObjAllProductsModel.KindID = 0;
            ObjAllProductsModel.Type = 1;
            if (ddlSupply.SelectedItem.Text == "库房")
            {
                ObjAllProductsModel.Type = 2;
            }
            if (ddlSupply.SelectedItem.Text == "新购买")
            {
                ObjAllProductsModel.Type = 3;
            }


            ObjAllProductsModel.Unit = txtUnit.Text;

            ObjAllProductsModel.Count = txtCount.Text.ToInt32();
            ObjAllProductsModel.SalePrice = txtSalePrice.Text.ToDecimal();
            //ObjAllProductsModel.ProductCategory = ObjCategoryModel.Parent;
            //ObjAllProductsModel.ProjectCategory = ObjCategoryModel.QCKey;
            ObjAllProductsModel.Explain = txtExplain.Text;
            ObjAllProductsModel.Specifications = txtExplain.Text;
            ObjAllProductsModel.PurchasePrice = txtPurchasePrice.Text.ToDecimal();
            ObjAllProductsModel.Remark = txtRemark.Text;
            ObjAllProductsModel.SupplierName = ddlSupply.SelectedItem.Text;
            ObjAllProductsModel.IsTobeDistributed = false;
            ObjAllProductsModel.Productproperty = rdotyper.SelectedValue.ToInt32();
            JavaScriptTools.SetValueByParentControl(Request["ControlKey"], ObjProductBLL.Insert(ObjAllProductsModel).ToString(), Request["Callback"], this.Page);

        }



        /// <summary>
        /// 递归树形
        /// </summary>
        /// <param name="ParentID"></param>
        private void BinderByParent(int ParentID, List<FD_QuotedCatgory> ObjDataSource, TreeNode ObjParentNode)
        {
            var DepartmenndList = ObjDataSource.Where(C => C.Parent == ParentID).ToList();
            if (ParentID == 0)
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode = new TreeNode(ObjItem.Title, ObjItem.QCKey.ToString());
                    this.treeCatogryList.Nodes.Add(ObjTerrNode);
                    BinderByParent(ObjItem.QCKey, ObjDataSource, ObjTerrNode);
                }

            }
            else
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode = new TreeNode(ObjItem.Title, ObjItem.QCKey.ToString());
                    if (ObjParentNode == null)
                    {
                        //ObjParentNode = new TreeNode();

                        foreach (var ObjDepartmennItem in ObjDataSource)
                        {
                            ObjTerrNode = new TreeNode(ObjDepartmennItem.Title, ObjDepartmennItem.QCKey.ToString());
                            this.treeCatogryList.Nodes.Add(ObjTerrNode);
                        }
                        return;

                    }
                    else
                    {
                        ObjParentNode.ChildNodes.Add(ObjTerrNode);
                    }
                    BinderByParent(ObjItem.QCKey, ObjDataSource, ObjTerrNode);
                }

            }

        }





        /// <summary>
        /// 类别项目选择变更的时候 绑定类别下的产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void treeCatogryList_SelectedNodeChanged(object sender, EventArgs e)
        {
            //绑定库房产品
            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByVType(9, treeCatogryList.SelectedNode.Value.ToInt32());
            this.repProductByCatogryforWarehouseList.DataBind();
            BinderCheckData();
        }


        /// <summary>
        /// 保存选择 并且关闭选择页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            string KeyValue = string.Empty;
            KeyValue = hideSelectProduct.Value.Trim(',');
            //所有供应商

            //判断是否选择了产品
            if (!string.IsNullOrWhiteSpace(KeyValue))
            {
                JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyValue, Request["Callback"], this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
            }


        }

        /// <summary>
        /// 绑定需要选中的数据
        /// </summary>
        private void BinderCheckData()
        {
            var KeyList = hideSelectProduct.Value.Trim(',').Split(',');

            for (int i = 0; i < repProductByCatogryforWarehouseList.Items.Count; i++)
            {
                var Control = repProductByCatogryforWarehouseList.Items[i].FindControl("chkProduct") as HtmlInputCheckBox;
                for (int z = 0; z < KeyList.Length; z++)
                {
                    if (Control.Value == KeyList[z])
                    {
                        Control.Checked = true;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMy_Click(object sender, EventArgs e)
        {
            //btnMy.CssClass = "btn btn-warning";
            //btnSupp.CssClass = "btn";
        }


        protected void btnSupp_Click(object sender, EventArgs e)
        {
            //btnMy.CssClass = "btn";
            //btnSupp.CssClass = "btn  btn-warning";


        }




        /// <summary>
        /// 获取 repeater 中所有指定名称的 checkbox 选中的值的字符串（以逗号分隔）。
        /// </summary>
        /// <param name="repeater">Repeater 对象。</param>
        /// <param name="checkBoxName">checkbox 名称。</param>
        /// <returns></returns>
        protected string GetRepeaterCheckedValue(Repeater repeater, string checkBoxName)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            for (int i = 0; i < repeater.Items.Count; i++)
            {
                HtmlInputCheckBox ObjchecjBox = (HtmlInputCheckBox)repeater.Items[i].FindControl(checkBoxName);
                if (ObjchecjBox.Checked)
                {
                    HtmlInputCheckBox ObjCheckBox = (HtmlInputCheckBox)repeater.Items[i].FindControl(checkBoxName);
                    result.Append(ObjCheckBox.Value).Append(',');
                }
            }
            return result.ToString().Trim(',');
        }


        protected int GetAvailableCount(object productid, object customerid, string timespan)
        {
            try
            {
                return timespan != null ?
                    StorehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(customerid), timespan) :
                    StorehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(customerid));
            }
            catch
            {
                return 0;
            }
        }

        protected bool IsDisposible(object productid)
        {
            return StorehouseSourceProductBLL.IsDisposible(Convert.ToInt32(productid));
        }


        protected string GetPutStoreDate(object productid)
        {
            DateTime? putStoreDate = StorehouseSourceProductBLL.GetPutStoreDate(Convert.ToInt32(productid));
            return putStoreDate.HasValue ? putStoreDate.Value.ToString("yyyy-MM-dd") : "未知";
        }


        ///// <summary>
        ///// 绑定虚拟产品
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void TreeSaleItem_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    QuotedProduct ObjQuotedProduct = new QuotedProduct();
        //    var Keys = ObjQuotedProduct.GetByQcKey(TreeSaleItem.SelectedNode.Value.ToInt32()).Keys;

        //    if (Keys != string.Empty)
        //    {
        //        hideSelectProduct.Value = "," + Keys;
        //    }

        //    var KeyList = hideSelectProduct.Value.Trim(',').Split(',');
        //    List<FD_AllProducts> ObjBinderList = new List<FD_AllProducts>();
        //    for (int z = 0; z < KeyList.Length; z++)
        //    {
        //        ObjBinderList.Add(ObjProductBLL.GetByID(KeyList[z].ToInt32()));
        //    }


        //    this.repSaleProduct.DataSource = ObjBinderList;
        //    this.repSaleProduct.DataBind();
        //}
    }
}