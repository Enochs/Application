using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web;
using HA.PMS.BLLAssmblly.Sys;


namespace HA.PMS.EditoerLibrary
{
    public class ddlMyManagerEmployee : DropDownList
    {
        public ddlMyManagerEmployee()
        {
            var LoginEmpLoyeeID = int.Parse(HttpContext.Current.Request.Cookies["HAEmployeeID"].Value);

            Employee ObjEmployeeBLL = new Employee();
            this.DataSource = ObjEmployeeBLL.GetMyManagerEmpLoyee(LoginEmpLoyeeID);

            this.DataTextField = "EmpLoyeeName";
            this.DataValueField = "EmployeeID";
            this.DataBind();

            var Imodel = ObjEmployeeBLL.GetByID(LoginEmpLoyeeID);
            this.Items.Add(new ListItem(Imodel.EmployeeName, Imodel.EmployeeID.ToString()));

            var Objitem = new ListItem("请选择", "0");
            Objitem.Selected = true;
            this.Items.Add(Objitem);
        }
    }
}
