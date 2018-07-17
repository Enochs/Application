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
    /// 服务类型 预制信息操作类
    /// </summary>
    public class MemberServiceTypeResult : ICRUDInterface<CS_MemberServiceTypeResult>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CS_MemberServiceTypeResult ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<CS_MemberServiceTypeResult> GetByAll()
        {
            return ObjEntity.CS_MemberServiceTypeResult.ToList();
        }

        public CS_MemberServiceTypeResult GetByID(int? KeyID)
        {
            return ObjEntity.CS_MemberServiceTypeResult.FirstOrDefault(C => C.ServiceTypeId == KeyID);
        }

        public List<CS_MemberServiceTypeResult> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_MemberServiceTypeResult.Count();

            List<CS_MemberServiceTypeResult> resultList = ObjEntity.CS_MemberServiceTypeResult
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ServiceTypeId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_MemberServiceTypeResult>();
            }
            return resultList;
        }

        public int Insert(CS_MemberServiceTypeResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_MemberServiceTypeResult.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ServiceTypeId;
                }

            }
            return 0;
        }

        public int Update(CS_MemberServiceTypeResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ServiceTypeId;
            }
            return 0;
        }
    }
}
