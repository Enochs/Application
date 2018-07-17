using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.FD
{
    public class CelebrationPackageProduct : ICRUDInterface<FD_CelebrationPackageProduct>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationPackageProduct ObjectT)
        {
            FD_CelebrationPackageProduct objChannelType = GetByID(ObjectT.PackageProductID);
            ObjEntity.FD_CelebrationPackageProduct.Remove(objChannelType);
            return ObjEntity.SaveChanges();
        }

        public List<FD_CelebrationPackageProduct> GetByAll()
        {
            return ObjEntity.FD_CelebrationPackageProduct.ToList();
        }

        /// <summary>
        /// 根据套系ID获取
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        public List<int> GetProductKeyArryByPackageID(int? PackageID)
        {
            var ObjModelList = ObjEntity.FD_CelebrationPackageProduct.Where(C => C.PackageID == PackageID).ToList();
            List<int> ObjKeyArry = new List<int>();
            foreach (var ObjItem in ObjModelList)
            {
                ObjKeyArry.Add(ObjItem.AllProductKeys.Value);
            }
            return ObjKeyArry;
        }


        public FD_CelebrationPackageProduct GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationPackageProduct.FirstOrDefault(C => C.PackageProductID == KeyID);
        }

        public List<FD_CelebrationPackageProduct> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CelebrationPackageProduct.Count();

            List<FD_CelebrationPackageProduct> resultList = ObjEntity.FD_CelebrationPackageProduct
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PackageProductID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationPackageProduct>();
            }
            return resultList;
        }

        [Obsolete]
        public List<View_PackageProduct> GetPackageProductByKeysIndex(int PackageID)
        {
            return ObjEntity.View_PackageProduct.Where(C => C.PackageID == PackageID).ToList();
        }
        public int Insert(FD_CelebrationPackageProduct ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationPackageProduct.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.PackageProductID;
                }

            }
            return 0;
        }

        public int Update(FD_CelebrationPackageProduct ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.PackageProductID;
            }
            return 0;
        }

        public List<View_PackageProduct> GetByPackageID(int PackageID)
        {
            return ObjEntity.View_PackageProduct.Where(C => C.PackageID == PackageID && C.IsDelete == false).ToList();
        }
    }
}
