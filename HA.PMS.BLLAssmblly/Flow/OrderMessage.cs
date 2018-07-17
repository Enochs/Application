
/**
 Version :HaoAi 1.0
 File Name :OrderMessage 订单消息；上级辅导意见(可以看做日志)
 Author:杨洋
 Date:2013.3.23
 Description:订单消息 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using System;


namespace HA.PMS.BLLAssmblly.Flow
{
    
    public class OrderMessage:ICRUDInterface<FL_OrderMessage>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderMessage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_OrderMessage.Remove(GetByID(ObjectT.WorkMessAgeID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// 根据订单号获取
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_OrderMessage> GetByOrderID(int? OrderID)
        {
            return ObjEntity.FL_OrderMessage.Where(C => C.OrderID == OrderID).ToList();
        }


        public List<FL_OrderMessage> GetByAll()
        {
            return ObjEntity.FL_OrderMessage.ToList();
        }

        public FL_OrderMessage GetByID(int? KeyID)
        {
            return ObjEntity.FL_OrderMessage.FirstOrDefault(C => C.WorkMessAgeID == KeyID);
        }

        public List<FL_OrderMessage> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_OrderMessage.Count();

            List<FL_OrderMessage> resultList = ObjEntity.FL_OrderMessage
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.WorkMessAgeID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_OrderMessage>();
            }
            return resultList;
        }


        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderMessage ObjectT)
        {
            ObjectT.CreateDate = DateTime.Now;
            ObjectT.IsDelete = false;
            if (ObjectT != null)
            {
                ObjEntity.FL_OrderMessage.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.WorkMessAgeID;
                }

            }
            return 0;
        }

        public int Update(FL_OrderMessage ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.WorkMessAgeID;
            }
            return 0;
        }
    }
}
