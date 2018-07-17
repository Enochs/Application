
/**
 Version :HaoAi 1.0
 File Name :Category
 Author:杨洋
 Date:2013.3.15
 Description:产品类型 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.FD
{
    public class Category : ICRUDInterface<FD_Category>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 产品删除方法
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_Category ObjectT)
        {
            if (ObjectT != null)
            {
                FD_Category objCategory = GetByID(ObjectT.CategoryID);

                objCategory.IsDelete = true;
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
        public List<FD_Category> GetByparent(int? ParentID)
        {
            return ObjEntity.FD_Category.Where(C => C.ParentID == ParentID&&C.IsDelete==false).ToList();
        }


        /// <summary>
        /// in查询类别
        /// </summary>
        /// <param name="ObjKeyList"></param>
        /// <returns></returns>
        public List<FD_Category> GetinList(int[] ObjKeyList)
        {
  
            var ObjSource = (from C in ObjEntity.FD_Category

                             where (ObjKeyList).Contains(C.CategoryID)
                             select C
                                 );
            return ObjSource.Where(C=>C.IsDelete==false).ToList();

            //return ObjEntity.FD_Category.Where(C => C.CategoryID in ObjKeyList).ToList();
        }

        /// <summary>
        /// 返回所有产品信息
        /// </summary>
        /// <returns></returns>
        public List<FD_Category> GetByAll()
        {
            return ObjEntity.FD_Category.Where(C => C.IsDelete == false).OrderBy(C => C.CategoryID).ToList();
        }
        /// <summary>
        /// 返回某个产品
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_Category GetByID(int? KeyID)
        {
            return ObjEntity.FD_Category.FirstOrDefault(C => C.CategoryID == KeyID);
        }
        /// <summary>
        /// 分页返回产品类型信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_Category> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_Category.Count();

            List<FD_Category> resultList = ObjEntity.FD_Category
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CategoryID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Category>();
            }
            return resultList;
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_Category ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FD_Category.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.CategoryID;
                }

            }
            return 0;
        }
        /// <summary>
        /// 修改类别信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_Category ObjectT)
        {
                return ObjEntity.SaveChanges();
        }
    }
}
