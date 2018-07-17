using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class WeddingPlanFile : ICRUDInterface<FL_WeddingPlanFile>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Delete(FL_WeddingPlanFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_WeddingPlanFile.Remove(GetByID(ObjectT.WFileid));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_WeddingPlanFile> GetByAll()
        {
            return ObjEntity.FL_WeddingPlanFile.ToList();
        }

        public List<FL_WeddingPlanFile> GetByPlanningID(int PlanningID)
        {
            return ObjEntity.FL_WeddingPlanFile.Where(C => C.PlanningID == PlanningID).ToList();
        }

        public FL_WeddingPlanFile GetByID(int? WFileid)
        {
            return ObjEntity.FL_WeddingPlanFile.Where(C => C.WFileid == WFileid).FirstOrDefault();
        }

        public List<FL_WeddingPlanFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(FL_WeddingPlanFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_WeddingPlanFile.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.WFileid;
                }

            }
            return 0;
        }

        public int Update(FL_WeddingPlanFile ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
