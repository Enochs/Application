


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.CA
{   ///目标类型
    public class TargetType:ICRUDInterface<CA_TargetType>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_TargetType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_TargetType.Remove(GetByID(ObjectT.TargetTypeId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// 根据目标名称返回对应实体信息
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public CA_TargetType GetTargetTypeByTargetName(string targetName) 
        {
          CA_TargetType target=   ObjEntity.CA_TargetType.Where(C => C.Goal == targetName).FirstOrDefault();
          if (target!=null)
          {
              return target;
          }
          return new CA_TargetType();
        }
        public List<CA_TargetType> GetByAll()
        {
            return ObjEntity.CA_TargetType.ToList();
        }
         public List<CA_TargetType> GetByDepartmentId(int departmentID) 
        {
            var query = ObjEntity.CA_TargetType.Where(C => C.DepartmentId.Value == departmentID);
            if (query!=null)
            {
                return query.ToList();

            }
            return new List<CA_TargetType>();
        }

         public List<CA_TargetType> GetByEmployeeId(int EmployeeId)
         {
             var query = ObjEntity.CA_TargetType.Where(C => C.CreateEmployeeId.Value == EmployeeId);
             if (query != null)
             {
                 return query.ToList();

             }
             return new List<CA_TargetType>();
         }

        public CA_TargetType GetByID(int? KeyID)
        {
            return ObjEntity.CA_TargetType.FirstOrDefault(C => C.TargetTypeId == KeyID);
        }

        public List<CA_TargetType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_TargetType.Count();

            List<CA_TargetType> resultList = ObjEntity.CA_TargetType
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TargetTypeId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_TargetType>();
            }
            return resultList;
        }

        public int Insert(CA_TargetType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_TargetType.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.TargetTypeId;
                }

            }
            return 0;
        }

        public int Update(CA_TargetType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.TargetTypeId;
            }
            return 0;
        }
    }
}
