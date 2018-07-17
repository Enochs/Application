using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.CS
{
    public class CompanyFile : ICRUDInterface<CA_CompanyFile>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除
        /// <summary>
        /// 删除对象
        /// </summary>  
        public int Delete(CA_CompanyFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_CompanyFile.Remove(GetByID(ObjectT.FileID));
                return ObjEntity.SaveChanges();
                //ComFile.IsDelete = true;
                //return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有文件
        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <returns></returns>

        public List<CA_CompanyFile> GetByAll()
        {
            return ObjEntity.CA_CompanyFile.Where(C => C.IsDelete == false).ToList();
        }
        #endregion

        #region 根据父级获取
        /// <summary>
        /// 根据父级获取相应的文件
        /// </summary>
        public List<CA_CompanyFile> GetByParentId(int ParentId)
        {
            return ObjEntity.CA_CompanyFile.Where(C => C.ParentFileId == ParentId && C.IsDelete == false).ToList();
        }
        #endregion

        #region 根据ID查找
        /// <summary>
        /// 查找
        /// </summary>
        public CA_CompanyFile GetByID(int? KeyID)
        {
            return ObjEntity.CA_CompanyFile.FirstOrDefault(C => C.FileID == KeyID);
        }
        #endregion


        #region 根据Name查找
        /// <summary>
        /// 查找
        /// </summary>
        public CA_CompanyFile GetByName(string Name, int ParentId)
        {
            return ObjEntity.CA_CompanyFile.FirstOrDefault(C => C.FileName == Name && C.ParentFileId == ParentId);
        }

        public CA_CompanyFile GetByName(string Name)
        {
            return ObjEntity.CA_CompanyFile.FirstOrDefault(C => C.FileName == Name);
        }
        #endregion

        public List<CA_CompanyFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Insert(CA_CompanyFile ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_CompanyFile.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 进行修改
        /// </summary>
        public int Update(CA_CompanyFile ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 分页获取所有 文件资料
        /// <summary>
        /// 分页获取
        /// </summary>
        public List<CA_CompanyFile> GetDataByParameter(List<PMSParameters> pars, string OrderByColumnName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<CA_CompanyFile>.GetDataByWhereParameter(pars, OrderByColumnName, PageSize, PageIndex, out SourceCount, OrderType.Asc);
        }
        #endregion
    }
}
