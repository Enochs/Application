using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedPriceForType : ICRUDInterface<FL_QuotedPriceForType>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        #region 删除功能
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>

        public int Delete(FL_QuotedPriceForType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPriceForType.Remove(GetByID(ObjectT.ID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        #region 获取所有折扣
        /// <summary>
        /// 获取所有
        /// </summary>
        public List<FL_QuotedPriceForType> GetByAll()
        {
            return ObjEntity.FL_QuotedPriceForType.ToList();
        }
        #endregion

        #region 根据Id获取折扣实体类
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public FL_QuotedPriceForType GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPriceForType.FirstOrDefault(C => C.ID == KeyID);
        }
        #endregion


        #region 根据OrderID查询
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public FL_QuotedPriceForType GetByOrderID(int? KeyID, int IsFirstMake = 0)
        {
            return ObjEntity.FL_QuotedPriceForType.FirstOrDefault(C => C.OrderID == KeyID && C.IsFirstMake == IsFirstMake);
        }
        #endregion

        #region 根据CustomerID查询
        /// <summary>
        /// 根据ID获取
        /// </summary>
        public List<FL_QuotedPriceForType> GetByCustomerID(int? KeyID, int IsFirstMake = 0)
        {
            return ObjEntity.FL_QuotedPriceForType.Where(C => C.CustomerID == KeyID).ToList();
        }
        #endregion

        #region 根据OrderID查询
        /// <summary>
        /// 根据ID获取列表
        /// </summary>
        public List<FL_QuotedPriceForType> GetByOrderID(int? KeyID)
        {
            return ObjEntity.FL_QuotedPriceForType.Where(C => C.OrderID == KeyID).ToList();
        }
        #endregion

        public List<FL_QuotedPriceForType> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        #region 新增
        /// <summary>
        /// 添加
        /// </summary>  
        public int Insert(FL_QuotedPriceForType ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedPriceForType.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion






        public int Update(FL_QuotedPriceForType ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ID;
            }
            return 0;
        }
        DBHelper db = new DBHelper();
        public int UpdatePriceForType(FL_QuotedPriceForType ObjectT)
        {

            string sql = string.Format("Update FL_QuotedPriceForType set Mprice={0} where CustomerID={1} and IsFirstMake={2}", ObjectT.MPrice, ObjectT.CustomerID, ObjectT.IsFirstMake);
            int result = db.ExcuteNonQuery(sql);
            return result;
        }


        public string GetSalePriceForType(DateTime Start, DateTime End, int Type, int CustomerID = 1)         //1.人员    2.物料     3.其他
        {
            if (CustomerID == 1)            //没有传入CustomerID
            {
                if (Type == 1)
                {
                    return (from C in ObjEntity.FL_QuotedPriceForType
                            join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                            where D.Partydate >= Start && D.Partydate <= End && D.State==206
                            select C).Sum(C => C.PPrice).ToString();
                }
                else if (Type == 2)
                {
                    return (from C in ObjEntity.FL_QuotedPriceForType
                            join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                            where D.Partydate >= Start && D.Partydate <= End && D.State == 206
                            select C).Sum(C => C.MPrice).ToString();
                }
                else if (Type == 3)
                {
                    return (from C in ObjEntity.FL_QuotedPriceForType
                            join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                            where D.Partydate >= Start && D.Partydate <= End && D.State == 206
                            select C).Sum(C => C.OPrice).ToString();
                }
            }
            else
            {
                if (Type == 1)
                {
                    return (from C in ObjEntity.FL_QuotedPriceForType
                            join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                            where D.Partydate >= Start && D.Partydate <= End && C.CustomerID == CustomerID && D.State == 206
                            select C).Sum(C => C.PPrice).ToString();
                }
                else if (Type == 2)
                {
                    return (from C in ObjEntity.FL_QuotedPriceForType
                            join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                            where D.Partydate >= Start && D.Partydate <= End && C.CustomerID == CustomerID && D.State == 206
                            select C).Sum(C => C.MPrice).ToString();
                }
                else if (Type == 3)
                {
                    return (from C in ObjEntity.FL_QuotedPriceForType
                            join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                            where D.Partydate >= Start && D.Partydate <= End && C.CustomerID == CustomerID && D.State == 206
                            select C).Sum(C => C.OPrice).ToString();
                }
            }
            return "0.00";
        }
    }
}
