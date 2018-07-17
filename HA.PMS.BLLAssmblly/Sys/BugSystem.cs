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
    public class BugSystem : ICRUDInterface<sys_BugSystem>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 改变删除状态   true/false
        /// </summary>
        public int Delete(sys_BugSystem ObjectT)
        {
            if (ObjectT != null)
            {
                sys_BugSystem BugSystemModel = GetByID(ObjectT.BugID);
                BugSystemModel.IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 删除 真正的删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Deletes(sys_BugSystem ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_BugSystem.Remove(GetByID(ObjectT.BugID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        public List<sys_BugSystem> GetByAll()
        {
            return ObjEntity.sys_BugSystem.ToList();
        }

        #region 根据ID获取Bug
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public sys_BugSystem GetByID(int? KeyID)
        {
            return ObjEntity.sys_BugSystem.FirstOrDefault(C => C.BugID == KeyID);
        }
        #endregion

        public List<sys_BugSystem> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加Bug
        /// <summary>
        /// 新增
        /// </summary>
        public int Insert(sys_BugSystem ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_BugSystem.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改Bug
        /// <summary>
        /// 修改 Modify
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Update(sys_BugSystem ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;

        }
        #endregion

        #region 分页获取Bug
        /// <summary>
        /// 分页获取
        /// </summary>
        public List<sys_BugSystem> GetDataByParameter(List<PMSParameters> pars, string OrderByColumnaName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<sys_BugSystem>.GetDataByWhereParameter(pars, OrderByColumnaName, PageSize, PageIndex, out SourceCount);
        }
        #endregion
    }
}
