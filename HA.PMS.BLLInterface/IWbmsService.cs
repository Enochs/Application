using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HA.PMS.BLLInterface
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IWbmsService”。
    [ServiceContract]
    public interface IWbmsService
    {
        [OperationContract]
        void DoWork();

        
        

        //登录
        [OperationContract]
        bool ClientLogin(string EmployeeName,string Password);



        //获取需要处理的消息
        [OperationContract]
        List<string> GetMessAge(string EmployeeName,int PageIndex, out int SourceCount);

        //获取需要处理的任务
        [OperationContract]
        List<string> GetMission(string EmployeeName, int PageIndex, out int SourceCount);
         
    }
}
