using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class LoginLog : ICRUDInterface<sys_LoginLog>
    {
        HA.PMS.DataAssmblly.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(sys_LoginLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_LoginLog.Remove(GetByID(ObjectT.Id));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion
        public List<sys_LoginLog> GetByAll()
        {
            return ObjEntity.sys_LoginLog.ToList();
        }

        #region 根据ID获取登录日志
        /// <summary>
        /// 获取日志
        /// </summary>
        public sys_LoginLog GetByID(int? KeyID)
        {
            return ObjEntity.sys_LoginLog.FirstOrDefault(C => C.Id == KeyID);
        }
        #endregion

        public List<sys_LoginLog> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 添加登录日志
        /// </summary>
        public int Insert(sys_LoginLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_LoginLog.Add(ObjectT);
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
        /// 修改 更新
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(sys_LoginLog ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        public List<sys_LoginLog> GetDataByParameters(List<PMSParameters> pars, string SortName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<sys_LoginLog>.GetDataByWhereParameter(pars, SortName, PageSize, PageIndex, out SourceCount);
        }
    }
}
