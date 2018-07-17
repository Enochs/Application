using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedLoss : ICRUDInterface<FL_QuotedLoss>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(FL_QuotedLoss ObjectT)
        {
            if (ObjectT == null)
            {
                ObjEntity.FL_QuotedLoss.Remove(GetByID(ObjectT.LossID.ToString().ToInt32()));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有
        /// <summary>
        /// 获取所有退单
        /// </summary>
        public List<FL_QuotedLoss> GetByAll()
        {
            return ObjEntity.FL_QuotedLoss.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary> 
        public FL_QuotedLoss GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedLoss.FirstOrDefault(C => C.LossID == KeyID);
        }
        #endregion

        #region 根据CustomerID获取
        /// <summary>
        /// 根据CustomerID获取
        /// </summary> 
        public FL_QuotedLoss GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_QuotedLoss.FirstOrDefault(C => C.CustomerID == KeyID);
        }
        #endregion

        #region 根据索引获取
        /// <summary>
        /// 根据索引获取
        /// </summary>   
        public List<FL_QuotedLoss> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        public int Insert(FL_QuotedLoss ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedLoss.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_QuotedLoss ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion
    }
}
