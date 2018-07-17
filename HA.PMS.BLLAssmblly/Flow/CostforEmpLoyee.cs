using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class CostforEmpLoyee : ICRUDInterface<FL_CostforEmpLoyee>
    {
        PMS_WeddingEntities Objentity = new PMS_WeddingEntities();
        public int Delete(FL_CostforEmpLoyee ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_CostforEmpLoyee> GetByAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据订单ID获取数据
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_CostforEmpLoyee> GetByOrderID(int? OrderID)
        {
           return Objentity.FL_CostforEmpLoyee.Where(C=>C.OrderID==OrderID).ToList();
        }

        public FL_CostforEmpLoyee GetByID(int? KeyID)
        {
            return Objentity.FL_CostforEmpLoyee.FirstOrDefault(C => C.Costkeys == KeyID);
        }

        public List<FL_CostforEmpLoyee> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(FL_CostforEmpLoyee ObjectT)
        {
            Objentity.FL_CostforEmpLoyee.Add(ObjectT);

            Objentity.SaveChanges();
            return ObjectT.Costkeys;
        }

        public int Update(FL_CostforEmpLoyee ObjectT)
        {
            Objentity.SaveChanges();
            return ObjectT.Costkeys;
        }
    }
}
