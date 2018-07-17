

/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.13
 Description:电话营销管理 实现ICRUDInterface<T> 接口中的方法
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Telemarketing : ICRUDInterface<FL_Telemarketing>
    {

        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 获取当期入客量
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public int GetCustomerSum(int Year, int Month)
        {


            return ObjEntity.View_GetTelmarketingCustomers.Count(C => C.IsDelete == false && C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month);
        }

        /// <summary>
        /// 当期客源量
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public int GetAllCustomerSum(int DepartmentID, int EmployeeID, DateTime Star, DateTime End)
        {
            if (EmployeeID > 0)
            {
                var SumQuotedPrice =
                             from O in ObjEntity.View_GetTelmarketingCustomers

                             where O.CreateDate >= Star && O.CreateDate <= End && O.EmployeeID == EmployeeID

                             select O;

                return SumQuotedPrice.Count();
            }
            if (DepartmentID > 0)
            {
                var SumQuotedPrice =
             from O in ObjEntity.View_GetTelmarketingCustomers

             where O.CreateDate >= Star && O.CreateDate <= End & O.DepartmentID == DepartmentID

             select O;

                return SumQuotedPrice.Count();
            }

            return 0;
        }


        /// <summary>
        /// 获取有效量
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public int GetAllSucess(int DepartmentID, int EmployeeID, DateTime Star, DateTime End)
        {
            var SumQuotedPrice =
                         from O in ObjEntity.View_GetTelmarketingCustomers
                         from Q in ObjEntity.View_GetInviteCustomers
                         where O.CustomerID == Q.CustomerID
                         && O.CreateDate >= Star && O.CreateDate <= End
                         && O.State > 5 && O.State != 29
                         select Q;

            return SumQuotedPrice.Count();
        }

        /// <summary>
        /// 删除电话营销记录 实际等于删除客户(软删除)
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Telemarketing ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Telemarketing.Remove(GetByID(ObjectT.MarkeID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }




        /// <summary>
        /// 返回所有
        /// </summary>
        /// <returns></returns>
        public List<FL_Telemarketing> GetByAll()
        {
            return ObjEntity.FL_Telemarketing.ToList();
        }


        /// <summary>
        /// 获取电话营销记录 根据ID
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Telemarketing GetByID(int? KeyID)
        {

            return ObjEntity.FL_Telemarketing.FirstOrDefault(C => C.MarkeID == KeyID);
        }
        /// <summary>
        /// 主要是通过客户ID某个客户是否已经分配
        /// </summary>
        /// <param name="CustomersId"></param>
        /// <returns></returns>
        public IList<FL_Telemarketing> GetByCustomersId(int CustomersId)
        {
            return ObjEntity.FL_Telemarketing.Where(C => C.CustomerID == CustomersId).ToList();
        }



        public FL_Telemarketing GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Telemarketing.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        /// <summary>
        /// 分页获取电话营销记录
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Telemarketing> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_Customers.Count();

            List<FL_Telemarketing> resultList = ObjEntity.FL_Telemarketing
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MarkeID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_Telemarketing>();
            }
            return resultList;
        }


        /// <summary>
        ///添加电话营销记录 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Telemarketing ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Telemarketing.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MarkeID;
                }

            }
            return 0;
        }
        /// <summary>
        /// 返回最大的SortOrder值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSortOrder()
        {
            var query = ObjEntity.FL_Telemarketing.Max(C => C.SortOrder);
            //为了初始值没有值默认返回值是0
            if (query != null)
            {
                return query.Value;
            }
            return 0;
        }
        /// <summary>
        /// 修改电话营销记录（主要是修改责任人）
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_Telemarketing ObjectT)
        {
            if (ObjectT != null)
            {
                //ObjEntity.FL_Telemarketing.Attach(ObjectT);

                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// 提供邀约分配明细创建后的查询不含分页功能
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<View_GetTelmarketingCustomers> GetTelmarketingCustomersByParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_GetTelmarketingCustomers>
                .GetDataByPower(new View_GetTelmarketingCustomers(), 0, 0, ObjParameterList).ToList();
            return query.ToList();
        }
        /// <summary>
        /// 提供邀约分配明细创建后的查询
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetTelmarketingCustomers> GetTelmarketingCustomersByParameter(int PageSize, int PageIndex, out int SourceCount, ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_GetTelmarketingCustomers>.GetDataByPower(new View_GetTelmarketingCustomers(), 0, 0, ObjParameterList).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = query.Count();

            List<View_GetTelmarketingCustomers> resultList = query.OrderByDescending(C => C.CreateDate)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_GetTelmarketingCustomers>();
            }

            return resultList.ToList();

        }

        /// <summary>
        /// 分页获取客源明细
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_SSCustomer> GetByWhereParameter1(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_SSCustomer>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }


        /// <summary>
        /// 分页获取客源明细
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_GetTelmarketingCustomers> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<View_GetTelmarketingCustomers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }

    }
}
