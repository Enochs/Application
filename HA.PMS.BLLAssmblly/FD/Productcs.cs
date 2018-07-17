
/**
 Version :HaoAi 1.0
 File Name :Productcs
 Author:杨洋
 Date:2013.3.17
 Description:产品 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Entity.Validation;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.FD
{
    public class Productcs : ICRUDInterface<FD_Product>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 删除产品列表
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_Product ObjectT)
        {
            if (ObjectT != null)
            {
                FD_Product objProduct = GetByID(ObjectT.ProductID);
                AllProducts objAllProductsBLL = new AllProducts();
                objAllProductsBLL.Delete(ObjectT.ProductID, 1);
                objProduct.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 返回产品列表
        /// </summary>
        /// <returns></returns>
        public List<FD_Product> GetByAll()
        {
            return ObjEntity.FD_Product.Where(C => C.IsDelete == false).ToList();
        }




        /// <summary>
        /// 获取数组范围内的产品
        /// </summary>
        /// <returns></returns>
        public List<FD_Product> GetinKeyList(int[] ObjKeyList)
        {

            return (from C in ObjEntity.FD_Product
                    where (ObjKeyList).Contains(C.ProductID)
                    select C).ToList();
        }

        /// <summary>
        /// 待分配产品
        /// </summary>
        /// <returns></returns>
        public List<View_DispatchingOrder> GetDispatch()
        {

            return ObjEntity.View_DispatchingOrder.ToList();
        }



        /// <summary>
        /// 根据类型获取产品
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public List<FD_Product> GetbyCategoryID(int? CategoryID)
        {
            return ObjEntity.FD_Product.Where(C => C.CategoryID == CategoryID).ToList();
        }
        /// <summary>
        /// 返回单个产品
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_Product GetByID(int? KeyID)
        {
            return ObjEntity.FD_Product.FirstOrDefault(C => C.ProductID == KeyID);
        }
        /// <summary>
        /// 分页获取产品信息(含 Product  ， Category ， Supplier 三个表的视图)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_ProductCategorySupplier> GetFD_ProductCategorySupplierByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ProductCategorySupplier.Count();

            List<FD_ProductCategorySupplier> resultList = ObjEntity.FD_ProductCategorySupplier.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ProductID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_ProductCategorySupplier>();
            }
            return resultList;
        }

        /// <summary>
        /// 提供供应商产品明细
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_SupplierProductQuery> GetFD_SupplierProductQuerybyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_SupplierProductQuery>.GetDataByParameter(new FD_SupplierProductQuery(), ObjParameterList);
            SourceCount = query.Where(C => C.IsDelete == false && C.Expr3 == false).Count();

            List<FD_SupplierProductQuery> resultList = query.Where(C => C.IsDelete == false && C.Expr3 == false).OrderByDescending(C => C.ProductID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_SupplierProductQuery>();
            }
            return resultList;



        }


        /// <summary>
        /// 提供供应商产品明细 不包括 待分配供应商的产品
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_SupplierProductQuery> GetSupplierProductByParameterWithoutUnSignIn(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            int CategoryID = 0;
            Category ObjCategoryBLL = new Category();
            var ObjCategoryQueryModel = new BLLAssmblly.FD.Category().GetByAll().Where(C => C.CategoryName.Contains("待分配产品")).FirstOrDefault();
            if (ObjCategoryQueryModel == null)
            {
                CategoryID = ObjCategoryBLL.Insert(new FD_Category { CategoryName = "待分配产品", ParentID = 0, SortOrder = "9999", IsDelete = false });
            }
            else
            {
                CategoryID = ObjCategoryQueryModel.CategoryID;
            }

            var query = PublicDataTools<FD_SupplierProductQuery>.GetDataByParameter(new FD_SupplierProductQuery(), ObjParameterList);
            SourceCount = query.Where(C => C.IsDelete == false && C.Expr3 == false && C.Expr1 != CategoryID).Count();

            List<FD_SupplierProductQuery> resultList = query.Where(C => C.IsDelete == false && C.Expr3 == false && C.Expr1 != CategoryID).OrderByDescending(C => C.ProductID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_SupplierProductQuery>();
            }
            return resultList;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_Product ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_Product.Add(ObjectT);
                ObjEntity.SaveChanges();
                if (ObjectT.ProductID > 0)
                {
                    FD_Supplier supplier=  new Supplier().GetByID(ObjectT.SupplierID);
                    
                    FD_AllProducts allProduct = new FD_AllProducts();
                    allProduct.Count = ObjectT.Count;
                    allProduct.SupplierName = supplier != null ? supplier.Name : string.Empty;
                    allProduct.Data = ObjectT.Data;
                    allProduct.Explain = ObjectT.Explain;
                    allProduct.IsDelete = false;
                    allProduct.KindID = ObjectT.ProductID;
                    allProduct.ProductCategory = ObjectT.CategoryID;
                    allProduct.ProjectCategory = ObjectT.ProductProject;
                    allProduct.PurchasePrice = ObjectT.ProductPrice;
                    allProduct.SalePrice = ObjectT.SalePrice;
                    allProduct.Specifications = ObjectT.Specifications;
                    allProduct.ProductName = ObjectT.ProductName;
                    allProduct.Type = 1;//供应商
                    allProduct.Unit = ObjectT.Unit;
                    allProduct.Remark = ObjectT.Remark;
                    Category objCategoryBLL = new Category();
                    FD_Category cate = objCategoryBLL.GetByID(ObjectT.CategoryID);
                    allProduct.Productproperty = cate.Productproperty;

                    ObjEntity.FD_AllProducts.Add(allProduct);
                   return  ObjEntity.SaveChanges();
                }
            }
            
            return 0;
        }

        [Obsolete("请使用 All_Products 中的 GetUsedTimes 方法")]
        public int GetOperCountByProductId(int productId)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.ProductID == productId).Count();
        }

        /// <summary>
        /// 修改产品ID
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_Product ObjectT)
        {
            FD_AllProducts allProduct = ObjEntity.FD_AllProducts.Where(C => C.KindID == ObjectT.ProductID && C.Type ==1).FirstOrDefault();
            if (allProduct != null)
            {


                allProduct.Count = ObjectT.Count;
                allProduct.Data = ObjectT.Data;
                allProduct.Explain = ObjectT.Explain;
                allProduct.IsDelete = false;
                allProduct.KindID = ObjectT.ProductID;
                allProduct.ProductCategory = ObjectT.CategoryID;
                allProduct.ProjectCategory = ObjectT.ProductProject;
                allProduct.PurchasePrice = ObjectT.ProductPrice;
                allProduct.SalePrice = ObjectT.SalePrice;
                allProduct.Specifications = ObjectT.Specifications;
                allProduct.ProductName = ObjectT.ProductName;
                allProduct.Type = 1;//供应商
                allProduct.Unit = ObjectT.Unit;
                allProduct.Remark = ObjectT.Remark;
                Category objCategoryBLL = new Category();
                FD_Category cate = objCategoryBLL.GetByID(ObjectT.CategoryID);
                allProduct.Productproperty = object.ReferenceEquals(cate, null) ? 2 : cate.Productproperty;//2 为待分配供应商产品的属性占位
            }
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ProductID;
            }
            return 0;
        }
        /// <summary>
        /// 通过供应商来查处对应的所有产品
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_Product> GetByIndex(int supplierId, int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.FD_Product.Where(C => C.IsDelete == false && C.SupplierID == supplierId).Count();

            List<FD_Product> resultList = ObjEntity.FD_Product.Where(C => C.IsDelete == false && C.SupplierID == supplierId)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ProductID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Product>();
            }
            return resultList;
        }

        /// <summary>
        /// 通过Category来查处对应的所有产品
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_Product> GetByIndexOfCategoryID(int CategoryID, int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.FD_Product.Where(C => C.IsDelete == false && C.CategoryID == CategoryID).Count();

            List<FD_Product> resultList = ObjEntity.FD_Product.Where(C => C.IsDelete == false && C.CategoryID == CategoryID)
                   .OrderByDescending(C => C.ProductID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Product>();
            }
            return resultList;
        }

        /// <summary>
        /// 通过供应商来查处对应的所有产品
        /// </summary>
        public List<FD_Product> GetProductBySupplierID(int supplierId)
        {

            var query = ObjEntity.FD_Product.Where(C => C.IsDelete == false && C.SupplierID == supplierId);


            return query.ToList();
        }
        public List<FD_Product> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.FD_Product.Where(C => C.IsDelete == false).Count();

            List<FD_Product> resultList = ObjEntity.FD_Product.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ProductID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Product>();
            }
            return resultList;
        }

        /// <summary>
        /// 获取供应商产品使用次数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetUsedTimes(int ProductID)
        {
            return new AllProducts().GetUsedTimes(ProductID, 1);
        }

        /// <summary>
        /// 获取供应商产品使用个数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetUsedCount(int ProductID)
        {
            return new AllProducts().GetUsedCount(ProductID, 1);
        } 


        /// <summary>
        /// 获取供应商产品剩余个数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns> 
        public int GetLeaveCount(int ProductID)
        {
            return new AllProducts().GetLeaveCount(ProductID, 1);
        }

        /// <summary>
        /// 获取供应商产品总数。
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetTotalCount(int ProductID)
        {
            return new AllProducts().GetTotalCount(ProductID, 1);
        }

        public IEnumerable<View_GetSupplierProduct> GetUsedProduct()
        {
            var query = new AllProducts().GetUsedProducts(1);
            return ObjEntity.View_GetSupplierProduct.Join(query, C => C.ProductID, D => D.KindID, (C, D) => C).Distinct().ToList().Distinct((C, D) => C.ProductID == D.ProductID);
        }
    }
}
