using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.CS
{
    public class TakeDiskContent : ICRUDInterface<CS_TakeDiskContent>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(CS_TakeDiskContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_TakeDiskContent.Remove(GetByID(ObjectT.Id));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<CS_TakeDiskContent> GetByAll()
        {
            return ObjEntity.CS_TakeDiskContent.ToList();
        }
        #endregion

        #region 获取TakeIDID获取
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<CS_TakeDiskContent> GetByTakeID(int? TakeID)
        {
            return ObjEntity.CS_TakeDiskContent.Where(C => C.TakeID == TakeID).ToList();
        }
        #endregion


        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public CS_TakeDiskContent GetByID(int? KeyID)
        {
            return ObjEntity.CS_TakeDiskContent.Where(C => C.Id == KeyID).FirstOrDefault();
        }
        #endregion

        #region 分页获取
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<CS_TakeDiskContent> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(CS_TakeDiskContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_TakeDiskContent.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.Id;
                }
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(CS_TakeDiskContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.Id;
            }
            return 0;
        }
        #endregion
    }
}
