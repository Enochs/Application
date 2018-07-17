using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.CA
{
    /// <summary>
    /// 我的目标
    /// </summary>
    public class MyGoalTarget:ICRUDInterface<CA_MyGoalTarget>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_MyGoalTarget ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.CA_MyGoalTarget.Remove(GetByID(ObjectT.GoalId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// 根据图  根据目标类型ID返回所有计划目标数据
        /// </summary>
        /// <param name="TargetTypeId"></param>
        /// <returns></returns>
        public List<CA_MyGoalTarget> GetListByTargetTypeId(int TargetTypeId) 
        {
            var query= ObjEntity.CA_MyGoalTarget.Where(C => C.TargetTypeId.Value == TargetTypeId).ToList();
            if (query!=null)
            {
                return query;
            }
            return new List<CA_MyGoalTarget>();
        }
        public List<CA_MyGoalTarget> GetByAll()
        {
            return ObjEntity.CA_MyGoalTarget.ToList();
        }

        public CA_MyGoalTarget GetByID(int? KeyID)
        {

            return ObjEntity.CA_MyGoalTarget.FirstOrDefault(C => C.GoalId == KeyID);
        }


        public List<View_MyGoalTargetType> GetbyParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<View_MyGoalTargetType>.GetDataByParameter(new View_MyGoalTargetType(), ObjParameterList);

            return query;

        }
        public List<CA_MyGoalTarget> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_MyGoalTarget.Count();

            List<CA_MyGoalTarget> resultList = ObjEntity.CA_MyGoalTarget
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GoalId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_MyGoalTarget>();
            }
            return resultList;
        }

        public int Insert(CA_MyGoalTarget ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_MyGoalTarget.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.GoalId;
                }

            }
            return 0;
        }

        public int Update(CA_MyGoalTarget ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.GoalId;
            }
            return 0;
        }
    }
}
