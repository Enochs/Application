using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.WBMSService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WbmsService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WbmsService.svc 或 WbmsService.svc.cs，然后开始调试。
    public class WbmsService : IWbmsService
    {
        //任务
        Employee ObjEmployeeBLL = new Employee();

        Message ObjMessageBLL = new Message();

        MissionDetailed ObjMissionDetailed = new MissionDetailed();
        public void DoWork()
        {
        }


        /// <summary>
        /// 客户端登录 
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool ClientLogin(string LoginName, string Password)
        {
            return (ObjEmployeeBLL.EmpLoyeeLogin(LoginName, Password) != null);
        }


        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        public List<string> GetMessAge(string EmployeeName, int PageIndex, out int SourceCount)
        {
            var ObjModelID = ObjEmployeeBLL.GetByLoginName(EmployeeName).EmployeeID;
            List<string> ObjList = new List<string>();
            var ObjDataList = ObjMessageBLL.GetByEmployeeID(20, PageIndex, out SourceCount, ObjModelID);
            foreach (var Objitem in ObjDataList)
            {
                ObjList.Add(Objitem.MessAgeTitle + "," + Objitem.MessageID);
            }
            return ObjList;
        }



        public List<string> GetMission(string EmployeeName, int PageIndex, out int SourceCount)
        {
            int employeeID = ObjEmployeeBLL.GetByLoginName(EmployeeName).EmployeeID;
            List<string> ObjList = new List<string>();

            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("EmployeeID", employeeID));
            parameters.Add(new ObjectParameter("IsDelete", false));
            parameters.Add(new ObjectParameter("IsOver", false));
            parameters.Add(new ObjectParameter("IsLook", false));
            parameters.Add(new ObjectParameter("ChecksState", 3));
            var ObjDataList = ObjMissionDetailed.GetMissionDetailedByWhere(20, PageIndex, out SourceCount, parameters);
            foreach (var Objitem in ObjDataList)
            {
                ObjList.Add(Objitem.MissionName + "," + Objitem.DetailedID);
            }
            return ObjList;
        }
    }
}
