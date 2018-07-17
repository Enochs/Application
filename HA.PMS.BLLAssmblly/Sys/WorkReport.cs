using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class WorkReport : ICRUDInterface<sys_WorkReport>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除功能
        /// <summary>
        /// 删除
        /// </summary>   
        public int Delete(sys_WorkReport ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_WorkReport.Remove(GetByID(ObjectT.WorkID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有
        /// <summary>
        /// 获取
        /// </summary> 
        public List<View_WorkReport> GetByAll()
        {
            return ObjEntity.View_WorkReport.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>

        public sys_WorkReport GetByID(int? KeyID)
        {
            return ObjEntity.sys_WorkReport.FirstOrDefault(C => C.WorkID == KeyID);
        }
        #endregion

        public List<View_WorkReport> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加数据
        /// <summary>
        /// 添加
        /// </summary>
        public int Insert(sys_WorkReport ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_WorkReport.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(sys_WorkReport ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion

        #region 分页获取数据
        /// <summary>
        /// 分页获取
        /// </summary>
        public List<View_WorkReport> GetDataByParameters(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_WorkReport>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, PageIndex, out SourceCount);
        }
        #endregion

        #region 根据时间获取
        /// <summary>
        /// 根据年  月  日 获取
        /// </summary>
        public View_WorkReport GetByTimes(int EmployeeID, int Year, int Month, int Day)
        {
            return ObjEntity.View_WorkReport.FirstOrDefault(C => C.EmployeeID == EmployeeID && C.Year == Year && C.Month == Month && C.Day == Day);
        }

        public sys_WorkReport GetEntityByTimes(int EmployeeID, int Year, int Month, int Day)
        {
            return ObjEntity.sys_WorkReport.FirstOrDefault(C => C.EmployeeID == EmployeeID && C.Year == Year && C.Month == Month && C.Day == Day);
        }
        #endregion

        //获取员工某天的日报表信息
        public View_WorkReport GetByTimeCustomerID(int EmployeeID, DateTime CreateDate)
        {
            return ObjEntity.View_WorkReport.Where(C => C.EmployeeID == EmployeeID && C.CreateDate == CreateDate).FirstOrDefault();
        }

        //获取员工某天的日报表信息
        public sys_WorkReport GetEntityByTimeCustomerID(int EmployeeID, DateTime CreateDate)
        {
            return ObjEntity.sys_WorkReport.Where(C => C.EmployeeID == EmployeeID && C.CreateDate == CreateDate).FirstOrDefault();
        }


        List<sys_WorkReport> ICRUDInterface<sys_WorkReport>.GetByAll()
        {
            throw new NotImplementedException();
        }

        List<sys_WorkReport> ICRUDInterface<sys_WorkReport>.GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
    }
}
