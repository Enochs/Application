using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.FD
{
    public class GuardianMovie : ICRUDInterface<FD_GuardianMovie>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_GuardianMovie ObjectT)
        {
            if (ObjectT != null)
            {
                FD_GuardianMovie objCategory = GetByID(ObjectT.GuradinMovieID);

                objCategory.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_GuardianMovie> GetByAll()
        {
            return ObjEntity.FD_GuardianMovie.Where(C => C.IsDelete == false).ToList();
        }

        public FD_GuardianMovie GetByID(int? KeyID)
        {
            return ObjEntity.FD_GuardianMovie.FirstOrDefault(C => C.GuradinMovieID == KeyID);
        }

        public List<FD_GuardianMovie> GetByIndex(int GuardianId,int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_GuardianMovie.Where(C =>C.FourGuardianId == GuardianId &&C.IsDelete == false).Count();

            List<FD_GuardianMovie> resultList = ObjEntity.FD_GuardianMovie.Where(C => C.FourGuardianId == GuardianId && C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.GuradinMovieID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_GuardianMovie>();
            }
            return resultList;
        }

        public int Insert(FD_GuardianMovie ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_GuardianMovie.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.GuradinMovieID;
                }

            }
            return 0;
        }

        public int Update(FD_GuardianMovie ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.GuradinMovieID;
            }
            return 0;
        }


        public List<FD_GuardianMovie> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
    }
}
