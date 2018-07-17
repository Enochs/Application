using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HA.PMS.BLLAssmblly.Flow
{
    public class OrderfinalCost : ICRUDInterface<FL_OrderfinalCost>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderfinalCost ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderfinalCost> GetByAll()
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 根据客户ID获取成本明细
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public List<FL_OrderfinalCost> GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_OrderfinalCost.Where(C => C.CustomerID == CustomerID && C.IsDelete == false).ToList();
        }


        /// <summary>
        /// 根据供应商获取需要更改的四蹄
        /// </summary>
        /// <param name="SuppLyName"></param>
        /// <returns></returns>
        public FL_OrderfinalCost GetBySupplilyName(string SuppLyName, int KindKey)
        {
            return ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.KindID == KindKey && C.KindType == 1 && C.ServiceContent == SuppLyName);
        }

        public FL_OrderfinalCost GetBySupplilyName(string SuppLyName, int KindKey, int KindType)
        {
            return ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.KindID == KindKey && C.KindType == KindType && C.ServiceContent == SuppLyName);
        }


        public FL_OrderfinalCost GetByEmpLoyeeName(string EmpLoyeeName, int KindKey)
        {
            return ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.KindID == KindKey && C.KindType == 2 && C.ServiceContent == EmpLoyeeName);
        }


        public FL_OrderfinalCost GetByPremissionnalName(string TeamName, int KindKey)
        {
            return ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.KindID == KindKey && C.KindType == 0 && C.ServiceContent == TeamName);
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_OrderfinalCost GetByID(int? KeyID)
        {
            return ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.CostID == KeyID);
        }




        public List<FL_OrderfinalCost> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        /// <summary>
        /// 获取结算金额 根据类型和时间
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="TimerStar"></param>
        /// <param name="TimerEnd"></param>
        /// <returns></returns>
        public string GetOrderCostBytypeandTimer(int SupplierID, DateTime? TimerStar, DateTime? TimerEnd)
        {
            if (TimerStar != null && TimerEnd != null)
            {
                var ObjList = ObjEntity.FL_OrderfinalCost.Where(C => C.CreateDate >= TimerStar && C.CreateDate <= TimerEnd && C.SupplierID == SupplierID).ToList();
                return ObjList.Sum(C => C.ActualExpenditure).ToString();
                //return ObjEntity.FL_OrderfinalCost.Where(C => C.CreateDate>=TimerStar&&C.CreateDate<=TimerEnd&&C.SupplierID==SupplierID).Sum(C => C.ActualExpenditure).ToString();

            }
            else
            {

                var ObjList = ObjEntity.FL_OrderfinalCost.Where(C => C.SupplierID == SupplierID).ToList();
                return ObjList.Sum(C => C.ActualExpenditure).ToString();
            }
        }


        /// <summary>
        /// 添加成本明细
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderfinalCost ObjectT)
        {

            if (ObjectT.KindType == 1)
            {
                var ObjModel = ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.ServiceContent == ObjectT.ServiceContent && C.KindType == 1 && C.KindID == ObjectT.KindID);
                if (ObjModel == null)
                {

                    ObjEntity.FL_OrderfinalCost.Add(ObjectT);
                    ObjEntity.SaveChanges();
                    return ObjectT.CostKey;
                }
            }
            else
            {
                ObjEntity.FL_OrderfinalCost.Add(ObjectT);
                ObjEntity.SaveChanges();
                return ObjectT.CostKey;
            }

            return 0;
        }

        public int Update(FL_OrderfinalCost ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.CostKey;
        }

        //[FL_OrderfinalCost]
        public IEnumerable<FL_OrderfinalCost> Where(Func<FL_OrderfinalCost, bool> predicate)
        {
            return ObjEntity.FL_OrderfinalCost.Where(predicate);
        }
        public FL_OrderfinalCost FirstOrDefault(Func<FL_OrderfinalCost, bool> predicate)
        {
            return ObjEntity.FL_OrderfinalCost.FirstOrDefault(predicate);
        }
    }
}
