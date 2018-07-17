
/**
 Version :HaoAi 1.0
 File Name :GuardianType 四大金刚 类型
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚类型 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.FD
{
    public class GuardianType:ICRUDInterface<FD_GuardianType>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_GuardianType ObjectT)
        {
            if (ObjectT != null)
            {
                FD_GuardianType objGuardianType = GetByID(ObjectT.TypeId);

                objGuardianType.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_GuardianType> GetByAll()
        {
            return ObjEntity.FD_GuardianType.Where(C => C.IsDelete == false).ToList();
        }

        public FD_GuardianType GetByID(int? KeyID)
        {
            return ObjEntity.FD_GuardianType.FirstOrDefault(C => C.TypeId == KeyID);
        }

        public List<FD_GuardianType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuardianType.Count();

            List<FD_GuardianType> resultList = ObjEntity.FD_GuardianType
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TypeId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuardianType>();
            }
            return resultList;
        }

        public int Insert(FD_GuardianType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_GuardianType.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.TypeId;
                }

            }
            return 0;
        }

        public int Update(FD_GuardianType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.TypeId;
            }
            return 0;
        }
    }
}
