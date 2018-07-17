using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Sys
{

    public class EmployeeDataFile : ICRUDInterface<sys_EmployeeDataFile>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除功能
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(sys_EmployeeDataFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_EmployeeDataFile.Remove(GetByID(ObjectT.DataId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有
        /// <summary>
        /// 获取所有
        /// </summary>
        public List<sys_EmployeeDataFile> GetByAll()
        {
            return ObjEntity.sys_EmployeeDataFile.ToList();
        }
        #endregion

        #region 根据ID获取档案资料
        /// <summary>
        /// 获取资料
        /// </summary>
        public sys_EmployeeDataFile GetByID(int? KeyID)
        {
            return ObjEntity.sys_EmployeeDataFile.FirstOrDefault(C => C.DataId == KeyID);
        }
        #endregion

        public List<sys_EmployeeDataFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加新项
        /// <summary>
        /// 添加
        /// </summary>
        public int Insert(sys_EmployeeDataFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_EmployeeDataFile.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DataId;
                }
            }
            return 0;
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改
        /// </summary> 
        public int Update(sys_EmployeeDataFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.DataId;
            }
            return 0;
        }
        #endregion


        #region 根据EmployeeID 获取档案
        public List<sys_EmployeeDataFile> GetByEmployeeID(int? EmployeeID)
        {
            return ObjEntity.sys_EmployeeDataFile.Where(C => C.EmployeeID == EmployeeID).ToList();
        }
        #endregion
    }
}
