using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using HA.PMS.Service.Filesrver;
 
using HA.PMS.BLLInterface;

namespace HA.PMS.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            ///开启服务
            using (ServiceHost ObjHost = new ServiceHost(typeof(FileServer)))
            {

                var Entpoint = new WSHttpBinding();
                Entpoint.Security.Mode = SecurityMode.None;
                ObjHost.AddServiceEndpoint(typeof(IFileServer), Entpoint, "http://127.0.0.1:9013/ServiceBin");
                if (ObjHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior ObjBehavior = new ServiceMetadataBehavior();
                    ObjBehavior.HttpGetEnabled = true;
                    ObjBehavior.HttpGetUrl = new Uri("http://127.0.0.1:9013/ServiceBin/metada");

                    ObjHost.Description.Behaviors.Add(ObjBehavior);
                }

                ObjHost.Opened += delegate
                {
                    Console.WriteLine("服务已经启动!");
                };

                ObjHost.Open();
                Console.ReadLine();
            }
        }

        

    }
}
