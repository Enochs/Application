using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Report
{
    public class QuotedItemChange
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Insert(SS_QuotedItemChange ObjectT)
        {

            ObjEntity.SS_QuotedItemChange.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.ChangeID;
        }

        public int Update(SS_QuotedItemChange ObjectT)
        {
            return ObjEntity.SaveChanges();
        }

        public List<SS_QuotedItemChange> GetByQuotedID(int QuotedID)
        {

            return ObjEntity.SS_QuotedItemChange.Where(C => C.QuotedID == QuotedID).ToList();

        }



        public SS_QuotedItemChange GetByChangeID(int ChangeID)
        {

            return ObjEntity.SS_QuotedItemChange.FirstOrDefault(C => C.ChangeID == ChangeID);

        }
    }
}
