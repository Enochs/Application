
/**
 Version :HaoAi 1.0
 File Name :StorehouseSourceProduct
 Author:杨洋
 Date:2013.4.18
 Description:库房产品表 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Flow;
namespace HA.PMS.BLLAssmblly.FD
{
    public class StorehouseSourceProduct : ICRUDInterface<FD_StorehouseSourceProduct>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_StorehouseSourceProduct ObjectT)
        {
            if (ObjectT != null)
            {
                FD_StorehouseSourceProduct objProduct = GetByID(ObjectT.SourceProductId);

                objProduct.IsDelete = true;

                AllProducts objAllProductsBLL = new AllProducts();
                objAllProductsBLL.Delete(ObjectT.SourceProductId, 2);
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 这里是根据Category 修改类别的情况下，对应的修改相关的产品记录
        /// </summary>
        /// <param name="StorehouseSourceID"></param>
        /// <returns></returns>
        public FD_StorehouseSourceProduct GetByStorehouseSourceID(int StorehouseSourceID)
        {
            return ObjEntity.FD_StorehouseSourceProduct.FirstOrDefault(C => C.StorehouseSourceID == StorehouseSourceID);

        }


        /// <summary>
        /// 根据项目获取产品
        /// </summary>
        /// <returns></returns>
        public List<FD_StorehouseSourceProduct> GetByCategory(int ProjectCategory)
        {
            return ObjEntity.FD_StorehouseSourceProduct.Where(C => C.ProductCategory == ProjectCategory && C.IsDelete == false).ToList();
        }
        /// <summary>
        /// 库房统计调用方法
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="customerList"></param>
        /// <returns></returns>
        public List<View_StorehouseStatistics> GetStorehouseStatisticsByCustomersId(bool isAdd, List<int> customerList)
        {
            var query = ObjEntity.View_StorehouseStatistics.Where(C => C.IsDelete == false).ToList();

            List<View_StorehouseStatistics> list = new List<View_StorehouseStatistics>();
            View_StorehouseStatistics statis;
            //代表使用条件查询
            if (isAdd)
            {

                foreach (var cid in customerList)
                {
                    statis = query.Where(C => C.CustomerID == cid).FirstOrDefault();
                    if (statis != null)
                    {
                        list.Add(statis);
                    }
                }
            }
            else
            {
                list = query;
            }
            return list;
        }

        public List<FD_StorehouseSourceProduct> GetByAll()
        {
            return ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDelete == false)
                .OrderBy(C => C.StorehouseSourceID).ToList();
        }

        public FD_StorehouseSourceProduct GetByID(int? KeyID)
        {
            return ObjEntity.FD_StorehouseSourceProduct.FirstOrDefault(C => C.SourceProductId == KeyID);
        }

        /// <summary>
        /// 分页获取某个库管的库房数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_StorehouseSourceProduct> GetByIndex(int PageSize, int PageIndex, out int SourceCount, int EmpLoyeeID)
        {
            Storehouse ObjStorehouseBLL = new Storehouse();

            var HouseID = ObjStorehouseBLL.GetKeyByManager(EmpLoyeeID);
            SourceCount = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDelete == false && C.StorehouseID == HouseID).Count();

            List<FD_StorehouseSourceProduct> resultList = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDelete == false && C.StorehouseID == HouseID)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SourceProductId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_StorehouseSourceProduct>();
            }
            return resultList;
        }


        /// <summary>
        /// 分页获取库房数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_StorehouseSourceProduct> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDelete == false).Count();

            List<FD_StorehouseSourceProduct> resultList = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SourceProductId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_StorehouseSourceProduct>();
            }
            return resultList;
        }


        /// <summary>
        /// 添加库房产量
        /// 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_StorehouseSourceProduct ObjectT)
        {


            if (ObjectT != null)
            {
                FD_AllProducts allProduct = new FD_AllProducts();
                if (ObjectT.Unit.Contains("VirtualProduct"))
                {
                    allProduct.VirtualType = 9;
                    ObjectT.Unit = ObjectT.Unit.Replace("VirtualProduct", "");
                    allProduct.Classification = ObjectT.Unit.Split('①')[1];
                    ObjectT.Unit = ObjectT.Unit.Split('①')[0];
                    allProduct.Unit = ObjectT.Unit.Split('①')[0];
                    QuotedCatgory objCategoryBLL = new QuotedCatgory();

                    FD_QuotedCatgory cate = objCategoryBLL.GetByID(ObjectT.ProductCategory);
                }
                else
                {
                    Category objCategoryBLL = new Category();
                    allProduct.VirtualType = 1;
                    FD_Category cate = objCategoryBLL.GetByID(ObjectT.ProductCategory);
                    allProduct.Unit = ObjectT.Unit;
                    allProduct.Productproperty = cate.Productproperty;
                }
                ObjEntity.FD_StorehouseSourceProduct.Add(ObjectT);
                ObjEntity.SaveChanges();

                if (ObjectT.SourceProductId > 0)
                {

                    allProduct.Count = ObjectT.SourceCount;
                    allProduct.Data = ObjectT.Data;
                    allProduct.Explain = "";
                    allProduct.IsDelete = false;
                    allProduct.SupplierName = "库房";
                    allProduct.ProductCategory = ObjectT.ProductCategory;
                    allProduct.ProjectCategory = ObjectT.ProductProject;
                    allProduct.PurchasePrice = ObjectT.PurchasePrice;
                    allProduct.SalePrice = ObjectT.SaleOrice;
                    allProduct.Specifications = ObjectT.Specifications;
                    allProduct.ProductName = ObjectT.SourceProductName;
                    allProduct.Type = 2;//库房

                    allProduct.Remark = ObjectT.Remark;
                    allProduct.KindID = ObjectT.SourceProductId;
                    allProduct.CreateDate = DateTime.Now;
                    allProduct.Position = ObjectT.Position;
                    allProduct.IsDisposible = ObjectT.IsDisposible;
                    allProduct.StorehouseID = ObjectT.StorehouseID;

                    AllProducts objAllProductsBLL = new AllProducts();
                    objAllProductsBLL.Insert(allProduct);


                    //ObjEntity.FD_StorehouseSourceProduct.Add(ObjectT);
                    //ObjEntity.SaveChanges();
                    return ObjectT.SourceProductId;
                }

            }
            return 0;
        }
        ///// <summary>
        ///// 这个主要是查询出当前的总派工ID 返回库房产品中的单个产品剩余个数
        ///// </summary>
        ///// <param name="SourceProductId">库房产品ID</param>
        ///// <param name="SourceCount">产品的总个数</param>
        ///// <returns></returns>
        //public int GetProductForDispatchCountBySourceProductId(int SourceProductId, int SourceCount)
        //{
        //    View_FLProductforDispatchingId current = ObjEntity.View_FLProductforDispatchingId.Where(C => C.KindID == SourceProductId).FirstOrDefault();
        //    if (current != null)
        //    {
        //        int DispatchingID = current.DispatchingID;
        //        FL_ProductforDispatching forDispatching = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID).FirstOrDefault();
        //        if (forDispatching != null)
        //        {
        //            return SourceCount - (forDispatching.Quantity + string.Empty).ToInt32();
        //        }
        //        else
        //        {
        //            return SourceCount;
        //        }
        //    }

        //    return SourceCount;

        //}
        ///// <summary>
        ///// 返回某个库房产品最近使用情况
        ///// </summary>
        ///// <param name="SourceProductId"></param>
        ///// <returns></returns>
        //public string GetGetProductForDispatchDateBySourceProductId(int SourceProductId)
        //{
        //    View_FLProductforDispatchingId current = ObjEntity.View_FLProductforDispatchingId.Where(C => C.KindID == SourceProductId).FirstOrDefault();
        //    if (current != null)
        //    {
        //        int DispatchingID = current.DispatchingID;

        //        FL_Dispatching dispatching = ObjEntity.FL_Dispatching.Where(C => C.DispatchingID == DispatchingID).FirstOrDefault();
        //        // forDispatching
        //        if (dispatching != null)
        //        {
        //            FL_Customers customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == dispatching.CustomerID).FirstOrDefault();
        //            if (customers != null)
        //            {
        //                return customers.PartyDate.Value.ToString("yyyy/MM/dd ") + customers.TimeSpans;
        //            }

        //        }
        //    }
        //    return "暂无使用";
        //}

        //public FL_Customers GetCustomer(int BySourceProductId)
        //{
        //    FL_Customers customers;
        //    View_FLProductforDispatchingId current = ObjEntity.View_FLProductforDispatchingId.Where(C => C.KindID.Value == BySourceProductId).FirstOrDefault();
        //    if (current != null)
        //    {
        //        int DispatchingID = current.DispatchingID;

        //        FL_Dispatching dispatching = ObjEntity.FL_Dispatching.Where(C => C.DispatchingID == DispatchingID).FirstOrDefault();

        //        // forDispatching
        //        if (dispatching != null)
        //        {
        //            return customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == dispatching.CustomerID).FirstOrDefault();
        //        }
        //    }
        //    return null;
        //}
        /// <summary>
        /// 返回客户
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        //public List<FL_DispatchingCustomer> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        //{
        //    var query = PublicDataTools<FL_DispatchingCustomer>.GetDataByParameter(new FL_DispatchingCustomer(), ObjParameterList);
        //    SourceCount = query.Count();

        //    List<FL_DispatchingCustomer> resultList = query.OrderByDescending(C => C.DispatchingID)
        //      .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

        //    if (query.Count == 0)
        //    {
        //        resultList = new List<FL_DispatchingCustomer>();
        //    }
        //    return resultList;



        //}


        /// <summary>
        /// 获取库房背料单
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetbyMarkParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParameterList);
            SourceCount = query.Count();

            List<View_DispatchingCustomers> resultList = query.OrderByDescending(C => C.PartyDate)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_DispatchingCustomers>();
            }
            return resultList;



        }

        /// <summary>
        /// 获取库房备料单
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetbyMarkParameterDistinctByCustomerID(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParameterList).Distinct((C, D) => C.CustomerID.Equals(D.CustomerID));
            SourceCount = query.Count();

            List<View_DispatchingCustomers> resultList = query.OrderByDescending(C => C.PartyDate)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_DispatchingCustomers>();
            }
            return resultList;



        }

        ///// <summary>
        ///// 返回所有产品和派工的联合查询
        ///// </summary>
        ///// <returns></returns>
        //public List<View_FLProductforDispatchingId> GetFLProductforDispatching()
        //{
        //    return ObjEntity.View_FLProductforDispatchingId.ToList();
        //}


        /// <summary>
        /// 分页获取库房产品明细
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<FD_AllProducts> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<FD_AllProducts>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }

        /// <summary>
        /// 获取库房产品明细
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        [Obsolete]
        public List<FD_StorehouseSourceProduct> GetDispatchingProduct(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount, int EmpLoyeeID)
        {
            var HouseID = new Storehouse().GetKeyByManager(EmpLoyeeID);
            var query = PublicDataTools<FD_StorehouseSourceProduct>.GetDataByParameter(new FD_StorehouseSourceProduct(), ObjParameterList).Where(C => C.IsDelete == false && C.StorehouseID == HouseID);
            SourceCount = query.Count();
            List<FD_StorehouseSourceProduct> resultList = query.OrderByDescending(C => C.SourceProductId)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count() == 0)
            {
                resultList = new List<FD_StorehouseSourceProduct>();
            }
            return resultList;
        }

        public List<FD_StorehouseSourceProduct> GetAllByParameter(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FD_StorehouseSourceProduct>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, PageIndex, out SourceCount).ToList();
        }


        [Obsolete]
        public List<FD_StorehouseSourceProduct> GetDispatchingProduct(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount, int EmpLoyeeID, DateTime PartyDateStart, DateTime PartyDateEnd)
        {
            Storehouse ObjStorehouseBLL = new Storehouse();

            var HouseID = ObjStorehouseBLL.GetKeyByManager(EmpLoyeeID);
            var query = PublicDataTools<FD_StorehouseSourceProduct>.GetDataByParameter(new FD_StorehouseSourceProduct(), ObjParameterList);

            IEnumerable<int> customerIDList = ObjEntity.FL_Customers.Where(C => C.PartyDate.HasValue && C.PartyDate.Value.CompareTo(PartyDateStart) >= 0 && C.PartyDate.Value.CompareTo(PartyDateEnd) <= 0).Select(C => C.CustomerID);


            ////List<int> productIDList = ObjEntity.View_StorehouseStatistics.Where(C => customerIDList.Contains(C.CustomerID.Value)).Select(C => C.ProductID.Value).ToList();
            ////
            ////var result = query.Where(C => productIDList.Contains(C.SourceProductId) && C.IsDelete == false && C.StorehouseID == HouseID).ToList();
            //IEnumerable<int> dispatchingIDList = ObjEntity.FL_Dispatching.Where(C => C.CustomerID.HasValue && customerIDList.Contains(C.CustomerID.Value)).Select(D => D.DispatchingID);
            //IEnumerable<int> productIDList = ObjEntity.FL_ProductforDispatching.Where(C => dispatchingIDList.Contains(C.DispatchingID) && C.ProductID.HasValue).Select(D => D.ProductID.Value);
            //List<int> kindIDList = ObjEntity.FD_AllProducts.Where(C => productIDList.Contains(C.Keys) && C.KindID.HasValue).Select(D => D.KindID.Value).ToList();
            //var result = query.Where(C => C.IsDelete == false && C.StorehouseID == HouseID && kindIDList.Contains(C.SourceProductId)).ToList();
            var result = query.Where(C => C.IsDelete == false && C.StorehouseID == HouseID).ToList();
            SourceCount = result.Count();

            List<FD_StorehouseSourceProduct> resultList = result.OrderByDescending(C => C.SourceProductId)
               .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (result.Count == 0)
            {
                resultList = new List<FD_StorehouseSourceProduct>();
            }
            return resultList;
        }


        public int Update(FD_StorehouseSourceProduct ObjectT)
        {
            FD_AllProducts allProduct;
            //这里的判断主要是用于在库房明细表当中的修改产品状态，如果此时为编辑是证明当前的操作时，
            //修改库房产品时，进行,相反则是在库房明细表当中操作产品状态时，用的
            if (ObjectT.ProductState == "编辑")
            {

                allProduct = ObjEntity.FD_AllProducts.Where(C => C.KindID == ObjectT.SourceProductId && C.Type == 2).FirstOrDefault();
                if (allProduct != null)
                {
                    allProduct.Count = ObjectT.SourceCount;
                    allProduct.Data = ObjectT.Data;
                    allProduct.Explain = "";
                    allProduct.IsDelete = false;
                    allProduct.KindID = ObjectT.ProductCategory;
                    allProduct.ProductCategory = ObjectT.ProductCategory;
                    allProduct.ProjectCategory = ObjectT.ProductProject;
                    allProduct.PurchasePrice = ObjectT.PurchasePrice;
                    allProduct.SalePrice = ObjectT.SaleOrice;
                    allProduct.Specifications = ObjectT.Specifications;
                    allProduct.ProductName = ObjectT.SourceProductName;
                    allProduct.Type = 2;//库房
                    allProduct.Unit = ObjectT.Unit;
                    allProduct.Remark = ObjectT.Remark;
                    allProduct.Productproperty = 1;
                    Category objCategoryBLL = new Category();
                    if (ObjectT.Unit.Contains("VirtualProduct"))
                    {
                        allProduct.VirtualType = 9;
                        //allProduct.Unit = allProduct.Unit.Replace("VirtualProduct", "");
                        ObjectT.Unit = ObjectT.Unit.Replace("VirtualProduct", "");
                        allProduct.Classification = ObjectT.Unit.Split('①')[1];
                        ObjectT.Unit = ObjectT.Unit.Split('①')[0];
                        allProduct.Unit = ObjectT.Unit.Split('①')[0];
                        QuotedCatgory objQCategoryBLL = new QuotedCatgory();

                        FD_QuotedCatgory cate = objQCategoryBLL.GetByID(ObjectT.ProductCategory);
                    }
                    else
                    {

                        allProduct.VirtualType = 1;
                        FD_Category cate = objCategoryBLL.GetByID(ObjectT.ProductCategory);
                        allProduct.Unit = ObjectT.Unit;
                        allProduct.Productproperty = cate.Productproperty;
                    }
                    allProduct.KindID = ObjectT.SourceProductId;
                    

                }



            }
            else
            {
                if (ObjectT.ProductState != null && ObjectT.ProductState.Length > 3)
                {
                    //编辑1 ，报废1 、、。这种格式
                    ObjectT.ProductState = ObjectT.ProductState.Substring(0, 2);
                }
            }


            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SourceProductId;
            }
            return 0;
        }

        public int Modify(FD_StorehouseSourceProduct ObjectT)
        {
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 获取库房产品使用次数
        /// </summary>
        /// <param name="SourceProductId"></param>
        /// <returns></returns>
        public int GetUsedTimes(int SourceProductId, bool IsNew = false)
        {
            return new AllProducts().GetUsedTimes(SourceProductId, IsNew ? 3 : 2);
        }

        /// <summary>
        /// 获取库房产品使用个数
        /// </summary>
        /// <param name="SourceProductId"></param>
        /// <returns></returns>
        public int GetUsedCount(int SourceProductId, bool IsNew = false)
        {
            return new AllProducts().GetUsedCount(SourceProductId, IsNew ? 3 : 2);
        }

        /// <summary>
        /// 获取库房产品剩余个数
        /// </summary>
        /// <param name="SourceProductId"></param>
        /// <returns></returns>
        public int GetLeaveCount(int SourceProductId, bool IsNew = false)
        {
            return new AllProducts().GetLeaveCount(SourceProductId, IsNew ? 3 : 2);
        }

        public IEnumerable<FD_StorehouseSourceProduct> GetUsedProduct(bool IsNew = false)
        {
            var query = new AllProducts().GetUsedProducts(IsNew ? 3 : 2);
            return ObjEntity.FD_StorehouseSourceProduct.Join(query, C => C.SourceProductId, D => D.KindID, (C, D) => C).Distinct().ToList().Distinct((C, D) => C.SourceProductId == D.SourceProductId);
        }

        public IEnumerable<View_DispatchingCustomers> GetDispatchingCustomers(int PageSize, int PageIndex, out int SourceCount, ObjectParameter[] paras)
        {
            var query = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), paras).Distinct((C, D) => C.CustomerID.Equals(D.CustomerID));
            SourceCount = query.Count();
            return query.Count() > 0 ? query.OrderByDescending(C => C.PartyDate).Skip(PageSize * (PageIndex - 1)).Take(PageSize) : new List<View_DispatchingCustomers>();
        }

        public IEnumerable<FD_StorehouseSourceProduct> GetPagedProducts<S>(int PageSize, int PageIndex, out int SourceCount, bool isAsc, Func<FD_StorehouseSourceProduct, S> keySelector, Func<FD_StorehouseSourceProduct, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FD_StorehouseSourceProduct> query = PublicDataTools<FD_StorehouseSourceProduct>
                .GetDataByParameter(new FD_StorehouseSourceProduct(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate);
            SourceCount = query.Count();
            return isAsc ? query.OrderBy<FD_StorehouseSourceProduct, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize) :
                query.OrderByDescending<FD_StorehouseSourceProduct, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }

        public IQueryable<FD_StorehouseSourceProduct> Where(Func<FD_StorehouseSourceProduct, bool> predicate)
        {
            return ObjEntity.FD_StorehouseSourceProduct.Where(predicate).AsQueryable();
        }
        public List<FD_StorehouseSourceProduct> Where(Func<FD_StorehouseSourceProduct, bool> predicate, IEnumerable<ObjectParameter> paras)
        {
            return PublicDataTools<FD_StorehouseSourceProduct>.GetDataByParameter(new FD_StorehouseSourceProduct(), paras.ToArray()).Where(predicate).ToList();
        }


        #region 基于 Repositoy

        public IEnumerable<FD_StorehouseSourceProduct> GetStorehouseProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FD_StorehouseSourceProduct, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            List<FD_StorehouseSourceProduct> query = PublicDataTools<FD_StorehouseSourceProduct>.GetDataByParameter(new FD_StorehouseSourceProduct(), parameters.ToArray());
            totalCount = query.Count();

            return isAsc ? PageDataTools<FD_StorehouseSourceProduct>.GetPagedData(query.OrderBy(keySelector), pageSize, pageIndex) :
               PageDataTools<FD_StorehouseSourceProduct>.GetPagedData(query.OrderByDescending(keySelector), pageSize, pageIndex);
        }

        public IEnumerable<FD_StorehouseSourceProduct> GetStorehouseDisposibleProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FD_StorehouseSourceProduct, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            parameters.Add(new ObjectParameter("IsDisposible", true));
            return GetStorehouseProducts(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters);
        }

        public IEnumerable<FD_StorehouseSourceProduct> GetStorehouseNonDisposibleProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FD_StorehouseSourceProduct, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            parameters.Add(new ObjectParameter("IsDisposible", false));
            return GetStorehouseProducts(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters);
        }

        public DateTime? GetPutStoreDate(int sourceProductId)
        {
            FD_StorehouseSourceProduct fD_StorehouseSourceProduct = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.SourceProductId == sourceProductId).FirstOrDefault();
            if (fD_StorehouseSourceProduct != null)
            {
                return fD_StorehouseSourceProduct.PutStoreDate;
            }
            return null;
        }

        public bool IsDisposible(int productid)
        {
            FD_StorehouseSourceProduct fD_StorehouseSourceProduct = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.SourceProductId == productid).FirstOrDefault();
            if (fD_StorehouseSourceProduct != null)
            {
                return Convert.ToBoolean(fD_StorehouseSourceProduct.IsDisposible);
            }
            return false;
        }

        public List<FD_StorehouseSourceProduct> GetStorehouseProducts(List<ObjectParameter> parameters)
        {
            return PublicDataTools<FD_StorehouseSourceProduct>.GetDataByParameter(new FD_StorehouseSourceProduct(), parameters.ToArray());
        }

        public List<FD_StorehouseSourceProduct> GetStorehouseDisposibleProductsByYear(int year)
        {
            DateTime start = new DateTime(year, 1, 1);
            DateTime end = new DateTime(year, 12, 31, 23, 59, 59, 999);
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("IsDisposible", true));
            parameters.Add(new ObjectParameter("PutStoreDate_between", string.Format("{0},{1}", start, end)));
            return GetStorehouseProducts(parameters);
        }

        #region 产品个数的计算

        /// <summary>
        /// 获取库房产品在指定婚期的可分配个数。
        /// </summary>
        /// <param name="productid">产品 ID。</param>
        /// <param name="partyDate">婚期。</param>
        /// <param name="timespan">时间段字符串（中午、晚上）。</param>
        /// <returns></returns>
        public int GetAvailableCount(int productid, DateTime partyDate, string timespan)
        {
            return IsDisposible(productid) ?
                //1.一次性：库房数量 - 报价未进入执行的产品数量 - 执行婚期未过和已过的产品数量
                GetStoreCount(productid) - GetQuotedAndNonDispatchedCount(productid) - GetDispatchedCount(productid) :
                //2.非一次性：库房数量 - 婚期当天时间段的报价产品数量 - 婚期当天时间段的执行产品数量
                GetStoreCount(productid) - GetQuotedOnlyOnPartyDateCount(productid, partyDate, timespan) - GetDispatchedOnPartyDateCount(productid, partyDate, timespan);
        }

        public int GetAvailableCount(int productid, int customerid, string timespan)
        {
            FL_Customers fL_Customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == customerid).FirstOrDefault();
            return fL_Customers != null && fL_Customers.PartyDate.HasValue ? GetAvailableCount(productid, fL_Customers.PartyDate.Value, timespan) : 0;
        }

        /// <summary>
        /// 获取库房产品在指定婚期指定时间段的可分配个数。
        /// </summary>
        /// <param name="productid">产品 ID。</param>
        /// <param name="partyDate">婚期。</param>
        /// <returns></returns>
        public int GetAvailableCount(int productid, DateTime partyDate)
        {
            return IsDisposible(productid) ?
                //1.一次性：库房数量 - 报价未进入执行的产品数量 - 执行婚期未过和已过的产品数量
                GetStoreCount(productid) - GetQuotedAndNonDispatchedCount(productid) - GetDispatchedCount(productid) :
                //2.非一次性：库房数量 - 婚期当天的报价产品数量 - 婚期当天的执行产品数量
                GetStoreCount(productid) - GetQuotedOnlyOnPartyDateCount(productid, partyDate) - GetDispatchedOnPartyDateCount(productid, partyDate);
        }

        public int GetAvailableCount(int productid, int customerid)
        {
            FL_Customers fL_Customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == customerid).FirstOrDefault();
            return fL_Customers != null && fL_Customers.PartyDate.HasValue ? GetAvailableCount(productid, fL_Customers.PartyDate.Value) : 0;
        }

        /// <summary>
        /// 获取产品剩余库存数量。
        /// </summary>
        /// <param name="productid">产品id</param>
        /// <returns></returns>
        public int GetLeaveCount(int productid)
        {
            return IsDisposible(productid) ?
                //1.一次性：库房数量 - 婚期完毕的产品数量
                GetStoreCount(productid) - GetDispatchedCount(productid, true) :
                //2.非一次性：库房数量
                GetStoreCount(productid);
        }

        /// <summary>
        /// 获取产品库存个数。（录入产品时的个数）
        /// </summary>
        /// <param name="productid">产品id</param>
        /// <returns></returns>
        public int GetStoreCount(int productid)
        {
            FD_StorehouseSourceProduct fD_StorehouseSourceProduct = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.SourceProductId == productid).FirstOrDefault();
            return fD_StorehouseSourceProduct != null ? Convert.ToInt32(fD_StorehouseSourceProduct.SourceCount) : 0;
        }

        /// <summary>
        ///  获取执行表中指定产品的使用个数。
        /// </summary>
        /// <param name="productid">产品 id。</param>
        /// <returns></returns>
        public int GetDispatchedCount(int productid)
        {
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3).ToList().Sum(C => C.Quantity);
        }

        /// <summary>
        /// 获取执行表中指定产品的使用个数。
        /// </summary>
        /// <param name="productid">产品 id。</param>
        /// <param name="isPartyOver">指示婚期是否结束。</param>
        /// <returns></returns>
        public int GetDispatchedCount(int productid, bool isFinishOver)
        {
            DateTime today = DateTime.Now.Date;

            List<int> customersList = isFinishOver ?
                ObjEntity.FL_Customers.Where(C => C.PartyDate < today).Select(C => C.CustomerID).ToList() :
                ObjEntity.FL_Customers.Where(C => C.PartyDate >= today).Select(C => C.CustomerID).ToList();
            //List<int> customersList = ObjEntity.FL_Customers.Where(C =>C.FinishOver == isFinishOver).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && C.Expr6.HasValue).ToList().Where(C => customersList.Contains(C.Expr6.Value)).Sum(C => C.Quantity);
        }

        /// <summary>
        /// 获取执行表中指定产品的使用个数。
        /// </summary>
        /// <param name="productid">产品 id 。</param>
        /// <param name="isFinishOver">婚期是否结束</param>
        /// <param name="timespan">时间段字符串。（中午、晚上）</param>
        /// <returns></returns>
        public int GetDispatchedCount(int productid, bool isFinishOver, string timespan)
        {
            DateTime today = DateTime.Now.Date;

            List<int> customersList = isFinishOver ?
                ObjEntity.FL_Customers.Where(C => C.PartyDate < today && C.TimeSpans == timespan).Select(C => C.CustomerID).ToList() :
                ObjEntity.FL_Customers.Where(C => C.PartyDate >= today && C.TimeSpans == timespan).Select(C => C.CustomerID).ToList();

            //List<int> customersList = ObjEntity.FL_Customers.Where(C => C.TimeSpans.Contains(timespan) && C.FinishOver == isFinishOver).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && customersList.Contains(C.Expr6.Value)).ToList().Sum(C => C.Quantity);
        }

        /// <summary>
        /// 获取执行表中指定产品的使用个数。
        /// </summary>
        /// <param name="productid">产品 id 。</param>
        /// <param name="timespan">时间段字符串。（中午、晚上）</param>
        /// <returns></returns>
        public int GetDispatchedCount(int productid, string timespan)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.TimeSpans.Contains(timespan)).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && customersList.Contains(C.Expr6.Value)).ToList().Sum(C => C.Quantity);
        }


        /// <summary>
        /// 获取产品使用次数。
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public int GetUsedTimes(int productid)
        {
            DateTime today = DateTime.Now.Date;
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate < today).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && customersList.Contains(C.Expr6.Value)).Count();
        }

        /// <summary>
        /// 获取未进入执行的报价单产品数量。
        /// </summary>
        /// <param name="productid">产品id</param>
        /// <returns></returns>
        public int GetQuotedAndNonDispatchedCount(int productid)
        {
            List<int> quotedIDList = ObjEntity.FL_QuotedPrice.Where(C => C.IsDispatching > 0 && C.IsDispatching <= 3).Select(C => C.QuotedID).ToList();
            return ObjEntity.FL_QuotedPriceItems.Where(C => quotedIDList.Contains(C.QuotedID) && C.RowType == 2 && C.ItemLevel == 3 && C.Quantity.HasValue && C.ProductID == productid).ToList().Sum(C => C.Quantity.Value);
        }

        public int GetQuotedAndNonDispatchedCount(int productid, string timespan)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.TimeSpans.Contains(timespan)).Select(C => C.CustomerID).ToList();
            List<int> quotedIDList = ObjEntity.FL_QuotedPrice.Where(C => C.IsDispatching > 0 && C.IsDispatching <= 3 && customersList.Contains(C.CustomerID.Value)).Select(C => C.QuotedID).ToList();
            return ObjEntity.FL_QuotedPriceItems.Where(C => quotedIDList.Contains(C.QuotedID) && C.RowType == 2 && C.ItemLevel == 3 && C.Quantity.HasValue && C.ProductID == productid).ToList().Sum(C => C.Quantity.Value);
        }


        /// <summary>
        /// 获取执行的库房产品中指定婚期前，指定产品使用的数量。
        /// </summary>
        /// <param name="productid">产品id</param>
        /// <param name="partyDate">婚期</param>
        /// <returns></returns>
        public int GetDispatchedBeforePartyCount(int productid, DateTime partyDate)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate <= partyDate).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && C.Expr6.HasValue).ToList().Where(C => customersList.Contains(C.Expr6.Value)).Sum(C => C.Quantity);
        }

        public int GetDispatchedBeforePartyCount(int productid, DateTime partyDate, string timespan)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate <= partyDate && C.TimeSpans.Contains(timespan)).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && C.Expr6.HasValue).ToList().Where(C => customersList.Contains(C.Expr6.Value)).Sum(C => C.Quantity);
        }

        /// <summary>
        /// 获取婚期当天执行的产品数量。
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="partyDate"></param>
        /// <returns></returns>
        public int GetDispatchedOnPartyDateCount(int productid, DateTime partyDate)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate == partyDate).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && C.Expr6.HasValue).ToList().Where(C => customersList.Contains(C.Expr6.Value)).Sum(C => C.Quantity);
        }

        public int GetDispatchedOnPartyDateCount(int productid, DateTime partyDate, string timespan)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate == partyDate && C.TimeSpans.Contains(timespan)).Select(C => C.CustomerID).ToList();
            return ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && C.Expr6.HasValue).ToList().Where(C => customersList.Contains(C.Expr6.Value)).Sum(C => C.Quantity);
        }

        /// <summary>
        /// 获取婚期当天并且未进入执行的报价的产品数量。
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="partyDate"></param>
        /// <returns></returns>
        public int GetQuotedOnlyOnPartyDateCount(int productid, DateTime partyDate)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate == partyDate).Select(C => C.CustomerID).ToList();
            List<int> quotedIDList = ObjEntity.FL_QuotedPrice.Where(C => C.IsDispatching > 0 && C.IsDispatching <= 3 && customersList.Contains(C.CustomerID.Value)).Select(C => C.QuotedID).ToList();
            return ObjEntity.FL_QuotedPriceItems.Where(C => quotedIDList.Contains(C.QuotedID) && C.RowType == 2 && C.ItemLevel == 3 && C.Quantity.HasValue).ToList().Sum(C => C.Quantity.Value);
        }

        public int GetQuotedOnlyOnPartyDateCount(int productid, DateTime partyDate, string timespan)
        {
            List<int> customersList = ObjEntity.FL_Customers.Where(C => C.PartyDate == partyDate && C.TimeSpans.Contains(timespan)).Select(C => C.CustomerID).ToList();
            List<int> quotedIDList = ObjEntity.FL_QuotedPrice.Where(C => C.IsDispatching > 0 && C.IsDispatching <= 3 && customersList.Contains(C.CustomerID.Value)).Select(C => C.QuotedID).ToList();
            return ObjEntity.FL_QuotedPriceItems.Where(C => quotedIDList.Contains(C.QuotedID) && C.RowType == 2 && C.ItemLevel == 3 && C.Quantity.HasValue).ToList().Sum(C => C.Quantity.Value);
        }

        /// <summary>
        /// 获取最近一次使用产品的新人的婚期。
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public string GetLastUsedDate(int productid)
        {
            IEnumerable<int> customersList = ObjEntity.FL_DispatchAllProducts.Where(C => C.KindID == productid && C.RowType == 2 && C.ItemLevel == 3 && C.Expr6.HasValue).ToList().Select(C => C.Expr6.Value).Distinct();
            FL_Customers fL_Customers = ObjEntity.FL_Customers.Where(C => customersList.Contains(C.CustomerID) && C.PartyDate.HasValue).ToList().OrderByDescending(C => C.PartyDate.Value).FirstOrDefault();
            return fL_Customers != null ? fL_Customers.PartyDate.Value.ToString("yyyy-MM-dd") + fL_Customers.TimeSpans : "暂未使用";
        }
        #endregion

        #endregion
    }
}
