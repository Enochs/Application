using System;
using System.Collections.Generic;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseProductStatistics : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }


        protected void BinderData(object sender, EventArgs e)
        {
            IEnumerable<ObjectParameter> paramsList = new List<ObjectParameter>();

            int sourcecount = 0;
            var query = new StorehouseSourceProduct().GetPagedProducts(StorePager.PageSize, StorePager.CurrentPageIndex, out sourcecount, OrderSelector.IsAscending, C => C.SourceProductId, C => true, paramsList);
            var dataSource = (from C in query
                              select new
                              {
                                  SourceProductId = C.SourceProductId,
                                  SourceProductName = C.SourceProductName,
                                  PutStoreDate = C.PutStoreDate,
                                  ProductCategory = C.ProductCategory,
                                  ProductProject = C.ProductProject,
                                  PurchasePrice = C.PurchasePrice,
                                  SaleOrice = C.SaleOrice,
                                  SourceCount = C.SourceCount,
                                  Unit = C.Unit,
                                  Position = C.Position,
                                  LeaveCount = GetLeaveCount(C.SourceProductId, C.SourceCount),
                                  LastUsedDate = GetLastUsedDate(C.SourceProductId),
                                  UsedTimes = GetUsedTimes(C.SourceProductId)
                              }).ToList();
            if (OrderSelector.IsAscending)
            {
                switch (ddlOrderColumn.SelectedValue)
                {
                    case "SourceProductName": rptStorehouse.DataSource = dataSource.OrderBy(C => C.SourceProductName); break;
                    case "PutStoreDate": rptStorehouse.DataSource = dataSource.OrderBy(C => C.PutStoreDate); break;
                    case "LeaveCount": rptStorehouse.DataSource = dataSource.OrderBy(C => C.LeaveCount); break;
                    case "SourceCount": rptStorehouse.DataSource = dataSource.OrderBy(C => C.SourceCount); break;
                    case "LastUsedDate": rptStorehouse.DataSource = dataSource.OrderBy(C => C.LastUsedDate); break;
                    case "UsedTimes": rptStorehouse.DataSource = dataSource.OrderBy(C => C.UsedTimes); break;
                }
            }
            else
            {
                switch (ddlOrderColumn.SelectedValue)
                {
                    case "SourceProductName": rptStorehouse.DataSource = dataSource.OrderByDescending(C => C.SourceProductName); break;
                    case "PutStoreDate": rptStorehouse.DataSource = dataSource.OrderByDescending(C => C.PutStoreDate); break;
                    case "LeaveCount": rptStorehouse.DataSource = dataSource.OrderByDescending(C => C.LeaveCount); break;
                    case "SourceCount": rptStorehouse.DataSource = dataSource.OrderByDescending(C => C.SourceCount); break;
                    case "LastUsedDate": rptStorehouse.DataSource = dataSource.OrderByDescending(C => C.LastUsedDate); break;
                    case "UsedTimes": rptStorehouse.DataSource = dataSource.OrderByDescending(C => C.UsedTimes); break;
                }
            }
            StorePager.RecordCount = sourcecount;
            rptStorehouse.DataBind();
        }

        /// <summary>
        /// 获取使用次数
        /// </summary>
        /// <param name="SourceProductId"></param>
        /// <returns></returns>
        protected int GetUsedTimes(object SourceProductId)
        {
            return new HA.PMS.BLLAssmblly.FD.StorehouseSourceProduct().GetUsedTimes((SourceProductId + string.Empty).ToInt32());
        }

        /// <summary>
        /// 获取剩余个数
        /// </summary>
        /// <param name="SourceCount"></param>
        /// <param name="SourceProductId"></param>
        /// <returns></returns>
        protected int GetLeaveCount(object SourceProductId, object SourceCount)
        {
            return (SourceCount + string.Empty).ToInt32() - new HA.PMS.BLLAssmblly.FD.StorehouseSourceProduct().GetUsedCount((SourceProductId + string.Empty).ToInt32());
        }

        protected string GetLastUsedDate(object SourceProductId)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            FD_AllProducts fD_AllProducts = new AllProducts().GetByKind(2, SourceProductId.To<Int32>(0));
            if (fD_AllProducts != null)
            {
                List<FL_ProductforDispatching> fL_ProductforDispatchingList = ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID.Value == fD_AllProducts.Keys).ToList();
                if (fL_ProductforDispatchingList != null && fL_ProductforDispatchingList.Count > 0)
                {
                    IEnumerable<int> dispatchingIDList = fL_ProductforDispatchingList.Select(C => C.DispatchingID);
                    IEnumerable<int?> customerIDList = ObjEntity.FL_Dispatching.Where(C => dispatchingIDList.Contains(C.DispatchingID)).Select(C => C.CustomerID);
                    List<FL_Customers> fL_CustomersList = ObjEntity.FL_Customers.Where(C => customerIDList.Contains(C.CustomerID) && C.PartyDate.HasValue).ToList();
                    if (fL_CustomersList != null && fL_CustomersList.Count > 0)
                    {
                        FL_Customers fL_Customers = fL_CustomersList.OrderByDescending(C => C.PartyDate.Value).First();
                        return string.Concat(fL_Customers.PartyDate.Value.ToString("yyyy-MM-dd"), fL_Customers.TimeSpans);
                    }
                }
            }
            return "暂未使用";
        }
    }
}