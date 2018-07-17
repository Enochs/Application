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
using System.Web.UI.HtmlControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectCategoryProduct : System.Web.UI.UserControl
    {
        Category objCategoryBLL = new Category();
        AllProducts objAllProductsBLL = new AllProducts();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataLeftTreeBinder();
            }
        }


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

        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            string rdo = Request["rdo"];

            int Keys = Request.QueryString["Keys"].ToInt32();

            int childParent = rdo.ToInt32();
            FD_Category parentCategory = objCategoryBLL.GetByID(childParent);
            int parentParent = parentCategory.ParentID;
            FD_AllProducts fdEdit = objAllProductsBLL.GetByID(Keys);
            fdEdit.ProductCategory = parentParent;
            fdEdit.ProjectCategory = childParent;
            objAllProductsBLL.Update(fdEdit);
        
            JavaScriptTools.AlertAndClosefancybox("选择成功", this.Page);
            // JavaScriptTools.SetValueByParentControl(Request["ControlKey"], ObjRadio.Value, this.Page);

        }
    }
}