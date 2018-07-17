
/**
 Version :HaoAi 1.0
 File Name :客户取件状态
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
    //  public class TakeState:ICRUDInterface<CS_TakeState>
    //{
    //      PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
    //    public int Delete(CS_TakeState ObjectT)
    //    {
    //        if (ObjectT != null)
    //        {
    //            ObjEntity.CS_TakeState.FirstOrDefault(
    //             C => C.TakeStateID == ObjectT.TakeStateID).IsDelete = true;
    //            return ObjEntity.SaveChanges();
    //        }
    //        return 0;
    //    }

    //    public List<CS_TakeState> GetByAll()
    //    {
    //        return ObjEntity.CS_TakeState.Where(C => C.IsDelete == false).ToList();
    //    }

    //    public CS_TakeState GetByID(int? KeyID)
    //    {
    //        return ObjEntity.CS_TakeState.FirstOrDefault(C => C.TakeStateID == KeyID);
    //    }

    //    public List<CS_TakeState> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
    //    {
    //        SourceCount = ObjEntity.CS_TakeState.Count();

    //        List<CS_TakeState> resultList = ObjEntity.CS_TakeState
    //            //进行排序功能操作，不然系统会抛出异常
    //               .OrderByDescending(C => C.TakeStateID)
    //               .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

    //        if (resultList.Count == 0)
    //        {
    //            resultList = new List<CS_TakeState>();
    //        }
    //        return resultList;
    //    }

    //    public int Insert(CS_TakeState ObjectT)
    //    {
    //        if (ObjectT != null)
    //        {
    //            ObjEntity.CS_TakeState.Add(ObjectT);
    //            if (ObjEntity.SaveChanges() > 0)
    //            {
    //                return ObjectT.TakeStateID;
    //            }

    //        }
    //        return 0;
    //    }

    //    public int Update(CS_TakeState ObjectT)
    //    {
    //        if (ObjectT != null)
    //        {
    //            ObjEntity.SaveChanges();
    //            return ObjectT.TakeStateID;
    //        }
    //        return 0;
    //    }
    //}
}
