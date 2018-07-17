using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLInterface
{
    [ServiceContract(Name = "ServiceBin")]
    public interface IFileServer
    {
        //自动更新
        [OperationContract]
        Byte[] AuotUpdate(string SN,string FileName,int Index);

        //获取文件数量
        [OperationContract]
        int GetFileSum();


        //验证序列号
        [OperationContract]
        List<string> ChecksSN(string SN);


        /// <summary>
        /// 安装验证序列号
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        [OperationContract]
        string CheckSNforCustomer(string SN);


        ////下载更新
        //[OperationContract]
        //Byte[] AuotDownLoad(string SN, string FileName, int Index, out decimal Lenght);


    }
}
