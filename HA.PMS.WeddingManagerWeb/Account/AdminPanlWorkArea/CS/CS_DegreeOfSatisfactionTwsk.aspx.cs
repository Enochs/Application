/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.15
 Description:一周开始
 History:修改日志

 Author:杨洋
 Date:2013.4.15
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
    public partial class CS_DegreeOfSatisfactionTwsk : SystemPage
    {
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Order objOrderBLL = new Order();
        InvestigateState objInvestigateState = new InvestigateState();


        HA.PMS.BLLAssmblly.Flow.Invite ObjInvite = new BLLAssmblly.Flow.Invite();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList();
                DataBinder();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetComeDate(object CustomerID)
        {
            var ObJModel = ObjInvite.GetByCustomerID(CustomerID.ToString().ToInt32());
            if (ObJModel != null)
            {

                return ObJModel.ComeDate.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        public string GetOrderDate(object CustomerID)
        {
            return objOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32()).LastFollowDate.ToString();
        }

        /// <summary>
        /// 下拉框绑定数据
        /// </summary>
        protected void DropDownList()
        {
            //ListItem firstChoose = new ListItem("请选择", "0");
            //ddlPlanner.DataSource = objOrderBLL.GetPlannerAll()
            //    .Distinct(new FL_QuotedPriceEmployeePlannerComparer());
            //ddlPlanner.DataTextField = "EmployeeName";
            //ddlPlanner.DataValueField = "EmpLoyeeID";

            //ddlPlanner.DataBind();
            //ddlPlanner.Items.Add(firstChoose);
            //ddlPlanner.SelectedIndex = ddlPlanner.Items.Count - 1;
        }
        /// <summary>
        /// 返回调查结果的相关的html字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDegreeOfSatisfactionHtmlStr(object source)
        {
            //int customersID = (source + string.Empty).ToInt32();
            //string htmlStr = "<td>{0}</td><td>{1}</td>";
            //CS_DegreeOfSatisfaction entity = objDegreeOfSatisfactionBLL.GetByCustomersID(customersID);
            //if (entity!=null)
            //{
            //    htmlStr = string.Format(htmlStr, GetDateStr(entity.DofDate), entity.DegreeResult);

            //    CS_InvestigateState entityInvestigateState = objInvestigateState.GetByID(entity.InvestigateStateID);
            //    if (entityInvestigateState!=null)
            //    {
            //        htmlStr = "<td>" + entityInvestigateState.StateContent + "</td>" + htmlStr;
            //    } 
            //    return htmlStr;
            //}
            //return "<td></td><td></td>";
            return string.Empty;
        }

        public string GetTakeDateByCustomersID(object source)
        {
            int customerID = (source + string.Empty).ToInt32();
            TakeDisk objTakeBLL = new TakeDisk();
            CS_TakeDisk entity = objTakeBLL.GetByCustomerID(customerID);
            if (entity != null)
            {
                return GetDateStr(entity.realityTime);
            }
            return string.Empty;

        }
        protected void DataBinder()
        {
            View_GetOrderCustomers getOrder = new View_GetOrderCustomers();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("CdState", 1));
            ObjParameterList.Add(new ObjectParameter("IsDelete", false));

            DateTime EndDate = DateTime.Today.AddDays(7-(int)DateTime.Today.DayOfWeek);


            ObjParameterList.Add(new ObjectParameter("PlanDate_between", DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek)).ToShortDateString() + "," + EndDate.ToShortDateString()));


            if (!string.IsNullOrEmpty(txtGromm.Text))
            {
                ObjParameterList.Add(new ObjectParameter("Bride_LIKE", txtGromm.Text));
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add(new ObjectParameter("BrideCellPhone", txtGroomCellPhone.Text));
            }
 
     
            #region 分页页码
            int startIndex = DegreePager.StartRecordIndex;
            int resourceCount = 0;

            var query = objDegreeOfSatisfactionBLL.GetDefrreSatisactionByParameter(DegreePager.PageSize, DegreePager.CurrentPageIndex, out resourceCount, ObjParameterList.ToArray());
            DegreePager.RecordCount = resourceCount;
            rptDegree.DataSource = query;
            rptDegree.DataBind();


            #endregion
        }

        protected void DegreePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnCustomerQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptDegree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int customerID = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName == "SaveChange")
            {
                var ObjUpdateModel = objDegreeOfSatisfactionBLL.GetByCustomerID(customerID);

                ObjUpdateModel.State = 1;

                objDegreeOfSatisfactionBLL.Update(ObjUpdateModel);
                DataBinder();
                JavaScriptTools.AlertWindow("保存成功", this.Page);

            }
        }
    }
}