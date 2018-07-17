using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanyJurisdiction : SystemPage
    {
        Employee objEmployeeBLL = new Employee();
        Department ObjDepartmentBLL = new Department();
        string OrderByColumnName = "EmployeeID";
        int SourceCount = 0;

        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLDataBinder();
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定

        public void DataBinder()
        {
            List<PMSParameters> objparmlist = new List<PMSParameters>();
            //员工名字
            objparmlist.Add(txtUserName.Text.Trim() != string.Empty, "EmployeeName", txtUserName.Text.Trim().ToString(), NSqlTypes.LIKE);
            //部门
            objparmlist.Add(ddlDepartment.SelectedValue.ToInt32() > 0, "DepartmentID", ddlDepartment.SelectedValue.ToInt32(), NSqlTypes.Equal);
            //状态（启用/禁用)
            objparmlist.Add(ddlSates.SelectedValue.ToInt32() > 0, "EmployeeTypeID", ddlSates.SelectedValue.ToInt32(), NSqlTypes.Equal);
            //状态(未删除的用户)
            objparmlist.Add("IsDelete", false, NSqlTypes.Bit);
            objparmlist.Add("EmployeeTypeID", 1, NSqlTypes.NotEquals);
            var DataList = objEmployeeBLL.GetEmployeeByParameter(objparmlist, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            rptUserJurisdiction.DataSource = DataList;
            rptUserJurisdiction.DataBind();
            CtrPageIndex.RecordCount = SourceCount;

            GetEnable();        //禁用/启用
        }
        #endregion

        #region 下拉框绑定
        public void DDLDataBinder()
        {
            Department ObjDepartmentBLL = new Department();
            var DataList = ObjDepartmentBLL.GetByAll();
            ddlDepartment.DataSource = DataList;
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add(new ListItem("请选择", "0"));
            ddlDepartment.Items.FindByValue("0").Selected = true;
        }
        #endregion

        #region 获取部门名称
        public string GetDepartmentName(object Source)
        {
            if (Source != null)
            {
                int DepartmentID = Source.ToString().ToInt32();
                if (DepartmentID != 0)
                {
                    return ObjDepartmentBLL.GetByID(DepartmentID).DepartmentName.ToString();
                }
            }
            return "";
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页绑定
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 绑定事件  启用/禁用
        /// <summary>
        /// 更改状态   是启用 还是禁用(公司文件/公式制度的权限)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptUserJurisdiction_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int EmployeeID = e.CommandArgument.ToString().ToInt32();
            Sys_Employee EmployeeModel = objEmployeeBLL.GetByID(EmployeeID);
            if (e.CommandName == "Allow")
            {
                EmployeeModel.EmployeeTypeID = 2;
                objEmployeeBLL.Update(EmployeeModel);

            }
            else if (e.CommandName == "Deny")
            {
                EmployeeModel.EmployeeTypeID = 3;
                objEmployeeBLL.Update(EmployeeModel);
            }

            DataBinder();
        }
        #endregion

        #region 点击查询按钮
        /// <summary>
        /// 查询
        /// </summary>   
        protected void btnLookFor_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            DataBinder();
        }
        #endregion


        #region 是否禁用/启用 (禁用 /启用)
        public void GetEnable()
        {
            //for (int i = 0; i < rptUserJurisdiction.Items.Count; i++)
            //{
            //    var ObjItem = rptUserJurisdiction.Items[i];
            //    LinkButton Allow = ObjItem.FindControl("lbtnAllow") as LinkButton;
            //    LinkButton Deny = ObjItem.FindControl("lbtnDeny") as LinkButton;
            //    HiddenField HideEmployeeID = ObjItem.FindControl("hideEmployeeID") as HiddenField;
            //    Sys_Employee Models = objEmployeeBLL.GetByID(HideEmployeeID.Value.ToInt32());
            //    Allow.Enabled = false;
            //    Deny.Enabled = true;
            //    //if (Models.EmployeeTypeID <= 2)
            //    //{
            //    //    Allow.Enabled = false;
            //    //    Deny.Enabled = true;
            //    //}
            //    //else
            //    //{
            //    //    Allow.Enabled = true;
            //    //    Deny.Enabled = false;
            //    //}


            //}
        }
        #endregion
    }
}