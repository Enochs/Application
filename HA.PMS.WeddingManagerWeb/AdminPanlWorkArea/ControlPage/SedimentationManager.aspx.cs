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
using System.IO;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class SedimentationManager : SystemPage
    {

        FileCategory objFileCategory = new FileCategory();
        FileDetails objFileDetailsBLL = new FileDetails();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadTreeView();
                string FileCategoryId = Request.QueryString["FileCategoryId"];
                if (FileCategoryId != null)
                {

                    // ltlContent.Text = objCompanySystemBLL.GetByID(SystemId.ToInt32()).SystemContent;
                }
            }

        }
        protected void LoadTreeView()
        {
            List<Sys_FileCategory> files = objFileCategory.GetByAll();

            BindTreeView(TreeView1, true, false, new Sys_FileCategory { FileCategoryName = "沉淀库", FileCategoryId = 0 }, files);
          //  TreeView1.SelectedNodeStyle.CssClass = "btn btn-info btn-mini";
            TreeView1.ExpandAll();
        }
        public void BindTreeView(TreeView tview, bool isExpanded, bool isLink, Sys_FileCategory root, List<Sys_FileCategory> childs)
        {
            tview.Nodes.Clear();
            TreeNode rootNode = new TreeNode(root.FileCategoryName, root.FileCategoryId + string.Empty);
            rootNode.Expanded = isExpanded;
            if (isLink)
            {
                //  rootNode.NavigateUrl = "FD_CelebrationKnowledgeManager.aspx?knowId=" + root.KnowledgeID;

            }
            tview.Nodes.Add(rootNode);
            this.CreateChildNodes(rootNode, childs, isExpanded, isLink);



        }

        public void CreateChildNodes(TreeNode parentNode, List<Sys_FileCategory> trees, bool isExpanded, bool isLink)
        {
            var _mytrees = trees.Where(o => o.ParentID.ToString() == parentNode.Value);
            foreach (Sys_FileCategory t in _mytrees)
            {
                ///创建新节点
                TreeNode node = new TreeNode();

                ///设置节点的属性
                node.Text = t.FileCategoryName;
                node.Value = t.FileCategoryId + string.Empty;
                node.Expanded = isExpanded;


                parentNode.ChildNodes.Add(node);
                ///递归调用，创建其他节点
                CreateChildNodes(node, trees, isExpanded, isLink);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            ViewState["parentId"] = TreeView1.SelectedNode.Value;
            ltlCategory.Text = TreeView1.SelectedNode.Text + "的文件列表";
            DataBinder();
            //
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCategory_Click(object sender, EventArgs e)
        {
            if (ViewState["parentId"] != null)
            {


                int parentId = ViewState["parentId"].ToString().ToInt32();

                Sys_FileCategory files = new Sys_FileCategory();
                files.IsDelete = false;
                if (!string.IsNullOrEmpty(txtCategoryName.Text))
                {


                    files.FileCategoryName = txtCategoryName.Text;
                    files.ParentID = parentId;
                    int newCategoryId = objFileCategory.Insert(files);
                    JavaScriptTools.RegisterJsCodeSource("alert('添加成功');window.location.href=window.location.href;", this.Page);
                   // JavaScriptTools.AlertWindow("添加成功", this.Page);
                   
                    Response.Redirect(Request.Url.ToString()); 
                    
                    TreeView1.SelectedNode.ChildNodes.Add(new TreeNode(txtCategoryName.Text, newCategoryId + string.Empty));
                }
                else
                {
                    JavaScriptTools.AlertWindow("请你填写分类名", this.Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("请你选择要操作的节点", this.Page);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["parentId"] != null)
            {
                if (!string.IsNullOrEmpty(txtCategoryName.Text))
                {
                    
                    int parentId = ViewState["parentId"].ToString().ToInt32();
                    Sys_FileCategory files = objFileCategory.GetByID(parentId);
                    files.FileCategoryName = txtCategoryName.Text.Trim();
                    objFileCategory.Update(files);

                    JavaScriptTools.RegisterJsCodeSource("alert('修改成功');window.location.href=window.location.href;", this.Page);
                    
                    TreeView1.SelectedNode.Text = txtCategoryName.Text.Trim();
                }
                else
                {
                    JavaScriptTools.AlertWindow("请你填写分类名", this.Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("请你选择要操作的节点", this.Page);
            }
        }

        protected void lkbtnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["parentId"] != null)
            {
                int parentId = ViewState["parentId"].ToString().ToInt32();
                objFileCategory.Delete(new Sys_FileCategory() { FileCategoryId = parentId });
                JavaScriptTools.AlertWindow("删除成功", this.Page);
                txtCategoryName.Text = "";
                LoadTreeView();
            }
            else
            {
                JavaScriptTools.AlertWindow("请你选择要操作的节点", this.Page);
            }
        }


        protected void DataBinder()
        {
            int categoryId = TreeView1.SelectedNode.Value.ToInt32();
            int startIndex = DetailsPager.StartRecordIndex;
            //如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。

            int resourceCount = 0;

            var query = objFileDetailsBLL.GetByIndex(categoryId, DetailsPager.PageSize, DetailsPager.CurrentPageIndex, out resourceCount); ;

            DetailsPager.RecordCount = resourceCount;
            //
            rptFileDetails.DataSource = query;
            rptFileDetails.DataBind();

        }

        protected void DetailsPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptFileDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int FileDetailsId = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName == "Delete")
            {


                objFileDetailsBLL.Delete(new Sys_FileDetails() { FileDetailsId = FileDetailsId });
                JavaScriptTools.AlertWindow("删除成功", this.Page);
                DataBinder();
            }
            if (e.CommandName=="DownLoad")
            {
                string localPath = GetServerPath(objFileDetailsBLL.GetByID(FileDetailsId).FileDetailsPath);
                DownLoad(localPath);
            }
        }
        protected string GetServerPath(object source) 
        {
            //获取程序根目录

            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            string imagesurl = (source + string.Empty).Replace(tmpRootDir, "");
            return imagesurl = "/" + imagesurl.Replace(@"\", @"/");
        
        }
        /// <summary>
        /// 单个文件进行下载
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        public void DownLoad(string path)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(path);
                //防止中文出现乱码
                string filename = HttpUtility.UrlEncode(fileInfo.Name);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.WriteFile(path);
                Response.End(); 
            }
            catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败，该文件不存在,很可能已被重命名或移除！", Page); }
        }

        protected void rptFileDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Sys_FileDetails singerDetails = e.Item.DataItem as Sys_FileDetails;
            PlaceHolder phImg = e.Item.FindControl("phImg") as PlaceHolder;
            PlaceHolder phPPT = e.Item.FindControl("phPPT") as PlaceHolder;
            PlaceHolder phText = e.Item.FindControl("phText") as PlaceHolder;
            PlaceHolder phMovie = e.Item.FindControl("phMovie") as PlaceHolder;
            
            //后缀
            string extension = Path.GetExtension(singerDetails.FileDetailsPath).ToLower();
            switch (extension)
            {
                case ".ppt":
                case ".pptx":
                    phImg.Visible = false;
                    phText.Visible = false;
                    phMovie.Visible = false;
                    break;
                case ".jpg":
                case ".bmp":
                case ".jpeg":
                case ".png":
                case ".gif": 
                    phPPT.Visible = false;
                    phText.Visible = false;
                    phMovie.Visible = false;
                    break;
                case".txt":
                    phPPT.Visible = false;
                    phImg.Visible = false;
                    phMovie.Visible = false;
                    break;
                case ".mp4":
                case ".rmvb":
                case ".avi":
                case ".wmv":
                case ".3gp":
                case ".f4v":
                case ".rm":
                    phPPT.Visible = false;
                    phImg.Visible = false;
                    phText.Visible = false;
                    break;
                default:
                    break;
            }

        }

    }
}