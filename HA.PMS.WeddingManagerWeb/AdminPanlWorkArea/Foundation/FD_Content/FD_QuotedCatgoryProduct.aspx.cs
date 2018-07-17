using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System.Web.UI.HtmlControls;
/*
 标准产品维护
 */
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgoryProduct : SystemPage
    {
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();


        QuotedProduct ObjQuotedProduct = new QuotedProduct();
        /// <summary>
        /// 产品
        /// </summary>
        AllProducts ObjProductBLL = new AllProducts();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }


        /// <summary>
        /// 绑定页面数据
        /// </summary>
        private void BinderData()
        {
            BinderByParent(0, ObjQuotedCatgoryBLL.GetByAll(), null);
            this.repProductByCatogryforWarehouseList.DataSource = ObjProductBLL.GetByVType(9, treeCatogryList.Nodes[0].Value.ToInt32());
            this.repProductByCatogryforWarehouseList.DataBind();

            var Keys = ObjQuotedProduct.GetByQcKey(Request["Qckey"].ToInt32()).Keys;
            if (Keys != string.Empty)
            {
                hideSelectProduct.Value = "," + Keys;
            }

            var KeyList = hideSelectProduct.Value.Trim(',').Split(',');
            for (int z = 0; z < KeyList.Length; z++)
            {
                if (KeyList[z].ToInt32() > 0)
                {
                    try
                    {
                        txtProductList.Text += ObjProductBLL.GetByID(KeyList[z].ToInt32()).ProductName + "\r\n";
                    }
                    catch
                    { 
                    
                    
                    }
                }
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
        /// 保存修改结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {

            var ObjModel = ObjQuotedProduct.GetByQcKey(Request["Qckey"].ToInt32());
            ObjModel.Keys = hideSelectProduct.Value.Trim(',');
            ObjQuotedProduct.Update(ObjModel);
            JavaScriptTools.AlertAndClosefancybox("保存完毕！", Page);
        }


        /// <summary>
        /// 绑定选择项目的产品
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
    }
}