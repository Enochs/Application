using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
namespace HA.PMS.BLLAssmblly.CS
{

    public class Credit : ICRUDInterface<CS_Credit>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(DataAssmblly.CS_Credit ObjectT)
        {
            throw new System.NotImplementedException();
        }

        public List<DataAssmblly.CS_Credit> GetByAll()
        {

            throw new System.NotImplementedException();
        }


        /// <summary>
        /// 获取需要积分的用户（成功预定的用户）
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Customers> GetCreditCustomer(int PageSize, int PageIndex, out int SourceCount)
        {
            PageIndex = PageIndex--;
            SourceCount = ObjEntity.FL_QuotedPrice.Count();
            var Query = (from C in ObjEntity.FL_Customers
                         join D in ObjEntity.CS_Credit
                              on C.CustomerID equals D.CustomerID
                         select C
          ).Skip(PageSize * PageIndex).Take(PageSize).ToList();

            return Query;
        }

        public DataAssmblly.CS_Credit GetByID(int? KeyID)
        {
            throw new System.NotImplementedException();
        }

        public List<DataAssmblly.CS_Credit> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(DataAssmblly.CS_Credit ObjectT)
        {
            throw new System.NotImplementedException();
        }

        public int Update(DataAssmblly.CS_Credit ObjectT)
        {
            throw new System.NotImplementedException();
        }
    }
}
