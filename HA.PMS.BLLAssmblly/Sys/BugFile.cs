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
    public class BugFile : ICRUDInterface<sys_BugFile>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        #region 删除
        /// <summary>
        /// 改变删除状态   true/false
        /// </summary>
        public int Delete(sys_BugFile ObjectT)
        {
            if (ObjectT != null)
            {
                sys_BugFile BugSystemModel = GetByID(ObjectT.FileID);
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
        public int Deletes(sys_BugFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_BugFile.Remove(GetByID(ObjectT.FileID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        public List<sys_BugFile> GetByAll()
        {
            return ObjEntity.sys_BugFile.ToList();
        }

        #region 根据ID获取图片
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public sys_BugFile GetByID(int? KeyID)
        {
            return ObjEntity.sys_BugFile.FirstOrDefault(C => C.FileID == KeyID);
        }
        #endregion



        #region 根据ID获取图片
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public List<sys_BugFile> GetByBugID(int? KeyID)
        {
            return ObjEntity.sys_BugFile.Where(C => C.BugID == KeyID).ToList();
        }
        #endregion

        public List<sys_BugFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加图片
        /// <summary>
        /// 新增
        /// </summary>
        public int Insert(sys_BugFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_BugFile.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改图片
        /// <summary>
        /// 修改 Modify
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Update(sys_BugFile ObjectT)
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
        public List<sys_BugFile> GetDataByParameter(List<PMSParameters> pars, string OrderByColumnaName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<sys_BugFile>.GetDataByWhereParameter(pars, OrderByColumnaName, PageSize, PageIndex, out SourceCount);
        }
        #endregion
    }
}
