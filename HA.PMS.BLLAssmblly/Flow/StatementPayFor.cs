using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class StatementPayFor : ICRUDInterface<FL_StatementPayFor>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary> 
        public int Delete(FL_StatementPayFor ObjectT)
        {
            ObjEntity.FL_StatementPayFor.Remove(GetByID(ObjectT.ID));
            return ObjEntity.SaveChanges();
        }
        #endregion

        public List<FL_StatementPayFor> GetByAll()
        {
            throw new NotImplementedException();
        }

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public FL_StatementPayFor GetByID(int? KeyID)
        {
            return ObjEntity.FL_StatementPayFor.FirstOrDefault(C => C.ID == KeyID);
        }
        #endregion


        #region 根据客户ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public List<FL_StatementPayFor> GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_StatementPayFor.Where(C => C.CustomerID == KeyID).ToList();
        }
        #endregion

        public List<FL_StatementPayFor> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        public int Insert(FL_StatementPayFor ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_StatementPayFor.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>    
        public int Update(FL_StatementPayFor ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion

        #region 分页查询

        public List<FL_StatementPayFor> GetDataByParameter(List<PMSParameters> pars, string OrderByName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FL_StatementPayFor>.GetDataByWhereParameter(pars, OrderByName, PageSize, PageIndex, out SourceCount).ToList();
        }
        #endregion
    }
}
