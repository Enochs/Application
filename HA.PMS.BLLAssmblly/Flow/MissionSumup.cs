
/**
 Version :HaoAi 1.0
 File Name :MissionSumup 任务变更或终止(可以看做日志)
 Author:杨洋
 Date:2013.3.23
 Description:计划总结 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;


namespace HA.PMS.BLLAssmblly.Flow
{
    public class MissionSumup : ICRUDInterface<FL_MissionSumup>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 根据任务包获取
        /// </summary>
        /// <param name="MissionID"></param>
        /// <returns></returns>
        public FL_MissionSumup GetByMissionID(int? MissionID)
        {

            return ObjEntity.FL_MissionSumup.FirstOrDefault(C => C.MissionID == MissionID);
        }

        public int Delete(FL_MissionSumup ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MissionSumup.Remove(GetByID(ObjectT.SumID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_MissionSumup> GetByAll()
        {
            return ObjEntity.FL_MissionSumup.ToList();
        }

        public FL_MissionSumup GetByID(int? KeyID)
        {
            return ObjEntity.FL_MissionSumup.FirstOrDefault(C => C.SumID == KeyID);
        }

        public List<FL_MissionSumup> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_MissionSumup.Count();

            List<FL_MissionSumup> resultList = ObjEntity.FL_MissionSumup
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SumID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MissionSumup>();
            }
            return resultList;
        }

        public int Insert(FL_MissionSumup ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MissionSumup.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.SumID;
                }

            }
            return 0;
        }

        public int Update(FL_MissionSumup ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SumID;
            }
            return 0;
        }
    }
}
