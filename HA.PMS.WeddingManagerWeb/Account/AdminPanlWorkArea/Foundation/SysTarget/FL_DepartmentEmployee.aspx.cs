using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget
{
    public partial class FL_DepartmentEmployee : System.Web.UI.Page
    {
        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rptEmployes.DataSource = ObjEmployeeBLL.GetByDepartmetnID(int.Parse(Request["DepartmentID"]));
                this.rptEmployes.DataBind();
            }
        }
    }
}