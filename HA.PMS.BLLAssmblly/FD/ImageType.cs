
/**
 Version :HaoAi 1.0
 File Name :ImageType
 Author:杨洋
 Date:2013.3.17
 Description:图片类型 实现ICRUDInterface<T> 接口中的方法
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

    public class ImageType : ICRUDInterface<FD_ImageType>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Delete(FD_ImageType ObjectT)
        {
            if (ObjectT != null)
            {
                FD_ImageType objFD_ImageType = GetByID(ObjectT.TypeId);

                objFD_ImageType.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_ImageType> GetByAll()
        {
            return ObjEntity.FD_ImageType.Where(C => C.IsDelete == false).ToList();
        }

        public FD_ImageType GetByID(int? KeyID)
        {
            return ObjEntity.FD_ImageType.FirstOrDefault(C => C.TypeId == KeyID);
        }

        public List<FD_ImageType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ImageType.Count();

            List<FD_ImageType> resultList = ObjEntity.FD_ImageType
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TypeId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_ImageType>();
            }
            return resultList;
        }

        public int Insert(FD_ImageType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_ImageType.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.TypeId;
                }

            }
            return 0;
        }

        public int Update(FD_ImageType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.TypeId;
            }
            return 0;
        }
    }
}
