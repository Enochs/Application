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
    public class QuotedPriceSchedule : ICRUDInterface<FL_QuotedPriceSchedule>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(FL_QuotedPriceSchedule ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPriceSchedule.Remove(GetByID(ObjectT.ScheID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有预定档期
        /// <summary>
        /// 获取所有预定档期
        /// </summary>
        public List<FL_QuotedPriceSchedule> GetByAll()
        {
            return ObjEntity.FL_QuotedPriceSchedule.ToList();
        }
        #endregion

        #region 根据ID获取预定档期
        /// <summary>
        /// 根据ID获取预定档期
        /// </summary>
        public FL_QuotedPriceSchedule GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPriceSchedule.FirstOrDefault(C => C.ScheID == KeyID);
        }
        #endregion

        #region 根据新人ID获取预定档期
        /// <summary>
        /// 根据新人ID获取预定档期
        /// </summary>
        public List<FL_QuotedPriceSchedule> GetByCustomerID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPriceSchedule.Where(C => C.ScheCustomerID == KeyID).ToList();
        }
        #endregion

        #region 根据索引获取预定档期
        /// <summary>
        /// 根据ID获取预定档期
        /// </summary>
        public List<FL_QuotedPriceSchedule> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增档期预定
        /// </summary>
        public int Insert(FL_QuotedPriceSchedule ObjectT)
        {
            if (ObjectT != null)
            {
                if (GetByCustomerIdName(ObjectT.ScheCustomerID.ToString().ToInt32(), ObjectT.ScheGuardianID.ToString().ToInt32()) != null)
                {
                    return -1;
                }
                else
                {
                    ObjEntity.FL_QuotedPriceSchedule.Add(ObjectT);
                    return ObjEntity.SaveChanges();
                }
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改档期预定
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Update(FL_QuotedPriceSchedule ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询 婚期预定
        /// </summary>
        public List<View_CustomerQuotedSchedule> GetByAllParameter(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_CustomerQuotedSchedule>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, PageIndex, out SourceCount);
        }
        #endregion

        #region 判断该执行团队(四大金刚)是否存在
        /// <summary>
        /// 判断
        /// </summary>
        public FL_QuotedPriceSchedule GetByCustomerIdName(int CustomerID, int GuardianId = 0)
        {
            return ObjEntity.FL_QuotedPriceSchedule.FirstOrDefault(C => C.ScheCustomerID == CustomerID && C.ScheGuardianID == GuardianId);
        }
        #endregion

        #region 根据四大金刚ID查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="GuardianId"></param>
        public List<FL_QuotedPriceSchedule> GetByGuardianID(int GuardianId)
        {
            return ObjEntity.FL_QuotedPriceSchedule.Where(C => C.ScheGuardianID == GuardianId).ToList();
        }
        #endregion

    }
}
