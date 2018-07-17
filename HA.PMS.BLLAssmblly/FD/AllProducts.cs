using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
//黄晓可 全部产品查询
namespace HA.PMS.BLLAssmblly.FD
{
    public class AllProducts
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Update(FD_AllProducts ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.Keys;
            }
            return 0;
        }
        /// <summary>
        /// 根据产品类型获取产品下的产品
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="ProjectCategory">项目ID</param>
        public List<FD_AllProducts> GetByType(int? Type, int? ProjectCategory)
        {
            return ObjEntity.FD_AllProducts.Where(C => C.Type == Type && C.ProjectCategory == ProjectCategory && C.IsDelete == false).ToList();
        }


        /// <summary>
        /// 根据虚拟类型获取虚拟产品
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="ProjectCategory"></param>
        /// <returns></returns>
        public List<FD_AllProducts> GetByVType(int? VType, int? ProjectCategory)
        {
            return ObjEntity.FD_AllProducts.Where(C => C.VirtualType == VType && C.ProjectCategory == ProjectCategory && C.IsDelete == false).ToList();
        }


        /// <summary>
        /// 根据ID获取产品
        /// </summary>
        /// <param name="Keys"></param>
        /// <returns></returns>
        public FD_AllProducts GetByID(int? Keys)
        {
            return ObjEntity.FD_AllProducts.FirstOrDefault(C => C.Keys == Keys);
        }


        /// <summary>
        /// 根据参数获取产品
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_AllProducts> GetbyAllProductsParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_AllProducts>.GetDataByParameter(new FD_AllProducts(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_AllProducts> resultList = query.OrderByDescending(C => C.Keys)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_AllProducts>();
            }
            return resultList;

        }


        public List<FD_AllProducts> GetbyAllProductsParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FD_AllProducts>.GetDataByParameter(new FD_AllProducts(), ObjParameterList);


            return query;

        }
        /// <summary>
        /// 根据类型和KindID获取
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="KindID"></param>
        /// <returns></returns>
        public FD_AllProducts GetByKind(int? Type, int? KindID)
        {

            return ObjEntity.FD_AllProducts.FirstOrDefault(C => C.Type == Type && C.KindID == KindID);
        }

        public FD_AllProducts GetVPByKind(int? VirtualType, int? KindID)
        {

            return ObjEntity.FD_AllProducts.FirstOrDefault(C => C.VirtualType == VirtualType && C.KindID == KindID);
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_AllProducts ObjectT)
        {

            ObjEntity.FD_AllProducts.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.Keys;
        }

        public int Delete(int KindID, int Type)
        {
            FD_AllProducts delProducts = ObjEntity.FD_AllProducts.Where(C => C.Type == Type && C.KindID == KindID).FirstOrDefault();
            ObjEntity.FD_AllProducts.Remove(delProducts);
            return ObjEntity.SaveChanges();
        }

        public int Delete(int Keys)
        {
            FD_AllProducts delProducts = ObjEntity.FD_AllProducts.Where(C => C.Keys == Keys).FirstOrDefault();
            ObjEntity.FD_AllProducts.Remove(delProducts);
            return ObjEntity.SaveChanges();
        }

        /// <summary>
        /// 根据ProductID获取供应商供应的产品
        /// </summary>
        /// <param name="ObjKeyList"></param>
        /// <returns></returns>
        public List<View_GetSupplierProduct> GetGetSupplierProduct(int[] ObjKeyList)
        {
            return (from C in ObjEntity.View_GetSupplierProduct
                    where (ObjKeyList).Contains(C.Keys)
                    select C).ToList();
        }

        /// <summary>
        /// 根据ID组
        /// </summary>
        /// <returns></returns>
        public List<FD_AllProducts> GetByKeysList(int[] ObjKeyList)
        {
            return (from C in ObjEntity.FD_AllProducts
                    where (ObjKeyList).Contains(C.Keys)
                    select C).ToList();
        }


        /// <summary>
        /// 获取产品使用次数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetUsedTimes(int ProductID, int RowType)
        {
            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            IEnumerable<int> CustomerIDs = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value < nowTime).Select(C => C.CustomerID);
            IEnumerable<int> DispatchingIDList = ObjEntity.FL_Dispatching.Where(C => CustomerIDs.Contains(C.CustomerID.Value)).Select(C => C.DispatchingID);

            FD_AllProducts fD_AllProducts = ObjEntity.FD_AllProducts.Where(C => C.KindID.HasValue && C.KindID.Value == ProductID && C.Type.HasValue && C.Type.Value == RowType).FirstOrDefault();
            if (fD_AllProducts != null)
            {
                int Keys = fD_AllProducts.Keys;
                return ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID.HasValue && C.ProductID.Value == Keys && C.RowType.HasValue && C.RowType.Value == RowType && DispatchingIDList.Contains(C.DispatchingID)).Count();
            }
            return 0;
        }

        /// <summary>
        /// 获取产品使用次数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetUsedTimes(int ProductID, int RowType, DateTime start, DateTime end)
        {
            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            IEnumerable<int> CustomerIDs = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value >= start && C.PartyDate.Value < end && C.PartyDate.Value < nowTime).Select(C => C.CustomerID);
            IEnumerable<int> DispatchingIDList = ObjEntity.FL_Dispatching.Where(C => CustomerIDs.Contains(C.CustomerID.Value)).Select(C => C.DispatchingID);

            FD_AllProducts fD_AllProducts = ObjEntity.FD_AllProducts.Where(C => C.KindID.HasValue && C.KindID.Value == ProductID && C.Type.HasValue && C.Type.Value == RowType).FirstOrDefault();
            if (fD_AllProducts != null)
            {
                int Keys = fD_AllProducts.Keys;
                return ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID.HasValue && C.ProductID.Value == Keys && C.RowType.HasValue && C.RowType.Value == RowType && DispatchingIDList.Contains(C.DispatchingID)).Count();
            }
            return 0;
        }

        /// <summary>
        /// 获取产品使用个数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetUsedCount(int ProductID, int RowType)
        {
            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            IEnumerable<int> CustomerIDs = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value < nowTime).Select(C => C.CustomerID);
            IEnumerable<int> DispatchingIDList = ObjEntity.FL_Dispatching.Where(C => CustomerIDs.Contains(C.CustomerID.Value)).Select(C => C.DispatchingID);

            FD_AllProducts fD_AllProducts = ObjEntity.FD_AllProducts.Where(C => C.KindID.HasValue && C.KindID.Value == ProductID && C.Type.HasValue && C.Type.Value == RowType).FirstOrDefault();
            if (fD_AllProducts != null)
            {
                int Keys = fD_AllProducts.Keys;
                List<FL_ProductforDispatching> query = ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID.HasValue && C.ProductID.Value == Keys && C.RowType.HasValue && C.RowType.Value == RowType && DispatchingIDList.Contains(C.DispatchingID)).ToList();
                return query.Sum(C => C.Quantity);
            }
            return 0;
        }

        /// <summary>
        /// 获取产品使用后剩余个数（婚期结束）
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetLeaveCount(int ProductID, int RowType)
        {
            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            IEnumerable<int> CustomerIDs = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value < nowTime).Select(C => C.CustomerID);
            IEnumerable<int> DispatchingIDList = ObjEntity.FL_Dispatching.Where(C => CustomerIDs.Contains(C.CustomerID.Value)).Select(C => C.DispatchingID);

            FD_AllProducts fD_AllProducts = ObjEntity.FD_AllProducts.Where(C => C.KindID.HasValue && C.KindID.Value == ProductID && C.Type.HasValue && C.Type.Value == RowType).FirstOrDefault();
            if (fD_AllProducts != null)
            {
                int Keys = fD_AllProducts.Keys;
                List<FL_ProductforDispatching> query = ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID.HasValue && C.ProductID.Value == Keys && C.RowType.HasValue && C.RowType.Value == RowType && DispatchingIDList.Contains(C.DispatchingID)).ToList();
                return fD_AllProducts.Count.HasValue ? fD_AllProducts.Count.Value - query.Sum(C => C.Quantity) : -query.Sum(C => C.Quantity);
            }
            return 0;
        }

        /// <summary>
        /// 获取当前产品的可用个数。
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="RowType"></param>
        /// <returns></returns>
        public int GetSpareCount(int ProductID, int RowType)
        {
            FD_AllProducts fD_AllProducts = ObjEntity.FD_AllProducts.Where(C => C.KindID.HasValue && C.KindID.Value == ProductID && C.Type.HasValue && C.Type.Value == RowType).FirstOrDefault();
            if (fD_AllProducts != null)
            {
                int Keys = fD_AllProducts.Keys;
                IEnumerable<FL_ProductforDispatching> query = ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID.HasValue && C.ProductID.Value == Keys && C.RowType.HasValue && C.RowType.Value == RowType);
                return fD_AllProducts.Count.HasValue ? fD_AllProducts.Count.Value - query.Sum(C => C.Quantity) : -query.Sum(C => C.Quantity);
            }
            return 0;
        }

        /// <summary>
        /// 获产品的总数。
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="RowType"></param>
        /// <returns></returns>
        public int GetTotalCount(int ProductID, int RowType)
        {
            FD_AllProducts fD_AllProducts = ObjEntity.FD_AllProducts.Where(C => C.KindID.HasValue && C.KindID.Value == ProductID && C.Type.HasValue && C.Type.Value == RowType).FirstOrDefault();
            if (fD_AllProducts != null)
            {
                return fD_AllProducts.Count.HasValue ? fD_AllProducts.Count.Value : 0;
            }
            return 0;
        }

        public IEnumerable<FL_DispatchAllProducts> GetDispatchAllProducts(int RowType, bool IsPartyDatePass)
        {
            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            IEnumerable<int> CustomerIDs = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value < nowTime).Select(C => C.CustomerID);
            IEnumerable<int> DispatchingIDList = ObjEntity.FL_Dispatching.Where(C => CustomerIDs.Contains(C.CustomerID.Value)).Select(C => C.DispatchingID);
            return IsPartyDatePass ? ObjEntity.FL_DispatchAllProducts.Where(C => C.RowType.HasValue && C.RowType.Value == RowType && C.ItemLevel.HasValue && C.ItemLevel.Value == 3 && DispatchingIDList.Contains(C.DispatchingID)) :
                ObjEntity.FL_DispatchAllProducts.Where(C => C.RowType.HasValue && C.RowType.Value == RowType && C.ItemLevel.HasValue && C.ItemLevel.Value == 3 && !DispatchingIDList.Contains(C.DispatchingID));
        }

        /// <summary>
        /// 获取使用过的产品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FD_AllProducts> GetUsedProducts(int RowType)
        {
            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            IEnumerable<int> CustomerIDs = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value < nowTime).Select(C => C.CustomerID);
            IEnumerable<int> DispatchingIDList = ObjEntity.FL_Dispatching.Where(C => CustomerIDs.Contains(C.CustomerID.Value)).Select(C => C.DispatchingID);

            IEnumerable<FL_ProductforDispatching> temp = ObjEntity.FL_ProductforDispatching.Where(C => C.RowType.HasValue && C.RowType.Value == RowType && C.ItemLevel.HasValue && C.ItemLevel.Value == 3 && DispatchingIDList.Contains(C.DispatchingID));
            return ObjEntity.FD_AllProducts.Join(temp, C => C.Keys, D => D.ProductID.Value, (C, D) => C);
        }

        /// <summary>
        /// 库房产品统计 使用的临时方法。（延迟加载）（只有这个，到时可以删掉）
        /// </summary>
        /// <param name="RowType"></param>
        /// <param name="CustomerIDs"></param>
        /// <returns></returns>
        public IEnumerable<FL_DispatchAllProducts> GetProducts(int RowType, IEnumerable<int> CustomerIDs)
        {

            DateTime nowTime = DateTime.Now.AddDays(1).AddSeconds(-1);
            if (CustomerIDs == null)
            {
                return ObjEntity.FL_DispatchAllProducts.Where(C => C.RowType.HasValue && C.RowType.Value == RowType && C.ItemLevel.HasValue && C.ItemLevel.Value == 3);
            }
            else
            {
                return ObjEntity.FL_DispatchAllProducts.Where(C => C.RowType.HasValue && C.RowType.Value == RowType && C.ItemLevel.HasValue && C.ItemLevel.Value == 3 && CustomerIDs.Contains(C.Expr6.Value));
            }
        }

        [Obsolete]
        public IEnumerable<FL_DispatchAllProducts> GetPagedProducts(int PageSize, int PageIndex, out int SourceCount, Func<FL_DispatchAllProducts, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FL_DispatchAllProducts> query = PublicDataTools<FL_DispatchAllProducts>
                .GetDataByParameter(new FL_DispatchAllProducts(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.DispatchingID);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }
        [Obsolete]
        public IEnumerable<FL_DispatchAllProducts> GetPagedProducts<S>(int PageSize, int PageIndex, out int SourceCount, bool isAsc, Func<FL_DispatchAllProducts, S> keySelector, Func<FL_DispatchAllProducts, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FL_DispatchAllProducts> query = PublicDataTools<FL_DispatchAllProducts>
                .GetDataByParameter(new FL_DispatchAllProducts(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate);
            SourceCount = query.Count();
            return isAsc ? query.OrderBy<FL_DispatchAllProducts, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize) :
                query.OrderByDescending<FL_DispatchAllProducts, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }


        //[FL_ProductforDispatching]
        public IEnumerable<FL_ProductforDispatching> GetPagedProductforDispatching(int PageSize, int PageIndex, out int SourceCount, Func<FL_ProductforDispatching, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FL_ProductforDispatching> query = PublicDataTools<FL_ProductforDispatching>
                .GetDataByParameter(new FL_ProductforDispatching(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.DispatchingID);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }
        public IEnumerable<FL_ProductforDispatching> GetProductforDispatching(Func<FL_ProductforDispatching, bool> predicate)
        {
            return ObjEntity.FL_ProductforDispatching.Where(predicate);
        }


        #region 基于 Repositoy

        #endregion
    }
}
