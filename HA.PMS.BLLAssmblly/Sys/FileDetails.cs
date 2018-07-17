using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;

namespace HA.PMS.BLLAssmblly.Sys
{
     public class FileDetails:ICRUDInterface<Sys_FileDetails>
    {
       HA.PMS.DataAssmblly.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(Sys_FileDetails ObjectT)
        {
            if (ObjectT != null)
            {
                 
                ObjEntity.Sys_FileDetails.Remove(GetByID(ObjectT.FileDetailsId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<Sys_FileDetails> GetByAll()
        {
            return ObjEntity.Sys_FileDetails.ToList();
        }

        public Sys_FileDetails GetByID(int? KeyID)
        {
            return ObjEntity.Sys_FileDetails.FirstOrDefault(C => C.FileDetailsId == KeyID);
        }

        public List<Sys_FileDetails> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.Sys_FileDetails.Count();

            List<Sys_FileDetails> resultList = ObjEntity.Sys_FileDetails
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.FileDetailsId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_FileDetails>();
            }
            return resultList;
        }

        public List<Sys_FileDetails> GetByIndex(int categoryId,int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.Sys_FileDetails.Count();

            List<Sys_FileDetails> resultList = ObjEntity.Sys_FileDetails.Where(C=>C.FileCategoryId==categoryId)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.FileDetailsId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_FileDetails>();
            }
            return resultList;
        }

        public int Insert(Sys_FileDetails ObjectT)
        {
             if (ObjectT != null)
            {
                ObjEntity.Sys_FileDetails.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {

                    return ObjectT.FileDetailsId;
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

        public int Update(Sys_FileDetails ObjectT)
        {
            if (ObjectT != null)
            {
                if (ObjEntity.SaveChanges() > 0)
                {

                    return ObjectT.FileDetailsId;
                }
            }

            return 0;
        }
    }
}
