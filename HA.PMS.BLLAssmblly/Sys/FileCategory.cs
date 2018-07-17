using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;

namespace HA.PMS.BLLAssmblly.Sys
{
     public  class FileCategory:ICRUDInterface<Sys_FileCategory>
    {
         HA.PMS.DataAssmblly.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(Sys_FileCategory ObjectT)
        {
            if (ObjectT != null)
            {


                ObjEntity.Sys_FileCategory.FirstOrDefault(
                C => C.FileCategoryId == ObjectT.FileCategoryId).IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<Sys_FileCategory> GetByAll()
        {
            return ObjEntity.Sys_FileCategory.Where(C => C.IsDelete == false).ToList();
        }

        public Sys_FileCategory GetByID(int? KeyID)
        {
            return ObjEntity.Sys_FileCategory.FirstOrDefault(C => C.FileCategoryId == KeyID);
        }

        public List<Sys_FileCategory> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.Sys_FileCategory.Count();

            List<Sys_FileCategory> resultList = ObjEntity.Sys_FileCategory.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.FileCategoryId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_FileCategory>();
            }
            return resultList;
        }

        public int Insert(Sys_FileCategory ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_FileCategory.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {

                    return ObjectT.FileCategoryId;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int Update(Sys_FileCategory ObjectT)
        {
            if (ObjectT != null)
            {
                if (ObjEntity.SaveChanges() > 0)
                {

                    return ObjectT.FileCategoryId;
                }
            }

            return 0;
        }
    }
}
