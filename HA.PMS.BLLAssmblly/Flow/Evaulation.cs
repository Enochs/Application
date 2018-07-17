using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Evaulation : ICRUDInterface<FL_Evaulation>
    {
        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        #region 删除功能  作品
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Evaulation ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Evaulation.Remove(GetByID(ObjectT.EvalID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有作品
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<FL_Evaulation> GetByAll()
        {
            return ObjEntity.FL_Evaulation.ToList();
        }
        #endregion

        #region 根据ID获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Evaulation GetByID(int? KeyID)
        {
            return ObjEntity.FL_Evaulation.Where(C => C.EvalID == KeyID).FirstOrDefault();
        }
        #endregion

        #region 根据Name获取
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Evaulation GetByName(string Title)
        {
            return ObjEntity.FL_Evaulation.Where(C => C.EvalTitle == Title).FirstOrDefault();
        }

        public FL_Evaulation GetByName(string Title,int? PlannerID)
        {
            return ObjEntity.FL_Evaulation.Where(C => C.EvalTitle == Title && C.EvalWorkID == PlannerID).FirstOrDefault();
        }
        #endregion

        public List<FL_Evaulation> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 添加数据
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Evaulation ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Evaulation.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.EvalID;
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
        public int Update(FL_Evaulation ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.EvalID;
            }
            return 0;
        }
        #endregion


        #region 根据PlannerID获取策划师个人所有作品
        /// <summary>
        /// 获取作品
        /// </summary>
        /// <param name="PlannerID"></param>
        /// <returns></returns>
        public List<FL_Evaulation> GetByPlannerID(int? PlannerID)
        {
            return ObjEntity.FL_Evaulation.Where(C => C.EvalWorkID == PlannerID).ToList();
        }
        #endregion
    }
}
