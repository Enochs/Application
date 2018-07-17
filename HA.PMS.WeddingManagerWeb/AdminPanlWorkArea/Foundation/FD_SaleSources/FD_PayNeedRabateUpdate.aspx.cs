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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_PayNeedRabateUpdate : SystemPage
    {
        SaleSources objSaleSourcesBLL = new SaleSources();
    
         PayNeedRabate objPayNeedRabateBLL = new PayNeedRabate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int Id = Request.QueryString["Id"].ToInt32();
                FD_PayNeedRabate payNeedRabate = objPayNeedRabateBLL.GetByID(Id);
                txtPayDate.Text = payNeedRabate.PayDate + string.Empty;
                txtPayMoney.Text = payNeedRabate.PayMoney + string.Empty;
                txtPaypolicy.Text = payNeedRabate.Paypolicy;
                txtOrderMoney.Text = payNeedRabate.OrderMoney + string.Empty;
                txtMoneyCell.Text = payNeedRabate.MoneyCell;


                 
            }
        }
        protected void ddlChannelTypeCreate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int type = ddlChannelTypeCreate.SelectedValue.ToInt32();
            ddlChannelCreate.DataSource = objSaleSourcesBLL.GetByAll().Where(C => C.ChannelTypeId == type);
            ddlChannelCreate.DataTextField = "Sourcename";
            ddlChannelCreate.DataValueField = "SourceID";
            ddlChannelCreate.DataBind();
        }

        protected void ddlChannelCreate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTactcontacts.Items.Clear();
            int sourceId = ddlChannelCreate.SelectedValue.ToInt32();
            HA.PMS.DataAssmblly.FD_SaleSources saleSource = objSaleSourcesBLL.GetByID(sourceId);

            ddlTactcontacts.Items.Add(new ListItem(saleSource.Tactcontacts1));
            ddlTactcontacts.Items.Add(new ListItem(saleSource.Tactcontacts2));
            ddlTactcontacts.Items.Add(new ListItem(saleSource.Tactcontacts3));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Id = Request.QueryString["Id"].ToInt32();
            FD_PayNeedRabate rabate = objPayNeedRabateBLL.GetByID(Id);
            
            rabate.MoneyCell = txtMoneyCell.Text.Trim();

            rabate.MoneyPerson = ddlTactcontacts.Text;
            rabate.ChannelTypeId = ddlChannelTypeCreate.SelectedValue.ToInt32();
            rabate.SourceID = ddlChannelCreate.SelectedValue.ToInt32();
            rabate.Paypolicy = txtPaypolicy.Text;
            rabate.PayMoney = txtPayMoney.Text.ToDecimal();
            rabate.OrderMoney = txtOrderMoney.Text.ToDecimal();
            rabate.PayDate = txtPayDate.Text.ToDateTime();
            int result = objPayNeedRabateBLL.Update(rabate);
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