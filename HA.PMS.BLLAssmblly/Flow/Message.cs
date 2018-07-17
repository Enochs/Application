
/**
 Version :HaoAi 1.0
 File Name :Message 消息正文表
 Author:杨洋
 Date:2013.3.23
 Description:客户邀请 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class Message : ICRUDInterface<FL_Message>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 根据客户删除消息
        /// </summary>
        /// <param name="CustomerID"></param>
        public void DeleteByCustomerID(int CustomerID)
        {
            ///AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=364&OrderID=163&FlowOrder=1
            var KeyWOrd = "CustomerID=" + CustomerID;
            var ObjDeleteList = ObjEntity.FL_Message.Where(C => C.KeyWords.Contains(KeyWOrd)).ToList();
            foreach (var ObjItem in ObjDeleteList)
            {
                ObjEntity.FL_Message.Remove(ObjItem);
                ObjEntity.SaveChanges();
            }

            var objWarList = ObjEntity.FL_WarningMessage.Where(C => C.ResualtAddress.Contains(KeyWOrd)).ToList();

            foreach (var ObjItem in objWarList)
            {
                ObjEntity.FL_WarningMessage.Remove(ObjItem);
                ObjEntity.SaveChanges();
            }
        }

        /// <summary>
        /// 根据未查看的消息查看
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public int GetMessAgeSumbyLook(int? EmployeeID)
        {
            return ObjEntity.FL_Message.Count(C => C.IsLook == false && C.EmployeeID == EmployeeID);
        }

         


        public List<FL_Message> GetByEmployeeID(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID)
        {
            var query = ObjEntity.FL_Message.Where(C => C.EmployeeID == EmployeeID);
            SourceCount = query.Count();

            List<FL_Message> resultList = query
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MessageID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_Message>();
            }
            return resultList;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Message ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Message.Remove(GetByID(ObjectT.MessageID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_Message> GetByAll()
        {
            return ObjEntity.FL_Message.ToList();
        }

        public FL_Message GetByID(int? KeyID)
        {
            return ObjEntity.FL_Message.FirstOrDefault(C => C.MessageID == KeyID);
        }

        public List<FL_Message> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_Message.Count();

            List<FL_Message> resultList = ObjEntity.FL_Message
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MessageID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_Message>();
            }
            return resultList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Message ObjectT)
        {
            ObjectT.CreateDate = System.DateTime.Now;
            ObjectT.IsLook = false;
            ObjectT.IsDelete = false;

            if (ObjectT != null)
            {
                ObjEntity.FL_Message.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MessageID;
                }

            }
            return 0;
        }

        public int Update(FL_Message ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.MessageID;
            }
            return 0;
        }
    }
}
