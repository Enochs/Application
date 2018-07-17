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
    public partial class SelectStoreProduct : System.Web.UI.UserControl
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
                BinderByParent(0, ObjCategoryBLL.GetByAll(), null);



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



            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByType(2, treeCatogryList.Nodes[0].Value.ToInt32());
            this.repProductByCatogryforWarehouseList.DataBind();


        }






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
            //绑定库房产品
            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByType(2, treeCatogryList.SelectedNode.Value.ToInt32());
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

    }
}