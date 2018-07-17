
/**
 Version :HaoAi 1.0
 File Name :Category
 Author:杨洋
 Date:2013.3.21
 Description:知识库 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
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
    public class CelebrationKnowledge:ICRUDInterface<FD_CelebrationKnowledge>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_CelebrationKnowledge ObjectT)
        {
            if (ObjectT != null)
            {
                FD_CelebrationKnowledge objCelebrationKnowledge = GetByID(ObjectT.KnowledgeID);

                objCelebrationKnowledge.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        public List<FD_CelebrationKnowledge> GetbyFD_CelebrationKnowledgeParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_CelebrationKnowledge>.GetDataByParameter(new FD_CelebrationKnowledge(), ObjParameterList).Where(C=>C.IsDelete==false);
            SourceCount = query.Count();

            List<FD_CelebrationKnowledge> resultList = query.OrderByDescending(C => C.KnowledgeID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count() == 0)
            {
                resultList = new List<FD_CelebrationKnowledge>();
            }
            return resultList;

        }
        public List<FD_CelebrationKnowledge> GetByAll()
        {
            return ObjEntity.FD_CelebrationKnowledge.Where(C => C.IsDelete == false) .ToList();
        }

        public FD_CelebrationKnowledge GetByID(int? KeyID)
        {
            return ObjEntity.FD_CelebrationKnowledge.FirstOrDefault(C => C.KnowledgeID == KeyID);
        }

        public List<FD_CelebrationKnowledge> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_Category.Count();

            List<FD_CelebrationKnowledge> resultList = ObjEntity.FD_CelebrationKnowledge
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.KnowledgeID)
                   .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_CelebrationKnowledge>();
            }
            return resultList;
        }

        public int Insert(FD_CelebrationKnowledge ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FD_CelebrationKnowledge.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.KnowledgeID;
                }

            }
            return 0;
        }

        public int Update(FD_CelebrationKnowledge ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.KnowledgeID;
            }
            return 0;
        }
    }
}
