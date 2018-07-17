using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.Configuration;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.SystemConfig
{
    public partial class BackUpDataBase : SystemPage
    {
        string ConnString = ConfigurationManager.AppSettings["PMS_WeddingEntities"];
        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 数据库备份
        /// <summary>
        /// 备份
        /// </summary>  
        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            string sql = "backup database PMS_Wedding to disk='D:/BackUp/PMS_Wedding.bak'";
            conn.Open();

            try
            {
                //首先判断文件夹是否存在
                if (!Directory.Exists("D:/BackUp"))       //如果文件夹不存在  就新建一个文件夹
                {
                    Directory.CreateDirectory("D:/BackUp");
                }

                if (File.Exists("D:/BackUp/PMS_Wedding.bak"))   //文件名称已存在
                {
                    File.Delete("D:/BackUp/PMS_Wedding.bak");
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();      //执行sql语句
                    JavaScriptTools.AlertWindow("备份数据成功", Page);
                }
                else        //文件不存在  第一次生成备份数据
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();      //执行sql语句
                    JavaScriptTools.AlertWindow("备份数据成功", Page);

                }
            }
            catch
            {
                JavaScriptTools.AlertWindow("备份数据失败,请稍后再试...", Page);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

    }
}