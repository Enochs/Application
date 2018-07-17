using HA.PMS.OnlineSysytem.DataBaseAutoUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBase
{
    public class OrderFinalCost
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public void SetFinalCost()
        {
            //var ObjCustomerList = ObjEntity.FL_Customers.ToList();
            AutoCustomer ObjCustomers = new AutoCustomer();
            ObjCustomers.AutoSSRepost();
 
            //foreach (var objitem in ObjCustomerList)
            //{
            //    var ObjList = ObjEntity.FL_ProductforDispatching.Where(C => C.CustomerID == objitem.CustomerID).ToList();
            //    foreach (var objProduct in ObjList)
            //    {
            //        FL_OrderfinalCost ObjFinalCostModel = new FL_OrderfinalCost();

            //        ObjFinalCostModel.KindType = 1;
            //        ObjFinalCostModel.IsDelete = false;
            //        ObjFinalCostModel.CostKey = GetByCustomerID(objitem.CustomerID).CostKey;
            //        ObjFinalCostModel.CreateDate = DateTime.Now;
            //        ObjFinalCostModel.CustomerID = objitem.CustomerID;
            //        ObjFinalCostModel.CellPhone = string.Empty;
            //        ObjFinalCostModel.InsideRemark = string.Empty;


            //        ObjFinalCostModel.KindID = objProduct.DispatchingID;


            //        ObjFinalCostModel.ServiceContent = objProduct.SupplierName;
            //        try
            //        {
            //            ObjFinalCostModel.PlannedExpenditure = objProduct.PurchasePrice.Value;
            //        }
            //        catch
            //        {
            //            ObjFinalCostModel.PlannedExpenditure = 0;
            //        }
            //        ObjFinalCostModel.ActualExpenditure = 0;
            //        ObjFinalCostModel.Expenseaccount = string.Empty;

            //        ObjFinalCostModel.ActualWorkload = string.Empty;
            //        if (objProduct.SupplierID > 0)
            //        {
            //            ObjFinalCostModel.SupplierID = objProduct.SupplierID;

            //            Insert(ObjFinalCostModel);

            //        }
            //        else
            //        {
            //            if (objProduct.Productproperty == 0)
            //            {
            //                ObjFinalCostModel = new FL_OrderfinalCost();
            //                ObjFinalCostModel.KindType = 0;
            //                ObjFinalCostModel.IsDelete = false;
            //                ObjFinalCostModel.CostKey = GetByCustomerID(objitem.CustomerID).CostKey;
            //                ObjFinalCostModel.CreateDate = DateTime.Now;
            //                ObjFinalCostModel.CustomerID = objProduct.CustomerID;
            //                ObjFinalCostModel.CellPhone = string.Empty;
            //                ObjFinalCostModel.InsideRemark = string.Empty;

            //                ObjFinalCostModel.KindID = objProduct.DispatchingID;
            //                ObjFinalCostModel.ServiceContent = objProduct.ServiceContent;

            //                try
            //                {
            //                    ObjFinalCostModel.PlannedExpenditure = objProduct.PurchasePrice.Value;
            //                }
            //                catch
            //                {
            //                    ObjFinalCostModel.PlannedExpenditure = 0;
            //                }
            //                ObjFinalCostModel.ActualExpenditure = 0;
            //                ObjFinalCostModel.Expenseaccount = string.Empty;
            //                ObjFinalCostModel.SupplierID = 0;
            //                ObjFinalCostModel.ActualWorkload = string.Empty;
            //            }


            //        }

            //    }
            //    Console.WriteLine(objitem.Bride + ":财务统计完成!");
            //    System.Threading.Thread.Sleep(350);
            //}
        }

        /// <summary>
        /// 根据客户获取
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public FL_Cost GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Cost.FirstOrDefault(c => c.CustomerID == CustomerID);
        }
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

                var ObjModel = ObjEntity.FL_OrderfinalCost.FirstOrDefault(C => C.ServiceContent == ObjectT.ServiceContent && C.KindID == ObjectT.KindID);
                if (ObjModel == null)
                {

                    ObjEntity.FL_OrderfinalCost.Add(ObjectT);
                    ObjEntity.SaveChanges();
                    return ObjectT.CostKey;
                }

            }

            return 0;
        }
    }
}
