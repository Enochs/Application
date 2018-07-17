using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class DesignThemes : ICRUDInterface<FL_DesignThemes>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(FL_DesignThemes ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DesignThemes.Remove(GetByID(ObjectT.ThemeID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有设计主题
        /// <summary>
        /// 获取所有
        /// </summary>
        public List<FL_DesignThemes> GetByAll()
        {
            return ObjEntity.FL_DesignThemes.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// ID获取
        /// </summary>
        public FL_DesignThemes GetByID(int? KeyID)
        {
            return ObjEntity.FL_DesignThemes.FirstOrDefault(C => C.ThemeID == KeyID);
        }
        #endregion

        #region 根据CustomerID获取
        /// <summary>
        /// ID获取
        /// </summary>
        public FL_DesignThemes GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_DesignThemes.FirstOrDefault(C => C.CustomerID == KeyID);
        }
        #endregion

        #region 根据QuotedID获取
        /// <summary>
        /// ID获取
        /// </summary>
        public FL_DesignThemes GetByQUotedID(int? KeyID)
        {
            return ObjEntity.FL_DesignThemes.FirstOrDefault(C => C.QuotedID == KeyID);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>

        public List<DesignThemes> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw null;
        }
        #endregion

        #region 新增数据
        /// <summary>
        /// 新增
        /// </summary>
        public int Insert(FL_DesignThemes ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DesignThemes.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 数据修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_DesignThemes ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ThemeID;
            }
            return 0;
        }
        #endregion


        List<FL_DesignThemes> ICRUDInterface<FL_DesignThemes>.GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
    }
}
