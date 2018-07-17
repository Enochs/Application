using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.CA
{
    public class Distrinct : ICRUDInterface<Distrinct>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        public int Delete(Distrinct ObjectT)
        {
            throw new NotImplementedException();
        }
        #endregion

        public List<Distrinct> GetByAll()
        {
            throw new NotImplementedException();
        }

        public Distrinct GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public List<Distrinct> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(Distrinct ObjectT)
        {
            throw new NotImplementedException();
        }

        public int Update(Distrinct ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<CA_Distrinct> GetDataByParameter(List<PMSParameters> pars,string OrderByColumnName,int PageSize,int PageIndex,out int SourceCount,OrderType Order)
        {
            return PublicDataTools<CA_Distrinct>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, PageIndex, out SourceCount, Order);
        }
    }
}
