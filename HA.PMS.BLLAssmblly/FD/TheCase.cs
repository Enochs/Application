using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.FD
{
    public class TheCase : ICRUDInterface<FD_TheCase>
    {
        /// <summary>
        /// 单个案例文件
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_TheCase ObjectT)
        {

            if (ObjectT != null)
            {
                FD_TheCase objTheCase = GetByID(ObjectT.CaseID);

                ObjEntity.FD_TheCase.Remove(objTheCase);
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_TheCase> GetByAll()
        {
            return ObjEntity.FD_TheCase.ToList();
        }

        public FD_TheCase GetByID(int? KeyID)
        {
            return ObjEntity.FD_TheCase.FirstOrDefault(C => C.CaseID == KeyID);
        }

        public List<FD_TheCase> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_TheCase.Count();
            List<FD_TheCase> resultList = ObjEntity.FD_TheCase
                //进行排序功能操作，不然系统会抛出异常
                   .OrderBy(C => C.CaseOrder)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_TheCase>();
            }
            return resultList;
        }

        public int Insert(FD_TheCase ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_TheCase.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.CaseID;
                }

            }
            return 0;
        }

        public int Update(FD_TheCase ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.CaseID;
            }
            return 0;
        }

        public List<FD_TheCase> GetCaseByParameter(List<PMSParameters> pars, string OrderColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FD_TheCase>.GetDataByWhereParameter(pars, OrderColumnName, PageSize, PageIndex, out SourceCount, OrderType.Asc);
        }
    }
}
