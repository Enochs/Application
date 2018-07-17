
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:员工管理页面
 History:修改日志
 
 Author:杨洋
 date:2013.3.13
 version:好爱1.0
 description:修改描述
 
 Author:黄晓可
 date:2013.3.29
 version:好爱1.0
 description:增加用户权限初始化工作
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_EmployeeManager : SystemPage
    {
        Employee objEmployeeBLL = new Employee();

        string OrderByColoumName = "EmployeeID";
        int SourceCount = 0;

        #region 页面加载
        /// <summary>
        /// 加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化绑定
                DDLDataBind();      //下拉框绑定
                DataBinder();

                if (User.Identity.Name == "1")
                {
                    btnEditAdmin.Visible = true;
                }

            }
        }
        #endregion

        #region 性别标识
        /// <summary>
        /// 转换性别标识
        /// </summary>
        /// <param name="sex">源数据</param>
        /// <returns></returns>
        protected string GetSex(object sex)
        {
            string sexStr = sex.ToString();
            return sexStr == "0" ? "男" : "女";
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定方法
        /// </summary>
        private void DataBinder()
        {
            int DepartmentID = Request["DepartmentID"].ToInt32();
            List<PMSParameters> Pars = new List<PMSParameters>();

            if (Request["Type"] != null)
            {
                if (Request["Type"] == "Look")
                {
                    Pars.Add("IsDelete", true, NSqlTypes.Bit);
                    td_states.Visible = false;
                    td_statesVal.Visible = false;
                }
            }

            if (ddlIsDelete.SelectedValue.ToInt32() > 0)
            {
                if (ddlIsDelete.SelectedValue.ToInt32() == 1)           //已删除 禁用
                {
                    Pars.Add("IsDelete", true, NSqlTypes.Bit);
                }
                else if (ddlIsDelete.SelectedValue.ToInt32() == 2)      //未删除 启用
                {
                    Pars.Add("IsDelete", false, NSqlTypes.Bit);
                }
            }
            //员工姓名 
            Pars.Add(txtEmployeeName.Text.Trim() != string.Empty, "EmployeeName", txtEmployeeName.Text.Trim().ToString(), NSqlTypes.LIKE);
            //部门
            Pars.Add(ddlDepartment.SelectedValue.ToInt32() > 0, "DepartmentID", ddlDepartment.SelectedValue.ToInt32(), NSqlTypes.Equal);
            //生日
            Pars.Add(DateRangerBirtyDay.IsNotBothEmpty, "BornDate", DateRangerBirtyDay.StartoEnd, NSqlTypes.DateBetween);
            //入职日期
            Pars.Add(DateRangerEntryTime.IsNotBothEmpty, "EntryTime", DateRangerEntryTime.StartoEnd, NSqlTypes.DateBetween);


            if (DepartmentID > 0)
            {
                //加载员工信息
                rptEmployes.DataSource = objEmployeeBLL.GetByDepartmetnID(DepartmentID);
            }
            else if (DepartmentID == -1)
            {
                if (ddlIsDelete.SelectedValue.ToInt32() != 1)           //已删除 禁用
                {
                    Pars.Add("IsDelete", true, NSqlTypes.Bit);
                }
                rptEmployes.DataSource = objEmployeeBLL.GetEmployeeByParameter(Pars, OrderByColoumName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            }
            else if (DepartmentID == -2)
            {
                rptEmployes.DataSource = objEmployeeBLL.GetEmployeeByParameter(Pars, OrderByColoumName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            }
            CtrPageIndex.RecordCount = SourceCount;
            rptEmployes.DataBind();
            lblEmployeeCount.Text = "当前员工数量：" + SourceCount;
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// ItemCommand事件用于删除操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptEmployes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //CommandName来区分删除
            if (e.CommandName == "Delete")
            {
                int employeeId = e.CommandArgument.ToString().ToInt32();

                Sys_Employee sys_Employee = new Sys_Employee()
                {
                    EmployeeID = employeeId

                };
                //删除操作
                objEmployeeBLL.Delete(sys_Employee);
                JavaScriptTools.AlertWindow("用户已停用！", Page);
                //删除操作之后重新绑定数据源
                DataBinder();
            }

            if (e.CommandName == "Open")
            {
                int employeeId = e.CommandArgument.ToString().ToInt32();

                Sys_Employee sys_Employee = objEmployeeBLL.GetByID(employeeId);
                sys_Employee.IsDelete = false;
                //删除操作
                objEmployeeBLL.Update(sys_Employee);

                if (Request["DepartmentID"].ToInt32() == -1)
                {
                    //如果当前页面所表示的是查看已停用的员工
                    if (!SetIsClosedVisibleStyle().Equals(string.Empty))
                    {
                        JavaScriptTools.AlertWindowAndLocation("已启用该员工，并将其恢复到原来的部门下！", "Sys_DepartmentManager.aspx?NeedPopu=1", Page);
                        return;
                    }
                }
                else if (Request["DepartmentID"].ToInt32() == -2)
                {
                    JavaScriptTools.AlertWindow("用户启用成功！", Page);
                }
                //删除操作之后重新绑定数据源
                DataBinder();
            }

            if (e.CommandName == "Star")
            {

                UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
                var UpdateModel = objEmployeeBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                //if (!UpdateModel.IsLoading.Value)
                //{
                JurisdictionforButton ObjJurisdictionforButtonBLL = new JurisdictionforButton();
                if (ObjUserJurisdictionBLL.StarUserJurisdiction(e.CommandArgument.ToString().ToInt32()))
                {
                    //  ObjJurisdictionforButtonBLL.StarJurisdictionforButton(e.CommandArgument.ToString().ToInt32());
                }


                UpdateModel.IsLoading = true;
                JavaScriptTools.AlertWindow("初始化成功！", this.Page);
                //}
                //else
                //{
                //    JavaScriptTools.AlertWindow("已经进行过初始化！", this.Page);
                //}
            }

        }
        #endregion

        #region 点击修改管理密码
        /// <summary>
        /// 修改信息
        /// </summary>
        protected void btnEditAdmin_Click(object sender, EventArgs e)
        {
            btnEditAdmin.Visible = false;
            btnSaveEdit.Visible = true;
            txtPassWord.Visible = true;
            Label1.Visible = true;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 修改之后 保存
        /// </summary>
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            btnEditAdmin.Visible = true;
            btnSaveEdit.Visible = false;
            txtPassWord.Visible = false;
            Label1.Visible = false;
            var UpdateModel = objEmployeeBLL.GetByID(1);
            UpdateModel.PassWord = txtPassWord.Text.MD5Hash();
            objEmployeeBLL.Update(UpdateModel);

            JavaScriptTools.AlertWindow("保存成功！", Page);
        }
        #endregion

        #region 隐藏事件  Repeater绑定事件

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <returns></returns>
        protected string SetIsClosedVisibleStyle()
        {
            return Request["DepartmentID"].ToInt32() <= 0 ? "style='display:none'" : string.Empty;
        }

        /// <summary>
        /// Repeater绑定事件
        /// </summary>
        protected void rptEmployes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!SetIsClosedVisibleStyle().Equals(string.Empty))
            {
                e.Item.FindControl("lkbtnDelete").Visible = false;
                e.Item.FindControl("lnkbtnStar").Visible = false;
                btnEditAdmin.Visible = false;
                Label1.Visible = false;
                btnSaveEdit.Visible = false;
                if (Request["DepartmentID"].ToInt32() == -2)
                {
                    e.Item.FindControl("lkbtnDelete").Visible = true;
                }
            }
        }
        #endregion

        #region 分页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 下拉框绑定
        /// </summary>

        public void DDLDataBind()
        {
            Department ObjDepartmentBLL = new Department();
            ddlDepartment.DataSource = ObjDepartmentBLL.GetByAll();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("请选择"));
        }
        #endregion

        #region 点击查询按钮
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnLook_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion
    }


}