/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.16
 Description:提供改派人员选择页面
 History:修改日志
 （客户电话营销）
 Author:杨洋
 date:2013.3.16
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.Telemarketing
{
    public partial class FL_TelemarketingOper :SystemPage
    { /// <summary>
        /// 部门操作
        /// </summary>
        Department ObjDepartmentBLL = new Department();

        Customers ObjCustomerBLL = new Customers();
        HA.PMS.BLLAssmblly.Flow.Telemarketing ObjTelemarketingsBLL = new BLLAssmblly.Flow.Telemarketing();
        Employee objEmployeeBLL = new Employee();
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

        public string ParentControl
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
            Employee ObjEmpLoyeeBLL = new Employee();
            //var DepartmentID = ObjEmpLoyeeBLL.GetByID(Request.Cookies["HAEmployeeID"].Value.ToInt32()).DepartmentID;
            var DataSource = ObjDepartmentBLL.GetByAll();
            if (DataSource.Count > 0)
            {
                BinderByParent(DataSource[0].Parent.Value, DataSource, null);
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
            this.repEmpLoyeeList.DataSource = ObjEmployeeBLL.GetByDepartmetnID(DepartmetID);
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            btnSure_Click(sender,e);
        }


        protected void btnSure_Click(object sender, EventArgs e)
        {
            int maxSortOrder = ObjTelemarketingsBLL.GetMaxSortOrder();
            int MarkId = Request.QueryString["MarkId"].ToInt32();
            int IsAssign = Request.QueryString["IsAssign"].ToInt32();
            FL_Telemarketing fL_Telemarketing = ObjTelemarketingsBLL.GetByID(MarkId);
            int EmpLoyeeID = hiddEmployeeKey.Value.ToInt32();
            
            fL_Telemarketing.CreateDate = DateTime.Now;
            fL_Telemarketing.SortOrder = maxSortOrder + 1;
            if (IsAssign == 0)
            {
                //分派
                //当前登录操作的员工ID
                fL_Telemarketing.EmployeeID = EmpLoyeeID;
            }
            else
            {
                //改派
                fL_Telemarketing.EmployeeID = EmpLoyeeID;
            }

            if (new Employee().GetByID(EmpLoyeeID) == null)
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败，没有该员工", this.Page);
                return;
            }

            int result = ObjTelemarketingsBLL.Update(fL_Telemarketing);

            if (result > 0)
            {
                JavaScriptTools.ResponseScript("parent.window.location.href = parent.window.location.href;parent.$.fancybox.close(1)", Page);
                UpdateCustomersState(fL_Telemarketing.CustomerID);

                /// <summary>
                /// 邀约操作
                /// </summary>
                HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();
                var ObjUpdateModel = ObjInviteBLL.GetByCustomerID(fL_Telemarketing.CustomerID);
                if (ObjUpdateModel != null)
                {
                    ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                    ObjInviteBLL.Update(ObjUpdateModel);

                    Report ObjReportBLL = new Report();
                    SS_Report ObjReportModel = new SS_Report();
                    ObjReportModel = ObjReportBLL.GetByCustomerID(ObjUpdateModel.CustomerID, ObjUpdateModel.EmpLoyeeID.Value);
                    ObjReportModel.InviteEmployee = ObjUpdateModel.EmpLoyeeID.Value;
                    
                    
                    ObjReportBLL.Update(ObjReportModel);
                }
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }

        /// <summary>
        /// 修改客户信息为未邀约
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        private void UpdateCustomersState(int? CustomerID)
        {

            //修改客户基础信息表记录

            /// <summary>
            /// 邀约操作
            /// </summary>
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();


            var ObjInviteModel = ObjInviteBLL.GetByCustomerID(CustomerID.Value);
            if (ObjInviteModel == null)
            {
                var ObjCustomersModel = ObjCustomerBLL.GetByID(CustomerID);
                ObjCustomersModel.State = (int)CustomerStates.DidNotInvite;
                ObjCustomerBLL.Update(ObjCustomersModel);
            }

        }

    }
}