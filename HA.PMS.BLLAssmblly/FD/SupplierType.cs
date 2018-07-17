using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
namespace HA.PMS.BLLAssmblly.FD
{
    public class SupplierType : ICRUDInterface<FD_SupplierType>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_SupplierType ObjectT)
        {
            if (ObjectT != null)
            {
                FD_SupplierType objSupplie = GetByID(ObjectT.SupplierTypeId);

                objSupplie.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_SupplierType> GetByAll()
        {
            return ObjEntity.FD_SupplierType.Where(C => C.IsDelete == false).ToList();
        }

        public FD_SupplierType GetByID(int? KeyID)
        {
            return ObjEntity.FD_SupplierType.FirstOrDefault(C => C.SupplierTypeId == KeyID);
        }

        public List<FD_SupplierType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_SupplierType.Where(C=>C.IsDelete==false).Count();

            List<FD_SupplierType> resultList = ObjEntity.FD_SupplierType.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SupplierTypeId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_SupplierType>();
            }
            return resultList;
        }

        public int Insert(FD_SupplierType ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FD_SupplierType.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.SupplierTypeId;
                }

            }
            return 0;
        }

        public int Update(FD_SupplierType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SupplierTypeId;
            }
            return 0;
        }
    }
}
