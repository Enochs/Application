using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;

namespace HA.PMS.BLLAssmblly.FD
{
    public class HotelLabelLog : ICRUDInterface<FD_HotelLabelLog>  
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_HotelLabelLog ObjectT)
        {
            if (ObjectT != null)
            {
                FD_HotelLabelLog objHotelLabelLog = GetByID(ObjectT.HotelLabelLogID);

                ObjEntity.FD_HotelLabelLog.Remove(objHotelLabelLog);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public int DeleteByHotelID(int HotelID)
        {
            var ObjDeleteList = ObjEntity.FD_HotelLabelLog.Where(C => C.HotelID == HotelID).ToList();
            if (ObjDeleteList != null)
            {
                foreach (var Item in ObjDeleteList)
                {
                    ObjEntity.FD_HotelLabelLog.Remove(Item);
                }
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FD_HotelLabelLog> GetByAll()
        {
            return ObjEntity.FD_HotelLabelLog.ToList();
        }
        public List<FD_HotelLabelLog> GetByHotelIDAll(int HotelID)
        {
            return ObjEntity.FD_HotelLabelLog.Where(C => C.HotelID == HotelID).ToList();
        }
        public FD_HotelLabelLog GetByID(int? KeyID)
        {
            return ObjEntity.FD_HotelLabelLog.FirstOrDefault(C => C.HotelLabelLogID == KeyID);
        }

        public List<FD_HotelLabelLog> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_HotelLabelLog.Count();

            List<FD_HotelLabelLog> resultList = ObjEntity.FD_HotelLabelLog
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.HotelLabelLogID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_HotelLabelLog>();
            }
            return resultList;
        }

        public int Insert(FD_HotelLabelLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_HotelLabelLog.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.HotelLabelLogID;
                }

            }
            return 0;
        }

        public int Update(FD_HotelLabelLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.HotelLabelLogID;
            }
            return 0;
        }
    }
}
