

/**
 Version :HaoAi 1.0
 File Name :yy 
 Author:杨洋
 Date:2013.4.7
 Description:库房及供应商虚拟总产品表 实现ICRUDInterface<T> 接口中的方法
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
    public class StorehouseSupplierProductAll : ICRUDInterface<FD_StorehouseSupplierProductAll>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        public int Delete(FD_StorehouseSupplierProductAll ObjectT)
        {
            if (ObjectT != null)
            {
                FD_StorehouseSupplierProductAll objStorehouseSupplierProductAll = GetByID(ObjectT.AllId);

                objStorehouseSupplierProductAll.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_StorehouseSupplierProductAll> GetByAll()
        {
            return ObjEntity.FD_StorehouseSupplierProductAll.Where(C => C.IsDelete == false).ToList();
        }

        public FD_StorehouseSupplierProductAll GetByID(int? KeyID)
        {
            return ObjEntity.FD_StorehouseSupplierProductAll.FirstOrDefault(C => C.AllId == KeyID);
        }
        /// <summary>
        /// 参数化查询方法
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_StorehouseSupplierProductAll> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_StorehouseSupplierProductAll>.GetDataByParameter(new FD_StorehouseSupplierProductAll(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_StorehouseSupplierProductAll> resultList = query.Where(C=>C.IsDelete==false).OrderByDescending(C => C.AllId)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_StorehouseSupplierProductAll>();
            }
            return resultList;



        }
        public List<FD_StorehouseSupplierProductAll> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_StorehouseSupplierProductAll.Count();

            List<FD_StorehouseSupplierProductAll> resultList = ObjEntity.FD_StorehouseSupplierProductAll
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.AllId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_StorehouseSupplierProductAll>();
            }
            return resultList;
        }

        public int Insert(FD_StorehouseSupplierProductAll ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_StorehouseSupplierProductAll.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.AllId;
                }

            }
            return 0;
        }

        public int Update(FD_StorehouseSupplierProductAll ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.AllId;
            }
            return 0;
        }
    }
}
