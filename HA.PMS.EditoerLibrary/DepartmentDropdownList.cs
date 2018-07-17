using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HA.PMS.EditoerLibrary
{
    public class DepartmentDropdownList : System.Web.UI.WebControls.DropDownList
    {
        public DepartmentDropdownList()
        {

            Department objDepartmentBLL = new Department();
            this.DataSource = objDepartmentBLL.GetMyManagerDepartment(int.Parse(HttpContext.Current.Request.Cookies["HAEmployeeID"].Value));
            this.DataTextField = "DepartmentName";
            this.DataValueField = "DepartmentID";
            this.DataBind();

            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }

        public void BinderByQuoted()
        {

            Department objDepartmentBLL = new Department();
            this.DataSource = objDepartmentBLL.GetByDataSourceDepartment("QuotedPriceWorkPanel");


            this.DataTextField = "DepartmentName";
            this.DataValueField = "DepartmentID";
            this.DataBind();

            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;

        }

        public void BinderDepartment()
        {
            Department objDepartmentBLL = new Department();
            this.DataSource = objDepartmentBLL.GetByAll();
            this.DataTextField = "DepartmentName";
            this.DataValueField = "DepartmentID";
            this.DataBind();

            this.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            this.Items.FindByText("请选择").Selected = true;
        }
    }
}
