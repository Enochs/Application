using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class LookURL : ICRUDInterface<sys_LookURL>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(sys_LookURL ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<sys_LookURL> GetByAll()
        {
            return ObjEntity.sys_LookURL.ToList();
        }

        public sys_LookURL GetByID(int? KeyID)
        {
            return ObjEntity.sys_LookURL.FirstOrDefault(C => C.ID == KeyID);
        }

        public List<sys_LookURL> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(sys_LookURL ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.sys_LookURL.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public int Update(sys_LookURL ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
    }
}
