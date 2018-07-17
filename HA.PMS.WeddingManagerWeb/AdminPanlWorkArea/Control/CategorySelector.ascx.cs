using HA.PMS.BLLAssmblly;
using HA.PMS.DataAssmblly;
using System;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CategorySelector : System.Web.UI.UserControl
    {
        public string SelectedValue
        {
            get
            {
                return DDLCategory.SelectedValue;
            }
            set
            {
                System.Web.UI.WebControls.ListItem listItem = DDLCategory.Items.FindByValue(value);
                if (listItem != null)
                {
                    DDLCategory.ClearSelection();
                    listItem.Selected = true;
                }
            }
        }

        public string SelectedText
        {
            get
            {
                return DDLCategory.SelectedItem.Text;
            }
            set
            {
                System.Web.UI.WebControls.ListItem listItem = DDLCategory.Items.FindByText(value);
                if (listItem != null)
                {
                    DDLCategory.ClearSelection();
                    listItem.Selected = true;
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                return DDLCategory.SelectedIndex;
            }
            set
            {
                DDLCategory.SelectedIndex = value;
            }
        }

        public bool EnableEmpty { get; set; }

        public string EmptyTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EnableEmpty)
            {
                DDLCategory.Items.Insert(0, EmptyTitle);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            var dataSource = new Repositoy<DataAssmblly.FD_Category>(new PMS_WeddingEntities()).LoadEntities(C => C.ParentID == 0 && !C.CategoryName.Equals("待分配产品")).Select(C => new { CategoryID = C.CategoryID, CategoryName = C.CategoryName }).ToList();
            DDLCategory.DataSource = dataSource;
            DDLCategory.DataTextField = "CategoryName";
            DDLCategory.DataValueField = "CategoryID";
            DDLCategory.DataBind();
            base.OnInit(e);
        }
    }
}