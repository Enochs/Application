using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
namespace HA.PMS.BLLAssmblly.FD
{
    /// <summary>
    /// 报价单类型
    /// </summary>
    public class QuotedCatgory : ICRUDInterface<FD_QuotedCatgory>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public string GetFirstSortOrder(int ParentID, int QCKey)
        {
            if (ParentID == 0)
            {
                if (ObjEntity.FD_QuotedCatgory.Count(C => C.Parent == ParentID) == 0)
                {
                    return "10000";
                }
                return (10000 * ObjEntity.FD_QuotedCatgory.Count(C => C.Parent == ParentID) + ObjEntity.FD_QuotedCatgory.Count(C => C.Parent == ParentID) + 1).ToString();
            }
            else
            {
                var Parent = ObjEntity.FD_QuotedCatgory.FirstOrDefault(C => C.QCKey == QCKey).Parent;
                return int.Parse(ObjEntity.FD_QuotedCatgory.FirstOrDefault(C => C.QCKey == QCKey).SortOrder) + string.Empty + (ObjEntity.FD_QuotedCatgory.Count(C => C.Parent == QCKey) + 1);
            }
        }





        public int? GetItemLevel(int ParentQCKey)
        {
            if (ParentQCKey == 0)
            {
                return 1;
            }
            else
            {
                return ObjEntity.FD_QuotedCatgory.FirstOrDefault(C => C.QCKey == ParentQCKey).ItemLevel + 1;
            }
        }

        public int Delete(FD_QuotedCatgory ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FD_QuotedCatgory> GetByAll()
        {
            return ObjEntity.FD_QuotedCatgory.OrderBy(C => C.SortOrder).ToList();
        }

        public FD_QuotedCatgory GetByID(int? KeyID)
        {
            if (KeyID != 0)
            {
                return ObjEntity.FD_QuotedCatgory.FirstOrDefault(C => C.QCKey == KeyID);
            }
            return null;
        }


        /// <summary>
        /// 根据父级获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FD_QuotedCatgory> GetByParentID(int? KeyID)
        {
            return ObjEntity.FD_QuotedCatgory.Where(C => C.Parent == KeyID).OrderBy(C => C.SortOrder).ToList();
        }

        /// <summary>
        /// 根据父级  ItemLevel获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FD_QuotedCatgory> GetByLevelParent(int? KeyID, int? ItemLevel)
        {
            return ObjEntity.FD_QuotedCatgory.Where(C => C.Parent == KeyID && C.ItemLevel == ItemLevel).OrderBy(C => C.SortOrder).ToList();
        }


        /// <summary>
        /// IN查询
        /// </summary>
        /// <param name="KeyList"></param>
        /// <returns></returns>
        public List<FD_QuotedCatgory> GetinKey(int[] KeyList)
        {

            var ObjSource = (from C in ObjEntity.FD_QuotedCatgory

                             where (KeyList).Contains(C.QCKey)
                             select C
                                 );
            return ObjSource.ToList();
        }

        public List<FD_QuotedCatgory> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int QcKey)
        {
            var List = ObjEntity.FD_QuotedCatgory.Where(C => C.QCKey == QcKey || C.Parent == QcKey).ToList();
            foreach (var Objitem in List)
            {
                ObjEntity.FD_QuotedCatgory.Remove(Objitem);
                ObjEntity.SaveChanges();
            }

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_QuotedCatgory ObjectT)
        {
            ObjEntity.FD_QuotedCatgory.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.QCKey;
        }

        public int Update(FD_QuotedCatgory ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.QCKey;
        }
    }
}
