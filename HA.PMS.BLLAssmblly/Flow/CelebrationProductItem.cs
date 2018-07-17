using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;

namespace HA.PMS.BLLAssmblly.Flow
{

    /// <summary>
    /// 派工执行总表
    /// </summary>
    public class CelebrationProductItem : ICRUDInterface<FL_CelebrationProductItem>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_CelebrationProductItem ObjectT)
        {
            
            throw new NotImplementedException();
        }

        public List<FL_CelebrationProductItem> GetByAll()
        {
            throw new NotImplementedException();
        }

        public FL_CelebrationProductItem GetByID(int? KeyID)
        {
          return  ObjEntity.FL_CelebrationProductItem.FirstOrDefault(C => C.ItemIndex==KeyID);
        }



        /// <summary>
        /// 获取非空非库房供应商
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetSupplierByNotNullOrWareHouse(int? CelebrationID)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID && C.SupplierName != null && C.SupplierName != "库房").ToList();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="QuotedID"></param>
        ///// <param name="ChangeID"></param>
        ///// <returns></returns>
        //public List<FL_CelebrationProductItem> GetByChangeID(int? ChangeID)
        //{

        //    return ObjEntity.FL_CelebrationProductItem.Where(C => C.ChangeID == ChangeID).ToList();
        //}

        public List<FL_CelebrationProductItem> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取某个级别下的数据
        /// </summary>
        /// <param name="GetByParentCelebrationID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByParentCelebrationID(int? GetByParentCelebrationID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.ParentCelebrationID == GetByParentCelebrationID && C.ItemLevel == level).ToList();
        }

        /// <summary>
        /// 根据QuotedID层级和其他元素获取数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="ParentCatageID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByParentCatageID(int? CelebrationID, int? ParentCatageID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID && C.ItemLevel == level && C.ParentCategoryID == ParentCatageID).ToList();
        }

        /// <summary>
        /// 获取单类下的元素
        /// </summary>
        /// <param name="CelebrationID"></param>
        /// <param name="ParentCatageID"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByParentCatageID(int? CelebrationID, int? ParentCatageID)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID &&C.ParentCategoryID == ParentCatageID).ToList();
        }


       

        /// <summary>
        /// 获取唯一项目
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="CatageID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public FL_CelebrationProductItem GetOnlyByCatageID(int? CelebrationID, int? CategoryID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.FirstOrDefault(C => C.CelebrationID == CelebrationID && C.CategoryID == CategoryID && C.ItemLevel == level);
        }


        /// <summary>
        /// 根据执行表IDhe层级获取数据
        /// </summary>
        /// <param name="GetByParentCelebrationID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByCelebrationID(int? CelebrationID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID && C.ItemLevel == level).ToList();
        }


        /// <summary>
        /// 根据报价单ID获取数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByQuotedID(int QuotedID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.QuotedID == QuotedID && C.ItemLevel == level).ToList();
        }
        /// <summary>
        /// 根据类型和层级获取
        /// </summary>
        /// <param name="CelebrationID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByCategoryID(int CelebrationID, int? CategoryID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID && C.CategoryID == CategoryID && C.ItemLevel == level).ToList();
        }


        /// <summary>
        /// 根据父级报价单获取
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FL_CelebrationProductItem> GetByParentQuotedID(int? QuotedID, int? level)
        {
            return ObjEntity.FL_CelebrationProductItem.Where(C => C.ParentQuotedID == QuotedID && C.ItemLevel == level).ToList();
        }


        /// <summary>
        /// 获取唯一产品
        /// </summary>
        /// <param name="CelebrationID"></param>
        /// <param name="ProductID"></param>
        /// <param name="ItemLevel"></param>
        /// <returns></returns>
        public FL_CelebrationProductItem GetOnlyByProductID(int? CelebrationID, int? ProductID, int? ItemLevel)
        {
            return ObjEntity.FL_CelebrationProductItem.FirstOrDefault(C => C.CelebrationID == CelebrationID && C.ProductID == ProductID&&C.ItemLevel==ItemLevel);
        }

        /// <summary>
        /// 增加变更
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_CelebrationProductItem ObjectT)
        {


            ObjEntity.FL_CelebrationProductItem.Add(ObjectT);


            ObjEntity.SaveChanges();
            return ObjectT.ItemIndex;

        }


        /// <summary>
        /// 修改执行总表
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_CelebrationProductItem ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.CelebrationID;
            }
            return 0;
        }
    }
}
