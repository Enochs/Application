/**
 Version :HaoAi 1.0
 File Name :SupplierErrorLog
 Author:杨洋
 Date:2013.4.6
 Description:差错记录表 实现ICRUDInterface<T> 接口中的方法
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
    public class SupplierErrorLog:ICRUDInterface<FD_SupplierErrorLog>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_SupplierErrorLog ObjectT)
        {
            if (ObjectT != null)
            {
                FD_SupplierErrorLog objProductOrderInuse = GetByID(ObjectT.ErrorLogId);

                objProductOrderInuse.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_SupplierErrorLog> GetByAll()
        {
            return ObjEntity.FD_SupplierErrorLog.Where(C => C.IsDelete == false).ToList(); 
        }

        public FD_SupplierErrorLog GetByID(int? KeyID)
        {
            return ObjEntity.FD_SupplierErrorLog.FirstOrDefault(C => C.ErrorLogId == KeyID);
        }

        public List<FD_SupplierErrorLog> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.FD_SupplierErrorLog.Count();

            List<FD_SupplierErrorLog> resultList = ObjEntity.FD_SupplierErrorLog
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ErrorLogId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_SupplierErrorLog>();
            }
            return resultList;
        }

        public int Insert(FD_SupplierErrorLog ObjectT)
        {
             if (ObjectT != null)
            {
                ObjEntity.FD_SupplierErrorLog.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ErrorLogId;
                }

            }
            return 0;
        }
        

        public int Update(FD_SupplierErrorLog ObjectT)
        {
             if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ErrorLogId;
            }
            return 0;
        }
    }
}
