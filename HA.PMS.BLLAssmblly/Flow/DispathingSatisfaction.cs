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
    public class DispathingSatisfaction : ICRUDInterface<FL_DispathingSatisfaction>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>     
        public int Delete(FL_DispathingSatisfaction ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_DispathingSatisfaction.Remove(GetByID(ObjectT.SatisfactionId));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 查询出所有
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>

        public List<FL_DispathingSatisfaction> GetByAll()
        {
            return ObjEntity.FL_DispathingSatisfaction.ToList();
        }
        #endregion

        #region 根据ID查找
        /// <summary>
        /// 根据ID查找
        /// </summary>
        public FL_DispathingSatisfaction GetByID(int? KeyID)
        {
            return ObjEntity.FL_DispathingSatisfaction.Where(C => C.SatisfactionId == KeyID).FirstOrDefault();
        }
        #endregion

        public List<FL_DispathingSatisfaction> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Insert(FL_DispathingSatisfaction ObjectT)
        {
            if (ObjectT.DispatchingID > 0)      //避免生成DispatchingID为0的数据
            {
                List<PMSParameters> pars = new List<PMSParameters>();
                pars.Add("DispatchingID", ObjectT.DispatchingID, NSqlTypes.Equal);
                pars.Add("SatisfactionName", ObjectT.SatisfactionName, NSqlTypes.StringEquals);
                pars.Add("EvaluationName", ObjectT.EvaluationName, NSqlTypes.StringEquals);

                bool isExists = PublicTools.PublicDataTools<FL_DispathingSatisfaction>.IsExists(pars);
                if (isExists == true)
                {
                    ObjEntity.SaveChanges();
                    return ObjectT.SatisfactionId;
                }
                else
                {
                    if (ObjectT != null)
                    {
                        ObjEntity.FL_DispathingSatisfaction.Add(ObjectT);
                        if (ObjEntity.SaveChanges() > 0)
                        {
                            return ObjectT.SatisfactionId;
                        }
                    }
                }
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        public int Update(FL_DispathingSatisfaction ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SatisfactionId;
            }
            return 0;
        }
        #endregion


        #region 根据DispatchingID查找
        /// <summary>
        /// 根据ID查找
        /// </summary>
        public List<FL_DispathingSatisfaction> GetByDispatchingID(int? DispatchingID)
        {
            return ObjEntity.FL_DispathingSatisfaction.Where(C => C.DispatchingID == DispatchingID).ToList();
        }
        #endregion

        #region 根据DispatchingID Name查找
        /// <summary>
        /// 根据Name ID查找
        /// </summary>
        public FL_DispathingSatisfaction GetByEvalationName(string EvaluationName, int? DispatchingID)
        {
            return ObjEntity.FL_DispathingSatisfaction.FirstOrDefault(C => C.EvaluationName == EvaluationName && C.DispatchingID == DispatchingID);
        }
        #endregion
    }
}
