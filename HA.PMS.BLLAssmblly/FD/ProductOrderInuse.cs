/**
 Version :HaoAi 1.0
 File Name :ProductOrderInuse
 Author:杨洋
 Date:2013.4.6
 Description:执行中订单产品 实现ICRUDInterface<T> 接口中的方法
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
    public class ProductOrderInuse:ICRUDInterface<FD_ProductOrderInuse>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_ProductOrderInuse ObjectT)
        {
            if (ObjectT != null)
            {
                FD_ProductOrderInuse objProductOrderInuse = GetByID(ObjectT.InuseId);

                objProductOrderInuse.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 参数化方法
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_ProductOrderInuse> GetbyFD_ProductOrderInuseParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_ProductOrderInuse>.GetDataByParameter(new FD_ProductOrderInuse(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_ProductOrderInuse> resultList = query.Where(C=>C.IsDelete==false).OrderByDescending(C => C.InuseId)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_ProductOrderInuse>();
            }
            return resultList;

        }
        public List<FD_ProductOrderInuse> GetByAll()
        {
            return ObjEntity.FD_ProductOrderInuse.Where(C => C.IsDelete == false).ToList(); 
        }

        public FD_ProductOrderInuse GetByID(int? KeyID)
        {
            return ObjEntity.FD_ProductOrderInuse.FirstOrDefault(C => C.InuseId == KeyID);
        }

        public List<FD_ProductOrderInuse> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ProductOrderInuse.Count();

            List<FD_ProductOrderInuse> resultList = ObjEntity.FD_ProductOrderInuse
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.InuseId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_ProductOrderInuse>();
            }
            return resultList;
        }

        public int Insert(FD_ProductOrderInuse ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_ProductOrderInuse.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.InuseId;
                }

            }
            return 0;
        }

        public int Update(FD_ProductOrderInuse ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.InuseId;
            }
            return 0;
        }
    }
}
