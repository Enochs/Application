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
    public class PlannerType : ICRUDInterface<FL_PlannerType>
    {
        DBHelper db = new DBHelper();
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        #region 删除


        public int Delete(FL_PlannerType ObjectT)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 获取所有


        public List<FL_PlannerType> GetByAll()
        {
            return ObjEntity.FL_PlannerType.Where(C => C.IsDelete == false).ToList();
        }


        public List<FL_PlannerType> GetAll()
        {
            return ObjEntity.FL_PlannerType.ToList();
        }
        #endregion

        #region 根据ID获取


        public FL_PlannerType GetByID(int? KeyID)
        {
            return ObjEntity.FL_PlannerType.FirstOrDefault(C => C.TypeID == KeyID);
        }
        #endregion


        public List<FL_PlannerType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 插入数据  新增


        public int Insert(FL_PlannerType ObjectT)
        {
            ObjEntity.FL_PlannerType.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }
        #endregion

        #region 修改数据  更新


        public int Update(FL_PlannerType ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion

        #region 分页获取

        public List<FL_PlannerType> GetDataByParameter(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int CurrentPage, out int SourceCount, OrderType order = OrderType.Desc)
        {
            return PublicDataTools<FL_PlannerType>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, CurrentPage, out SourceCount);
        }
        #endregion
    }
}
