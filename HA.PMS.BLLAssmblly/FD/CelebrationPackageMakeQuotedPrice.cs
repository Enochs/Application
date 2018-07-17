
/**
 Version :HaoAi 1.0
 File Name :Category
 Author:杨洋
 Date:2013.3.21
 Description:套系制作报单 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using System.Data.Entity.Validation;
namespace HA.PMS.BLLAssmblly.FD
{
     public class CelebrationPackageMakeQuotedPrice:ICRUDInterface<FD_CelebrationPackageMakeQuotedPrice>
    {
         PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationPackageMakeQuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                FD_CelebrationPackageMakeQuotedPrice objCategory = GetByID(ObjectT.MakeId);

                objCategory.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_CelebrationPackageMakeQuotedPrice> GetByAll()
        {
            return ObjEntity.FD_CelebrationPackageMakeQuotedPrice.Where(C => C.IsDelete == false).OrderBy(C => C.MakeId).ToList();
        }

        public FD_CelebrationPackageMakeQuotedPrice GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationPackageMakeQuotedPrice.FirstOrDefault(C => C.MakeId == KeyID);
        }

        public List<FD_CelebrationPackageMakeQuotedPrice> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CelebrationPackageMakeQuotedPrice.Count();

            List<FD_CelebrationPackageMakeQuotedPrice> resultList = ObjEntity.FD_CelebrationPackageMakeQuotedPrice
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MakeId)
                   .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationPackageMakeQuotedPrice>();
            }
            return resultList;
        }

        public int Insert(FD_CelebrationPackageMakeQuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationPackageMakeQuotedPrice.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MakeId;
                }
            }
            
            return 0;
        }
        ///// <summary>
        ///// 查询当前库房和产品的
        ///// </summary>
        ///// <param name="PageSize"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="SourceCount"></param>
        ///// <returns></returns>
        //public List<FD_ProductStorehouse> GetFD_ProductStorehouseByIndex(int PageSize, int PageIndex, out int SourceCount)
        //{
        //    SourceCount = ObjEntity.FD_ProductStorehouse.Count();

        //    List<FD_ProductStorehouse> resultList = ObjEntity.FD_ProductStorehouse
        //        //进行排序功能操作，不然系统会抛出异常
        //           .OrderByDescending(C => C.SupplierID)
        //           .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

        //    if (resultList.Count == 0)
        //    {
        //        resultList = new List<FD_ProductStorehouse>();
        //    }
        //    return resultList;
        //}
  

        public int Update(FD_CelebrationPackageMakeQuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.MakeId;
            }
            return 0;
        }
    }
}
