

/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:人员类型表
 History:实现ICRUDInterface<T> 接口中的方法
 
 Author:杨洋
 date:2013.3.12
 version:好爱1.0
 description:修改描述
 添加人员类型 的增删改查方法以及分页方法
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
namespace HA.PMS.BLLAssmblly.Sys
{
    public class EmployeeType:ICRUDInterface<Sys_EmployeeType>
    {
        HA.PMS.DataAssmblly.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 人员类型EmployeeTypeID来进行删除操作
        /// </summary>
        /// <param name="ObjectT">人员类型实体类</param>
        /// <returns></returns>
        public int Delete(Sys_EmployeeType ObjectT)
        {
            if (ObjectT != null)
            {
               

                ObjEntity.Sys_EmployeeType.FirstOrDefault(
                C => C.EmployeeTypeID == ObjectT.EmployeeTypeID).IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        /// <summary>
        /// 返回人员类型表中所有的信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_EmployeeType> GetByAll()
        {
            return ObjEntity.Sys_EmployeeType.Where(C => C.IsDelete == false).ToList();
        }


        /// <summary>
        /// 对于人员类型表的分页操作
        /// </summary>
        /// <param name="PageSize">一页多少条</param>
        /// <param name="PageIndex">当前第几页</param>
        /// <param name="SourceCount">out 输出一共有多少条记录</param>
        /// <returns>返回人员类型表的集合</returns>
        public List<Sys_EmployeeType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.Sys_EmployeeType.Count();

            List<Sys_EmployeeType> resultList = ObjEntity.Sys_EmployeeType.Where(C=>C.IsDelete==false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.EmployeeTypeID)
                   .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_EmployeeType>();
            }
            return resultList;
        }


        /// <summary>
        /// 添加用户类型
        /// </summary>
        /// <param name="ObjectT">用户类型实体</param>
        /// <returns>返回新增加的自增序列</returns>
        public int Insert(Sys_EmployeeType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_EmployeeType.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {

                    return ObjectT.EmployeeTypeID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 修改用户类型
        /// </summary>
        /// <param name="ObjectT">用户类型实体</param>
        /// <returns>返回修改的自增序列</returns>
        public int Update(Sys_EmployeeType ObjectT)
        {
            if (ObjectT != null)
            {
                if (ObjEntity.SaveChanges() > 0)
                {

                    return ObjectT.EmployeeTypeID;
                }
            }

            return 0;
        }

        /// <summary>
        /// 返回单个员工类型信息
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public Sys_EmployeeType GetByID(int? KeyID)
        {
          return  ObjEntity.Sys_EmployeeType.FirstOrDefault(C=>C.EmployeeTypeID==KeyID);
        }


       
    }
}
