/**临时注释
 订单表（成功邀约到店后 生成初步订单 进行跟单）
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class Order : ICRUDInterface<FL_Order>
    {
        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 获取统计类型
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetOrderSumByWhere(List<ObjectParameter> ObjParameterList, int Type)
        {
            switch (Type)
            {

                //预约量
                case 1:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Count.ToString();

                    break;
                case 2:
                    //到店量
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.State > 5).Count().ToString();
                    break;
                case 3:
                    //成功预定量
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.State >= 13 && C.State < 29).Count().ToString();
                    break;
                //总定金金额
                case 4:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Sum(C => C.EarnestMoney).ToString(); ;
                    break;
                //总订单额
                case 5:
                    var SumQuotedPrice =
                          from O in ObjEntity.FL_Order
                          from Q in ObjEntity.FL_QuotedPrice
                          where O.OrderID == Q.OrderID
                          select Q;
                    return SumQuotedPrice.Sum(C => C.FinishAmount).ToString();
                    break;
                //流失量
                case 6:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Count.ToString();
                    break;
                case 7:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Count.ToString();
                    break;
                //完工对数
                case 8:
                    ObjParameterList.Add(new ObjectParameter("IsDispatching", 4));
                    return PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParameterList.ToArray()).Count.ToString();
                    break;
                case 9:
                    ObjParameterList.Add(new ObjectParameter("IsDispatching", 4));
                    //ObjParameterList.Add(new ObjectParameter("PartyDate_between", (Year) + "-1-01," + Year + "-" + Month + "-" + DateTime.DaysInMonth((Year), 12)));
                    return PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParameterList.ToArray()).Sum(C => C.FinishAmount).ToString();
                    break;

            }
            return string.Empty;
        }

        /// <summary>
        /// 获取统计类型
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetOrderSumByDateTime(List<ObjectParameter> ObjParameterList, int Year, int Month, int Type)
        {
            switch (Type)
            {

                //预约量
                case 1:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month).Count().ToString();


                    break;
                case 2:
                    //到店量
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month).Where(C => C.State > 5).Count().ToString();
                    break;
                case 3:
                    //成功预定量
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month).Where(C => C.State >= 13 && C.State < 29).Count().ToString();
                    break;
                //总定金金额
                case 4:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month).Sum(C => C.EarnestMoney).ToString(); ;
                    break;
                //总订单额
                case 5:
                    var SumQuotedPrice =
                          from O in ObjEntity.FL_Order
                          from Q in ObjEntity.FL_QuotedPrice
                          where O.OrderID == Q.OrderID &&
                          O.CreateDate.Value.Year == Year && O.CreateDate.Value.Month == Month
                          select Q;
                    return SumQuotedPrice.Sum(C => C.FinishAmount).ToString();
                    break;
                //流失量
                case 6:
                    return PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParameterList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State == 29).Count().ToString();
                    break;
                //完工对数
                case 8:
                    return ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount != null && C.PartyDate.Value.Year == Year && C.PartyDate.Value.Month == Month && C.IsDispatching > 3).Count().ToString();
                    break;
                //完工额
                case 9:
                    return ObjEntity.View_CustomerQuoted.Where(C => C.FinishAmount != null && C.PartyDate.Value.Year == Year && C.PartyDate.Value.Month == Month && C.IsDispatching > 3).Sum(C => C.FinishAmount).ToString();
                    break;

                //case 9:
                //    ObjParameterList.Add(new ObjectParameter("IsDispatching", 4));
                //    ObjParameterList.Add(new ObjectParameter("PartyDate_between", (Year) + "-1-01," + Year + "-"+Month+"-" + DateTime.DaysInMonth((Year), 12)));
                //    return PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParameterList.ToArray()).Count.ToString();
                //    break;
            }
            return string.Empty;
        }

        public int GetOrderSumByYearMonth(int Year, int Month)
        {
            return ObjEntity.FL_Order.Count(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Order ObjectT)
        {
            if (ObjectT != null)
            {
                int CustomerID = ObjectT.CustomerID.Value;
                if (ObjectT != null)
                {
                    OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
                    var ObjItem = ObjEntity.FL_OrderEarnestMoney.FirstOrDefault(C => C.OrderID == ObjectT.OrderID);
                    if (ObjItem != null)
                    {

                        ObjEntity.FL_OrderEarnestMoney.Remove(ObjItem);
                        ObjEntity.SaveChanges();
                    }

                    Celebration ObjCelebrationBLL = new Celebration();
                    ObjCelebrationBLL.DeleteByOrderID(ObjectT.OrderID);

                    ObjEntity.FL_Order.Remove(ObjectT);
                    ObjEntity.SaveChanges();

                    return ObjectT.OrderID;
                }
                else
                {
                    return 0;
                }
            }
            return 0;

        }

        /// <summary>
        /// 或许排序号
        /// </summary>
        /// <param name="PartyDate"></param>
        /// <returns></returns>
        public string GetOrderCoder(DateTime PartyDate)
        {

            return "0" + ObjEntity.FL_Customers.Count(C => C.PartyDate.Value.Day == PartyDate.Day && C.PartyDate.Value.Year == PartyDate.Year);
        }

        /// <summary>
        /// 计算订单号
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string ComputeOrderCoder(int CustomerID)
        {
            DateTime today = System.DateTime.Now.Date; ;
            return DateTime.Now.ToString("yyMMddHH") + (ObjEntity.FL_Customers.Where(C => C.RecorderDate > today).Count() + 1).ToString("0000");
        }

        /// <summary>
        /// 获取邀约流失原因
        /// </summary>
        /// <param name="InviteID"></param>
        /// <returns></returns>
        public string GetLoseContentByOrderID(int? OrderID)
        {
            var ObjModel = ObjEntity.FL_OrderDetails.FirstOrDefault(C => C.OrderID == OrderID && C.LoseContent != null);
            if (ObjModel != null)
            {
                return ObjModel.LoseContent;
            }
            else
            {
                return "未填写";
            }
        }

        public List<FL_Order> GetByAll()
        {
            return ObjEntity.FL_Order.ToList();
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Order GetByID(int? KeyID)
        {
            return ObjEntity.FL_Order.FirstOrDefault(C => C.OrderID == KeyID);
        }

        /// <summary>
        /// 根据新人ID获取跟单信息
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public FL_Order GetbyCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Order.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        public List<FL_Order> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获取订单
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        [Obsolete("请使用GetByWhereParameter（）")]
        public List<View_GetOrderCustomers> GetOrderCustomerByIndex(List<ObjectParameter> ObjParList)
        {


            var DataSource = PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParList.ToArray());

            if (DataSource.Count == 0)
            {
                DataSource = new List<View_GetOrderCustomers>();
            }
            return DataSource;

        }


        /// <summary>
        /// 分页获取跟单数据
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_GetOrderCustomers> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            var ReturnList = PublicDataTools<View_GetOrderCustomers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount, OrderType.Desc);
            return ReturnList;

        }


        /// <summary>
        /// 根据条已经销售派单的用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        [Obsolete("请使用GetByWhereParameter（）")]
        public List<View_GetOrderCustomers> GetOrderCustomerByIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.LastFollowDate).ToList();
            SourceCount = DataSource.Count;

            if (PageIndex >= 0)
            {

                DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();
            }

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetOrderCustomers>();
            }

            return PageDataTools<View_GetOrderCustomers>.AddtoPageSize(DataSource);


        }

        public List<View_GetOrderCustomers> GetOrderCustomer(int PageSize, int PageIndex, out int SourceCount, List<PMSParameters> ObjParList)
        {
            return PublicDataTools<View_GetOrderCustomers>.GetDataByWhereParameter(ObjParList, "CustomerID", PageSize, PageIndex, out SourceCount);
        }
        /// <summary>
        /// 根据条件查找没有进行调查的用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        [Obsolete("请使用GetByWhereParameter（）")]
        public List<View_GetOrderCustomers> GetOrderDegreeCustomerByIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<View_GetOrderCustomers>.GetDataByParameter(new View_GetOrderCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            List<View_GetOrderCustomers> objResult = new List<View_GetOrderCustomers>();
            DegreeOfSatisfaction objCS_DegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
            //已经进行调查的客户

            var objHaveDegreeCustomers = objCS_DegreeOfSatisfactionBLL.GetByAll();
            var IntersectCustomer = (from m in DataSource select m.CustomerID).Except(from m in objHaveDegreeCustomers select m.CustomerID.Value);

            foreach (var itemChild in IntersectCustomer)
            {
                var singer = DataSource.Where(C => C.CustomerID == itemChild).FirstOrDefault();
                if (singer != null)
                {
                    objResult.Add(singer);
                }

            }
            SourceCount = objResult.Count;
            var LastResult = objResult.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                LastResult = new List<View_GetOrderCustomers>();
            }

            return LastResult;


        }

        /// <summary>
        /// 新添加订单计划
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Order ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Order.Add(ObjectT);

                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.OrderID;
                }
                return 0;
            }
            return 0;
        }


        /// <summary>
        /// 更新订单总计划
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_Order ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.OrderID;
            }
            return 0;
        }



        public int GetCustomerOrderBYEmployee(int EmployeeID)
        {
            return ObjEntity.FL_Order.Where(C => C.EmployeeID == EmployeeID).Count();

        }

        public List<FL_Order> Where(System.Linq.Expressions.Expression<Func<FL_Order, bool>> predicate)
        {
            return ObjEntity.FL_Order.Where(predicate).ToList();
        }

        public string GetOrderSum(IEnumerable<View_GetOrderCustomers> source, OrderSumTypes type)
        {
            switch (type)
            {
                //预约量
                case OrderSumTypes.TotalOrderCount: return source.Count().ToString();
                //实际到店量
                case OrderSumTypes.ActualOrderCount: return source.Count(C => C.State > 5).ToString();
                //成功预订量
                case OrderSumTypes.SuccessOrderCount: return source.Count(C => C.State >= 13 && C.State < 29).ToString();
                //定金总额
                case OrderSumTypes.TotalEarnestMoney: return source.Where(C => C.EarnestMoney.HasValue).Sum(C => C.EarnestMoney.Value).ToString("f2");
                //订单总额
                case OrderSumTypes.TotalFinishAmount:
                    IEnumerable<int> orderids = source.Select(C => C.OrderID);
                    return ObjEntity.FL_QuotedPrice.Select(C => new { C.FinishAmount, C.OrderID }).Where(C => C.FinishAmount.HasValue && orderids.Contains(C.OrderID.Value)).ToList().Sum(C => C.FinishAmount.Value).ToString("f2");
                //流失量
                case OrderSumTypes.LoseCount: return source.Count(C => C.State == 29 || C.State == 300).ToString();
                default: return string.Empty;
            }
        }

        public string GetOrderSum(IEnumerable<View_GetOrderCustomers> source, OrderSumTypes type, int year, int month)
        {
            switch (type)
            {
                //预约量
                case OrderSumTypes.TotalOrderCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month).ToString();
                //实际到店量
                case OrderSumTypes.ActualOrderCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month && C.State > 5).ToString();
                //成功预订量
                case OrderSumTypes.SuccessOrderCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month && C.State >= 13 && C.State < 29).ToString();
                //定金总额
                case OrderSumTypes.TotalEarnestMoney: return source.Where(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month && C.EarnestMoney.HasValue).Sum(C => C.EarnestMoney.Value).ToString("f2");
                //订单总额
                case OrderSumTypes.TotalFinishAmount:
                    IEnumerable<int> orderids = source.Where(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month).Select(C => C.OrderID);
                    return ObjEntity.FL_QuotedPrice.Select(C => new { C.FinishAmount, C.OrderID }).Where(C => C.FinishAmount.HasValue && orderids.Contains(C.OrderID.Value)).ToList().Sum(C => C.FinishAmount.Value).ToString("f2");
                //流失量
                case OrderSumTypes.LoseCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month && (C.State == 29 || C.State == 300)).ToString();
                default: return string.Empty;
            }
        }


        /// <summary>
        /// 成功预定页面  获取订金 本期合计
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        public string GetAllRealityAmount(List<View_GetOrderCustomers> DataList)
        {
            //List<int?> OrderIDList = DataList.Select(c => (int?)c.OrderID).ToList();
            //string realityAmount = ObjEntity.FL_QuotedCollectionsPlan.Where(c => c.Node == "定金" && OrderIDList.Contains(c.OrderID)).Sum(c => c.RealityAmount).ToString();
            decimal realityAmount = 0;
            foreach (var item in DataList)
            {
                if (item.OrderID != null && item.OrderID != 0)
                {
                    var datalist = ObjEntity.FL_QuotedCollectionsPlan.Where(c => c.OrderID == item.OrderID).ToList();
                    if (datalist.Count() > 0)
                    {
                        realityAmount += datalist.OrderBy(c => c.CollectionTime).FirstOrDefault().RealityAmount.ToString().ToDecimal();
                    }
                }
            }
            return realityAmount.ToString();

        }
    }
}
