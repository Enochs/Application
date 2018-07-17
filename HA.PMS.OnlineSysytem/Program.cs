using HA.PMS.OnlineSysytem.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem
{
    class Program
    {
        //邀约消息
        public delegate void SetIntiveMessage();


        //执行订单倒计时消息
        public delegate void SetDispatchingMessage();


        /// <summary>
        /// 
        /// </summary>
        public delegate void Targetmanager();

        //创建统计
        public delegate void CreateMsiionSum();

        //执行统计
        public delegate void MissionSum();


        //倒计时
        public delegate void MissionTimerEnd();
        static void Main(string[] args)
        {

            Console.WriteLine(DateTime.Now > DateTime.Now.AddDays(1));
            Console.WriteLine(DateTime.Now < DateTime.Now.AddDays(1));
            //MessAgeforEmpLoyee SetInviteMessage = new MessAgeforEmpLoyee();
            //SetInviteMessage.SetInviteMessage();
            MessAgeforEmpLoyee ObjMessAgeManager = new MessAgeforEmpLoyee();

            //任务统计
            DataMission ObjDataMissionFlow = new DataMission();
            //目标管理
            TargetforEmployee ObjTargetforEmployee = new TargetforEmployee();
            //邀约消息
            SetIntiveMessage StarMessage = ObjMessAgeManager.SetInviteMessage;

            //执行订单倒计时
            SetDispatchingMessage DispatchingMessage = ObjMessAgeManager.SetDispatchingMessageMessage;

            //任务倒计时
            Mission ObjMission = new Mission();
            //目标完成情况统计


            Targetmanager Target = ObjTargetforEmployee.InserTargetforEmployee;

            Target.BeginInvoke(null, null);


            // StarMessage += ObjMessAgeManager.SetOrderMessage;
            //邀约现成
            IAsyncResult asyncResult = StarMessage.BeginInvoke(null, null);

            //执行订单线程
            DispatchingMessage.BeginInvoke(null, null);



            //录入任务统计
            CreateMsiionSum CreateSum = ObjDataMissionFlow.CreateMsiionSum;
            CreateSum.BeginInvoke(null, null);
            //统计任务统计
            MissionSum CreateMissionSumSum = ObjDataMissionFlow.MissionSum;
            CreateMissionSumSum.BeginInvoke(null, null);

            //任务倒计时
            MissionTimerEnd CreateMissionTimerEnd = ObjMission.MissionTimerEnd;
            CreateMissionTimerEnd.BeginInvoke(null, null);

            Console.ReadLine();
        }
    }
}
