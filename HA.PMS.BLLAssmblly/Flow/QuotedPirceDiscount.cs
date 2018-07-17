using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedPirceDiscount : ICRUDInterface<FL_QuotedPirceDiscount>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除功能
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Delete(FL_QuotedPirceDiscount ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPirceDiscount.Remove(GetByID(ObjectT.ID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有折扣
        /// <summary>
        /// 获取所有
        /// </summary>
        public List<FL_QuotedPirceDiscount> GetByAll()
        {
            return ObjEntity.FL_QuotedPirceDiscount.ToList();
        }
        #endregion

        #region 根据Id获取折扣实体类
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public FL_QuotedPirceDiscount GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPirceDiscount.FirstOrDefault(C => C.ID == KeyID);
        }
        #endregion


        #region 根据OrderID查询
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public FL_QuotedPirceDiscount GetByOrderID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPirceDiscount.FirstOrDefault(C => C.OrderID == KeyID);
        }
        #endregion

        public List<FL_QuotedPirceDiscount> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 添加
        /// </summary>  
        public int Insert(FL_QuotedPirceDiscount ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPirceDiscount.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_QuotedPirceDiscount ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
        #endregion
    }
}
