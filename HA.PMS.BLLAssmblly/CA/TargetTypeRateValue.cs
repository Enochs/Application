using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.CA
{
    public class TargetTypeRateValue : ICRUDInterface<CA_TargetTypeRateValue>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_TargetTypeRateValue ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_TargetTypeRateValue.Remove(GetByID(ObjectT.RateId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CA_TargetTypeRateValue> GetByAll()
        {
            return ObjEntity.CA_TargetTypeRateValue.ToList();
        }

        public CA_TargetTypeRateValue GetByID(int? KeyID)
        {
            return ObjEntity.CA_TargetTypeRateValue.FirstOrDefault(C => C.RateId == KeyID);
        }

        public List<CA_TargetTypeRateValue> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_TargetTypeRateValue.Count();

            List<CA_TargetTypeRateValue> resultList = ObjEntity.CA_TargetTypeRateValue
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.RateId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_TargetTypeRateValue>();
            }
            return resultList;
        }

        public int Insert(CA_TargetTypeRateValue ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_TargetTypeRateValue.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.RateId;
                }

            }
            return 0;
        }

        public int Update(CA_TargetTypeRateValue ObjectT)
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
