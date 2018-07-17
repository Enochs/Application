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
    /// 新人满意度评分配置
    /// </summary>
    public class DegreeAssessResult : ICRUDInterface<CS_DegreeAssessResult>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CS_DegreeAssessResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeAssessResult.Remove(GetByID(ObjectT.AssessId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CS_DegreeAssessResult> GetByAll()
        {
            return ObjEntity.CS_DegreeAssessResult.ToList();
        }

        public CS_DegreeAssessResult GetByID(int? KeyID)
        {
            return ObjEntity.CS_DegreeAssessResult.FirstOrDefault(C => C.AssessId == KeyID);
        }

        public List<CS_DegreeAssessResult> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_DegreeAssessResult.Count();

            List<CS_DegreeAssessResult> resultList = ObjEntity.CS_DegreeAssessResult
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.AssessId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_DegreeAssessResult>();
            }
            return resultList;
        }

        public int Insert(CS_DegreeAssessResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeAssessResult.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.AssessId;
                }

            }
            return 0;
        }

        public int Update(CS_DegreeAssessResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.AssessId;
            }
            return 0;
        }
    }
}
