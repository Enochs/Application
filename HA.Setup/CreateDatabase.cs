using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.Setup
{
    public class CreateDatabase
    {

        //public void RunSqltxt(string Path)
        //{
        //    string infile = Path;
        //    Process sqlprocess = new Process();
        //    sqlprocess.StartInfo.FileName = "osql.exe";
        //    sqlprocess.StartInfo.Arguments = String.Format("-U{0}-P{1}-S{2}-i{3}", "sa", "sasa", ".", @infile); //U为用户名,P为密码,S为目标服务器的ip,infile为数据库脚本所在的路径 
        //   // sqlprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    sqlprocess.Start();
        //    sqlprocess.WaitForExit(); //等待程序执行.Sql脚本
        //    sqlprocess.Close(); 

        // }

        public void RunSqltxt(string Path,string DataBase)
        {
            //参数

            if (!File.Exists(Path))
            {
                return;
            }
            StreamReader Objreader = new StreamReader(Environment.CurrentDirectory + "\\SqlPath.xml");
            string ObjSqlConnString = Objreader.ReadToEnd();
            string[] args = new string[5];
            args[0] = "-U " + ObjSqlConnString.Split('|')[0]; //用户名
            args[1] = "-P " + ObjSqlConnString.Split('|')[1];//用户密码
            args[2] = "-S " + ObjSqlConnString.Split('|')[2]; //服务器
            args[3] = "-d " + DataBase; //数据库
            args[4] = "-i " + Path; //sql脚本路径


            CommandLine("osql.exe", args);

            System.IO.File.Delete(Path);
            

        }

        #region 调用命令行工具

        /// <summary>
        /// 调用命令行工具
        /// </summary>
        /// <param name="name">命令行工具名称</param>
        /// <param name="args">可选命令行参数</param>
        /// <remarks>注意：所有命令行工具都必须保存于system32文件夹中</remarks>
        /// <returns></returns>
        private string CommandLine(string name, params string[] args)
        {
            return CommandLine(name, "", args);
        }

        /// <summary>
        /// 调用命令行工具
        /// </summary>
        /// <param name="name">命令行工具名称</param>
        /// <param name="workingDirectory">设置工作目录</param>
        /// <param name="args">可选命令行参数</param>
        /// <remarks>注意：所有命令行工具都必须保存于system32文件夹中</remarks>
        /// <returns></returns>
        private string CommandLine(string name, string workingDirectory, params string[] args)
        {
            string returnValue = "";

            using (Process commandline = new Process())
            {
                try
                {
                    commandline.StartInfo.UseShellExecute = false;
                    commandline.StartInfo.CreateNoWindow = true;
                    commandline.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    commandline.StartInfo.RedirectStandardOutput = true;
                    commandline.StartInfo.FileName = name;
                    commandline.StartInfo.WorkingDirectory = workingDirectory;
                    //添加命令行参数
                    if (args.Length > 0) commandline.StartInfo.Arguments = string.Join(" ", args);
                    commandline.Start();
                    commandline.WaitForExit(8000);
                    
                    returnValue = commandline.StandardOutput.ReadToEnd();
                    commandline.Close();
                }
                catch
                {
                    commandline.Dispose();
                    throw;
                }
            }

            return returnValue;
        }

        #endregion
    }
}
