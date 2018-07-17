using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;

namespace HA.PMS.BLLAssmblly.FD
{
    /// <summary>
    /// 利润率
    /// </summary>
    public class MoneyRateSpan : ICRUDInterface<FL_MoneyRateSpan>      
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_MoneyRateSpan ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_MoneyRateSpan> GetByAll()
        {
           return ObjEntity.FL_MoneyRateSpan.ToList();
        }

        public FL_MoneyRateSpan GetByID(int? KeyID)
        {
            return ObjEntity.FL_MoneyRateSpan.FirstOrDefault(C => C.RateId == KeyID);
        }

        public List<FL_MoneyRateSpan> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_MoneyRateSpan.Count();

            List<FL_MoneyRateSpan> resultList = ObjEntity.FL_MoneyRateSpan
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.RateId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MoneyRateSpan>();
            }
            return resultList;
        }

        public int Insert(FL_MoneyRateSpan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MoneyRateSpan.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.RateId;
                }

            }
            return 0;
        }

        public int Update(FL_MoneyRateSpan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.RateId;
            }
            return 0;
        }
    }
}
