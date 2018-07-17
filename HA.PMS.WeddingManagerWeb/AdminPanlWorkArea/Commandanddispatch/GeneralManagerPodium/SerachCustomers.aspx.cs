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
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium
{
    public partial class SerachCustomers : SystemPage
    {
        Employee objEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void DepartmentDropdownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentId = DepartmentDropdownList1.SelectedValue.ToInt32();
            ddlEmployee.DataSource = objEmployeeBLL.GetByDepartmetnID(departmentId);
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            int employeeId = ddlEmployee.SelectedValue.ToInt32();
            Response.Write("<script>window.open('/AdminPanlWorkArea/AdminMain.aspx?EmployeeID=" + employeeId + "','_blank');</script>");
           // Server.Transfer("~/AdminPanlWorkArea/AdminMain.aspx?EmployeeID=" + employeeId);
        }
    }
}