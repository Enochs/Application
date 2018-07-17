using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.FD
{
     public class GuardianImage:ICRUDInterface<FD_GuardianImage>
    {
         PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_GuardianImage ObjectT)
        {
            if (ObjectT != null)
            {
                FD_GuardianImage objCategory = GetByID(ObjectT.GuardianImageID);

                objCategory.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_GuardianImage> GetByAll()
        {
            return ObjEntity.FD_GuardianImage.Where(C => C.IsDelete == false).ToList();
        }

        public FD_GuardianImage GetByID(int? KeyID)
        {
            return ObjEntity.FD_GuardianImage.FirstOrDefault(C => C.GuardianImageID == KeyID);
        }

        public List<FD_GuardianImage> GetByIndex(int GuardianId, int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuardianImage.Where(C => C.FourGuardianId == GuardianId && C.IsDelete == false).Count();

            List<FD_GuardianImage> resultList = ObjEntity.FD_GuardianImage.Where(C => C.FourGuardianId == GuardianId && C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GuardianImageID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuardianImage>();
            }
            return resultList;
        }

        public int Insert(FD_GuardianImage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_GuardianImage.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.GuardianImageID;
                }

            }
            return 0;
        }

        public int Update(FD_GuardianImage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.GuardianImageID;
            }
            return 0;
        }


        public List<FD_GuardianImage> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
    }
}
