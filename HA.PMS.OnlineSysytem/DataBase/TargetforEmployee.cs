using HA.PMS.OnlineSysytem.DataBaseAutoUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBase
{
    public class TargetforEmployee
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 注入
        /// </summary>
        public void InserTargetforEmployee()
        {
            while (true)
            {
                OrderFinalCost ObjCost = new OrderFinalCost();
                ObjCost.SetFinalCost();
                var EmpLoyeeList = ObjEntity.Sys_Employee.ToList();
                var TargetModel = ObjEntity.FL_Target.ToList();
                foreach (var ObjItem in EmpLoyeeList)
                {
                    System.Threading.Thread.Sleep(100);
                    foreach (var ObjTarget in TargetModel)
                    {
                        System.Threading.Thread.Sleep(100);
                        InserOrUpdateTargetFinish(ObjTarget.TargetID, ObjItem.EmployeeID);
                    }
                    Console.WriteLine(ObjItem.EmployeeName + ":目标统计完成!");
                }
    

                //成本核算
 
                System.Threading.Thread.Sleep(86390000);
            }
        }


        

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="TargetID"></param>
        /// <param name="EmpLoyeeID"></param>
        public void InserOrUpdateTargetFinish(int TargetID, int EmpLoyeeID)
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            for (int M = 1; M < 12; M++)
            {
                var FinishModel = ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.Year == Year && C.TargetID == TargetID && C.EmployeeID == EmpLoyeeID);
                if (FinishModel != null)
                {
                    var TargetModel = ObjEntity.FL_Target.FirstOrDefault(C => C.TargetID == FinishModel.TargetID);
                    if (TargetModel != null)
                    {

                        var FinishValue = GetFinishList(TargetModel.TargetID, M, FinishModel.EmployeeID);
                        //if (FinishValue == 0)
                        //{
                        //    //FinishValue =0;
                        //    FinishValue = 15000;
                        //}
                        //FinishValue = 15000;
                        //判断具体该给谁赋值
                        var Key = ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.Year == Year && C.TargetID == TargetID && C.EmployeeID == EmpLoyeeID).FinishKey;
                        var OverModel = ObjEntity.FL_FinishTargetSum.First(C => C.FinishKey == Key);
                        switch (M)
                        {
                            case 1:
                                OverModel.MonthFinsh1 = FinishValue;
                                break;
                            case 2:
                                OverModel.MonthFinish2 = FinishValue;
                                break;
                            case 3:
                                OverModel.MonthFinish3 = FinishValue;
                                break;
                            case 4:
                                OverModel.MonthFinish4 = FinishValue;
                                break;
                            case 5:
                                OverModel.MonthFinish5 = FinishValue;
                                break;
                            case 6:
                                OverModel.MonthFinish6 = FinishValue;
                                break;
                            case 7:
                                OverModel.MonthFinish7 = FinishValue;
                                break;
                            case 8:
                                OverModel.MonthFinish8 = FinishValue;
                                break;
                            case 9:
                                OverModel.MonthFinish9 = FinishValue;
                                break;
                            case 10:
                                OverModel.MonthFinish10 = FinishValue;
                                break;
                            case 11:
                                OverModel.MonthFinish11 = FinishValue;
                                break;
                            case 12:
                                OverModel.MonthFinish12 = FinishValue;
                                break;

                        }

                        if (FinishValue > 0)
                        {
                            OverModel.FinishSum = FinishValue;
                            OverModel.OverYearFinishSum = FinishValue;
                            OverModel.PlanSum = FinishModel.MonthPlan1 + FinishModel.MonthPlan2 + FinishModel.MonthPlan3 + FinishModel.MonthPlan4 + FinishModel.MonthPlan5 + FinishModel.MonthPlan6 + FinishModel.MonthPlan7 + FinishModel.MonthPlan8 + FinishModel.MonthPlan9 + FinishModel.MonthPlan10 + FinishModel.MonthPlan11 + FinishModel.MonthPlan12;
                            OverModel.UpdateTime = DateTime.Now;
                            //OverModel.FinishKey = 3;
                            //ObjEntity.FL_FinishTargetSum.Attach(FinishModel);
                            int i = ObjEntity.SaveChanges();
                        }

                    }
                }
            }
            //if (FinishModel == null)
            //{
            //  FL_FinishTargetSum ObjModel= new FL_FinishTargetSum();
            //  ObjModel.EmployeeID = EmpLoyeeID;
            //  ObjModel.TargetID = TargetID;
            //  ObjModel.Year = DateTime.Now.Year;
            //  ObjModel.YearFinish = 0;
            //  ObjModel.MonthFinish = 0;
            //  ObjModel.QuarterFinish = 0;
            //  ObjModel.Month = DateTime.Now.Month;

            //  ObjEntity.FL_FinishTargetSum.Add(ObjModel);
            //  ObjEntity.SaveChanges();
            //}
            //else
            //{
            //    FinishModel.YearFinish = GetYearFinish(TargetID,EmpLoyeeID,DateTime.Now);
            //    FinishModel.MonthFinish = GetMonthFinish(TargetID, EmpLoyeeID, DateTime.Now);
            //    FinishModel.QuarterFinish = GetQuarterFinish(TargetID, EmpLoyeeID, DateTime.Now);
            //    ObjEntity.SaveChanges();
            //}

        }


        /// <summary>
        /// 统计数据
        /// </summary>
        /// <param name="TargetID"></param>
        /// <param name="Month"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        private decimal GetFinishList(int TargetID, int Month, int EmployeeID)
        {
            decimal ReturnValue = 0;
            switch (TargetID)
            {
                //有效信息数
                case 1:
                    ReturnValue = ObjEntity.View_GetInviteCustomers.Where(C => C.EmpLoyeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State > 3).Count();
                    break;
                //有效客源率
                case 2:
                    ReturnValue = ObjEntity.View_GetInviteCustomers.Where(C => C.EmpLoyeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State > 3).Count();
                    ReturnValue = ReturnValue / ObjEntity.View_GetInviteCustomers.Where(C => C.EmpLoyeeID == EmployeeID && C.State == 1).Count();
                    break;
                //邀约成功数
                case 3:
                    ReturnValue = ObjEntity.View_GetInviteCustomers.Where(C => C.EmpLoyeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State >= 6).Count();
                    break;
                //邀约成功率
                case 4:
                    //获取成功的总数
                    var SucessCount = ObjEntity.View_GetInviteCustomers.Where(C => C.EmpLoyeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State >= 6 && C.IsDelete == false).Count();
                    //获取本月邀约的总数
                    var InviteCount = ObjEntity.View_GetInviteCustomers.Where(C => C.EmpLoyeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.IsDelete == false).Count();
                    //保留两位小数
                    ReturnValue = decimal.Parse((decimal.Parse(SucessCount.ToString()) / decimal.Parse(InviteCount.ToString())).ToString("0.00"));

                    break;
                //成功预定数
                case 5:
                    //状态大于等于13(成功预定) 小于等于29(流失)
                    ReturnValue = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State >= 13 && C.State <= 29).Count();
                    break;
                //成功预定率
                case 6:
                    //先获取成功预定总量
                    var SucessOrder = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State >= 13 && C.State <= 29).Count();
                    //获取本月所有订单
                    var MonthOrder = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month).Count();
                    ReturnValue = decimal.Parse((decimal.Parse(SucessOrder.ToString()) / decimal.Parse(MonthOrder.ToString())).ToString("0.00"));
                    // ReturnValue = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State == 13).Count();
                    break;
                //当期执行订单总额
                case 7:
                    //婚期在当月的
                    var SumValue = ObjEntity.View_Dispatching.Where(C => C.EmployeeID == EmployeeID && C.PartyDate.Value.Year == DateTime.Now.Year && C.PartyDate.Value.Month ==Month && C.Isover == false).Sum(C => C.FinishAmount);
                    if (SumValue != null)
                    {
                        ReturnValue = SumValue.Value;

                    }
                    else
                    {
                        ReturnValue = 0;
                    }
                    // ReturnValue = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State == 13).Count();
                    break;
                //毛利率
                case 8:
                    ReturnValue = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State == 13).Count();
                    break;
                //销售额(当期新增订单金额)
                case 9:
                    var IteMlist = ObjEntity.View_CustomerQuoted.Where(C => C.EmpLoyeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.IsDispatching < 4 && C.FinishAmount != null).ToList();
                    if (IteMlist.Count > 0)
                    {
                        ReturnValue = IteMlist.Sum(C => C.FinishAmount).Value;
                    }
                    else
                    {
                        ReturnValue = 0;
                    }
                    break;
                //执行订单数
                case 10:
                    ReturnValue = ObjEntity.View_Dispatching.Where(C => C.EmployeeID == EmployeeID && C.PartyDate.Value.Year == DateTime.Now.Year && C.PartyDate.Value.Month == Month && C.PartyDate.Value.Day > DateTime.Now.Day).Count();
                    break;
                //满意度
                case 11:
                    ReturnValue = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State == 13).Count();
                    break;
                case 12:
                    //投诉率
                    ReturnValue = ObjEntity.View_GetOrderCustomers.Where(C => C.EmployeeID == EmployeeID && C.CreateDate.Value.Year == DateTime.Now.Year && C.CreateDate.Value.Month == Month && C.State == 13).Count();
                    break;
                //执行订单总数
                case 13:
                    ReturnValue = ObjEntity.View_Dispatching.Where(C => C.EmployeeID == EmployeeID && C.PartyDate.Value.Year == DateTime.Now.Year && C.PartyDate.Value.Month == Month).Count();
                    break;
                //订单毛利率
                case 14:
                    break;

            }

            return ReturnValue;
        }


        public decimal GetYearFinish(int TargetID, int EmpLoyeeID, DateTime Timer)
        {
            return 0;
        }



        public decimal GetMonthFinish(int TargetID, int EmpLoyeeID, DateTime Timer)
        {
            return 0;
        }



        public decimal GetQuarterFinish(int TargetID, int EmpLoyeeID, DateTime Timer)
        {
            return 0;
        }
    }
}
