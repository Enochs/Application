using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Report
{
    public class QuotedSourceItem
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Insert(SS_QuotedSourceItem ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.SS_QuotedSourceItem.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ChangeItemID;
                }
            }
            return 0;
        }

        public int Update(SS_QuotedSourceItem ObjectT)
        {
            return ObjEntity.SaveChanges();
        }

        public List<SS_QuotedSourceItem> GetByChangeID(int ChangeID)
        {

            return ObjEntity.SS_QuotedSourceItem.Where(C => C.ChangeID == ChangeID).ToList();

        }

    }
}
