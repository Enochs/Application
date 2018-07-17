using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class DispatchingEmpLoyeeManager : SystemPage
    {
        DispatchingEmployeeManager ObjDispatchingEmployeeManagerBLL = new DispatchingEmployeeManager();
        int DispatchingID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            if (!IsPostBack)
            {

                BinderData();
            }
        }


        private void BinderData()
        {
            this.repWeddingPlanning.DataSource = ObjDispatchingEmployeeManagerBLL.GetEmpLoyeeByDispachingID(DispatchingID);
            this.repWeddingPlanning.DataBind();
        }

        /// <summary>
        /// 保存项目人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            FL_DispatchingEmployeeManager ObjInsertModel = new DataAssmblly.FL_DispatchingEmployeeManager();
            ObjInsertModel.DispatchingID = DispatchingID;
            ObjInsertModel.EmployeeName = txtEmployeeName.Text;
            ObjInsertModel.EmployeeType = txtEmployeeType.Text;
            ObjInsertModel.Amount = txtAmount.Text.ToDecimal();
            ObjInsertModel.Bulding = txtBulding.Text;
            ObjInsertModel.TelPhone = txtTelPhone.Text;
            ObjInsertModel.CreateDate = DateTime.Now;
            ObjInsertModel.CreateEmployee = User.Identity.Name.ToString();
            ObjDispatchingEmployeeManagerBLL.Insert(ObjInsertModel);


            //财务成本主体
            Cost ObjCostBLL = new Cost();

            //财务成本明细
            OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();

            FL_OrderfinalCost ObjFinalCostModel;


            #region 财务成本明细


            ObjFinalCostModel = new FL_OrderfinalCost();
            ObjFinalCostModel.KindType = 2;
            ObjFinalCostModel.IsDelete = false;
            ObjFinalCostModel.CostKey = ObjCostBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).CostKey; ;
            ObjFinalCostModel.CreateDate = DateTime.Now;
            ObjFinalCostModel.CustomerID = Request["CustomerID"].ToInt32();
            ObjFinalCostModel.CellPhone = txtTelPhone.Text;
            ObjFinalCostModel.InsideRemark = string.Empty;
            ObjFinalCostModel.KindID = Request["DispatchingID"].ToInt32();
            ObjFinalCostModel.ServiceContent = txtEmployeeName.Text;

            ObjFinalCostModel.PlannedExpenditure = txtAmount.Text.ToDecimal();
            ObjFinalCostModel.ActualExpenditure = 0;
            ObjFinalCostModel.Expenseaccount = string.Empty;

            ObjFinalCostModel.ActualWorkload = txtBulding.Text;
            ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);

            #endregion
            txtTelPhone.Text = string.Empty;
            txtEmployeeType.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtBulding.Text = string.Empty;
            txtAmount.Text = string.Empty;
            BinderData();
        }


        /// <summary>
        /// 删除或者修改
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repWeddingPlanning_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                ObjDispatchingEmployeeManagerBLL.Delete(new FL_DispatchingEmployeeManager() { DeJey = e.CommandArgument.ToString().ToInt32() });
                OrderfinalCost ObjOrderfinalCost = new OrderfinalCost();


                var ObjModel = ObjOrderfinalCost.GetByEmpLoyeeName(((TextBox)e.Item.FindControl("txtEmployeeName")).Text, DispatchingID);
                if (ObjModel != null)
                {
                    ObjModel.IsDelete = true;


                    ObjOrderfinalCost.Update(ObjModel);
                    JavaScriptTools.AlertWindow("已经保存数据到财务处!", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("暂未找到数据!", Page);
                }
            }

            BinderData();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }
    }
}