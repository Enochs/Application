using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class DesignUpload : ICRUDInterface<FL_DesignUpload>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();




        #region 删除
        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <returns></returns>

        public int Delete(FL_DesignUpload ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DesignUpload.Remove(GetByID(ObjectT.DuId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion


        public List<FL_DesignUpload> GetByAll() 
        {
            return ObjEntity.FL_DesignUpload.ToList();
        }

        #region 根据DesignClassId查询
        /// <summary>
        /// Id查询
        /// </summary>
        public List<FL_DesignUpload> GetByDesignClassID(int? KeyID,int Type)
        {
            return ObjEntity.FL_DesignUpload.Where(C => C.DesignId == KeyID && C.Type == Type).ToList();
        }
        #endregion

        #region 根据OrderID查询
        /// <summary>
        /// Id查询
        /// </summary>
        public List<FL_DesignUpload> GetByOrderID(int? KeyID)
        {
            return ObjEntity.FL_DesignUpload.Where(C => C.OrderId == KeyID).ToList();
        }
        #endregion

        
        #region 根据Id查询
        /// <summary>
        /// Id查询
        /// </summary>
        public FL_DesignUpload GetByID(int? KeyID)
        {
            return ObjEntity.FL_DesignUpload.Where(C => C.DuId == KeyID).FirstOrDefault();
        }
        #endregion


        public List<FL_DesignUpload> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 新增
        public int Insert(FL_DesignUpload ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DesignUpload.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DuId;
                }
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_DesignUpload ObjectT)
        {
             if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.DuId;
            }
            return 0;
        }
        #endregion
    }
}
