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
    /// ;婚礼现场内部满意度评价
    /// </summary>
    public class WeddingSceneEvaluationResult : ICRUDInterface<CA_WeddingSceneEvaluationResult>      
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_WeddingSceneEvaluationResult ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<CA_WeddingSceneEvaluationResult> GetByAll()
        {
            return ObjEntity.CA_WeddingSceneEvaluationResult.ToList();
        }

        public CA_WeddingSceneEvaluationResult GetByID(int? KeyID)
        {
            return ObjEntity.CA_WeddingSceneEvaluationResult.FirstOrDefault(C => C.EvaluationId == KeyID);
        }

        public List<CA_WeddingSceneEvaluationResult> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_WeddingSceneEvaluationResult.Count();

            List<CA_WeddingSceneEvaluationResult> resultList = ObjEntity.CA_WeddingSceneEvaluationResult
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.EvaluationId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_WeddingSceneEvaluationResult>();
            }
            return resultList;
        }

        public int Insert(CA_WeddingSceneEvaluationResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_WeddingSceneEvaluationResult.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.EvaluationId;
                }

            }
            return 0;
        }

        public int Update(CA_WeddingSceneEvaluationResult ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.EvaluationId;
            }
            return 0;
        }
    }
}
