        
/**
 Version :HaoAi 1.0
 File Name :Category
 Author:杨洋
 Date:2013.3.15
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
    public class DeliverySchedule : ICRUDInterface<FD_DeliverySchedule> 
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_DeliverySchedule ObjectT)
        {
            if (ObjectT != null)
            {
                FD_DeliverySchedule objFD_Storehouse = GetByID(ObjectT.ScheduleId);

                objFD_Storehouse.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_DeliverySchedule> GetByAll()
        {
            return ObjEntity.FD_DeliverySchedule.Where(C => C.IsDelete == false).ToList();
        }

        public FD_DeliverySchedule GetByID(int? KeyID)
        {
            return ObjEntity.FD_DeliverySchedule.FirstOrDefault(C => C.ScheduleId == KeyID);
        }

        public List<FD_DeliverySchedule> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_DeliverySchedule.Count();

            List<FD_DeliverySchedule> resultList = ObjEntity.FD_DeliverySchedule
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ScheduleId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_DeliverySchedule>();
            }
            return resultList;
        }

        public int Insert(FD_DeliverySchedule ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_DeliverySchedule.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ScheduleId;
                }

            }
            return 0;
        }
        public List<FD_DeliverySchedule> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_DeliverySchedule>.GetDataByParameter(new FD_DeliverySchedule(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_DeliverySchedule> resultList = query
                .Where(C=>C.IsDelete==false)
                .OrderByDescending(C => C.ScheduleId)
                .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_DeliverySchedule>();
            }
            return resultList;



        }

        public int Update(FD_DeliverySchedule ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ScheduleId;
            }
            return 0;
        }
    }
}
