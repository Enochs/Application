/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:黄晓可
 Date:2013.3.14
 Description:频道管理
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
namespace HA.PMS.BLLAssmblly.Sys
{
    public class Channel : ICRUDInterface<Sys_Channel>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="ObjectT">频道实体</param>
        /// <returns>返回删除的的实体KEY</returns>
        public int Delete(Sys_Channel ObjectT)
        {
            UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();

            ObjUserJurisdictionBLL.DeleteByChannelID(ObjectT.ChannelID);
            ObjEntity.Sys_Channel.Remove(GetByID(ObjectT.ChannelID));
            ObjEntity.SaveChanges();
            return ObjectT.ChannelID;
        }
        
        /// <summary>
        /// 获取部门所需
        /// </summary>
        /// <returns></returns>
        public List<Sys_Channel> GetforDepartment()
        {
            return ObjEntity.Sys_Channel.Where(C => C.Parent == 0&&C.ChannelGetType!=null&&C.ChannelGetType!=string.Empty).OrderBy(C => C.OrderCode).ToList();
        }

        /// <summary>
        /// 获取所有频道菜单
        /// </summary>
        /// <returns></returns>
        public List<Sys_Channel> GetByAll()
        {
            return ObjEntity.Sys_Channel.Where(C=>C.IsMenu==true).ToList();
        }


        /// <summary>
        /// 获取子集频道
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        public List<Sys_Channel> GetByParent(int? ChannelID)
        {
            return ObjEntity.Sys_Channel.Where(C => C.Parent == ChannelID).OrderBy(C=>C.OrderCode).ToList();
        }

        /// <summary>
        /// 根据ID获取频道
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public Sys_Channel GetByID(int? KeyID)
        {
            return ObjEntity.Sys_Channel.FirstOrDefault(C=>C.ChannelID==KeyID);
        }


        /// <summary>
        /// 暂时不需要分页
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<Sys_Channel> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 根据频道类型获取
        /// </summary>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public Sys_Channel GetbyClassType(string ClassType)
        {
          var ObjModel= ObjEntity.Sys_Channel.FirstOrDefault(C=>C.ChannelGetType==ClassType);
          return ObjModel;
        }


        /// <summary>
        /// 添加频道
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(Sys_Channel ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.Sys_Channel.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ChannelID;
                }

            }
            return 0;
        }

        /// <summary>
        /// 修改频道
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(Sys_Channel ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ChannelID;
            }
            return 0;
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<Sys_Channel> GetbyParameter(ObjectParameter[] ObjParameterList)
        {
           return PublicDataTools<Sys_Channel>.GetDataByParameter(new Sys_Channel(), ObjParameterList);
        }
    }
}
