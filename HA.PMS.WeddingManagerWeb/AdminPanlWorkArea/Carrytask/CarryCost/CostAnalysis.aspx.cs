using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class CostAnalysis : SystemPage
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
        /// 成本操作
        /// </summary>
        CostSum ObjCostSumBLL = new CostSum();

        /// <summary>
        /// 
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();

        /// <summary>
        /// 获取各类的价格
        /// </summary>
        QuotedPriceForType ObjForTypeBLL = new QuotedPriceForType();


        /// <summary>
        /// 获取产品的价格
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();


        /// <summary>
        /// 派工
        /// </summary>
        ProductforDispatching ObjProductForDispatchingBLL = new ProductforDispatching();


        Cost ObjCostBLL = new Cost();
        DateTime Start = DateTime.MinValue;
        DateTime End = DateTime.MaxValue;
        int SourceCount = 0;
        string OrderByName = "PartyDate";

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 获取成本 订单金额
        /// <summary>
        /// 1 成本 2 订单金额
        /// </summary>
        /// <param name="CustomerIDs"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetMoney(object CustomerIDs, int Type)
        {
            int CustomerID = CustomerIDs.ToString().ToInt32();
            if (Type == 1)              //成本
            {
                var CostSum = ObjCostSumBLL.GetByCustomerID(CustomerID);
                if (CostSum != null)
                {
                    string sum = CostSum.Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString("f2");
                    return sum;
                }
                else
                {
                    return "0";
                }
            }
            else if (Type == 2)             //销售价
            {
                var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
                if (Model != null)
                {
                    return Model.FinishAmount.ToString();
                }
            }
            else if (Type == 3)         //利润/成本  利润率
            {
                if (GetMoney(CustomerIDs, 2).ToDecimal() > 0)
                {
                    if (GetMoney(CustomerIDs, 2).ToDecimal() != 0)
                    {
                        return ((GetMoney(CustomerIDs, 2).ToDecimal() - GetMoney(CustomerIDs, 1).ToDecimal()) / GetMoney(CustomerIDs, 2).ToDecimal()).ToString("0.00%");
                    }
                    else
                    {
                        return "0.00%";
                    }
                }
                else
                {
                    return "0.00%";
                }
            }
            return "0";
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            Repeater1.Visible = false;
            Repeater1.DataSource = ObjQuotedPriceBLL.GetByAll().Take(1).ToList();
            Repeater1.DataBind();
        }
        #endregion

        #region 分页
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
        /// 查询
        /// </summary>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 获取各类ID
        /// <summary>
        /// 1 ：订单 OrderID  2：派工 DispathingID
        /// </summary>
        public string GetByCusomerId(object Source, int Type)
        {
            int CustomerID = Source.ToString().ToInt32();
            Dispatching ObjDispatchingBLL = new Dispatching();
            FL_Dispatching DispatchingModel = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            if (DispatchingModel != null)
            {
                if (Type == 1)
                {
                    return DispatchingModel.OrderID.ToString();
                }
                else if (Type == 2)
                {
                    return DispatchingModel.DispatchingID.ToString();
                }
            }
            return "0";

        }
        #endregion

        #region 获取本期合计
        public void GetAllSum(List<PMSParameters> ObjParList)
        {
            string Rate = "";
            //List<View_SSCustomer> DataLists = ObjCustomerBLL.GetByWhereParameter(ObjParList, "PartyDate", 10000, 1, out SourceCount);
            decimal AllOrderSum = ObjCustomerBLL.GetOrderSum(Start, End).ToDecimal();
            decimal AllCostSum = ObjCustomerBLL.GetCostSum(Start, End).ToDecimal();
            if (AllOrderSum != 0)
            {
                Rate = ((AllOrderSum - AllCostSum) / AllOrderSum).ToString("0.00%");
            }
            else
            {
                Rate = "0.00%";
            }

            lblOrderSum.Text = AllOrderSum.ToString("f2");
            lblCostSum.Text = AllCostSum.ToString("f2");
            lblRaeSum.Text = Rate;

            //销售价
            lblSumPersonSale.Text = ObjQuotedPriceItemsBLL.GetSaleFinishAmount(Start, End, 1);          //1.人员
            lblSumMaterialSale.Text = ObjQuotedPriceItemsBLL.GetSaleFinishAmount(Start, End, 2);       //2.物料
            lblSumOtherSale.Text = ObjQuotedPriceItemsBLL.GetSaleFinishAmount(Start, End, 3);           //3.其他


            //成本价
            lblSumPersonCost.Text = ObjCostSumBLL.GetCostSum(Start, End, 1);        //1.人员
            lblSumMaterialCost.Text = ObjCostSumBLL.GetCostSum(Start, End, 2);      //2.物料
            lblSumOtherCost.Text = ObjCostSumBLL.GetCostSum(Start, End, 3) == "" ? "0.00" : ObjCostSumBLL.GetCostSum(Start, End, 3);         //3.其他


            //毛利率
            if (lblSumPersonSale.Text.ToString().ToDecimal() > 0)               //人员
            {
                lblSumPersonRate.Text = ((lblSumPersonSale.Text.ToString().ToDecimal() - lblSumPersonCost.Text.ToString().ToDecimal()) / lblSumPersonSale.Text.ToString().ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblSumPersonRate.Text = "0.00%";
            }


            if (lblSumMaterialSale.Text.ToString().ToDecimal() > 0)               //物料
            {
                lblSumMaterialRate.Text = ((lblSumMaterialSale.Text.ToString().ToDecimal() - lblSumMaterialCost.Text.ToString().ToDecimal()) / lblSumMaterialSale.Text.ToString().ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblSumMaterialRate.Text = "0.00%";
            }

            if (lblSumOtherSale.Text.ToString().ToDecimal() > 0)               //其他
            {
                lblSumOtherRate.Text = ((lblSumOtherSale.Text.ToString().ToDecimal() - lblSumOtherCost.Text.ToString().ToDecimal()) / lblSumOtherSale.Text.ToString().ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblSumOtherRate.Text = "0.00%";
            }


        }
        #endregion

        decimal PageOrderSum = 0;       //订单金额 / 销售额
        decimal PageCostSum = 0;        //成本
        decimal PageRateSum = 0;        //利润率

        decimal PagePersonSale = 0;         //人员销售
        decimal PagePersonCost = 0;         //人员成本

        decimal PageMaterialSale = 0;         //物料销售
        decimal PageMaterialCost = 0;         //物料成本

        decimal PageOtherSale = 0;         //其它销售
        decimal PageOtherCost = 0;         //其它成本

        #region 获取本页合计
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ObjItem = (View_SSCustomer)e.Item.DataItem;
            int CustomerID = ObjItem.CustomerID;

            if (CustomerID != 0)
            {
                //合计
                PageOrderSum += GetMoney(CustomerID, 2).ToDecimal();          //订单金额 / 销售额
                PageCostSum += GetMoney(CustomerID, 1).ToDecimal();          //成本 
                PageRateSum += GetMoney(CustomerID, 3).ToDecimal();          //利润率 
                lblPageOrderSum.Text = PageOrderSum.ToString("f2");
                lblPageCostSum.Text = PageCostSum.ToString("f2");

                if (lblPageOrderSum.Text.ToDecimal() != 0)
                {
                    lblPageRaeSum.Text = ((lblPageOrderSum.Text.ToDecimal() - lblPageCostSum.Text.ToDecimal()) / lblPageOrderSum.Text.ToDecimal()).ToString().ToDecimal().ToString("0.00%");
                }
                else
                {
                    lblPageRaeSum.Text = "0.00%";
                }

                //人员
                PagePersonSale += GetFinishAmount(CustomerID, 1).ToDecimal();       //人员售价
                PagePersonCost += GetCostAmounts(CustomerID, 1).ToDecimal();       //人员成本
                lblPagePersonSale.Text = PagePersonSale.ToString("f2");
                lblPagePersonCost.Text = PagePersonCost.ToString("f2");
                if (lblPagePersonSale.Text.ToDecimal() > 0)              //利润率
                {
                    lblPagePersonRate.Text = ((lblPagePersonSale.Text.ToDecimal() - lblPagePersonCost.Text.ToDecimal()) / lblPagePersonSale.Text.ToDecimal()).ToString().ToDecimal().ToString("0.00%");
                }
                else
                {
                    lblPagePersonRate.Text = "0.00%";
                }

                //物料
                PageMaterialSale += GetFinishAmount(CustomerID, 2).ToDecimal();       //物料售价
                PageMaterialCost += GetCostAmounts(CustomerID, 2).ToDecimal();       //物料成本
                lblPageMaterialSale.Text = PageMaterialSale.ToString("f2");
                lblPageMaterialCost.Text = PageMaterialCost.ToString("f2");
                if (lblPageMaterialSale.Text.ToDecimal() > 0)              //利润率
                {
                    lblPageMaterialRate.Text = ((lblPageMaterialSale.Text.ToDecimal() - lblPageMaterialCost.Text.ToDecimal()) / lblPageMaterialSale.Text.ToDecimal()).ToString().ToDecimal().ToString("0.00%");
                }
                else
                {
                    lblPageMaterialRate.Text = "0.00%";
                }

                //其他
                PageOtherSale += GetFinishAmount(CustomerID, 3).ToDecimal();       //其他售价
                PageOtherCost += GetCostAmounts(CustomerID, 3).ToDecimal();       //其他成本
                lblPageOtherSale.Text = PageOtherSale.ToString("f2");
                lblPageOtherCost.Text = PageOtherCost.ToString("f2");
                if (lblPageOtherSale.Text.ToDecimal() > 0)              //利润率
                {
                    lblPageOtherRate.Text = ((lblPageOtherSale.Text.ToDecimal() - lblPageOtherCost.Text.ToDecimal()) / lblPageOtherSale.Text.ToDecimal()).ToString().ToDecimal().ToString("0.00%");
                }
                else
                {
                    lblPageOtherRate.Text = "0.00%";
                }

                //会员标志
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
            }
        }
        #endregion

        #region 外层进行 数据绑定
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater RptData = e.Item.FindControl("repCustomer") as Repeater;

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<PMSParameters> ObjParList = new List<PMSParameters>();
            //按婚期查询
            if (PartyDateRanger.IsNotBothEmpty)
            {
                ObjParList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
                Start = PartyDateRanger.StartoEnd.Split(',')[0].ToString().ToDateTime();
                End = PartyDateRanger.StartoEnd.Split(',')[1].ToString().ToDateTime();
            }

            //按新人姓名查询
            CstmNameSelector.AppandTo(ObjParList);

            //按联系电话查询
            ObjParList.Add(txtContactPhone.Text != string.Empty, "BrideCellPhone", txtContactPhone.Text.Trim().ToString(), NSqlTypes.LIKE);

            //按策划师名字查询
            ObjParList.Add(MyManager.SelectedValue.ToInt32() > 0, "QuotedEmployee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);

            //ObjParList.Add("EvalState", 1, NSqlTypes.Equal);
            ObjParList.Add("State", 206, NSqlTypes.Equal);


            var DataList = ObjCustomerBLL.GetByWhereParameter(ObjParList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
            if (DataList.Count == 0)
            {
                lblPageOrderSum.Text = "0.00";
                lblPageCostSum.Text = "0.00";
                lblPageRaeSum.Text = "0.00%";
            }

            GetAllSum(ObjParList);

            #region 查询的以前的成本表
            //ObjParList.Add("IsLock", "", NSqlTypes.IsNotNull);

            //var DataList = ObjCostBLL.GetDataByWhereParameter(ObjParList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            #endregion
        }
        #endregion

        #region 导出Excel

        protected void btnExport_Click(object sender, EventArgs e)
        {
            List<PMSParameters> ObjParmList = new List<PMSParameters>();
            var ObjAllDataList = ObjCustomerBLL.GetByWhereParameter(GetWhere(ObjParmList), OrderByName, 10000, 1, out SourceCount);
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                var ObjDataItem = Repeater1.Items;
                Repeater RptData = ObjDataItem[i].FindControl("repCustomer") as Repeater;
                RptData.DataBind(ObjAllDataList);
            }

            Repeater1.Visible = true;

            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=订单成本明细" + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.Repeater1.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region 条件 GetWhere

        public List<PMSParameters> GetWhere(List<PMSParameters> Pars)
        {
            //按婚期查询
            if (PartyDateRanger.IsNotBothEmpty)
            {
                Pars.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
                Start = PartyDateRanger.StartoEnd.Split(',')[0].ToString().ToDateTime();
                End = PartyDateRanger.StartoEnd.Split(',')[1].ToString().ToDateTime();
            }

            //按新人姓名查询
            CstmNameSelector.AppandTo(Pars);

            //按联系电话查询
            Pars.Add(txtContactPhone.Text != string.Empty, "BrideCellPhone", txtContactPhone.Text.Trim().ToString(), NSqlTypes.LIKE);

            Pars.Add("State", 206, NSqlTypes.Equal);
            return Pars;
        }
        #endregion

        #region 填写成本明细/查看成本明细
        /// <summary>
        /// 填写还是查看
        /// </summary> 
        public string GetIsLock(object Source, int Type)
        {
            int CustomerID = Source.ToString().ToInt32();
            var OrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (OrderModel == null)
            {
                //var CustomerModel = ObjCustomerBLL.GetByID(CustomerID);
                //CustomerModel.State = 5;
                //ObjCustomerBLL.Update(CustomerModel);
                //BinderData();
            }
            else
            {
                FL_Cost CostModel = ObjCostBLL.GetByOrderID(OrderModel.OrderID);

                if (CostModel != null)
                {
                    if (CostModel.IsLock == true && Type == 1)
                    {
                        return "style='display:none;'";
                    }

                    if (CostModel.IsLock == false && Type == 2)
                    {
                        return "style='display:none;'";
                    }
                }
            }
            return "";
        }
        #endregion

        #region 获取派工ID(DispatchingID)
        /// <summary>
        /// 获取派工ID
        /// </summary>
        public string GetDispatchingID(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                return Model.DispatchingID.ToString();
            }
            return "0";
        }
        #endregion

        #region 获取人员 物料 其他的销售价
        /// <summary>
        /// Type类型 1.人员  2.物料 3.其他
        /// </summary>
        public string GetFinishAmount(object Source, object TypeSource)
        {
            decimal FinishAmount = 0;


            int Type = TypeSource.ToString().ToInt32();         //1.物料    2.物料     3.其他

            //if (Source == null)
            //{
            //    FinishAmount = ObjForTypeBLL.GetSalePriceForType(Start, End, Type).ToDecimal();
            //}
            //else
            //{
            int CustomerID = Source.ToString().ToInt32();
            FinishAmount = ObjForTypeBLL.GetSalePriceForType(Start, End, Type, CustomerID).ToDecimal();
            //}
            return FinishAmount.ToString("f2");
        }
        #endregion

        #region 获取成本价
        /// <summary>
        /// 成本价
        /// </summary>
        public string GetCostAmounts(object Source, object TypeSource)
        {
            decimal CostAmount = 0;

            int CustomerID = Source.ToString().ToInt32();
            var DataList = ObjCostSumBLL.GetByCustomerID(CustomerID);

            int Type = TypeSource.ToString().ToInt32();
            if (Type == 1)                  //人员
            {
                CostAmount = DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
            }
            else if (Type == 2)             //物料
            {
                CostAmount = DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 8 || C.RowType == 10).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
            }
            else if (Type == 3)             //其他
            {
                CostAmount = DataList.Where(C => C.RowType == 9 || C.RowType == 11).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
            }
            return CostAmount.ToString("f2");

        }
        #endregion

        #region 获取毛利率
        /// <summary>
        /// 成本价
        /// </summary>
        public string GetProfitRate(object Source, object TypeSource)
        {
            decimal FinishAmount = 0;           //初始化销售价
            decimal CostAmount = 0;             //初始化成本价

            FinishAmount = GetFinishAmount(Source, TypeSource).ToDecimal();      //销售价
            CostAmount = GetCostAmounts(Source, TypeSource).ToDecimal();         //成本价

            decimal ProfitAmount = (FinishAmount - CostAmount).ToString().ToDecimal();      //利润
            if (FinishAmount != 0)              //根据TypeSource自动变化转化率
            {
                string Profit = (ProfitAmount / FinishAmount).ToString("0.00%");
                return Profit;
            }
            return "0.00%";
        }
        #endregion

    }

}