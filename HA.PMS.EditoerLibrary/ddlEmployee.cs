using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.EditoerLibrary
{
    public class ddlEmployee : DropDownList
    {

        public void BinderByDepartment(int? DepartmetnID)
        {
            this.Width = 75;
            Employee ObjEmpLoyeeBLL = new Employee();
            this.DataSource = ObjEmpLoyeeBLL.GetByDepartmetnID(DepartmetnID);
            this.DataTextField = "EmployeeName";
            this.DataValueField = "EmployeeID";
            this.DataBind();
            this.Items.Add(new ListItem("全部","-1"));
            this.ClearSelection();
            this.Items.FindByText("全部").Selected = true;

        }


        public  ddlEmployee()
        {
            this.Width = 75;
            Employee ObjEmpLoyeeBLL = new Employee();
            this.DataSource = ObjEmpLoyeeBLL.GetByAll();
            this.DataTextField = "EmployeeName";
            this.DataValueField = "EmployeeID";
            this.DataBind();
            this.Items.Add(new ListItem("全部", "-1"));
            this.ClearSelection();
            this.Items.FindByText("全部").Selected = true;

        }

 
    }
}
