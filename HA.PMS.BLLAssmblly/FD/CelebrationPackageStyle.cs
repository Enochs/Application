

/**
 Version :HaoAi 1.0
 File Name :CelebrationPackageStyle 
 Author:杨洋
 Date:2013.4.8
 Description:套系风格表 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;


namespace HA.PMS.BLLAssmblly.FD
{
   public  class CelebrationPackageStyle:ICRUDInterface<FD_CelebrationPackageStyle>
    {
       PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationPackageStyle ObjectT)
        {
            if (ObjectT != null)
            {
                FD_CelebrationPackageStyle objFD_CelebrationPackageStyle = GetByID(ObjectT.StyleId);

                objFD_CelebrationPackageStyle.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_CelebrationPackageStyle> GetByAll()
        {
            return ObjEntity.FD_CelebrationPackageStyle.Where(C => C.IsDelete == false).ToList();
        }

        public FD_CelebrationPackageStyle GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationPackageStyle.FirstOrDefault(C => C.StyleId == KeyID);
        }
        public List<FD_CelebrationPackageStyle> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_CelebrationPackageStyle>.GetDataByParameter(new FD_CelebrationPackageStyle(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_CelebrationPackageStyle> resultList = query.OrderByDescending(C => C.StyleId)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_CelebrationPackageStyle>();
            }
            return resultList;

        }

        public List<FD_CelebrationPackageStyle> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CelebrationPackageStyle.Where(C=>C.IsDelete==false).Count();

            List<FD_CelebrationPackageStyle> resultList = ObjEntity.FD_CelebrationPackageStyle.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.StyleId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationPackageStyle>();
            }
            return resultList;
        }

        public int Insert(FD_CelebrationPackageStyle ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationPackageStyle.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.StyleId;
                }

            }
            return 0;
        }

        public int Update(FD_CelebrationPackageStyle ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.StyleId;
            }
            return 0;
        }
    }
}
