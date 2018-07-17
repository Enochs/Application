using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedContent:ICRUDInterface<FL_QuotedContent>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除功能  
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_QuotedContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedContent.Remove(GetByID(ObjectT.QContentID));
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
        public List<FL_QuotedContent> GetByAll()
        {
            return ObjEntity.FL_QuotedContent.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedContent GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedContent.Where(C => C.QContentID == KeyID).FirstOrDefault();
        }
        #endregion


        public List<FL_QuotedContent> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加数据
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_QuotedContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedContent.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.QContentID;
                }
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_QuotedContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.QContentID;
            }
            return 0;
        }
        #endregion


        #region 根据QuotedID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FL_QuotedContent> GetByQuotedID(int? KeyID)
        {
            return ObjEntity.FL_QuotedContent.Where(C => C.QuotedID == KeyID).ToList();
        }
        #endregion

        
    }
}
