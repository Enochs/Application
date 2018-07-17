

/**
 Version :HaoAi 1.0
 File Name :MessageBoard 留言类
 Author:杨洋
 Date:2013.4.15
 Description:留言类 实现ICRUDInterface<T> 接口中的方法
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class MessageBoard:ICRUDInterface<FL_MessageBoard>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_MessageBoard ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FL_MessageBoard.Remove(GetByID(ObjectT.MessageBoardID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }



        /// <summary>
        /// 获取本人的留言
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="Pareent"></param>
        /// <returns></returns>
        public List<FL_MessageBoard> GetMessageByEmpLoyeeID(int? EmpLoyeeID,int ParentID)
        {
            return ObjEntity.FL_MessageBoard.Where(C => C.EmpLoyeeID == EmpLoyeeID && C.Parent == ParentID).ToList();
        }


        /// <summary>
        /// 获取本人的留言或者被留言
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<FL_MessageBoard> GetMineMessage(int? EmpLoyeeID, int ParentID)
        {
            return ObjEntity.FL_MessageBoard.Where(C =>C.Parent==ParentID&&( C.EmpLoyeeID == EmpLoyeeID ||C.CreateEmpLoyee==EmpLoyeeID)).ToList();
        }

        public List<FL_MessageBoard> GetMineMessage(int? EmpLoyeeID, int ParentID, int PageSize, int PageIndex, out int SourceCount)
        {
            var objResult= ObjEntity.FL_MessageBoard.Where(C => C.Parent == ParentID && (C.EmpLoyeeID == EmpLoyeeID || C.CreateEmpLoyee == EmpLoyeeID)).ToList();
            SourceCount = objResult.Count();

            List<FL_MessageBoard> resultList = objResult
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MessageBoardID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MessageBoard>();
            }
            return resultList;
        }

        /// <summary>
        /// 获取唯一项目
        /// </summary>
        /// <returns></returns>
        public FL_MessageBoard GetOnlyeyParent(int Parent)
        {
            var ObjReturnModel=ObjEntity.FL_MessageBoard.FirstOrDefault(C => C.Parent == Parent);
            if (ObjReturnModel != null)
            {
                return ObjReturnModel;
            }
            return new FL_MessageBoard() { MessAgeContent=string.Empty};
        }


        /// <summary>
        /// 获取所有的消息
        /// </summary>
        /// <returns></returns>
        public List<FL_MessageBoard> GetByAll()
        {
            return ObjEntity.FL_MessageBoard.ToList();
        }


        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_MessageBoard GetByID(int? KeyID)
        {
            return ObjEntity.FL_MessageBoard.FirstOrDefault(C => C.MessageBoardID == KeyID);
        }
        /// <summary>
        /// 读取留言列表
        /// </summary>
        /// <param name="ToEmployeeId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_MessageBoard> GetByIndex(int ToEmployeeId, int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_MessageBoard.Where(C => C.EmpLoyeeID == ToEmployeeId).Count();

            List<FL_MessageBoard> resultList = ObjEntity.FL_MessageBoard.Where(C => C.EmpLoyeeID == ToEmployeeId)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MessageBoardID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MessageBoard>();
            }
            return resultList;
        }

        public List<FL_MessageBoard> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_MessageBoard.Count();

            List<FL_MessageBoard> resultList = ObjEntity.FL_MessageBoard
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MessageBoardID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MessageBoard>();
            }
            return resultList;
        }

        public int Insert(FL_MessageBoard ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MessageBoard.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MessageBoardID;
                }

            }
            return 0;
        }

        public int Update(FL_MessageBoard ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.MessageBoardID;
            }
            return 0;
        }
    }
}
