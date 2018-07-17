
/**
 Version :HaoAi 1.0
 File Name :CelebrationPackagePriceSpan 四大金刚等级
 Author:杨洋
 Date:2013.4.8
 Description:套系价格段 实现ICRUDInterface<T> 接口中的方法
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
    public class CelebrationPackagePriceSpan:ICRUDInterface<FD_CelebrationPackagePriceSpan>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationPackagePriceSpan ObjectT)
        {
            if (ObjectT != null)
            {
                FD_CelebrationPackagePriceSpan objPriceSpan = GetByID(ObjectT.SpanID);

                objPriceSpan.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        public List<FD_CelebrationPackagePriceSpan> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_CelebrationPackagePriceSpan>.GetDataByParameter(new FD_CelebrationPackagePriceSpan(), ObjParameterList);
            SourceCount = query.Count();

            List<FD_CelebrationPackagePriceSpan> resultList = query.OrderByDescending(C => C.SpanID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_CelebrationPackagePriceSpan>();
            }
            return resultList;

        }

        public List<FD_CelebrationPackagePriceSpan> GetByAll()
        {
            return ObjEntity.FD_CelebrationPackagePriceSpan.Where(C => C.IsDelete == false).ToList();
        }

        public FD_CelebrationPackagePriceSpan GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationPackagePriceSpan.FirstOrDefault(C => C.SpanID == KeyID);
        }

        public List<FD_CelebrationPackagePriceSpan> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_CelebrationPackagePriceSpan.Where(C=>C.IsDelete==false).Count();

            List<FD_CelebrationPackagePriceSpan> resultList = ObjEntity.FD_CelebrationPackagePriceSpan.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SpanID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationPackagePriceSpan>();
            }
            return resultList;
        }

        public int Insert(FD_CelebrationPackagePriceSpan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationPackagePriceSpan.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.SpanID;
                }

            }
            return 0;
        }

        public int Update(FD_CelebrationPackagePriceSpan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SpanID;
            }
            return 0;
        }
    }
}
