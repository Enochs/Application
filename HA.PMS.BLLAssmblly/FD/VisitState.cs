using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.FD
{
    public class VisitState : ICRUDInterface<FD_VisitState>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 根据ID删除
        /// </summary>
        public int Delete(FD_VisitState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_VisitState.Remove(GetByID(ObjectT.ID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有状态
        /// <summary>
        /// 获取所有
        /// </summary>
        public List<FD_VisitState> GetByAll()
        {
            return ObjEntity.FD_VisitState.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        public FD_VisitState GetByID(int? KeyID)
        {
            return ObjEntity.FD_VisitState.FirstOrDefault(C => C.ID == KeyID);
        }
        #endregion

        public List<FD_VisitState> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加
        /// <summary>
        /// 新增
        /// </summary>
        public int Insert(FD_VisitState ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_VisitState.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion


        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FD_VisitState ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion
    }
}
