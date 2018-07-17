using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedCollectionsPlanUpdate :SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();
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

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repDataKist.Items.Count; i++)
            {
                var UpdateModel=  ObjQuotedCollectionsPlanBLL.GetByID(((HiddenField)repDataKist.Items[i].FindControl("hidekey")).Value.ToInt32());
                UpdateModel.Amountmoney = ((TextBox)repDataKist.Items[i].FindControl("txtAmount")).Text.ToDecimal();
                UpdateModel.CreateDate = ((TextBox)repDataKist.Items[i].FindControl("txtTimer")).Text.ToDateTime();
                //UpdateModel.RowLock = false;
                ObjQuotedCollectionsPlanBLL.Update(UpdateModel);
            }
            BinderData();
            JavaScriptTools.AlertWindow("全部修改完毕",Page);
        }
    }
}