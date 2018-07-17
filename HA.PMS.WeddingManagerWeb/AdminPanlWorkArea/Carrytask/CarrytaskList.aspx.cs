using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using System.Text;
using HA.PMS.DataAssmblly;
using System.IO;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskList : SystemPage
    {
        /// <summary>1
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        CostSum ObjCostSumBLL = new CostSum();

        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        /// <summary>
        /// 派工操作
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();


        //查询成本与利润
        Cost ObjCostBLL = new Cost();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Employee ObjEmployeeBLL = new Employee();


        /// <summary>
        /// 满意度调查
        /// </summary>
        DegreeOfSatisfaction ObjDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();

        string ColumnName = "PartyDate";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary> 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //System.Web.UI.WebControls.ListItem listItem = ddlChooseYear.Items.FindByText(string.Concat(DateTime.Now.Year, "年"));
                //if (listItem != null)
                //{
                //    ddlChooseYear.ClearSelection();
                //    listItem.Selected = true;
                //}

                ddlHotel.ClearSelection();
                ddlHotel.Items.FindByValue("0").Selected = true;
                BindData(sender, e);

                DepartmentDropdownList1.BinderByQuoted();
            }
        }
        #endregion

        #region 页面查询
        /// <summary>
        /// 获取订单总额
        /// </summary>
        /// <returns></returns>
        public string GetFinishAmount(object QuotedID)
        {
            DataAssmblly.FL_QuotedPrice fL_QuotedPrice = new BLLAssmblly.Flow.QuotedPrice().GetByID(QuotedID.ToString().ToInt32());
            return (!object.ReferenceEquals(fL_QuotedPrice, null) && fL_QuotedPrice.FinishAmount.HasValue) ? fL_QuotedPrice.FinishAmount.Value.ToString() : "暂未填写";
        }



        public string GetQuotedidByCustomerID(object CustomerID)
        {
            DataAssmblly.FL_QuotedPrice fL_QuotedPrice = new BLLAssmblly.Flow.QuotedPrice().GetByCustomerID(CustomerID.To<Int32>(0));
            return object.ReferenceEquals(fL_QuotedPrice, null) ? string.Empty : fL_QuotedPrice.QuotedID.ToString();
        }

        /// <summary>
        /// 获取利润率
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetProfitMargin(object OrderID)
        {
            DataAssmblly.FL_Cost fL_Cost = new Cost().GetByOrderID(OrderID.To<Int32>(0));
            return object.ReferenceEquals(fL_Cost, null) ? "财务暂未填写" : fL_Cost.ProfitMargin + string.Empty;
        }


        /// <summary>
        /// 获取总体满意度
        /// </summary>
        /// <returns></returns>
        public string GetSumDof(object CustomerID)
        {
            DataAssmblly.CS_DegreeOfSatisfaction cS_DegreeOfSatisfaction = new DegreeOfSatisfaction().GetByCustomerID(CustomerID.To<Int32>(0));
            return object.ReferenceEquals(cS_DegreeOfSatisfaction, null) ? string.Empty : cS_DegreeOfSatisfaction.SumDof;
        }


        /// <summary>
        /// 获取余款数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetOverFinishMoney(int CustomerID)
        {
            OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
            /// <summary>
            /// 收款计划
            /// </summary>
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
            decimal AllMoney = QuotedModel.FinishAmount.Value;
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);
            decimal FinishAmount = 0;
            FinishAmount += EarnestMoney;
            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }
            var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            if (ObjEorder != null)
            {
                if (ObjEorder.EarnestMoney != null)
                {
                    FinishAmount += ObjEorder.EarnestMoney.Value;
                }
            }
            //if (EarnestMoney != null)
            //{
            //    FinishAmount += EarnestMoney.ToString().ToDecimal();
            //}

            //if (AllMoney != null)
            //{

            return (AllMoney.ToString().ToDecimal() - FinishAmount).ToString();
            //}
            //else
            //{
            //    return "";
            //}
        }

        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        protected void BindData(object sender, EventArgs e)
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
            objParmList.Add("IsDelete", false, NSqlTypes.Bit);

            //按新人姓名查询
            CstmNameSelector.AppandTo(objParmList);


            //按酒店查询
            objParmList.Add(ddlHotel.SelectedItem.Value.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);

            //按婚期查询
            if (QueryDateRanger.IsNotBothEmpty)
            {
                if (ddltype.SelectedValue.ToInt32() == 1)       //婚期
                {
                    objParmList.Add("PartyDate", QueryDateRanger.StartoEnd, NSqlTypes.DateBetween);
                    ColumnName = "PartyDate";
                }
                else if (ddltype.SelectedValue.ToInt32() == 2)          //订单日期
                {
                    objParmList.Add("QuotedCreateDate", QueryDateRanger.StartoEnd, NSqlTypes.DateBetween);
                    ColumnName = "QuotedCreateDate";
                }
            }


            //按渠道名称查询 
            if (ddlChannelType1.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("ChannelType", ddlChannelType1.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //按渠道名称查询 
            if (ddlChannelName2.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Channel", ddlChannelName2.SelectedItem.Text, NSqlTypes.StringEquals);
            }


            //员工类型
            if (ddlEmployeeType.SelectedValue != "-1")
            {
                if (MyManager.SelectedValue.ToInt32() > 0)
                {
                    objParmList.Add(ddlEmployeeType.SelectedValue, MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
                }
            }
            else
            {

                if (DepartmentDropdownList1.SelectedItem.Value.ToInt32() > 0)
                {
                    string keys = "";
                    Department ObjDepartmentBLL = new Department();
                    var DataLists = ObjEmployeeBLL.GetMyManagerEmpLoyee(ObjDepartmentBLL.GetByID(DepartmentDropdownList1.SelectedValue.ToInt32()).DepartmentManager);
                    int index = 0;
                    foreach (var item in DataLists)
                    {
                        index += 1;
                        if (index == DataLists.Count)
                        {
                            keys += item.EmployeeID;
                        }
                        else
                        {
                            keys += item.EmployeeID + ",";
                        }
                    }
                    if (ddlEmployeeType.SelectedValue != "-1")
                    {
                        objParmList.Add(ddlEmployeeType.SelectedValue, keys, NSqlTypes.IN);
                    }
                    else
                    {
                        objParmList.Add("OrderEmployee", keys, NSqlTypes.IN, true);
                    }

                }
            }

            #region 金额区间

            if (ddlStartPayMent.SelectedValue.ToInt32() >= 1 && ddlEndPayMent.SelectedValue.ToInt32() >= 1)
            {
                objParmList.Add("FinishAmount", ddlStartPayMent.SelectedItem.Text + "," + ddlEndPayMent.SelectedItem.Text, NSqlTypes.NumBetween);
            }
            else if (ddlStartPayMent.SelectedValue.ToInt32() >= 1 && ddlEndPayMent.SelectedValue.ToInt32() == -1)
            {
                objParmList.Add("FinishAmount", ddlStartPayMent.SelectedItem.Text, NSqlTypes.Greaterthan);
            }
            else if (ddlStartPayMent.SelectedValue.ToInt32() == -1 && ddlEndPayMent.SelectedValue.ToInt32() >= 1)
            {
                objParmList.Add("FinishAmount", ddlEndPayMent.SelectedItem.Text, NSqlTypes.NumLessThan);
            }
            #endregion
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, ColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            List<View_CustomerQuoted> AllQuotedDataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, ColumnName, 10000, 1, out SourceCount);
            lblTotalSums.Text = "客户数量:" + SourceCount.ToString();
            repCustomer.DataBind(DataList);
            CtrPageIndex.RecordCount = SourceCount;
            DataKChartBinder(AllQuotedDataList);

            GetAllMoneySum(AllQuotedDataList);
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
            ddlChannelName2.Width = 134;
        }
        #endregion

        #region 汇总统计
        /// <summary>
        /// 绘制K线图
        /// </summary>
        protected void DataKChartBinder(List<View_CustomerQuoted> DataList)
        {
            int ThanIndex = 0;
            int CarryIndex = 0;
            int LessIndex = 0;
            int NotIndex = 0;
            int NoIndex = 0;

            string[] date = new string[2];

            #region 汇总统计
            if (QueryDateRanger.IsNotBothEmpty)
            {
                if (ddltype.SelectedValue.ToInt32() == 1)
                {
                    string time = QueryDateRanger.StartoEnd;
                    date = time.Split(',');
                }
                else if (ddltype.SelectedValue.ToInt32() == 2)
                {
                    string time = QueryDateRanger.StartoEnd;
                    date = time.Split(',');
                }
            }
            else
            {
                date[0] = DateTime.MinValue.ToShortDateString();
                date[1] = DateTime.MaxValue.ToShortDateString();
            }

            DateTime chooseDateStar = date[0].ToDateTime();
            DateTime chooseDateEnd = date[1].ToDateTime();

            string EmpoyeeType = ddlEmployeeType.SelectedValue;
            int EmployeeID = MyManager.SelectedValue.ToInt32();


            lblOrderCount.Text = DataList.Count.ToString();             //当期执行订单数(订单数量)
            ltlCurrentExcuteOrderCount.Text = DataList.Sum(C => C.FinishAmount).ToString();         //执行订单总金额
            lblFinishAmount.Text = ObjQuotedCollectionsPlanBLL.GetAllFinishAmount(DataList).ToString();         //已收款
            if (lblOrderCount.Text.ToInt32() > 0)                //平均消费
            {
                lblAvgCost.Text = (ltlCurrentExcuteOrderCount.Text.ToDecimal() / lblOrderCount.Text.ToDecimal()).ToString("f2");
            }
            if (lblFinishAmount.Text.ToDecimal() > 0)             //当期毛利率
            {
                ltlCurrentExcuteApp.Text = ((lblFinishAmount.Text.ToDecimal() - ObjDispatchingBLL.GetTotalCostforDispatching(chooseDateStar, chooseDateEnd, EmpoyeeType, EmployeeID).ToDecimal()) / lblFinishAmount.Text.ToDecimal()).ToString("0.00%");
            }

            lblTotalCost.Text = ObjCostSumBLL.GetAllCostSum(DataList).ToDecimal().ToString();               //总成本
            //满意度
            List<View_GetDefrreSatisaction> queryDegree = objDegreeOfSatisfactionBLL.GetAllByDate(chooseDateStar, chooseDateEnd, EmpoyeeType, EmployeeID);
            int SourceCount = DataList.Count;
            foreach (var item in DataList)
            {
                var Model = ObjDegreeOfSatisfactionBLL.GetByCustomerID(item.CustomerID);
                if (Model != null)
                {
                    if (Model.SumDof == "超越期望值")
                    {
                        ThanIndex++;
                    }
                    else if (Model.SumDof == "达到期望值")
                    {
                        CarryIndex++;
                    }
                    else if (Model.SumDof == "未达到期望值")
                    {
                        LessIndex++;
                    }
                    else if (Model.SumDof == null || Model.SumDof == "")
                    {
                        NotIndex++;
                    }
                }
                else if (Model == null)
                {
                    NoIndex++;
                }
            }
            int OverTask = ThanIndex;       //超越期望值
            int CarryTask = CarryIndex;     //达到期望值
            int CarryNotTask = LessIndex;   //未达到期望值
            int NotEvalution = NotIndex;     //未评价
            int NoCustomer = NoIndex;

            int OverCarryTask = 0;
            if (queryDegree != null)
            {
                OverCarryTask = queryDegree.Where(C => C.SumDof == "超越期望值" || C.SumDof == "达到期望值").ToList().Count;
            }

            if (SourceCount > 0)
            {
                if (queryDegree != null)
                {
                    lblSumSatisfaction.Text = (OverCarryTask.ToString().ToDecimal() / SourceCount.ToString().ToDecimal()).ToString("0.00%") + "(" + OverCarryTask.ToString() + "/" + SourceCount + ")";
                }
                lblOverSatisfaction.Text = (OverTask.ToString().ToDecimal() / SourceCount.ToString().ToDecimal()).ToString("0.00%") + "(" + OverTask.ToString() + "/" + SourceCount + ")";
                lblCarrySataisfaction.Text = (CarryTask.ToString().ToDecimal() / SourceCount.ToString().ToDecimal()).ToString("0.00%") + "(" + CarryTask.ToString() + "/" + SourceCount + ")";
                lblNotCarrySatisfacion.Text = (CarryNotTask.ToString().ToDecimal() / SourceCount.ToString().ToDecimal()).ToString("0.00%") + "(" + CarryNotTask.ToString() + "/" + SourceCount + ")";
                lblNotEvaulation.Text = (NotEvalution.ToString().ToDecimal() / SourceCount.ToString().ToDecimal()).ToString("0.00%") + "(" + NotEvalution.ToString() + "/" + SourceCount + ")";
                lblNoCustomer.Text = (NoCustomer.ToString().ToDecimal() / SourceCount.ToString().ToDecimal()).ToString("0.00%") + "(" + NoCustomer.ToString() + "/" + SourceCount + ")";
            }
            #endregion
        }
        #endregion

        #region 获取满意度评分
        public void GetSatisfaciton(object Source, int ThanIndex)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjDegreeOfSatisfactionBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                ThanIndex++;
            }
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportoExcel_Click(object sender, EventArgs e)
        {
            using (StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/CarrytaskSumModel.xml")))
            {
                string ObjTempletContent = Objreader.ReadToEnd();
                ObjTempletContent = ObjTempletContent.Replace("<=1ByNow>", lblOrderCount.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=2ByNow>", ltlCurrentExcuteOrderCount.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=3ByNow>", lblAvgCost.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=4ByNow>", lblTotalCost.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=5ByNow>", ltlCurrentExcuteApp.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=6ByNow>", lblSumSatisfaction.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=7ByNow>", lblOverSatisfaction.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=8ByNow>", lblCarrySataisfaction.Text);
                ObjTempletContent = ObjTempletContent.Replace("<=9ByNow>", lblNotCarrySatisfacion.Text);
                IOTools.DownLoadByString(ObjTempletContent, "xls");
            }
        }
        #endregion

        #region 获取总成本
        /// <summary>
        /// 获取客户的总成本
        /// </summary>
        /// <param name="CustomerID"></param>
        public string GetCostByCustomerID(object source)
        {
            CostSum ObjCostSumBLL = new CostSum();
            if (source != null && source.ToString().ToInt32() != 0)
            {
                int CustomerID = source.ToString().ToInt32();
                var CostSumModel = ObjCostSumBLL.GetByCustomerID(CustomerID).ToList();
                string TotalSum = CostSumModel.Sum(C => C.Sumtotal).ToString().ToInt32().ToString("f2");
                return TotalSum;
            }
            return "0.00";
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
            decimal FinishAmount = 0;
            //decimal EarnestMoney = 0;
            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            //if (QuotedModel.EarnestMoney != null)
            //{
            //    EarnestMoney = QuotedModel.EarnestMoney.Value;
            //}

            //FinishAmount += EarnestMoney;

            //获得收款计划的东西
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);

            foreach (var Objitem in ObjList)
            {
                if (Objitem.RealityAmount != null)
                {
                    FinishAmount += Objitem.RealityAmount.Value;
                }
            }

            ////定金
            //var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            //if (ObjEorder != null)
            //{
            //    if (ObjEorder.EarnestMoney.HasValue && ObjEorder.EarnestMoney != null)
            //    {
            //        FinishAmount += ObjEorder.EarnestMoney.Value;
            //    }
            //}
            #endregion

            return FinishAmount.ToString();

        }
        #endregion

        #region 获取毛利率
        /// <summary>
        /// 毛利率
        /// </summary>
        public string GetRates(object source, object source1)
        {
            if (source != null && source.ToString().ToInt32() != 0 && source1 != null)
            {
                decimal TotalCost = GetCostSum(source).ToDecimal();        //总成本   Source  CustomerID
                decimal SalePrice = source1.ToString().ToDecimal();                  //销售额  
                if (SalePrice > 0)
                {
                    string rates = ((SalePrice - TotalCost) / SalePrice).ToString("0.00%");
                    return rates;
                }
            }
            return "0.00%";
        }
        #endregion

        #region 获取DispatchingID
        public int GetDispachingIDByCustomerID(object Source)
        {
            Dispatching ObjDispatchingBLL = new Dispatching();
            int CustomerID = Source.ToString().ToInt32();
            FL_Dispatching ObjDisModel = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            if (ObjDisModel != null)
            {
                return ObjDisModel.DispatchingID;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 获取成本
        /// <summary>
        /// 获取成本
        /// </summary>
        /// <returns></returns>
        public string GetCostSum(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var DataList = ObjCostSumBLL.GetByCustomerID(CustomerID);
            return DataList.Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString("f2");
        }
        #endregion

        decimal PaggFinishMoney = 0;
        decimal PageFinishAmount = 0;
        decimal PageCostSum = 0;

        #region 绑定完成事件   本页合计  会员标记
        protected void repCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            View_CustomerQuoted QuotedItem = (View_CustomerQuoted)e.Item.DataItem;
            PaggFinishMoney += GetQuotedDispatchingFinishMoney(QuotedItem.CustomerID).ToString().ToDecimal();
            PageFinishAmount += QuotedItem.FinishAmount.ToString().ToDecimal();
            PageCostSum += GetCostSum(QuotedItem.CustomerID).ToString().ToDecimal();

            lblPageFinishMoney.Text = PaggFinishMoney.ToString();
            lblPageFinishAmount.Text = PageFinishAmount.ToString();
            lblPageCostSum.Text = PageCostSum.ToString();


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

        decimal AllFinishMoney = 0;             //已收款
        decimal AllFinishAmount = 0;            //订单金额
        decimal AllCostSum = 0;                 //成本

        #region 获取本期合计
        public void GetAllMoneySum(List<View_CustomerQuoted> DataList)
        {
            AllFinishMoney = ObjQuotedCollectionsPlanBLL.GetAllFinishAmount(DataList).ToDecimal();
            AllFinishAmount = DataList.Sum(C => C.FinishAmount).ToString().ToDecimal();
            AllCostSum = ObjCostSumBLL.GetAllCostSum(DataList).ToDecimal();

            lblSumFinishMoney.Text = AllFinishMoney.ToString();
            lblSumFinishAmount.Text = AllFinishAmount.ToString();
            lblSumCostSum.Text = AllCostSum.ToString();
        }

        #endregion
    }
}