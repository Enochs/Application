using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class CostSum : ICRUDInterface<FL_CostSum>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_CostSum ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_CostSum.Remove(GetByID(ObjectT.CostSumId));
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
        public List<FL_CostSum> GetByAll()
        {
            return ObjEntity.FL_CostSum.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_CostSum GetByID(int? KeyID)
        {
            return ObjEntity.FL_CostSum.Where(C => C.CostSumId == KeyID).FirstOrDefault();
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
        public List<FL_CostSum> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
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
        public int Insert(FL_CostSum ObjectT)
        {
            if (ObjectT != null)
            {
                var Model = GetByCheckID(ObjectT.Name, ObjectT.DispatchingID.ToString().ToInt32(), ObjectT.RowType.ToString().ToInt32());
                if (Model == null)          //不存在  就新增
                {
                    ObjEntity.FL_CostSum.Add(ObjectT);
                    if (ObjEntity.SaveChanges() > 0)
                    {
                        return ObjectT.CostSumId;
                    }
                }
                else
                {
                    ObjEntity.SaveChanges();
                    return ObjectT.CostSumId;
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
        public int Update(FL_CostSum ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.CostSumId;
            }
            return 0;
        }
        #endregion

        #region 根据DispachingId  Name查找
        public FL_CostSum GetByCheckID(string name, int DispatchingId, int RowType = 0)
        {
            if (RowType == 0)
            {
                return ObjEntity.FL_CostSum.Where(C => C.Name == name && C.DispatchingID == DispatchingId).FirstOrDefault();
            }
            else
            {
                return ObjEntity.FL_CostSum.Where(C => C.Name == name && C.DispatchingID == DispatchingId && C.RowType == RowType).FirstOrDefault();
            }
        }

        //判断是否存在  DesignList.aspx需要使用
        public List<FL_CostSum> GetByChecks(string name, int DispatchingId, int RowType = 0)
        {
            if (RowType == 0)
            {
                return ObjEntity.FL_CostSum.Where(C => C.Name == name && C.DispatchingID == DispatchingId).ToList();
            }
            else
            {
                return ObjEntity.FL_CostSum.Where(C => C.Name == name && C.DispatchingID == DispatchingId && C.RowType == RowType).ToList();
            }
        }
        #endregion

        #region 根据Name查找
        public FL_CostSum GetByName(string name)
        {
            return ObjEntity.FL_CostSum.Where(C => C.Name == name).FirstOrDefault();
        }
        #endregion

        #region 根据DispachingId查找
        public List<FL_CostSum> GetByDispatchingID(int DispatchingId)
        {
            return ObjEntity.FL_CostSum.Where(C => C.DispatchingID == DispatchingId).ToList();
        }
        #endregion

        #region 根据DispachingId  Name查找
        public FL_CostSum GetByDispatchingIDName(int DispatchingId, string Name)
        {
            return ObjEntity.FL_CostSum.FirstOrDefault(C => C.DispatchingID == DispatchingId && C.Name == Name);
        }

        //特殊情况 出错 可能出现两个
        public List<FL_CostSum> GetByDispatchingIDNames(int DispatchingId, string Name)
        {
            return ObjEntity.FL_CostSum.Where(C => C.DispatchingID == DispatchingId && C.Name == Name).ToList();
        }
        #endregion

        #region 根据CustomerID查找
        public List<FL_CostSum> GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_CostSum.Where(C => C.CustomerId == CustomerID).ToList();
        }

        public List<FL_CostSum> GetByCustomerIDType(int CustomerID, int RowType)
        {
            return ObjEntity.FL_CostSum.Where(C => C.CustomerId == CustomerID && C.RowType == RowType).ToList();
        }
        #endregion

        #region 根据DispachingId RowType组合查找
        public List<FL_CostSum> GetByDispatchingID(int DispatchingId, int RowType)
        {
            return ObjEntity.FL_CostSum.Where(C => C.DispatchingID == DispatchingId && C.RowType == RowType).ToList();
        }
        #endregion

        #region 根据条件 分页查找
        /// <summary>
        /// 条件  分页查找
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="OrderColumneName"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_CostSum> GetAllByparameter(List<PMSParameters> parms, string OrderColumnName, int PageSize, int PageIndex, ref int SourceCount)
        {

            return PublicDataTools<FL_CostSum>.GetDataByWhereParameter(parms, OrderColumnName, PageSize, PageIndex, out SourceCount);
        }
        #endregion

        #region 根据年份 月份来获取
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Year"></param>
        public List<FL_CostSum> GetByMonths(int Year, int Month)
        {
            return ObjEntity.FL_CostSum.Where(C => C.CreateDate.Year == Year && C.CreateDate.Month == Month).ToList();
        }
        #endregion

        #region 根据时间 EmployeeID来获取 总成本
        public string GetCostSumByDate(DateTime Start, DateTime End, int EmployeeID)
        {
            return (from C in ObjEntity.FL_CostSum
                    join D in ObjEntity.View_CustomerQuoted
                        on C.QuotedID equals D.QuotedID
                    where D.PartyDate >= Start && D.PartyDate <= End
                        && D.EmpLoyeeID == EmployeeID && C.Sumtotal != null
                    select C).Sum(C => C.Sumtotal).ToString();
        }
        #endregion

        #region 获取所有成本  View_CustomerQuoted

        public string GetAllCostSum(List<View_CustomerQuoted> DataList)
        {
            var List = (from C in DataList join D in ObjEntity.FL_CostSum on C.CustomerID equals D.CustomerId select D).ToList();
            return List.Sum(C => C.Sumtotal).ToString();
        }
        #endregion

        #region 根据CustomerID 以及供应商 名称来查询
        /// <summary>
        /// 查找
        /// </summary>
        public FL_CostSum GetByCustomerID(int CustomerID, string SupplierName, int RowType)
        {
            return ObjEntity.FL_CostSum.FirstOrDefault(C => C.CustomerId == CustomerID && C.Name == SupplierName && C.RowType == RowType);
        }
        #endregion

        public List<GetDesignCostProc_Result> GetCostSumForDesigner(int CustomerID, string GetWhere)
        {
            return ObjEntity.GetDesignCostProc(CustomerID, GetWhere).ToList();
        }


        public string GetCostSum(DateTime Start, DateTime End, int Type)     //Type 1.人员   2.物料   3.其他
        {
            if (Type == 1)
            {
                return (from D in ObjEntity.FL_CostSum
                        join C in ObjEntity.View_SSCustomer on D.CustomerId equals C.CustomerID
                        where C.Partydate >= Start && C.Partydate <= End && (D.RowType == 4 || D.RowType == 5 || D.RowType == 7)
                        select D).Sum(D => D.ActualSumTotal).ToString();
            }
            else if (Type == 2)
            {
                return (from D in ObjEntity.FL_CostSum
                        join C in ObjEntity.View_SSCustomer on D.CustomerId equals C.CustomerID
                        where C.Partydate >= Start && C.Partydate <= End && (D.RowType == 1 || D.RowType == 2 || D.RowType == 3 || D.RowType == 6 || D.RowType == 8 || D.RowType == 10)
                        select D).Sum(D => D.ActualSumTotal).ToString();
            }
            else if (Type == 3)
            {
                return (from D in ObjEntity.FL_CostSum
                        join C in ObjEntity.View_SSCustomer on D.CustomerId equals C.CustomerID
                        where C.Partydate >= Start && C.Partydate <= End && (D.RowType == 9 || D.RowType == 11)
                        select D).Sum(D => D.ActualSumTotal).ToString();
            }
            return "";
        }
    }
}
