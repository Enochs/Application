using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;


//调查项目
namespace HA.PMS.BLLAssmblly.CS
{
    public class DegreeOfSatisfactionItem:ICRUDInterface<CS_DegreeOfSatisfactionItem>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CS_DegreeOfSatisfactionItem ObjectT)
        {
            var ObjUpdateModel= this.GetByID(ObjectT.ItemKey);
            ObjUpdateModel.IsDelete = true;
            return ObjEntity.SaveChanges();
            
        }

        public List<CS_DegreeOfSatisfactionItem> GetByAll()
        {
            return ObjEntity.CS_DegreeOfSatisfactionItem.Where(C=>C.IsDelete==false).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public CS_DegreeOfSatisfactionItem GetByID(int? KeyID)
        {
            return ObjEntity.CS_DegreeOfSatisfactionItem.FirstOrDefault(C=>C.ItemKey==KeyID);
        }

        public List<CS_DegreeOfSatisfactionItem> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(CS_DegreeOfSatisfactionItem ObjectT)
        {
            ObjectT.IsDelete = false;
            ObjEntity.CS_DegreeOfSatisfactionItem.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.ItemKey;
        }

        public int Update(CS_DegreeOfSatisfactionItem ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.ItemKey;
        }
    }
}
