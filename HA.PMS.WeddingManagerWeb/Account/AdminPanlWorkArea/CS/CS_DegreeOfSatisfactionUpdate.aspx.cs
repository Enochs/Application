/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:客户满意度修改页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_DegreeOfSatisfactionUpdate : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        InvestigateState objInvestigateStateBLL = new InvestigateState();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                int DofKey = Request.QueryString["DofKey"].ToInt32();
                CS_DegreeOfSatisfaction cS_DegreeOfSatisfaction = objDegreeOfSatisfactionBLL.GetByID(DofKey);

                txtDofDate.Text = cS_DegreeOfSatisfaction.DofDate + string.Empty;
                txtDofContent.Text = cS_DegreeOfSatisfaction.DofContent;


                txtDegreeResult.Text = cS_DegreeOfSatisfaction.DegreeResult;
                txtSumDof.Text = cS_DegreeOfSatisfaction.SumDof + string.Empty;


                ddlCustomer.Items.FindByValue(cS_DegreeOfSatisfaction.CustomerID + string.Empty).Selected = true;
                //ddlInvestigateStateID.Items.FindByValue(cS_DegreeOfSatisfaction.InvestigateStateID + string.Empty).Selected = true;
             
            }
        }

        protected void DataBinder()
        {
            ddlCustomer.DataSource = objCustomersBLL.GetByAll();
            ddlCustomer.DataTextField = "Groom";
            ddlCustomer.DataValueField = "CustomerID";
            ddlCustomer.DataBind();

            ddlInvestigateStateID.DataSource = objInvestigateStateBLL.GetByAll();
            ddlInvestigateStateID.DataTextField = "StateContent";
            ddlInvestigateStateID.DataValueField = "InvestigateStateID";
            ddlInvestigateStateID.DataBind();


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //int DofKey = Request.QueryString["DofKey"].ToInt32();
            //CS_DegreeOfSatisfaction cS_DegreeOfSatisfaction = objDegreeOfSatisfactionBLL.GetByID(DofKey);


            //cS_DegreeOfSatisfaction.DofDate = txtDofDate.Text.ToDateTime();
            //cS_DegreeOfSatisfaction.DofContent = txtDofContent.Text;
            //cS_DegreeOfSatisfaction.CustomerID = ddlCustomer.SelectedValue.ToInt32();
            //cS_DegreeOfSatisfaction.InvestigateStateID = ddlInvestigateStateID.SelectedValue.ToInt32();
            //cS_DegreeOfSatisfaction.IsDelete = false;
            //cS_DegreeOfSatisfaction.DegreeResult = txtDegreeResult.Text;
            //cS_DegreeOfSatisfaction.SumDof = txtSumDof.Text.ToInt32();
            //int result = objDegreeOfSatisfactionBLL.Update(cS_DegreeOfSatisfaction);
            ////根据返回判断添加的状态
            //if (result > 0)
            //{
            //    JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            //}
            //else
            //{
            //    JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            //}
        }
    }
}