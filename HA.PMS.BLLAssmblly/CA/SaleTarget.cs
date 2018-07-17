using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.CA
{
    public class SaleTarget:ICRUDInterface<CA_SaleTarget>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_SaleTarget ObjectT) 
        {
            throw new NotImplementedException();
        }

        public List<CA_SaleTarget> GetByAll()
        {
            return ObjEntity.CA_SaleTarget.ToList();
        }

        public CA_SaleTarget GetByID(int? KeyID)
        {
            return ObjEntity.CA_SaleTarget.FirstOrDefault(C => C.TargetKey == KeyID);
        }

        public List<CA_SaleTarget> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_SaleTarget.Count();

            List<CA_SaleTarget> resultList = ObjEntity.CA_SaleTarget
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TargetKey)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_SaleTarget>();
            }
            return resultList;
        }

        public int Insert(CA_SaleTarget ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_SaleTarget.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.TargetKey;
                }

            }
            return 0;
        }

        public int Update(CA_SaleTarget ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.TargetKey;
            }
            return 0;
        }
    }
}
