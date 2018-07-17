using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class DepartmentEmployeeSelector : System.Web.UI.UserControl
    {
        private int itemLeval = 3;

        public int ItemLeval
        {
            get
            {
                return itemLeval;
            }
            set
            {
                itemLeval = value;
            }
        }

        public string DepartmentTitle
        {
            set
            {
                lblDepartmentTitle.Text = value;
            }
        }

        public string EmployeeTitle
        {
            set
            {
                lblEmployeeTitle.Text = value;
            }
        }

        public IEnumerable<int> SelectedEmployees
        {
            get
            {
                int DepartmentID = ddlDepartment.SelectedValue.ToInt32();
                List<int> Employees = new List<int>();
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    Employees.Add(ddlEmployee.SelectedValue.ToInt32());
                    Employees.AddRange(new Employee().GetMyManagerEmpLoyee(ddlEmployee.SelectedValue.ToInt32()).Select(C => C.EmployeeID));
                }
                else
                {
                    IEnumerable<int> departmentIDs = new Department().GetbyChildenByDepartmetnID(ddlEmployee.SelectedValue.ToInt32()).Select(C => C.DepartmentID);
                    Employees.AddRange(new Employee().Where(C => departmentIDs.Contains(C.DepartmentID)).Select(C => C.EmployeeID).AsEnumerable());
                }
                return Employees;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlDepartment.DataSource = new Department().Where(C => C.DepartmentID != 1 && C.ItemLevel.Value <= ItemLeval).ToList();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            int HAEmployeeID = (HttpContext.Current.Request.Cookies["HAEmployeeID"].Value + string.Empty).ToInt32();
            if (!new Employee().IsManager(HAEmployeeID))
            {
                lblEmployeeTitle.Visible = false;
                ddlEmployee.Visible = false;
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEmployee.DataSource = new Employee().GetByDepartmetnID(ddlDepartment.SelectedValue.ToInt32());
            ddlDepartment.DataTextField = "EmployeeName";
            ddlDepartment.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "");
            ddlEmployee.ClearSelection();
            ddlEmployee.Items.FindByValue("0").Selected = true;
        }
    }
}