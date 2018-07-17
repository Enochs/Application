using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.FinancialAffairsbll
{
    public class OrderEarnestMoney:ICRUDInterface<FL_OrderEarnestMoney>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderEarnestMoney ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderEarnestMoney> GetByAll()
        {
            throw new NotImplementedException();
        }

        public FL_OrderEarnestMoney GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public FL_OrderEarnestMoney GetByOrderID(int? KeyID)
        {
            return ObjEntity.FL_OrderEarnestMoney.FirstOrDefault(C=>C.OrderID==KeyID);
        }

        public List<FL_OrderEarnestMoney> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderEarnestMoney ObjectT)
        {
            ObjEntity.FL_OrderEarnestMoney.Add(ObjectT);

            ObjEntity.SaveChanges();
            return ObjectT.OrderID;
        }

        public int Update(FL_OrderEarnestMoney ObjectT)
        {

            ObjEntity.SaveChanges();
            return ObjectT.OrderID;
        }
    }
}
