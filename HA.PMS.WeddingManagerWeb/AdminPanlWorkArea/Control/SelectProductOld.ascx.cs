using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using System.Web.UI.HtmlControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectProductOld : System.Web.UI.UserControl
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
        Category ObjQuotedCatgoryBLL = new Category();

        /// <summary>
        /// 供应商
        /// </summary>
        Supplier ObjSupplierBLL = new Supplier();


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
                //JavaScriptTools.AlertWindow(Request["CustomerID"], Page);
                BinderByParent(0, ObjQuotedCatgoryBLL.GetByAll(), null);
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
            //this.repProductList.DataSource = ObjProductBLL.GetbyCategoryID(Request["CategoryID"].ToInt32());
            //this.repProductList.DataBind();

            //this.repProductList.DataSource = ObjProductBLL.GetByType(1, Request["CategoryID"].ToInt32());
            //this.repProductList.DataBind();


            //this.repWareList.DataSource = ObjProductBLL.GetByType(2, Request["CategoryID"].ToInt32());
            //this.repWareList.DataBind();

            this.ddlSupply.DataSource = ObjSupplierBLL.GetByAll();
            this.ddlSupply.DataTextField = "Name";
            this.ddlSupply.DataValueField = "SupplierID";
            this.ddlSupply.DataBind();

            this.ddlSupply.Items.Add(new ListItem("库房", "库房"));
            this.ddlSupply.Items.Add(new ListItem("新购买", "新购买"));
            this.ddlSupply.Items[this.ddlSupply.Items.Count - 1].Selected = true;

            ///当有指定供应商的时候就指定某个供应商
            if (Request["SupplierName"] != "库房" && Request["SupplierName"] != "")
            {
                this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.Nodes[0].Value.ToInt32()).Where(C => C.SupplierName == Request["SupplierName"].ToString());
            }
            else
            {
                this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.Nodes[0].Value.ToInt32());
            }
            this.repProductByCatogryList.DataBind();


            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByType(2, treeCatogryList.Nodes[0].Value.ToInt32());
            this.repProductByCatogryforWarehouseList.DataBind();


            this.repSuppProductList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.Nodes[0].Value.ToInt32());

            this.repSuppProductList.DataBind();

        }


        ///// <summary>
        ///// 确认选择并且关闭弹出窗
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSaveSelect_Click(object sender, EventArgs e)
        //{
        //    string KeyValue = string.Empty;
        //    for (int i = 0; i < repProductList.Items.Count; i++)
        //    {
        //        HtmlInputCheckBox ObjchecjBox = (HtmlInputCheckBox)repProductList.Items[i].FindControl("chkProduct");
        //        if (ObjchecjBox.Checked)
        //        {
        //            HtmlInputCheckBox ObjCheckBox = (HtmlInputCheckBox)repProductList.Items[i].FindControl("chkProduct");
        //            KeyValue += ObjCheckBox.Value + ",";
        //        }
        //    }

        //    KeyValue = KeyValue.Trim(',');
        //    if (KeyValue != string.Empty)
        //    {
        //        JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyValue, Request["Callback"], this.Page);
        //    }
        //    else
        //    {
        //        JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
        //    }
        //}


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
            ObjAllProductsModel.PurchasePrice = txtPurchasePrice.Text.ToDecimal();
            ObjAllProductsModel.Remark = txtRemark.Text;
            ObjAllProductsModel.SupplierName = ddlSupply.SelectedItem.Text;
            ObjAllProductsModel.IsTobeDistributed = false;
            ObjAllProductsModel.Productproperty = rdotyper.SelectedValue.ToInt32();
            JavaScriptTools.SetValueByParentControl(Request["ControlKey"], ObjProductBLL.Insert(ObjAllProductsModel).ToString(), Request["Callback"], this.Page);

        }


        ///// <summary>
        ///// 选择库房
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSelectWare_Click(object sender, EventArgs e)
        //{
        //    string KeyValue = string.Empty;
        //    for (int i = 0; i < repWareList.Items.Count; i++)
        //    {
        //        HtmlInputCheckBox ObjchecjBox = (HtmlInputCheckBox)repWareList.Items[i].FindControl("chkProduct");
        //        if (ObjchecjBox.Checked)
        //        {
        //            HtmlInputCheckBox ObjCheckBox = (HtmlInputCheckBox)repWareList.Items[i].FindControl("chkProduct");
        //            KeyValue += ObjCheckBox.Value + ",";
        //        }
        //    }

        //    KeyValue = KeyValue.Trim(',');
        //    if (KeyValue != string.Empty)
        //    {
        //        JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyValue, Request["Callback"], this.Page);
        //    }
        //    else
        //    {
        //        JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
        //    }
        //}


        /// <summary>
        /// 递归树形
        /// </summary>
        /// <param name="ParentID"></param>
        private void BinderByParent(int ParentID, List<FD_Category> ObjDataSource, TreeNode ObjParentNode)
        {
            var DepartmenndList = ObjDataSource.Where(C => C.ParentID == ParentID).ToList();
            if (ParentID == 0)
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode = new TreeNode(ObjItem.CategoryName, ObjItem.CategoryID.ToString());
                    this.treeCatogryList.Nodes.Add(ObjTerrNode);
                    BinderByParent(ObjItem.CategoryID, ObjDataSource, ObjTerrNode);
                }

            }
            else
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode = new TreeNode(ObjItem.CategoryName, ObjItem.CategoryID.ToString());
                    if (ObjParentNode == null)
                    {
                        //ObjParentNode = new TreeNode();

                        foreach (var ObjDepartmennItem in ObjDataSource)
                        {
                            ObjTerrNode = new TreeNode(ObjDepartmennItem.CategoryName, ObjDepartmennItem.CategoryID.ToString());
                            this.treeCatogryList.Nodes.Add(ObjTerrNode);
                        }
                        return;

                    }
                    else
                    {
                        ObjParentNode.ChildNodes.Add(ObjTerrNode);
                    }
                    BinderByParent(ObjItem.CategoryID, ObjDataSource, ObjTerrNode);
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
            //this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.SelectedNode.Value.ToInt32());
            //this.repProductByCatogryList.DataBind();

            ///当有指定供应商的时候就指定某个供应商
            ///当指定了供应商 则绑定指定供应商下的产品
            if (Request["SupplierName"] != "库房" && Request["SupplierName"] != "" && Request["SupplierName"] != null)
            {
                this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.SelectedNode.Value.ToInt32()).Where(C => C.SupplierName == Request["SupplierName"].ToString());
            }
            else
            {
                this.repProductByCatogryList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.SelectedNode.Value.ToInt32());
            }
            this.repProductByCatogryList.DataBind();


            this.repSuppProductList.DataSource = ObjProductBLL.GetByType(1, treeCatogryList.SelectedNode.Value.ToInt32());

            this.repSuppProductList.DataBind();

            //绑定库房产品
            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByType(2, treeCatogryList.SelectedNode.Value.ToInt32());
            this.repProductByCatogryforWarehouseList.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMy_Click(object sender, EventArgs e)
        {
            btnMy.CssClass = "btn btn-warning";
            btnSupp.CssClass = "btn";
        }


        protected void btnSupp_Click(object sender, EventArgs e)
        {
            btnMy.CssClass = "btn";
            btnSupp.CssClass = "btn  btn-warning";


        }


        /// <summary>
        /// 保存选择 并且关闭选择页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            string KeyValue = string.Empty;

            //switch (hideTyper.Value)
            //{
            //    //新购入
            //    case "1": break;
            //    //供应商
            //    case "2": KeyValue = GetRepeaterCheckedValue(repProductByCatogryList, "chkProduct"); break;
            //    //库房
            //    case "3": KeyValue = GetRepeaterCheckedValue(repProductByCatogryforWarehouseList, "chkProduct"); break;
            //    //所有供应商
            //    case "4": KeyValue = GetRepeaterCheckedValue(repSuppProductList, "chkProduct"); break;
            //    default: break;
            //}

            //判断是否选择了产品
            if (!string.IsNullOrWhiteSpace(hideSelectProduct.Value.Trim(',')))
            {
                JavaScriptTools.SetValueByParentControl(Request["ControlKey"], hideSelectProduct.Value.Trim(','), Request["Callback"], this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
            }

            //string KeyValue = string.Empty;
            //if (hideTyper.Value == "4")
            //{

            //    //循环确认
            //    for (int i = 0; i < repSuppProductList.Items.Count; i++)
            //    {
            //        HtmlInputCheckBox ObjchecjBox = (HtmlInputCheckBox)repSuppProductList.Items[i].FindControl("chkProduct");
            //        if (ObjchecjBox.Checked)
            //        {
            //            HtmlInputCheckBox ObjCheckBox = (HtmlInputCheckBox)repSuppProductList.Items[i].FindControl("chkProduct");
            //            KeyValue += ObjCheckBox.Value + ",";
            //        }
            //    }

            //    KeyValue = KeyValue.Trim(',');
            //    //判断是否选择了产品
            //    if (KeyValue != string.Empty)
            //    {
            //        JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyValue, Request["Callback"], this.Page);
            //    }
            //    else
            //    {
            //        JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
            //    }
            //    return;
            //}

            //if (hideTyper.Value == "2")
            //{

            //    //循环确认
            //    for (int i = 0; i < repProductByCatogryList.Items.Count; i++)
            //    {
            //        HtmlInputCheckBox ObjchecjBox = (HtmlInputCheckBox)repProductByCatogryList.Items[i].FindControl("chkProduct");
            //        if (ObjchecjBox.Checked)
            //        {
            //            HtmlInputCheckBox ObjCheckBox = (HtmlInputCheckBox)repProductByCatogryList.Items[i].FindControl("chkProduct");
            //            KeyValue += ObjCheckBox.Value + ",";
            //        }
            //    }

            //    KeyValue = KeyValue.Trim(',');
            //    //判断是否选择了产品
            //    if (KeyValue != string.Empty)
            //    {
            //        JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyValue, Request["Callback"], this.Page);
            //    }
            //    else
            //    {
            //        JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < repProductByCatogryforWarehouseList.Items.Count; i++)
            //    {
            //        HtmlInputCheckBox ObjchecjBox = (HtmlInputCheckBox)repProductByCatogryforWarehouseList.Items[i].FindControl("chkProduct");
            //        if (ObjchecjBox.Checked)
            //        {
            //            HtmlInputCheckBox ObjCheckBox = (HtmlInputCheckBox)repProductByCatogryforWarehouseList.Items[i].FindControl("chkProduct");
            //            KeyValue += ObjCheckBox.Value + ",";
            //        }
            //    }

            //    KeyValue = KeyValue.Trim(',');
            //    if (KeyValue != string.Empty)
            //    {
            //        JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyValue, Request["Callback"], this.Page);
            //    }
            //    else
            //    {
            //        JavaScriptTools.AlertWindow("请至少选择一项！", this.Page);
            //    }

            //}
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
            return timespan != null ?
                StorehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(customerid), timespan) :
                StorehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(customerid));
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
    }
}