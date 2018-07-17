using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using HA.PMS.BLLInterface;
using System.IO;
using System.Windows.Forms;

namespace HA.PMS.DeskProject
{
    public class LineSince
    {



        public decimal FinishSUm = 0;
        /// <summary>
        /// 更改数据库
        /// </summary>
        public void AlterDataBase()
        {
            //SqlProcessData spd = new SqlProcessData();
            //spd.RunSqltxt(Application.StartupPath + "\\AdminPanlWorkArea\\CreateDataBase.sql");

            try
            {
                //运行SQL文件
                SqlProcessData ObjSqlProcessData = new SqlProcessData();
                ObjSqlProcessData.RunSqltxt(Application.StartupPath + "\\AdminPanlWorkArea\\AlterDataBase.sql");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"数据库更新失败！请联系开发管理员 电话02368127494");
            }


        }


        public void AutoUpdate(string ServiceAddress, string SN, string BinPath, ProgressBar ObjControl, Label lblMessAge)
        {

            var ObjBinding = new WSHttpBinding();
            ObjBinding.Security.Mode = SecurityMode.None;
            string NewSN = SN;
            using (ChannelFactory<IFileServer> ObjChannelFactory = new ChannelFactory<IFileServer>(ObjBinding, ServiceAddress))
            {
                IFileServer ObjFileServer = ObjChannelFactory.CreateChannel();
                using (ObjFileServer as IDisposable)
                {
                    //循环获取版本号
                    for (int V = 0; V < 1000; V++)
                    {

                        //判断版本号
                        string Vision = "1";


                        System.IO.StreamReader ObjVisionReader = new System.IO.StreamReader(Application.StartupPath + "\\Vision.bat");
                        Vision = ObjVisionReader.ReadToEnd().Trim();
                        ObjVisionReader.Close();

                        SN = NewSN + "," + Vision;
                        var ObjFileList = ObjFileServer.ChecksSN(SN);
                        if (ObjFileList.Last().Contains("Vision"))
                        {
                            //写入最新版本号
                            File.Delete(Environment.CurrentDirectory + "\\Vision.txt");
                            StreamWriter ObjWriteVision = new StreamWriter(Environment.CurrentDirectory + "\\Vision.bat");
                            ObjWriteVision.WriteLine(ObjFileList.Last().Replace("Vision=", ""));
                            ObjWriteVision.Close();

                            ObjFileList.RemoveAt(ObjFileList.Count - 1);
                            //获取需要更新的文件
                            //  var ObjFileList = GetFileItem(BinPath,ObjFileServer,SN);

                            ObjControl.Maximum = ObjFileList.Count;
                            //   int NeedPoint = 100 / ObjFileList.Count;

                            //if (NeedPoint == 0)
                            //{
                            //    NeedPoint = 1;
                            //}
                            foreach (var ObjFileName in ObjFileList)
                            {
                                if (ObjFileName.Trim() != string.Empty)
                                {
                                    //循环文件
                                    if (!Directory.Exists(BinPath + ObjFileName.Split('|')[2].Trim() + "\\" + ObjFileName.Split('|')[0].Trim()))
                                    {
                                        Directory.CreateDirectory(BinPath + ObjFileName.Split('|')[2].Trim());
                                    }
                                    FileStream ObjWrite = new FileStream(BinPath + ObjFileName.Split('|')[2].Trim() + "\\" + ObjFileName.Split('|')[0].Trim(), FileMode.Create, FileAccess.Write);


                                    //是否有此文件夹 无则添加
                                    if (!Directory.Exists(BinPath + ObjFileName.Split('|')[2].Trim()))
                                    {
                                        Directory.CreateDirectory(BinPath + ObjFileName.Split('|')[2].Trim());
                                    }
                                    int Length = int.Parse(ObjFileName.Split('|')[1]);
                                    int Index = Length / 16000;
                                    int Model = Length % 16000;
                                    if (Model > 0)
                                    {
                                        Index = Index + 1;
                                    } 



                                    //定义数组 每次下载16000byte
                                    byte[] ObjByte = new byte[Length];
                                    List<byte> ObjList = new List<byte>();
                                    for (int i = 0; i < Index; i++)
                                    {
                                        var ObjStream = ObjFileServer.AuotUpdate(SN, ObjFileName, i);
                                        ObjList.AddRange(ObjStream);
                                        // ObjStream.CopyTo(ObjByte, (i * 4096));
                                        //System.Threading.Thread.Sleep(10);
                                    }

                                    var WriteByte = ObjList.ToArray();
                                    ObjWrite.Write(WriteByte, 0, WriteByte.Length);
                                    ObjWrite.Close();
                                    System.Threading.Thread.Sleep(50);
                                    ObjControl.Value += 1;
                                    lblMessAge.Text = "正在下载" + ObjFileName;
                                }
                            }
                            AlterDataBase();
                            lblMessAge.Text = "本版本更新完成,准备更新下一版本!";
                            System.Threading.Thread.Sleep(300);
                            lblMessAge.Text = "正在链接服务!";
                            System.Threading.Thread.Sleep(300);
                            ObjControl.Value = 0;

                     
                        }
                        else
                        {


                            break;
                        }
                    }



                    MessageBox.Show("更新完毕!");

                }
            }
        }
    }
}
