using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBaseAutoUpdate
{

    public class AutoCustomer
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        private void AutoMoneyPlan()
        {
            //int OrderID = 0;
            //var QuotedList = ObjEntity.FL_QuotedPrice.ToList();
            //foreach (var objItem in QuotedList)
            //{
            //    OrderID = objItem.OrderID.Value;
            //    var Modelist = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.OrderID == OrderID).ToList();
            //    if (Modelist.Count() == 0)
            //    {
            //        var OrderModel = ObjEntity.FL_Order.FirstOrDefault(C => C.OrderID == OrderID);
            //        var ObjQuotedModel = ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.OrderID == OrderID);
            //        if (ObjQuotedModel != null && OrderModel!=null)
            //        {
            //            ObjEntity.FL_QuotedCollectionsPlan.Add(new FL_QuotedCollectionsPlan()
            //            {
            //                OrderID = OrderID,
            //                QuotedID = ObjQuotedModel.QuotedID,
            //                RealityAmount = OrderModel.EarnestMoney,
            //                CollectionTime = ObjQuotedModel.CreateDate,
            //                CreateEmpLoyee = ObjQuotedModel.EmpLoyeeID,
            //                AmountEmployee = ObjQuotedModel.EmpLoyeeID,
            //                CreateDate = ObjQuotedModel.CreateDate,
            //                Amountmoney = OrderModel.EarnestMoney,
            //                RowLock = false,
            //                Node = "定金",
            //                MoneyType = "现金"

            //            });
            //            Console.WriteLine("添加定金计划:" + objItem.CustomerID);
            //        }


            //        if (ObjQuotedModel != null && OrderModel != null)
            //        {
            //            ObjEntity.FL_QuotedCollectionsPlan.Add(new FL_QuotedCollectionsPlan()
            //            {
            //                OrderID = OrderID,
            //                QuotedID = ObjQuotedModel.QuotedID,
            //                RealityAmount = ObjQuotedModel.EarnestMoney,
            //                CollectionTime = ObjQuotedModel.CreateDate,
            //                CreateEmpLoyee = ObjQuotedModel.EmpLoyeeID,
            //                AmountEmployee = ObjQuotedModel.EmpLoyeeID,
            //                CreateDate = ObjQuotedModel.CreateDate,
            //                Amountmoney = OrderModel.EarnestMoney,
            //                RowLock = false,
            //                Node = "首期预付款",
            //                MoneyType = "现金"
            //            });
            //        }
            //        Console.WriteLine("添加预付款计划:" + objItem.CustomerID);
            //    }
        
            //}
        }

        public void AutoSSRepost()
        {
            var NoneList = ObjEntity.FL_Customers.ToList();
            AutoMoneyPlan();
            foreach (var ObjNoneModel in NoneList)
            {
                var NoneModel = ObjEntity.SS_Report.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);
                if (NoneModel == null)
                {
                    var ObjInviteModel = ObjEntity.FL_Invite.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);
                    var ObjOrderModel = ObjEntity.FL_Order.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);
                    var ObjQuotedModel = ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);

                    SS_Report ObjReport = new SS_Report();
                    ObjReport.CustomerID = ObjNoneModel.CustomerID;
                    ObjReport.CreateDate = ObjNoneModel.RecorderDate;
                    ObjReport.Partydate = ObjNoneModel.PartyDate;
                 
                    if (ObjInviteModel != null)
                    {
                        ObjReport.InviteCreateDate = ObjInviteModel.CreateDate;
                        ObjReport.InviteEmployee = ObjInviteModel.EmpLoyeeID;
                    }


                    if (ObjOrderModel != null)
                    {
                        ObjReport.OrderCreateDate = ObjOrderModel.CreateDate;
                        ObjReport.OrderEmployee = ObjOrderModel.EmployeeID;
                    }




                    if (ObjQuotedModel != null)
                    {
                        ObjReport.QuotedCreateDate = ObjQuotedModel.CreateDate;
                        ObjReport.OrderEmployee = ObjQuotedModel.EmpLoyeeID;
                        ObjReport.QuotedDateSucessDate = ObjQuotedModel.CreateDate;
                        ObjReport.QuotedMoney = ObjQuotedModel.FinishAmount;
                        ObjReport.OrderCreateDate = ObjOrderModel.CreateDate;
         
 
                    }
                    Console.WriteLine("添加数据报表:" + ObjNoneModel.Bride);
                    ObjEntity.SS_Report.Add(ObjReport);
                    ObjEntity.SaveChanges();
                }
                else
                {
                    var ObjInviteModel = ObjEntity.FL_Invite.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);
                    var ObjOrderModel = ObjEntity.FL_Order.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);
                    var ObjQuotedModel = ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.CustomerID == ObjNoneModel.CustomerID);
                    var CustomerModel = ObjEntity.FL_Customers.FirstOrDefault(C=>C.CustomerID==ObjNoneModel.CustomerID);
                    SS_Report ObjReport = ObjEntity.SS_Report.FirstOrDefault(C=>C.CustomerID==ObjNoneModel.CustomerID);
                    ObjReport.CustomerID = ObjNoneModel.CustomerID;
                    ObjReport.CreateDate = ObjNoneModel.RecorderDate;
                    ObjReport.Partydate = CustomerModel.PartyDate;
                    
                    if (ObjInviteModel != null)
                    {
                        ObjReport.InviteCreateDate = ObjInviteModel.CreateDate;
                        ObjReport.InviteEmployee = ObjInviteModel.EmpLoyeeID;
                        ObjEntity.SaveChanges();


                        FL_Telemarketing telemarketing = new FL_Telemarketing();
                        telemarketing.EmployeeID = ObjInviteModel.EmpLoyeeID;
                        telemarketing.CreateEmpLoyee = ObjInviteModel.EmpLoyeeID;
                        telemarketing.CustomerID = ObjInviteModel.CustomerID;
                        telemarketing.SortOrder = 0; //获取最大批次量返回时加 一 更新批次量
                        telemarketing.CreateDate = DateTime.Now;
                     
                        ObjEntity.FL_Telemarketing.Add(telemarketing);
                        ObjEntity.SaveChanges();
     
                    }


                    if (ObjOrderModel != null)
                    {
                        ObjReport.OrderCreateDate = ObjOrderModel.CreateDate;
                        ObjReport.OrderEmployee = ObjOrderModel.EmployeeID;
                        ObjEntity.SaveChanges();
                    }




                    if (ObjQuotedModel != null)
                    {
                        ObjReport.QuotedCreateDate = ObjQuotedModel.CreateDate;
                        ObjReport.OrderEmployee = ObjQuotedModel.EmpLoyeeID;
                        ObjReport.QuotedDateSucessDate = ObjQuotedModel.CreateDate;
                        ObjReport.QuotedMoney = ObjQuotedModel.FinishAmount;
                        ObjReport.OrderCreateDate = ObjOrderModel.CreateDate;
                        ObjEntity.SaveChanges();
                     
                    }
                    Console.WriteLine("恢复联系人:" + ObjNoneModel.Bride + "" + ObjNoneModel.Groom);



                    NoneModel.ContactType = 0;
                    if (!string.IsNullOrEmpty(ObjNoneModel.Bride))
                    {
                        NoneModel.ContactMan = ObjNoneModel.Bride;
                        NoneModel.ContactPhone = ObjNoneModel.BrideCellPhone;
                    }
                    else
                    {
                        NoneModel.ContactMan = ObjNoneModel.Groom;
                        NoneModel.ContactPhone = ObjNoneModel.GroomCellPhone;
                    }
                    NoneModel.Partydate = ObjNoneModel.PartyDate;
                    ObjEntity.SaveChanges();

      
                     
               
                }
              
            }
        }
    }
}
