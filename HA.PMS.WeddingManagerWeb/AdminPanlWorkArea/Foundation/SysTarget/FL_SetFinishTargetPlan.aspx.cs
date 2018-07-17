using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget
{
    public partial class FL_SetFinishTargetPlan : SystemPage
    {
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();

        Department ObjDepartmentBLL = new Department();

        Employee ObjEmployeeBLL = new Employee();

        int SourceCount = 0;
        string OrderColumnname = "FinishKey";
        int index = 0;


        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (index == 0)
                {
                    List<Sys_Employee> EmployeeList = ObjEmployeeBLL.GetByAll();
                    foreach (var item in EmployeeList)
                    {
                        FL_FinishTargetSum TargetModel = ObjFinishTargetSumBLL.GetByEmployeeID(item.EmployeeID);
                        if (TargetModel != null)
                        {
                            TargetModel.DepartmentID = item.DepartmentID;
                            ObjFinishTargetSumBLL.Update(TargetModel);
                        }
                    }
                    index++;
                }
                ddlChooseYear.SelectedItem.Text = DateTime.Now.Year.ToString();
                DataBinder();
                
            }
        }
        #endregion

        #region 加载数据 方法
        /// <summary>
        /// 加载
        /// </summary>
        public void DataBinder()
        {
            List<PMSParameters> objparlist = new List<PMSParameters>();
            Sys_Employee ObjEmployeeModel = ObjEmployeeBLL.GetByID(ddlEmployee1.SelectedValue.ToInt32());
            string GetWhere = " and Year=" + ddlChooseYear.SelectedItem.Text.ToInt32();
            objparlist.Add("Year", ddlChooseYear.SelectedItem.Text, NSqlTypes.Equal);


            if (ddlDepartment.SelectedItem.Text != "请选择")
            {
                GetWhere += " and DepartmentID=" + ddlDepartment.SelectedValue;
                objparlist.Add("DepartmentID", ddlDepartment.SelectedValue, NSqlTypes.Equal);
            }

            if (ddlEmployee1.SelectedValue.ToInt32() > 0)
            {
                GetWhere = " and IsActive=1 and EmployeeID=" + ddlEmployee1.SelectedValue;
                objparlist.Add("IsActive", 1, NSqlTypes.Bit);
                objparlist.Add("EmployeeID", ddlEmployee1.SelectedValue);
            }
            else
            {
                GetWhere = " and IsActive=1";
                objparlist.Add("IsActive", 1, NSqlTypes.Bit);
            }

            List<FL_FinishTargetSum> ObjFinishTargetSumList = ObjFinishTargetSumBLL.GetDataByWhereParameter(objparlist, OrderColumnname, CtrPageIndex.PageSize + 10000, 1, out SourceCount, OrderType.Asc);
            List<FL_FinishTargetSum> DataList = new List<FL_FinishTargetSum>();

            foreach (var item in ObjFinishTargetSumList)        //显示未删除  已删除的就不要显示了
            {
                if (ObjEmployeeBLL.GetByID(item.EmployeeID).IsDelete == false)
                {
                    DataList.Add(item);
                }
                SourceCount++;
            }

            //this.rptTargetFinish.DataSource = ObjFinishTargetSumBLL.GetByActiveOrEmployeeID(GetWhere);
            this.rptTargetFinish.DataSource = DataList.Skip((CtrPageIndex.CurrentPageIndex - 1) * CtrPageIndex.PageSize).Take(10);
            this.rptTargetFinish.DataBind();

            CtrPageIndex.RecordCount = SourceCount;

            for (int i = 0; i < rptTargetFinish.Items.Count; i++)
            {
                var ObjItem = rptTargetFinish.Items[i];
                Label lblPlanSum = ObjItem.FindControl("lblYearPlanSum") as Label;
                Label lblFinishSum = ObjItem.FindControl("lblFinishYear") as Label;
                Label lblFinishRates = ObjItem.FindControl("lblFinishRates") as Label;
                HiddenField HideKey = ObjItem.FindControl("HideKey") as HiddenField;
                FL_FinishTargetSum ObjFinishModel = ObjFinishTargetSumBLL.GetByID(HideKey.Value.ToInt32());
                lblPlanSum.Text = (ObjFinishModel.MonthPlan1 + ObjFinishModel.MonthPlan2 + ObjFinishModel.MonthPlan3 + ObjFinishModel.MonthPlan4 + ObjFinishModel.MonthPlan5 + ObjFinishModel.MonthPlan6 + ObjFinishModel.MonthPlan7 + ObjFinishModel.MonthPlan8 + ObjFinishModel.MonthPlan9 + ObjFinishModel.MonthPlan10 + ObjFinishModel.MonthPlan11 + ObjFinishModel.MonthPlan12).ToString();
                lblFinishSum.Text = (ObjFinishModel.MonthFinsh1 + ObjFinishModel.MonthFinish2 + ObjFinishModel.MonthFinish3 + ObjFinishModel.MonthFinish4 + ObjFinishModel.MonthFinish5 + ObjFinishModel.MonthFinish6 + ObjFinishModel.MonthFinish7 + ObjFinishModel.MonthFinish8 + ObjFinishModel.MonthFinish9 + ObjFinishModel.MonthFinish10 + ObjFinishModel.MonthFinish11 + ObjFinishModel.MonthFinish12).ToString();
                if (lblPlanSum.Text.ToDecimal() > 0)
                {
                    lblFinishRates.Text = (lblFinishSum.Text.ToDecimal() / lblPlanSum.Text.ToDecimal()).ToString("0.00%");
                }
                else
                {
                    lblFinishRates.Text = "0.00%";
                }
            }

        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 点击查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 选择员工
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSerchEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("FL_DepartmentEmployee.aspx?DepartmentID=" + ddlDepartment.SelectedValue);
        }
        #endregion

        #region 获取完成率
        /// <summary>
        /// 获取完成率
        /// </summary>
        /// <param name="Plan"></param>
        /// <param name="Finish"></param>
        /// <returns></returns>
        public string GetFiinishAvg(object Plan, object Finish)
        {

            if (Plan.ToString().ToDecimal() > 0)
            {
                return ((Finish.ToString().ToDecimal() / Plan.ToString().ToDecimal()) * 100).ToString("f2") + "%";

            }
            else
            {
                return "0%";

            }
        }
        #endregion

        #region 部门下拉框选择
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEmployee1.BinderByDepartment(ddlDepartment.SelectedValue.ToInt32());
        }
        #endregion

        #region Repeater生成事件 保存
        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void rptTargetFinish_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            TextBox txtPlan1 = e.Item.FindControl("txtTargetPlan1") as TextBox;
            TextBox txtPlan2 = e.Item.FindControl("txtTargetPlan2") as TextBox;
            TextBox txtPlan3 = e.Item.FindControl("txtTargetPlan3") as TextBox;
            TextBox txtPlan4 = e.Item.FindControl("txtTargetPlan4") as TextBox;
            TextBox txtPlan5 = e.Item.FindControl("txtTargetPlan5") as TextBox;
            TextBox txtPlan6 = e.Item.FindControl("txtTargetPlan6") as TextBox;
            TextBox txtPlan7 = e.Item.FindControl("txtTargetPlan7") as TextBox;
            TextBox txtPlan8 = e.Item.FindControl("txtTargetPlan8") as TextBox;
            TextBox txtPlan9 = e.Item.FindControl("txtTargetPlan9") as TextBox;
            TextBox txtPlan10 = e.Item.FindControl("txtTargetPlan10") as TextBox;
            TextBox txtPlan11 = e.Item.FindControl("txtTargetPlan11") as TextBox;
            TextBox txtPlan12 = e.Item.FindControl("txtTargetPlan12") as TextBox;

            if (e.CommandName == "Save")
            {
                int finishKey = Convert.ToInt32(e.CommandArgument);
                FL_FinishTargetSum ObjFinishTargetModel = ObjFinishTargetSumBLL.GetByID(finishKey);
                ObjFinishTargetModel.MonthPlan1 = txtPlan1.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan2 = txtPlan2.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan3 = txtPlan3.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan4 = txtPlan4.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan5 = txtPlan5.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan6 = txtPlan6.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan7 = txtPlan7.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan8 = txtPlan8.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan9 = txtPlan9.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan10 = txtPlan10.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan11 = txtPlan11.Text.ToString().ToDecimal();
                ObjFinishTargetModel.MonthPlan12 = txtPlan12.Text.ToString().ToDecimal();
                ObjFinishTargetModel.PlanSum = ObjFinishTargetModel.MonthPlan1 + ObjFinishTargetModel.MonthPlan2 + ObjFinishTargetModel.MonthPlan3 + ObjFinishTargetModel.MonthPlan4 + ObjFinishTargetModel.MonthPlan5 + ObjFinishTargetModel.MonthPlan6 + ObjFinishTargetModel.MonthPlan7 + ObjFinishTargetModel.MonthPlan8 + ObjFinishTargetModel.MonthPlan9 + ObjFinishTargetModel.MonthPlan10 + ObjFinishTargetModel.MonthPlan11 + ObjFinishTargetModel.MonthPlan12;
                int result = ObjFinishTargetSumBLL.Update(ObjFinishTargetModel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("保存成功", Page);
                    DataBinder();
                }
                else
                {
                    JavaScriptTools.AlertWindow("修改失败,请稍候再试...", Page);
                }
            }

        }
        #endregion

        #region 点击分页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion


        public void GetAllPlan()
        {
            string KeyList = string.Empty;
            var ObjDepartMentList = ObjEmployeeBLL.GetMyManagerEmpLoyees(ddlEmployee1.SelectedValue.ToInt32(), ref KeyList);

            KeyList = KeyList.Trim(',');
            if (KeyList == string.Empty)
            {
                KeyList = ObjEmployeeBLL.GetByID(Request.Cookies["HAEmployeeID"].Value.ToInt32()).DepartmentID.ToString();
            }


            List<int> EmployeeKey = new List<int>();


            EmployeeKey.Add(ddlEmployee1.SelectedValue.ToInt32());

            var ObjNeed = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);
            if (ObjNeed.Count > 0)
            {

                int Month = DateTime.Now.Month;
                var ObjList = new List<View_DepartmentTarget>();
                FL_FinishTargetSum FinishModel = new FL_FinishTargetSum();

                if (ObjEmployeeBLL.IsManager(Request.Cookies["HAEmployeeID"].Value.ToInt32()))
                {
                    ObjList = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year=" + DateTime.Now.Year + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                }
                else
                {
                    ObjList = ObjFinishTargetSumBLL.GetEmployeetargetbyID(Request.Cookies["HAEmployeeID"].Value, " and Year=" + DateTime.Now.Year + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                }
            }



        }
    }
}