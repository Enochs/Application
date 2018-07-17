using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{

    public class EarlyDesigner : ICRUDInterface<FL_EarlyDesigner>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除前期设计单
        /// </summary>
        public int Delete(FL_EarlyDesigner ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_EarlyDesigner.Remove(GetByID(ObjectT.ID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有
        /// <summary>
        /// 获取所有前期设计单
        /// </summary>
        public List<FL_EarlyDesigner> GetByAll()
        {
            return ObjEntity.FL_EarlyDesigner.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取前期设计单
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>

        public FL_EarlyDesigner GetByID(int? KeyID)
        {
            return ObjEntity.FL_EarlyDesigner.FirstOrDefault(C => C.ID == KeyID);
        }
        #endregion

        #region 根据CustomerID获取
        /// <summary>
        /// 根据CustomerID获取前期设计单
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>

        public FL_EarlyDesigner GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_EarlyDesigner.FirstOrDefault(C => C.CustomerID == KeyID);
        }
        #endregion

        #region 根据索引获取前期设计单
        /// <summary>
        /// 根据索引获取前期设计单
        /// </summary>
        public List<FL_EarlyDesigner> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 添加
        /// <summary>
        /// 新增前期设计单
        /// </summary>
        public int Insert(FL_EarlyDesigner ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_EarlyDesigner.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改前期设计单
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Update(FL_EarlyDesigner ObjectT)
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
