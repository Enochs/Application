using HA.PMS.BLLInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.EasyMessage
{
    public class Service
    {

        /// <summary>
        /// 登录想爱他
        /// </summary>
        /// <param name="Loginname"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool GetLoginService(string Loginname, string Password)
        {
            var ObjBinding = new BasicHttpBinding();

            string ServiceAddress = GetConnectionString("ServiceAddress");
            using (ChannelFactory<IWbmsService> ObjChannelFactory = new ChannelFactory<IWbmsService>(ObjBinding, ServiceAddress))
            {
                IWbmsService ObjFileServer = ObjChannelFactory.CreateChannel();

                if (ObjFileServer.ClientLogin(Loginname, Password))
                {
                    return true;
                }

            }
            return false;
        }





        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="Loginname"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public List<string> GetMessageService(string Loginname, int Index, out int SourceCount)
        {
            var ObjBinding = new BasicHttpBinding();

            string ServiceAddress = GetConnectionString("ServiceAddress");
            using (ChannelFactory<IWbmsService> ObjChannelFactory = new ChannelFactory<IWbmsService>(ObjBinding, ServiceAddress))
            {
                IWbmsService ObjFileServer = ObjChannelFactory.CreateChannel();
                return ObjFileServer.GetMessAge(Loginname, Index, out SourceCount);

            }

        }



        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="Loginname"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public List<string> GetMissionService(string Loginname, int Index, out int SourceCount)
        {
            var ObjBinding = new BasicHttpBinding();

            string ServiceAddress = GetConnectionString("ServiceAddress");
            using (ChannelFactory<IWbmsService> ObjChannelFactory = new ChannelFactory<IWbmsService>(ObjBinding, ServiceAddress))
            {
                IWbmsService ObjFileServer = ObjChannelFactory.CreateChannel();
                return ObjFileServer.GetMission(Loginname, Index, out SourceCount);

            }

        }
        /// <summary>
        /// md5加密算法
        /// </summary>
        /// <returns></returns>
        public string GetMd5Password(string Password)
        {
            byte[] result = Encoding.Default.GetBytes(Password);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        private static string GetConnectionString(string connectionName)
        {

            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();

        }
    }
}
