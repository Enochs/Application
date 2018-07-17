using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.EditoerLibrary
{
    public class CategoryDropDownList : System.Web.UI.WebControls.DropDownList
    {
        /// <summary>
        /// 类别
        /// </summary>
        Category ObjCategoryBLL = new Category();
        public int ParentID
        { get; set; }

        public int BindByNow
        {
            get;
            set;
        }

        public CategoryDropDownList()
        {

            this.DataSource = ObjCategoryBLL.GetByparent(ParentID).Where(C => !C.CategoryName.Contains("待分配产品"));
            this.DataTextField = "CategoryName";
            this.DataValueField = "CategoryID";
            this.DataBind();

        }

        /// <summary>
        /// 根据父级绑定
        /// </summary>
        public void BinderByparent()
        {
            this.DataSource = ObjCategoryBLL.GetByparent(ParentID).Where(C => C.IsShow == true);
            this.DataTextField = "CategoryName";
            this.DataValueField = "CategoryID";
            this.DataBind();
        }
    }
}
