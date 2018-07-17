using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.CS
{
    public class CompanySystem : ICRUDInterface<CA_CompanySystem>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_CompanySystem ObjectT)
        {
            if (ObjectT != null)
            {
                //CA_CompanySystem objCelebrationKnowledge = GetByID(ObjectT.SystemId);
                //objCelebrationKnowledge.IsDelete = true;
                ObjEntity.CA_CompanySystem.Remove(GetByID(ObjectT.SystemId));
                return ObjEntity.SaveChanges();

            }
            return 0;
        }
        public List<CA_CompanySystem> GetbyFD_CelebrationKnowledgeParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<CA_CompanySystem>.GetDataByParameter(new CA_CompanySystem(), ObjParameterList);
            SourceCount = query.Count();

            List<CA_CompanySystem> resultList = query.OrderByDescending(C => C.SystemId)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<CA_CompanySystem>();
            }
            return resultList;

        }
        public List<CA_CompanySystem> GetByAll()
        {
            return ObjEntity.CA_CompanySystem.Where(C => C.IsDelete == false).ToList();
        }


        #region 根据Title查找
        /// <summary>
        /// 查找
        /// </summary>
        public CA_CompanySystem GetByName(string Name, int ParentId)
        {
            return ObjEntity.CA_CompanySystem.FirstOrDefault(C => C.SystemTitle == Name && C.ParentID == ParentId);
        }

        public CA_CompanySystem GetByTitle(string Title)
        {
            return ObjEntity.CA_CompanySystem.FirstOrDefault(C => C.SystemTitle == Title);
        }
        #endregion

        public CA_CompanySystem GetByID(int? KeyID)
        {
            return ObjEntity.CA_CompanySystem.FirstOrDefault(C => C.SystemId == KeyID);
        }


        public List<CA_CompanySystem> GetByParentID(int? ParentID)
        {
            return ObjEntity.CA_CompanySystem.Where(C => C.ParentID == ParentID).ToList();
        }

        public List<CA_CompanySystem> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_CompanySystem.Count();

            List<CA_CompanySystem> resultList = ObjEntity.CA_CompanySystem
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SystemId)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_CompanySystem>();
            }
            return resultList;
        }

        public int Insert(CA_CompanySystem ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.CA_CompanySystem.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public int Update(CA_CompanySystem ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SystemId;
            }
            return 0;
        }
    }
}
