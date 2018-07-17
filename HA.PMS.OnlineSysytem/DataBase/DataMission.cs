using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBase
{

    public class DataMission
    {
        OnlineSysytem.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public void CreateMsiionSum()
        {
 
            while (true)
            {
                var ObjEmpLoyeeList = ObjEntity.Sys_Employee.Where(C => C.IsDelete == false).ToList();
                foreach (var ObjEmpLoyeeItem in ObjEmpLoyeeList)
                {
                    int MissionSum = ObjEntity.FL_MissionDetailed.Count(C => C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID && C.PlanDate.Value.Year == DateTime.Now.Year);
                    if (MissionSum > 0)
                    {
                        DateTime ObjDtNow = DateTime.Parse(DateTime.Now.ToShortDateString());
                        var ObjMissionSumModelList = ObjEntity.FL_EmpLoyeeMissionSum.Where(C => C.CreateDate == ObjDtNow && C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID).ToList();
                        if (ObjMissionSumModelList.Count == 0)
                        {
                            FL_EmpLoyeeMissionSum ObjMissionSumModel = new FL_EmpLoyeeMissionSum();
                            ObjMissionSumModel.FlowSum = MissionSum;
                            ObjMissionSumModel.DoingSum = 0;
                            ObjMissionSumModel.FinishRatio = 0;
                            ObjMissionSumModel.FinishSUm = 0;
                            ObjMissionSumModel.NewSum = 0;
                            ObjMissionSumModel.WaitDoingSum = 0;
                            ObjMissionSumModel.Updatetime = DateTime.Now;
                            ObjMissionSumModel.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                            ObjMissionSumModel.EmpLoyeeID = ObjEmpLoyeeItem.EmployeeID;
                            ObjMissionSumModel.EmployeeName = ObjEmpLoyeeItem.EmployeeName;
                            ObjMissionSumModel.DepartmentID = ObjEmpLoyeeItem.DepartmentID;

                            ObjEntity.FL_EmpLoyeeMissionSum.Add(ObjMissionSumModel);
                            ObjEntity.SaveChanges();
                            Console.WriteLine("录入任务统计:" + ObjEmpLoyeeItem.EmployeeName);
                        }


                    }
                }
                Console.WriteLine("录入任务统计完毕");
                System.Threading.Thread.Sleep(84400000);
            }
        }

        public void MissionSum()
        {
            int FinishSum = 1;
            int Doing = 1;
            decimal FinishRatio = 1;
            int WaitDoingSum = 1;
            int NewSum = 1;


            while (true)
            {
                var ObjEmpLoyeeList = ObjEntity.Sys_Employee.Where(C => C.IsDelete == false).ToList();
                foreach (var ObjEmpLoyeeItem in ObjEmpLoyeeList)
                {
                    System.Threading.Thread.Sleep(1000);
                    int MissionSum = ObjEntity.FL_MissionDetailed.Count(C => C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID && C.PlanDate.Value.Year == DateTime.Now.Year);
                    if (MissionSum > 0)
                    {
                        DateTime ObjDtNow = DateTime.Parse(DateTime.Now.ToShortDateString());


                        FL_EmpLoyeeMissionSum ObjMissionSumModel = ObjEntity.FL_EmpLoyeeMissionSum.FirstOrDefault(C => C.CreateDate == ObjDtNow && C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID);
                        if (ObjMissionSumModel != null)
                        {
                            Doing = ObjEntity.FL_MissionDetailed.Count(C => C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID && C.PlanDate.Value.Year == DateTime.Now.Year&&C.IsLook==true);
                            FinishSum = ObjEntity.FL_MissionDetailed.Count(C => C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID && C.PlanDate.Value.Year == DateTime.Now.Year && C.IsOver == true);
                            FinishRatio = FinishSum / MissionSum;
                            WaitDoingSum = ObjEntity.FL_MissionDetailed.Count(C => C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID && C.PlanDate.Value.Year == DateTime.Now.Year && C.IsOver == false);
                            NewSum = ObjEntity.FL_MissionDetailed.Count(C => C.EmpLoyeeID == ObjEmpLoyeeItem.EmployeeID && C.PlanDate.Value.Year == DateTime.Now.Year && C.IsLook == false&&C.IsOver==false);

                            ObjMissionSumModel.FlowSum = MissionSum;
                            ObjMissionSumModel.DoingSum = Doing;
                            ObjMissionSumModel.FinishRatio = FinishRatio;
                            ObjMissionSumModel.FinishSUm = FinishSum;
                            ObjMissionSumModel.NewSum = NewSum;
                            ObjMissionSumModel.WaitDoingSum = WaitDoingSum;
                            ObjMissionSumModel.Updatetime = DateTime.Now;
                            //ObjEntity.FL_EmpLoyeeMissionSum.Add(ObjMissionSumModel);
                            ObjEntity.SaveChanges();
                            Console.WriteLine("更新任务统计:" + ObjEmpLoyeeItem.EmployeeName);
                            System.Threading.Thread.Sleep(100);
                        }
                    }
            
                }
                Console.WriteLine("更新任务统计:完成");
                System.Threading.Thread.Sleep(3500000);

            }

        }

    }
}
