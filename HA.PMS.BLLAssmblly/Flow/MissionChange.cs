
/**
 Version :HaoAi 1.0
 File Name :MissionChange 任务变更或终止(可以看做日志)
 Author:杨洋
 Date:2013.3.23
 Description:客户管理 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;


namespace HA.PMS.BLLAssmblly.Flow
{
    public class MissionChange : ICRUDInterface<FL_MissionChange>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_MissionChange ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MissionChange.Remove(GetByID(ObjectT.ChangeID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_MissionChange> GetByAll()
        {
            return ObjEntity.FL_MissionChange.ToList();
        }

        public FL_MissionChange GetByID(int? KeyID)
        {
            return ObjEntity.FL_MissionChange.FirstOrDefault(C => C.ChangeID == KeyID);
        }

        public List<FL_MissionChange> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_MissionChange.Count();

            List<FL_MissionChange> resultList = ObjEntity.FL_MissionChange
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ChangeID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MissionChange>();
            }
            return resultList;
        }


        /// <summary>
        /// 分页获取任务待审核
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionChange> GetMissionChangeByWhere(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {
            //PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<FL_MissionChange>.GetDataByParameter(new FL_MissionChange(), ObjParList.ToArray());
            SourceCount = DataSource.Count;
            //DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageIndex).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<FL_MissionChange>();
            }
            return PageDataTools<FL_MissionChange>.AddtoPageSize(DataSource);
        }

        public int Insert(FL_MissionChange ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MissionChange.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ChangeID;
                }

            }
            return 0;
        }

        public int Update(FL_MissionChange ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ChangeID;
            }
            return 0;
        }
    }
}
