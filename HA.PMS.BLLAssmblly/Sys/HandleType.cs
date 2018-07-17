using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class HandleType : ICRUDInterface<sys_HandleType>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(sys_HandleType ObjectT)
        {
            throw new NotImplementedException();
        }

        #region 获取所有类型
        /// <summary>
        /// 获取所有
        /// </summary> 
        public List<sys_HandleType> GetByAll()
        {
            return ObjEntity.sys_HandleType.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取所有
        /// </summary>       
        public sys_HandleType GetByID(int? KeyID)
        {
            return ObjEntity.sys_HandleType.FirstOrDefault(C => C.TypeID == KeyID);
        }
        #endregion

        public List<sys_HandleType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>

        public int Insert(sys_HandleType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_HandleType.Add(ObjectT);
                return ObjEntity.SaveChanges();
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

        public int Update(sys_HandleType ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion
    }
}
