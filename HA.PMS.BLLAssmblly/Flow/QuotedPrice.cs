//临时注释 以后完善  初级报价单  不针对产品  最粗略的报价   黄晓可
//临时注释 初级派工 最粗略的派工
//增加报价单图片附件
//增加报价 文件上传
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
//报价单主表
namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedPrice : ICRUDInterface<FL_QuotedPrice>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        Employee ObjEmployeeBLL = new Employee();
        #region 数据查询
        public decimal GetAvgMoneyByMonth(int Year, int Month)
        {
            var ObjList = ObjEntity.FL_QuotedPrice.Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month);
            if (ObjList.Count() > 0)
            {
                var ObjSumMoney = ObjList.Where(C => C.FinishAmount != null).Sum(C => C.FinishAmount);
                var SourctCount = ObjList.Count();
                return (ObjSumMoney / SourctCount).Value;
            }
            else
            {
                return 0;
            }

        }



        /// <summary>
        /// 获取余款
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public decimal GetFinishMoneyByMonth(DateTime Star, DateTime End, int EmployeeID)
        {
            var EmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(EmployeeID);
            List<int> objEmployeeKeyList = new List<int>();
            foreach (var ObjItem in EmployeeList)
            {
                objEmployeeKeyList.Add(ObjItem.EmployeeID);
            }
            objEmployeeKeyList.Add(EmployeeID);
            var SumMoney = (from Q in ObjEntity.FL_QuotedPrice
                            join
                            S in ObjEntity.FL_QuotedCollectionsPlan
                            on
                            Q.QuotedID equals S.QuotedID
                            join
                            C in ObjEntity.FL_Customers
                            on
                            Q.CustomerID equals C.CustomerID
                            where C.PartyDate >= Star && C.PartyDate <= End && (objEmployeeKeyList).Contains(Q.EmpLoyeeID.Value)
                            select S.RealityAmount
                              ).Sum();
            if (SumMoney > 0)
            {
                return SumMoney.Value;
            }
            return 0;

        }




        /// <summary>
        /// 获取成总金额
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetSumMoneyByPaetyDate(DateTime Star, DateTime End, int EmployeeID)
        {
            var EmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(EmployeeID);
            List<int> objEmployeeKeyList = new List<int>();
            foreach (var ObjItem in EmployeeList)
            {
                objEmployeeKeyList.Add(ObjItem.EmployeeID);
            }
            objEmployeeKeyList.Add(EmployeeID);
            var SumMoney = (from Q in ObjEntity.FL_QuotedPrice
                            join
                            S in ObjEntity.SS_Report
                            on
                            Q.CustomerID equals S.CustomerID
                            where S.Partydate >= Star && S.Partydate <= End && (objEmployeeKeyList).Contains(Q.EmpLoyeeID.Value)
                            select Q.FinishAmount
                              ).Sum();
            if (SumMoney > 0)
            { return SumMoney.Value; }

            return 0;

        }

        /// <summary>
        /// 获取报价单统计数据
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <param name="Type">类型1应收 2已收 3订单总金额</param>
        public string GetQuotedSumMOney(DateTime Star, DateTime End, int Type, int EmployeeID, int TimerType)
        {

            //获取我所管理的下级人员
            var EmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(EmployeeID);
            List<int> objEmployeeKeyList = new List<int>();
            foreach (var ObjItem in EmployeeList)
            {
                objEmployeeKeyList.Add(ObjItem.EmployeeID);
            }
            //if (objEmployeeKeyList.Count == 0)
            //{
            objEmployeeKeyList.Add(EmployeeID);
            //}
            if (TimerType == 0)
            {
                //判断查询类型
                switch (Type)
                {
                    case 1:

                        var PlanSUM = (from C in ObjEntity.FL_QuotedCollectionsPlan
                                       join QT in ObjEntity.View_CustomerQuoted
                                       on C.QuotedID equals QT.QuotedID
                                       where (objEmployeeKeyList).Contains(C.CreateEmpLoyee.Value)
                                       where QT.PartyDate >= Star && QT.PartyDate <= End

                                       select C).Sum(C => C.Amountmoney).ToString();


                        return PlanSUM;
                        // return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.Amountmoney != null && C.CreateDate >= Star && C.CreateDate <= End).Sum(C => C.Amountmoney).ToString();
                        break;
                    case 2:
                        //var objList = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.RowLock == true && C.CreateDate >= Star && C.CreateDate <= End).ToList();


                        var RealSUM = (from C in ObjEntity.FL_QuotedCollectionsPlan
                                       join QT in ObjEntity.View_CustomerQuoted
                                       on C.QuotedID equals QT.QuotedID
                                       where (objEmployeeKeyList).Contains(C.CreateEmpLoyee.Value)
                                       where QT.PartyDate >= Star && QT.PartyDate <= End

                                       select C).Sum(C => C.RealityAmount).ToString();


                        return RealSUM;
                        //var RealSUM = (from C in ObjEntity.FL_QuotedCollectionsPlan
                        //               where (objEmployeeKeyList).Contains(C.CreateEmpLoyee.Value)
                        //               where C.CreateDate >= Star && C.CreateDate <= End
                        //               select C).Sum(C => C.RealityAmount).ToString();


                        //return RealSUM;//objList.Sum(C => C.RealityAmount).ToString();
                        break;
                    case 3:

                        if (EmployeeID > 0)
                        {
                            var QuerySUM = (from C in ObjEntity.View_CustomerQuoted
                                            where (objEmployeeKeyList).Contains(C.EmpLoyeeID.Value)
                                            where C.PartyDate >= Star && C.PartyDate <= End && C.EmpLoyeeID == EmployeeID
                                            select C).Sum(C => C.FinishAmount).ToString();
                            //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                            return QuerySUM;
                        }
                        else
                        {

                            var QuerySUM = (from C in ObjEntity.View_CustomerQuoted
                                            where (objEmployeeKeyList).Contains(C.EmpLoyeeID.Value)
                                            where C.PartyDate >= Star && C.PartyDate <= End
                                            select C).Sum(C => C.FinishAmount).ToString();
                            //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                            return QuerySUM;
                        }
                        break;
                    case 4:

                        var OrderSUM = (from C in ObjEntity.View_GetOrderCustomers
                                        where (objEmployeeKeyList).Contains(C.EmployeeID.Value)
                                        where C.PartyDate >= Star && C.PartyDate <= End
                                        select C).Sum(C => C.EarnestMoney).ToString();
                        //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                        return OrderSUM;
                        break;

                    case 5:

                        var CustomerCount = (from C in ObjEntity.View_CustomerQuoted
                                             where (objEmployeeKeyList).Contains(C.EmpLoyeeID.Value)
                                             where C.PartyDate >= Star && C.PartyDate <= End && C.ParentQuotedID == 0 && C.IsDelete == false
                                             select C).Count().ToString();
                        //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                        return CustomerCount;
                        break;
                }
            }
            else
            {
                //判断查询类型
                switch (Type)
                {
                    case 1:

                        var PlanSUM = (from C in ObjEntity.FL_QuotedCollectionsPlan
                                       join QT in ObjEntity.View_CustomerQuoted
                                       on C.QuotedID equals QT.QuotedID
                                       where (objEmployeeKeyList).Contains(C.CreateEmpLoyee.Value)
                                       where QT.CreateDate >= Star && QT.CreateDate <= End

                                       select C).Sum(C => C.Amountmoney).ToString();


                        return PlanSUM;
                        // return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.Amountmoney != null && C.CreateDate >= Star && C.CreateDate <= End).Sum(C => C.Amountmoney).ToString();
                        break;
                    case 2:
                        //var objList = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.RowLock == true && C.CreateDate >= Star && C.CreateDate <= End).ToList();


                        var RealSUM = (from C in ObjEntity.FL_QuotedCollectionsPlan
                                       join QT in ObjEntity.View_CustomerQuoted
                                       on C.QuotedID equals QT.QuotedID
                                       where (objEmployeeKeyList).Contains(C.CreateEmpLoyee.Value)
                                       where QT.CreateDate >= Star && QT.CreateDate <= End

                                       select C).Sum(C => C.RealityAmount).ToString();


                        return RealSUM;
                        //var RealSUM = (from C in ObjEntity.FL_QuotedCollectionsPlan
                        //               where (objEmployeeKeyList).Contains(C.CreateEmpLoyee.Value)
                        //               where C.CreateDate >= Star && C.CreateDate <= End
                        //               select C).Sum(C => C.RealityAmount).ToString();


                        //return RealSUM;//objList.Sum(C => C.RealityAmount).ToString();
                        break;
                    case 3:

                        var QuerySUM = (from C in ObjEntity.View_CustomerQuoted
                                        where (objEmployeeKeyList).Contains(C.EmpLoyeeID.Value)
                                        where C.CreateDate >= Star && C.CreateDate <= End
                                        select C).Sum(C => C.FinishAmount).ToString();
                        //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                        return QuerySUM;
                        break;
                    case 4:

                        var OrderSUM = (from C in ObjEntity.View_GetOrderCustomers
                                        where (objEmployeeKeyList).Contains(C.EmployeeID.Value)
                                        where C.CreateDate >= Star && C.CreateDate <= End
                                        select C).Sum(C => C.EarnestMoney).ToString();
                        //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                        return OrderSUM;
                        break;

                    case 5:

                        var CustomerCount = (from C in ObjEntity.View_CustomerQuoted
                                             where (objEmployeeKeyList).Contains(C.EmpLoyeeID.Value)
                                             where C.CreateDate >= Star && C.CreateDate <= End && C.ParentQuotedID == 0 && C.IsDelete == false
                                             select C).Count().ToString();
                        //  var objQuotedList = ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount > 0 && C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).Sum(C => C.FinishAmount).ToString();
                        return CustomerCount;
                        break;
                }
            }
            return "0";
        }


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<FL_QuotedPrice> GetByAll()
        {
            return ObjEntity.FL_QuotedPrice.ToList();
        }

        /// <summary>
        /// 根据订单类型获取数据
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<FL_QuotedPrice> GetByType(int? Type)
        {
            return ObjEntity.FL_QuotedPrice.Where(C => C.QuotedType == Type).ToList();
        }

        /// <summary>
        /// 根据订单ID获取初级报价单
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedPrice GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedID == KeyID);
        }


        /// <summary>
        /// 根据订单ID获取初级报价单
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedPrice GetByQuotedTitle(string QuotedTitle)
        {
            return ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedTitle == QuotedTitle);
        }


        /// <summary>
        /// 根据报价单类型获取
        /// </summary>
        /// <param name="Kind"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public FL_QuotedPrice GetByKind(int? Kind, int? Type)
        {
            var ObjModel = ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedKind == Kind && C.QuotedType == Type);
            if (ObjModel == null)
            {
                ObjModel = ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedKind == 0 && C.QuotedType == Type);
                ObjModel.QuotedKind = Kind;
                this.Update(ObjModel);
            }
            return ObjModel;
        }


        /// <summary>
        /// 根据某个订单ID返回报价对象
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public FL_QuotedPrice GetByOrderId(int OrderId)
        {
            return ObjEntity.FL_QuotedPrice.Where(C => C.OrderID == OrderId).FirstOrDefault();
        }

        public FL_QuotedPrice GetOnlyFirstByOrderID(int OrderID)
        {
            return ObjEntity.FL_QuotedPrice.Where(C => C.OrderID == OrderID && C.ParentQuotedID == 0).FirstOrDefault();
        }
        /// <summary>
        /// 根据客户ID获取订单
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedPrice GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.CustomerID == KeyID && C.ParentQuotedID == 0);
        }


        /// <summary>
        /// 根据初级报价单ID获取报价单
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedPrice GetByQuotedID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedID == KeyID);
        }

        public FL_QuotedPrice GetByQuotedsID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedID == KeyID && C.ParentQuotedID == 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_QuotedPrice> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }




        public List<FL_QuotedPrice> GetByParentQuotedID(int? QuotedID)
        {
            return ObjEntity.FL_QuotedPrice.Where(C => C.ParentQuotedID == QuotedID).ToList();
        }

        /// <summary>
        /// 根据条件获取邀约用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCustomerQuotedByStateIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_CustomerQuoted>();
            }
            return PageDataTools<View_CustomerQuoted>.AddtoPageSize(DataSource);

        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        public List<View_CustomerQuoted> GetDataByParameter(List<PMSParameters> ObjParList, string sortName, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<View_CustomerQuoted>.GetDataByWhereParameter(ObjParList, sortName, PageSize, PageIndex, out SourceCount);

        }

        public List<View_CustomerQuoted> GetCustomerQuotedByStateIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList, Func<View_CustomerQuoted, bool> predicate)
        {

            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParList.ToArray()).Where(predicate).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_CustomerQuoted>();
            }
            return PageDataTools<View_CustomerQuoted>.AddtoPageSize(DataSource);

        }

        /// <summary>
        /// 根据条件获取有收款计划的订单
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetQuotedCostPlan> GetQuotedCostPlanByStateIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<View_GetQuotedCostPlan>.GetDataByParameter(new View_GetQuotedCostPlan(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetQuotedCostPlan>();
            }
            return PageDataTools<View_GetQuotedCostPlan>.AddtoPageSize(DataSource);

        }

        /// <summary>
        /// 根据条件获取有收款计划的订单
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetQuotedCostPlan> GetQuotedCostPlanByStateDistinctIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<View_GetQuotedCostPlan>.GetDataByParameter(new View_GetQuotedCostPlan(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).Distinct(new GetQuotedCostPlanComparer()).ToList();


            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetQuotedCostPlan>();
            }
            return PageDataTools<View_GetQuotedCostPlan>.AddtoPageSize(DataSource);

        }

        /// <summary>
        /// 根据条件获取邀约用户 不含分页
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCustomerQuotedParameter(List<ObjectParameter> ObjParList)
        {


            var DataSource = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParList.ToArray()).ToList();

            return DataSource;

        }


        /// <summary>
        /// 分页获取报价单数据
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_CustomerQuoted> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount, OrderType order = OrderType.Desc)
        {
            return PublicDataTools<View_CustomerQuoted>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount, order);
        }



        public List<View_DispatchingEvaluation> GetByWhereParameters(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<View_DispatchingEvaluation>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }

        /// <summary>
        /// 获取顾客的总体评价
        /// </summary>
        /// <returns></returns>
        public List<View_CustomerEvaulation> GetByCustomerParameters(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount, OrderType Order = OrderType.Desc)
        {

            return PublicDataTools<View_CustomerEvaulation>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }

        /// <summary>
        /// 直接返回所有 庆典报价单和客户的关联对象
        /// </summary>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCustomerQuotedAll()
        {


            var DataSource = ObjEntity.View_CustomerQuoted.ToList();

            return DataSource;

        }

        public View_CustomerQuoted GetByViewQuotedID(int? QuotedID)
        {
            return ObjEntity.View_CustomerQuoted.FirstOrDefault(C => C.QuotedID == QuotedID || C.ParentQuotedID == 0);
        }

        /// <summary>
        #endregion


        #region 数据操作

        ///上传报价单图片
        public int QuotedImageInsert(FL_QuotedImage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedImage.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.QuotedID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        //上传提案
        public int QuotedPricefileManagerInsert(FL_QuotedPricefileManager ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPricefileManager.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.QuotedID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateQuotedPricefileManager(FL_QuotedPricefileManager ObjectT)
        {

            ObjEntity.SaveChanges();
            return ObjectT.FileID;
        }


        /// <summary>
        /// 获取报价单下的提案
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<FL_QuotedPricefileManager> GetQuotedPricefileByQuotedID(int? QuotedID, int Type)
        {
            return ObjEntity.FL_QuotedPricefileManager.Where(C => C.Type == Type && C.QuotedID == QuotedID).ToList();
        }



        /// <summary>
        /// 获取报价单文件
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns></returns>
        public FL_QuotedPricefileManager GetQuotedPricefileByFileID(int FileID)
        {
            return ObjEntity.FL_QuotedPricefileManager.Where(C => C.FileID == FileID).FirstOrDefault();
        }

        /// <summary>
        /// 删除报价单文件
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns></returns>
        public int DeleteQuotedPricefile(FL_QuotedPricefileManager ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPricefileManager.Remove(GetQuotedPricefileByFileID(ObjectT.FileID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// 获取报价单单项的图片
        /// </summary>
        /// <returns></returns>
        public List<FL_QuotedPricefileManager> GetImageByKind(int? QuotedID, int? Kind, int? Type)
        {
            return ObjEntity.FL_QuotedPricefileManager.Where(C => C.KindID == Kind && C.Type == Type && C.QuotedID == QuotedID).ToList();
        }

        /// <summary>
        /// 删除报价单
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_QuotedPrice ObjectT)
        {
            ObjectT.IsDelete = true;
            //ObjEntity.FL_QuotedPrice.Remove(ObjectT);
            ObjEntity.SaveChanges();
            return 0;
        }

        /// 添加初级报价单
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_QuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjectT.IsChecks = false;
                ObjectT.IsDelete = false;
                ObjectT.IsFirstCreate = false;
                ObjectT.Dispatching = 0;
                ObjEntity.FL_QuotedPrice.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.QuotedID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 修改初级报价单
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_QuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.QuotedID;
            }
            return 0;
        }
        #endregion


        #region FL_QuotedPrice
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="paras"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<FL_QuotedPrice> GetPagedData(int PageSize, int PageIndex, out int SourceCount, Func<FL_QuotedPrice, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<FL_QuotedPrice> query = PublicDataTools<FL_QuotedPrice>
                .GetDataByParameter(new FL_QuotedPrice(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.CreateDate);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<FL_QuotedPrice> Where(Func<FL_QuotedPrice, bool> predicate)
        {
            return ObjEntity.FL_QuotedPrice.Where(predicate).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public FL_QuotedPrice FirstOrDefault(Func<FL_QuotedPrice, bool> predicate)
        {
            return ObjEntity.FL_QuotedPrice.FirstOrDefault(predicate);
        }

        #endregion

        #region View_CustomerQuoted
        /// <summary>
        /// return view of FL_Customers join FL_QuotedPrice
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="paras"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<View_CustomerQuoted> GetPagedCustomerQuoted(int PageSize, int PageIndex, out int SourceCount, Func<View_CustomerQuoted, bool> predicate, IEnumerable<ObjectParameter> paras = null)
        {
            IEnumerable<View_CustomerQuoted> query = PublicDataTools<View_CustomerQuoted>
                .GetDataByParameter(new View_CustomerQuoted(), paras != null ? paras.ToArray() : new List<ObjectParameter>().ToArray())
                .Where(predicate)
                .OrderByDescending(C => C.PartyDate);
            SourceCount = query.Count();
            return query.Skip(PageSize * (PageIndex - 1)).Take(PageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<View_CustomerQuoted> GetCustomerQuoted(Func<View_CustomerQuoted, bool> predicate)
        {
            return ObjEntity.View_CustomerQuoted.Where(predicate);
        }
        #endregion


        public List<int> GetAllCustomerId()
        {
            var ObjDataList = (from C in ObjEntity.FL_QuotedPrice select C.CustomerID.ToString().ToInt32()).ToList();
            return ObjDataList as List<int>;
        }

        public FL_QuotedPrice GetByCustomerID(int CustomerId)
        {
            return ObjEntity.FL_QuotedPrice.Where(C => C.CustomerID == CustomerId && C.ParentQuotedID == 0).FirstOrDefault();
        }


        /// <summary>
        /// 获取订金
        /// </summary>
        /// <returns></returns>
        public string GetOrderMondeySum()
        {
            string OrderMoneySum = (from C in ObjEntity.View_CustomerQuoted
                                    join D in ObjEntity.FL_Order
                                        on C.OrderID equals D.OrderID
                                    where D.EarnestMoney != null
                                    select D).ToList().Sum(D => D.EarnestMoney).ToString();
            return OrderMoneySum;
        }

        /// <summary>
        /// 获取现金流收款
        /// </summary>
        /// <returns></returns>
        public string GetFinishMoney()
        {

            string OrderMoneySum = (from C in ObjEntity.View_CustomerQuoted
                                    join D in ObjEntity.FL_QuotedCollectionsPlan
                                        on C.OrderID equals D.OrderID
                                    where D.RealityAmount != null
                                    select D).ToList().Sum(D => D.RealityAmount).ToString();
            return OrderMoneySum;
        }

        public string GetFinishMoneys(List<View_CustomerQuoted> DataList)
        {

            string OrderMoneySum = (from C in DataList
                                    join D in ObjEntity.FL_QuotedCollectionsPlan
                                        on C.OrderID equals D.OrderID
                                    where D.RealityAmount != null
                                    select D).ToList().Sum(D => D.RealityAmount).ToString();
            return OrderMoneySum;
        }


        public string GetFinishAmount(List<PMSParameters> pars)
        {
            int SourceCount = 0;
            var DataList = GetByWhereParameter(pars, "CreateDate", 100000, 1, out SourceCount, OrderType.Desc);

            string OrderMoneySum = (from C in DataList                          //现金流
                                    join D in ObjEntity.FL_QuotedCollectionsPlan
                                        on C.OrderID equals D.OrderID
                                    where D.RealityAmount != null
                                    select D).ToList().Sum(D => D.RealityAmount).ToString();
            string FirstMoneySum = (from C in DataList                       //获取首付款
                                    join D in ObjEntity.FL_OrderEarnestMoney
                                        on C.OrderID equals D.OrderID
                                    where D.EarnestMoney != null
                                    select D).ToList().Sum(D => D.EarnestMoney).ToString();
            string PreMoneySum = (from C in DataList       //预付款
                                  join D in ObjEntity.FL_Order
                                      on C.OrderID equals D.OrderID
                                  where D.EarnestMoney != null
                                  select D).ToList().Sum(D => D.EarnestMoney).ToString();
            string MoneysSums = (OrderMoneySum.ToDecimal() + FirstMoneySum.ToDecimal() + PreMoneySum.ToDecimal()).ToString();
            return OrderMoneySum;
        }


        /// <summary>
        /// 获取首付款
        /// </summary>
        /// <returns></returns>
        public string GetOrderEarnestMoney()
        {
            string OrderMoneySum = (from C in ObjEntity.View_CustomerQuoted
                                    join D in ObjEntity.FL_OrderEarnestMoney
                                        on C.OrderID equals D.OrderID
                                    where D.EarnestMoney != null
                                    select D).ToList().Sum(D => D.EarnestMoney).ToString();
            return OrderMoneySum;
        }

        /// <summary>
        /// 获取订单总金额
        /// </summary>
        /// <returns></returns>
        public string GetFinishAmountSum()
        {
            string OrderMoneySum = (from C in ObjEntity.View_CustomerQuoted
                                    join D in ObjEntity.FL_QuotedPrice
                                        on C.CustomerID equals D.CustomerID
                                    where D.FinishAmount != null
                                    select D).ToList().Sum(D => D.FinishAmount).ToString();
            return OrderMoneySum;
        }

        public string GetFinishAmountSums(List<View_CustomerQuoted> DataList)
        {
            string OrderMoneySum = (from C in DataList
                                    join D in ObjEntity.FL_QuotedPrice
                                        on C.CustomerID equals D.CustomerID
                                    where D.FinishAmount != null
                                    select D).ToList().Sum(D => D.FinishAmount).ToString();
            return OrderMoneySum;
        }

        /// <summary>
        /// 获取余款
        /// </summary>
        /// <returns></returns>
        public string GetOverMoneySum()
        {
            decimal AllMoney = (from C in ObjEntity.View_CustomerQuoted
                                join D in ObjEntity.FL_QuotedPrice
                                    on C.CustomerID equals D.CustomerID
                                where D.FinishAmount != null
                                select D).ToList().Sum(D => D.FinishAmount).ToString().ToDecimal();
            decimal OverMoneys = (AllMoney - GetFinishMoney().ToDecimal()).ToString().ToDecimal();
            return OverMoneys.ToString(); ;

        }

        public string GetOverMoneySums(List<View_CustomerQuoted> DataList)
        {
            //decimal AllMoney = (from C in DataList
            //                    join D in ObjEntity.FL_QuotedPrice
            //                        on C.CustomerID equals D.CustomerID
            //                    where D.FinishAmount != null
            //                    select D).ToList().Sum(D => D.FinishAmount).ToString().ToDecimal();
            decimal OverMoneys = (GetFinishAmountSums(DataList).ToDecimal() - GetFinishMoneys(DataList).ToDecimal()).ToString().ToDecimal();
            return OverMoneys.ToString(); ;

        }

        /// <summary>
        /// 根据时间获得订单金额(完工额) 执行额  已完成的
        /// </summary>
        public string GetFinishAmountByDate(DateTime Start, DateTime End, int EmployeeID)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.PartyDate >= Start && C.PartyDate <= End && C.EmpLoyeeID == EmployeeID && C.EmpLoyeeID != null && C.ParentQuotedID == 0 && C.State != 29).Sum(C => C.FinishAmount).ToString();
        }

        /// <summary>
        /// 根据时间获得完工量(完工量) 执行量  已完成的
        /// </summary>

        public int GetFinishCountByDate(DateTime Start, DateTime End, int EmployeeID)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.PartyDate >= Start && C.PartyDate <= End && C.EmpLoyeeID == EmployeeID && C.EmpLoyeeID != null && C.ParentQuotedID == 0 && C.State != 29).Count();
        }

        public List<View_CustomerQuoted> GetByDesignEmployee(int DesignEmployee)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.DesignerEmployee == DesignEmployee).ToList();
        }



    }


    /// <summary>
    /// 收款计划比较器
    /// </summary>
    public class GetQuotedCostPlanComparer : IEqualityComparer<View_GetQuotedCostPlan>
    {
        public bool Equals(View_GetQuotedCostPlan x, View_GetQuotedCostPlan y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            {
                return false;
            }
            return x.QuotedID == y.QuotedID;
        }

        public int GetHashCode(View_GetQuotedCostPlan obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return 0;
            }
            return obj.PlanID.GetHashCode();

        }





    }
}
