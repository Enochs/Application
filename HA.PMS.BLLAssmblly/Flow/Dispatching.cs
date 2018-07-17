using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
///总派工表

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Dispatching : ICRUDInterface<FL_Dispatching>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 数据查询
        /// <summary>
        /// 根据总表ID获取一级分类
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FL_CategoryforDispatching> GetCategoryByDispatching(int? KeyID)
        {
            return ObjEntity.FL_CategoryforDispatching.Where(C => C.DispatchingID == KeyID).ToList();
        }




        /// <summary>
        /// 获取本人的新订单
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetforMineByNew(int? EmpLoyeeID, int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList)
        {
            ProductforDispatching objProductforDispatchingBLL = new ProductforDispatching();
            var ObjNewListModel = objProductforDispatchingBLL.GetByEmpLoyeeID(EmpLoyeeID, true);
            if (ObjNewListModel.Count > 0)
            {
                List<int> ObjKeyList = new List<int>();
                foreach (var ObjNewItem in ObjNewListModel)
                {
                    ObjKeyList.Add(ObjNewItem.DispatchingID);
                }
                ObjKeyList = ObjKeyList.Distinct().ToList();

                var DataSource = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParList.ToArray());
                DataSource.AddRange(this.GetByKeyList(ObjKeyList.ToArray()));
                //DataSource = DataSource.Where(C => C.IsBegin==false).ToList();
                SourceCount = DataSource.Count;
                // DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageIndex).ToList();

                if (SourceCount == 0)
                {
                    DataSource = new List<View_DispatchingCustomers>();
                }

                return DataSource;
            }
            SourceCount = 0;
            return new List<View_DispatchingCustomers>();
        }


        /// <summary>
        /// 获取我的正在执行的订单
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetforMineByDoing(int? EmpLoyeeID, int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList)
        {
            ProductforDispatching objProductforDispatchingBLL = new ProductforDispatching();
            var ObjNewListModel = objProductforDispatchingBLL.GetByEmpLoyeeID(EmpLoyeeID, false);
            if (ObjNewListModel.Count > 0)
            {
                List<int> ObjKeyList = new List<int>();
                var DataSource = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
                //  var DataList= new List<View_DispatchingCustomers>();
                foreach (var ObjNewItem in ObjNewListModel)
                {
                    ObjKeyList.Add(ObjNewItem.DispatchingID);
                    //var DisModel = this.GetByKeyList(ObjKeyList.ToArray());
                    //if (DisModel.Count > 0)
                    //{
                    //    View_DispatchingCustomers ObjKA = new View_DispatchingCustomers();
                    //    ObjKA = DisModel[0];
                    //    ObjKA.EmployeeID = ObjNewItem.EmployeeID;
                    //  //  DisModel[0].EmployeeID = ObjNewItem.EmployeeID;
                    //    DataSource.Add(ObjKA);
                    //    DataList.Add(ObjKA);
                    //}
                }
                ObjKeyList = ObjKeyList.Distinct().ToList();


                DataSource.AddRange(this.GetByKeyList(ObjKeyList.ToArray()));
                // DataSource = DataSource.Where(C => C.IsBegin == true).ToList();
                SourceCount = DataSource.Count;
                // DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageIndex).ToList();

                if (SourceCount == 0)
                {
                    DataSource = new List<View_DispatchingCustomers>();
                }

                return DataSource.Distinct(new KeyClassEquers()).ToList();
            }
            else
            {
                SourceCount = 0;
                return this.GetDispatchingCustomersByWhere(PageSize, PageIndex, out SourceCount, EmployeeID, ObjParList).Distinct(new KeyClassEquers()).ToList();
            }

        }

        //[View_GetSupplierforCarrytask]
        /// <summary>
        /// 获取订单合作供应商
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<View_GetSupplierforCarrytask> GetSupplierforCarrytask(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_GetSupplierforCarrytask>.GetDataByParameter(new View_GetSupplierforCarrytask(), ObjParList.ToArray()).ToList();
            SourceCount = DataSource.Count;
            PageIndex = PageIndex - 1;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetSupplierforCarrytask>();
            }
            return PageDataTools<View_GetSupplierforCarrytask>.AddtoPageSize(DataSource);
        }

        [Obsolete]
        /// <summary>
        /// 获取订单合作供应商（Distinct）
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetSupplierforCarrytaskDistinct(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList, DateTime PartyDateStart, DateTime PartDateEnd)
        {

            var DataSource = PublicDataTools<FL_ProductforDispatching>.GetDataByParameter(new FL_ProductforDispatching(), ObjParList.ToArray()).Where(C => C.RowType == 1 && C.SupplierID > 0).ToList();

            var ObjDispatchingBLL = new Dispatching();
            //补全信息
            foreach (var Item in DataSource)
            {
                Item.CustomerID = ObjDispatchingBLL.GetByID(Item.DispatchingID).CustomerID;
                Item.OrderID = ObjDispatchingBLL.GetByID(Item.DispatchingID).OrderID;
            }
            //过滤重复
            var query = DataSource.Distinct((C, D) => (C.OrderID.Equals(D.OrderID)) && (C.SupplierID.Equals(D.SupplierID)));

            //根据婚期过滤
            var ObjCustomerList = new Customers().GetByAll().Where(C => C.PartyDate.Value.CompareTo(PartyDateStart) >= 0 && C.PartyDate.Value.CompareTo(PartDateEnd) <= 0);
            List<int> CustomerIDList = new List<int>();
            foreach (var Item in ObjCustomerList)
            {
                CustomerIDList.Add(Item.CustomerID);
            }
            var result = query.Where(C => CustomerIDList.Contains(C.CustomerID.Value)).ToList();

            SourceCount = result.Count;
            PageIndex = PageIndex - 1;
            DataSource = result.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<FL_ProductforDispatching>();
            }
            return PageDataTools<FL_ProductforDispatching>.AddtoPageSize(DataSource);
        }

        /// <summary>
        /// 根据总表ID获取产品
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductByDispatching(int? KeyID, int? ParentCategoryID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == KeyID && C.CategoryID == ParentCategoryID).ToList();
        }




        #region Obsolete
        [Obsolete("请使用 GetDispatchingCustomers(Func<FL_DispatchingCustomer, bool> predicate)")]
        /// <summary>
        /// 根据订单ID获取数据(in查询)
        /// </summary>
        /// <param name="ObjKeyList"></param>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetByKeyList(int[] ObjKeyList)
        {
            return (from C in ObjEntity.View_DispatchingCustomers
                    where (ObjKeyList).Contains(C.DispatchingID)
                    select C).ToList();
        }
        [Obsolete("请使用 GetDispatchingCustomers(Func<FL_DispatchingCustomer, bool> predicate)")]
        /// <summary>
        /// 根据OrderID组获取数据
        /// </summary>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetByOrderKeysList(int[] ObjKeyList)
        {
            return (from C in ObjEntity.View_DispatchingCustomers
                    where (ObjKeyList).Contains(C.OrderID)
                    select C).ToList();
        }

        [Obsolete("请使用 GetPagedDispatchingCustomers（……）")]
        /// <summary>
        /// 根据用户ID获取派工总表
        /// </summary>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetDispatchingCustomersByEmpLoyeeIDandState(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, CustomerStates States)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.View_GetOrderCustomers.Count(C => C.State == (int)States);

            List<View_DispatchingCustomers> resultList = ObjEntity.View_DispatchingCustomers.
                Where(C => C.State == (int)States && C.EmployeeID == EmployeeID)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_DispatchingCustomers>();
            }
            return resultList;

        }


        public List<View_DispatchingCustomers> GetDispatchingCustomersByWhere(List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParList.ToArray());

            return DataSource;
        }

        /// <summary>
        /// 分页查询执行总表
        /// </summary>
        /// <param name="ObjParList">参数列表</param>
        /// <param name="OrdreByColumname">排序列号</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总数量</param>
        /// <returns>分页</returns>
        public List<View_DispatchingCustomers> GetDispatchingPageByWhere(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_DispatchingCustomers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount,OrderType.Asc);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        //public List<View_DispatchingCustomers> GetDispatchingCustomersByWhere(List<ObjectParameter> ObjParList)
        //{
        //    var DataSource = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParList.ToArray());

        //    return DataSource;
        //}


        [Obsolete("请使用 GetDispatchingPageByWhere（……）")]
        /// <summary>
        /// 分页获取派工
        /// </summary>
        /// <returns></returns>
        public List<View_DispatchingCustomers> GetDispatchingCustomersByWhere(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_DispatchingCustomers>.GetDataByParameter(new View_DispatchingCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            PageIndex = PageIndex - 1;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_DispatchingCustomers>();
            }
            return PageDataTools<View_DispatchingCustomers>.AddtoPageSize(DataSource);
        }


        /// <summary>
        /// 根据ID获取派工总表
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Dispatching GetByID(int? KeyID)
        {
            return ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == KeyID);
        }

        [Obsolete("请使用 GetPagedData(……)")]
        /// <summary>
        /// 分页获取派工总表
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Dispatching> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate)")]
        /// <summary>
        /// 根据项目获取
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public FL_Dispatching GetByCelebrationID(int CelebrationID)
        {
            return ObjEntity.FL_Dispatching.FirstOrDefault(C => C.CelebrationID == CelebrationID);
        }

        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate)")]
        /// <summary>
        /// 根据报价单ID获数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public FL_Dispatching GetByQuotedID(int? QuotedID)
        {
            Celebration ObjFL_CelebrationBLL = new Celebration();

            var CelebrationModel = ObjFL_CelebrationBLL.GetByQuotedID(QuotedID);
            if (CelebrationModel != null)
            {
                return ObjEntity.FL_Dispatching.FirstOrDefault(C => C.CelebrationID == CelebrationModel.CelebrationID);
            }
            else
            {
                return null;
            }
        }

        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate)")]
        /// <summary>
        /// 获取所有派工总表
        /// </summary>
        /// <returns></returns>
        public List<FL_Dispatching> GetByAll()
        {
            return ObjEntity.FL_Dispatching.ToList();
        }

        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate)")]
        /// <summary>
        /// 根据OrderID获取数据
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public FL_Dispatching GetByOrder(int? OrderID)
        {
            return ObjEntity.FL_Dispatching.FirstOrDefault(C => C.OrderID == OrderID);
        }

        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate)")]
        /// <summary>
        /// 根据客户ID获取
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public FL_Dispatching GetDispatchingByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Dispatching.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate)")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjKeyList"></param>
        /// <returns></returns>
        public List<FL_Dispatching> GetByCustomerKey(int[] ObjKeyList)
        {
            return (from C in ObjEntity.FL_Dispatching
                    where (ObjKeyList).Contains(C.CustomerID.Value)
                    select C).ToList();
        }


        [Obsolete("请使用 Where(Func<FL_Dispatching, bool> predicate).Select(C.EmployeeID) ")]
        /// <summary>
        /// 获取所有参与本项目的关系人
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<int?> GetEmployeeByOrderID(int? OrderID)
        {
            List<int?> ObjEmployeeIDList = new List<int?>();
            var ObjDispatchingModel = this.GetByOrder(OrderID);
            var ProductList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == ObjDispatchingModel.DispatchingID).ToList();
            ProductList.AddRange(ObjEntity.FL_ProductforDispatching.Where(C => C.ParentDispatchingID == ObjDispatchingModel.DispatchingID).ToList());
            foreach (var ObjCateg in ProductList)
            {
                if (ObjCateg.EmployeeID != null)
                {
                    ObjEmployeeIDList.Add(ObjCateg.EmployeeID);
                }
            }


            return ObjEmployeeIDList.Distinct().ToList();

        }
        #endregion





        /// <summary>
        /// 根据婚期获取执行订单金额
        /// </summary>
        /// <param name="PartyDateStar"></param>
        /// <param name="PartyDayEnd"></param>
        /// <returns></returns>
        public string GetFinishMoneyforDispatching(DateTime PartyDateStar, DateTime PartyDayEnd, string EmployeeType, int EmployeeID)
        {
            if (EmployeeID > 0)
            {
                if (EmployeeType == "CreateEmpLoyee")       //顾问
                {

                    return (from C in ObjEntity.FL_QuotedPrice
                            join D in ObjEntity.View_DispatchingCustomers
                                on C.QuotedID equals D.QuotedID
                            where D.PartyDate >= PartyDateStar && D.PartyDate <= PartyDayEnd && C.FinishAmount != null && D.CreateEmpLoyee == EmployeeID
                            select C.FinishAmount).Sum().ToString();
                }
                else if (EmployeeType == "QuotedEmployee")      //策划师
                {

                    return (from C in ObjEntity.FL_QuotedPrice
                            join D in ObjEntity.View_DispatchingCustomers
                                on C.QuotedID equals D.QuotedID
                            where D.PartyDate >= PartyDateStar && D.PartyDate <= PartyDayEnd && C.FinishAmount != null && D.QuotedEmpLoyee == EmployeeID
                            select C.FinishAmount).Sum().ToString();

                }
                else if (EmployeeType == "-1")          //默认 策划师
                {
                    var DataList = (from D in ObjEntity.View_DispatchingCustomers
                                    join C in ObjEntity.FL_QuotedPrice
                                on D.QuotedID equals C.QuotedID
                                    where D.PartyDate >= PartyDateStar && D.PartyDate <= PartyDayEnd && C.FinishAmount != null && D.QuotedEmpLoyee == EmployeeID
                                    select C).ToList();
                    return DataList.Sum(C => C.FinishAmount).ToString();
                }
                else
                {
                    return "";
                }

                //return ObjEntity.View_Dispatching.Where(C => C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && C.FinishAmount != null).Sum(C => C.FinishAmount).ToString();
            }
            else
            {
                return (from C in ObjEntity.FL_QuotedPrice
                        join D in ObjEntity.View_DispatchingCustomers
                            on C.QuotedID equals D.QuotedID
                        where D.PartyDate >= PartyDateStar && D.PartyDate <= PartyDayEnd && C.FinishAmount != null
                        select C.FinishAmount).Sum().ToString();
            }
        }


        /// <summary>
        /// 根据婚期获取总成本
        /// </summary>
        /// <param name="PartyDateStar"></param>
        /// <param name="PartyDayEnd"></param>
        /// <returns></returns>
        public string GetTotalCostforDispatching(DateTime PartyDateStar, DateTime PartyDayEnd, string EmployeeType, int EmployeeID)
        {
            if (EmployeeID > 0)
            {
                if (EmployeeType == "CreateEmpLoyee")       //顾问
                {

                    return (from C in ObjEntity.View_DispatchingCustomers
                            join D in ObjEntity.FL_CostSum
                                on C.DispatchingID equals D.DispatchingID
                            where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && D.Sumtotal != null && C.CreateEmpLoyee == EmployeeID
                            select D.Sumtotal).Sum().ToString();
                }
                else if (EmployeeType == "QuotedEmployee")      //策划师
                {

                    return (from C in ObjEntity.View_DispatchingCustomers
                            join D in ObjEntity.FL_CostSum
                                on C.DispatchingID equals D.DispatchingID
                            where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && D.Sumtotal != null && C.QuotedEmpLoyee == EmployeeID
                            select D.Sumtotal).Sum().ToString();

                }
                else if (EmployeeType == "-1")          //默认 策划师
                {
                    return (from C in ObjEntity.View_DispatchingCustomers
                            join D in ObjEntity.FL_CostSum
                                on C.DispatchingID equals D.DispatchingID
                            where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && D.Sumtotal != null && C.QuotedEmpLoyee == EmployeeID
                            select D.Sumtotal).Sum().ToString();
                }
                else
                {
                    return "";
                }

            }
            else
            {
                return (from C in ObjEntity.View_DispatchingCustomers
                        join D in ObjEntity.FL_CostSum
                            on C.DispatchingID equals D.DispatchingID
                        where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && D.Sumtotal != null
                        select D.Sumtotal).Sum().ToString();
            }
        }


        /// <summary>
        /// 根据时间段获取订单总数
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetDispatchingCountByDate(int Month, int Year)
        {

            var ObjList = ObjEntity.View_Dispatching.Where(C => C.PartyDate.Value.Year == Year);
            return ObjList.Count(C => C.PartyDate.Value.Month == Month).ToString();
        }


        public string GetFinishiAmountByDate(int Month, int Year)
        {
            var ObjList = ObjEntity.View_Dispatching.Where(C => C.PartyDate.Value.Year == Year && C.PartyDate.Value.Month == Month);
            return ObjList.Sum(C => C.FinishAmount).ToString(); ;
        }

        /// <summary>
        /// 根据婚庆获取销售额
        /// </summary>
        /// <param name="PartyDateStar"></param>
        /// <param name="PartyDayEnd"></param>
        /// <returns></returns>
        public string GetSaleSum(DateTime PartyDateStar, DateTime PartyDayEnd)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.CreateDate >= PartyDateStar && C.CreateDate <= PartyDayEnd && C.FinishAmount != null).Sum(C => C.FinishAmount).ToString();
        }

        /// <summary>
        /// 根据婚期获取执行订数
        /// </summary>
        /// <param name="PartyDateStar"></param>
        /// <param name="PartyDayEnd"></param>
        /// <returns></returns>
        public int GetStarDispatchingSumCount(DateTime PartyDateStar, DateTime PartyDayEnd, string EmployeeType, int EmployeeID)
        {
            if (EmployeeID > 0)
            {
                if (EmployeeType == "CreateEmpLoyee")       //顾问
                {

                    return (from C in ObjEntity.View_DispatchingCustomers where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && C.CreateEmpLoyee == EmployeeID select C).ToList().Count;
                }
                else if (EmployeeType == "QuotedEmployee")      //策划师
                {

                    return (from C in ObjEntity.View_DispatchingCustomers where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && C.QuotedEmpLoyee == EmployeeID select C).ToList().Count;

                }
                else if (EmployeeType == "-1")          //默认 策划师
                {
                    return (from C in ObjEntity.View_DispatchingCustomers where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd && C.QuotedEmpLoyee == EmployeeID select C).ToList().Count;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return (from C in ObjEntity.View_DispatchingCustomers where C.PartyDate >= PartyDateStar && C.PartyDate <= PartyDayEnd select C).ToList().Count;
            }


        }

        /// <summary>
        /// 获取会员系统所需的资源数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCsCustomersByWhere(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParList.ToArray()).OrderByDescending(C => C.PartyDate).ToList();
            SourceCount = DataSource.Count;
            PageIndex = PageIndex - 1;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_CustomerQuoted>();
            }
            return DataSource;
        }

        [Obsolete("此方法已过时")]
        public List<View_CustomerQuoted> GetCsCustomersByBirthdayRange(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList, DateTime start, DateTime end)
        {
            DateTime NewStartDate = new DateTime(2013, start.Month, start.Day);
            DateTime NewEndDate = new DateTime(2013, end.Month, end.Day);
            var DataSource = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParList.ToArray())
                .Where(C =>
                {
                    if (C.BrideBirthday.HasValue)
                    {
                        DateTime NewBrideBirthday = new DateTime(2013, C.BrideBirthday.Value.Month, C.BrideBirthday.Value.Day);
                        DateTime NewGroomBirthday = new DateTime(2013, C.GroomBirthday.Value.Month, C.GroomBirthday.Value.Day);
                        return ((NewBrideBirthday.Date.CompareTo(NewStartDate) >= 0) && (NewBrideBirthday.Date.CompareTo(NewEndDate) <= 0))
                            || ((NewGroomBirthday.Date.CompareTo(NewStartDate) >= 0) && (NewGroomBirthday.Date.CompareTo(NewEndDate) <= 0));
                    }
                    return false;
                }).OrderByDescending(C => C.PartyDate).ToList();
            SourceCount = DataSource.Count;
            PageIndex = PageIndex - 1;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_CustomerQuoted>();
            }
            return PageDataTools<View_CustomerQuoted>.AddtoPageSize(DataSource);
        }


        public List<View_CustomerQuoted> GetCsCustomersByPartyDateRangeIgnoreYear(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList, DateTime start, DateTime end)
        {
            DateTime NewStartDate = new DateTime(2013, start.Month, start.Day);
            DateTime NewEndDate = new DateTime(2013, end.Month, end.Day);

            var DataSource = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParList.ToArray())
                .Where(C =>
                {
                    DateTime NewPartyDate = new DateTime(2013, C.PartyDate.Value.Month, C.PartyDate.Value.Day);
                    return (NewPartyDate.Date.CompareTo(NewStartDate) >= 0) && (NewPartyDate.Date.CompareTo(NewEndDate) <= 0);
                }).OrderByDescending(C => C.PartyDate).ToList();
            SourceCount = DataSource.Count;
            PageIndex = PageIndex - 1;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_CustomerQuoted>();
            }
            return PageDataTools<View_CustomerQuoted>.AddtoPageSize(DataSource);
        }

        /// <summary>
        /// 分页获取派工
        /// </summary>
        /// <returns></returns>
        [Obsolete("此方法已过时")]
        public List<View_Dispatching> GetDispatchingByWhere(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_Dispatching>.GetDataByParameter(new View_Dispatching(), ObjParList.ToArray()).OrderByDescending(C => C.Expr3).ToList();
            SourceCount = DataSource.Count;
            PageIndex = PageIndex - 1;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_Dispatching>();
            }
            return PageDataTools<View_Dispatching>.AddtoPageSize(DataSource);
        }

        /// <summary>
        /// 分页获取派工
        /// </summary>
        /// <returns></returns>
        public List<View_Dispatching> GetDispatchingByWhere(List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_Dispatching>.GetDataByParameter(new View_Dispatching(), ObjParList.ToArray()).OrderByDescending(C => C.Expr3).ToList();





            return PageDataTools<View_Dispatching>.AddtoPageSize(DataSource);
        }

        /// <summary>
        /// 分页获取派工
        /// </summary>
        /// <returns></returns>
        public List<View_Dispatching> GetByWhereParameter(List<PMSParameters> ObjParList, string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_Dispatching>.GetDataByWhereParameter(ObjParList, OrderByColumnName, PageSize, PageIndex, out SourceCount);
        }
        #endregion

        #region 数据操作
        /// <summary>
        /// 添加总派工表
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Dispatching ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Dispatching.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DispatchingID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新派工总表
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_Dispatching ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.DispatchingID;
            }
            return 0;
        }

        public int Delete(FL_Dispatching ObjectT)
        {
            throw new NotImplementedException();
        }

        #endregion


        public class KeyClassEquers : IEqualityComparer<View_DispatchingCustomers>
        {
            public bool Equals(View_DispatchingCustomers x, View_DispatchingCustomers y)
            {
                if (x.EmployeeID == y.EmployeeID && x.DispatchingID == y.DispatchingID)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(View_DispatchingCustomers obj)
            {
                return 0;
            }
        }

        public class View_GetSupplierforCarrytaskComparer : IEqualityComparer<View_GetSupplierforCarrytask>
        {
            public bool Equals(View_GetSupplierforCarrytask x, View_GetSupplierforCarrytask y)
            {
                if (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null))
                {
                    return x.SupplierID.Equals(y.SupplierID) && x.OrderID.Equals(y.OrderID);
                }
                return false;
            }

            public int GetHashCode(View_GetSupplierforCarrytask obj)
            {
                return object.ReferenceEquals(obj, null) ? 0 : obj.GetHashCode();
            }
        }

        //[FL_Dispatching]
        public IEnumerable<FL_Dispatching> GetPagedData(int PageSize, int PageIndex, out int SourceCount, Func<FL_Dispatching, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FL_Dispatching> query = PublicDataTools<FL_Dispatching>
                .GetDataByParameter(new FL_Dispatching(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.CreateDate);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }
        public IEnumerable<FL_Dispatching> Where(Func<FL_Dispatching, bool> predicate)
        {
            return ObjEntity.FL_Dispatching.Where(predicate);
        }
        public FL_Dispatching FirstOrDefault(Func<FL_Dispatching, bool> predicate)
        {
            return ObjEntity.FL_Dispatching.FirstOrDefault(predicate);
        }

        //[FL_DispatchingCustomer]


        //[FL_ProductforDispatching]
        public IEnumerable<FL_ProductforDispatching> GetPagedProductforDispatching<S>(int PageSize, int PageIndex, out int SourceCount, bool isAsc, Func<FL_ProductforDispatching, S> keySelector, Func<FL_ProductforDispatching, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FL_ProductforDispatching> query = PublicDataTools<FL_ProductforDispatching>.GetDataByParameter(new FL_ProductforDispatching(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray()).Where(predicate);
            SourceCount = query.Count();
            return isAsc ? query.OrderBy<FL_ProductforDispatching, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize) :
                query.OrderByDescending<FL_ProductforDispatching, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }

        //public IEnumerable<FL_ProductforDispatching> GetPagedProductforDispatchingDistinct<S>(int PageSize, int PageIndex, out int SourceCount, bool isAsc, Func<FL_ProductforDispatching, S> keySelector, Func<FL_ProductforDispatching, FL_ProductforDispatching, bool> distinct, Func<FL_ProductforDispatching, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        //{
        //    IEnumerable<FL_ProductforDispatching> query = PublicDataTools<FL_ProductforDispatching>.GetDataByParameter(new FL_ProductforDispatching(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray()).Where(predicate).ToList().Distinct(distinct);
        //    SourceCount = query.Count();
        //    return isAsc ? query.OrderBy<FL_ProductforDispatching, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize) :
        //        query.OrderByDescending<FL_ProductforDispatching, S>(keySelector).Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        //}

        public IEnumerable<FL_ProductforDispatching> GetProductforDispatching(Func<FL_ProductforDispatching, bool> predicate)
        {
            return ObjEntity.FL_ProductforDispatching.Where(predicate);
        }


        public FL_Dispatching GetByCustomerID(int? CustomerID)
        {
            return ObjEntity.FL_Dispatching.Where(C => C.CustomerID == CustomerID).FirstOrDefault();
        }

    }
}
