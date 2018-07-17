
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:庆典文库页面
 History:修改日志

 Author:杨洋
 date:2013.3.21
 version:好爱1.0
 description:修改描述
 
 
 
 */
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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge
{
    public partial class FD_CelebrationKnowBrowse :SystemPage
    {
        CelebrationKnowledge objCelebrationKnowledgeManagerBLL = new CelebrationKnowledge();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<HA.PMS.DataAssmblly.FD_CelebrationKnowledge> clel = objCelebrationKnowledgeManagerBLL
                .GetByAll();
                BindTreeView(TreeView1, true, false, new HA.PMS.DataAssmblly.FD_CelebrationKnowledge { KnowledgeTitle = "知识管理库", KnowledgeID = 0 }, clel);
                string knowId = Request.QueryString["knowId"];
                if (knowId != null)
                {
                    ltlContent.Text = objCelebrationKnowledgeManagerBLL.GetByID(knowId.ToInt32()).KnowledgeContent;
                }
            }
             
        }

        public void BindTreeView(TreeView tview, bool isExpanded, bool isLink, HA.PMS.DataAssmblly.FD_CelebrationKnowledge root, List<HA.PMS.DataAssmblly.FD_CelebrationKnowledge> childs)
        {
            tview.Nodes.Clear();
            TreeNode rootNode = new TreeNode(root.KnowledgeTitle, root.KnowledgeID + string.Empty);
            rootNode.Expanded = isExpanded;
            if (isLink)
            {
                //  rootNode.NavigateUrl = "FD_CelebrationKnowledgeManager.aspx?knowId=" + root.KnowledgeID;

            }
            tview.Nodes.Add(rootNode);
            this.CreateChildNodes(rootNode, childs, isExpanded, isLink);



        }

        public void CreateChildNodes(TreeNode parentNode, List<HA.PMS.DataAssmblly.FD_CelebrationKnowledge> trees, bool isExpanded, bool isLink)
        {
            var _mytrees = trees.Where(o => o.ParentID.ToString() == parentNode.Value);
            foreach (HA.PMS.DataAssmblly.FD_CelebrationKnowledge t in _mytrees)
            {
                ///创建新节点
                TreeNode node = new TreeNode();
                ///设置节点的属性
                node.Text = t.KnowledgeTitle;
                node.Value = t.KnowledgeID + string.Empty;
                node.Expanded = isExpanded;

                //if (isLink == true)
                //{
                    ///格式化链接地址
                    node.NavigateUrl = "FD_CelebrationKnowBrowse.aspx?knowId=" + t.KnowledgeID;

              //  }
                parentNode.ChildNodes.Add(node);
                ///递归调用，创建其他节点
                CreateChildNodes(node, trees, isExpanded, isLink);
            }
        }
    }
}