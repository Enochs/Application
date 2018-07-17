using System;
using System.Collections.Generic;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesEmployeeManager : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData(sender, e);
            }
        }

        protected void BindData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": CtrPageIndex.CurrentPageIndex = 1; break;
                }
            }

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.FD.SaleSources().Where(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, C => C.ProlongationEmployee > 1, (C, D) => C.ProlongationEmployee.Equals(D.ProlongationEmployee)));
            CtrPageIndex.RecordCount = SourceCount;
        }

        protected Sys_Employee GetEmployee(object employeeID)
        {
            return new HA.PMS.BLLAssmblly.Sys.Employee().GetByID((employeeID + string.Empty).ToInt32());
        }

        protected string GetJobName(object jobID)
        {
            Sys_EmployeeJob sys_EmployeeJob=new HA.PMS.BLLAssmblly.Sys.EmployeeJobI().GetByID((jobID + string.Empty).ToInt32());
            return object.ReferenceEquals(sys_EmployeeJob, null) ? string.Empty : sys_EmployeeJob.Jobname;
        }

        protected string GetDepartmentName(object departmentID)
        {
            Sys_Department sys_Department=new HA.PMS.BLLAssmblly.Sys.Department().GetByID((departmentID + string.Empty).ToInt32());
            return object.ReferenceEquals(sys_Department, null) ? string.Empty : sys_Department.DepartmentName;
        }

        protected void repCustomer_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int key = (e.CommandArgument + string.Empty).ToInt32();
            int empLoyeeID = e.GetTextValue("hiddeEmpLoyeeID").To<Int32>(User.Identity.Name.To<Int32>(1));
            
            switch (e.CommandName)
            {
                case "Save":
                    new BLLAssmblly.FD.SaleSources().ReplaceOwner(key, empLoyeeID);
                    JavaScriptTools.AlertWindow("批量转移成功", Page);
                    break;
                default: break;
            }
            BindData(source, e);
        }
    }
}