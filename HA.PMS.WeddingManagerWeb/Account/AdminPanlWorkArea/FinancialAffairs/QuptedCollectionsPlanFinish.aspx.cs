using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class QuptedCollectionsPlanFinish : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.repDataKist.DataSource = ObjQuotedCollectionsPlanBLL.GetByOrderID(Request["OrderID"].ToInt32());
            this.repDataKist.DataBind();

            GetOverFinishMoney(Request["CustomerID"].ToInt32());

        }






        /// <summary>
        /// 全部确认收款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repDataKist.Items.Count; i++)
            {
                var UpdateModel = ObjQuotedCollectionsPlanBLL.GetByID(((HiddenField)repDataKist.Items[i].FindControl("hidekey")).Value.ToInt32());
                UpdateModel.RealityAmount = ((TextBox)repDataKist.Items[i].FindControl("txtAmount")).Text.ToDecimal();
                UpdateModel.CollectionTime = ((TextBox)repDataKist.Items[i].FindControl("txtTimer")).Text.ToDateTime();
                UpdateModel.FinancialEmployee = User.Identity.Name.ToInt32();
                UpdateModel.RowLock = true;
                ObjQuotedCollectionsPlanBLL.Update(UpdateModel);
            }
            BinderData();
            JavaScriptTools.AlertWindow("确认成功", Page);
        }


        /// <summary>
        /// 单挑确认收款
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repDataKist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Finish")
            {
                var UpdateModel = ObjQuotedCollectionsPlanBLL.GetByID(((HiddenField)e.Item.FindControl("hidekey")).Value.ToInt32());
                UpdateModel.RealityAmount = ((TextBox)e.Item.FindControl("txtAmount")).Text.ToDecimal();
                if (UpdateModel.RealityAmount == 0)
                {
                    JavaScriptTools.AlertWindow("收款金额不能等于0", Page);
                    return;
                }

                if (((TextBox)e.Item.FindControl("txtTimer")).Text == string.Empty)
                {
                    JavaScriptTools.AlertWindow("收款时间不能为空", Page);
                    return;
                }
                UpdateModel.CollectionTime = ((TextBox)e.Item.FindControl("txtTimer")).Text.ToDateTime();


                UpdateModel.FinancialEmployee = User.Identity.Name.ToInt32();
                UpdateModel.RowLock = true;

                ObjQuotedCollectionsPlanBLL.Update(UpdateModel);
            }
            JavaScriptTools.AlertWindowAndLocation("确认成功", Request.Url.ToString(), Page);
   
        }


        /// <summary>
        /// 绑定禁止操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repDataKist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var Objitem = (FL_QuotedCollectionsPlan)e.Item.DataItem;
            if (Objitem.RowLock.Value)
            {
                TextBox txtTimer = (TextBox)e.Item.FindControl("txtTimer");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                LinkButton LinkButton1 = (LinkButton)e.Item.FindControl("LinkButton1");
                txtTimer.Enabled = false;
                txtAmount.Enabled = false;
                LinkButton1.Visible = false;
            }
        }
    }
}
