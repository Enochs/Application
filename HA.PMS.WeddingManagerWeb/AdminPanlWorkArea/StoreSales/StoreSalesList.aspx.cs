using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System.Data.Objects;
using System.Text;
using System.IO;
using System.Linq;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Report;
using System.Web.UI.WebControls;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class StoreSalesList : SystemPage
    {
        BLLAssmblly.Flow.Order orderBLL = new BLLAssmblly.Flow.Order();
        BLLAssmblly.Flow.QuotedPrice quotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        Report objReportBLL = new Report();
        string OrderName = "PartyDate";

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {

            var objParmList = new List<PMSParameters>();



            //根据状态查询
            if (DdlCustomersState1.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("State", DdlCustomersState1.SelectedValue, NSqlTypes.Equal);
            }

            //根据时间查询
            if (ddlDateType.SelectedValue.ToInt32() > 0 && DateRanger.IsNotBothEmpty)
            {
                if (ddlDateType.SelectedValue.ToInt32() == 1)   //到店时间  本应该是ComeDate  但ComeDate 一直没保存过  是之后才加上去的  ProjectDate一直都保存了的
                {
                    objParmList.Add("ComeDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                    OrderName = "ComeDate";
                }
                //else if (ddlDateType.SelectedValue.ToInt32() == 2)  //预订时间
                //{
                //    objParmList.Add("PlanComeDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                //    OrderName = "PlanComeDate";
                //}
                else if (ddlDateType.SelectedValue.ToInt32() == 2)  //订单时间
                {
                    objParmList.Add("OrderCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                    OrderName = "OrderCreateDate";
                }
                else if (ddlDateType.SelectedValue.ToInt32() == 3)      //婚期时间
                {
                    objParmList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                    OrderName = "PartyDate";
                }
            }

            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //责任人
            //objParmList.Add(MyManager.SelectedValue.ToInt32() > 0, "EmployeeID", MyManager.SelectedValue, NSqlTypes.StringEquals);
            MyManager.GetEmployeePar(objParmList);

            //新人姓名
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.LIKE);
            }
            //根据新人状态查询
            if (DdlCustomersState1.SelectedValue.ToInt32() >= 0)
            {
                objParmList.Add("State", DdlCustomersState1.SelectedValue, NSqlTypes.Equal);
            }
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = orderBLL.GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

            #region MyRegion
            //#region 汇总统计->当前

            //var queryCurrent = orderBLL.GetOrderCustomerByIndex(parameters);
            ////总预约量
            //ltlCurrentTotalOrderCount.Text = orderBLL.GetOrderSum(queryCurrent, OrderSumTypes.TotalOrderCount);
            ////实际到店量
            //ltlCurrentActualOrderCount.Text = orderBLL.GetOrderSum(queryCurrent, OrderSumTypes.ActualOrderCount);
            ////成功预订量
            //ltlCurrentSuccessOrderCount.Text = orderBLL.GetOrderSum(queryCurrent, OrderSumTypes.SuccessOrderCount);
            ////定金总额
            //ltlCurrentTotalEarnestMoney.Text = orderBLL.GetOrderSum(queryCurrent, OrderSumTypes.TotalEarnestMoney);
            ////订单总额
            //ltlCurrentTotalFinishAmount.Text = orderBLL.GetOrderSum(queryCurrent, OrderSumTypes.TotalFinishAmount);
            ////流失量
            //ltlCurrentLoseCount.Text = orderBLL.GetOrderSum(queryCurrent, OrderSumTypes.LoseCount);
            ////到店率
            //if (ltlCurrentTotalOrderCount.Text.ToDecimal() > 0)
            //{
            //    //到店率
            //    ltlCurrentActualOrderRate.Text = (ltlCurrentActualOrderCount.Text.ToDecimal() / ltlCurrentTotalOrderCount.Text.ToDecimal()).ToString("0.00%");
            //    //预订率
            //    ltlCurrentSuccessOrderRate.Text = (ltlCurrentSuccessOrderCount.Text.ToDecimal() / ltlCurrentTotalOrderCount.Text.ToDecimal()).ToString("0.00%");
            //    //流失率
            //    ltlCurrentLoseRate.Text = (ltlCurrentLoseCount.Text.ToDecimal() / ltlCurrentTotalOrderCount.Text.ToDecimal()).ToString("0.00%");
            //}
            //#endregion

            //#region 汇总统计->历史

            //List<ObjectParameter> paraHistory = new List<ObjectParameter>();
            //paraHistory = MyManager.GetEmployeePar(paraHistory);
            //paraHistory.Add(new ObjectParameter("IsDelete", false));
            //paraHistory.Add("State_NumGreaterthan", 0);
            //var queryHistory = orderBLL.GetOrderCustomerByIndex(paraHistory);
            ////总预约量
            //ltlHistoryTotalOrderCount.Text = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.TotalOrderCount);
            ////实际到店量
            //ltlHistoryActualOrderCount.Text = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.ActualOrderCount);
            ////成功预订量
            //ltlHistorySuccessOrderCount.Text = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.SuccessOrderCount);
            ////流失量
            //ltlHistoryLoseCount.Text = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.LoseCount);
            ////定金总额
            //ltlHistoryTotalEarnestMoney.Text = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.TotalEarnestMoney);
            ////订单总额
            //ltlHistoryTotalFinishAmount.Text = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.TotalFinishAmount);

            //if (ltlHistoryTotalOrderCount.Text.ToDecimal() > 0)
            //{
            //    //到店率
            //    ltlHistoryActualOrderRate.Text = (ltlHistoryActualOrderCount.Text.ToDecimal() / ltlHistoryTotalOrderCount.Text.ToDecimal()).ToString("0.00%");
            //    //预订率
            //    ltlHistorySuccessOrderRate.Text = (ltlHistorySuccessOrderCount.Text.ToDecimal() / ltlHistoryTotalOrderCount.Text.ToDecimal()).ToString("0.00%");
            //    //流失率
            //    ltlHistoryLoseRate.Text = (ltlHistoryLoseCount.Text.ToDecimal() / ltlHistoryTotalOrderCount.Text.ToDecimal()).ToString("0.00%");
            //}

            //#endregion

            //#region 绘制折线图指标

            //StringBuilder totalOrderCount = new StringBuilder(); //总预约量
            //StringBuilder actualOrderCount = new StringBuilder();//实际到店量
            //StringBuilder successOrderCount = new StringBuilder(); //邀约成功量
            //StringBuilder loseCount = new StringBuilder();  //流失量
            //StringBuilder actualOrderRate = new StringBuilder(); //有效率
            //StringBuilder successOrderRate = new StringBuilder(); //成功率
            //StringBuilder loseRate = new StringBuilder();//流失率
            //StringBuilder totalEarnestMoney = new StringBuilder();//定金总额
            //StringBuilder totalFinishAmount = new StringBuilder();//订单总额 

            //int year = YearSelector.Text.ToInt32();
            //for (int i = 1; i <= 12; i++)
            //{
            //    string tmptotal = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.TotalOrderCount, year, i);
            //    totalOrderCount.Append(string.Concat(tmptotal, ","));

            //    string tmpactual = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.ActualOrderCount, year, i);
            //    actualOrderCount.Append(string.Concat(tmpactual, ","));

            //    string tmpsuccess = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.SuccessOrderCount, year, i);
            //    successOrderCount.Append(string.Concat(tmpsuccess, ","));

            //    string tmplose = orderBLL.GetOrderSum(queryHistory, OrderSumTypes.LoseCount, year, i);
            //    loseCount.Append(string.Concat(tmplose, ","));

            //    totalEarnestMoney.Append(string.Concat(orderBLL.GetOrderSum(queryHistory, OrderSumTypes.TotalEarnestMoney, year, i), ","));
            //    totalFinishAmount.Append(string.Concat(orderBLL.GetOrderSum(queryHistory, OrderSumTypes.TotalFinishAmount, year, i), ","));

            //    if (decimal.Parse(tmptotal) > 0)
            //    {
            //        actualOrderRate.Append(string.Concat((decimal.Divide(decimal.Parse(tmpactual), decimal.Parse(tmptotal)) * 100).ToString("f2"), ","));
            //        successOrderRate.Append(string.Concat((decimal.Divide(decimal.Parse(tmpsuccess), decimal.Parse(tmptotal)) * 100).ToString("f2"), ","));
            //        loseRate.Append(string.Concat((decimal.Divide(decimal.Parse(tmplose), decimal.Parse(tmptotal)) * 100).ToString("f2"), ","));
            //    }
            //    else
            //    {
            //        actualOrderRate.Append("0,");
            //        successOrderRate.Append("0,");
            //        loseRate.Append("0,");
            //    }
            //}

            //ViewState["totalOrderCount"] = totalOrderCount.ToString().Trim(',');//总预约量
            //ViewState["actualOrderCount"] = actualOrderCount.ToString().Trim(',');//实际到店量
            //ViewState["successOrderCount"] = successOrderCount.ToString().Trim(','); //邀约成功量
            //ViewState["loseCount"] = loseCount.ToString().Trim(','); //流失量
            //ViewState["actualOrderRate"] = actualOrderRate.ToString().Trim(',');//有效率
            //ViewState["successOrderRate"] = successOrderRate.ToString().Trim(','); //成功率
            //ViewState["loseRate"] = loseRate.ToString().Trim(','); //流失率
            //ViewState["totalEarnestMoney"] = totalEarnestMoney.ToString().Trim(','); //定金总额
            //ViewState["totalFinishAmount"] = totalFinishAmount.ToString().Trim(','); //订单总额

            //#endregion 
            #endregion
        }
        #endregion

        #region 获取订单金额
        /// <summary>
        /// 获取订单金额
        /// </summary>
        public string GetFinishAmount(object OrderID)
        {
            FL_QuotedPrice fL_QuotedPrice = quotedPriceBLL.GetByOrderId(Convert.ToInt32(OrderID));
            return fL_QuotedPrice != null ? Convert.ToDecimal(fL_QuotedPrice.FinishAmount).ToString("f2") : string.Empty;
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary>   
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData(sender, e);
        }
        #endregion

        #region 点击导出
        /// <summary>
        /// 导出Excel
        /// </summary>
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/OrderSumModel.xml")))
            {
                content = reader.ReadToEnd();
                reader.Close();
                content = content.Replace("<=1ByNow>", ltlCurrentTotalOrderCount.Text);
                content = content.Replace("<=2ByNow>", ltlCurrentActualOrderCount.Text);
                content = content.Replace("<=3ByNow>", ltlCurrentSuccessOrderCount.Text);
                content = content.Replace("<=4ByNow>", ltlCurrentTotalEarnestMoney.Text);
                content = content.Replace("<=5ByNow>", ltlCurrentTotalFinishAmount.Text);
                content = content.Replace("<=6ByNow>", ltlCurrentLoseCount.Text);
                content = content.Replace("<=7ByNow>", ltlCurrentActualOrderRate.Text);
                content = content.Replace("<=8ByNow>", ltlCurrentSuccessOrderRate.Text);
                content = content.Replace("<=9ByNow>", ltlCurrentLoseRate.Text);

                content = content.Replace("<=1ByOther>", ltlHistoryTotalOrderCount.Text);
                content = content.Replace("<=2ByOther>", ltlHistoryActualOrderCount.Text);
                content = content.Replace("<=3ByOther>", ltlHistorySuccessOrderCount.Text);
                content = content.Replace("<=4ByOther>", ltlHistoryTotalEarnestMoney.Text);
                content = content.Replace("<=5ByOther>", ltlHistoryTotalFinishAmount.Text);
                content = content.Replace("<=6ByOther>", ltlHistoryLoseCount.Text);
                content = content.Replace("<=7ByOther>", ltlHistoryActualOrderRate.Text);
                content = content.Replace("<=8ByOther>", ltlHistorySuccessOrderRate.Text);
                content = content.Replace("<=9ByOther>", ltlHistoryLoseRate.Text);
                reader.Dispose();
            }
            IOTools.DownLoadByString(content, "xls");
        }
        #endregion

        #region 获取已收款
        /// <summary>
        /// 获取已收款
        /// </summary>
        public decimal GetRealityAmount(object Source)
        {
            QuotedCollectionsPlan ObjPlanBLL = new QuotedCollectionsPlan();
            int OrderID = Source.ToString().ToInt32();
            var m_plan = ObjPlanBLL.GetOrderDate(OrderID);
            if (m_plan != null)
            {
                return m_plan.RealityAmount.ToString().ToDecimal();
            }
            return 0;
        }
        #endregion

        #region 获取到店时间
        /// <summary>
        /// 获取到店时间
        /// </summary>
        public string GetComeDate(object Source)  //获取到店时间
        {
            if (Source != null)
            {
                int CustomerID = Source.ToString().ToInt32();

                var ComeDate = objReportBLL.GetByCustomerID(CustomerID).OrderCreateDate;

                return ComeDate.ToString();
            }
            return "";
        }
        #endregion

        #region Repeater绑定完成事件
        /// <summary>
        /// 修改订单表到店时间
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            Customers ObjCustomerBLL = new Customers();
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();

            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;

            var CustomerModel = ObjCustomerBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }

            var ComeDate = objReportBLL.GetByCustomerID(CustomerID).OrderCreateDate;
            var Model = orderBLL.GetbyCustomerID(CustomerID);        //SS_Report的到店时间准确  所以修改
            Model.ComeDate = ComeDate;
            orderBLL.Update(Model);

        }
        #endregion
    }
}