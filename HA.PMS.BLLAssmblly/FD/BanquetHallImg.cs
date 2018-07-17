using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;

namespace HA.PMS.BLLAssmblly.FD
{
    public class BanquetHallImg : ICRUDInterface<FD_BanquetHallImg>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_BanquetHallImg ObjectT)
        {
            if (ObjectT != null)
            {
                FD_BanquetHallImg objBanquetHallImg = GetByID(ObjectT.BanquetHallImgId);

                ObjEntity.FD_BanquetHallImg.Remove(objBanquetHallImg);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FD_BanquetHallImg> GetByAll()
        {
            return ObjEntity.FD_BanquetHallImg.ToList();
        }

        public FD_BanquetHallImg GetByID(int? KeyID)
        {
            return ObjEntity.FD_BanquetHallImg.FirstOrDefault(C => C.BanquetHallImgId == KeyID);
        }
        public List<FD_BanquetHallImg> GetByBanquetHallIDAll(int BanquetHallID)
        {
            return ObjEntity.FD_BanquetHallImg.Where(C => C.BanquetHallID == BanquetHallID).ToList();
        }
        public List<FD_BanquetHallImg> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_BanquetHallImg.Count();

            List<FD_BanquetHallImg> resultList = ObjEntity.FD_BanquetHallImg
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.BanquetHallImgId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_BanquetHallImg>();
            }
            return resultList;
        }

        public List<FD_BanquetHallImg> GetByIndex(int BanquetHallID, int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_BanquetHallImg.Where(C => C.BanquetHallID == BanquetHallID).Count();

            List<FD_BanquetHallImg> resultList = ObjEntity.FD_BanquetHallImg.Where(C => C.BanquetHallID == BanquetHallID)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.BanquetHallImgId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_BanquetHallImg>();
            }
            return resultList;
        }

        public int Insert(FD_BanquetHallImg ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_BanquetHallImg.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.BanquetHallImgId;
                }

            }
            return 0;
        }

        public int Update(FD_BanquetHallImg ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.BanquetHallImgId;
            }
            return 0;
        }
    }
}
