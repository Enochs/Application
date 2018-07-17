using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;


namespace HA.PMS.BLLAssmblly.FD
{
    
    public class HotelImg:ICRUDInterface<FD_HotelImg>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        public int Delete(FD_HotelImg ObjectT)
        {
            if (ObjectT != null)
            {
                FD_HotelImg objHotelImg = GetByID(ObjectT.HotelImgId);

                ObjEntity.FD_HotelImg.Remove(objHotelImg);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FD_HotelImg> GetByAll()
        {
            return ObjEntity.FD_HotelImg.ToList();
        }

        public FD_HotelImg GetByID(int? KeyID)
        {
            return ObjEntity.FD_HotelImg.FirstOrDefault(C => C.HotelImgId == KeyID);
        }
        public List<FD_HotelImg> GetByHotelIDAll(int HotelId)
        {
            return ObjEntity.FD_HotelImg.Where(C => C.HotelId == HotelId).ToList();
        }
        public List<FD_HotelImg> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_HotelImg.Count();

            List<FD_HotelImg> resultList = ObjEntity.FD_HotelImg
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.HotelImgId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_HotelImg>();
            }
            return resultList;
        }
        public List<FD_HotelImg> GetByIndex(int HotelId, int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = GetByHotelIDAll(HotelId).Count();

            List<FD_HotelImg> resultList = GetByHotelIDAll(HotelId)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.HotelImgId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_HotelImg>();
            }
            return resultList;
        }


        public int Insert(FD_HotelImg ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_HotelImg.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.HotelImgId;
                }

            }
            return 0;
        }

        public int Update(FD_HotelImg ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.HotelImgId;
            }
            return 0;
        }
    }
}
