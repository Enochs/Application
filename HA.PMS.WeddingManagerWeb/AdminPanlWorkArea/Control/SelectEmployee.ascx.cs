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
    public partial class SelectEmployee : System.Web.UI.UserControl
    {
        /// <summary>
        /// 部门操作
        /// </summary>
        Department ObjDepartmentBLL = new Department();


        /// <summary>
        /// 人员操作
        /// </summary>
        Employee ObjEmpLoyeeBLL = new Employee();


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
            var DepartmentID = ObjEmpLoyeeBLL.GetByID(Request.Cookies["HAEmployeeID"].Value.ToInt32()).DepartmentID;
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
        private void BinderByParent(int ParentID, List<Sys_Department> ObjDataSource,TreeNode ObjParentNode)
        {
            var DepartmenndList = ObjDataSource.Where(C => C.Parent == ParentID).ToList();
            if (ParentID == 0)
            {
                foreach (var ObjItem in DepartmenndList)
                {
                    TreeNode ObjTerrNode=new TreeNode(ObjItem.DepartmentName, ObjItem.DepartmentID.ToString());
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
                        this.treeDepartment.Nodes.Add(ObjTerrNode);
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


        /// <summary>
        /// 确认选择 同时绑定父页面赋值控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            HtmlInputRadioButton ObjRadio ;
            for (int i = 0; i < repEmpLoyeeList.Items.Count; i++)
            {

                ObjRadio = (HtmlInputRadioButton)repEmpLoyeeList.Items[i].FindControl("rdoEmpLoyee");
                if (ObjRadio.Checked)
                {
                    if (Request["Callback"] != null)
                    {
                        JavaScriptTools.SetValueByParentControl(Request["ControlKey"], ObjRadio.Value, Request["Callback"], this.Page);
                        return;
                    }
                    JavaScriptTools.SetValueByParentControl(Request["ControlKey"], ObjRadio.Value, this.Page);

                    return;
                }
            }
        }

    }
}