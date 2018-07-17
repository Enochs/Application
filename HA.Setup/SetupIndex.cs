using HA.PMS.BLLInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.ServiceModel;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace HA.Setup
{
    public partial class Setup : Form
    {

        //默认密钥向量
        //private static string Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        string Vision = "10";//版本号
        public Setup()
        {
                
            InitializeComponent();
        }


        /// <summary>
        /// 验证客户端电脑是否合法有效
        /// </summary>
        /// <returns></returns>
        private string ClientChecking()
        {
            string CPUID = string.Empty;//cpu序列号  
            string HDID = string.Empty;
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                CPUID = FindComputerCPU_ID();

            }

            //获取硬盘ID  

            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDID = (string)mo.Properties["Model"].Value;

            }

            return HDID;
        }


        /// <summary>
        /// 查找CPUID
        /// </summary>
        /// <returns></returns>
        public static string FindComputerCPU_ID()
        {
            ManagementScope ms = new ManagementScope("root\\cimv2");
            ms.Connect();
            ManagementObjectSearcher sysinfo = new ManagementObjectSearcher(ms, new SelectQuery("Win32_Processor"));
            string cpuId = "";
            foreach (ManagementObject sys in sysinfo.Get())
            {
                cpuId = sys["ProcessorId"].ToString();
            }
            return cpuId;
        }


        private void DownLoad()
        {
            //System.IO.StreamReader ObjReader = new System.IO.StreamReader(Application.StartupPath + "\\Service.txt");
            //string ServiceAddress = ObjReader.ReadToEnd();
            //ObjReader.Close();
            //var ObjBinding = new WSHttpBinding();
            //ObjBinding.Security.Mode = SecurityMode.None;
            //ObjBinding.Name = "AuotUpdate";
            //using (ChannelFactory<IFileServer> ObjChannelFactory = new ChannelFactory<IFileServer>(ObjBinding, ServiceAddress))
            //{
            //    IFileServer ObjFileServer = ObjChannelFactory.CreateChannel();
            //    using (ObjFileServer as IDisposable)
            //    {
            //        decimal Length = 0;
            //        ObjFileServer.AuotDownLoad(string.Empty, string.Empty, 0, out Length);
            //        int  LastLength = int.Parse(Length.ToString());
            //        int Index = LastLength / 16000;
            //        int Model = LastLength % 16000;
            //        if (Model > 0)
            //        {
            //            Index = Index + 1;
            //        }
            //        byte[] ObjByte = new byte[LastLength];
            //        List<byte> ObjList = new List<byte>();
            //        for (int i = 0; i < Index; i++)
            //        {
            //            var ObjStream = ObjFileServer.AuotDownLoad(string.Empty, string.Empty,i,out Length);
            //            ObjList.AddRange(ObjStream);
            //            // ObjStream.CopyTo(ObjByte, (i * 4096));
            //            System.Threading.Thread.Sleep(100);
            //        }
            //        FileStream ObjWrite = new FileStream(Application.StartupPath + "\\", FileMode.Create, FileAccess.Write);

            //        var WriteByte = ObjList.ToArray();
            //        ObjWrite.Write(WriteByte, 0, WriteByte.Length);
            //        ObjWrite.Close();
            //        System.Threading.Thread.Sleep(1000);
            //    }
            //}
        }

        /// <summary>
        /// 服务器验证 生成本地文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
         //   893BB0EE-4EEE-4980-B028-69D863389CC4
            System.IO.StreamReader ObjReader = new System.IO.StreamReader(Application.StartupPath + "\\Service.txt");
            string Address = ObjReader.ReadToEnd();
            ObjReader.Close();
            var ObjBinding = new WSHttpBinding();
            ObjBinding.Security.Mode = SecurityMode.None;
            btnNext.Visible = false;
            string CpuID = FindComputerCPU_ID();
            string HDID = ClientChecking();
            using (ChannelFactory<IFileServer> ObjChannelFactory = new ChannelFactory<IFileServer>(ObjBinding, Address))
            {
                IFileServer ObjFileServer = ObjChannelFactory.CreateChannel();
                using (ObjFileServer as IDisposable)
                {
                    string ServerString = ObjFileServer.CheckSNforCustomer(textBox2.Text + "," + CpuID + "," + HDID);
                    DateTime DateTimerEnde = DateTime.Parse(ServerString.Split('|')[0]);

                    if (DateTimerEnde > DateTime.Now)
                    {

                        string ByteString = DateTimerEnde + "|" + textBox2.Text + "|" + HDID + "|" + CpuID;
                        var WriteByte = System.Text.Encoding.UTF8.GetBytes(DESEncrypt(ByteString, ServerString.Split('|')[1], ServerString.Split('|')[2]));
                        File.WriteAllBytes(Application.StartupPath + "\\Collections.bat", WriteByte);



                        StreamWriter ObjWrite = new StreamWriter(Application.StartupPath + "\\SN.bat");
                        ObjWrite.WriteLine(textBox2.Text);
                        ObjWrite.Close();

                        //没有则默认为1.0版本
                        if (!System.IO.File.Exists(Application.StartupPath + "\\Vision.bat"))
                        {

                            System.IO.StreamWriter ObjVisionWrite = new System.IO.StreamWriter(Application.StartupPath + "\\Vision.bat");
                            ObjVisionWrite.WriteLine(Vision);
                            ObjVisionWrite.Close();
                        }

                        StreamWriter ObjKeyWordWrite = new StreamWriter(Application.StartupPath + "\\Words.bat");
                        ObjKeyWordWrite.WriteLine(ServerString.Split('|')[1] + "|" + ServerString.Split('|')[2]);
                        ObjKeyWordWrite.Close();
 

                        CreateDataBaseXml ObjCreateXml = new CreateDataBaseXml();

                        this.Hide();
                        ObjCreateXml.ShowDialog();

                        //ObjCreateXml.FormClosing += FormClose;
                     

                       // this.Dispose();
                        //DownLoad();
                    }
                    else
                    {
                        MessageBox.Show("序列号不存在!");
                        btnNext.Visible = true;
                    }
                }
            }




            //            String2Byte
            //System.Text.Encoding.ASCII.GetBytes();
            //System.Text.Encoding.Unicode.GetBytes();     
            //System.Text.Encoding.UTF8.GetBytes();

            //Byte2String
            //System.Text.Encoding.ASCII.GetString();
            //System.Text.Encoding.Unicode.GetString() 
            //System.Text.Encoding.UTF8.GetString();
        }
 
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptStr, string key, string IV)
        {
            //将key和IV处理成8个字符 
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ict = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(encryptStr);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            //加上一些干扰字符 
            string retVal = Convert.ToBase64String(ms.ToArray());
            System.Random ra = new Random();

            for (int i = 0; i < 8; i++)
            {
                int radNum = ra.Next(36);
                char radChr = Convert.ToChar(radNum + 65);//生成一个随机字符 

                retVal = retVal.Substring(0, 2 * i + 1) + radChr.ToString() + retVal.Substring(2 * i + 1);
            }
            return retVal.Replace("+", "%2B");
        }


        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormClose(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("安装完毕!");
            this.Close();
            this.Dispose();
        }

        private void Setup_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        ////解密函数： 
        ///// <summary> 
        ///// 使用DES解密指定字符串 
        ///// </summary> 
        ///// <param name="encryptedValue">待解密的字符串 </param> 
        ///// <param name="key">密钥(最大长度8) </param> 
        ///// <param name="IV">初始化向量(最大长度8) </param> 
        ///// <returns>解密后的字符串 </returns> 
        //public static string DESDecrypt(string encryptedValue, string key, string IV)
        //{
        //    //去掉干扰字符 
        //    string tmp = encryptedValue.Replace("%2B", "+");
        //    if (tmp.Length < 16)
        //    {
        //        return "";
        //    }

        //    for (int i = 0; i < 8; i++)
        //    {
        //        tmp = tmp.Substring(0, i + 1) + tmp.Substring(i + 2);
        //    }
        //    encryptedValue = tmp;

        //    //将key和IV处理成8个字符 
        //    key += "12345678";
        //    IV += "12345678";
        //    key = key.Substring(0, 8);
        //    IV = IV.Substring(0, 8);

        //    SymmetricAlgorithm sa;
        //    ICryptoTransform ict;
        //    MemoryStream ms;
        //    CryptoStream cs;
        //    byte[] byt;

        //    try
        //    {
        //        sa = new DESCryptoServiceProvider();
        //        sa.Key = Encoding.UTF8.GetBytes(key);
        //        sa.IV = Encoding.UTF8.GetBytes(IV);
        //        ict = sa.CreateDecryptor();

        //        byt = Convert.FromBase64String(encryptedValue);

        //        ms = new MemoryStream();
        //        cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
        //        cs.Write(byt, 0, byt.Length);
        //        cs.FlushFinalBlock();

        //        cs.Close();

        //        return Encoding.UTF8.GetString(ms.ToArray());
        //    }
        //    catch (System.Exception e)
        //    {

        //        return null;
        //    }
        //}



    }
}
