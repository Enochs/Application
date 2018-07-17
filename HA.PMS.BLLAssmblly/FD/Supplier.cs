using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.BLLAssmblly.FD
{
    public class Supplier : ICRUDInterface<FD_Supplier>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 删除供应商信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_Supplier ObjectT)
        {
            if (ObjectT != null)
            {
                FD_Supplier objSupplie = GetByID(ObjectT.SupplierID);

                objSupplie.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 返回所有供应商信息
        /// </summary>
        /// <returns></returns>
        public List<FD_Supplier> GetByAll()
        {
            return ObjEntity.FD_Supplier.Where(C => C.IsDelete == false).ToList();
        }
        /// <summary>
        /// 返回供应商供应次数
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public int GetSupperlierBySupplierIDCount(int supplierID)
        {
            List<int> listProject = ObjEntity.FD_SupplierProductCount.Select(C => C.SupplierID).ToList();
            int count = 0;
            for (int i = 0; i < listProject.Count; i++)
            {
                if (listProject[i] == supplierID)
                {
                    count++;
                }
            }
            return count;
            //
        }
        /// <summary>
        /// 返回单个供应商
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_Supplier GetByID(int? KeyID)
        {
            return ObjEntity.FD_Supplier.FirstOrDefault(C => C.SupplierID == KeyID);
        }

        public FD_Supplier GetByName(string Name)
        {
            return ObjEntity.FD_Supplier.FirstOrDefault(C => C.Name == Name);
        }


        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_Supplier> GetSupplierbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_Supplier>.GetDataByParameter(new FD_Supplier(), ObjParameterList);
            SourceCount = query.Where(C => C.IsDelete == false).Count();
            List<FD_Supplier> resultList = query.Where(C => C.IsDelete == false).OrderByDescending(C => C.SupplierID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
            if (query.Count == 0)
            {
                resultList = new List<FD_Supplier>();
            }
            return resultList;
        }
        /// <summary>
        /// 分页返回供应商信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_Supplier> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_Supplier.Where(C => C.IsDelete == false).Count();

            List<FD_Supplier> resultList = ObjEntity.FD_Supplier
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SupplierID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Supplier>();
            }
            return resultList;
        }

        /// <summary>
        /// 添加供应商信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_Supplier ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_Supplier.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.SupplierID;
                }

            }
            return 0;
        }

        /// <summary>
        /// 返回所有供应商的产品
        /// </summary>
        /// <returns></returns>
        public List<FD_SupplierProductQuery> GetFD_SupplierProductQueryAll()
        {

            return ObjEntity.FD_SupplierProductQuery.ToList();
        }

        /// <summary>
        /// 通过一组产品ID在通过供应商和产品的视图 返回最终查询结果
        /// </summary>
        /// <param name="supplierType"></param>
        /// <param name="supplierName"></param>
        /// <param name="products"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_SupplierProductQuery> GetSupplierProductByProductIds(int supplierType, string supplierName, IList<int> products, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = ObjEntity.FD_SupplierProductQuery.ToList();

            List<FD_SupplierProductQuery> resultList = query.ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_SupplierProductQuery>();
            }
            List<FD_SupplierProductQuery> FD_SupplierProductQuery = new List<FD_SupplierProductQuery>();
            foreach (var item in products)
            {
                FD_SupplierProductQuery currentQuery = resultList.Where(C => C.ProductID == item).FirstOrDefault();
                if (currentQuery != null)
                {
                    FD_SupplierProductQuery.Add(currentQuery);
                }

            }


            //根据参入的参数查询条件进行刷选
            if (supplierType != 0)
            {
                FD_SupplierProductQuery = FD_SupplierProductQuery
                    .Where(C => C.CategoryID == supplierType).ToList();
            }
            if (supplierName != "")
            {
                FD_SupplierProductQuery = FD_SupplierProductQuery
                   .Where(C => C.Name == supplierName).ToList();
            }

            //开始分页
            SourceCount = query.Where(C => C.IsDelete == false).Count();

            List<FD_SupplierProductQuery> finlishResult = FD_SupplierProductQuery.OrderByDescending(C => C.SupplierID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_SupplierProductQuery>();
            }

            return finlishResult;



        }

        /// <summary>
        /// 执行表联查
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_DispatchAllProducts> GetbyDispatchParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FL_DispatchAllProducts>.GetDataByParameter(new FL_DispatchAllProducts(), ObjParameterList);

            return query;
        }


        /// <summary>
        /// 返回所有的物料执行表中的客户信息以及相关物流信息
        /// </summary>
        /// <returns></returns>
        public List<FD_SupplierDispatchCustomer> GetSupplierDispatchByTypeOne(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FD_SupplierDispatchCustomer>.GetDataByParameter(new FD_SupplierDispatchCustomer(), ObjParameterList);


            List<FD_SupplierDispatchCustomer> resultList = query.ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_SupplierDispatchCustomer>();
            }
            return resultList;

        }
        /// <summary>
        /// 修改供应商信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_Supplier ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SupplierID;
            }
            return 0;
        }


        /// <summary>
        /// 获取供应商产品种类个数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public int GetProductCount(int SupplierID)
        {
            return ObjEntity.FD_Product.Where(C => C.SupplierID == SupplierID).Count();
        }

        /// <summary>
        /// 获取供应商所有产品的总个数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public int GetProductsTotalCount(int SupplierID)
        {
            int reslut = 0;
            foreach (var item in ObjEntity.FD_Product.Where(C => C.SupplierID == SupplierID))
            {
                if (item.Count.HasValue)
                {
                    reslut += item.Count.Value;
                }
            }
            return reslut;
        }

        /// <summary>
        /// 获取供应商所有产品的总供货次数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public int GetProductsUsedTimes(int SupplierID)
        {
            int result = 0;
            AllProducts allProducts = new AllProducts();
            foreach (var item in ObjEntity.FD_Product.Where(C => C.SupplierID == SupplierID))
            {
                result += allProducts.GetUsedTimes(item.ProductID, 1);
            }
            return result;
        }

        /// <summary>
        /// 获取供应商所有产品的供货次数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public int GetProductsUsedTimes(int SupplierID, int year, int month)
        {
            int result = 0;
            AllProducts allProducts = new AllProducts();
            DateTime tmp = new DateTime(year, month, 1);
            foreach (var item in ObjEntity.FD_Product.Where(C => C.SupplierID == SupplierID))
            {
                result += allProducts.GetUsedTimes(item.ProductID, 1, tmp, tmp.AddMonths(1));
            }
            return result;
        }

        /// <summary>
        /// 获取供应商差错次数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public int GetErroStateCount(int SupplierID)
        {
            return ObjEntity.FL_OrderAppraise.Count(C => C.SupplierID.HasValue && C.SupplierID.Value == SupplierID && C.ErroState == 1);
        }

        /// <summary>
        /// 获取供应商评价满意度
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public string GetAveragePoint(int SupplierID)
        {
            List<FL_OrderAppraise> fL_OrderAppraiseList = ObjEntity.FL_OrderAppraise.Where(C => C.SupplierID.HasValue && C.SupplierID.Value == SupplierID && C.Point.HasValue).ToList();
            if (fL_OrderAppraiseList.Count > 0)
            {
                double Point = fL_OrderAppraiseList.Average(C => C.Point.Value);
                switch (Convert.ToInt32(Math.Ceiling(Point)))
                {
                    case 1: return "不好";
                    case 2: return "一般";
                    case 3: return "较好";
                    case 4: return "很好";
                    default: return string.Empty;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取供应商结算额
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public decimal GetOrderfinalCost(int SupplierID, Func<FL_OrderfinalCost, bool> predicate)
        {
            //return ObjEntity.FL_OrderfinalCost.Where(C => C.KindType == 1 && C.SupplierID == SupplierID).Where(predicate).Sum(C => C.ActualExpenditure);
            decimal result = 0;
            List<FL_OrderfinalCost> fL_OrderfinalCostList = ObjEntity.FL_OrderfinalCost.Where(C => C.KindType == 1).Where(predicate).ToList();
            Supplier supplier = new Supplier();
            foreach (FL_OrderfinalCost fL_OrderfinalCost in fL_OrderfinalCostList)
            {
                if (fL_OrderfinalCost.SupplierID.HasValue)
                {
                    if (fL_OrderfinalCost.SupplierID.Value == SupplierID)
                    {
                        result += fL_OrderfinalCost.ActualExpenditure;
                    }
                }
                else
                {
                    FD_Supplier fD_Supplier = supplier.GetByName(fL_OrderfinalCost.ServiceContent);
                    if (fD_Supplier != null && fD_Supplier.SupplierID == SupplierID)
                    {
                        result += fL_OrderfinalCost.ActualExpenditure;
                    }
                    else if (fL_OrderfinalCost.ServiceContent.Equals("新购入"))
                    {
                    }
                }
            }
            return result;
        }


        //[FD_Supplier]
        public IEnumerable<FD_Supplier> GetPagedData(int PageSize, int PageIndex, out int SourceCount, Func<FD_Supplier, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FD_Supplier> query = PublicDataTools<FD_Supplier>
                .GetDataByParameter(new FD_Supplier(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.SupplierID);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }
        public List<FD_Supplier> Where(Func<FD_Supplier, bool> predicate)
        {
            return ObjEntity.FD_Supplier.Where(predicate).ToList();
        }
        public FD_Supplier FirstOrDefault(Func<FD_Supplier, bool> predicate)
        {
            return ObjEntity.FD_Supplier.FirstOrDefault(predicate);
        }

        //[FD_SupplierProductQuery]
        public IEnumerable<FD_SupplierProductQuery> GetPagedCustomerQuoted(int PageSize, int PageIndex, out int SourceCount, Func<FD_SupplierProductQuery, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FD_SupplierProductQuery> query = PublicDataTools<FD_SupplierProductQuery>
                .GetDataByParameter(new FD_SupplierProductQuery(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.SupplierID);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }
        public IEnumerable<FD_SupplierProductQuery> GetCustomerQuoted(Func<FD_SupplierProductQuery, bool> predicate)
        {
            return ObjEntity.FD_SupplierProductQuery.Where(predicate);
        }


        public IEnumerable<FL_ProductforDispatching> GetHaveOrderfinalCostProductforDispatching(int PageSize, int PageIndex, out int SourceCount, IEnumerable<ObjectParameter> paras = null)
        {
            List<int> SupplierIDs = new OrderfinalCost().Where(C => C.SupplierID.HasValue && C.SupplierID.Value > 0 && C.KindType == 1).Select(C => C.SupplierID.Value).ToList();
            return new AllProducts().GetPagedProductforDispatching(PageSize, PageIndex, out SourceCount, C => SupplierIDs.Contains(C.SupplierID.Value), paras);
        }


        public List<FD_Supplier> GetSupplierGroup(int PageSize, int CurrentPageIndex, List<PMSParameters> pars, ref int SourceCount)
        {
            return PublicDataTools<FD_Supplier>.GetSupplierGroup(PageSize, CurrentPageIndex, pars, ref SourceCount);
        }

        public string GetSuppierById(int SupplierId, List<PMSParameters> pars, int Type)
        {
            return PublicDataTools<string>.GetSupplierById(SupplierId, pars, Type);
        }

        public List<FD_Supplier> GetByCategoryId(int CategoryId)
        {
            return ObjEntity.FD_Supplier.Where(C => C.CategoryID == CategoryId).ToList();
        }

        /// <summary>
        /// 本期合计
        /// </summary>
        public string GetAllMoneySum(List<PMSParameters> pars, int Type, string Category)       //供应商管理  本期合计
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = PublicDataTools<string>.GetWhere(pars);
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            int sourceCount = 0;
            decimal SumMoney = 0;
            List<FD_Supplier> DataList = PublicDataTools<string>.GetSupplierGroup(10000, 1, pars, ref sourceCount);
            List<FD_FourGuardian> GuardList = PublicDataTools<string>.GetGuardianGroup(10000, 1, pars, ref sourceCount);
            List<Sys_Employee> EmployeeList = PublicDataTools<string>.GetEmployeeGroup(10000, 1, pars, ref sourceCount);
            List<FL_Statement> OutPersonList = PublicDataTools<string>.GetOutEmployeeGroup(10000, 1, pars, ref sourceCount);
            if (Category == "供应商")
            {
                foreach (var item in DataList)
                {
                    if (Type == 1)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.SupplierID, 1, WhereStr).ToList().FirstOrDefault().SubTotal.ToString().ToDecimal();
                    }
                    else if (Type == 2)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.SupplierID, 1, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString().ToDecimal();
                    }
                    else if (Type == 3)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.SupplierID, 1, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString().ToDecimal();
                    }
                }
            }
            else if (Category == "四大金刚")
            {
                foreach (var item in GuardList)
                {
                    if (Type == 1)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.GuardianId, 4, WhereStr).ToList().FirstOrDefault().SubTotal.ToString().ToDecimal();
                    }
                    else if (Type == 2)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.GuardianId, 4, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString().ToDecimal();
                    }
                    else if (Type == 3)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.GuardianId, 4, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString().ToDecimal();
                    }
                }
            }
            else if (Category == "内部人员")
            {
                foreach (var item in EmployeeList)
                {
                    if (Type == 1)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.EmployeeID, 5, WhereStr).ToList().FirstOrDefault().SubTotal.ToString().ToDecimal();
                    }
                    else if (Type == 2)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.EmployeeID, 5, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString().ToDecimal();
                    }
                    else if (Type == 3)
                    {
                        SumMoney += ObjEntity.GetSuppliersById(item.EmployeeID, 5, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString().ToDecimal();
                    }
                }
            }
            else if (Category == "外部人员")
            {
                foreach (var item in OutPersonList)
                {
                    if (Type == 1)
                    {
                        SumMoney = ObjEntity.GetSuppliersById(item.SupplierID, 5, WhereStr).ToList().Sum(C => C.SubTotal).ToString().ToDecimal();
                    }
                    else if (Type == 2)
                    {
                        SumMoney = ObjEntity.GetSuppliersById(item.SupplierID, 5, WhereStr).ToList().Sum(C => C.SubPayMent).ToString().ToDecimal();
                    }
                    else if (Type == 3)
                    {
                        SumMoney = ObjEntity.GetSuppliersById(item.SupplierID, 5, WhereStr).ToList().Sum(C => C.SubNoPayMent).ToString().ToDecimal();
                    }
                }
            }
            return SumMoney.ToString();
        }
    }
}
