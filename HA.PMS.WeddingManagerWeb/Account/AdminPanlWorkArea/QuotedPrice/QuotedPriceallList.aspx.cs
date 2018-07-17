using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using System.Text;
using System.IO;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceallList : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary>  
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Dispatching ObjDispatchingBLL = new Dispatching();
        string ColumnName = "PartyDate";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ColumnName = ddlSortName.SelectedValue.ToString();
                BinderData();
            }
        }
        #endregion

        #region 页面状态显示


        /// <summary>
        /// 显示更新
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string ShowUpdate(object IsFirstCreate)
        {
            return Convert.ToBoolean(IsFirstCreate) ? string.Empty : "style='display:none'";
        }

        /// <summary>
        /// 隐藏提交审核
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideChecks(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            else
            {
                if (State.ToString().ToInt32() == 3)
                {
                    return string.Empty;

                }
                else
                {
                    return "style='display:none'";
                }
            }
        }

        /// <summary>
        /// 绑定样式
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string SetClass(object QuotedID)
        {

            var ObjQuotedModel = ObjDispatchingBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            if (ObjQuotedModel != null)
            {
                return string.Empty;
            }
            else
            {
                return "RemoveClass";
            }

        }



        /// <summary>
        /// 根据报价单获取执行单ID
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetDispatchingIDByQuotedID(string QuotedID)
        {

            var ObjQuotedModel = ObjDispatchingBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            if (ObjQuotedModel != null)
            {
                return ObjQuotedModel.DispatchingID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetOrderEmoney(object OrderID)
        {
            var ObjOrderModel = ObjOrderBLL.GetByID(OrderID.ToString().ToInt32());
            if (ObjOrderModel != null)
            {
                if (ObjOrderModel.EarnestMoney == null || ObjOrderModel.EarnestMoney.ToString() == "")
                {
                    return "0";
                }
                else
                {
                    return ObjOrderModel.EarnestMoney.ToString();
                }
            }
            return "0";


        }


        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideCreate(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            if (State.ToString() == "False")
            {
                return string.Empty;

            }
            else
            {
                return "style='display:none'";
            }
        }


        #endregion

        #region 数据绑定
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();


            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
            objParmList.Add("IsDelete", false, NSqlTypes.Bit);

            //按新人姓名查询
            objParmList.Add(txtContactMan.Text != "", "ContactMan", txtContactMan.Text.Trim().ToString(), NSqlTypes.LIKE);
            objParmList.Add(txtContactMan.Text != "", "Groom", txtContactMan.Text.Trim().ToString(), NSqlTypes.ORLike);
            objParmList.Add(txtContactMan.Text != "", "Bride", txtContactMan.Text.Trim().ToString(), NSqlTypes.ORLike);

            //按联系电话查询
            objParmList.Add(txtContactPhone.Text != "", "ContactPhone", txtContactPhone.Text.Trim().ToString(), NSqlTypes.StringEquals);
            objParmList.Add(txtContactPhone.Text != "", "BrideCellPhone", txtContactPhone.Text.Trim().ToString(), NSqlTypes.OR);
            objParmList.Add(txtContactPhone.Text != "", "GroomCellPhone", txtContactPhone.Text.Trim().ToString(), NSqlTypes.OR);

            //按婚期 订单日期 签约日期查询
            if (ddlTimeType.SelectedItem.Text != "请选择" && ddlTimeType.SelectedItem.Text != "订单金额")
            {
                objParmList.Add(ddlTimeType.SelectedItem.Text != "请选择", ddlTimeType.SelectedValue.ToString(), DateRangers.StartoEnd, NSqlTypes.DateBetween);
            }
            else if (ddlTimeType.SelectedItem.Text == "订单金额")
            {
                //按订单金额查询
                string StartFinishAmount = txtStartFinishAmount.Text.Trim().ToString();
                string EndFinishAmount = txtEndFinishAmount.Text.Trim().ToString();
                if (txtStartFinishAmount.Text.Trim().ToString() != string.Empty && txtEndFinishAmount.Text.Trim().ToString() != string.Empty)
                {
                    objParmList.Add("FinishAmount", StartFinishAmount + "," + EndFinishAmount, NSqlTypes.NumBetween);
                }
                else if (txtStartFinishAmount.Text.Trim().ToString() != string.Empty && txtEndFinishAmount.Text.Trim().ToString() == string.Empty)
                {
                    objParmList.Add("FinishAmount", StartFinishAmount, NSqlTypes.Greaterthan);
                }
                else if (txtStartFinishAmount.Text.Trim().ToString() == string.Empty && txtEndFinishAmount.Text.Trim().ToString() != string.Empty)
                {
                    objParmList.Add("FinishAmount", EndFinishAmount, NSqlTypes.NumLessThan);
                }
            }

            //按渠道名称查询 
            if (ddlChannelName2.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Channel", ddlChannelName2.SelectedItem.Text, NSqlTypes.StringEquals);
            }

            //按新人的状态查询
            if (ddlCustomersState1.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("State", ddlCustomersState1.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //按各种日期排序
            ColumnName = ddlSortName.SelectedValue.ToString();



            //按酒店查询
            if (ddlHotel.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.StringEquals);
            }
            if (ddlEmployeeType.SelectedItem.Text != "请选择")
            {
                if (MyManager.SelectedValue.ToInt32() > 0)
                {
                    objParmList.Add(ddlEmployeeType.SelectedValue.ToString(), MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
                }
                else
                {
                    objParmList.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
                }
            }



            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, ColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            repCustomer.DataBind(DataList);
            if (DataList.Count == 0)
            {
                lblPageFinishMoney.Text = "0.00";
                lblPageFinishAmount.Text = "0.00";
            }

            CtrPageIndex.RecordCount = SourceCount;
            lblTotalSums.Text = "客户数量:" + SourceCount.ToString();
            GetAllMoneySum(objParmList);
        }
        #endregion

        #region 渠道类型选择
        /// <summary>
        /// 选择
        /// </summary>
        protected void ddlChannelType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelName2.Items.Clear();
            ddlChannelName2.BindByParent(ddlChannelType1.SelectedValue.ToInt32());
        }
        #endregion

        #region 获取已收款
        /// <summary>
        /// 获取已付款
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetQuotedDispatchingFinishMoney(object CustomerID)
        {
            #region 注释

            HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney ObjOrderEarnestMoneyBLL = new HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal FinishAmount = 0;
            decimal EarnestMoney = 0;
            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            //if (QuotedModel.EarnestMoney != null)
            //{
            //    EarnestMoney = QuotedModel.EarnestMoney.Value;
            //}

            //FinishAmount += EarnestMoney;

            //获得收款计划的东西
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);
            FinishAmount += ObjList.Sum(C => C.RealityAmount).ToString().ToDecimal();
            //foreach (var Objitem in ObjList)
            //{
            //    if (Objitem.RealityAmount != null)
            //    {
            //        FinishAmount += Objitem.RealityAmount.Value;
            //    }
            //}

            //定金
            //var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            //if (ObjEorder != null)
            //{
            //    if (ObjEorder.EarnestMoney.HasValue && ObjEorder.EarnestMoney != null)
            //    {
            //        FinishAmount += ObjEorder.EarnestMoney.Value;
            //    }
            //}
            #endregion

            //decimal EarnestMoney = ObjQuotedPriceBLL.GetOrderMondeySum().ToDecimal();    //订金   
            //decimal FirstEarnestMoney = ObjQuotedPriceBLL.GetOrderEarnestMoney().ToDecimal();     //首付款
            //decimal FinishMoney = ObjQuotedPriceBLL.GetFinishMoney().ToDecimal();           //现金流
            //string FinishAmount = (EarnestMoney + FirstEarnestMoney + FinishMoney).ToString();
            return FinishAmount.ToString();

        }
        #endregion

        #region 分页查询
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion


        #region 点击查询
        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion

        decimal PageFinishMoneySum = 0;     //已收款
        decimal PageOrderMoneySum = 0;      //订金
        decimal PageFinishAmountSum = 0;    //订单金额

        #region 数据绑定执行事件  本页合计
        /// <summary>
        /// 绑定执行事件
        /// </summary> 
        protected void repCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            View_CustomerQuoted DataModel = (View_CustomerQuoted)e.Item.DataItem;
            PageFinishMoneySum += GetQuotedDispatchingFinishMoney(DataModel.CustomerID).ToDecimal();        //已收款
            PageOrderMoneySum += GetOrderEmoney(DataModel.OrderID).ToDecimal();         //订金
            PageFinishAmountSum += DataModel.FinishAmount.ToString().ToDecimal();       //订单金额
            lblPageFinishMoney.Text = PageFinishMoneySum.ToString("f2");
            lblPageFinishAmount.Text = PageFinishAmountSum.ToString("f2");
        }
        #endregion

        decimal FinishAmountSum = 0;    //订单金额

        #region 本期合计 方法
        /// <summary>
        /// 计算合计
        /// </summary>
        public void GetAllMoneySum(object objParmList)
        {

            int SourceCounts = 0;
            List<PMSParameters> parmsList = objParmList as List<PMSParameters>;
            parmsList = objParmList as List<PMSParameters>;
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(parmsList, "CreateDate", 100000, 1, out SourceCounts, OrderType.Desc);
            decimal FinishMoney = ObjQuotedPriceBLL.GetFinishAmount(parmsList).ToDecimal();
            FinishAmountSum = DataList.Where(C => C.FinishAmount != null).Sum(C => C.FinishAmount).ToString().ToDecimal();     //订单金额

            lblSumFinishMoney.Text = FinishMoney.ToString("f2");
            lblSumFinishAmount.Text = FinishAmountSum.ToString("f2");
        }
        #endregion

        protected void ddlHotels_TextChanged(object sender, EventArgs e)
        {
            //string text = "测试成功,";
            //string text1 = "比较欣慰！";
            //JavaScriptTools.AlertWindow(text + text1, Page);
        }

    }
}