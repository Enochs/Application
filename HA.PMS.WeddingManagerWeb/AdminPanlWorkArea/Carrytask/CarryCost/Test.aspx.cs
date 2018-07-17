using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class Test : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        HA.PMS.BLLAssmblly.Flow.Order ObjOrderBLL = new BLLAssmblly.Flow.Order();

        Employee ObjEmployeeBLL = new Employee();
        Report ObjReportBLL = new Report();
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Target ObjTargetBLL = new Target();

        int CustomerID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        protected void btn_Updates_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < repDataKist.Items.Count; i++)
            //{
            //    var ObjItem = repDataKist.Items[i];
            //    int PlanId = ((HiddenField)ObjItem.FindControl("HidePlanId")).Value.ToInt32();
            //    FL_QuotedCollectionsPlan ObjPlanModel = ObjQuotedCollectionsPlanBLL.GetByID(PlanId);

            //    Button btnDel = (Button)ObjItem.FindControl("btn_del");

            //    TextBox txtRealityAmount = (TextBox)ObjItem.FindControl("txtRealityAmount");
            //    //RadioButtonList rdoMoneytypes = (RadioButtonList)ObjItem.FindControl("rdoMoneytypes");
            //    TextBox txtNode = (TextBox)ObjItem.FindControl("txtNode");


            //    ObjPlanModel.RealityAmount = txtRealityAmount.Text.ToDecimal();
            //    ObjPlanModel.Amountmoney = txtRealityAmount.Text.ToDecimal();
            //    //ObjPlanModel.MoneyType = rdoMoneytypes.SelectedItem.Text;
            //    ObjPlanModel.Node = txtNode.Text.ToString();
            //    ObjQuotedCollectionsPlanBLL.Update(ObjPlanModel);
            //    BinderData();


            //}

            for (int j = 0; j < repQuotedPlan.Items.Count; j++)
            {
                var currentItem = repQuotedPlan.Items[j];
                string RealityAmount = (currentItem.FindControl("txtRealityAmount") as TextBox).Text;
                string Node = (currentItem.FindControl("txtNode") as TextBox).Text;

                int PlanId = (currentItem.FindControl("HidePlanId") as HiddenField).Value.ToInt32();
                FL_QuotedCollectionsPlan ObjPlanModel = ObjQuotedCollectionsPlanBLL.GetByID(PlanId);

                ObjPlanModel.RealityAmount = RealityAmount.ToDecimal();
                ObjPlanModel.Amountmoney = RealityAmount.ToDecimal();
                ObjPlanModel.Node = Node;
                ObjQuotedCollectionsPlanBLL.Update(ObjPlanModel);
                BinderData();
            }
        }
        private void BinderData()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))     //是管理层  就可以看见修改按钮
            {
                btn_Updates.Visible = true;
            }
            else
            {
                btn_Updates.Visible = false;
            }

            var BinderList = ObjQuotedCollectionsPlanBLL.GetByOrderID(Request["OrderID"].ToInt32());
            lblFinishMoney.Text = BinderList.Sum(C => C.RealityAmount).ToString();
            lblSaleAmount.Text = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32()).RealAmount.ToString();
            lblHaveMoney.Text = (lblSaleAmount.Text.ToDecimal() - lblFinishMoney.Text.ToDecimal()).ToString();

            this.repQuotedPlan.DataSource = BinderList;
            this.repQuotedPlan.DataBind();

        }
    }
}