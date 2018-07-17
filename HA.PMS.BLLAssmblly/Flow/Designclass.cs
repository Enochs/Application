using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Designclass : ICRUDInterface<FL_Designclass>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_Designclass ObjectT)
        {
            ObjEntity.FL_Designclass.Remove(ObjectT);
            return ObjEntity.SaveChanges();
        }

        public List<FL_Designclass> GetByAll()
        {
            return ObjEntity.FL_Designclass.ToList();
        }

        public List<FL_Designclass> GetByCustomerId(int CustomerID)
        {
            //return ObjEntity.FL_Designclass.Where(C => C.CustomerID == CustomerID && C.State == 2).ToList();
            return ObjEntity.FL_Designclass.Where(C => C.CustomerID == CustomerID).ToList();
        }



        /// <summary>
        /// 根据订单获取花艺数据
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_Designclass> GetByQuotedID(int QuotedID)
        {
            return ObjEntity.FL_Designclass.Where(C => C.QuotedID == QuotedID && (C.State == null || C.State == 0)).ToList();
        }

        /// <summary>
        /// 根据订单获取花艺数据
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_Designclass> GetByQuotedIDs(int QuotedID)
        {
            return ObjEntity.FL_Designclass.Where(C => C.QuotedID == QuotedID).ToList();
        }

        /// <summary>
        /// 根据订单获取
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_Designclass> GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Designclass.Where(C => C.CustomerID == CustomerID).ToList();
        }


        public List<FL_Designclass> GetBySupplierID(int CustomerID, string SupplierID)
        {
            return ObjEntity.FL_Designclass.Where(C => C.CustomerID == CustomerID && C.Supplier == SupplierID).ToList();
        }


        public FL_Designclass GetByID(int? KeyID)
        {
            return ObjEntity.FL_Designclass.FirstOrDefault(C => C.DesignclassID == KeyID);
        }

        public List<FL_Designclass> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        public int Insert(FL_Designclass ObjectT)
        {
            ObjEntity.FL_Designclass.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }

        public int Update(FL_Designclass ObjectT)
        {
            return ObjEntity.SaveChanges();
        }

        #region 查询清单明细
        /// <summary>
        /// 传入条件查询
        /// </summary>
        public List<View_DesignerCustomer> GetAllByParameter(List<PMSParameters> parms, string OrderColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_DesignerCustomer>.GetDataByWhereParameter(parms, OrderColumnName, PageSize, PageIndex, out SourceCount);
        }
        #endregion
    }
}

