/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.12
 Description:控件类
 History:修改日志
 
 Author:杨洋
 date:2013.3.12
 version:好爱1.0
 description:修改描述
 
 
 
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
    public class Controls : ICRUDInterface<Sys_Controls>
    {


        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 根据控件的ID来进行删除操作
        /// </summary>
        /// <param name="ObjectT">控件实体类</param>
        /// <returns></returns>
        public int Delete(Sys_Controls ObjectT)
        {
            if (ObjectT != null)
            {
                
                ObjEntity.Sys_Controls.FirstOrDefault(
                   C => C.ControlID == ObjectT.ControlID).IsDelete = false;
                return ObjEntity.SaveChanges();
            }
            return 0;

        }
        /// <summary>
        /// 返回控件表中所有的信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_Controls> GetByAll()
        {
            return ObjEntity.Sys_Controls.Where(C => C.IsDelete == false).ToList();
        }


        /// <summary>
        /// 根据频道获取控件
        /// </summary>
        /// <returns></returns>
        public List<Sys_Controls> GetByChannel(int? ChannelID)
        {
            return ObjEntity.Sys_Controls.Where(C => C.IsDelete == false&&C.ChannelID==ChannelID).ToList();
        }
        /// <summary>
        /// 根据主键返回单个控件信息
        /// </summary>
        /// <param name="KeyID">主键值</param>
        /// <returns>根据查询之后的结果，如果为空则返回默认实例</returns>
        public Sys_Controls GetByID(int? KeyID)
        {
            if (KeyID.HasValue)
            {
                Sys_Controls control = ObjEntity.Sys_Controls.FirstOrDefault(
                    C => C.ControlID == KeyID);
                if (control != null)
                {
                    return control;
                }

            }

            return new Sys_Controls();


        }
        /// <summary>
        /// 对于控件表的分页操作
        /// </summary>
        /// <param name="PageSize">一页多少条</param>
        /// <param name="PageIndex">当前第几页</param>
        /// <param name="SourceCount">out 输出一共有多少条记录</param>
        /// <returns>返回控件表的集合</returns>
        public List<Sys_Controls> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.Sys_Controls.Count();

            List<Sys_Controls> resultList = ObjEntity.Sys_Controls
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ControlID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<Sys_Controls>();
            }
            return resultList;

        }
        /// <summary>
        /// 添加控件信息
        /// </summary>
        /// <param name="ObjectT">控件实体类</param>
        /// <returns>返回新添加控件的编号</returns>
        public int Insert(Sys_Controls ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_Controls.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ControlID;
                }

            }
            return 0;
        }
        /// <summary>
        /// 根据控件ID，修改某个控件的信息
        /// </summary>
        /// <param name="ObjectT">控件类实体</param>
        /// <returns>返回被修改的某个控件的ControlID</returns>
        public int Update(Sys_Controls ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ControlID;
            }
            return 0;
        }
    }

}
