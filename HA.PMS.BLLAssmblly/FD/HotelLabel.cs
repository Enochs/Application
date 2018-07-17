using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.FD
{
    public class HotelLabel : ICRUDInterface<FD_HotelLabel>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_HotelLabel ObjectT)
        {
            if (ObjectT != null)
            {
                FD_HotelLabel objHotelLabel = GetByID(ObjectT.HotelLabelID);

                ObjEntity.FD_HotelLabel.Remove(objHotelLabel);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FD_HotelLabel> GetByAll()
        {
            return ObjEntity.FD_HotelLabel.ToList();
        }

        public FD_HotelLabel GetByID(int? KeyID)
        {
            return ObjEntity.FD_HotelLabel.FirstOrDefault(C => C.HotelLabelID == KeyID);
        }

        public List<FD_HotelLabel> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_HotelLabel.Count();

            List<FD_HotelLabel> resultList = ObjEntity.FD_HotelLabel
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.HotelLabelID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_HotelLabel>();
            }
            return resultList;
        }

        public int Insert(FD_HotelLabel ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_HotelLabel.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.HotelLabelID;
                }

            }
            return 0;
        }

        public int Update(FD_HotelLabel ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.HotelLabelID;
            }
            return 0;
        }


        public List<FD_HotelLabel> GetDataByParameter(List<PMSParameters> pars, string OrderByName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FD_HotelLabel>.GetDataByWhereParameter(pars, OrderByName, PageSize, PageIndex, out SourceCount);
        }

    }
}
