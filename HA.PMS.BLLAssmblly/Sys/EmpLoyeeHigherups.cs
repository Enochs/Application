

/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:人员上级
 History:实现ICRUDInterface<T> 接口中的方法
 
 Author:杨洋
 date:2013.3.12
 version:好爱1.0
 description:修改描述
 添加人员上级 的增删改查方法以及分页方法
 
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
    //
    public class EmpLoyeeHigherups : ICRUDInterface<Sys_EmpLoyeeHigherups>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 根据人员上级的UpKey来进行删除操作
        /// </summary>
        /// <param name="ObjectT">人员上级实体类</param>
        /// <returns></returns>
        public int Delete(Sys_EmpLoyeeHigherups ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_EmpLoyeeHigherups.Remove(
                   ObjEntity.Sys_EmpLoyeeHigherups.FirstOrDefault(
                   C => C.IsDelete == true)
                );
                return ObjEntity.SaveChanges();

            }
            return 0;

        }
        /// <summary>
        /// 返回人员上级表中所有的信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_EmpLoyeeHigherups> GetByAll()
        {
            return ObjEntity.Sys_EmpLoyeeHigherups.Where(C=>C.IsDelete==false).ToList();
        }
        /// <summary>
        /// 根据主键返回单个人员上级信息
        /// </summary>
        /// <param name="KeyID">主键值</param>
        /// <returns>根据查询之后的结果，如果为空则返回默认实例</returns>
        public Sys_EmpLoyeeHigherups GetByID(int? KeyID)
        {
            if (KeyID.HasValue)
            {
                Sys_EmpLoyeeHigherups empHight = ObjEntity.Sys_EmpLoyeeHigherups.FirstOrDefault(
                    C => C.UpKey == KeyID);
                if (empHight != null)
                {
                    return empHight;
                }

            }

            return new Sys_EmpLoyeeHigherups();


        }
        /// <summary>
        /// 对于人员上级表的分页操作
        /// </summary>
        /// <param name="PageSize">一页多少条</param>
        /// <param name="PageIndex">当前第几页</param>
        /// <param name="SourceCount">out 输出一共有多少条记录</param>
        /// <returns>返回人员上级表的集合</returns>
        public List<Sys_EmpLoyeeHigherups> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.Sys_EmpLoyeeHigherups.Count();

            List<Sys_EmpLoyeeHigherups> resultList = ObjEntity.Sys_EmpLoyeeHigherups
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.UpKey)
                   .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_EmpLoyeeHigherups>();
            }
            return resultList;

        }
        /// <summary>
        /// 添加人员上级信息
        /// </summary>
        /// <param name="ObjectT">人员上级实体类</param>
        /// <returns>返回新添加人员上级信息的编号</returns>
        public int Insert(Sys_EmpLoyeeHigherups ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_EmpLoyeeHigherups.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.UpKey;
                }

            }
            return 0;
        }
        /// <summary>
        /// 根据人员上级ID，修改某个人员上级的信息
        /// </summary>
        /// <param name="ObjectT">人员上级类实体</param>
        /// <returns>返回被修改的某个人员上级的UpKey</returns>
        public int Update(Sys_EmpLoyeeHigherups ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.UpKey;
            }
            return 0;
        }
    }
}
