using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class MyManager : System.Web.UI.UserControl
    {
        public int EmployeeID
        {
            get;
            set;
        }
        private string title = "责任人";
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string SelectedValue
        {
            get
            {
                return ddlMyManagerEmployee1.SelectedValue;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Employee ObjEmployeeBLL = new Employee();
            int loginEmployeeId = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            if (!ObjEmployeeBLL.IsManager(loginEmployeeId))
            {
                this.Visible = false;
                if (loginEmployeeId == 175)
                {
                    this.Visible = true;
                }
            }


        }

        public List<ObjectParameter> GetEmployeePar(List<ObjectParameter> ObjKeyParList)
        {

            //是否按照责任人查
            if (EmployeeID != 0)
            {
                ObjKeyParList.Add(new System.Data.Objects.ObjectParameter("EmployeeID", EmployeeID));
            }
            else
            {
                ObjKeyParList.Add(new System.Data.Objects.ObjectParameter("EmpLoyeeID", int.Parse(Request.Cookies["HAEmployeeID"].Value)));
            }
            return ObjKeyParList;
        }


        /// <summary>
        /// 按照责任人查询
        /// </summary>
        /// <param name="ObjKeyParList"></param>
        /// <returns></returns>
        public List<PMSParameters> GetEmployeePar(List<PMSParameters> ObjKeyParList, string Employee = "EmployeeID", bool IsShowAll = true)
        {

            //是否按照责任人查
            if (EmployeeID != 0)
            {
                ObjKeyParList.Add(Employee, EmployeeID);
            }
            else
            {
                ObjKeyParList.Add(Employee, int.Parse(Request.Cookies["HAEmployeeID"].Value), NSqlTypes.IN, IsShowAll);
            }
            return ObjKeyParList;
        }



        protected override void OnLoad(EventArgs e)
        {
            if (this.ddlMyManagerEmployee1.SelectedValue != "0")
            {
                EmployeeID = int.Parse(ddlMyManagerEmployee1.SelectedValue);
            }
            else
            {
                EmployeeID = 0;
            }
            base.OnLoad(e);

        }
    }
}