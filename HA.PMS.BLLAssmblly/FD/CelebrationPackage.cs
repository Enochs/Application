
/**
 Version :HaoAi 1.0
 File Name :CelebrationPackage
 Author:杨洋
 Date:2013.3.19
 Description:套系管理 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.FD
{
    public class CelebrationPackage : ICRUDInterface<FD_CelebrationPackage>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationPackage ObjectT)
        {

            if (ObjectT != null)
            {
                FD_CelebrationPackage objCelebrationPackage = GetByID(ObjectT.PackageID);

                objCelebrationPackage.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_CelebrationPackage> GetPackageDataByParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_CelebrationPackage>.GetDataByParameter(new FD_CelebrationPackage(), ObjParameterList);
            //SourceCount = query.Where(C => C.IsDelete == false).Count();
            SourceCount = query.Count();
            //List<FD_CelebrationPackage> resultList = query.Where(C => C.IsDelete == false).OrderByDescending(C => C.PackageID)
            List<FD_CelebrationPackage> resultList = query.OrderByDescending(C => C.PackageID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_CelebrationPackage>();
            }
            return resultList;
        }

        public List<FD_CelebrationPackage> GetPackageDataByParameter()
        {
            var query = ObjEntity.FD_CelebrationPackage.ToList();

            return query;
        }


        public List<FD_CelebrationPackage> GetByAll()
        {
            return ObjEntity.FD_CelebrationPackage.Where(C => C.IsDelete == false).ToList();
            // return ObjEntity.FD_CelebrationPackage.ToList();
        }

        public FD_CelebrationPackage GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationPackage.FirstOrDefault(C => C.PackageID == KeyID);
        }

        public List<FD_CelebrationPackage> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            // SourceCount = ObjEntity.FD_CelebrationPackage.Where(C=>C.IsDelete==false).Count();
            SourceCount = ObjEntity.FD_CelebrationPackage.Count();
            List<FD_CelebrationPackage> resultList = ObjEntity.FD_CelebrationPackage
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PackageID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationPackage>();
            }
            return resultList;
        }

        public int Insert(FD_CelebrationPackage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationPackage.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.PackageID;
                }

            }
            return 0;
        }

        public int Update(FD_CelebrationPackage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.PackageID;
            }
            return 0;
        }
        /// <summary>
        /// 返回最大的ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxPackageID()
        {
            int maxPackageId = 0;
            //查询最新的套系ID  
            var query = ObjEntity.FD_CelebrationPackage
                       .OrderByDescending(C => C.PackageID).FirstOrDefault();
            //如果不等于空,返回该ID，在此基础上面加 1
            if (query != null)
            {

                maxPackageId = query.PackageID + 1;
            }
            else
            {
                //默认返回1
                maxPackageId = 1;
            }
            return maxPackageId;

        }
    }
}
