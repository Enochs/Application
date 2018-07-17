
/**
 Version :HaoAi 1.0
 File Name :客户满意度调查类
 Author:杨洋
 Date:2013.3.25
 Description:客户投诉类 实现ICRUDInterface<T> 接口中的方法
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.CS
{
    public class InvestigateState:ICRUDInterface<CS_InvestigateState>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CS_InvestigateState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_InvestigateState.FirstOrDefault(
                 C => C.InvestigateStateID == ObjectT.InvestigateStateID).IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CS_InvestigateState> GetByAll()
        {
            return ObjEntity.CS_InvestigateState.Where(C => C.IsDelete == false).ToList();
        }

        public CS_InvestigateState GetByID(int? KeyID)
        {
            return ObjEntity.CS_InvestigateState.FirstOrDefault(C => C.InvestigateStateID == KeyID);
        }

        public List<CS_InvestigateState> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.CS_InvestigateState.Count();

            List<CS_InvestigateState> resultList = ObjEntity.CS_InvestigateState
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.InvestigateStateID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_InvestigateState>();
            }
            return resultList;
        }

        public int Insert(CS_InvestigateState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_InvestigateState.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.InvestigateStateID;
                }

            }
            return 0;
        }

        public int Update(CS_InvestigateState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.InvestigateStateID;
            }
            return 0;
        }
    }
}
