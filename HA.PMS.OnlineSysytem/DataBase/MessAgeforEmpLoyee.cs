using HA.PMS.OnlineSysytem.Serverice;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBase
{
    public class MessAgeforEmpLoyee
    {
        OnlineSysytem.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 开始自动触发
        /// </summary>
        public void SetMessAge()
        {



        }

        /// <summary>
        /// 判断是否为主管
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public bool IsManager(int? EmployeeID)
        {
            if (ObjEntity.Sys_Department.Count(C => C.DepartmentManager == EmployeeID) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 根据主键返回单个雇员信息
        /// </summary>
        /// <param name="KeyID">主键值</param>
        /// <returns>根据查询之后的结果，如果为空则返回默认实例</returns>
        public Sys_Employee GetByID(int? KeyID)
        {
            if (KeyID.HasValue)
            {
                Sys_Employee emp = ObjEntity.Sys_Employee.FirstOrDefault(
                    C => C.EmployeeID == KeyID);
                if (emp != null)
                {
                    return emp;
                }

            }

            return new Sys_Employee();

        }

        /// <summary>
        /// 获取本人的上级审核人
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public int GetMineCheckEmployeeID(int? EmployeeID)
        {

            if (IsManager(EmployeeID))
            {
                var ObjDepartmentID = 0;
                ObjDepartmentID = this.GetByID(EmployeeID).DepartmentID;
                var Parent = ObjEntity.Sys_Department.FirstOrDefault(D => D.DepartmentID == ObjDepartmentID).Parent;
                var ObjDepartment = ObjEntity.Sys_Department.FirstOrDefault(C => C.DepartmentID == Parent);
                if (ObjDepartment == null)
                {
                    return 0;
                }
                else
                {
                    return ObjDepartment.DepartmentManager.Value;
                }
            }
            else
            {
                var ObjDepartmentID = this.GetByID(EmployeeID).DepartmentID;
                return ObjEntity.Sys_Department.FirstOrDefault(D => D.DepartmentID == ObjDepartmentID).DepartmentManager.Value;
            }
        }

        /// <summary>
        /// 超过三天未执行邀约
        /// </summary>
        public void SetInviteMessage()
        {
            UpdateDis();
            while (true)
            {
    
                //    //var GetWhereParList = new List<ObjectParameter>(); 
                DateTime ObjTimerEnd = DateTime.Now.AddDays(3);
                //Employee EmployeeBLL = new Employee();
                //FL_Message ObjMessageModel = new FL_Message();
                // Message ObjMessageBLL = new Message();

                FL_Message ObjMessageModel = new FL_Message();

                //报警统计信息  进入指挥台
                FL_WarningMessage ObjWareMessage = new FL_WarningMessage();

                var DataList = ObjEntity.View_GetInviteCustomers.Where(C => C.State == 3).ToList(); //ObjInvtieBLL.GetTelCustomerByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, GetWhereParList); //PageDataTools<View_GetTelmarketingCustomers>.AddtoPageSize(ObjInvtieBLL.GetTelCustomerByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, SerchControl.ObjParList));
                foreach (var Objitem in DataList)
                {
                    System.Threading.Thread.Sleep(5000);
                    if ((ObjTimerEnd.DayOfYear - Objitem.CreateDate.Value.DayOfYear) > 3)
                    {
                        //给个人发消息
                        ObjMessageModel = new FL_Message();
                        ObjMessageModel.EmployeeID = Objitem.EmpLoyeeID;
                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message = "超过三天未执行邀约";
                        ObjMessageModel.MessAgeTitle = "邀约警告";
                        ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + Objitem.CustomerID;
                        ObjMessageModel.CreateEmployeename = "系统";
                        ObjMessageModel.CreateDate = DateTime.Now;
                        ObjEntity.FL_Message.Add(ObjMessageModel);
                        ObjEntity.SaveChanges();


                        //自动报警信息
                        ObjWareMessage.CreateDate = DateTime.Now;
                        ObjWareMessage.EmpLoyeeID = Objitem.EmpLoyeeID;
                        ObjWareMessage.IsLook = false;
                        ObjWareMessage.MessAgeTitle = "邀约报警";
                        ObjWareMessage.ResualtAddress = "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID="+Objitem.CustomerID;
                        ObjWareMessage.managerEmployee = GetMineCheckEmployeeID(Objitem.EmpLoyeeID);
                        ObjWareMessage.FinishKey = Objitem.InviteID;
                        ObjWareMessage.State = 3;
                        ObjWareMessage.CustomerID = Objitem.CustomerID;
                        ObjEntity.FL_WarningMessage.Add(ObjWareMessage);
                        ObjEntity.SaveChanges();

                    }
                }

             

                System.Console.WriteLine("邀约消息提示完成");

                SetOrderMessage();
                // System.Threading.Thread.Sleep(5000);
                System.Threading.Thread.Sleep(84400000);


            }
        }




        /// <summary>
        ///  
        /// </summary>
        public void SetOrderMessage()
        {

            while (true)
            {

                //    //var GetWhereParList = new List<ObjectParameter>(); 
                DateTime ObjTimerEnd = DateTime.Now.AddDays(3);
                //Employee EmployeeBLL = new Employee();
                //FL_Message ObjMessageModel = new FL_Message();
                // Message ObjMessageBLL = new Message();

                FL_Message ObjMessageModel = new FL_Message();

                //报警统计信息  进入指挥台
                FL_WarningMessage ObjWareMessage = new FL_WarningMessage();

                var DataList = ObjEntity.View_GetOrderCustomers.Where(C => C.State == 8).ToList(); //ObjInvtieBLL.GetTelCustomerByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, GetWhereParList); //PageDataTools<View_GetTelmarketingCustomers>.AddtoPageSize(ObjInvtieBLL.GetTelCustomerByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, SerchControl.ObjParList));
                foreach (var Objitem in DataList)
                {
                    System.Threading.Thread.Sleep(5000);
                    if ((ObjTimerEnd.DayOfYear - Objitem.CreateDate.Value.DayOfYear) > 7)
                    {
                        //给个人发消息
                        ObjMessageModel = new FL_Message();
                        ObjMessageModel.EmployeeID = Objitem.EmployeeID;
                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message = "超过七天未进行跟单";
                        ObjMessageModel.MessAgeTitle = "跟单警告";
                        ObjMessageModel.KeyWords = "/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=" + Objitem.CustomerID + "&OrderID=" + Objitem.OrderID + "&FlowOrder=1";
                        ObjMessageModel.CreateEmployeename = "系统";
                        ObjMessageModel.CreateDate = DateTime.Now;
                        ObjEntity.FL_Message.Add(ObjMessageModel);
                        ObjEntity.SaveChanges();


                        //自动报警信息
                        ObjWareMessage.CreateDate = DateTime.Now;
                        ObjWareMessage.EmpLoyeeID = Objitem.EmployeeID;
                        ObjWareMessage.IsLook = false;
                        ObjWareMessage.MessAgeTitle = "跟单警告";
                        ObjMessageModel.KeyWords = "/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=" + Objitem.CustomerID + "&OrderID=" + Objitem.OrderID + "&FlowOrder=1";
                        ObjWareMessage.managerEmployee = GetMineCheckEmployeeID(Objitem.EmployeeID);
                        ObjWareMessage.FinishKey = Objitem.OrderID;
                        ObjWareMessage.State = 3;
                        ObjWareMessage.CustomerID = Objitem.CustomerID;
                        ObjEntity.FL_WarningMessage.Add(ObjWareMessage);
                        ObjEntity.SaveChanges();

                    }
                }



                System.Console.WriteLine("邀约消息提示完成");
                // System.Threading.Thread.Sleep(5000);
                System.Threading.Thread.Sleep(84400000);


            }
        }
 

        /// <summary>
        /// 更新错误数据
        /// </summary>
        public void UpdateDis()
        {

            //var ObjProductModel=ObjEntity.FL_ProductforDispatching.ToList();
            //foreach (var ObjProduct in ObjProductModel)
            //{
            //    if (ObjProduct.SupplierName == "库房" || ObjProduct.SupplierName == null || ObjProduct.SupplierName == string.Empty)
            //    {
            //        ObjProduct.OrderID = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == ObjProduct.DispatchingID).OrderID;
            //        ObjProduct.CustomerID = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == ObjProduct.DispatchingID).CustomerID;
            //        ObjProduct.SupplierID = 0;
            //        ObjEntity.SaveChanges();

            //    }
            //    else
            //    {
            //        var ObjUpdateModel = ObjEntity.FD_Supplier.FirstOrDefault(C=>C.Name==ObjProduct.SupplierName);
            //        if (ObjUpdateModel != null)
            //        {
            //            ObjProduct.SupplierID = ObjUpdateModel.SupplierID;
            //            ObjProduct.OrderID = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == ObjProduct.DispatchingID).OrderID;
            //            ObjProduct.CustomerID = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == ObjProduct.DispatchingID).CustomerID;
            //            ObjEntity.SaveChanges();
            //        }
            //        else
            //        {
            //            ObjProduct.OrderID = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == ObjProduct.DispatchingID).OrderID;
            //            ObjProduct.CustomerID = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == ObjProduct.DispatchingID).CustomerID;
            //            ObjProduct.SupplierID = 0;
            //            ObjEntity.SaveChanges();
            //        }
                    

                  
            //    }
            //    Console.WriteLine("执行表数据更新完毕");
            //    System.Threading.Thread.Sleep(100);
            //}
            

            AutoUpdate ObjAutoUpdateBLL = new AutoUpdate();

            ObjAutoUpdateBLL.CreateSince();
            Console.WriteLine("系统服务启动成功");
            
            //var ObjDisList= ObjEntity.FL_Dispatching.ToList();
            //foreach (var Objitem in ObjDisList)
            //{
            //    var CustomerID= ObjEntity.FL_Order.FirstOrDefault(C => C.OrderID == Objitem.OrderID).CustomerID;

            //    Objitem.CustomerID = CustomerID;
            //    ObjEntity.SaveChanges();
          
            //    System.Threading.Thread.Sleep(300);

            //    var ObjCostItem= ObjEntity.FL_Cost.FirstOrDefault(C => C.CustomerID == Objitem.CustomerID);
            //    if (ObjCostItem == null)
            //    {
            //        var objCostModel = new FL_Cost();
            //        objCostModel.CustomerID = Objitem.CustomerID;
            //        objCostModel.OrderID = Objitem.OrderID;
            //        objCostModel.ProfitMargin = 0;
            //        objCostModel.TotalAmount = 0;
            //        objCostModel.Cost = 0;
            //        objCostModel.CreateDate = DateTime.Now;
            //        objCostModel.CreateEmpLoyee = Objitem.EmployeeID;
                   

            //        ObjEntity.FL_Cost.Add(objCostModel);
            //        ObjEntity.SaveChanges();

            //        Console.WriteLine("财务表更新完毕");
            //        System.Threading.Thread.Sleep(100);
                   
            //    }

            //}


            //var ObjAripriseList = ObjEntity.FL_OrderAppraise.ToList();
            //foreach (var ObjAripriseModel in ObjAripriseList)
            //{

            //    if (ObjAripriseModel.Type == 3)
            //    { 
            //        var ObjMOdel= ObjEntity.FD_Supplier.FirstOrDefault(C=>C.Name==ObjAripriseModel.AppraiseTitle);
            //        if(ObjMOdel!=null)
            //        {
            //            ObjAripriseModel.SupplierID = ObjMOdel.SupplierID;
               
            //            ObjEntity.SaveChanges();
            //        }
            //    }

            //    Console.WriteLine("评价表更新完毕");
            //    System.Threading.Thread.Sleep(100);
            //}


            //var ObjFinilCost = ObjEntity.FL_OrderfinalCost.ToList();
            //foreach (var FiniCost in ObjFinilCost)
            //{

            //    if (FiniCost.KindType == 1)
            //    {
            //        var ObjMOdel = ObjEntity.FD_Supplier.FirstOrDefault(C => C.Name == FiniCost.ServiceContent);
            //        if (ObjMOdel != null)
            //        {
            //            FiniCost.SupplierID = ObjMOdel.SupplierID;
    
            //            ObjEntity.SaveChanges();
            //        }
            //    }
            //    Console.WriteLine("供应商更新完毕");
            //    System.Threading.Thread.Sleep(100);
            //}



        }

        //执行订单婚期倒计时
        public void SetDispatchingMessageMessage()
        {
            while (true)
            {
                
                System.Threading.Thread.Sleep(3000);
                DateTime ObjTimerEnd = DateTime.Now.AddDays(3);
                FL_Message ObjMessageModel = new FL_Message();
                int ObjYear=DateTime.Now.Year;
                var DataList = ObjEntity.View_Dispatching.Where(C => C.Isover == false && C.PartyDate.Value.Year == ObjYear).ToList(); //ObjInvtieBLL.GetTelCustomerByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, GetWhereParList); //PageDataTools<View_GetTelmarketingCustomers>.AddtoPageSize(ObjInvtieBLL.GetTelCustomerByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, SerchControl.ObjParList));
                foreach (var Objitem in DataList)
                {
                    if (Objitem.PartyDate.Value.DayOfYear - ObjTimerEnd.DayOfYear == 7)
                    {
                        ObjMessageModel = new FL_Message();
                        ObjMessageModel.EmployeeID = Objitem.EmployeeID;
                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message = Objitem.Bride + "的" + "婚期倒计时七天";
                        ObjMessageModel.MessAgeTitle = "婚期倒计时7天";
                        //ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Invite/Customer/Donotinvite.aspx";
                        ObjMessageModel.CreateEmployeename = "系统";
                        ObjMessageModel.CreateDate = DateTime.Now;
                        ObjEntity.FL_Message.Add(ObjMessageModel);
                        ObjEntity.SaveChanges();

                        Console.WriteLine("婚期倒计时七天" + Objitem.Bride);
                    }
                }
                System.Threading.Thread.Sleep(15000);
                foreach (var Objitem in DataList)
                {
                    if (Objitem.PartyDate.Value.DayOfYear - ObjTimerEnd.DayOfYear == 3)
                    {
                        ObjMessageModel = new FL_Message();
                        ObjMessageModel.EmployeeID = Objitem.EmployeeID;
                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message = Objitem.Bride + "的" + "婚期倒计时三天";
                        ObjMessageModel.MessAgeTitle = "婚期倒计时3天";
                        //ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Invite/Customer/Donotinvite.aspx";
                        ObjMessageModel.CreateEmployeename = "系统";
                        ObjMessageModel.CreateDate = DateTime.Now;
                        ObjEntity.FL_Message.Add(ObjMessageModel);
                        ObjEntity.SaveChanges();

                        Console.WriteLine("婚期倒计时3天" + Objitem.Bride);
                    }
                }
                System.Threading.Thread.Sleep(15000);
                foreach (var Objitem in DataList)
                {
                    if (Objitem.PartyDate.Value.DayOfYear - ObjTimerEnd.DayOfYear==1)
                    {
                        ObjMessageModel = new FL_Message();
                        ObjMessageModel.EmployeeID = Objitem.EmployeeID;
                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message =Objitem.Bride+"的"+"婚期倒计时一天";
                        ObjMessageModel.MessAgeTitle = "婚期倒计时1天";
                        //ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Invite/Customer/Donotinvite.aspx";
                        ObjMessageModel.CreateEmployeename = "系统";
                        ObjMessageModel.CreateDate = DateTime.Now;
                        ObjEntity.FL_Message.Add(ObjMessageModel);
                        ObjEntity.SaveChanges();

                        Console.WriteLine("婚期倒计时1天" + Objitem.Bride);
                    }
                }
                System.Threading.Thread.Sleep(15000);
                foreach (var Objitem in DataList)
                {
                    if (Objitem.PartyDate <= DateTime.Now && Objitem.PartyDate>DateTime.Parse("2000-1-1"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        var ObjUpdateModel = ObjEntity.FL_Dispatching.FirstOrDefault(C => C.DispatchingID == Objitem.DispatchingID);

                        ObjUpdateModel.Isover = true;
                        ObjEntity.SaveChanges();
                        Console.WriteLine("婚期已过,客户为:" + Objitem.Bride);

                        var QuotedEmployeeID = ObjEntity.FL_QuotedPrice.FirstOrDefault(C => C.QuotedID == ObjUpdateModel.QuotedID).EmpLoyeeID;

                        try
                        {
                            CreateMessageforOver(ObjUpdateModel.QuotedEmpLoyee, ObjUpdateModel.CustomerID);
                        }
                        catch
                        { 
                            
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }

                UpdateCustomer();
                System.Threading.Thread.Sleep(5000);
                System.Threading.Thread.Sleep(83400000);
 
 
            }
        }


        public void CreateMessageforOver(int? EmpLoyeeID,int? Brid)
        {
            if (Brid > 0)
            {

                FL_Message ObjMessageModel = new FL_Message();

                ObjMessageModel.EmployeeID = EmpLoyeeID;

                ObjMessageModel.MissionID = 0;
                ObjMessageModel.IsDelete = false;
                ObjMessageModel.IsLook = false;
                ObjMessageModel.Message = "婚期已过，可以进行项目评价：" + DateTime.Now;
                ObjMessageModel.MessAgeTitle = ObjEntity.FL_Customers.FirstOrDefault(C => C.CustomerID == Brid).Bride + "执行评价";

                ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Carrytask/CarrytaskAppraise.aspx";
                ObjMessageModel.CreateEmployeename = ObjEntity.Sys_Employee.FirstOrDefault(C => C.EmployeeID == EmpLoyeeID).EmployeeName;
                ObjEntity.FL_Message.Add(ObjMessageModel);
                ObjEntity.SaveChanges();


                System.Threading.Thread.Sleep(1000);


            }
        }

        public void UpdateCustomer()
        {
            DateTime EndDate=DateTime.Parse("2000-1-1");
            var ObjCustomerModel = ObjEntity.FL_Customers.Where(C => C.FinishOver == null && C.PartyDate < DateTime.Now && C.PartyDate > EndDate).ToList();

            foreach (var Objitem in ObjCustomerModel)
            {
                try
                {
                    Objitem.FinishOver = true;
                    ObjEntity.SaveChanges();
                    Console.WriteLine(Objitem.Bride + "婚期已过");
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                { }
            }
        }


        public void SetNoneMessage()
        { }


    }
}
