using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget
{
    public partial class FL_TargetByDepartment : SystemPage
    {
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();


        Employee ObjEmployeeBLL = new Employee();

        Department ObjDepartmentBLL = new Department();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem listItem = ddlChooseYear.Items.FindByText(string.Concat(DateTime.Now.Year, "年"));
                if (listItem != null)
                {
                    ddlChooseYear.ClearSelection();
                    listItem.Selected = true;
                }

                if (Request["EmployeeID"] == null)
                {
                    hideNeedShow.Value = "1";
                    lblEmpLoyeeName.Visible = false;

                    var ObjDepartMentList = ObjDepartmentBLL.GetMyManagerDepartment(User.Identity.Name.ToInt32());
                    string KeyList = string.Empty;

                    foreach (var Objdepartment in ObjDepartMentList)
                    {
                        KeyList += Objdepartment.DepartmentID + ",";

                    }

                    if (KeyList.Length > 0)
                    {
                        string GetWhere = "";
                        if (ddlEmployee1.SelectedItem != null)
                        {
                            GetWhere = " and IsActive=1 and EmployeeID=" + ddlEmployee1.SelectedValue;
                        }
                        else
                        {
                            GetWhere = " and IsActive=1 ";
                        }
                        KeyList = KeyList.Trim(',');
                        //this.repList.DataSource = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, GetWhere);
                        //this.repList.DataBind();
                    }
                    else
                    {
                        btnQuery.Visible = false;
                        btnSerchEmployee.Visible = false;
                    }
                }
                else
                {
                    Employee ObjEmployeeBLL = new Employee();

                    hideNeedShow.Value = "1";
                    ddlChooseYear.Visible = false;
                    ddlDepartment.Visible = false;
                    btnQuery.Visible = false;
                    lblEmpLoyeeName.Text = ObjEmployeeBLL.GetByID(Request["EmployeeID"].ToInt32()).EmployeeName;
                    this.repList.DataSource = ObjFinishTargetSumBLL.GetEmployeetargetbyID(Request["EmployeeID"], " and IsActive=1");
                    this.repList.DataBind();
                }

            }
        }


        #region 获取完成率
        /// <summary>
        /// 获取完成率
        /// </summary>
        public string GetFiinishAvg(object Plan, object Finish)
        {

            if (Plan.ToString().ToDecimal() > 0)
            {
                return (Finish.ToString().ToDecimal() / Plan.ToString().ToDecimal()).ToString("0.00");
            }
            else
            {
                return "0.00%";

            }
        }
        #endregion

        #region 搜索绑定
        /// <summary>
        /// 搜索绑定
        /// </summary>
        private void SerchBinder()
        {
            if (ddlDepartment.SelectedItem.Text == "请选择")
            {
                JavaScriptTools.AlertWindow("请选择部门", Page);
                return;
            }
            string GetWhere = "";
            if (ddlEmployee1.SelectedValue.ToInt32() > 0)
            {
                GetWhere = " and IsActive=1 and EmployeeID=" + ddlEmployee1.SelectedValue;
            }
            else
            {
                GetWhere = " and IsActive=1 ";
            }

            this.repList.DataSource = ObjFinishTargetSumBLL.GetDepartmentTarget(ddlDepartment.SelectedValue, GetWhere);
            this.repList.DataBind();
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            SerchBinder();
        }
        #endregion

        protected void btnSerchEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("FL_DepartmentEmployee.aspx?DepartmentID=" + ddlDepartment.SelectedValue);
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEmployee1.BinderByDepartment(ddlDepartment.SelectedValue.ToInt32());
            SerchBinder();
        }



        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            List<int> EmployeeKey = new List<int>();


            var ObjEmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32());

            foreach (var ObjItem in ObjEmployeeList)
            {
                EmployeeKey.Add(ObjItem.EmployeeID);
            }
            EmployeeKey.Add(User.Identity.Name.ToInt32());
            var BinderSource = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);

            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                this.repList.DataSource = BinderSource.Where(C => C.EmployeeID != User.Identity.Name.ToInt32());
                this.repList.DataBind();
            }
            else
            {
                this.repList.DataSource = BinderSource;
                this.repList.DataBind();
            }

        }


        #region 数据绑定完成事件 ItemDataBound
        /// <summary>
        /// 完成事件
        /// </summary>
        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblPlanSum = e.Item.FindControl("lblPanSum") as Label;
            Label lblFinishSum = e.Item.FindControl("lblFinishSum") as Label;
            Label lblFinishRates = e.Item.FindControl("lblFinishRates") as Label;

            View_DepartmentTarget ItemData = (View_DepartmentTarget)e.Item.DataItem;
            ///计划合计
            lblPlanSum.Text = (ItemData.MonthPlan1 + ItemData.MonthPlan2 + ItemData.MonthPlan3 + ItemData.MonthPlan4 + ItemData.MonthPlan5 + ItemData.MonthPlan6 + ItemData.MonthPlan7 + ItemData.MonthPlan8 + ItemData.MonthPlan9 + ItemData.MonthPlan10 + ItemData.MonthPlan11 + ItemData.MonthPlan12).ToString();
            ///实际完成合计
            lblFinishSum.Text = (ItemData.MonthFinsh1 + ItemData.MonthFinish2 + ItemData.MonthFinish3 + ItemData.MonthFinish4 + ItemData.MonthFinish5 + ItemData.MonthFinish6 + ItemData.MonthFinish7 + ItemData.MonthFinish8 + ItemData.MonthFinish9 + ItemData.MonthFinish10 + ItemData.MonthFinish11 + ItemData.MonthFinish12).ToString();
            ///完成率
            if (lblPlanSum.Text.ToDecimal() > 0)
            {
                lblFinishRates.Text = (lblFinishSum.Text.ToDecimal() / lblPlanSum.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblFinishRates.Text = "0.00%";
            }
        }
        #endregion
    }
}