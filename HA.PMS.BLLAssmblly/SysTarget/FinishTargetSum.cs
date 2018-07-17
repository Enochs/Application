using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
//本人完成情况统计

namespace HA.PMS.BLLAssmblly.SysTarget
{
    public class FinishTargetSum : ICRUDInterface<FL_FinishTargetSum>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 根据目标和年份获取指标完成情况
        /// </summary>
        /// <param name="TargetID"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<FL_FinishTargetSum> GetinTargetRanking(int TargetID, int Year)
        {
            var ObjList = (from C in ObjEntity.FL_FinishTargetSum
                           where C.Year == 2013 && C.TargetID == TargetID
                           select C).OrderByDescending(C => C.FinishSum).ToList();
            return ObjList;
        }


        /// <summary>
        /// 根据用户ID组获取
        /// </summary>
        /// <returns></returns>
        public List<FL_FinishTargetSum> GetinEmployeeKeyListNeedCreate(List<int> EmployeeKeyList, int Type)
        {
            var ObjList = (from C in ObjEntity.FL_FinishTargetSum
                           where (EmployeeKeyList).Contains(C.EmployeeID) && C.Year == DateTime.Now.Year && C.IsActive == true
                           select C).ToList();
            if (ObjList.Count > 0)
            {

                //Target ObjtargetBLL = new Target();

                //List<FL_FinishTargetSum> Objouter = new List<FL_FinishTargetSum>();
                //var ObjTargetList = ObjtargetBLL.GetEmployeeTarget(EmployeeKeyList, out Objouter, Type);


                //foreach (var ObjTargetModel in ObjTargetList)
                //{
                //    if (ObjList.FirstOrDefault(A => A.TargetID == ObjTargetModel.TargetID) == null)
                //    {
                //        ObjList.Add(new FL_FinishTargetSum() { TargetID = ObjTargetModel.TargetID, TargetTitle = ObjTargetModel.TargetTitle, Year = DateTime.Now.Year, IsActive = false });
                //    }
                //}

                return ObjList;
            }
            else
            {
                return new List<FL_FinishTargetSum>();
                //Target ObjtargetBLL = new Target();

                //List<FL_FinishTargetSum> Objouter = new List<FL_FinishTargetSum>();
                //var ObjTargetList = ObjtargetBLL.GetEmployeeTarget(EmployeeKeyList, out Objouter, Type);

                //return Objouter.OrderBy(C => C.EmployeeID).ToList();
            }
        }


        public void SetNoneByEmployeeID(int EmployeeID)
        {
            var ObjModelList = ObjEntity.FL_FinishTargetSum.Where(C => C.EmployeeID == EmployeeID).ToList();
            foreach (var ObjItem in ObjModelList)
            {
                ObjItem.IsActive = false;
                ObjEntity.SaveChanges();
            }
        }

        public void SetActiveByID(int TargetID)
        {

        }
        /// <summary>
        /// 根据用户ID获取本人目标
        /// </summary>
        /// <returns></returns>
        public List<FL_FinishTargetSum> GetinEmployeeKeyList(List<int> EmployeeKeyList, int Type)
        {
            var ObjList = (from C in ObjEntity.FL_FinishTargetSum
                           where (EmployeeKeyList).Contains(C.EmployeeID) && C.Year == DateTime.Now.Year
                           select C).ToList();


            if (ObjList.Count > 0)
            {

                //Target ObjtargetBLL = new Target();

                //List<FL_FinishTargetSum> Objouter = new List<FL_FinishTargetSum>();
                //var ObjTargetList = ObjtargetBLL.GetEmployeeTarget(EmployeeKeyList, out Objouter, Type);


                //foreach (var ObjTargetModel in ObjTargetList)
                //{
                //    if (ObjList.FirstOrDefault(A=>A.TargetID==ObjTargetModel.TargetID) == null)
                //    {
                //        ObjList.Add(new FL_FinishTargetSum() { TargetID=ObjTargetModel.TargetID,TargetTitle=ObjTargetModel.TargetTitle,Year=DateTime.Now.Year,IsActive=false});
                //    }
                //}

                return ObjList;
            }
            else
            {
                Target ObjtargetBLL = new Target();

                List<FL_FinishTargetSum> Objouter = new List<FL_FinishTargetSum>();
                var ObjTargetList = ObjtargetBLL.GetEmployeeTarget(EmployeeKeyList, out Objouter, Type);

                return Objouter.OrderBy(C => C.TargetID).ToList();
            }
        }

        public List<View_DepartmentTarget> GetDepartmentTarget(string DepartmentKeyList, string Getwhere)
        {
            return ObjEntity.GetDepartmentTarget(DepartmentKeyList, Getwhere).ToList();
        }

        /// <summary>
        /// 获取本人的目标汇总
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<View_DepartmentTarget> GetEmployeetargetbyID(string EmpLoyeeID, string Getwhere)
        {
            return ObjEntity.GetEmployeetargetbyID(EmpLoyeeID, Getwhere).ToList();
        }



        ///// <summary>
        ///// 根据部门ID组
        ///// </summary>
        ///// <returns></returns>
        //public List<Sys_Employee> GetByDepartmetnKeysList(int[] ObjKeyList)
        //{
        //    return (from C in ObjEntity.Sys_Employee
        //            where (ObjKeyList).Contains(C.DepartmentID)
        //            select C).ToList();
        //}

        public int Delete(FL_FinishTargetSum ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_FinishTargetSum> GetByAll()
        {
            return ObjEntity.FL_FinishTargetSum.ToList();
        }


        /// <summary>
        /// 根据ID单条获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_FinishTargetSum GetByID(int? KeyID)
        {
            return ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.FinishKey == KeyID);
        }

        public List<FL_FinishTargetSum> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询某员工 某年 现金流的实体
        /// </summary>
        /// <returns></returns>
        public FL_FinishTargetSum GetByEmployeeIDTitle(int? EmployeeID, int Year, string TargetTitle)
        {
            if (!string.IsNullOrEmpty(TargetTitle))         //标题不为空
            {
                return ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.EmployeeID == EmployeeID && C.Year == Year && C.TargetTitle == TargetTitle);
            }
            else
            {
                return ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.EmployeeID == EmployeeID && C.Year == Year);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_FinishTargetSum ObjectT)
        {
            ObjEntity.FL_FinishTargetSum.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.FinishKey;
        }

        public int Update(FL_FinishTargetSum ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.FinishKey;
        }


        public FL_FinishTargetSum GetFinishTargetSums(int EmployeeId, string KeyList)
        {
            var departmentIDParameter = KeyList != null ?
                new ObjectParameter("DepartmentID", KeyList) :
                new ObjectParameter("DepartmentID", typeof(string));
            return ObjEntity.FL_FinishTargetSum.Where(C => C.Year == DateTime.Now.Year && C.UpdateTime.Month == DateTime.Now.Month && C.IsActive == true).FirstOrDefault();
        }


        public List<FL_FinishTargetSum> GetByActiveOrEmployeeID(string GetWhere)
        {
            List<FL_FinishTargetSum> list = new List<FL_FinishTargetSum>();
            return list;
        }


        public List<FL_FinishTargetSum> GetDataByWhereParameter(List<PMSParameters> Parm, string OrderByColumnName, int Currentpage, int PageIndex, out int SoruceCount, OrderType order)
        {
            return PublicDataTools<FL_FinishTargetSum>.GetDataByWhereParameter(Parm, OrderByColumnName, Currentpage, PageIndex, out SoruceCount, order);
        }


        public FL_FinishTargetSum GetByEmployeeID(int EmployeeID)
        {
            return ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.EmployeeID == EmployeeID);
        }


        public List<FL_FinishTargetSum> GetListByEmployeeID(int EmployeeID)
        {
            return ObjEntity.FL_FinishTargetSum.Where(C => C.EmployeeID == EmployeeID).ToList();
        }

    }
}
