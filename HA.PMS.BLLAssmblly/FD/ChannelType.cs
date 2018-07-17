
/**
 Version :HaoAi 1.0
 File Name :ChannelType
 Author:杨洋
 Date:2013.3.15
 Description:渠道类型 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
namespace HA.PMS.BLLAssmblly.FD
{
    public class ChannelType : ICRUDInterface<FD_ChannelType>
    {

        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 删除渠道类型
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_ChannelType ObjectT)
        {
            if (ObjectT != null)
            {
                FD_ChannelType objChannelType = GetByID(ObjectT.ChannelTypeId);

                objChannelType.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 获取渠道类型
        /// </summary>
        /// <returns></returns>
        
        public List<FD_ChannelType> GetByAll()
        {
            return ObjEntity.FD_ChannelType.ToList();
        }
        /// <summary>
        /// 根据渠道类型ID渠道类型
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_ChannelType GetByID(int? KeyID)
        {
            return ObjEntity.FD_ChannelType.FirstOrDefault(C => C.ChannelTypeId == KeyID);
        }

        /// <summary>
        /// 分页获取渠道类型
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_ChannelType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_ChannelType.Count();

            List<FD_ChannelType> resultList = ObjEntity.FD_ChannelType.Where(C=>C.IsDelete==false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ChannelTypeId)
                   .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_ChannelType>();
            }
            return resultList;
        }
        /// <summary>
        /// 添加录入渠道类型信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_ChannelType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_ChannelType.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ChannelTypeId;
                }

            }
            return 0;
        }
        /// <summary>
        /// 修改渠道类型信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_ChannelType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ChannelTypeId;
            }
            return 0;
        }

        /// <summary>
        /// 指示指定渠道名称是否存在。
        /// </summary>
        /// <param name="channelTypeName"></param>
        /// <returns></returns>
        public bool IsChannelTypeNameExist(string channelTypeName)
        {
            return ObjEntity.FD_ChannelType.Count(C => C.ChannelTypeName == channelTypeName) > 0;
        }
    }
}
