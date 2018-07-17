using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class WarningMessage:ICRUDInterface<FL_WarningMessage>
    {

        //Entity
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 是否有警告信息
        /// </summary>
        /// <returns></returns>
        public bool HaveWaring(int? EmpLoyeeID)
        {

            return (ObjEntity.FL_WarningMessage.Count(C => C.EmpLoyeeID == EmpLoyeeID) > 0);
        
        }

        /// <summary>
        /// 获取员工的警告信息
        /// </summary>
        /// <returns></returns>
        public List<FL_WarningMessage> GetByEmpLoyee(int? EmployeeID)
        {

            return ObjEntity.FL_WarningMessage.Where(C=>C.EmpLoyeeID==EmployeeID).ToList();
        }


        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public FL_WarningMessage GetOnlyEmployeeID(int? EmployeeID)
        {
            return ObjEntity.FL_WarningMessage.FirstOrDefault(C=>C.EmpLoyeeID==EmployeeID);
        }

        public int Delete(FL_WarningMessage ObjectT)
        {
            return 0;
        }

        public List<FL_WarningMessage> GetByAll()
        {
            return null;
        }


        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_WarningMessage GetByID(int? KeyID)
        {
            return ObjEntity.FL_WarningMessage.FirstOrDefault(C => C.WMID == KeyID);
        }

        public List<FL_WarningMessage> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_WarningMessage.Count();

            List<FL_WarningMessage> resultList = ObjEntity.FL_WarningMessage
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.WMID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_WarningMessage>();
            }
            return resultList;
        }


        public List<FL_WarningMessage> GetByIndex(int PageSize, int PageIndex, out int SourceCount,int EmployeeID)
        {
            SourceCount = ObjEntity.FL_WarningMessage.Where(C => C.EmpLoyeeID == EmployeeID).Count();

            List<FL_WarningMessage> resultList = ObjEntity.FL_WarningMessage.Where(C=>C.EmpLoyeeID==EmployeeID)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.WMID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_WarningMessage>();
            }
            return resultList;
        }



        /// <summary>
        /// 添加警告信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_WarningMessage ObjectT)
        {
            ObjEntity.FL_WarningMessage.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_WarningMessage ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.WMID;
        }
    }
}
