using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.IO;
using System.Xml;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class FD_GuardianFilesChoose : SystemPage
    {
        GuardianImage objGuardianImageBLL = new GuardianImage();
        GuardianMovie objGuardianMovieBLL = new GuardianMovie();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataBinder();
            }
        }


        protected void DataBinder()
        {
            //1 是视频，2是图片
            int GuradianFileType = Request.QueryString["GuradianFileType"].ToInt32();

            // string key = "";
            string path = "/Files/GuardianMovie/";
            if (GuradianFileType != 1)
            {

                path = "/Files/GuardianImage/";
            }
            #region

            //XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            //doc.Load(Path.Combine(Request.PhysicalApplicationPath, "web.config"));
            //XmlNode nodeAppSettings = doc.SelectSingleNode("configuration/appSettings");
            //foreach (XmlNode item in nodeAppSettings.ChildNodes)
            //{
            //    if (item.Attributes[key]!=null)
            //    {
            //        path += "~"+item.Value;
            //    }
            //}
            #endregion

            List<FD_GuradianFile> list = new List<FD_GuradianFile>();
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~" + path));


            FileInfo[] currentFileInfo = dirInfo.GetFiles("*", SearchOption.AllDirectories);

            //   string[] files = DirectoryInfo.GetFiles(Server.MapPath(path));
            foreach (FileInfo itemFile in currentFileInfo)
            {
                // FileStream singerFile = System.IO.File.OpenRead(txtFile);
                var DataList = objGuardianMovieBLL.GetByAll();
                foreach (var item in DataList)
                {
                    if (item.MovieName == itemFile.Name)
                    {
                        itemFile.MoveTo(Server.MapPath("~/Files/GuardianMovieNew/" + item.MovieName));
                    }
                }
                FD_GuradianFile gf = new FD_GuradianFile();

                gf.GuradianFilePath = itemFile.FullName;
                gf.GuradianFileName = itemFile.Name;

                list.Add(gf);
            }

            rptGuradian.DataSource = list;
            rptGuradian.DataBind();
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            int checkCount = 0;
            int GuradianFileType = Request.QueryString["GuradianFileType"].ToInt32();
            int GuardianId = Request.QueryString["GuardianId"].ToInt32();

            for (int i = 0; i < rptGuradian.Items.Count; i++)
            {
                CheckBox chSinger = rptGuradian.Items[i].FindControl("chSinger") as CheckBox;
                if (chSinger.Checked)
                {
                    Literal ltlGuradianFileName = rptGuradian.Items[i].FindControl("ltlGuradianFileName") as Literal;
                    Literal ltlGuradianFilePath = rptGuradian.Items[i].FindControl("ltlGuradianFilePath") as Literal;

                    checkCount++;
                    //视频
                    if (GuradianFileType == 1)
                    {
                        FD_GuardianMovie movieSinger = new FD_GuardianMovie();
                        movieSinger.CreateEmployee = User.Identity.Name.ToInt32();
                        movieSinger.CreateTime = DateTime.Now;
                        movieSinger.FourGuardianId = GuardianId;
                        movieSinger.MovieName = ltlGuradianFileName.Text;
                        movieSinger.MoviePath = "~/Files/GuardianMovieNew/" + ltlGuradianFileName.Text;
                        movieSinger.IsDelete = false;
                        movieSinger.MovieTopImagePath = "~/Files/GuardianMovieTopImage/movieTop.jpg";
                        objGuardianMovieBLL.Insert(movieSinger);


                    }
                    else
                    {
                        //图片
                        FD_GuardianImage imgSinger = new FD_GuardianImage();
                        imgSinger.CreateEmployee = User.Identity.Name.ToInt32();
                        imgSinger.Createtime = DateTime.Now;
                        imgSinger.IsDelete = false;
                        imgSinger.FourGuardianId = GuardianId;
                        imgSinger.ImageName = ltlGuradianFileName.Text;
                        imgSinger.ImagePath = "~/Files/GuardianImage/" + ltlGuradianFileName.Text;
                        imgSinger.IsDelete = false;
                        objGuardianImageBLL.Insert(imgSinger);
                    }
                }
            }

            if (checkCount > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("请你选择文件", this.Page);
            }
            DataBinder();
        }

        #region 点击查询按钮
        /// <summary>
        /// 查询功能
        /// </summary>
        protected void btnLook_Click(object sender, EventArgs e)
        {
            string path = "/Files/GuardianMovie/";
            List<FD_GuradianFile> list = new List<FD_GuradianFile>();
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~" + path));
            FileInfo[] currentFileInfo = dirInfo.GetFiles("*", SearchOption.AllDirectories);

            string FilenName = txtFileName.Text.Trim().ToString();
            foreach (FileInfo itemFile in currentFileInfo)
            {
                if (itemFile.Name.Contains(FilenName))
                {
                    FD_GuradianFile gf = new FD_GuradianFile();

                    gf.GuradianFilePath = itemFile.FullName;
                    gf.GuradianFileName = itemFile.Name;

                    list.Add(gf);
                }
            }

            rptGuradian.DataBind(list);

        }
        #endregion
    }
}