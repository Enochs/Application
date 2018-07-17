using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedPriceItems : ICRUDInterface<FL_QuotedPriceItems>
    {
        PMS_WeddingEntities ObjEntity = new DataAssmblly.PMS_WeddingEntities();


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_QuotedPriceItems ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPriceItems.Remove(GetByID(ObjectT.ChangeID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }



        /// <summary>
        /// 根据报价单删除
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public int DeleteByQuotedID(int? QuotedID)
        {
            var ObjDeleteList = this.GetByQuotedID(QuotedID);
            if (ObjDeleteList.Count > 0)
            {
                foreach (var ObjectT in ObjDeleteList)
                {
                    ObjEntity.FL_QuotedPriceItems.Remove(ObjectT);
                }
                return ObjEntity.SaveChanges();
            }
            return 0;

        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByAll()
        {
            return ObjEntity.FL_QuotedPriceItems.ToList();
        }



        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedPriceItems GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPriceItems.FirstOrDefault(C => C.ChangeID == KeyID);
        }

        public List<FL_QuotedPriceItems> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 根据等级获取
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByParentQuotedIDandLevel(int? QuotedID, int? Level)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.ItemLevel == Level).OrderBy(C => C.ParentCategoryID).ToList();
        }

        /// <summary>
        /// 根据父级报价单获取
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByParentQuotedID(int? QuotedID)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.ParentQuotedID == QuotedID).OrderBy(C => C.ParentCategoryID).ToList();
        }

        /// <summary>
        /// 根据父级ID获取
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="CatageID"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByParentCatageID(int? QuotedID, int? ParentCategoryID)
        {

            return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.ParentCategoryID == ParentCategoryID).OrderBy(C => C.ParentCategoryID).ToList();
        }


        /// <summary>
        /// 根据父级ID获取
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="CatageID"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByParentCatageID(int? QuotedID, int? ParentCategoryID, string Pretype)
        {
            int Typer = int.Parse(Pretype);
            var Keyquery = (from K in ObjEntity.FL_QuotedPriceItems
                            where K.QuotedID == QuotedID && K.ItemLevel == 3 &&

                            K.Productproperty == Typer
                            select K.ParentCategoryID).Distinct().ToList();

            return (from C in ObjEntity.FL_QuotedPriceItems
                    where C.QuotedID == QuotedID && C.ParentCategoryID == ParentCategoryID

                    && (Keyquery).Contains(C.CategoryID)
                    select C).ToList();


        }

        public List<FL_QuotedPriceItems> GetByParentCatageID(int? QuotedID, int? ParentCategoryID, int? Level, string Pretype)
        {

            int Typer = int.Parse(Pretype);
            var Keyquery = (from K in ObjEntity.FL_QuotedPriceItems
                            where K.QuotedID == QuotedID && K.ItemLevel == 3 &&

                            K.Productproperty == Typer
                            select K.ParentCategoryID).Distinct().ToList();

            return (from C in ObjEntity.FL_QuotedPriceItems
                    where C.QuotedID == QuotedID && C.ParentCategoryID == ParentCategoryID && C.ItemLevel == Level

                    && (Keyquery).Contains(C.ParentCategoryID)
                    select C).ToList();
        }


        public List<FL_QuotedPriceItems> GetByParentCatageID(int? QuotedID, int? ParentCategoryID, int? Level)
        {

            return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.ParentCategoryID == ParentCategoryID && C.ItemLevel == Level).OrderBy(C => C.ParentCategoryID).ToList();
        }

        /// <summary>
        /// 根据类型获取一条记录 一般是一二级
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="ParentCategoryID"></param>
        /// <returns></returns>
        public FL_QuotedPriceItems GetOnlyByCatageID(int? QuotedID, int? CategoryID, int? Level)
        {

            return ObjEntity.FL_QuotedPriceItems.FirstOrDefault(C => C.QuotedID == QuotedID && C.CategoryID == CategoryID && C.ItemLevel == Level);
        }

        /// <summary>
        /// 获取报价单下三级唯一项目
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <param name="ProductID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public FL_QuotedPriceItems GetOnlyByProductID(int? QuotedID, int? ProductID, int? Level)
        {
            return ObjEntity.FL_QuotedPriceItems.FirstOrDefault(C => C.QuotedID == QuotedID && C.ProductID == ProductID && C.ItemLevel == Level);
        }


        public FL_QuotedPriceItems GetByProductIDOrder(int? OrderID, int? ProductID, int? Level)
        {
            return ObjEntity.FL_QuotedPriceItems.FirstOrDefault(C => C.OrderID == OrderID && C.ProductID == ProductID && C.ItemLevel == Level);
        }



        /// <summary>
        /// 根据QuotedID获取一级正常报价单
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByQuotedID(int? QuotedID)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID).OrderBy(C => C.ParentCategoryID).ToList();
        }

        public List<FL_QuotedPriceItems> GetByQuotedsID(int? QuotedID, int? IsFirstMake)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.IsFirstMake == IsFirstMake).OrderBy(C => C.ParentCategoryID).ToList();
        }



        public List<FL_QuotedPriceItems> GetByQuotedID(int? QuotedID, int? Level)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.ItemLevel == Level).OrderBy(C => C.ParentCategoryID).ToList();
        }



        public List<FL_QuotedPriceItems> GetByCategoryID(int? QuotedID, int? CategoryID, int? Level = -1)
        {
            if (Level == -1)
            {
                return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.CategoryID == CategoryID).OrderBy(C => C.ParentCategoryID).ToList();
            }
            else
            {
                return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.CategoryID == CategoryID && C.ItemLevel == Level).OrderBy(C => C.ParentCategoryID).ToList();
            }
        }

        /// <summary>
        /// 获取一级项目
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByOrderID(int OrderID)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.OrderID == OrderID && C.ParentCategoryID == 0).ToList();
        }

        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetByOrdersID(int? OrderID)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.OrderID == OrderID).ToList();
        }


        /// <summary>
        /// 获取花艺项目
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public List<FL_QuotedPriceItems> GetFlowerItem(int? QuotedID)
        {
            return ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.ItemLevel == 3 && C.Productproperty == 2).OrderBy(C => C.ParentCategoryID).ToList();
        }


        /// <summary>
        /// 添加报价单所有的类别产品项目等信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_QuotedPriceItems ObjectT)
        {
            ObjEntity.FL_QuotedPriceItems.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.ChangeID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_QuotedPriceItems ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.ChangeID;
        }


        #region 基于 Repositoy

        public IEnumerable<FL_QuotedPriceItems> GetQuotedPriceSHProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_QuotedPriceItems, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            List<FL_QuotedPriceItems> query = PublicDataTools<FL_QuotedPriceItems>.GetDataByParameter(new FL_QuotedPriceItems(), parameters.ToArray());
            totalCount = query.Count();

            return isAsc ? PageDataTools<FL_QuotedPriceItems>.GetPagedData(query.OrderBy(keySelector), pageSize, pageIndex) :
               PageDataTools<FL_QuotedPriceItems>.GetPagedData(query.OrderByDescending(keySelector), pageSize, pageIndex);
        }

        #endregion


        public decimal GetTotalUnitPrice(int QuotedID)
        {
            decimal result = 0;
            var query = ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.UnitPrice.HasValue && C.Quantity.HasValue).ToList();
            foreach (var C in query)
            {
                result = result + C.UnitPrice.Value * C.Quantity.Value;
            }
            return result;
        }

        public decimal GetTotalPurchasePrice(int QuotedID)
        {
            decimal result = 0;
            var query = ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID && C.PurchasePrice.HasValue && C.Quantity.HasValue).ToList();
            foreach (var C in query)
            {
                result = result + C.PurchasePrice.Value * C.Quantity.Value;
            }
            return result;
        }


        public string GetSaleFinishAmount(DateTime Start, DateTime End, int Type)        //Type 1.人员  2.物料  3.其他
        {
            if (Type == 1)
            {
                return (from C in ObjEntity.FL_QuotedPriceForType
                        join D in ObjEntity.View_SSCustomer
                            on C.CustomerID equals D.CustomerID
                        where D.Partydate >= Start && D.Partydate <= End
                             && D.State == 206 && D.EvalState == 1
                        select C).Sum(C => C.PPrice).ToString();
            }
            else if (Type == 2)
            {
                return (from C in ObjEntity.FL_QuotedPriceForType
                        join D in ObjEntity.View_SSCustomer
                            on C.CustomerID equals D.CustomerID
                        where D.Partydate >= Start && D.Partydate <= End
                             && D.State == 206 && D.EvalState == 1
                        select C).Sum(C => C.MPrice).ToString();
            }
            else if (Type == 3)
            {
                return (from C in ObjEntity.FL_QuotedPriceForType
                        join D in ObjEntity.View_SSCustomer
                            on C.CustomerID equals D.CustomerID
                        where D.Partydate >= Start && D.Partydate <= End
                             && D.State == 206 && D.EvalState == 1
                        select C).Sum(C => C.OPrice).ToString();
            }
            return "";
        }
    }
}