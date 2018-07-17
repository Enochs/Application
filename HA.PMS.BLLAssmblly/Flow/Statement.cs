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
    public class Statement : ICRUDInterface<FL_Statement>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(FL_Statement ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Statement.Remove(GetByID(ObjectT.StatementID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion


        public int Delete(int DispatchingId, string name)
        {
            var model = ObjEntity.FL_Statement.FirstOrDefault(C => C.DispatchingID == DispatchingId && C.Name == name);
            if (model != null)
            {
                ObjEntity.FL_Statement.Remove(model);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public int Delete(int CostSumID)
        {
            var model = ObjEntity.FL_Statement.FirstOrDefault(C => C.CostSumId == CostSumID);
            if (model != null)
            {
                ObjEntity.FL_Statement.Remove(model);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_Statement> GetByAll()
        {
            return ObjEntity.FL_Statement.ToList();
        }

        #region 根据ID获取结算表
        /// <summary>
        /// 结算表
        /// </summary>
        public FL_Statement GetByID(int? KeyID)
        {
            return ObjEntity.FL_Statement.FirstOrDefault(C => C.StatementID == KeyID);
        }
        #endregion

        public List<FL_Statement> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 插入新数据
        /// <summary>
        /// 修改
        /// </summary>  
        public int Insert(FL_Statement ObjectT)
        {
            if (ObjectT != null)
            {
                var Model = GetByDispatchingID(ObjectT.DispatchingID.ToString().ToInt32(), ObjectT.Name);       //一个名称只能出现一次
                if (Model == null)          //不存在   就新增
                {
                    if (ObjectT.Name != null)
                    {
                        ObjEntity.FL_Statement.Add(ObjectT);

                        if (ObjEntity.SaveChanges() > 0)
                        {
                            return ObjectT.StatementID;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_Statement ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.StatementID;
            }
            return 0;
        }
        #endregion


        public List<view_CustomerStatement> GetByAllParameter(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int CurrentPage, out int SourceCount)
        {
            return PublicDataTools<view_CustomerStatement>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, CurrentPage, out SourceCount, OrderType.Desc).ToList();
        }


        public List<FL_Statement> GetByDispatchingID(int DispatchingID)
        {
            return ObjEntity.FL_Statement.Where(C => C.DispatchingID == DispatchingID).ToList();
        }

        public FL_Statement GetByDispatchingID(int DispatchingID, string name)
        {
            return ObjEntity.FL_Statement.FirstOrDefault(C => C.DispatchingID == DispatchingID && C.Name == name);
        }

        public List<FL_Statement> GetListByDispatchingID(int DispatchingID, string name)
        {
            return ObjEntity.FL_Statement.Where(C => C.DispatchingID == DispatchingID && C.Name == name).ToList();
        }

        public string GetBySupplierID(int SupplierID, int RowType, DateTime StartDate, DateTime EndDate)
        {
            var DataList = ObjEntity.FL_Statement.Where(C => C.SupplierID == SupplierID && C.RowType == RowType).ToList();
            int count = (from C in DataList join D in ObjEntity.FL_Customers on C.CustomerID equals D.CustomerID where D.PartyDate >= StartDate && D.PartyDate <= EndDate select C).ToList().Count;
            return count.ToString();
        }

        //获取唯一的供应商 算去一个月的收款金额 (一个月  供应商只出现一次  结算表需要)
        public FL_Statement GetByYearDate(int Year, int Monh, int SupplierID)
        {
            return ObjEntity.FL_Statement.FirstOrDefault(C => C.Year == Year && C.Month == Monh && C.SupplierID == SupplierID);
        }


        public FL_Statement GetByCostSumId(int CostSumId)
        {
            return ObjEntity.FL_Statement.FirstOrDefault(C => C.CostSumId == CostSumId);
        }

        #region 获取某个供应商某场的数据
        /// <summary>
        /// 获取供应商某场婚礼数据
        /// </summary>
        public FL_Statement GetByCustomerID(int CustomerID, string SupplierName)
        {
            return ObjEntity.FL_Statement.FirstOrDefault(C => C.CustomerID == CustomerID && C.Name == SupplierName);
        }

        public FL_Statement GetByCustomerID(int CustomerID, string SupplierName, int RowType)
        {
            return ObjEntity.FL_Statement.FirstOrDefault(C => C.CustomerID == CustomerID && C.Name == SupplierName && C.RowType == RowType);
        }
        #endregion


    }
}
