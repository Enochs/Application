using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Planner : ICRUDInterface<HA.PMS.DataAssmblly.FL_Planner>
    {
        DBHelper db = new DBHelper();
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除


        public int Delete(FL_Planner ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Planner.Remove(GetByID(ObjectT.PlannerID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有

        public List<FL_Planner> GetByAll()
        {
            return ObjEntity.FL_Planner.Where(C => C.IsDelete == false).ToList();
        }


        public List<FL_Planner> GetByType(int? Type)
        {
            return ObjEntity.FL_Planner.Where(C => C.PlannerJob == Type && C.IsDelete == false).ToList();
        }

        #endregion

        #region 根据ID寻找
        /// <summary>
        /// 寻找
        /// </summary> 
        public FL_Planner GetByID(int? KeyID)
        {
            return ObjEntity.FL_Planner.FirstOrDefault(C => C.PlannerID == KeyID);
        }

        public List<FL_Planner> GetByIDs(int? KeyID)
        {
            return ObjEntity.FL_Planner.Where(C => C.PlannerID == KeyID).ToList();
        }
        #endregion

        public List<FL_Planner> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增

        public int Insert(FL_Planner ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Planner.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改


        public int Update(FL_Planner ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion

        #region 分页获取


        public List<FL_Planner> GetAllByParameter(List<PMSParameters> pars, string OrderColumnName, int PageSize, int CurrentPage, out int SourceCount, OrderType ordertype = OrderType.Asc)
        {
            return PublicDataTools<FL_Planner>.GetDataByWhereParameter(pars, OrderColumnName, PageSize, CurrentPage, out SourceCount);
        }
        #endregion


    }
}
