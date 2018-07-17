
/**
 Version :HaoAi 1.0
 File Name :客户取件类
 Author:杨洋
 Date:2013.3.25
 Description:客户取件类 实现ICRUDInterface<T> 接口中的方法
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.CS
{
    public class TakeDisk : ICRUDInterface<CS_TakeDisk>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 获取本人的取件订单
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<View_EmployeeTakeDisk> GetTakeDiskByParameter(int PageSize, int PageIndex, out int SourceCount, ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_EmployeeTakeDisk>.GetDataByParameter(new View_EmployeeTakeDisk(), ObjParameterList);
            SourceCount = query.Count();

            List<View_EmployeeTakeDisk> resultList = query.OrderByDescending(C => C.TakeID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_EmployeeTakeDisk>();
            }
            return resultList;
        }

        public int Delete(CS_TakeDisk ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_TakeDisk.FirstOrDefault(
                 C => C.TakeID == ObjectT.TakeID).IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CS_TakeDisk> GetByAll()
        {
            return ObjEntity.CS_TakeDisk.Where(C => C.IsDelete == false).ToList();
        }

        public CS_TakeDisk GetByCustomerID(int? KeyID)
        {
            return ObjEntity.CS_TakeDisk.FirstOrDefault(C => C.CustomerID == KeyID);
        }
        public CS_TakeDisk GetByID(int? KeyID)
        {
            return ObjEntity.CS_TakeDisk.FirstOrDefault(C => C.TakeID == KeyID);
        }

        public List<CS_TakeDisk> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_TakeDisk.Count();

            List<CS_TakeDisk> resultList = ObjEntity.CS_TakeDisk
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TakeID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_TakeDisk>();
            }
            return resultList;
        }



        /// <summary>
        /// 获得取件信息
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetTakeDisk> GetByWhere(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_GetTakeDisk>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }


        /// <summary>
        /// 获得取件信息
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetbyCustomerQuotedParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<View_CustomerQuoted>.GetDataByParameter(new View_CustomerQuoted(), ObjParameterList);
            SourceCount = query.Count();

            List<View_CustomerQuoted> resultList = query.OrderByDescending(C => C.CustomerID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_CustomerQuoted>();
            }
            return resultList;



        }


        public List<View_GetTakeDisk> GetbyParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_GetTakeDisk>.GetDataByParameter(new View_GetTakeDisk(), ObjParameterList);

            return query.ToList();



        }
        public List<View_GetTakeDisk> GetByCS_TakeDiskOrderCustomersIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.View_GetTakeDisk.Count();

            List<View_GetTakeDisk> resultList = ObjEntity.View_GetTakeDisk
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TakeID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_GetTakeDisk>();
            }
            return resultList;
        }

        public int Insert(CS_TakeDisk ObjectT)
        {
            if (ObjectT != null)
            {
                var model = GetTakeByCustomerID(ObjectT.CustomerID);
                if (model.Count == 0)
                {
                    ObjEntity.CS_TakeDisk.Add(ObjectT);
                    if (ObjEntity.SaveChanges() > 0)
                    {
                        return ObjectT.TakeID;
                    }
                }
            }
            return 0;
        }

        public int Update(CS_TakeDisk ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.TakeID;
            }
            return 0;
        }

        public List<CS_TakeDisk> GetTakeByCustomerID(int CustomerID)
        {
            return ObjEntity.CS_TakeDisk.Where(C => C.CustomerID == CustomerID).ToList();
        }

        public string GetGuardianName(int CustomerID, int TypeID)
        {
            string GuardianName = "";
            var DataList = ObjEntity.GetTakeDisks(CustomerID, TypeID).ToList();
            if (DataList != null && DataList.Count > 0)
            {
                if (DataList.Count >= 2)
                {
                    int index = 0;
                    foreach (var item in DataList)
                    {
                        if (index == DataList.Count - 1)
                        {
                            GuardianName += item.GuardianName.ToString();
                        }
                        else
                        {
                            GuardianName += item.GuardianName.ToString() + ",";
                            index++;
                        }
                    }
                }
                else
                {
                    GuardianName = ObjEntity.GetTakeDisks(CustomerID, TypeID).ToList().FirstOrDefault().Name;
                }
            }
            return GuardianName;
        }
    }
}
