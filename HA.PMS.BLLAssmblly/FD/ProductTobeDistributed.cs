/**
 Version :HaoAi 1.0
 File Name :ProductOrderInuse
 Author:杨洋
 Date:2013.4.6
 Description:待分配产品库 实现ICRUDInterface<T> 接口中的方法
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
   public  class ProductTobeDistributed:ICRUDInterface<FD_ProductTobeDistributed>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_ProductTobeDistributed ObjectT)
        {
            if (ObjectT != null)
            {
                FD_ProductTobeDistributed objProductTobeDistributed = GetByID(ObjectT.DistributedId);

                objProductTobeDistributed.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_ProductTobeDistributed> GetByAll()
        {
            return ObjEntity.FD_ProductTobeDistributed.Where(C => C.IsDelete == false).ToList(); 
        }

        public FD_ProductTobeDistributed GetByID(int? KeyID)
        {
            return ObjEntity.FD_ProductTobeDistributed.FirstOrDefault(C => C.DistributedId == KeyID);
        }

        public List<FD_ProductTobeDistributed> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ProductOrderInuse.Count();

            List<FD_ProductTobeDistributed> resultList = ObjEntity.FD_ProductTobeDistributed
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.DistributedId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_ProductTobeDistributed>();
            }
            return resultList;
        }

        public int Insert(FD_ProductTobeDistributed ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_ProductTobeDistributed.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DistributedId;
                }

            }
            return 0;
        }

        public int Update(FD_ProductTobeDistributed ObjectT)
        {
           if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.DistributedId;
            }
            return 0;
        }
    }
}
