/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.11
 Description:新人满意度页面
 History:修改日志

 Author:杨洋
 Date:2013.4.11
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
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_DegreeOfSatisfactionContentItemManager : SystemPage
    {
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Customers objCustomersBLL = new Customers();
        
        InvestigateState objInvestigateStateBLL = new InvestigateState();
        DegreeOfSatisfactionContent objDegreeOfSatisfactionContentBLL = new DegreeOfSatisfactionContent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDownList();
                DataBinder();
            }
        }

        protected void DataBinder()
        {
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            #region 分页页码
            //int startIndex = DegreePager.StartRecordIndex;
            //int resourceCount = 0;
            //var query = objDegreeOfSatisfactionBLL.GetbyDegreeOfSatisfactionContentItemParameter(ObjParameterList.ToArray(), DegreePager.PageSize, DegreePager.CurrentPageIndex, out resourceCount);
            //DegreePager.RecordCount = resourceCount;

            //rptDegree.DataSource = query;
            //rptDegree.DataBind();
            #endregion
        }


        protected void DataDownList()
        {
            //查询没有流失的客户
            ddlCustomers.DataSource = objCustomersBLL.GetByAll().Where(C => C.IsLose == false);
            ddlCustomers.DataTextField = "Groom";
            ddlCustomers.DataValueField = "CustomerID";
            ddlCustomers.DataBind();
            ddlInvestigateState.DataSource = objInvestigateStateBLL.GetByAll();
            ddlInvestigateState.DataTextField = "StateContent";
            ddlInvestigateState.DataValueField = "InvestigateStateID";
            ddlInvestigateState.DataBind();
            ddlDegreeOfSatisfactionContent.DataSource = objDegreeOfSatisfactionContentBLL.GetByAll();
            ddlDegreeOfSatisfactionContent.DataTextField = "CSContent";
            ddlDegreeOfSatisfactionContent.DataValueField = "TypeKey";
            ddlDegreeOfSatisfactionContent.DataBind();
        }
        protected void DegreePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //CS_DegreeOfSatisfaction degrees = new CS_DegreeOfSatisfaction();
            //degrees.CustomerID = ddlCustomers.SelectedValue.ToInt32();
            //degrees.SumDof = txtSumDof.Text.ToInt32();
            //degrees.DofContent = txtDofContent.Text;
            //degrees.DofDate = DateTime.Now;
      
            //degrees.InvestigateStateID = ddlInvestigateState.SelectedValue.ToInt32();
            //degrees.IsDelete = false;
            //int result = objDegreeOfSatisfactionBLL.Insert(degrees);
            //if (result > 0)
            //{
            //    DataBinder();
               
            //}
            
        }
    }
}