/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.11
 Description:未开始界面
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



    public partial class CS_DegreeOfSatisfactionNotStar : SystemPage
    {
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Order objOrderBLL = new Order();
        InvestigateState objInvestigateState = new InvestigateState();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        HA.PMS.BLLAssmblly.Flow.Invite ObjInvite = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomerBLL = new Customers();

        Order ObjOrderBLL = new BLLAssmblly.Flow.Order();

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSatisfaction();
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DataBinder()
        {

            //写入参数
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();
            ObjParameterList.Add("CdState", 0);
            ObjParameterList.Add("State", 206 + "," + 208 + "," + 24, NSqlTypes.IN);
            ObjParameterList.Add("IsDelete", false, NSqlTypes.Bit);
            //ObjParameterList.Add("FinishOver", true, NSqlTypes.Bit);
            //ObjParameterList.Add("ParentQuotedID", 0, NSqlTypes.Equal);


            //参数构造 按新人姓名查询
            if (!string.IsNullOrEmpty(txtBride.Text))
            {
                ObjParameterList.Add("Bride", txtBride.Text + ",,string", NSqlTypes.Split);
                ObjParameterList.Add("Groom", txtBride.Text + ",1,string", NSqlTypes.Split);
            }

            //按联系电话查询
            if (!string.IsNullOrEmpty(txtBrideCellPhone.Text))
            {
                ObjParameterList.Add("BrideCellPhone", txtBrideCellPhone.Text, NSqlTypes.StringEquals);
                ObjParameterList.Add("GroomCellPhone", txtBrideCellPhone.Text, NSqlTypes.OR);
            }
            //按婚期查询
            ObjParameterList.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

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

        #region 获取预定时间
        /// <summary>
        /// 获取预定时间
        /// </summary>
        public string GetOrderDate(object CustomerID)
        {
            var CustoerModel = ObjOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32());
            if (CustoerModel == null)
            {
                return "";
            }
            else
            {
                if (CustoerModel.LastFollowDate == null || CustoerModel.LastFollowDate.ToString() == "")
                {
                    return "";
                }
                else
                {
                    return CustoerModel.LastFollowDate.ToString();
                }
            }
        }
        #endregion

        #region 获取取件时间
        /// <summary>
        /// 获取取件时间
        /// </summary>
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
        #endregion

        #region 获取已付款
        /// <summary>
        /// 获取已付款
        /// </summary>
        public string GetQuotedDispatchingFinishMoney(object CustomerID)
        {
            HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney ObjOrderEarnestMoneyBLL = new HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal FinishAmount = 0;

            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            if (QuotedModel == null)
            {
                //decimal EarnestMoney = 0;
                //FinishAmount += EarnestMoney;
                //return FinishAmount.ToString();
                return "0";
            }
            else
            {
                decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
                FinishAmount += EarnestMoney;

                //获得收款计划的东西
                var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);

                foreach (var Objitem in ObjList)
                {
                    FinishAmount += Objitem.RealityAmount.Value;
                }

                //定金
                var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
                if (ObjEorder != null)
                {
                    if (ObjEorder.EarnestMoney.HasValue)
                    {
                        FinishAmount += ObjEorder.EarnestMoney.Value;
                    }
                }
                return FinishAmount.ToString();

            }
        }
        #endregion

        #region 分页触发时间
        /// <summary>
        /// 分页触发时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DegreePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 点击查询事件
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCustomerQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 绑定做事件
        /// <summary>
        /// 横向保存事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptDegree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int customerID = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName == "SaveChange")
            {
                if (((TextBox)e.Item.FindControl("txtDate")).Text != string.Empty)
                {
                    var ObjUpdateModel = objDegreeOfSatisfactionBLL.GetByCustomerID(customerID);

                    ObjUpdateModel.State = 1;

                    ObjUpdateModel.PlanDate = ((TextBox)e.Item.FindControl("txtDate")).Text.ToDateTime();
                    objDegreeOfSatisfactionBLL.Update(ObjUpdateModel);
                    DataBinder();
                }
                JavaScriptTools.AlertWindow("保存成功", this.Page);

            }
        }
        #endregion

        #region 满意度调查 (处理重复的订单)
        public void BindSatisfaction()
        {

            //添加满意度调查
            var DataList = ObjCustomerBLL.GetByAll().Where(C => C.State == 2016 || C.State == 208 || C.State == 24);
            foreach (var item in DataList)
            {
                var ObjDegreeModel = objDegreeOfSatisfactionBLL.GetByCustomersID(item.CustomerID);
                if (ObjDegreeModel == null)             //未创建满意度调查
                {
                    objDegreeOfSatisfactionBLL.Insert(new CS_DegreeOfSatisfaction()
                    {
                        CustomerID = item.CustomerID,
                        SumDof = "",
                        DofContent = "",
                        DofDate = null,
                        IsDelete = false,
                        DegreeResult = null,
                        State = 0,
                        UpdateTime = DateTime.Now.ToString().ToDateTime(),
                        UpdateEmployeeID = User.Identity.Name.ToInt32(),
                        PlanDate = null,
                    });
                }
            }


            //删除所有重复的满意度调查  (1.只要有评价过 删除为评价的)  2.否则删除所有（一个单子可能会重复）
            var query = objDegreeOfSatisfactionBLL.GetByAll().GroupBy(C => C.CustomerID).ToList();
            for (int i = 0; i < query.Count; i++)
            {
                if (query[i].ToList().Count() >= 2)
                {
                    foreach (var item in query[i])
                    {
                        int index = 1;
                        if (item.State != 2)        // 2.已评价过的 0.是未评价的  删除为评价的
                        {
                            objDegreeOfSatisfactionBLL.Deletes(item);
                            index++;
                        }
                    }
                }
            }



        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员
        /// </summary>  
        protected void rptDegree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion

    }
}