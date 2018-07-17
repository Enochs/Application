using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Data.SqlClient;

namespace HA.Setup
{
    public partial class CreateDataBaseXml : Form
    {
        public CreateDataBaseXml()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 创建数据库零时配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncCreate_Click(object sender, EventArgs e)
        {
            btncCreate.Visible = false;
            string constr = "server=" + txtPath.Text + ";pwd=" + txtPassword.Text + ";uid=" + txtLoginName.Text + ";database=master";

            SqlConnection con = new SqlConnection(constr);
            try
            {
                con.Open();
                con.Close();
            }
            catch
            {
                MessageBox.Show("数据库访问失败");
                return;
            }

            string ZipPath = Application.StartupPath + "\\WebSite.zip";
            string ExtractPath = Application.StartupPath + "\\";

            using (ZipArchive Archive = ZipFile.Open(ZipPath, ZipArchiveMode.Update))
            {

                Archive.ExtractToDirectory(ExtractPath);
            }
            File.Delete(ZipPath);




            //生成在线统计配置文件
            StreamReader ObjOnlineSystemjReader = new StreamReader(Application.StartupPath + "\\OnlineSystem\\HA.PMS.OnlineSysytem.exe.config");
            string OnlineConfig = ObjOnlineSystemjReader.ReadToEnd();
            OnlineConfig = OnlineConfig.Replace("192.168.0.126", txtPath.Text);
            OnlineConfig = OnlineConfig.Replace("sa", txtLoginName.Text);
            OnlineConfig = OnlineConfig.Replace("sasa", txtPassword.Text);
            ObjOnlineSystemjReader.Close();
            StreamWriter ObjOnlineConfigwrite = new StreamWriter(Application.StartupPath + "\\OnlineSystem\\HA.PMS.OnlineSysytem.exe.config", false, Encoding.UTF8);
            ObjOnlineConfigwrite.Write(OnlineConfig);
            ObjOnlineConfigwrite.Close();





            //生成Web程序配置文件
            StreamReader ObjWebReader = new StreamReader(Application.StartupPath + "\\Web.config");
            string WebConfig = ObjWebReader.ReadToEnd();
            WebConfig = WebConfig.Replace("192.168.0.126", txtPath.Text);
            WebConfig = WebConfig.Replace("sa", txtLoginName.Text);
            WebConfig = WebConfig.Replace("sasa", txtPassword.Text);
            ObjWebReader.Close();
            StreamWriter ObjWebConfigwrite = new StreamWriter(Application.StartupPath + "\\Web.config", false, Encoding.UTF8);
            ObjWebConfigwrite.Write(WebConfig);
            ObjWebConfigwrite.Close();

            string Path = Application.StartupPath + "\\SqlPath.xml";
            StreamWriter Objwrite = new StreamWriter(Path, false);
            //创建临数据库配置文件
            Objwrite.Write(txtLoginName.Text + "|" + txtPassword.Text + "|" + txtPath.Text + "|PMS_Wedding");
            Objwrite.Close();
            //StreamReader ObjReader = new StreamReader(Application.StartupPath + "\\CreateDataBase.sql");
            //var ObjCreateDataBasestring = ObjReader.ReadToEnd();
            //ObjReader.Close();

            //生成数据库

            //ObjCreateDataBasestring = ObjCreateDataBasestring.Replace("DatabasePath", txtDataBaseFilePath.Text + "\\PMS_Wedding.mdf");
            //ObjCreateDataBasestring = ObjCreateDataBasestring.Replace("DataBaseLogPath", txtDataBaseFilePath.Text + "\\PMS_Wedding.ldf");



            ////创建执行数据库SQL文件
            ////StreamWriter ObjSqlwrite = new StreamWriter(Application.StartupPath + "\\CreateDatbase.sql", false, Encoding.UTF8);
            ////ObjSqlwrite.Write(ObjCreateDataBasestring);
            //CreateDatabase ObjCreateDatabase = new CreateDatabase();
            ////Objwrite.Close();

            //ObjCreateDatabase.RunSqltxt(Application.StartupPath+"\\CreateDatbase.sql","master");
            //System.Threading.Thread.Sleep(3000);
            //ObjCreateDatabase.RunSqltxt(Application.StartupPath + "\\ChannelList.sql", "PMS_Wedding");
            CreateDataBase();
            ExSite ObjExSite = new ExSite();
            this.Hide();
            ObjExSite.ShowDialog();

            //(Application.StartupPath+"\\CreateDatbase.sql");File.Delete(Application.StartupPath + "\\CreateDatbase.sql");
            // this.Close();
            //this.Dispose();

        }

        #region 附加数据库

        private void CreateDataBase()
        {


            try
            {
                string DataBaseName = "";
                string constr = "server=" + txtPath.Text + ";pwd=" + txtPassword.Text + ";uid=" + txtLoginName.Text + ";database=master";

                SqlConnection con = new SqlConnection(constr);

                string sql = " use    master    EXEC    sp_attach_db    @dbname    =    N'PMS_Wedding'," +

                       " @filename1    =    N'" + Application.StartupPath + "\\App_Data\\PMS_Wedding.mdf" + "'," +
                       " @filename2    =   N'" + Application.StartupPath + "\\App_Data\\PMS_Wedding_log.ldf" + "'";



                SqlCommand com = new SqlCommand(sql, con);

                con.Open();

                com.ExecuteNonQuery();
                com.Clone();
                com.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库添加失败！");
            }
        }
        //private void CreateDataBase()
        //{


        //    try
        //    {
        //        string DataBaseName = "";
        //        string constr = "server=" + txtPath.Text + ";pwd=" + txtPassword.Text + ";uid=" + txtLoginName.Text + ";database=master";

        //        SqlConnection con = new SqlConnection(constr);

        //        string sql = " use    master    EXEC    sp_attach_db    @dbname    =    N'PMS_EasyWedding'," +

        //               " @filename1    =    N'" + Application.StartupPath + "\\App_Data\\PMS_EasyWedding.mdf" + "'," +
        //               " @filename2    =   N'" + Application.StartupPath + "\\App_Data\\PMS_EasyWedding_log.ldf" + "'";



        //        SqlCommand com = new SqlCommand(sql, con);

        //        con.Open();

        //        com.ExecuteNonQuery();
        //        com.Clone();
        //        MessageBox.Show("数据库添加成功!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("数据库添加失败！");
        //    }
        //}
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.txtDataBaseFilePath.Text = path.SelectedPath;
        }
    }
}
