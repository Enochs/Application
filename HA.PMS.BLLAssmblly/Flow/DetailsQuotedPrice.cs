using HA.PMS.BLLInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class DetailsQuotedPrice : ICRUDInterface<FL_DetailsQuotedPrice>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        #region 删除功能  方法
        /// <summary>
        /// 删除
        /// </summary> 
        public int Delete(FL_DetailsQuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DetailsQuotedPrice.Remove(GetByID(ObjectT.DetailsID));
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        #endregion

        #region 获取所有沟通记录
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>

        public List<FL_DetailsQuotedPrice> GetByAll()
        {
            return ObjEntity.FL_DetailsQuotedPrice.ToList();
        }
        #endregion


        #region 根据Id获取实体
        /// <summary>
        /// 根据Id获取
        /// </summary>
        public FL_DetailsQuotedPrice GetByID(int? KeyID)
        {
            return ObjEntity.FL_DetailsQuotedPrice.Where(C => C.DetailsID == KeyID).FirstOrDefault();
        }
        #endregion

        #region 根据CustomerId获取实体
        /// <summary>
        /// 根据CustomerId获取
        /// </summary>
        public List<FL_DetailsQuotedPrice> GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_DetailsQuotedPrice.Where(C => C.CustomerID == KeyID).ToList();
        }
        #endregion

        public List<FL_DetailsQuotedPrice> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增 添加
        /// <summary>
        /// 添加
        /// </summary>
        public int Insert(FL_DetailsQuotedPrice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DetailsQuotedPrice.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_DetailsQuotedPrice ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion
    }
}
