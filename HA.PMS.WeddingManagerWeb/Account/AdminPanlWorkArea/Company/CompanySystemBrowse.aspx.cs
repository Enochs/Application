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
using HA.PMS.BLLAssmblly.CS;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanySystemBrowse : SystemPage
    {
        CompanySystem objCompanySystemBLL = new CompanySystem();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<CA_CompanySystem> clel = objCompanySystemBLL
                .GetByAll();
                BindTreeView(TreeView1, true, false, new CA_CompanySystem { SystemTitle = "制度管理库", SystemId = 0 }, clel);
                string SystemId = Request.QueryString["SystemId"];
 
            }

        }

        public void BindTreeView(TreeView tview, bool isExpanded, bool isLink, CA_CompanySystem root, List<CA_CompanySystem> childs)
        {
            tview.Nodes.Clear();
            TreeNode rootNode = new TreeNode(root.SystemTitle, root.SystemId + string.Empty);
            rootNode.Expanded = isExpanded;
            if (isLink)
            {
                //  rootNode.NavigateUrl = "FD_CelebrationKnowledgeManager.aspx?knowId=" + root.KnowledgeID;

            }
            tview.Nodes.Add(rootNode);
            this.CreateChildNodes(rootNode, childs, isExpanded, isLink);



        }

        public void CreateChildNodes(TreeNode parentNode, List<CA_CompanySystem> trees, bool isExpanded, bool isLink)
        {
            var _mytrees = trees.Where(o => o.ParentID.ToString() == parentNode.Value);
            foreach (CA_CompanySystem t in _mytrees)
            {
                ///创建新节点
                TreeNode node = new TreeNode();
                ///设置节点的属性
                node.Text = t.SystemTitle;
                node.Value = t.SystemId + string.Empty;
                node.Expanded = isExpanded;

                //if (isLink == true)
                //{
                ///格式化链接地址
                node.NavigateUrl = "CompanySystemBrowse.aspx?SystemId=" + t.SystemId;

                //  }
                parentNode.ChildNodes.Add(node);
                ///递归调用，创建其他节点
                CreateChildNodes(node, trees, isExpanded, isLink);
            }
        }
    }

}