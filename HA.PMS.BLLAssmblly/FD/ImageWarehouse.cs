
/**
 Version :HaoAi 1.0
 File Name :Category
 Author:杨洋
 Date:2013.3.17
 Description:产品类型 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;
namespace HA.PMS.BLLAssmblly.FD
{
    public class ImageWarehouse:ICRUDInterface<FD_ImageWarehouse>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_ImageWarehouse ObjectT)
        {
            if (ObjectT != null)
            {
                FD_ImageWarehouse objImageWarehouse = GetByID(ObjectT.ImageID);

                objImageWarehouse.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_ImageWarehouse> GetByAll()
        {
            return ObjEntity.FD_ImageWarehouse.Where(C => C.IsDelete == false).ToList();
        }

        public FD_ImageWarehouse GetByID(int? KeyID)
        {
            return ObjEntity.FD_ImageWarehouse.FirstOrDefault(C => C.ImageID == KeyID);
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_ImageWarehouse> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ImageWarehouse.Count();

            List<FD_ImageWarehouse> resultList = ObjEntity.FD_ImageWarehouse
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ImageID)
                   .Skip(PageSize *( PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_ImageWarehouse>();
            }
            return resultList;
        }

 
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_ImageWarehouse ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_ImageWarehouse.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ImageID;
                }

            }
            return 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_ImageWarehouse ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ImageID;
            }
            return 0;
        }
    }
}
