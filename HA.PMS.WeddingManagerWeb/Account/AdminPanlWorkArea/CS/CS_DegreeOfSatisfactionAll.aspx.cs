/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.15
 Description:全部调查界面
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
    public partial class CS_DegreeOfSatisfactionAll : SystemPage
    {
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Order objOrderBLL = new Order();
        InvestigateState objInvestigateState = new InvestigateState();




        HA.PMS.BLLAssmblly.Flow.Invite ObjInvite = new BLLAssmblly.Flow.Invite();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }


        #region 获取到店时间
        /// <summary>
        /// 获取到店时间
        /// </summary>
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
        #endregion


        #region 获取预定事件
        /// <summary>
        /// 获取预定时间
        /// </summary>
        public string GetOrderDate(object CustomerID)
        {
            var ObjModel = objOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32());
            if (ObjModel != null)
            {
                return ObjModel.LastFollowDate.ToString();
            }
            return string.Empty;
        }
        #endregion


        #region 数据绑定
        /// <summary>
        /// 绑定总体满意度
        /// </summary>
        protected void DataBinder()
        {

            //写入参数
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();
            ObjParameterList.Add("CdState", 2);     //已经评价满意度的
            ObjParameterList.Add("State", 206 + "," + 208, NSqlTypes.IN);
            ObjParameterList.Add("IsDelete", false, NSqlTypes.Bit);
            ObjParameterList.Add("FinishOver", true, NSqlTypes.Bit);
            ObjParameterList.Add("ParentQuotedID", 0, NSqlTypes.Equal);



            //参数构造
            if (!string.IsNullOrEmpty(txtBride.Text))
            {
                ObjParameterList.Add("Bride", txtBride.Text + ",,string", NSqlTypes.Split);
                ObjParameterList.Add("Groom", txtBride.Text + ",1,string", NSqlTypes.Split);

            }
            if (!string.IsNullOrEmpty(txtBrideCellPhone.Text))
            {
                ObjParameterList.Add("BrideCellPhone", txtBrideCellPhone.Text, NSqlTypes.StringEquals);
                ObjParameterList.Add("GroomCellPhone", txtBrideCellPhone.Text, NSqlTypes.OR);
            }


            ObjParameterList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            // ObjParameterList.Add(new ObjectParameter("BrideCellPhone", txtGroomCellPhone.Text));



            #region 分页页码
            int startIndex = DegreePager.StartRecordIndex;
            int resourceCount = 0;



            var query = objDegreeOfSatisfactionBLL.GetByWhereParameter(ObjParameterList, "PartyDate", DegreePager.PageSize, DegreePager.CurrentPageIndex, out resourceCount);
            DegreePager.RecordCount = resourceCount;
            rptDegree.DataSource = query;
            rptDegree.DataBind();


            #endregion
        }
        #endregion

        /// <summary>
        /// 分页触发效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DegreePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 查询效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCustomerQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 横向保存
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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