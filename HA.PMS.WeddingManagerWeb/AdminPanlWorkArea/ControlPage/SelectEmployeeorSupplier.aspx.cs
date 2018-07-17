using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class SelectEmployeeorSupplier : System.Web.UI.Page
    {
        Supplier ObjSupplierBLL = new Supplier();


        /// <summary>
        /// 部门操作
        /// </summary>
        Department ObjDepartmentBLL = new Department();


        /// <summary>
        /// 人员操作
        /// </summary>
        Employee ObjEmpLoyeeBLL = new Employee();

        Dispatching ObjDispatchingBLL = new Dispatching();

        //DispatchingID
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                BindDepartment();
                BinderSupplier();
            }


        }

        #region 绑定供应商


        private void BinderSupplier()
        {
            //供应商类型
            SupplierType ObjSupplierTypeBLL = new SupplierType();
            this.lstTyperBox.DataSource = ObjSupplierTypeBLL.GetByAll();
            lstTyperBox.DataTextField = "TypeName";
            lstTyperBox.DataValueField = "SupplierTypeId";

            this.lstTyperBox.DataBind();
        }

        /// <summary>
        /// 绑定供应商
        /// </summary>
        private void BinderData(int typerID)
        {
            var DataSource = ObjSupplierBLL.GetByAll();
            DataSource.Add(new DataAssmblly.FD_Supplier() { Name = "库房" });
            this.repSupplier.DataSource = DataSource.Where(C => C.CategoryID == int.Parse(lstTyperBox.SelectedValue));
            this.repSupplier.DataBind();
        }

        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {

        }

        protected void lstTyperBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData(int.Parse(this.lstTyperBox.SelectedValue));
        }

        #endregion


        #region 绑定员工


        /// <summary>
        /// 绑定部门数据
        /// </summary>
        private void BindDepartment()
        {
            var DepartmentID = ObjEmpLoyeeBLL.GetByID(Request.Cookies["HAEmployeeID"].Value.ToInt32()).DepartmentID;
            if (Request["ALL"] == null)
            {

                if (DepartmentID != 0)
                {
                    if (Request["ClassType"] != null)
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
                    else
                    {
                        var DataSource = ObjDepartmentBLL.GetbyChildenByDepartmetnID(DepartmentID);
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
                        foreach (var ObjDepartmennItem in ObjDataSource)
                        {
                            ObjTerrNode = new TreeNode(ObjDepartmennItem.DepartmentName, ObjDepartmennItem.DepartmentID.ToString());
                            ObjTerrNode.Expanded = false;
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



        /// <summary>
        /// 根据部门ID绑定用户
        /// </summary>
        /// <param name="DepartmetID"></param>
        private void BinderEmpLoyssByDepartment(int? DepartmetID)
        {
            this.repEmpLoyeeList.DataSource = ObjEmpLoyeeBLL.GetByDepartmetnID(DepartmetID);
            this.repEmpLoyeeList.DataBind();
        }


        /// <summary>
        /// Node重新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void treeDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            BinderEmpLoyssByDepartment(this.treeDepartment.SelectedNode.Value.ToInt32());
        }
        #endregion



        /// <summary>
        /// 保存选择的供应商或者人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetSelectValue_Click(object sender, EventArgs e)
        {
            var UpdateModel=ObjDispatchingBLL.GetByID(Request[""].ToInt32());
            
        }

    }
}