
/**
 Version :HaoAi 1.0
 File Name :GuradianLeven 四大金刚等级
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚等级 实现ICRUDInterface<T> 接口中的方法
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
    public class GuradianLeven:ICRUDInterface<FD_GuradianLeven>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_GuradianLeven ObjectT)
        {
            if (ObjectT != null)
            {
                FD_GuradianLeven objGuradianLeven = GetByID(ObjectT.LevenId);

                objGuradianLeven.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_GuradianLeven> GetByAll()
        {
            return ObjEntity.FD_GuradianLeven.Where(C => C.IsDelete == false).ToList();
        }

        public FD_GuradianLeven GetByID(int? KeyID)
        {
            return ObjEntity.FD_GuradianLeven.FirstOrDefault(C => C.LevenId == KeyID);
        }

        public List<FD_GuradianLeven> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuradianLeven.Count();

            List<FD_GuradianLeven> resultList = ObjEntity.FD_GuradianLeven
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.LevenId)
                   .Where(C=>C.IsDelete==false)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuradianLeven>();
            }
            return resultList;
        }

        public int Insert(FD_GuradianLeven ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FD_GuradianLeven.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.LevenId;
                }

            }
            return 0;
        }

        public int Update(FD_GuradianLeven ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.LevenId;
            }
            return 0;
        }
    }
}
