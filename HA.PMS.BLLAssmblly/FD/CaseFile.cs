using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;

namespace HA.PMS.BLLAssmblly.FD
{
    /// <summary>
    /// 案例文件
    /// </summary>
    public class CaseFile : ICRUDInterface<FD_CaseFile>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CaseFile ObjectT)
        {
            if (ObjectT != null)
            {
                FD_CaseFile objCaseFile = GetByID(ObjectT.CaseFileId);

                ObjEntity.FD_CaseFile.Remove(objCaseFile);
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_CaseFile> GetByAll()
        {
            return ObjEntity.FD_CaseFile.ToList();
        }


        public List<FD_CaseFile> GetByCaseID(int CaseID)
        {
            return ObjEntity.FD_CaseFile.Where(C => C.CaseId == CaseID && C.FileType==1).ToList();
        }

        public List<FD_CaseFile> GetByCasesID(int CaseID)
        {
            return ObjEntity.FD_CaseFile.Where(C => C.CaseId == CaseID && C.FileType == 2).ToList();
        }

        public FD_CaseFile GetByID(int? KeyID)
        {
            return ObjEntity.FD_CaseFile.FirstOrDefault(C => C.CaseFileId == KeyID);
        }

        public List<FD_CaseFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CaseFile.Count();

            List<FD_CaseFile> resultList = ObjEntity.FD_CaseFile
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CaseFileId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CaseFile>();
            }
            return resultList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileType">文件类型标识</param>
        /// <param name="CaseId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_CaseFile> GetByIndex(int FileType, int CaseId, int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CaseFile.Where(C => C.FileType == FileType && C.CaseId == CaseId).Count();

            List<FD_CaseFile> resultList = ObjEntity.FD_CaseFile.Where(C => C.FileType == FileType && C.CaseId == CaseId)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CaseFileId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CaseFile>();
            }
            return resultList;
        }

        public int Insert(FD_CaseFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CaseFile.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.CaseFileId;
                }

            }
            return 0;
        }

        public int Update(FD_CaseFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.CaseFileId;
            }
            return 0;
        }
    }
}
