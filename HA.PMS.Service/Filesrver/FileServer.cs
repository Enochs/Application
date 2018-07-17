
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using HA.PMS.BLLInterface;
namespace HA.PMS.Service.Filesrver
{
    public class FileServer : IFileServer
    {
        Sys_SndatabaseEntities Objentity = new Sys_SndatabaseEntities();



        string AutoPath = string.Empty;
        string MaxVision = string.Empty;
        /// <summary>
        /// 获取更新文件路径
        /// </summary>
        /// <returns></returns>
        public string GetFileServerPath()
        {

            AutoPath = Environment.CurrentDirectory + "\\Floder.txt";
            StreamReader Objreader = new StreamReader(AutoPath);

            AutoPath = Objreader.ReadToEnd().Trim();


            StreamReader ObjVisionreader = new StreamReader(AutoPath + "\\MaxVision.txt");
            MaxVision = ObjVisionreader.ReadToEnd();
            ObjVisionreader.Close();
            
            Objreader.Close();
            return AutoPath;
        }


        /// <summary>
        /// 自动更新
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="FileName"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public byte[] AuotUpdate(string SN, string FileName, int Index)
        {
            return GetFileByte(FileName, SN, Index);
        }


        /// <summary>
        /// 获取文件byte数组
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="SN"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public byte[] GetFileByte(string Path, string SN, int Index)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]    


            FileStream fs = new FileStream(GetFileServerPath() +(int.Parse(SN.Split(',')[3])+1) + "\\" + Path.Split('|')[2].Trim() + "\\" + Path.Split('|')[0].Trim(), FileMode.Open);
            byte[] byData = new byte[fs.Length];
            byte[] SetByte = new byte[16000];

            fs.Read(byData, 0, byData.Length);
            SetByte = byData.Skip(Index * 16000).Take(16000).ToArray();
            fs.Close();
            return SetByte;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="SN"></param>
        ///// <param name="FileName"></param>
        ///// <param name="Index"></param>
        ///// <param name="Length"></param>
        ///// <returns></returns>
        //public byte[] AuotDownLoad(string SN, string FileName, int Index, out decimal Length)
        //{


        //    return ObjFileServer.AuotDownLoad(SN, FileName, Index, out Length);
        //}



        public int GetFileSum()
        {

            return 10;
        }



        /// <summary>
        /// 判断序列号 开始自动更新
        /// </summary>
        /// <param name="SN">序列号+硬盘号+CPU号+版本号</param>
        /// <returns>返回更新列表</returns>
        public List<string> ChecksSN(string SN)
        {

            //SN = 
            string SNT = SN.Split(',')[0];
            string DiskiID =SN.Split(',')[2];
            string CpuID = SN.Split(',')[1];

            //判断此客户端是否存在
            var ObjModel = Objentity.Table_SnManager.FirstOrDefault(C => C.Sn == SNT && C.DiskID == DiskiID && C.CpuID == CpuID);
            if (ObjModel != null)
            {
                //if (!ObjModel.FirstCreate)
                //{
                //    ObjModel.DiskID = SN.Split(',')[1];
                //    ObjModel.CpuID = SN.Split(',')[0];
                //    ObjModel.FirstCreate = true;
                //    Objentity.SaveChanges();
                //}

                //读取相应版本号下的需要更新的文件

                StreamReader Objreader = new StreamReader(GetFileServerPath() + (int.Parse(SN.Split(',')[3]) + 1) + "\\ItemFileList.txt");


                string ObjFileList = Objreader.ReadToEnd();
                Objreader.Close();


                var ObjList = ObjFileList.Split(',');
                var ObjReturnList = ObjList.ToList();
                //如果客户端版本号低于当前版本号则添加下一版本号
                if (decimal.Parse(MaxVision) > int.Parse(SN.Split(',')[3])+1)
                {
                    ObjReturnList.Add("Vision=" + (int.Parse(SN.Split(',')[3]) + 1) + string.Empty);
                }


                if (ObjList.Count() > 0)
                {
                    //返回下个版本号
              

                    return ObjReturnList;
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                var ReturnList = new List<string>();
                ReturnList.Add("404");
                return ReturnList;
            }

        }



        /// <summary>
        /// 验证序列号
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        public string CheckSNforCustomer(string SN)
        {
            string SNT = SN.Split(',')[0];
            string CpuID = SN.Split(',')[1];
            string DiskID = SN.Split(',')[2];
            var ObjModel = Objentity.Table_SnManager.FirstOrDefault(C => C.Sn == SNT);
            if (ObjModel != null)
            {
                if (!ObjModel.FirstCreate)
                {
                    ObjModel.CpuID = SN.Split(',')[1];
                    ObjModel.DiskID = SN.Split(',')[2];


                    ObjModel.FirstCreate = true;
                    ObjModel.Word1 = Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + ObjModel.DiskID.Substring(0, 2);
                    ObjModel.Word2 = Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + ObjModel.CpuID.Substring(0, 2);
                    Objentity.SaveChanges();
                    return ObjModel.OverDate.Value.ToShortDateString() + "|" + ObjModel.Word1 + "|" + ObjModel.Word2;
                }
                else
                {
                    var ObjRealModel = Objentity.Table_SnManager.FirstOrDefault(C => C.Sn == SNT && C.CpuID == CpuID && C.DiskID == DiskID);
                    if (ObjRealModel == null)
                    {
                        return Guid.NewGuid().ToString();
                    }
                    else
                    {
                        return ObjRealModel.OverDate.Value.ToShortDateString() + "|" + ObjRealModel.Word1 + "|" + ObjRealModel.Word2;

                    }
                }

            }

            return Guid.NewGuid().ToString();
        }
    }
}
