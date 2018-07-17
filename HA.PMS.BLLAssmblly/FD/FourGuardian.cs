
/**
 Version :HaoAi 1.0
 File Name :FD_FourGuardian 四大金刚
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.FD
{
    public class FourGuardian : ICRUDInterface<FD_FourGuardian>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_FourGuardian ObjectT)
        {
            if (ObjectT != null)
            {
                FD_FourGuardian objCategory = GetByID(ObjectT.GuardianId);

                objCategory.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }


        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<FD_FourGuardian> GetByType(int? type)
        {
            return ObjEntity.FD_FourGuardian.Where(C => C.IsDelete == false && C.GuardianTypeId == type).ToList();
        }



        /// <summary>
        /// 获取组内的四大
        /// </summary>
        /// <param name="KeyList"></param>
        /// <returns></returns>
        public List<FD_FourGuardian> GetByInKeyList(int[] ObjKeyList)
        {
            var ObjSource = (from C in ObjEntity.FD_FourGuardian

                             where (ObjKeyList).Contains(C.GuardianTypeId)
                             select C
                     );
            return ObjSource.ToList();

        }




        public List<FD_FourGuardian> GetByAll()
        {
            return ObjEntity.FD_FourGuardian.Where(C => C.IsDelete == false).ToList();
        }

        public FD_FourGuardian GetByID(int? KeyID)
        {
            return ObjEntity.FD_FourGuardian.FirstOrDefault(C => C.GuardianId == KeyID);
        }


        public FD_FourGuardian GetByName(string Name)
        {
            return ObjEntity.FD_FourGuardian.FirstOrDefault(C => C.GuardianName == Name);
        }

        public List<FD_GuardianTypeLeven> GetbyParameter(List<PMSParameters> ObjParameterList,string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_GuardianTypeLeven>.GetDataByWhereParameter(ObjParameterList, OrderByColumnName, PageSize, PageIndex, out SourceCount);
            return query;
            //List<FD_GuardianTypeLeven> resultList = query.OrderByDescending(C => C.GuardianId)
            //  .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            //if (query.Count() == 0)
            //{
            //    resultList = new List<FD_GuardianTypeLeven>();
            //}
            //return resultList;

        }

        public List<FD_GuardianTypeLeven> GetAllByParameter(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FD_GuardianTypeLeven>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, PageIndex, out SourceCount);
        }

        public List<FD_FourGuardian> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_FourGuardian.Count();

            List<FD_FourGuardian> resultList = ObjEntity.FD_FourGuardian
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GuardianId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_FourGuardian>();
            }
            return resultList;
        }
        public List<FD_GuardianTypeLeven> GetByGuardianTypeLevenIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuardianTypeLeven.Count();

            List<FD_GuardianTypeLeven> resultList = ObjEntity.FD_GuardianTypeLeven
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GuardianId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuardianTypeLeven>();
            }
            return resultList;
        }




        public int Insert(FD_FourGuardian ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_FourGuardian.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.GuardianId;
                }

            }
            return 0;
        }

        public int Update(FD_FourGuardian ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.GuardianId;
            }
            return 0;
        }

        public List<FD_FourGuardian> GetGuardianGroup(int PageSize, int CurrentPageIndex, List<PMSParameters> pars, ref int SourceCount)
        {
            return PublicDataTools<FD_FourGuardian>.GetGuardianGroup(PageSize, CurrentPageIndex, pars, ref SourceCount);
        }

        public string GetGuardianById(int GuardianId, List<PMSParameters> pars, int Type)
        {
            return PublicDataTools<string>.GetGuardianById(GuardianId, pars, Type);
        }

    }
}
