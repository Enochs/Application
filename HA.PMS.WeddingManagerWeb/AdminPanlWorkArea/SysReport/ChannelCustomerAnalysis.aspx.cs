using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport
{
    public partial class ChannelCustomerAnalysis : SystemPage
    {
        Report ObjReportBLL = new Report();
        SaleSources objSaleSourcesBLL = new SaleSources();

        DateTime Star = DateTime.Now.AddDays(-DateTime.Now.DayOfYear);
        DateTime End = DateTime.Now.AddDays(1);


        ChannelType ObjChannelTypeBLL = new ChannelType();

        string[] time = new string[2];


        #region 渠道名称实体类

        //数据保存类
        public class DataClass
        {
            //渠道名称
            public string SourceName { get; set; }

            //客源量
            public int CustomerCount { get; set; }

            /// <summary>
            /// 入客量
            /// </summary>
            public int ComeCount { get; set; }

            /// <summary>
            /// 转换率
            /// </summary>
            public string TurnoveRate { get; set; }

            //预订量
            public int AdvanceCount { get; set; }

            //预定总金额
            public decimal FinisMoneySum { get; set; }

            //预定平均消费金额
            public decimal FinishAvgMoney { get; set; }

            //完工量
            public int FinishCount { get; set; }

            //完工额
            public decimal FinishSumMoney { get; set; }

            //渠道费用
            public decimal SourceSumMoney { get; set; }

            public decimal Cost { get; set; }

            //成交率
            public string DealRate { get; set; }

            //到店成交率
            public string ComeDealRate { get; set; }

            //毛利率
            public string Gross { get; set; }

            public decimal TurnoveRateCount { get; set; }

            public decimal ComeDealRateCount { get; set; }

            public decimal GrossCount { get; set; }
        }
        #endregion

        #region 渠道类型实体类

        //数据保存类
        public class ChannelTypeDataClass
        {

            public int ChannelTypeID { get; set; }

            //渠道名称
            public string SourceName { get; set; }

            //客源量
            public int CustomerCount { get; set; }

            /// <summary>
            /// 入客量
            /// </summary>
            public int ComeCount { get; set; }

            /// <summary>
            /// 转换率
            /// </summary>
            public string TurnoveRate { get; set; }

            //预订量
            public int AdvanceCount { get; set; }

            //预定总金额
            public decimal FinisMoneySum { get; set; }

            //预定平均消费金额
            public decimal FinishAvgMoney { get; set; }

            //完工量
            public int FinishCount { get; set; }

            //完工额
            public decimal FinishSumMoney { get; set; }

            //渠道费用
            public decimal SourceSumMoney { get; set; }

            public decimal Cost { get; set; }

            //成交率
            public string DealRate { get; set; }

            //到店成交率
            public string ComeDealRate { get; set; }

            //毛利率
            public string Gross { get; set; }

            public decimal TurnoveRateCount { get; set; }

            public decimal ComeDealRateCount { get; set; }

            public decimal GrossCount { get; set; }
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary> 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
                BinderDataType();

            }
        }
        #endregion

        #region 数据获取


        /// <summary>
        /// 获取渠道下的客源量 
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomerSumByChannel(string ChannelName)
        {
            return ObjReportBLL.GetCustomerSumByChannel(ChannelName, time[0].ToDateTime(), time[1].ToDateTime());
        }


        /// <summary>
        /// 获取渠道下的到店客源量 
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomerComeSumByChannel(string ChannelName)
        {
            return ObjReportBLL.GetCustomerComeSumByChannel(ChannelName, time[0].ToDateTime(), time[1].ToDateTime());
        }


        /// <summary>
        /// 获取渠道下的预定量
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomAdvance(string ChannelName)
        {
            return ObjReportBLL.GetCustomAdvance(ChannelName, time[0].ToDateTime(), time[1].ToDateTime());
        }


        /// <summary>
        /// 获取渠道下的预定总金额  销售额
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public decimal GetCustomFinisMoneySum(string ChannelName)
        {
            return ObjReportBLL.GetCustomFinisMoneySum(ChannelName, time[0].ToDateTime(), time[1].ToDateTime());
        }

        /// <summary>
        /// 获取完工量/执行量
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomFinish(string ChannelName)
        {
            return ObjReportBLL.GetCustomFinish(ChannelName, time[0].ToDateTime(), time[1].ToDateTime());
        }
        #endregion

        List<DataClass> DataList = new List<DataClass>();
        List<ChannelTypeDataClass> TypeDataList = new List<ChannelTypeDataClass>();

        #region 数据绑定
        /// <summary>
        /// 根据查询条件绑定渠道
        /// </summary>
        private void BinderData(int ChannelType = 0, Repeater repDetails = null)
        {
            if (DateRanger.IsNotBothEmpty)
            {
                time = DateRanger.StartoEnd.Split(',');
            }
            else
            {
                time[0] = Star.ToString();
                time[1] = End.ToString();
            }
            Star = time[0].ToDateTime();
            End = time[1].ToDateTime();

            if (ddlChannelType.SelectedValue.ToInt32() > 0 || ChannelType > 0)
            {
                List<FD_SaleSources> List = new List<FD_SaleSources>();
                if (ChannelType > 0)
                {
                    List = objSaleSourcesBLL.GetByType(ChannelType);
                }
                else
                {
                    List = objSaleSourcesBLL.GetByType(ddlChannelType.SelectedValue.ToInt32());
                }

                foreach (var item in List)
                {
                    var ObjDataItem = new DataClass();
                    ObjDataItem.SourceName = item.Sourcename;           //渠道名称 (酒店)
                    ObjDataItem.CustomerCount = GetCustomerSumByChannel(item.Sourcename);       //客源量
                    ObjDataItem.ComeCount = GetCustomerComeSumByChannel(item.Sourcename);       //入客量
                    ObjDataItem.AdvanceCount = GetCustomAdvance(item.Sourcename);       //签到量
                    if (ObjDataItem.CustomerCount > 0)
                    {   //转换率
                        ObjDataItem.TurnoveRate = ((ObjDataItem.ComeCount.ToString().ToDecimal()) / (ObjDataItem.CustomerCount.ToString().ToDecimal())).ToString("0.00%");
                        ObjDataItem.DealRate = (ObjDataItem.AdvanceCount.ToString().ToDecimal() / ObjDataItem.CustomerCount.ToString().ToDecimal()).ToString("0.00%");   //成交率
                        ObjDataItem.TurnoveRateCount = ((ObjDataItem.ComeCount.ToString().ToDecimal()) / (ObjDataItem.CustomerCount.ToString().ToDecimal())).ToString("0.00").ToDecimal();

                    }
                    else
                    {
                        ObjDataItem.TurnoveRate = "0.00%";
                    }

                    if (ObjDataItem.ComeCount > 0)
                    {
                        ObjDataItem.ComeDealRate = (ObjDataItem.AdvanceCount.ToString().ToDecimal() / ObjDataItem.ComeCount.ToString().ToDecimal()).ToString("0.00%");   //到店成交率
                        ObjDataItem.ComeDealRateCount = (ObjDataItem.AdvanceCount.ToString().ToDecimal() / ObjDataItem.ComeCount.ToString().ToDecimal()).ToString("0.00").ToDecimal();   //到店
                    }
                    else
                    {
                        ObjDataItem.ComeDealRate = "0.00%";
                    }
                    //ObjDataItem.FinisMoneySum = GetCustomFinisMoneySum(item.Sourcename);        //销售额
                    ObjDataItem.FinishCount = GetCustomFinish(item.Sourcename);     //执行量
                    ObjDataItem.FinishSumMoney = ObjReportBLL.GetCustomFinishSumMoney(item.Sourcename, Star, End);      //执行额
                    if (ObjDataItem.FinishCount > 0)
                    {//平均消费金额
                        ObjDataItem.FinishAvgMoney = (ObjDataItem.FinishSumMoney / ObjDataItem.FinishCount).ToString("0.00").ToDecimal();
                    }
                    else
                    {
                        ObjDataItem.FinishAvgMoney = "0.00".ToDecimal();
                    }
                    ObjDataItem.Cost = ObjReportBLL.GetCostByStratOrEnd(item.Sourcename, DateTime.Now.Year, time[0].ToDateTime(), time[1].ToDateTime());     //总成本


                    if (ObjDataItem.FinishSumMoney > 0)
                    {   //毛利率
                        ObjDataItem.Gross = (ObjDataItem.Cost / ObjDataItem.FinishSumMoney).ToString("0.00%");
                        ObjDataItem.GrossCount = (ObjDataItem.Cost / ObjDataItem.FinishSumMoney).ToString("0.00").ToDecimal();
                    }
                    else
                    {
                        ObjDataItem.Gross = "0.00%";
                    }
                    ObjDataItem.SourceSumMoney = ObjReportBLL.GetSourceSumMoney(item.SourceID, Star, End);      //渠道费用

                    DataList.Add(ObjDataItem);

                }
            }
            else
            {

                var List = objSaleSourcesBLL.GetByAll();
                List<FD_SaleSources> SourceList = new List<FD_SaleSources>();
                List.Add(new FD_SaleSources() { Sourcename = "手动录入", SourceID = 59 });
                List.Add(new FD_SaleSources() { Sourcename = "自己收集", SourceID = 60 });


                foreach (var item in List)
                {
                    var ObjDataItem = new DataClass();
                    ObjDataItem.SourceName = item.Sourcename;           //渠道名称 (酒店)
                    ObjDataItem.CustomerCount = GetCustomerSumByChannel(item.Sourcename);       //客源量
                    ObjDataItem.ComeCount = GetCustomerComeSumByChannel(item.Sourcename);       //入客量
                    ObjDataItem.AdvanceCount = GetCustomAdvance(item.Sourcename);       //签到量
                    if (ObjDataItem.CustomerCount > 0)
                    {   //转换率
                        ObjDataItem.TurnoveRate = ((ObjDataItem.ComeCount.ToString().ToDecimal()) / (ObjDataItem.CustomerCount.ToString().ToDecimal())).ToString("0.00%");
                        ObjDataItem.DealRate = (ObjDataItem.AdvanceCount.ToString().ToDecimal() / ObjDataItem.CustomerCount.ToString().ToDecimal()).ToString("0.00%");   //成交率
                        ObjDataItem.TurnoveRateCount = ((ObjDataItem.ComeCount.ToString().ToDecimal()) / (ObjDataItem.CustomerCount.ToString().ToDecimal())).ToString("0.00").ToDecimal();
                    }
                    else
                    {
                        ObjDataItem.TurnoveRate = "0.00%";
                    }

                    if (ObjDataItem.ComeCount > 0)
                    {
                        ObjDataItem.ComeDealRate = (ObjDataItem.AdvanceCount.ToString().ToDecimal() / ObjDataItem.ComeCount.ToString().ToDecimal()).ToString("0.00%");   //到店成交率
                        ObjDataItem.ComeDealRateCount = (ObjDataItem.AdvanceCount.ToString().ToDecimal() / ObjDataItem.ComeCount.ToString().ToDecimal()).ToString("0.00").ToDecimal();
                    }
                    else
                    {
                        ObjDataItem.ComeDealRate = "0.00%";
                    }
                    //ObjDataItem.FinisMoneySum = GetCustomFinisMoneySum(item.Sourcename);        //销售额
                    ObjDataItem.FinishCount = GetCustomFinish(item.Sourcename);     //执行量
                    ObjDataItem.FinishSumMoney = ObjReportBLL.GetCustomFinishSumMoney(item.Sourcename, Star, End);      //执行额
                    if (ObjDataItem.FinishCount > 0)
                    {//平均消费金额
                        ObjDataItem.FinishAvgMoney = (ObjDataItem.FinishSumMoney / ObjDataItem.FinishCount).ToString("0.00").ToDecimal();
                    }
                    else
                    {
                        ObjDataItem.FinishAvgMoney = "0.00".ToDecimal();
                    }
                    ObjDataItem.Cost = ObjReportBLL.GetCostByStratOrEnd(item.Sourcename, DateTime.Now.Year, time[0].ToDateTime(), time[1].ToDateTime());     //总成本


                    if (ObjDataItem.FinishSumMoney > 0)
                    {   //毛利率
                        ObjDataItem.Gross = (ObjDataItem.Cost / ObjDataItem.FinishSumMoney).ToString("0.00%");
                        ObjDataItem.GrossCount = (ObjDataItem.Cost / ObjDataItem.FinishSumMoney).ToString("0.00").ToDecimal();
                    }
                    else
                    {
                        ObjDataItem.Gross = "0.00%";
                    }
                    ObjDataItem.SourceSumMoney = ObjReportBLL.GetSourceSumMoney(item.SourceID, Star, End);      //渠道费用

                    DataList.Add(ObjDataItem);
                }
            }

            if (ChannelType > 0 || HideValues.Value == "1")        //选择渠道类型之后 显示  
            {
                for (int i = 0; i < repChannelList.Items.Count; i++)
                {
                    var ObjItem = repChannelList.Items[i];
                    Repeater repeater = ObjItem.FindControl("repDetaislList") as Repeater;

                    repeater.DataSource = null;
                    repeater.DataBind();

                }

                if (repDetails != null)
                {
                    if (repDetails.DataSource == null)
                    {
                        repDetails.DataBind(DataList.OrderByDescending(C => C.CustomerCount));
                    }
                    else
                    {
                        repDetails.DataSource = null;
                        repDetails.DataBind();
                    }
                }
                else            //点击查询之后  显示渠道名称
                {
                    if (ddlChannelType.SelectedValue.ToInt32() >= 1)
                    {
                        repDetails = repChannelList.Items[ddlChannelType.SelectedValue.ToInt32() - 1].FindControl("repDetaislList") as Repeater;
                        if (repDetails != null)
                        {
                            if (repDetails.DataSource == null)
                            {
                                repDetails.DataBind(DataList.OrderByDescending(C => C.CustomerCount));
                            }
                            else
                            {
                                repDetails.DataSource = null;
                                repDetails.DataBind();
                            }
                        }
                    }
                }

            }
            else            //默认显示所有渠道  根据下拉款选择之后的显示
            {
                this.repList.DataBind(DataList.OrderByDescending(C => C.CustomerCount));
                GetTotalSum(DataList);
            }

        }
        #endregion

        #region 渠道类型数据绑定
        /// <summary>
        /// 渠道类型绑定
        /// </summary>
        public void BinderDataType()
        {
            if (DateRanger.IsNotBothEmpty)
            {
                time = DateRanger.StartoEnd.Split(',');
            }
            else
            {
                time[0] = Star.ToString();
                time[1] = End.ToString();
            }
            Star = time[0].ToDateTime();
            End = time[1].ToDateTime();

            var DataList = ObjChannelTypeBLL.GetByAll();
            foreach (var item in DataList)
            {
                var ObjItem = new ChannelTypeDataClass();
                ObjItem.ChannelTypeID = item.ChannelTypeId;
                ObjItem.SourceName = item.ChannelTypeName;                                      //渠道类型
                ObjItem.CustomerCount = ObjReportBLL.GetCustomerSumByChannelType(item.ChannelTypeId, Star, End);    //客源量
                ObjItem.ComeCount = ObjReportBLL.GetCustomerComeSumByChannelType(item.ChannelTypeId, Star, End);    //入客量
                ObjItem.AdvanceCount = ObjReportBLL.GetCustomAdvanceType(item.ChannelTypeId, Star, End);            //签单量

                if (ObjItem.CustomerCount > 0)
                {   //转换率
                    ObjItem.TurnoveRate = ((ObjItem.ComeCount.ToString().ToDecimal()) / (ObjItem.CustomerCount.ToString().ToDecimal())).ToString("0.00%");
                    ObjItem.DealRate = (ObjItem.AdvanceCount.ToString().ToDecimal() / ObjItem.CustomerCount.ToString().ToDecimal()).ToString("0.00%");   //成交率
                    ObjItem.TurnoveRateCount = ((ObjItem.ComeCount.ToString().ToDecimal()) / (ObjItem.CustomerCount.ToString().ToDecimal())).ToString("0.00").ToDecimal();

                }
                else
                {
                    ObjItem.TurnoveRate = "0.00%";
                }

                if (ObjItem.ComeCount > 0)
                {
                    ObjItem.ComeDealRate = (ObjItem.AdvanceCount.ToString().ToDecimal() / ObjItem.ComeCount.ToString().ToDecimal()).ToString("0.00%");   //到店成交率
                    ObjItem.ComeDealRateCount = (ObjItem.AdvanceCount.ToString().ToDecimal() / ObjItem.ComeCount.ToString().ToDecimal()).ToString("0.00").ToDecimal();   //到店
                }
                else
                {
                    ObjItem.ComeDealRate = "0.00%";
                }
                ObjItem.FinishCount = ObjReportBLL.GetCustomFinishType(item.ChannelTypeId, Star, End);     //执行量
                ObjItem.FinishSumMoney = ObjReportBLL.GetCustomFinishSumMoneyType(item.ChannelTypeId, Star, End);      //执行额
                if (ObjItem.FinishCount > 0)
                {//平均消费金额
                    ObjItem.FinishAvgMoney = (ObjItem.FinishSumMoney / ObjItem.FinishCount).ToString("0.00").ToDecimal();
                }
                else
                {
                    ObjItem.FinishAvgMoney = "0.00".ToDecimal();
                }
                ObjItem.Cost = ObjReportBLL.GetCostByStratOrEndType(item.ChannelTypeId, DateTime.Now.Year, time[0].ToDateTime(), time[1].ToDateTime());     //总成本
                if (ObjItem.FinishSumMoney > 0)
                {   //毛利率
                    ObjItem.Gross = (ObjItem.Cost / ObjItem.FinishSumMoney).ToString("0.00%");
                    ObjItem.GrossCount = (ObjItem.Cost / ObjItem.FinishSumMoney).ToString("0.00").ToDecimal();
                }
                else
                {
                    ObjItem.Gross = "0.00%";
                }
                ObjItem.SourceSumMoney = ObjReportBLL.GetSourceSumMoneyType(item.ChannelTypeId, Star, End);      //渠道费用


                TypeDataList.Add(ObjItem);
            }
            repChannelList.DataBind(TypeDataList);
            GetTotalSumByType(TypeDataList);
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderDataType();
            BinderData();

        }
        #endregion

        #region 选择排序方式
        /// <summary>
        /// 排序
        /// </summary>    
        protected void ddlSortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击标头排序
        /// <summary>
        /// 排序
        /// </summary>
        protected void lbtnSourceName_Click(object sender, EventArgs e)
        {
            BinderData();
            LinkButton lbtnSource = (sender as LinkButton);
            DataClass dc = new DataClass();
            switch (lbtnSource.CommandName)
            {
                case "Sourcename":
                    DataList = DataList.OrderByDescending(C => C.SourceName).ToList();
                    break;
                case "CustomerCount":
                    DataList = DataList.OrderByDescending(C => C.CustomerCount).ToList();
                    break;
                case "ComeCount":
                    DataList = DataList.OrderByDescending(C => C.ComeCount).ToList();
                    break;
                case "TurnoveRate":
                    DataList = DataList.OrderByDescending(C => C.TurnoveRateCount).ToList();
                    break;
                case "AdvanceCount":
                    DataList = DataList.OrderByDescending(C => C.AdvanceCount).ToList();
                    break;
                case "ComeDealRate":
                    DataList = DataList.OrderByDescending(C => C.ComeDealRateCount).ToList();
                    break;
                case "FinisMoneySum":
                    DataList = DataList.OrderByDescending(C => C.FinisMoneySum).ToList();
                    break;
                case "FinishCount":
                    DataList = DataList.OrderByDescending(C => C.FinishCount).ToList();
                    break;
                case "FinishSumMoney":
                    DataList = DataList.OrderByDescending(C => C.FinishSumMoney).ToList();
                    break;
                case "FinishAvgMoney":
                    DataList = DataList.OrderByDescending(C => C.FinishAvgMoney).ToList();
                    break;
                case "Cost":
                    DataList = DataList.OrderByDescending(C => C.Cost).ToList();
                    break;
                case "Gross":
                    DataList = DataList.OrderByDescending(C => C.GrossCount).ToList();
                    break;
                case "SourceSumMoney":
                    DataList = DataList.OrderByDescending(C => C.SourceSumMoney).ToList();
                    break;
            }

            //if (HideValues.Value == "1")
            //{
            //    Repeater repDetails = repChannelList.Items[HideValues.Value.ToInt32() - 1].FindControl("repDetaislList") as Repeater;
            //    repDetails.DataBind(DataList);
            //}
            //else
            //{
            this.repList.DataBind(DataList);
            //}
        }
        #endregion

        #region 合计
        /// <summary>
        /// 合计
        /// </summary>
        /// <param name="DataList"></param>

        public void GetTotalSum(object DataList)
        {
            var ObjDataList = (DataList as List<DataClass>);
            lblCustomerCountSum.Text = ObjDataList.Sum(C => C.CustomerCount).ToString();    //客源量

            lblComeCountSum.Text = ObjDataList.Sum(C => C.ComeCount).ToString();        //入客量
            if (lblCustomerCountSum.Text.ToDecimal() > 0)
            {
                lblTurnoveRateSum.Text = (ObjDataList.Sum(C => C.ComeCount).ToString().ToDecimal() / ObjDataList.Sum(C => C.CustomerCount).ToString().ToDecimal()).ToString("0.00%");               //转换率
            }
            else
            {
                lblTurnoveRateSum.Text = "0.00%";
            }

            lblAdvanceCountSum.Text = ObjDataList.Sum(C => C.AdvanceCount).ToString();      //签单量
            if (lblComeCountSum.Text.ToDecimal() > 0)
            {
                lblComeDealRateSum.Text = (ObjDataList.Sum(C => C.AdvanceCount).ToString().ToDecimal() / ObjDataList.Sum(C => C.ComeCount).ToString().ToDecimal()).ToString("0.00%");       //成交率
            }
            else
            {
                lblComeDealRateSum.Text = "0.00%";
            }
            lblFinishCountSum.Text = ObjDataList.Sum(C => C.FinishCount).ToString();        //执行量
            lblFinishSumMoneySum.Text = ObjDataList.Sum(C => C.FinishSumMoney).ToString();  //执行额
            if (lblFinishCountSum.Text.ToDecimal() > 0)     //平均消费
            {
                lblFinishAvgMoneySum.Text = (ObjDataList.Sum(C => C.FinishSumMoney).ToString().ToDecimal() / ObjDataList.Sum(C => C.FinishCount).ToString().ToDecimal()).ToString("f2");     //平均消费
            }
            else
            {
                lblFinishAvgMoneySum.Text = "0.00";
            }
            lblCostSum.Text = ObjDataList.Sum(C => C.Cost).ToString();      //总成本
            if (lblFinishSumMoneySum.Text.ToDecimal() > 0)
            {
                lblGrossSum.Text = (ObjDataList.Sum(C => C.Cost).ToString().ToDecimal() / ObjDataList.Sum(C => C.FinishSumMoney).ToString().ToDecimal()).ToString("0.00%");       //毛利率
            }
            else
            {
                lblGrossSum.Text = "0.00%";
            }
            lblSourceSumMoneySum.Text = ObjDataList.Sum(C => C.SourceSumMoney).ToString();  //渠道费用

        }
        #endregion

        #region 类型 获取合计
        /// <summary>
        /// 获取合计
        /// </summary>
        public void GetTotalSumByType(object DataList)
        {
            var ObjDataList = (DataList as List<ChannelTypeDataClass>);
            lblCustomerCounSum.Text = ObjDataList.Sum(C => C.CustomerCount).ToString();    //客源量

            lblComeSum.Text = ObjDataList.Sum(C => C.ComeCount).ToString();        //入客量
            if (lblCustomerCounSum.Text.ToDecimal() > 0)
            {
                lblTurnRateSum.Text = (ObjDataList.Sum(C => C.ComeCount).ToString().ToDecimal() / ObjDataList.Sum(C => C.CustomerCount).ToString().ToDecimal()).ToString("0.00%");               //转换率
            }
            else
            {
                lblTurnRateSum.Text = "0.00%";
            }

            lblAdvanceSum.Text = ObjDataList.Sum(C => C.AdvanceCount).ToString();      //签单量
            if (lblComeSum.Text.ToDecimal() > 0)
            {
                lblComeRateSum.Text = (ObjDataList.Sum(C => C.AdvanceCount).ToString().ToDecimal() / ObjDataList.Sum(C => C.ComeCount).ToString().ToDecimal()).ToString("0.00%");       //成交率
            }
            else
            {
                lblComeRateSum.Text = "0.00%";
            }
            lblFinishSum.Text = ObjDataList.Sum(C => C.FinishCount).ToString();        //执行量
            lblFinishMoneySum.Text = ObjDataList.Sum(C => C.FinishSumMoney).ToString();  //执行额
            if (lblFinishSum.Text.ToDecimal() > 0)     //平均消费
            {
                lblFinishAvgSum.Text = (ObjDataList.Sum(C => C.FinishSumMoney).ToString().ToDecimal() / ObjDataList.Sum(C => C.FinishCount).ToString().ToDecimal()).ToString("f2");     //平均消费
            }
            else
            {
                lblFinishAvgSum.Text = "0.00";
            }
            lblCostCountSum.Text = ObjDataList.Sum(C => C.Cost).ToString();      //总成本
            if (lblFinishMoneySum.Text.ToDecimal() > 0)
            {
                lblGrossCountSum.Text = (ObjDataList.Sum(C => C.Cost).ToString().ToDecimal() / ObjDataList.Sum(C => C.FinishSumMoney).ToString().ToDecimal()).ToString("0.00%");       //毛利率
            }
            else
            {
                lblGrossCountSum.Text = "0.00%";
            }
            lblSourceMoneySum.Text = ObjDataList.Sum(C => C.SourceSumMoney).ToString();  //渠道费用
        }
        #endregion

        #region 点击导出
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnExport_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/ChannelCustomerAnalysis.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();
            BinderData();
            int index = 0;
            foreach (var ObjDataItem in DataList)
            {
                index++;
                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + index + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SourceName + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.CustomerCount + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ComeCount + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.TurnoveRate + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.AdvanceCount + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ComeDealRate + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.FinishCount + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.FinishSumMoney + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.FinishAvgMoney + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Cost + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Gross + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SourceSumMoney + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");
            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }
        #endregion

        #region 外层绑定 详细显示
        /// <summary>
        /// 选择类型之后  显示详细的渠道
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repCahnnelList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                int ChannelTypeID = e.CommandArgument.ToString().ToInt32();
                Repeater rptDetails = e.Item.FindControl("repDetaislList") as Repeater;
                //HideValues.Value = ChannelTypeID.ToString();
                HideValues.Value = "1";
                BinderData(ChannelTypeID, rptDetails);
            }
        }
        #endregion

    }
}