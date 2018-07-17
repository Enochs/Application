
/**
 Version :HaoAi 1.0
 File Name :CelebrationPackage
 Author:杨洋
 Date:2013.3.19
 Description:套系图片 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.FD
{
   
    public class CelebrationPackageImage:ICRUDInterface<FD_CelebrationPackageImage>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationPackageImage ObjectT)
        {
            if (ObjectT != null)
            {
                FD_CelebrationPackageImage objHotel = GetByID(ObjectT.ImageId);

                objHotel.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_CelebrationPackageImage> GetByAll()
        {
            return ObjEntity.FD_CelebrationPackageImage.Where(C => C.IsDelete == false).ToList();
        }

        public FD_CelebrationPackageImage GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationPackageImage.FirstOrDefault(C => C.ImageId == KeyID);
        }

        public List<FD_CelebrationPackageImage> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CelebrationPackageImage.Count();

            List<FD_CelebrationPackageImage> resultList = ObjEntity.FD_CelebrationPackageImage
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ImageId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationPackageImage>();
            }
            return resultList;
        }

        public int Insert(FD_CelebrationPackageImage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationPackageImage.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ImageId;
                }

            }
            return 0;
        }

        public int Update(FD_CelebrationPackageImage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ImageId;
            }
            return 0;
        }

        public List<FD_CelebrationPackageImage> GetByPackageID(int PackageID)
        {
            return ObjEntity.FD_CelebrationPackageImage.Where(C => C.PackageId == PackageID && C.IsDelete == false).ToList();
        }
    }
}
