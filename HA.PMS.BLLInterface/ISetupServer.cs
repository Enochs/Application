using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLInterface
{
    [ServiceContract(Name = "ServiceBin")]
    public interface ISetupServer
    {
        //下载更新
        [OperationContract]
        Byte[] AuotDownLoad(string SN, string FileName, int Index, out decimal Lenght);
    }
}
