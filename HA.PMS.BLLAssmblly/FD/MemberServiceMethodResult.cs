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
    /// 会员服务方式 预制信息操作类
    /// </summary>
    public class MemberServiceMethodResult : ICRUDInterface<CS_MemberServiceMethodResult>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CS_MemberServiceMethodResult ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<CS_MemberServiceMethodResult> GetByAll()
        {
            return ObjEntity.CS_MemberServiceMethodResult.ToList();
        }

        public CS_MemberServiceMethodResult GetByID(int? KeyID)
        {
            return ObjEntity.CS_MemberServiceMethodResult.FirstOrDefault(C => C.ServiceId == KeyID);
        }

        public List<CS_MemberServiceMethodResult> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_MemberServiceMethodResult.Count();

            List<CS_MemberServiceMethodResult> resultList = ObjEntity.CS_MemberServiceMethodResult
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ServiceId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_MemberServiceMethodResult>();
            }
            return resultList;
        }

        public int Insert(CS_MemberServiceMethodResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_MemberServiceMethodResult.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ServiceId;
                }

            }
            return 0;
        }

        public int Update(CS_MemberServiceMethodResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ServiceId;
            }
            return 0;
        }
    }
}
