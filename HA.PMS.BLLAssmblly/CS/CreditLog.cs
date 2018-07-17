using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.CS
{
    public class CreditLog : ICRUDInterface<CS_CreditLog>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 获取单人积分
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public decimal GetPointforCustomer(int CustomerID)
        {
            try
            {
                return ObjEntity.CS_CreditLog.Where(C => C.CustomerID == CustomerID).Sum(C => C.Point);
            }
            catch
            {
                return 0;
            }
        }


        public List<CS_CreditLog> GetByCustomerID(int CustomerID)
        {
            return ObjEntity.CS_CreditLog.Where(C => C.CustomerID == CustomerID).ToList();
        }
        public int Delete(CS_CreditLog ObjectT)
        {
            throw new System.NotImplementedException();
        }

        public List<CS_CreditLog> GetByAll()
        {
            throw new System.NotImplementedException();
        }

        public CS_CreditLog GetByID(int? KeyID)
        {
            throw new System.NotImplementedException();
        }

        public List<CS_CreditLog> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(CS_CreditLog ObjectT)
        {
            ObjEntity.CS_CreditLog.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }

        public int Update(CS_CreditLog ObjectT)
        {
            throw new System.NotImplementedException();
        }
    }
}
