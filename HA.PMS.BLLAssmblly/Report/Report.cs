using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.Report
{
    public class Report
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 获取渠道客源量
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomerSumByChannel(string ChannelName, DateTime Star, DateTime End)
        {
            return ObjEntity.View_SSCustomer.Where(C => C.Channel == ChannelName && C.CreateDate >= Star && C.CreateDate <= End).Count();
        }

        /// <summary>
        /// 根据类型获取  //1酒店 2网络 3异业联盟 4展会 5转介绍 6直接到店 7婚礼现场 8其他
        /// </summary>
        public int GetCustomerSumByChannelType(int ChannelTypeID, DateTime Star, DateTime End)
        {
            return ObjEntity.View_SSCustomer.Where(C => C.ChannelType == ChannelTypeID && C.CreateDate >= Star && C.CreateDate <= End).Count();
        }

        /// <summary>
        /// 获取员工客源量
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomerSumByEmployee(int EmployeeID, DateTime Star, DateTime End)
        {
            //自己录入
            int InviteNum = (from C in ObjEntity.View_SSCustomer join D in ObjEntity.FL_Invite on C.CustomerID equals D.CustomerID where C.InviteEmployee == EmployeeID && D.CreateDate >= Star && D.CreateDate <= End select D).ToList().Count();
            //改派客户
            int Count = (from C in ObjEntity.View_SSCustomer join D in ObjEntity.FL_Invite on C.CustomerID equals D.CustomerID where C.InviteEmployee == EmployeeID && D.ChangeDate >= Star && D.ChangeDate <= End select D).ToList().Count();
            return (InviteNum + Count).ToString().ToInt32();
        }

        /// <summary>
        /// 获取员工到店量(到店量)
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomerComeSumByEmployee(int EmployeeID, DateTime Star, DateTime End)
        {
            var DataList = (from C in ObjEntity.View_SSCustomer join D in ObjEntity.FL_Order on C.CustomerID equals D.CustomerID where C.OrderEmployee == EmployeeID && D.OrderState == 0 && C.OrderCreateDate >= Star && C.OrderCreateDate <= End select D).ToList();
            return DataList.Count();
        }


        /// <summary>
        /// 获取渠道入客量
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int GetCustomerComeSumByChannel(string ChannelName, DateTime Star, DateTime End)
        {
            return ObjEntity.View_SSCustomer.Where(C => C.Channel == ChannelName && C.State > 9 && C.State != 29 && C.OrderEmployee > 0 && C.OrderCreateDate >= Star && C.OrderCreateDate <= End).Count();
        }
        /// <summary>
        /// 获取渠道类型入客量
        /// </summary>
        public int GetCustomerComeSumByChannelType(int ChannelTypeID, DateTime Star, DateTime End)
        {
            return ObjEntity.View_SSCustomer.Where(C => C.ChannelType == ChannelTypeID && C.State > 9 && C.State != 29 && C.OrderEmployee > 0 && C.OrderCreateDate >= Star && C.OrderCreateDate <= End).Count();
        }


        /// <summary>
        /// 获取签单量 
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetCustomAdvance(string ChannelName, DateTime Star, DateTime End)
        {
            //return ObjEntity.View_CustomerQuoted.Where(C => C.Channel == ChannelName && C.CreateDate >= Star && C.CreateDate <= End).Count();
            return (from C in ObjEntity.View_GetMinDateCollectionPlan join D in ObjEntity.View_CustomerQuoted on C.CustomerID equals D.CustomerID where D.Channel == ChannelName && C.MinCollectionDate >= Star && C.MinCollectionDate <= End select C).Count();
        }

        public int GetCustomAdvanceType(int ChannelTypeID, DateTime Star, DateTime End)
        {
            return (from C in ObjEntity.View_GetMinDateCollectionPlan join D in ObjEntity.View_CustomerQuoted on C.CustomerID equals D.CustomerID where D.ChannelType == ChannelTypeID && C.MinCollectionDate >= Star && C.MinCollectionDate <= End select C).Count();
        }


        /// <summary>
        /// 获取预定量(获取某个渠道的现金流)
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public decimal GetCustomFinisMoneySum(string ChannelName, DateTime Star, DateTime End)
        {
            var CostPrice = (from C in ObjEntity.FL_QuotedCollectionsPlan
                             join D in ObjEntity.View_CustomerQuoted
                                on C.QuotedID equals D.QuotedID
                             where D.PartyDate >= Star && D.PartyDate <= End && D.Channel == ChannelName
                             && C.RealityAmount != null
                             select C).ToList().Sum(C => C.RealityAmount);
            return CostPrice.ToString().ToDecimal();
        }


        public decimal GetFinishMoneySum(int month, int year)
        {
            var CostPrice = (from C in ObjEntity.FL_QuotedCollectionsPlan
                             join D in ObjEntity.View_CustomerQuoted
                                on C.QuotedID equals D.QuotedID
                             where D.CreateDate.Month == month && D.CreateDate.Year == year
                             && C.RealityAmount != null
                             select C).ToList().Sum(C => C.RealityAmount);
            return CostPrice.ToString().ToDecimal();
        }


        /// <summary>
        /// 获取完工量/执行量(吴总要求 就是按照酒店)
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public int GetCustomFinish(string ChannelName, DateTime Star, DateTime End)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.Wineshop == ChannelName && C.PartyDate >= Star && C.PartyDate <= End && C.State != 29 && C.State != 202 && C.State != 203 && C.State != 300 && C.State >= 14).Count();
        }

        //根据渠道类型获取执行量
        public int GetCustomFinishType(int ChannelTypeID, DateTime Star, DateTime End)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.ChannelType == ChannelTypeID && C.PartyDate >= Star && C.PartyDate <= End && C.State != 29 && C.State != 202 && C.State != 203 && C.State != 300 && C.State >= 14).Count();
        }


        /// <summary>
        /// 获取完工额/执行额 (吴总要求 按照酒店)
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public decimal GetCustomFinishSumMoney(string ChannelName, DateTime Star, DateTime End)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.Wineshop == ChannelName && C.PartyDate >= Star && C.PartyDate <= End && C.FinishAmount != null && C.State != 29).ToList().Sum(C => C.FinishAmount.ToString().ToDecimal());
        }

        //根据渠道类型获取执行额
        public decimal GetCustomFinishSumMoneyType(int ChannelTypeID, DateTime Star, DateTime End)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.ChannelType == ChannelTypeID && C.PartyDate >= Star && C.PartyDate <= End && C.FinishAmount != null && C.State != 29).ToList().Sum(C => C.FinishAmount.ToString().ToDecimal());
        }



        /// <summary>
        /// 获取渠道费用
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public decimal GetSourceSumMoney(int SourceID, DateTime Star, DateTime End)
        {
            return ObjEntity.FD_PayNeedRabate.Where(C => C.SourceID == SourceID && C.PayDate >= Star && C.PayDate <= End && C.PayMoney != null).ToList().Sum(C => C.PayMoney.Value);
        }

        //根据渠道类型获取渠道累心费用
        public decimal GetSourceSumMoneyType(int ChannelTypeID, DateTime Star, DateTime End)
        {
            return ObjEntity.FD_PayNeedRabate.Where(C => C.ChannelTypeId == ChannelTypeID && C.PayDate >= Star && C.PayDate <= End && C.PayMoney != null).ToList().Sum(C => C.PayMoney.Value);
        }

        /// <summary>
        /// 根据条件获取统计数据
        /// </summary>
        /// <param name="SSType"></param>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <returns></returns>
        public List<View_SSCustomer> GetReportByWhereClause(List<PMSParameters> ObjParList, string OrdreByColumname)
        {
            int SourceCount = 0;
            return PublicDataTools<View_SSCustomer>.GetDataByWhereParameter(ObjParList, OrdreByColumname, 100000, 1, out SourceCount);
        }

        /// <summary>
        /// 获取管理者的统计数据
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="SSType">1相关统计 2现金流
        /// 12 渠道客源量</param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public List<SS_Report> GetReportByEmployee(int SSType, List<PMSParameters> ObjParList, string OrdreByColumname)
        {

            //var ReturnList = PublicDataTools<View_GetOrderCustomers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);
            //return ReturnList;
            int SourceCount = 0;
            var FinishDate = DateTime.Now;
            switch (SSType)
            {
                case 1:
                    return PublicDataTools<SS_Report>.GetDataByWhereParameter(ObjParList, OrdreByColumname, 100000, 1, out SourceCount);
                    break;
                case 2:
                    PublicDataTools<FL_QuotedCollectionsPlan>.GetDataByWhereParameter(ObjParList, OrdreByColumname, 100000, 1, out SourceCount);
                    break;



            }
            return new List<SS_Report>();
        }


        /// <summary>
        /// 返回现金流
        /// </summary>
        /// <param name="SSType"></param>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <returns></returns>
        public decimal GetCollectionsPlanByEmployee(DateTime Start, DateTime End, int EmployeeID)
        {
            return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CollectionTime >= Start && C.CollectionTime <= End && C.CreateEmpLoyee == EmployeeID && C.CreateEmpLoyee != null).Sum(C => C.Amountmoney).ToString().ToDecimal();
        }


        /// <summary>
        /// 获取当月现金流
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <returns></returns>
        public decimal GetCollectionsPlanByMonth(int year, int Month)
        {

            if (Month == 13)
            {
                return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CollectionTime.Value.Year == year && C.RealityAmount != null).ToList().Sum(C => C.RealityAmount.Value);
            }
            else
            {
                return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CollectionTime.Value.Year == year && C.CollectionTime.Value.Month == Month && C.RealityAmount != null).ToList().Sum(C => C.RealityAmount.Value);

            }
        }


        /// <summary>
        /// 获取当月到店客户量
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <returns></returns>
        public int GetNewCustomerByMonth(int year, int Month, int EmployeeId = 0)
        {
            if (EmployeeId == 0)
            {
                if (Month == 13)
                {
                    return ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.State >= 9 && C.State != 29).Count();
                }
                else
                {
                    return ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.OrderCreateDate.Value.Month == Month && C.State >= 9 && C.State != 29).Count();

                }
            }
            else
            {
                if (Month == 13)
                {
                    return ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId && C.State >= 9 && C.State != 29).Count();
                }
                else
                {
                    return ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.OrderCreateDate.Value.Month == Month && C.QuotedEmployee.Value == EmployeeId && C.State >= 9 && C.State != 29).Count();

                }
            }
        }


        /// <summary>
        /// 获取当月到店签单量
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <returns></returns>
        public int GetSucessCustomerByMonth(int year, int Month, int EmployeeId = 0)
        {
            if (EmployeeId == 0)
            {
                if (Month == 13)
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year).Count();
                    return ObjEntity.View_GetMinDateCollectionPlan.Where(C => C.MinCollectionDate != null && C.MinCollectionDate.Value.Year == year).Count();
                }
                else
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == Month).Count();
                    return ObjEntity.View_GetMinDateCollectionPlan.Where(C => C.MinCollectionDate != null && C.MinCollectionDate.Value.Year == year && C.MinCollectionDate.Value.Month == Month).Count();
                }
            }
            else
            {
                if (Month == 13)
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId).Count();
                    return (from C in ObjEntity.View_GetMinDateCollectionPlan
                            join D in ObjEntity.SS_Report on C.CustomerID equals D.CustomerID
                            where C.MinCollectionDate != null && C.MinCollectionDate.Value.Year == year
                            && D.QuotedEmployee == EmployeeId
                            select C).Count();
                }
                else
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == Month && C.QuotedEmployee.Value == EmployeeId).Count();
                    return (from C in ObjEntity.View_GetMinDateCollectionPlan
                            join D in ObjEntity.SS_Report on C.CustomerID equals D.CustomerID
                            where C.MinCollectionDate != null && C.MinCollectionDate.Value.Year == year
                            && C.MinCollectionDate.Value.Month == Month && D.QuotedEmployee == EmployeeId
                            select C).Count();
                }
            }
        }

        #region 本月婚期的客户
        /// <summary>
        /// 获取当月客户数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetSucessCustomerCountByYearMonth(int year, int month, int EmployeeId = 0)
        {
            if (EmployeeId == 0)
            {
                if (month == 13)
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year).Count();
                    return ObjEntity.View_SSCustomer.Where(C => C.Partydate.Value.Year == year && C.State != 29 && C.State != 202 && C.State != 203 && C.State != 300 && C.State >= 14).Count();
                }
                else
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == month).Count();
                    return ObjEntity.View_SSCustomer.Where(C => C.Partydate.Value.Year == year && C.Partydate.Value.Month == month && C.State != 29 && C.State != 202 && C.State != 300 && C.State != 203 && C.State >= 14).Count();
                }
            }
            else
            {
                if (month == 13)
                {
                    //return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId).Count();
                    return ObjEntity.View_SSCustomer.Where(C => C.Partydate.Value.Year == year && C.QuotedEmployee == EmployeeId && C.State != 29 && C.State != 202 && C.State != 300 && C.State != 203 && C.State >= 14).Count();
                }
                else
                {
                    //    return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == month && C.QuotedEmployee.Value == EmployeeId).Count();
                    return ObjEntity.View_SSCustomer.Where(C => C.Partydate.Value.Year == year && C.Partydate.Value.Month == month && C.QuotedEmployee == EmployeeId && C.State != 29 && C.State != 202 && C.State != 300 && C.State != 203 && C.State >= 14).Count();
                }
            }
        }


        /// <summary>
        /// 获取当月平均消费
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public decimal? GeAvgtQuotedMoneyByMonth(int year, int month, int EmployeeId = 0)
        {
            if (EmployeeId == 0)
            {
                if (month == 13)
                {
                    return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year).Average(C => C.QuotedMoney);
                }
                else
                {
                    return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == month).Average(C => C.QuotedMoney);
                }
            }
            else
            {
                if (month == 13)
                {
                    return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId).Average(C => C.QuotedMoney);
                }
                else
                {
                    return ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == month && C.QuotedEmployee.Value == EmployeeId).Average(C => C.QuotedMoney);
                }
            }
        }


        #endregion


        /// <summary>
        /// 获取当月到成交率
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <returns></returns>
        public string GetTurnoverRateByMonth(int year, int Month, int EmployeeId = 0)
        {
            if (EmployeeId == 0)
            {
                if (Month == 13)
                {
                    var CustomerCount = ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year).Count();
                    var SucessCount = ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year).Count();
                    if (CustomerCount > 0)
                    {
                        return (SucessCount.ToString().ToDecimal() / CustomerCount.ToString().ToDecimal()).ToString("0.00%");
                    }
                }
                else
                {
                    var CustomerCount = ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.OrderCreateDate.Value.Month == Month).Count();
                    var SucessCount = ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == Month).Count();
                    if (CustomerCount > 0)
                    {
                        return (SucessCount.ToString().ToDecimal() / CustomerCount.ToString().ToDecimal()).ToString("0.00%");
                    }
                }
            }
            else
            {
                if (Month == 13)
                {
                    var CustomerCount = ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId).Count();
                    var SucessCount = ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId).Count();
                    if (CustomerCount > 0)
                    {
                        return (SucessCount.ToString().ToDecimal() / CustomerCount.ToString().ToDecimal()).ToString("0.00%");
                    }
                }
                else
                {
                    var CustomerCount = ObjEntity.SS_Report.Where(C => C.OrderCreateDate.Value.Year == year && C.OrderCreateDate.Value.Month == Month && C.QuotedEmployee.Value == EmployeeId).Count();
                    var SucessCount = ObjEntity.SS_Report.Where(C => C.QuotedDateSucessDate.Value.Year == year && C.QuotedDateSucessDate.Value.Month == Month && C.QuotedEmployee.Value == EmployeeId).Count();
                    if (CustomerCount > 0)
                    {
                        return (SucessCount.ToString().ToDecimal() / CustomerCount.ToString().ToDecimal()).ToString("0.00%");
                    }
                }
            }

            return "0";
        }



        /// <summary>
        /// 获取当月完工额
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public decimal GetCustomFinishSumMoneyByMonth(int year, int Month, int EmployeeId = 0)
        {
            if (EmployeeId == 0)
            {

                if (Month == 13)
                {
                    return ObjEntity.View_CustomerQuoted.Where(C => C.PartyDate.Value.Year == year && C.FinishAmount != null && C.State != 29 && C.State != 202 && C.State != 203 && C.State >= 14).ToList().Sum(C => C.FinishAmount.Value);

                }
                else
                {
                    return ObjEntity.View_CustomerQuoted.Where(C => C.PartyDate.Value.Year == year && C.PartyDate.Value.Month == Month && C.FinishAmount != null && C.State != 29 && C.State != 202 && C.State != 203 && C.State >= 14).ToList().Sum(C => C.FinishAmount.Value);
                }
            }
            else
            {
                if (Month == 13)
                {
                    return ObjEntity.View_CustomerQuoted.Where(C => C.PartyDate.Value.Year == year && C.QuotedEmployee.Value == EmployeeId && C.FinishAmount != null && C.State != 29 && C.State != 202 && C.State != 203 && C.State >= 14).ToList().Sum(C => C.FinishAmount.Value);

                }
                else
                {
                    return ObjEntity.View_CustomerQuoted.Where(C => C.PartyDate.Value.Year == year && C.PartyDate.Value.Month == Month && C.QuotedEmployee.Value == EmployeeId && C.FinishAmount != null && C.State != 29 && C.State != 202 && C.State != 203 && C.State >= 14).ToList().Sum(C => C.FinishAmount.Value);
                }
            }

        }


        /// <summary>
        /// 获取成本
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetCostByStratOrEnd(string Channel, int Year, DateTime Star, DateTime End)
        {

            if (Channel == "")
            {
                var Query = (from C in ObjEntity.FL_CostSum
                             join D in ObjEntity.FL_Customers
                                on C.CustomerId equals D.CustomerID
                             where D.PartyDate >= Star && C.CreateDate <= End && C.Sumtotal != null
                             select C
                                 ).ToList().Sum(C => C.Sumtotal);
                return Query.ToString().ToDecimal();
            }
            else
            {
                var Query = (from C in ObjEntity.FL_CostSum
                             join D in ObjEntity.FL_Customers
                                on C.CustomerId equals D.CustomerID
                             where D.PartyDate >= Star && D.PartyDate <= End && D.Channel == Channel && C.Sumtotal != null
                             select C
                 ).ToList().Sum(C => C.Sumtotal);
                return Query.ToString().ToDecimal();
            }
        }

        //根据渠道类型获取成本
        public decimal GetCostByStratOrEndType(int ChannelTypeID, int Year, DateTime Star, DateTime End)
        {

            if (ChannelTypeID == 0)
            {
                var Query = (from C in ObjEntity.FL_CostSum
                             join D in ObjEntity.FL_Customers
                                on C.CustomerId equals D.CustomerID
                             where D.PartyDate >= Star && C.CreateDate <= End && C.Sumtotal != null
                             select C
                                 ).ToList().Sum(C => C.Sumtotal);
                return Query.ToString().ToDecimal();
            }
            else
            {
                var Query = (from C in ObjEntity.FL_CostSum
                             join D in ObjEntity.FL_Customers
                                on C.CustomerId equals D.CustomerID
                             where D.PartyDate >= Star && D.PartyDate <= End && D.ChannelType == ChannelTypeID && C.Sumtotal != null
                             select C
                 ).ToList().Sum(C => C.Sumtotal);
                return Query.ToString().ToDecimal();
            }
        }



        /// <summary>
        /// 获取成本 起止时间
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetCostByMonth(string Channel, string WineShop, int Year, int Month)
        {

            if (Channel == "")
            {
                if (Month == 13)
                {

                    var Query = (from C in ObjEntity.FL_CostSum
                                 join D in ObjEntity.FL_Customers
                                    on C.CustomerId equals D.CustomerID
                                 where C.CreateDate.Year == Year && D.Wineshop == WineShop
                                 select C
                                     ).ToList().Sum(C => C.Sumtotal);
                    return Query.ToString().ToDecimal();//ObjEntity.FL_CostforOrder.Where(C => C.CreateDate.Year == Year).Sum(C => C.FinishCost);
                }
                else
                {
                    var Query = (from C in ObjEntity.FL_CostSum
                                 join D in ObjEntity.FL_Customers
                                    on C.CustomerId equals D.CustomerID
                                 where C.CreateDate.Year == Year && C.CreateDate.Month == Month && D.Wineshop == WineShop
                                 select C
                     ).ToList().Sum(C => C.Sumtotal);
                    return Query.ToString().ToDecimal();
                    // return ObjEntity.FL_CostforOrder.Where(C => C.CreateDate.Year == Year && C.CreateDate.Month == Month).Sum(C=>C.FinishCost);
                }
            }
            else
            {
                if (Month == 13)
                {

                    var Query = (from C in ObjEntity.FL_CostSum
                                 join D in ObjEntity.FL_Customers
                                    on C.CustomerId equals D.CustomerID
                                 where C.CreateDate.Year == Year && D.Channel == Channel && D.Wineshop == WineShop
                                 select C
                                     ).ToList().Sum(C => C.Sumtotal);
                    return Query.ToString().ToDecimal();//ObjEntity.FL_CostforOrder.Where(C => C.CreateDate.Year == Year).Sum(C => C.FinishCost);
                }
                else
                {
                    var Query = (from C in ObjEntity.FL_CostSum
                                 join D in ObjEntity.FL_Customers
                                    on C.CustomerId equals D.CustomerID
                                 where C.CreateDate.Year == Year && C.CreateDate.Month == Month && D.Channel == Channel && D.Wineshop == WineShop
                                 select C
                     ).ToList().Sum(C => C.Sumtotal);
                    return Query.ToString().ToDecimal();
                    // return ObjEntity.FL_CostforOrder.Where(C => C.CreateDate.Year == Year && C.CreateDate.Month == Month).Sum(C=>C.FinishCost);
                }
            }
        }



        /// <summary>
        /// 获取成本
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetCostByEmployee(int EmployeeID, int Year, int Month)
        {

            if (Month == 13)
            {
                //return ObjEntity.FL_CostSum.Where(C => C.CreateDate.Year == Year).Sum(C => C.Sumtotal).ToString().ToDecimal();
                return (from C in ObjEntity.FL_CostSum
                        join D in ObjEntity.FL_Customers
                          on C.CustomerId equals D.CustomerID
                        where D.PartyDate.Value.Year == Year && C.ActualSumTotal != null
                        select C).ToList().Sum(C => C.ActualSumTotal).ToString().ToDecimal();
            }
            else
            {
                // return ObjEntity.FL_CostSum.Where(C => C.CreateDate.Year == Year && C.CreateDate.Month == Month).Sum(C => C.Sumtotal).ToString().ToDecimal();
                return (from C in ObjEntity.FL_CostSum
                        join D in ObjEntity.FL_Customers
                          on C.CustomerId equals D.CustomerID
                        where D.PartyDate.Value.Year == Year && D.PartyDate.Value.Month == Month && C.ActualSumTotal != null
                        select C).ToList().Sum(C => C.ActualSumTotal).ToString().ToDecimal();
            }

        }

        /// <summary>
        /// 获取当年或者上年合计
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="SSType"></param>
        /// <returns></returns>
        public string GetSumReport(int EmployeeID, int SSType, int Year)
        {
            var FinishDate = DateTime.Now;
            int Month = 12;
            var ThisYear = string.Empty;
            switch (SSType)
            {
                case 1:
                    ThisYear = ObjEntity.SS_Report.Where(C => C.Emoney != null && C.OrderSucessDate.Value.Year == Year).ToList().Sum(C => C.Emoney).ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.OrderEmployee == EmployeeID && C.Emoney != null && C.OrderSucessDate.Value.Year == (Year - 1)).ToList().Sum(C => C.Emoney).ToString();
                    break;
                case 2:

                    ThisYear = ObjEntity.SS_Report.Where(C => C.Emoney != null && C.OrderSucessDate.Value.Year == Year).Count().ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.Emoney != null && C.OrderSucessDate.Value.Year == (Year - 1)).Count().ToString();

                    break;
                case 3:
                    ThisYear = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == Year).ToList().Sum(C => C.QuotedMoney).ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == (Year - 1)).ToList().Sum(C => C.QuotedMoney).ToString();
                    break;
                case 4:
                    ThisYear = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == Year).Count().ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == (Year - 1)).Count().ToString();
                    break;
                case 5:
                    var ThisYearCount = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == Year).Count().ToString();
                    var ThisYearMoney = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == Year).ToList().Sum(C => C.QuotedMoney).ToString();

                    var LastYearCount = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == (Year - 1)).Count().ToString();
                    var LastMoney = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.QuotedCreateDate.Value.Year == (Year - 1)).ToList().Sum(C => C.QuotedMoney).ToString();


                    if (ThisYearCount.ToInt32() > 0)
                    {
                        if (LastYearCount.ToInt32() > 0)
                        {
                            return (ThisYearMoney.ToDecimal() / ThisYearCount.ToDecimal()).ToString("0.00") + "," + (LastMoney.ToDecimal() / LastYearCount.ToDecimal()).ToString("0.00");
                        }
                        else
                        {
                            return (ThisYearMoney.ToDecimal() / ThisYearCount.ToDecimal()).ToString("0.00") + ",0.00";
                        }
                    }
                    else
                    {
                        return ThisYearMoney + "," + "0";
                    }
                    break;

                case 6:

                    ThisYear = ObjEntity.SS_Report.Where(C => C.CreateDate.Year == Year).Count().ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.CreateDate.Year == Year).Count().ToString();
                    break;
                case 7:
                    var ThisYearCreateCount = ObjEntity.SS_Report.Where(C => C.CreateDate.Year == Year).Count().ToString();
                    var ThisYearSucessCount = ObjEntity.SS_Report.Where(C => C.Emoney != null && C.CreateDate.Year == Year).ToList().Count().ToString();

                    var LastYearCreateCount = ObjEntity.SS_Report.Where(C => C.CreateDate.Year == (Year - 1)).Count().ToString();
                    var LastYearSucessCount = ObjEntity.SS_Report.Where(C => C.Emoney != null && C.CreateDate.Year == (Year - 1)).ToList().Count().ToString();

                    if (ThisYearCreateCount.ToInt32() > 0)
                    {

                        if (LastYearCreateCount.ToInt32() > 0)
                        {
                            return (ThisYearSucessCount.ToDecimal() / ThisYearCreateCount.ToDecimal()).ToString("0.00") + "," + (LastYearSucessCount.ToDecimal() / LastYearCreateCount.ToDecimal()).ToString("0.00");
                        }
                        else
                        {
                            return (ThisYearSucessCount.ToDecimal() / ThisYearCreateCount.ToDecimal()).ToString("0.00") + ",0.00";
                        }

                    }
                    else
                    {
                        return ThisYearSucessCount + ",0";
                    }
                    break;

                case 8:
                    ThisYear = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CreateDate.Value.Year == Year).Sum(C => C.Amountmoney).ToString();
                    return ThisYear + "," + ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CreateDate.Value.Year == (Year - 1)).Sum(C => C.Amountmoney).ToString();
                    break;
                case 9:

                    ThisYear = ObjEntity.SS_Report.Where(C => C.OrderEmployee == EmployeeID && C.QuotedMoney != null && C.Partydate < FinishDate).ToList().Sum(C => C.QuotedMoney).ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.OrderEmployee == EmployeeID && C.QuotedMoney != null && C.Partydate.Value.Year == FinishDate.Year).ToList().Sum(C => C.QuotedMoney).ToString();
                    break;
                case 10:

                    ThisYear = ObjEntity.SS_Report.Where(C => C.OrderEmployee == EmployeeID && C.QuotedMoney != null && C.Partydate < FinishDate).ToList().Count().ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Where(C => C.OrderEmployee == EmployeeID && C.QuotedMoney != null && C.Partydate.Value.Year == (Year - 1)).ToList().Count().ToString();
                    break;
                case 11:


                    //成本
                    var ThisYearPrice = (from C in ObjEntity.FL_Dispatching
                                         from D in ObjEntity.FL_Customers
                                         from E in ObjEntity.FL_ProductforDispatching
                                         where D.CustomerID == C.CustomerID && D.CustomerID == E.CustomerID && D.PartyDate.Value.Year == Year && D.PartyDate.Value.Month == Month
                                         select E
                                   ).ToList().Sum(C => C.PurchasePrice);
                    //完工额
                    var ThisYearSale = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.Partydate < FinishDate).ToList().Sum(C => C.QuotedMoney);


                    //成本
                    var LastYearPrice = (from C in ObjEntity.FL_Dispatching
                                         from D in ObjEntity.FL_Customers
                                         from E in ObjEntity.FL_ProductforDispatching
                                         where D.CustomerID == C.CustomerID && D.CustomerID == E.CustomerID && D.PartyDate.Value.Year == (Year - 1) && D.PartyDate.Value.Month == Month
                                         select E
                                   ).ToList().Sum(C => C.PurchasePrice);
                    //完工额
                    var LastYearSale = ObjEntity.SS_Report.Where(C => C.QuotedMoney != null && C.Partydate.Value.Year == (Year - 1)).ToList().Sum(C => C.QuotedMoney);

                    return (ThisYearSale - ThisYearPrice).ToString() + "," + (LastYearSale - LastYearPrice).ToString();
                    break;

                case 12:
                    ThisYear = ObjEntity.SS_Report.Count(C => C.ChannelType != null && C.CreateDate.Year == Year).ToString();
                    return ThisYear + "," + ObjEntity.SS_Report.Count(C => C.ChannelType != null && C.CreateDate.Year == (Year - 1)).ToString();
                    break;
                case 13:
                    //有效的
                    var effective = ObjEntity.SS_Report.Count(C => C.ChannelType != null && C.CreateDate.Year == Year).ToString().ToDecimal();
                    var Lose = ObjEntity.SS_Report.Count(C => C.ISeffective == false && C.CreateDate.Year == Year).ToString().ToDecimal();

                    var Lasteffective = ObjEntity.SS_Report.Count(C => C.ChannelType != null && C.CreateDate.Year == (Year - 1)).ToString().ToDecimal();
                    var LastLose = ObjEntity.SS_Report.Count(C => C.ISeffective == false && C.CreateDate.Year == (Year - 1)).ToString().ToDecimal();
                    if (effective > 0)
                    {
                        if (Lasteffective > 0)
                        {
                            return (Lose / effective).ToString() + "," + (LastLose / Lasteffective).ToString();
                        }
                        else
                        {
                            return "0,0";
                        }
                    }
                    return "0,0";
                    break;
                case 14:
                    return "0,0";
                    break;
                case 15:
                    return "0,0";
                    break;

            }
            return string.Empty;
        }


        /// <summary>
        /// 修改新人信息
        /// </summary>
        /// <param name="ObjModel"></param>
        public void Update(SS_Report ObjModel)
        {
            ObjEntity.SaveChanges();

        }

        public int Updates(SS_Report ObjModel)
        {
            if (ObjModel != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }


        public SS_Report GetByID(int key)
        {
            return ObjEntity.SS_Report.FirstOrDefault(C => C.SSID == key);
        }



        /// <summary>
        /// 获取某个新人的统计信息
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public SS_Report GetByCustomerID(int CustomerID, int? EmployeeID)
        {
            var ReturnValue = ObjEntity.SS_Report.FirstOrDefault(C => C.CustomerID == CustomerID);
            if (ReturnValue == null)
            {
                Insert(new SS_Report() { CreateDate = DateTime.Now, CustomerID = CustomerID, CreateEmployee = EmployeeID });
                return ObjEntity.SS_Report.FirstOrDefault(C => C.CustomerID == CustomerID);
            }
            else
            {
                return ReturnValue;
            }
        }

        public SS_Report GetByCustomerID(int CustomerID)
        {
            return ObjEntity.SS_Report.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        /// <summary>
        /// 创建新人统计信息
        /// </summary>
        public void Insert(SS_Report ObjModel)
        {
            ObjEntity.SS_Report.Add(ObjModel);
            ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 返回本月销售额
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetSumQuotedMoneyByMonth(int EmployeeId, int CustomerId, int Month)
        {
            var ThisMonth = string.Empty;
            ThisMonth = ObjEntity.SS_Report.Where(C => C.QuotedEmployee == EmployeeId && C.TargetState == 0 && C.CustomerID == CustomerId && C.QuotedMoney != null && C.OrderSucessDate.Value.Month == Month).ToList().Sum(C => C.QuotedMoney).ToString();
            decimal sum = Convert.ToDecimal(ThisMonth.ToString());
            return sum;
        }

        /// <summary>
        /// 到店客户
        /// </summary>
        /// <returns></returns>
        public int GetComeOrderCountByDate(DateTime Start, DateTime End, int EmployeeID)
        {
            //return ObjEntity.FL_Order.Where(C => C.CreateDate >= Start && C.CreateDate <= End && C.EmployeeID == EmployeeID && C.EmployeeID != null).ToList().Count;

            return (from C in ObjEntity.SS_Report join D in ObjEntity.FL_Order on C.CustomerID equals D.CustomerID where C.OrderCreateDate >= Start && C.OrderCreateDate <= End && (D.EmployeeID == EmployeeID) select D).Count();

        }



    }
}
