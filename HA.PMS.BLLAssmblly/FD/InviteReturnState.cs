using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.FD
{
    public class InviteReturnState : ICRUDInterface<FD_InviteReturnState>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_InviteReturnState ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.FD_InviteReturnState.Remove(GetByID(ObjectT.InviteStateID));
                return ObjEntity.SaveChanges();
            }
            return 0;

        }

        public List<FD_InviteReturnState> GetByAll()
        {
            return ObjEntity.FD_InviteReturnState.ToList();
        }

        public FD_InviteReturnState GetByID(int? KeyID)
        {
            return ObjEntity.FD_InviteReturnState.Where(C => C.InviteStateID == KeyID).FirstOrDefault();
        }

        public List<FD_InviteReturnState> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(FD_InviteReturnState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_InviteReturnState.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.InviteStateID;
                }
            }
            return 0;
        }

        public int Update(FD_InviteReturnState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.InviteStateID;
            }
            return 0;
        }
    }
}
