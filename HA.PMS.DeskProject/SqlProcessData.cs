using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace HA.PMS.DeskProject
{
    public class SqlProcessData
    {
        /*
        public void RunSqltxt(string Path)
        {
            string infile = Path;
            Process sqlprocess = new Process();
            sqlprocess.StartInfo.FileName = "osql.exe";
            sqlprocess.StartInfo.Arguments = String.Format("-U{0}-P{1}-S{2}-i{3}", "sa", "sasa", ".", @infile); //U为用户名,P为密码,S为目标服务器的ip,infile为数据库脚本所在的路径 
           // sqlprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            sqlprocess.Start();
            sqlprocess.WaitForExit(); //等待程序执行.Sql脚本
            sqlprocess.Close(); 

         }
        */

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        private static string GetConnectionString(string connectionName)
        {

            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();

        }
        //数据库连接字符串
        private string connectionString = "Data Source=.;Initial Catalog=master;User ID=sa;Password=sasa";
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        //数据库默认配置字符串(文件中)
        private string defaultConfigString = "sa|sasa|.|master";
        public string DefaultConfigString
        {
            get { return defaultConfigString; }
            set { defaultConfigString = value; }
        }

        //配置文件路径
        private string configFilePath = Application.StartupPath + "\\SqlPath.xml";
        public string ConfigFilePath
        {
            get { return configFilePath; }
            set { configFilePath = value; }
        }

        #region 构造方法 +SqlProcessData()
        /// <summary>
        /// 无参构造方法
        /// </summary>
        public SqlProcessData()
        {
            this.ConnectionString = CreateSqlConStrByConfigFile(this.ConfigFilePath, this.DefaultConfigString);
        }

        /// <summary>
        /// 带参构造方法
        /// </summary>
        /// <param name="configFilePath">配置文件地址</param>
        public SqlProcessData(string configFilePath)
        {
            this.configFilePath = configFilePath;
            //读取配置文件中特定格式的数据库连接字符串，设置数据库连接字符串
            this.ConnectionString = CreateSqlConStrByConfigFile(configFilePath, this.DefaultConfigString);
        }

        /// <summary>
        /// 带参构造方法
        /// </summary>
        /// <param name="configFilePath">配置文件地址</param>
        /// <param name="defaultConfigStr">默认配置文件字符串</param>
        public SqlProcessData(string configFilePath, string defaultConfigStr)
        {
            this.configFilePath = configFilePath;
            //读取配置文件中特定格式的数据库连接字符串，设置数据库连接字符串
            this.ConnectionString = CreateSqlConStrByConfigFile(configFilePath, this.DefaultConfigString);
            //设置数据库默认配置字符串
            this.DefaultConfigString = defaultConfigStr;
        }
        #endregion

        #region 读取文件所有文本 +ReadAllText(string filePath,string defaultText)
        /// <summary>
        /// 取文件所有文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="text">文件为空时，写入文件的数据</param>
        /// <returns>文件内容</returns>
        public string ReadAllText(string filePath, string defaultText)
        {
            string temp = string.Empty;
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        temp = reader.ReadToEnd();
                        if (temp == string.Empty)
                        {
                            throw new Exception("File is Empty!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        //写入默认数据（配置）
                        writer.Write(defaultText);
                        writer.Flush();
                        //设置返回默认数据
                        temp = defaultText;
                    }
                }
            }
            return temp;
        }
        #endregion

        #region 构建Sql连接字符串 +CreateSqlConStrByConfigFile(string filePath, string defaultConfigStr)
        /// <summary>
        /// 根据配置文件构建Sql连接字符串
        /// </summary>
        /// <param name="filePath">配置文件路径</param>
        /// <param name="defaultConfigStr">默认配置字符串</param>
        /// <returns>数据库连接字符串</returns>
        public string CreateSqlConStrByConfigFile(string filePath, string defaultConfigStr)
        {
            //string configText = ReadAllText(filePath, defaultConfigStr);
            //string[] strarr = configText.Split('|');
            //SqlConnectionStringBuilder objConStr = new SqlConnectionStringBuilder();
            ////如果配置信息不完整，则设置为默认
            //if (strarr.Length < 4)
            //{
            //    strarr = defaultConfigStr.Split('|');
            //}
            //objConStr.UserID = strarr[0];
            //objConStr.Password = strarr[1];
            //objConStr.DataSource = strarr[2];
            //objConStr.InitialCatalog = strarr[3];

            return GetConnectionString("ServiceAddress");
        }
        #endregion

        #region 执行带GO的sql语句 +void ExecuteSqlWithGo(string sql)
        /// <summary>
        /// 执行带GO的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        public void ExecuteSqlWithGo(string sql)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    conn.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ConnectionString);
                }
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //注： 此处以 换行_后面带0到多个空格_再后面是go 来分割字符串   
                    string[] sqlArr = Regex.Split(sql.Trim(), "\r\n\\s*go", RegexOptions.IgnoreCase);
                    foreach (string strsql in sqlArr)
                    {
                        if (strsql.Trim().Length > 1 && strsql.Trim() != "\r\n")
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        #endregion

        #region 执行等GO的sql语句（事务）+void ExecuteSqlWithGoUseTran(string sql)
        /// <summary>
        /// 执行等GO的sql语句（事务）
        /// </summary>
        /// <param name="sql">sql语句</param>
        public void ExecuteSqlWithGoUseTran(string sql)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    SqlTransaction tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    //注： 此处以 换行_后面带0到多个空格_再后面是go 来分割字符串   
                    String[] sqlArr = Regex.Split(sql.Trim(), "\r\n\\s*go", RegexOptions.IgnoreCase);
                    try
                    {
                        foreach (string strsql in sqlArr)
                        {
                            if (strsql.Trim().Length > 1 && strsql.Trim() != "\r\n")
                            {
                                cmd.CommandText = strsql;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tx.Commit();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        tx.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region 执行带GO的sql文件 +void ExecuteSqlWithGoFromFile(string filePath)
        /// <summary>
        /// 执行带GO的sql文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void ExecuteSqlWithGoFromFile(string filePath)
        {
            this.ExecuteSqlWithGo(ReadAllText(filePath, ""));
        }
        #endregion

        #region 执行带GO的sql文件（事务）+void ExecuteSqlWithGoUseTranFromFile(string filePath)
        /// <summary>
        /// 执行带GO的sql文件（事务）
        /// </summary>
        /// <param name="filePath">路径</param>
        public void ExecuteSqlWithGoUseTranFromFile(string filePath)
        {
            this.ExecuteSqlWithGoUseTran(ReadAllText(filePath, ""));
        }
        #endregion

        public void RunSqltxt(string Path)
        {
            this.ExecuteSqlWithGoFromFile(Path);
            System.Threading.Thread.Sleep(5000);
            File.Delete(Path);
            ////参数

            //if (!File.Exists(Path))
            //{
            //    return;
            //}
            //StreamReader Objreader = new StreamReader(Environment.CurrentDirectory + "\\SqlPath.xml");
            //string ObjSqlConnString = Objreader.ReadToEnd();
            //Objreader.Close();
            //string[] args = new string[5];
            //args[0] = "-U " + ObjSqlConnString.Split('|')[0]; //用户名
            //args[1] = "-P " + ObjSqlConnString.Split('|')[1];//用户密码
            //args[2] = "-S " + ObjSqlConnString.Split('|')[2]; //服务器
            //args[3] = "-d " + ObjSqlConnString.Split('|')[3]; //数据库
            //args[4] = "-i " + Path; //sql脚本路径


            //CommandLine("osql.exe", Path, args);
        }

        #region 调用命令行工具

        /// <summary>
        /// 调用命令行工具
        /// </summary>
        /// <param name="name">命令行工具名称</param>
        /// <param name="args">可选命令行参数</param>
        /// <remarks>注意：所有命令行工具都必须保存于system32文件夹中</remarks>
        /// <returns></returns>
        private string CommandLine(string name, string SqlPath, params string[] args)
        {
            return CommandLine(name, "", SqlPath, args);
        }

        /// <summary>
        /// 调用命令行工具
        /// </summary>
        /// <param name="name">命令行工具名称</param>
        /// <param name="workingDirectory">设置工作目录</param>
        /// <param name="args">可选命令行参数</param>
        /// <remarks>注意：所有命令行工具都必须保存于system32文件夹中</remarks>
        /// <returns></returns>
        private string CommandLine(string name, string workingDirectory, string SqlPath, params string[] args)
        {
            string returnValue = "";

            using (Process commandline = new Process())
            {
                try
                {
                    commandline.StartInfo.UseShellExecute = false;
                    commandline.StartInfo.CreateNoWindow = true;
                    commandline.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    commandline.StartInfo.RedirectStandardOutput = true;
                    commandline.StartInfo.FileName = name;
                    commandline.StartInfo.WorkingDirectory = workingDirectory;
                    //添加命令行参数
                    if (args.Length > 0) commandline.StartInfo.Arguments = string.Join(" ", args);
                    commandline.Start();
                    commandline.WaitForExit();
                    returnValue = commandline.StandardOutput.ReadToEnd();
                    commandline.Close();
                    System.IO.File.Delete(SqlPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    System.IO.File.Delete(SqlPath);
                    commandline.Dispose();
                    throw;
                }
            }

            return returnValue;
        }

        #endregion
    }
}
