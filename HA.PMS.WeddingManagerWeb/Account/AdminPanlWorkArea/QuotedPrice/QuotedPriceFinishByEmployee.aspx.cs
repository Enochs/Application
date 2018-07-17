using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using System.Linq;
using System.Data.Objects;
using System.IO;
using System.Text;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceFinishByEmployee : SystemPage
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
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 成本
        /// </summary>
        CostSum ObjCostSumBLL = new CostSum();


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Dispatching ObjDispatchingBLL = new Dispatching();

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
                //DataKChartBinder();
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
            return ObjOrderBLL.GetByID(OrderID.ToString().ToInt32()).EarnestMoney.ToString();
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

        decimal AllFinishAmount = 0;

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();


            objParmList.Add("FinishOver", true, NSqlTypes.Equal);       //已完成的   当前时间 大于婚期
            objParmList.Add("IsDelete", false, NSqlTypes.Equal);        //未删除的客户    false  未删除
            objParmList.Add("State", 206, NSqlTypes.Equal);             //206表示 已完成  

            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                if (!(ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32())))
                {
                    this.MyManager.GetEmployeePar(objParmList, "QuotedEmployee");   //婚礼策划
                }
            }
            else if (MyManager.SelectedValue.ToInt32() > 0)
            {
                this.MyManager.GetEmployeePar(objParmList, "QuotedEmployee");   //婚礼策划
            }



            //按新人姓名查询
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtContactMan.Text.Trim(), NSqlTypes.LIKE);
            }
            //按联系电话查询
            objParmList.Add(txtContactPhone.Text != string.Empty, "ContactPhone", txtContactPhone.Text.Trim(), NSqlTypes.StringEquals);

            //按婚期查询
            if (PartyDateRanger.IsNotBothEmpty)
            {
                objParmList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            objParmList.Add("PartyDate", DateTime.Now.ToShortDateString().ToDateTime(), NSqlTypes.LessThan);
            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);      //为0 只显示一次  否则会有重复的数据

            //按状态查询
            if (ddlState.SelectedValue.ToInt32() == 1)          //未评价
            {
                objParmList.Add("EvalState", "isNulls", NSqlTypes.IsNull);
                objParmList.Add("SacState", "isNulls", NSqlTypes.OrInts);
            }
            else if (ddlState.SelectedValue.ToInt32() == 2)    //已评价 
            {
                objParmList.Add("EvalState", 1, NSqlTypes.Equal);
                objParmList.Add("SacState", 2, NSqlTypes.Equal);
            }

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<View_CustomerEvaulation> DataList = ObjQuotedPriceBLL.GetByCustomerParameters(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);      //最后评价修改时间排序
            repCustomer.DataBind(DataList);
            CtrPageIndex.RecordCount = SourceCount;
            var AllList = ObjQuotedPriceBLL.GetByCustomerParameters(objParmList, "PartyDate", 10000, CtrPageIndex.CurrentPageIndex, out SourceCount);      //最后评价修改时间排序
            lblAllFinishAmount.Text = AllList.Sum(C => C.FinishAmount).ToString();
        }
        #endregion

        #region 获取已付款
        /// <summary>
        /// 获取已付款
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetQuotedDispatchingFinishMoney(object OrderID)
        {
            #region 注释


            //HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney ObjOrderEarnestMoneyBLL = new HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney();
            ////预付款
            //var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            //if (QuotedModel == null)
            //{
            //    //decimal EarnestMoney = 0;
            //    //FinishAmount += EarnestMoney;
            //    //return FinishAmount.ToString();
            //    return "0";
            //}
            //else
            //{
            //    decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
            //    FinishAmount += EarnestMoney;

            //获得收款计划的东西


            //    //定金
            //    var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            //    if (ObjEorder != null)
            //    {
            //        if (ObjEorder.EarnestMoney.HasValue)
            //        {
            //            FinishAmount += ObjEorder.EarnestMoney.Value;
            //        }
            //    }


            //}

            #endregion

            if (OrderID != null)
            {
                HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
                decimal FinishAmount = 0;
                //var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
                var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID.ToString().ToInt32());

                foreach (var Objitem in ObjList)
                {
                    FinishAmount += Objitem.RealityAmount.Value;
                }
                return FinishAmount.ToString();
            }
            else
            {
                return "0";
            }

        }
        #endregion

        #region 分页 上一页/下一页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary> 
        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();

        }
        #endregion

        #region 获取毛利率(订单金额-成本/订单金额)
        /// <summary>
        /// 获取毛利率
        /// </summary>
        public string GetRates(object Source, object Source1)
        {
            if (Source != null && Source1 != null)
            {
                decimal FinishAmount = Source1.ToString().ToDecimal();
                int CustomerID = Source.ToString().ToInt32();
                var DataList = ObjCostSumBLL.GetByCustomerID(CustomerID);
                decimal Cost = 0;
                foreach (var item in DataList)
                {
                    Cost += item.ActualSumTotal.ToString().ToDecimal();
                }

                string Rates = ((FinishAmount - Cost) / FinishAmount).ToString().ToDecimal().ToString("0.00%");     //毛利率
                return Rates;
            }
            return "";
        }
        #endregion

        decimal FinishAmount = 0;       //订单金额
        #region 绑定完成事件  获取本页的订单金额
        /// <summary>
        /// 完成事件
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            View_CustomerEvaulation Model = e.Item.DataItem as View_CustomerEvaulation;
            FinishAmount += Model.FinishAmount.ToString().ToDecimal();
            lblPageFinishAmount.Text = FinishAmount.ToString();
        }
        #endregion

    }
}