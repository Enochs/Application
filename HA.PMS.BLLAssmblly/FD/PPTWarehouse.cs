
/**
 Version :HaoAi 1.0
 File Name :ImageType
 Author:杨洋
 Date:2013.3.17
 Description:PPT 实现ICRUDInterface<T> 接口中的方法
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
    public class PPTWarehouse : ICRUDInterface<FD_PPTWarehouse>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Delete(FD_PPTWarehouse ObjectT)
        {
            if (ObjectT != null)
            {
                FD_PPTWarehouse objPPTWarehouse = GetByID(ObjectT.PPTID);

                objPPTWarehouse.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_PPTWarehouse> GetByAll()
        {
            return ObjEntity.FD_PPTWarehouse.Where(C => C.IsDelete == false).ToList();
        }

        public FD_PPTWarehouse GetByID(int? KeyID)
        {
            return ObjEntity.FD_PPTWarehouse.FirstOrDefault(C => C.PPTID == KeyID);
        }

        public List<FD_PPTWarehouse> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ImageType.Count();

            List<FD_PPTWarehouse> resultList = ObjEntity.FD_PPTWarehouse
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PPTID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_PPTWarehouse>();
            }
            return resultList;
        }

        public int Insert(FD_PPTWarehouse ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FD_PPTWarehouse.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.PPTID;
                }

            }
            return 0;
        }

        public int Update(FD_PPTWarehouse ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.PPTID;
            }
            return 0;
        }
        public List<FD_PPTWarehouseFDImageType> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_PPTWarehouseFDImageType>.GetDataByParameter(new FD_PPTWarehouseFDImageType(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_PPTWarehouseFDImageType> resultList = query.OrderByDescending(C => C.PPTID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_PPTWarehouseFDImageType>();
            }
            return resultList;



        }
        /// <summary>
        /// 分页获取图片信息(含 FD_PPTWarehouse  ， FD_ImageType 两个表的视图)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_PPTWarehouseFDImageType> GetFD_PPTWarehouseFD_ImageTypeByIndex(int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.FD_PPTWarehouseFDImageType.Count();

            List<FD_PPTWarehouseFDImageType> resultList = ObjEntity.FD_PPTWarehouseFDImageType.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PPTID)

                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_PPTWarehouseFDImageType>();
            }
            return resultList;
        }
    }
}
