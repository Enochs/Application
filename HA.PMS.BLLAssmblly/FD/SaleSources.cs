

/**
 Version :HaoAi 1.0
 File Name :渠道信息表 
 Author:杨洋
 Date:2013.3.16
 Description:渠道信息表 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools; 

namespace HA.PMS.BLLAssmblly.FD
{
    public class SaleSources : ICRUDInterface<FD_SaleSources>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 数据查询

        /// <summary>
        /// 根据类型获取渠道
        /// </summary>
        /// <param name="ChannelTypeId"></param>
        /// <returns></returns>
        public List<FD_SaleSources> GetByType(int? ChannelTypeId)
        {
            return ObjEntity.FD_SaleSources.Where(C => C.ChannelTypeId == ChannelTypeId && C.IsDelete == false).ToList();
        }

        /// <summary>
        /// 获取渠道 
        /// </summary>
        /// <returns></returns>
        public List<FD_SaleSources> GetByAll()
        {
            return ObjEntity.FD_SaleSources.Where(C => C.IsDelete == false).ToList();
        }
        /// <summary>
        /// 根据渠道类型ID渠道 
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_SaleSources GetByID(int? KeyID)
        {
            return ObjEntity.FD_SaleSources.FirstOrDefault(C => C.SourceID == KeyID);
        }


        /// <summary>
        /// 根据渠道名称获取
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public FD_SaleSources GetByName(string Sourcename)
        {

            return ObjEntity.FD_SaleSources.FirstOrDefault(C => C.Sourcename == Sourcename);
        }
        /// <summary>
        /// 分页获取渠道新信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_SaleSources> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_SaleSources.Count();
            PageIndex = PageIndex - 1;
            List<FD_SaleSources> resultList = ObjEntity.FD_SaleSources
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SourceID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_SaleSources>();
            }
            return resultList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_SaleSources> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            var ReturnList = PublicDataTools<FD_SaleSources>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);
            return ReturnList;

        }

        public List<FD_SaleSources> Where(int PageSize, int PageIndex, out int SourceCount, Func<FD_SaleSources, bool> predicate, Func<FD_SaleSources, FD_SaleSources, bool> condition)
        {
            var query = ObjEntity.FD_SaleSources.Where(predicate).ToList().Distinct(condition);
            SourceCount = query.Count();
            PageIndex = PageIndex - 1;
            List<FD_SaleSources> resultList = query
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SourceID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                query = new List<FD_SaleSources>();
            }
            return query;
        }

        public void ReplaceOwner(int oldEmployeeID, int newEmployeeID)
        {
            IQueryable<FD_SaleSources> query = ObjEntity.FD_SaleSources.Where(C => C.ProlongationEmployee.Value.Equals(oldEmployeeID));
            foreach (FD_SaleSources fD_SaleSources in query)
            {
                fD_SaleSources.ProlongationEmployee = newEmployeeID;
            }
            ObjEntity.SaveChanges();
        }

        #endregion
        #region 数据操作

        /// <summary>
        /// 新增渠道 信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_SaleSources ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_SaleSources.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.SourceID;
                }

            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_SaleSources ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SourceID;
            }
            return 0;
        }
        /// <summary>
        /// 删除渠道信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_SaleSources ObjectT)
        {
            if (ObjectT != null)
            {
                FD_SaleSources objSaleSources = GetByID(ObjectT.SourceID);

                objSaleSources.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }


      
        #endregion


    
      
    }
}
