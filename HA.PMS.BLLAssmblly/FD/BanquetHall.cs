using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.FD
{
    public class BanquetHall:ICRUDInterface<FD_BanquetHall>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_BanquetHall ObjectT)
        {
            if (ObjectT != null)
            {
                FD_BanquetHall objBanquetHall = GetByID(ObjectT.BanquetHallID);


                 ObjEntity.FD_BanquetHall.Remove(objBanquetHall);
                 return ObjEntity.SaveChanges();
            }
            return 0;
        }
        public List<FD_BanquetHall> GetByHotelIDAll(int HotelId)
        {
            return ObjEntity.FD_BanquetHall.Where(C => C.HotelId == HotelId).ToList();
        }

        public List<FD_BanquetHall> GetByAll()
        {
            return ObjEntity.FD_BanquetHall.ToList();
        }

        public FD_BanquetHall GetByID(int? KeyID)
        {
            return ObjEntity.FD_BanquetHall.FirstOrDefault(C => C.BanquetHallID == KeyID);
        }

        public List<FD_BanquetHall> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_BanquetHall.Count();

            List<FD_BanquetHall> resultList = ObjEntity.FD_BanquetHall
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.BanquetHallID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_BanquetHall>();
            }
            return resultList;
        }
        public List<FD_BanquetHall> GetByIndex(int HotelId, int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = GetByHotelIDAll(HotelId).Count();

            List<FD_BanquetHall> resultList = GetByHotelIDAll(HotelId)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.BanquetHallID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_BanquetHall>();
            }
            return resultList;
        }



        public int Insert(FD_BanquetHall ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_BanquetHall.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.BanquetHallID;
                }

            }
            return 0;
        }

        public int Update(FD_BanquetHall ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.BanquetHallID;
            }
            return 0;
        }
    }
}
