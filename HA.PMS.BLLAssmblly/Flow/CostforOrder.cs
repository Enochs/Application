using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class CostforOrder : ICRUDInterface<FL_CostforOrder>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        CostSum ObjCostSumBLl = new CostSum();
        public int Delete(FL_CostforOrder ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_CostforOrder.Remove(GetByID(ObjectT.CostID));
            return ObjEntity.SaveChanges();
        }
            return 0;
        }

        public List<FL_CostforOrder> GetByAll()
        {
            return ObjEntity.FL_CostforOrder.ToList();
        }

        public List<FL_CostforOrder> GetByOrderID(int? OrderID)
        {
            return ObjEntity.FL_CostforOrder.Where(C => C.OrderID == OrderID).ToList();
        }

        /// <summary>
        /// 根据行类型获取数据
        /// </summary>
        /// <param name="Rowtype"></param>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_CostforOrder> GetByRowType(int Rowtype, int DispatchingID)
        {
            return ObjEntity.FL_CostforOrder.Where(C => C.RowType == Rowtype && C.DispatchingID == DispatchingID).ToList();

        }
        /// <summary>
        /// 根据行类型获取数据
        /// </summary>
        /// <param name="Rowtype"></param>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_CostforOrder> GetByRowTypes(int Rowtype, int DispatchingID, string SupplierName, string CategoryName)
        {

            return ObjEntity.FL_CostforOrder.Where(C => C.RowType == Rowtype && C.DispatchingID == DispatchingID && C.Name == SupplierName && C.Node == CategoryName).ToList();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_CostforOrder> GetByDispatchingID(int? DispatchingID)
        {
            return ObjEntity.FL_CostforOrder.Where(C => C.DispatchingID == DispatchingID).ToList();
        }
        /// <summary>
        /// 根据供应商ID
        /// </summary>
        /// <returns></returns>
        public List<FL_CostforOrder> GetBySupplierIdAll(int supplierId)
        {
            //return ObjEntity.FL_CostforOrder.Where(C => C.SupplierID == supplierId).ToList();
            return new List<FL_CostforOrder>();
        }

        public FL_CostforOrder GetByID(int? KeyID)
        {
            return ObjEntity.FL_CostforOrder.FirstOrDefault(C => C.CostID == KeyID);
        }

        ///// <summary>
        ///// 根据订单号获取
        ///// </summary>
        ///// <param name="OrderID"></param>
        ///// <returns></returns>
        //public FL_CostforOrder GetByOrderID(int? OrderID)
        //{
        //    return ObjEntity.FL_CostforOrder.FirstOrDefault(C=>C.OrderID==OrderID);
        //}

        public List<FL_CostforOrder> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_CostforOrder ObjectT)
        {
            //var ExistModel = ObjEntity.FL_CostforOrder.FirstOrDefault(C => C.DispatchingID == ObjectT.DispatchingID && C.Name == ObjectT.Name);
            //if (ExistModel == null)
            //{
            //    ObjectT.CreateDate = DateTime.Now;

            //    ObjEntity.FL_CostforOrder.Add(ObjectT);
            //    return ObjEntity.SaveChanges();

            //}
            //else
            //{

            //    ExistModel.PlanCost = ObjectT.PlanCost;
            //    ExistModel.FinishCost = ObjectT.FinishCost;
            //    return ObjEntity.SaveChanges();

            //}
            if (ObjectT != null)
            {
                ObjEntity.FL_CostforOrder.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.CostID;
                }
            }
            return 0;

        }
        public void DeleteforNull(string[] NameList, int RowType)
        {
            var DeletetList = (from C in ObjEntity.FL_CostforOrder
                               where !NameList.Contains(C.Name) && C.RowType == RowType
                               select C
                                 ).ToList();
            foreach (var Objitem in DeletetList)
            {
                this.Delete(Objitem);

            }

        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int InsertNoneUpdate(List<FL_CostforOrder> ObjectT, int DispatchingID, int RowType)
        {
            var ExistModel = ObjEntity.FL_CostforOrder.Where(C => C.DispatchingID == DispatchingID && C.RowType == RowType).ToList();
            foreach (var Deletemodel in ExistModel)
            {
                Delete(Deletemodel);
            }
            int i = 0;
            foreach (var InsertModel in ObjectT)
            {
                ObjEntity.FL_CostforOrder.Add(InsertModel);
                i = ObjEntity.SaveChanges();

            }
            return i;


        }
        public int Update(FL_CostforOrder ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.CostID;
        }

        /// <summary>
        /// 根据行类型获取数据
        /// </summary>
        /// <param name="Rowtype"></param>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_CostforOrder> GetByName(string Name, int DispatchingID)
        {
            return ObjEntity.FL_CostforOrder.Where(C => C.Node == Name && C.DispatchingID == DispatchingID).ToList();
        }
        
        //获取总成本
        public List<CostSumTs> GetCostSums(int DispatchingID)
        {
            return ObjEntity.GetCostSum(DispatchingID).ToList();
        }
    }
}
