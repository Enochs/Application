using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class OrderCostPrint : SystemPage
    {
        /// <summary>
        /// 成本表
        /// </summary>
        CostSum ObjCostSumBLL = new CostSum();


        /// <summary>
        /// 结算表
        /// </summary>
        Statement ObjStatementBLL = new Statement();

        /// <summary>
        /// 保健的详细项目
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        /// <summary>
        /// 各类的统计
        /// </summary>
        QuotedPriceForType ObjForTypeBLL = new QuotedPriceForType();


        /// <summary>
        /// 报价单
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        int CustomerID;
        int DispatchingID;

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>   
        protected void Page_Load(object sender, EventArgs e)
        {

            CustomerID = Request["CustomerID"].ToString().ToInt32();
            DispatchingID = Request["DispatchingID"].ToString().ToInt32();
            if (!IsPostBack)
            {
                BinderData();
                GetSaleCost();
            }
        }
        #endregion


        #region 数据绑定


        private void BinderData()
        {
            var DataList = ObjCostSumBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());

            //供应商成本 来自供应商明细产品表

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7));  //执行团队 5  内部人员 4  手动添加  7

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 8 || C.RowType == 10).OrderBy(C => C.CostSumId));        //物料 1      库房 2      新购买 3    系统默认添加的设计师 6   手动添加 8  设计单 10

            repOtherCost.DataBind(DataList.Where(C => C.RowType == 9 || C.RowType == 11));  //其他

            repSaleMoney.DataBind(DataList.Where(C => C.RowType == 12));            //销售成本

            if (repOtherCost.Items.Count == 0)          //其他
            {
                repOtherCost.Visible = false;
                tr_Other.Visible = false;
            }

            if (repSaleMoney.Items.Count == 0)          //销售成本
            {
                repSaleMoney.Visible = false;
                tr_Sale.Visible = false;
            }


            //人员 物料 其他 销售价 成本价 毛利率
            GetFinishAmount(3);     //销售价
            GetCostAmounts(3);      //成本价
            GetProfitRate();        //毛利率


        }
        #endregion

        #region 总金额 总成本  毛利率

        //1.销售价  2.成本  3.毛利率
        public void GetSaleCost()
        {
            HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
            CostSum ObjCostSumBLL = new CostSum();
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            var CostSumList = ObjCostSumBLL.GetByCustomerID(CustomerID);
            lblSaleAmount.Text = QuotedModel.FinishAmount.ToString().ToDecimal().ToString("f2");
            lblSaleCost.Text = CostSumList.Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString("f2");
            if (lblSaleAmount.Text.ToDecimal() > 0)
            {
                lblSaleRate.Text = ((lblSaleAmount.Text.ToDecimal() - lblSaleCost.Text.ToDecimal()) / lblSaleAmount.Text.ToDecimal()).ToString("0.00%"); ;
            }
        }
        #endregion

        #region 获取人员 物料 其他的销售价
        /// <summary>
        /// Type类型 1.人员  2.物料 3.其他
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="TypeSource"></param>
        /// <returns></returns>

        public void GetFinishAmount(int Type)
        {
            decimal FinishAmount = 0;

            int CustomerID = Request["CustomerID"].ToInt32();
            var ObjQuotedPriceList = ObjForTypeBLL.GetByCustomerID(CustomerID);

            if (ObjQuotedPriceList.Count > 0)       //单独存人员 物料 其他价格的表有数据  直接读取显示
            {
                for (int i = 1; i <= Type; i++)
                {
                    if (i == 1)
                    {
                        lblPersonSale.Text = ObjQuotedPriceList.Sum(C => C.PPrice).ToString().ToDecimal().ToString("f2");
                        FinishAmount = ObjQuotedPriceList.Sum(C => C.PPrice).ToString().ToDecimal();
                    }
                    else if (i == 2)
                    {
                        lblMaterialSale.Text = ObjQuotedPriceList.Sum(C => C.MPrice).ToString().ToDecimal().ToString("f2");
                        FinishAmount = ObjQuotedPriceList.Sum(C => C.MPrice).ToString().ToDecimal();
                    }
                    else if (i == 3)
                    {
                        lblQuotedOtherSale.Text = ObjQuotedPriceList.Sum(C => C.OPrice).ToString().ToDecimal().ToString("f2");
                        FinishAmount = ObjQuotedPriceList.Sum(C => C.OPrice).ToString().ToDecimal();
                    }
                }
            }
            else            //如果不存在  就获取产品表的各个产品的总和
            {
                FinishAmount = ObjQuotedPriceItemsBLL.GetByQuotedID(GetQuotedID(CustomerID)).Where(C => C.Type == Type).Sum(C => C.Subtotal).ToString().ToDecimal(); ;
            }
        }
        #endregion

        #region 获取成本价
        /// <summary>
        /// 成本价
        /// </summary>
        public void GetCostAmounts(int Type)
        {
            decimal CostAmount = 0;

            int CustomerID = Request["CustomerID"].ToInt32();
            var DataList = ObjCostSumBLL.GetByCustomerID(CustomerID);

            for (int i = 1; i <= Type; i++)
            {
                if (i == 1)                  //人员
                {
                    CostAmount = DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
                    lblPersonCost.Text = CostAmount.ToString("f2");
                }
                else if (i == 2)             //物料
                {
                    CostAmount = DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 10 || C.RowType == 8).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
                    lblMaterialCost.Text = CostAmount.ToString("f2");
                }
                else if (i == 3)             //其他
                {
                    CostAmount = DataList.Where(C => C.RowType == 11 || C.RowType == 9).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
                    lblQuotedOtherCost.Text = CostAmount.ToString("f2");
                }
            }

        }
        #endregion

        #region 获取毛利率
        /// <summary>
        /// 成本价
        /// </summary>
        public void GetProfitRate()
        {
            //人员毛利率
            if (lblPersonSale.Text.ToDecimal() > 0)
            {
                lblPersonRate.Text = ((lblPersonSale.Text.ToDecimal() - lblPersonCost.Text.ToDecimal()) / lblPersonSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblMaterialRate.Text = "0.00%";
            }

            //物料毛利率
            if (lblMaterialSale.Text.ToDecimal() > 0)
            {
                lblMaterialRate.Text = ((lblMaterialSale.Text.ToDecimal() - lblMaterialCost.Text.ToDecimal()) / lblMaterialSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblMaterialRate.Text = "0.00%";
            }

            //其他毛利率
            if (lblQuotedOtherSale.Text.ToDecimal() > 0)
            {
                lblQuotedOtherRate.Text = ((lblQuotedOtherSale.Text.ToDecimal() - lblQuotedOtherCost.Text.ToDecimal()) / lblQuotedOtherSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblQuotedOtherRate.Text = "0.00%";
            }
        }
        #endregion


        #region 判断三大类(人员 物料 其他)是否有数据
        public string IsHide(object Source)
        {
            int Type = Source.ToString().ToInt32();
            var DataList = ObjCostSumBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());
            List<FL_CostSum> GetList = new List<FL_CostSum>();
            if (Type == 1)
            {
                if (DataList != null)
                {
                    GetList = DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 3).ToList();
                }
            }
            else if (Type == 2)
            {
                if (DataList != null)
                {
                    GetList = DataList.Where(C => C.RowType == 11).ToList();
                }
            }
            else if (Type == 3)
            {
                if (DataList != null)
                {
                    GetList = DataList.Where(C => C.RowType == 11).ToList();
                }
            }

            if (GetList.Count == 0 || GetList == null)
            {
                return "style='display:none;'";
            }
            return "";

        }

        #endregion

        #region 获取QuotedID
        /// <summary>
        /// 获取QuotedID
        /// </summary>
        public int GetQuotedID(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                return Model.QuotedID;
            }
            return 0;
        }
        #endregion

        public string GetQuotedEmployee()
        {
            int QuotedID = GetQuotedID(Request["CustomerID"].ToString().ToInt32());
            var QuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            if (QuotedModel != null)
            {
                return GetEmployeeName(QuotedModel.EmpLoyeeID).ToString();
            }
            return "";
        }
    }
}