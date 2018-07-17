using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
//骂了隔壁的金色百年
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.CustomerSystem
{
    public class PackageReserve : ICRUDInterface<CC_PackageReserve>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CC_PackageReserve ObjectT)
        {
            ObjEntity.CC_PackageReserve.Remove(ObjectT);
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 根据时间段查询预订列表
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<CC_PackageReserve> GetByTimerSpan(DateTime Star,DateTime End)
        {
            return ObjEntity.CC_PackageReserve.Where(C => C.PartyDate >= Star && C.PartyDate <= End && C.IsDelete == false).ToList();
        }


        public CC_PackageReserve GetOnlyModel(DateTime PartyDate, string DateItem, int PackageID)
        {

            return ObjEntity.CC_PackageReserve.FirstOrDefault(C => C.PartyDate == PartyDate && C.DateItem == DateItem && C.PackageID == PackageID);
        }
        

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<CC_PackageReserve> GetByAll()
        {
            return ObjEntity.CC_PackageReserve.ToList();
        }

        /// <summary>
        /// 根据ID获取单条
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public CC_PackageReserve GetByID(int? KeyID)
        {
            return ObjEntity.CC_PackageReserve.FirstOrDefault(C => C.SourceKey == KeyID);
        }

        public List<CC_PackageReserve> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(CC_PackageReserve ObjectT)
        {
            ObjEntity.CC_PackageReserve.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.SourceKey;
        }

        public int Update(CC_PackageReserve ObjectT)
        {
            return ObjEntity.SaveChanges();
        }

        public List<CC_PackageReserve> GetAllByParameter(List<PMSParameters> parm, string OrderColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<CC_PackageReserve>.GetDataByWhereParameter(parm, OrderColumnName, PageSize, PageIndex, out SourceCount);
        }
    }
}
