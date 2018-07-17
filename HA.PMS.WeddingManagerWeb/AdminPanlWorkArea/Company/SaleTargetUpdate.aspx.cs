using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class SaleTargetUpdate : SystemPage
    {
        SaleTarget objSaleTargetBLL = new SaleTarget();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int TargetKey = Request.QueryString["TargetKey"].ToInt32();
                CA_SaleTarget ca_SaleTarget = objSaleTargetBLL.GetByID(TargetKey);
                txtTarget.Text = ca_SaleTarget.TargetKey + string.Empty;
                txtYear.Text = ca_SaleTarget.Year + string.Empty;
                txtMonth.Text = ca_SaleTarget.Month + string.Empty;
                txtQuarter.Text = ca_SaleTarget.Quarter + string.Empty;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int TargetKey = Request.QueryString["TargetKey"].ToInt32();
            CA_SaleTarget ca_SaleTarget = objSaleTargetBLL.GetByID(TargetKey);
            ca_SaleTarget.TargetKey = txtTarget.Text.ToInt32();
            ca_SaleTarget.Year = txtYear.Text.ToDecimal();
            ca_SaleTarget.Month = txtMonth.Text.ToDecimal();
            ca_SaleTarget.Quarter = txtQuarter.Text.ToDecimal();
            int result = objSaleTargetBLL.Update(ca_SaleTarget);
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
    }
}