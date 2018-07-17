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
    /// <summary>
    /// 订单金额价格段设置
    /// </summary>
    public class OrderMoneySpan : ICRUDInterface<FL_OrderMoneySpan>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderMoneySpan ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderMoneySpan> GetByAll()
        {
            return ObjEntity.FL_OrderMoneySpan.ToList();
        }

        public FL_OrderMoneySpan GetByID(int? KeyID)
        {
            return ObjEntity.FL_OrderMoneySpan.FirstOrDefault(C => C.MoneySpanId == KeyID);
        }

        public List<FL_OrderMoneySpan> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_OrderMoneySpan.Count();

            List<FL_OrderMoneySpan> resultList = ObjEntity.FL_OrderMoneySpan
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MoneySpanId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_OrderMoneySpan>();
            }
            return resultList;
        }

        public int Insert(FL_OrderMoneySpan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_OrderMoneySpan.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MoneySpanId;
                }

            }
            return 0;
        }

        public int Update(FL_OrderMoneySpan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.MoneySpanId;
            }
            return 0;
        }
    }
}
