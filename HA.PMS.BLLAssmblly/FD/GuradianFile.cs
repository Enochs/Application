using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.FD
{
    public class GuradianFile : ICRUDInterface<FD_GuradianFile>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_GuradianFile ObjectT)
        {
            if (ObjectT != null)
            {
                FD_GuradianFile objCategory = GetByID(ObjectT.GuradianFileId);

                objCategory.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_GuradianFile> GetByAll()
        {
            return ObjEntity.FD_GuradianFile.Where(C => C.IsDelete == false).ToList();
        }
        public List<FD_GuradianFile> GetByTypeAll(int typeId)
        {
            return ObjEntity.FD_GuradianFile.Where(C => C.IsDelete == false&&C.GuradianFileType==typeId).ToList();
        }
        public FD_GuradianFile GetByID(int? KeyID)
        {
            return ObjEntity.FD_GuradianFile.FirstOrDefault(C => C.GuradianFileId == KeyID);
        }

        public List<FD_GuradianFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuradianFile.Count();

            List<FD_GuradianFile> resultList = ObjEntity.FD_GuradianFile
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GuradianFileId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuradianFile>();
            }
            return resultList;
        }
        public List<FD_GuradianFile> GetByTypeIndex(int typeId,int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuradianFile.Where(C => C.IsDelete == false && C.GuradianFileType == typeId).Count();

            List<FD_GuradianFile> resultList = ObjEntity.FD_GuradianFile
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GuradianFileId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuradianFile>();
            }
            return resultList;
        }

        public int Insert(FD_GuradianFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_GuradianFile.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.GuradianFileId;
                }

            }
            return 0;
        }

        public int Update(FD_GuradianFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.GuradianFileId;
            }
            return 0;
        }
    }
}
