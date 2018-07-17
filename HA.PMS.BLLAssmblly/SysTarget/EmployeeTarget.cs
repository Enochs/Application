using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

///本人的目标

namespace HA.PMS.BLLAssmblly.SysTarget
{
    //public class EmployeeTarget : ICRUDInterface<FL_EmployeeTarget>
    //{
    //    PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
    //    public int Delete(FL_EmployeeTarget ObjectT)
    //    {
    //        throw new NotImplementedException();
    //    }



    //    /// <summary>
    //    /// 获取本人管理的人员目标
    //    /// </summary>
    //    /// <param name="EmployeeKeyList"></param>
    //    /// <returns></returns>
    //    public List<View_EmployeeTarget> GetEmployeeTarget(List<int> EmployeeKeyList)
    //    {
    //        return (from C in ObjEntity.View_EmployeeTarget
    //                where (EmployeeKeyList).Contains(C.EmployeeID)
    //                select C).ToList().OrderBy(C=>C.EmployeeID).ToList();
    //    }



    //    ///// <summary>
    //    ///// 根据部门ID组
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public List<Sys_Employee> GetByDepartmetnKeysList(int[] ObjKeyList)
    //    //{
    //    //    return (from C in ObjEntity.Sys_Employee
    //    //            where (ObjKeyList).Contains(C.DepartmentID)
    //    //            select C).ToList();
    //    //}

    //    public List<FL_EmployeeTarget> GetByAll()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public FL_EmployeeTarget GetByID(int? KeyID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public List<FL_EmployeeTarget> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Insert(FL_EmployeeTarget ObjectT)
    //    {
    //        ObjEntity.FL_EmployeeTarget.Add(ObjectT);
    //        return ObjectT.EmployeeID;
    //    }

    //    public int Update(FL_EmployeeTarget ObjectT)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
