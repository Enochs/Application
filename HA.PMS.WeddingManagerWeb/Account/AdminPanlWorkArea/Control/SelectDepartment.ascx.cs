using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using System.Web.UI.HtmlControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectDepartment : System.Web.UI.UserControl
    {

        /// <summary>
        /// 部门操作
        /// </summary>
        Department ObjDepartmentBLL = new Department();


        /// <summary>
        /// 人员操作
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();


        /// <summary>
        /// ControlKey
        /// </summary>
        public string ControlKey
        {
            get;
            set;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.hideControlKey.Value = Request["ControlKey"];

            this.hiddDeparttxtKey.Value = Request["TxtControl"];
            if (!IsPostBack)
            {
                BindDepartment();
            }
        }

        /// <summary>
        /// 绑定部门数据
        /// </summary>
        private void BindDepartment()
        {
            var DepartmentID = ObjEmployeeBLL.GetByID(Request.Cookies["HAEmployeeID"].Value.ToInt32()).DepartmentID;
            if (Request["ALL"] == null)
            {

                if (DepartmentID != 0)
                {
                    var DataSource = ObjDepartmentBLL.GetByDataSourceDepartment(Request["ClassType"]);
                    if (DataSource.Count > 0)
                    {
                        BinderByParent(DataSource[0].Parent.Value, DataSource, null);
                    }
                    else
                    {
                        DataSource = ObjDepartmentBLL.GetbyChildenByDepartmetnID(DepartmentID);
                        if (DataSource.Count > 0)
                        {
                            BinderByParent(DataSource[0].Parent.Value, DataSource, null);
                        }
                    }
                }
            }
            else
            {

                var DataSource = ObjDepartmentBLL.GetByAll();
                BinderByParent(0, DataSource, null);

            }
        }


        /// <summary>
        /// 递归树形
        /// </summary>
        /// <param name="ParentID"></param>
        private void BinderByParent(int ParentID, List<Sys_Department> ObjDataSource, TreeNode ObjParentNode)
        {
            var DepartmenndList = ObjDataSource.Where(C => C.Parent == ParentID).ToList();
            if (ParentID == 0)
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode = new TreeNode(ObjItem.DepartmentName, ObjItem.DepartmentID.ToString());
                    this.treeDepartment.Nodes.Add(ObjTerrNode);
                    BinderByParent(ObjItem.DepartmentID, ObjDataSource, ObjTerrNode);
                }

            }
            else
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode = new TreeNode(ObjItem.DepartmentName, ObjItem.DepartmentID.ToString());
                    if (ObjParentNode == null)
                    {
                        //ObjParentNode = new TreeNode();

                        foreach (var ObjDepartmennItem in ObjDataSource)
                        {
                            ObjTerrNode =  new TreeNode(ObjDepartmennItem.DepartmentName, ObjDepartmennItem.DepartmentID.ToString());
                            this.treeDepartment.Nodes.Add(ObjTerrNode);
                        }
                        return;
                       
                    }
                    else
                    {
                        ObjParentNode.ChildNodes.Add(ObjTerrNode);
                    }
                    BinderByParent(ObjItem.DepartmentID, ObjDataSource, ObjTerrNode);
                }

            }

        }

        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 选择部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void treeDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDepartmentName.Text = this.treeDepartment.SelectedNode.Text;
            hideDepartmentKey.Value = this.treeDepartment.SelectedNode.Value;
        }
    }
}