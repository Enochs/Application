
/**
 Version :HaoAi 1.0
 File Name :客户满意度调查类
 Author:杨洋
 Date:2013.3.25
 Description:客户投诉类 实现ICRUDInterface<T> 接口中的方法
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
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.CS
{
    public class DegreeOfSatisfaction : ICRUDInterface<CS_DegreeOfSatisfaction>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 获取本人的满意度调查
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<View_DispatchingDegreeOfSatisfaction> GetTakeDiskByParameter(int PageSize, int PageIndex, out int SourceCount, ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_DispatchingDegreeOfSatisfaction>.GetDataByParameter(new View_DispatchingDegreeOfSatisfaction(), ObjParameterList);
            SourceCount = query.Count();

            List<View_DispatchingDegreeOfSatisfaction> resultList = query.OrderByDescending(C => C.CreateDate)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_DispatchingDegreeOfSatisfaction>();
            }
            return resultList;
        }



        /// <summary>
        /// 获取满意率 根据婚期获取
        /// </summary>
        /// <param name="TimerStar"></param>
        /// <param name="TimerEnd"></param>
        /// <returns></returns>
        public string GetManyidu(DateTime TimerStar, DateTime TimerEnd)
        {

            decimal AllCount = (from C in ObjEntity.CS_DegreeOfSatisfaction
                                join QT in ObjEntity.View_CustomerQuoted
                                on C.CustomerID equals QT.CustomerID

                                where QT.PartyDate >= TimerStar && QT.PartyDate <= TimerEnd

                                select C).Count();


            decimal ManyiCount = (from C in ObjEntity.CS_DegreeOfSatisfaction
                                  join QT in ObjEntity.View_CustomerQuoted
                                  on C.CustomerID equals QT.CustomerID

                                  where QT.PartyDate >= TimerStar && QT.PartyDate <= TimerEnd && C.SumDof == "很好"

                                  select C).Count();
            if (AllCount > 0)
            {
                return (ManyiCount / AllCount).ToString("0.00%") + "";
            }
            else
            {
                return string.Empty;
            }
        }

        public int Delete(CS_DegreeOfSatisfaction ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeOfSatisfaction.FirstOrDefault(
                 C => C.DofKey == ObjectT.DofKey).IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }


        public int Deletes(CS_DegreeOfSatisfaction ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeOfSatisfaction.Remove(GetByID(ObjectT.DofKey));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public CS_DegreeOfSatisfaction GetByCustomerID(int CustomerID)
        {
            return ObjEntity.CS_DegreeOfSatisfaction.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        public List<CS_DegreeOfSatisfaction> GetByAll()
        {
            return ObjEntity.CS_DegreeOfSatisfaction.Where(C => C.IsDelete == false).ToList();
        }

        public CS_DegreeOfSatisfaction GetByID(int? KeyID)
        {
            return ObjEntity.CS_DegreeOfSatisfaction.FirstOrDefault(C => C.DofKey == KeyID);
        }
        public CS_DegreeOfSatisfaction GetByCustomersID(int? KeyID)
        {
            return ObjEntity.CS_DegreeOfSatisfaction.FirstOrDefault(C => C.CustomerID == KeyID);
        }
        ///// <summary>
        ///// 该方法是为了提供新人满意度调查表的中显示的字段
        ///// </summary>
        ///// <param name="ObjParameterList"></param>
        ///// <param name="PageSize"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="SourceCount"></param>
        ///// <returns></returns>
        //public List<CS_DegreeOfSatisfactionContentItem> GetbyDegreeOfSatisfactionContentItemParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        //{
        //    var query = PublicDataTools<CS_DegreeOfSatisfactionContentItem>.GetDataByParameter(new CS_DegreeOfSatisfactionContentItem(), ObjParameterList);
        //    SourceCount = query.Count();

        //    List<CS_DegreeOfSatisfactionContentItem> resultList = query.OrderByDescending(C => C.DofKey)
        //      .Skip(PageSize * (PageIndex - 1)).Take(PageSize).Where(C => C.IsDelete == false).ToList();

        //    if (query.Count == 0)
        //    {
        //        resultList = new List<CS_DegreeOfSatisfactionContentItem>();
        //    }
        //    return resultList;
        //}

        public List<CS_DegreeOfSatisfaction> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_DegreeOfSatisfaction.Count();

            List<CS_DegreeOfSatisfaction> resultList = ObjEntity.CS_DegreeOfSatisfaction.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.DofKey)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).Where(C => C.IsDelete == false).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_DegreeOfSatisfaction>();
            }
            return resultList;
        }


        /// <summary>
        ///根据参数查询
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetDegreeOfSatisfaction> GetDegreeOfSatisfactionByDofID(int EofKey)
        {
            return ObjEntity.View_GetDegreeOfSatisfaction.Where(C => C.DofKey == EofKey).ToList();
        }


        /// <summary>
        /// 根据条件获取需要调查的玩意
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        [Obsolete("请使用GetByWhereParameter")]
        public List<View_GetDefrreSatisaction> GetDefrreSatisactionByParameter(int PageSize, int PageIndex, out int SourceCount, ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_GetDefrreSatisaction>.GetDataByParameter(new View_GetDefrreSatisaction(), ObjParameterList);
            SourceCount = query.Count();

            List<View_GetDefrreSatisaction> resultList = query.OrderBy(C => C.PartyDate)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).Where(C => C.IsDelete == false).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_GetDefrreSatisaction>();
            }
            return resultList;
        }

        /// <summary>
        /// 根据条件获取需要调查的玩意
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_GetDefrreSatisaction> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<View_GetDefrreSatisaction>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }


        /// <summary>
        /// 婚期查询  起止时间
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        /// 
        public List<View_GetDefrreSatisaction> GetAllByDate(DateTime Start, DateTime End, string EmployeeType, int EmployeeID)
        {
            if (EmployeeID > 0)
            {
                if (EmployeeType == "CreateEmpLoyee")       //顾问
                {

                    return (from C in ObjEntity.View_DispatchingCustomers join D in ObjEntity.View_GetDefrreSatisaction on C.CustomerID equals D.CustomerID where C.PartyDate >= Start && C.PartyDate <= End && C.CreateEmpLoyee == EmployeeID select D).ToList();
                }
                else if (EmployeeType == "QuotedEmployee")      //策划师
                {

                    return (from C in ObjEntity.View_DispatchingCustomers join D in ObjEntity.View_GetDefrreSatisaction on C.CustomerID equals D.CustomerID where C.PartyDate >= Start && C.PartyDate <= End && C.QuotedEmpLoyee == EmployeeID select D).ToList();

                }
                else if (EmployeeType == "-1")          //默认 策划师
                {
                    return (from C in ObjEntity.View_DispatchingCustomers join D in ObjEntity.View_GetDefrreSatisaction on C.CustomerID equals D.CustomerID where C.PartyDate >= Start && C.PartyDate <= End && C.QuotedEmpLoyee == EmployeeID select D).ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return (from C in ObjEntity.View_DispatchingCustomers join D in ObjEntity.View_GetDefrreSatisaction on C.CustomerID equals D.CustomerID where C.PartyDate >= Start && C.PartyDate <= End select D).ToList();
            }
        }


        /// <summary>
        /// 统计 K线图  起止时间
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        /// 
        public List<View_GetDefrreSatisaction> GetAllByDates(DateTime Start, DateTime End)
        {
            return ObjEntity.View_GetDefrreSatisaction.Where(C => C.PartyDate >= Start && C.PartyDate <= End).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="sourceCount"></param>
        /// <param name="paras"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<View_GetDefrreSatisaction> GetDefrreSatisaction(int pageSize, int pageIndex, out int sourceCount, ObjectParameter[] paras, Func<View_GetDefrreSatisaction, bool> predicate)
        {

            var query = PublicDataTools<View_GetDefrreSatisaction>.GetDataByParameter(new View_GetDefrreSatisaction(), paras).Where(predicate);
            sourceCount = query.Count();

            List<View_GetDefrreSatisaction> result = query.OrderByDescending(C => C.PlanDate)
              .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return result.Count.Equals(0) ? new List<View_GetDefrreSatisaction>() : result;
        }



        public int Insert(CS_DegreeOfSatisfaction ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeOfSatisfaction.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DofKey;
                }

            }
            return 0;
        }

        public int Update(CS_DegreeOfSatisfaction ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.DofKey;
            }
            return 0;
        }
    }
}
