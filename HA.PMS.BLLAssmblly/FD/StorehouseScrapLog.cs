using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.FD
{
    public class StorehouseScrapLog : ICRUDInterface<FD_StorehouseScrapLog>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_StorehouseScrapLog ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FD_StorehouseScrapLog> GetByAll()
        {
            throw new NotImplementedException();
        }

        public FD_StorehouseScrapLog GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public List<FD_StorehouseScrapLog> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加记录 同时减少数量
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_StorehouseScrapLog ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_StorehouseScrapLog.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    StorehouseSourceProduct ObjStorehouseProductBLL = new StorehouseSourceProduct();
                    var ObjUpdateModel=ObjStorehouseProductBLL.GetByID(ObjectT.SourceProductId);
                    ObjUpdateModel.SourceCount =ObjUpdateModel.SourceCount.Value-int.Parse(ObjectT.ScrapSum.ToString());
                    ObjStorehouseProductBLL.Update(ObjUpdateModel);



                    return ObjectT.ScrapID;
                }

            }
            return 0;
        }

        public int Update(FD_StorehouseScrapLog ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
