
/**
 Version :HaoAi 1.0
 File Name :CustomerReturnVisit
 Author:杨洋
 Date:2013.4.16
 Description:新人回访 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using System.Linq.Expressions;
using System;
using System.Data.Objects;
using System.Data.EntityClient;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class CustomerReturnVisit : ICRUDInterface<FL_CustomerReturnVisit>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public List<FD_InvestigateItem> GetReturnItemByall()
        {
            return ObjEntity.FD_InvestigateItem.ToList();

        }


        /// <summary>
        /// 添加回访调查项目
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int AddReturnItem(FD_InvestigateItem ObjectT)
        {
            ObjEntity.FD_InvestigateItem.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 修改回访调查项目
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int UpdateReturnItem(FD_InvestigateItem ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ItemKey;
            }
            return 0;
        }


        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public FD_InvestigateItem GetReturnItemByID(int ItemKey)
        {
            return ObjEntity.FD_InvestigateItem.FirstOrDefault(C => C.ItemKey == ItemKey);

        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int DeleteReturnItem(FD_InvestigateItem ObjectT)
        {

            ObjEntity.FD_InvestigateItem.Remove(ObjectT);
            return ObjEntity.SaveChanges();
        }

        public int Delete(FL_CustomerReturnVisit ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_CustomerReturnVisit.Remove(GetByID(ObjectT.ReturnId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_CustomerReturnVisit> GetByAll()
        {
            return ObjEntity.FL_CustomerReturnVisit.ToList();
        }

        public FL_CustomerReturnVisit GetByID(int? KeyID)
        {
            return ObjEntity.FL_CustomerReturnVisit.FirstOrDefault(C => C.ReturnId == KeyID);
        }

        public List<FL_CustomerReturnVisit> GetByCustomerID(int? CustomerId)
        {
            return ObjEntity.FL_CustomerReturnVisit.Where(C => C.CustomerId == CustomerId).ToList();
        }

        /// <summary>
        /// 分页绑定回访数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_CustomerReturnVisit> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_CustomerReturnVisit.Count();

            List<FL_CustomerReturnVisit> resultList = ObjEntity.FL_CustomerReturnVisit
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ReturnId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_CustomerReturnVisit>();
            }
            return resultList;
        }
        ///// <summary>
        ///// 新人回访记录
        ///// </summary>
        ///// <param name="ObjParameterList"></param>
        ///// <param name="PageSize"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="SourceCount"></param>
        ///// <returns></returns>
        //public List<FL_CustomersOrderReturn> GetbyFL_CustomersOrderReturnParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        //{
        //    var query = PublicDataTools<FL_CustomersOrderReturn>.GetDataByParameter(new FL_CustomersOrderReturn(), ObjParameterList);
        //    SourceCount = query.Count();

        //    List<FL_CustomersOrderReturn> resultList = query.OrderByDescending(C => C.CustomerID)
        //      .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

        //    if (query.Count == 0)
        //    {
        //        resultList = new List<FL_CustomersOrderReturn>();
        //    }
        //    return resultList;
        //}




        /// <summary>
        ///// 会员查询 返回时间
        ///// </summary>
        ///// <param name="ObjParameterList"></param>
        ///// <returns></returns>
        //public List<GetCustomerReturnVisit> GetbyCustomerReturnVisitParameter(ObjectParameter[] ObjParameterList)
        //{

        //    var query = PublicDataTools<GetCustomerReturnVisit>.GetDataByParameter(new GetCustomerReturnVisit(), ObjParameterList);

        //    return query;
        //}


        ///// <summary>
        ///// 获取回访新人
        ///// </summary>
        ///// <param name="ObjParameterList"></param>
        ///// <param name="PageSize"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="SourceCount"></param>
        ///// <returns></returns>
        //public List<GetCustomerReturnVisit> GetbyCustomerReturnVisitParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        //{

        //    var query = PublicDataTools<GetCustomerReturnVisit>.GetDataByParameter(new GetCustomerReturnVisit(), ObjParameterList);
        //    SourceCount = query.Count();

        //    List<GetCustomerReturnVisit> resultList = query.OrderByDescending(C => C.ReturnDate)
        //      .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

        //    if (query.Count == 0)
        //    {
        //        resultList = new List<GetCustomerReturnVisit>();
        //    }
        //    return resultList;
        //}


        //public List<GetCustomerReturnVisit> GetCustomerReturnVisit(int pageSize, int pageIndex, out int sourceCount, ObjectParameter[] paras, Func<GetCustomerReturnVisit, bool> predicate)
        //{

        //    var query = PublicDataTools<GetCustomerReturnVisit>.GetDataByParameter(new GetCustomerReturnVisit(), paras).Where(predicate);
        //    sourceCount = query.Count();

        //    List<GetCustomerReturnVisit> result = query.OrderByDescending(C => C.ReturnDate)
        //      .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

        //    return result.Count.Equals(0) ? new List<GetCustomerReturnVisit>() : result;
        //}




        /// <summary>
        /// 分页获取回访数据
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_SSCustomer> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<View_SSCustomer>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }



        //public List<FL_CustomersOrderReturn> GetbyFL_CustomersOrderReturnParameter(ObjectParameter[] ObjParameterList)
        //{
        //    var query = PublicDataTools<FL_CustomersOrderReturn>.GetDataByParameter(new FL_CustomersOrderReturn(), ObjParameterList);
        //    return query.ToList();
        //}
        public int Insert(FL_CustomerReturnVisit ObjectT)
        {
            if (ObjectT != null)
            {
                var Model = GetByCustomerID(ObjectT);
                if (Model == null)          //不存在 才添加
                {
                    ObjEntity.FL_CustomerReturnVisit.Add(ObjectT);
                    return ObjEntity.SaveChanges();
                }
            }
            return 0;
        }

        public int Update(FL_CustomerReturnVisit ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ReturnId;
            }
            return 0;
        }


        public FL_CustomerReturnVisit GetByCustomerID(FL_CustomerReturnVisit ObjectT)
        {
            return ObjEntity.FL_CustomerReturnVisit.FirstOrDefault(C => C.CustomerId == ObjectT.CustomerId && C.StateItem == ObjectT.StateItem);
        }
    }
}
