using HA.PMS.BLLInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class HandleLog : ICRUDInterface<sys_HandleLog>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除操作日志
        /// </summary> 
        public int Delete(sys_HandleLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_HandleLog.Remove(GetByID(ObjectT.HandleID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        public List<sys_HandleLog> GetByAll()
        {
            throw new NotImplementedException();
        }

        #region 根据ID获取实体类
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public sys_HandleLog GetByID(int? KeyID)
        {
            return ObjEntity.sys_HandleLog.FirstOrDefault(C => C.HandleID == KeyID);
        }
        #endregion

        public List<sys_HandleLog> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增  添加
        /// <summary>
        /// 添加
        /// </summary>
        public int Insert(sys_HandleLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_HandleLog.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        public int Update(sys_HandleLog ObjectT)
        {
            throw new NotImplementedException();
        }

        #region 分页获取列表
        /// <summary>
        /// 分页获取列表
        /// </summary>
        public List<sys_HandleLog> GetDataByParameter(List<PMSParameters> pars, string OrderByName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicTools.PublicDataTools<sys_HandleLog>.GetDataByWhereParameter(pars, OrderByName, PageSize, PageIndex, out SourceCount);
        }
        #endregion

    }
}
