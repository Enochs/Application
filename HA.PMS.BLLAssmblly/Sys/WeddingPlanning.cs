using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class WeddingPlanning:ICRUDInterface<Sys_WeddingPlanning>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(Sys_WeddingPlanning ObjectT)
        {

            if (ObjectT != null)
            {
                Sys_WeddingPlanning objWeddingPlanning = GetByID(ObjectT.PlanningID);

                objWeddingPlanning.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 获取并创建新的顶级节点 SortOrder值
        /// </summary>
        /// <returns></returns>
        public string QuerySortOderTop()
        {
            var query = GetByAll();
            //获取最后一个顶级节点 的第一位数
            var queryTopSortOrder = (from m in query
                                     where m.SortOrder.Length == 3
                                     select m).LastOrDefault();
            int newTopSortOrder = 0;
            //如果不为空的话，就返回对应的Sortder值
            if (queryTopSortOrder != null)
            {
                newTopSortOrder = queryTopSortOrder.SortOrder.Substring(0, 1).ToInt32();
                newTopSortOrder += 1;
                return newTopSortOrder + "01";
            }
            else
            {
                return "101";
            }


        }
        /// <summary>
        /// 返回一个顶级的最后一个子节点的sortOrder，并在原有的基础上加1 返回一个新的SortOrder值
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public string QuerySortOrderChild(int CategoryId)
        {
            var queryLastSortOrder = (from m in GetByAll()
                                      where m.ParentID == CategoryId
                                      orderby m.SortOrder descending
                                      select m).FirstOrDefault();
            int newChildSortOrder = 0;
            //证明该顶级节点下面有子节点在原有的基础上面累加
            if (queryLastSortOrder != null)
            {
                newChildSortOrder = queryLastSortOrder.SortOrder.ToInt32() + 1;
                return newChildSortOrder.ToString();
            }
            else
            {
                //如果为空的话，就在对应的父级节点的SortOrder加字符1
                var parentCategory = GetByID(CategoryId);
                return parentCategory.SortOrder + "1";
            }

        }
        /// <summary>
        ///根据父级ID获取产品类别
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public List<Sys_WeddingPlanning> GetByparent(int? ParentID)
        {
            return ObjEntity.Sys_WeddingPlanning.Where(C => C.ParentID == ParentID&&C.IsDelete==false).ToList();
        }


        public List<Sys_WeddingPlanning> GetByAll()
        {
            return ObjEntity.Sys_WeddingPlanning.Where(C => C.IsDelete == false).OrderBy(C => C.PlanningID).ToList();
        }

        public Sys_WeddingPlanning GetByID(int? KeyID)
        {
            return ObjEntity.Sys_WeddingPlanning.FirstOrDefault(C => C.PlanningID == KeyID);
        }
        

        public List<Sys_WeddingPlanning> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.Sys_WeddingPlanning.Count();

            List<Sys_WeddingPlanning> resultList = ObjEntity.Sys_WeddingPlanning
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PlanningID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_WeddingPlanning>();
            }
            return resultList;
        }

        public int Insert(Sys_WeddingPlanning ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_WeddingPlanning.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.PlanningID;
                }

            }
            return 0;
        }

        public int Update(Sys_WeddingPlanning ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.PlanningID;
            }
            return 0;
        }
    }
}
