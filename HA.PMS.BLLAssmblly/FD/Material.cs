using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.FD
{
    public class Material:ICRUDInterface<FD_Material>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_Material ObjectT)
        {
            if (ObjectT != null)
            {
                FD_Material ObjMaterial = GetByID(ObjectT.MaterialId);
                ObjMaterial.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        /// <summary>
        /// 返回所有材质
        /// </summary>
        /// <returns></returns>
        public List<FD_Material> GetByAll()
        {
            return ObjEntity.FD_Material.Where(C => C.IsDelete == false).OrderBy(C => C.MaterialId).ToList();
        }

        /// <summary>
        /// 根据Id查找材质
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_Material GetByID(int? KeyID)
        {
            return ObjEntity.FD_Material.FirstOrDefault(c => c.MaterialId == KeyID);
        }



        /// <summary>
        /// 分页查询材质
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_Material> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_Material.Count();
            List<FD_Material> resultList = ObjEntity.FD_Material.
                //进行排序功能操作，不然系统会抛出异常
                OrderByDescending(C => C.MaterialId).Skip(PageSize * PageIndex).Take(PageSize).ToList();
            if (resultList.Count > 0)
            {
                return new List<FD_Material>();
            }
            return resultList;
        }

        /// <summary>
        /// 分页获取报价单数据
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<FD_Material> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<FD_Material>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_Material ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_Material.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MaterialId;
                }
            }
            return 0;
           
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_Material ObjectT)
        {
            return ObjEntity.SaveChanges();
        }


        
    }
}
