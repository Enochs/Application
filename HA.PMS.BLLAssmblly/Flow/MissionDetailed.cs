
/**
 Version :HaoAi 1.0
 File Name :MissionDetailed 任务详细表
 Author:杨洋
 Date:2013.3.23
 Description:客户管理 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
using System;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class MissionDetailed : ICRUDInterface<FL_MissionDetailed>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_MissionDetailed ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MissionDetailed.Remove(GetByID(ObjectT.DetailedID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        ///// <summary>
        ///// 获取用户任务
        ///// </summary>
        ///// <param name="PageSize"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="SourceCount"></param>
        ///// <returns></returns>
        //public List<FL_MissionDetailedEmployee> GetByFL_MissionDetailedIndex(int PageSize, int PageIndex, out int SourceCount)
        //{
        //    SourceCount = ObjEntity.FL_MissionDetailedEmployee.Count();

        //    List<FL_MissionDetailedEmployee> resultList = ObjEntity.FL_MissionDetailedEmployee
        //        //进行排序功能操作，不然系统会抛出异常
        //           .OrderByDescending(C => C.DetailedID)
        //           .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

        //    if (resultList.Count == 0)
        //    {
        //        resultList = new List<FL_MissionDetailedEmployee>();
        //    }
        //    return resultList;
        //}

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionDetailed> GetByAll()
        {
            return ObjEntity.FL_MissionDetailed.ToList();
        }


        /// <summary>
        /// 分页获取派工
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionDetailed> GetMissionDetailedByWhere(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {
            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<FL_MissionDetailed>.GetDataByParameter(new FL_MissionDetailed(), ObjParList.ToArray()).OrderByDescending(C => C.PlanDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<FL_MissionDetailed>();
            }
            return PageDataTools<FL_MissionDetailed>.AddtoPageSize(DataSource);
        }


        /// <summary>
        /// 根据ID获取任务详情
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_MissionDetailed GetByID(int? KeyID)
        {
            return ObjEntity.FL_MissionDetailed.FirstOrDefault(C => C.DetailedID == KeyID);
        }
        //FL_MissionDetailedEmployee

        /// <summary>
        /// 根据任务主体ID获取任务详情列表
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionDetailed> GetbyMissionID(int? MissionID)
        {

            return ObjEntity.FL_MissionDetailed.Where(C => C.MissionID == MissionID).OrderByDescending(C => C.CreateDate).ToList();
        }

        /// <summary>
        /// 根据任务主体ID获取任务详情列表 Distinct
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionDetailed> GetbyMissionIDDistinct(int? MissionID)
        {

            // return ObjEntity.FL_MissionDetailed.Where(C => C.MissionID == MissionID).OrderByDescending(C => C.CreateDate).ToList().DistinctC(new MissionDetailedComparer());
            return ObjEntity.FL_MissionDetailed.Where(C => C.MissionID == MissionID).OrderByDescending(C => C.CreateDate).ToList().Distinct((C, D) => C.MissionID.Equals(D.MissionID));
        }


        public List<FL_MissionDetailed> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_Message.Count();

            List<FL_MissionDetailed> resultList = ObjEntity.FL_MissionDetailed
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.DetailedID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MissionDetailed>();
            }
            return resultList;
        }


        /// <summary>
        /// 添加任务详情
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_MissionDetailed ObjectT)
        {
            if (ObjectT != null)
            {
                ObjectT.MissionState = 0;
                ObjEntity.FL_MissionDetailed.Add(ObjectT);

                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DetailedID;
                }

            }
            return 0;
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_MissionDetailed ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }



        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="MissionType"></param>
        /// <param name="FinishKey"></param>
        /// <returns></returns>
        public int UpdateforFlow(int? MissionType, int? FinishKey, int? EmpLoyeeID)
        {
            var ObjUpdateModel = ObjEntity.FL_MissionDetailed.Where(C => C.FinishKey == FinishKey && C.MissionType == MissionType && C.EmpLoyeeID == EmpLoyeeID).ToList();
            if (ObjUpdateModel.Count > 0)
            {
                foreach (var ObjItem in ObjUpdateModel)
                {
                    ObjItem.IsOver = true;
                    ObjItem.FinishDate = DateTime.Now;
                    ObjEntity.SaveChanges();

                }
                return 0;
            }
            else
            {
                return 0;
            }
        }


        public int UpdateforFlowEmpLoyee(int? MissionType, int? FinishKey, int? EmpLoyeeID)
        {
            var ObjUpdateModel = ObjEntity.FL_MissionDetailed.FirstOrDefault(C => C.FinishKey == FinishKey);
            if (ObjUpdateModel != null)
            {

                ObjUpdateModel.FinishDate = DateTime.Now;
                ObjUpdateModel.IsOver = true;
                ObjUpdateModel.EmpLoyeeID = EmpLoyeeID;
                ObjEntity.SaveChanges();
                return ObjUpdateModel.DetailedID;
            }
            else
            {
                return 0;
            }
        }



        /// <summary>
        /// 根据类型和完成kEY
        /// </summary>
        /// <param name="FinishKey"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int GetIDByTypeandFinishKey(int FinishKey, int Type)
        {
            var ObjModel = ObjEntity.FL_MissionDetailed.FirstOrDefault(C => C.FinishKey == FinishKey && C.MissionType == Type);
            if (ObjModel != null)
            {
                return ObjModel.DetailedID;
            }
            else
            {
                return 0;
            }
        }

        public List<FL_MissionDetailed> GetAllByParameter(List<PMSParameters> pars, string OrderByName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FL_MissionDetailed>.GetDataByWhereParameter(pars,OrderByName,PageSize,PageIndex,out SourceCount);
        }

        /// <summary>
        /// 获取该员工所有的任务
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public int GetCountByEmployeeID(int? EmployeeID)
        {
            return ObjEntity.FL_MissionDetailed.Where(C => C.EmpLoyeeID == EmployeeID).ToList().Count.ToString().ToInt32();
        }

    }

    public class MissionDetailedComparer : System.Collections.Generic.IEqualityComparer<FL_MissionDetailed>
    {
        public bool Equals(FL_MissionDetailed x, FL_MissionDetailed y)
        {
            if (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null))
            {
                return x.MissionID.Equals(y.MissionID);
            }
            else return false;
        }

        public int GetHashCode(FL_MissionDetailed obj)
        {
            return object.ReferenceEquals(obj, null) ? 0 : obj.GetHashCode();
        }
    }
}
