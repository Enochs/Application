using System;
using System.Collections.Generic;
using System.Linq;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class StorehouseStatistics : HA.PMS.Pages.SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        protected string GetProductName(object SourceProductId)
        {
            FD_StorehouseSourceProduct fD_StorehouseSourceProduct = objStorehouseSourceProductBLL.GetByID(Convert.ToInt32(SourceProductId));
            return fD_StorehouseSourceProduct != null ? fD_StorehouseSourceProduct.SourceProductName : string.Empty;
        }

        protected void BinderData(object sender, EventArgs e)
        {
            IEnumerable<int> customerIDs = new Customers().Where(C => C.PartyDate >= PartyDateRanger.Start && C.PartyDate <= PartyDateRanger.End).Select(C => C.CustomerID);
            //List<View_StorehouseStatistics> viewStatistics = new StorehouseSourceProduct().GetStorehouseStatisticsByCustomersId(PartyDateRanger.IsNotBothEmpty, queryCustomerList.Select(C => C.CustomerID).ToList());
            var query = new AllProducts().GetProducts(2, customerIDs).ToList();

            #region 销售排名
            var queryGoup = (from m in query
                             group m by m.KindID into statistic
                             orderby statistic.Sum(C => C.Quantity) descending
                             select new { SourceProductId = statistic.Key.Value, SaleCount = statistic.Sum(C => C.Quantity), SaleTimes = statistic.Count() }).ToList();
            if (queryGoup.Count() > 20)
            {
                rptSaleTop10.Visible = true;
                rptSaleTop20.Visible = true;
                rptSaleTop30.Visible = true;
            }
            else
            {
                if (queryGoup.Count() <= 20)
                {
                    rptSaleTop30.Visible = false;
                }
                if (queryGoup.Count() <= 10)
                {
                    rptSaleTop20.Visible = false;
                }
                if (queryGoup.Count <= 0)
                {
                    rptSaleTop10.Visible = false;
                }
            }
            rptSaleTop10.DataBind(queryGoup.Take(10));
            rptSaleTop20.DataBind(queryGoup.Skip(10).Take(10));
            rptSaleTop30.DataBind(queryGoup.Skip(20).Take(10));

            if (queryGoup.Count() > 30)
            {
                int count = (queryGoup.Count() - 30) / 3;
                switch ((queryGoup.Count() - 30) % 3)
                {
                    case 0:
                        rptColumn1.DataBind(queryGoup.Skip(30).Take(count));
                        rptColumn2.DataBind(queryGoup.Skip(30 + count).Take(count));
                        rptColumn3.DataBind(queryGoup.Skip(30 + 2 * count).Take(count));
                        break;
                    case 1:
                        rptColumn1.DataBind(queryGoup.Skip(30).Take(count + 1));
                        rptColumn2.DataBind(queryGoup.Skip(31 + count).Take(count));
                        rptColumn3.DataBind(queryGoup.Skip(31 + 2 * count).Take(count));
                        break;
                    case 2:
                        rptColumn1.DataBind(queryGoup.Skip(30).Take(count + 1));
                        rptColumn2.DataBind(queryGoup.Skip(31 + count).Take(count + 1));
                        rptColumn3.DataBind(queryGoup.Skip(32 + 2 * count).Take(count));
                        break;
                }
                rptColumn1.Visible = true;
                rptColumn2.Visible = true;
                rptColumn3.Visible = true;
            }
            else
            {
                rptColumn1.Visible = false;
                rptColumn2.Visible = false;
                rptColumn3.Visible = false;
            }

            #endregion

            #region 销售概况
            int totalSaleCount = queryGoup.Sum(C => C.SaleCount);

            int top10 = queryGoup.Where(C => C.SaleTimes >= 10).Sum(C => C.SaleCount);
            ltlTop10.Text = top10.ToString();

            int top5 = queryGoup.Where(C => C.SaleTimes >= 5 && C.SaleTimes < 10).ToList().Sum(C => C.SaleCount);
            ltlTop5.Text = top5.ToString();

            var m1 = queryGoup.Where(C => C.SaleTimes >= 1 && C.SaleTimes < 5).ToList();
            int top1 = queryGoup.Where(C => C.SaleTimes >= 1 && C.SaleTimes < 5).ToList().Sum(C => C.SaleCount);
            ltlTop1.Text = top1.ToString();

            int top0 = queryGoup.Where(C => C.SaleTimes == 0).ToList().Sum(C => C.SaleCount);
            ltlTop0.Text = top0.ToString();

            if (totalSaleCount == 0)
            {
                ltlTop10Rate.Text = "0 %";
                ltlTop5Rate.Text = "0 %";
                ltlTop1Rate.Text = "0 %";
                ltlTop0Rate.Text = "0 %";
            }
            else
            {
                ltlTop10Rate.Text = GetDoubleFormat((double)top10 / totalSaleCount);
                ltlTop5Rate.Text = GetDoubleFormat((double)top5 / totalSaleCount);
                ltlTop1Rate.Text = GetDoubleFormat((double)top1 / totalSaleCount);
                ltlTop0Rate.Text = GetDoubleFormat((double)top0 / totalSaleCount);
            }

            #endregion

            #region 新入库
            //库房产品源数量
            //新入库对像
            var queryall = new AllProducts().GetProducts(2, null).ToList();

            int WasteCount = 0;
            int OperCount = 0;
            //当前月份的数字
            int monthNumber = DateTime.Now.Month;
            //新入库个数
            int monthPutCount = 0;
            //采购总价
            decimal priceSum = 0;
            //销售价
            decimal SalePriceSum = 0;
            //新产品使用总次数
            int operSumCount = 0;
            //库房原始产品库对象数据集
            var querySourceProduct = objStorehouseSourceProductBLL.GetByAll();
            //当月新入库房产品原始数据集
            var sourceProductAll = querySourceProduct.Where(C => C.PutStoreDate.Month == monthNumber && C.PutStoreDate.Year == DateTime.Now.Year);
            foreach (var item in sourceProductAll)
            {
                monthPutCount++;
                priceSum = priceSum + item.PurchasePrice.Value;
                SalePriceSum = SalePriceSum + item.SaleOrice.Value;
                //根据上面的新入库产品对象中 的产品ID 
                //然后再 物料执行表中判断是否存在然后求出新产品使用次数
                foreach (var dispatchingItem in queryall)
                {
                    if (dispatchingItem.KindID.Value == item.SourceProductId)
                    {
                        operSumCount++;
                    }
                }
            }
            // 在库房原始产品库遍历产品状态情况个数
            foreach (var item in querySourceProduct)
            {
                if (item.ProductState == "报废")
                {
                    WasteCount++;
                }
                if (item.ProductState == "拆用")
                {
                    OperCount++;
                }
            }
            #endregion

            #region 报废拆用情况
            ltlNewProductOperCount.Text = operSumCount + "次";
            //报废个数
            ltlWasteCount.Text = WasteCount + "个";
            //拆用个数
            ltlOperCount.Text = OperCount + "个";
            //新入库个数
            ltlNewProductCount.Text = monthPutCount + "个";
            //新入库采购价
            ltlNewPriceSum.Text = priceSum + "元";
            //销售总价
            ltlSalePriceSum.Text = SalePriceSum + "元";
            #endregion
        }
    }
}