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
using HA.PMS.BLLAssmblly.CS;

using System.IO;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class SedimentationCreate : SystemPage
    {
        FileCategory objFileCategory = new FileCategory();
        FileDetails objFileDetailsBLL = new FileDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string urlPar = Request.QueryString["FileCategoryId"];
                if (urlPar!= "")
                {


                    string path = "/Files/Sedimentation/";
                    List<SedimentationTreeBrowse.FileMessage> list
                        = new List<SedimentationTreeBrowse.FileMessage>();
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~" + path));
                    FileInfo[] currentFileInfo = dirInfo.GetFiles("*", SearchOption.AllDirectories);
                    //   string[] files = DirectoryInfo.GetFiles(Server.MapPath(path));
                    foreach (FileInfo itemFile in currentFileInfo)
                    {
                        // FileStream singerFile = System.IO.File.OpenRead(txtFile);
                        SedimentationTreeBrowse.FileMessage singer =
                             new SedimentationTreeBrowse.FileMessage();
                        singer.filePaths = itemFile.FullName;
                        singer.fileNames = itemFile.Name;
                        singer.fileSizes = FormatFileSize(itemFile.Length);

                        list.Add(singer);
                    }

                    rptFiles.DataSource = list;
                    rptFiles.DataBind();
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("请你选择要添加文件分类名", this.Page);
                }
            }
           
        }

        /// <summary>
        /// 返回文件后缀名
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetFileExtention(object source) 
        {
            return Path.GetExtension(source + string.Empty);
        }

        /// <summary>
        /// 返回文件类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetFileType(object source) 
        {
            string extension = GetFileExtention(source).ToLower();
            string fileType=string.Empty;
            switch (extension)
            {
                case ".ppt":
                case ".pptx":
                    fileType = "PPT";
                    break;
                case ".jpg":
                case ".bmp":
                case ".jpeg":
                case ".png":
                case ".gif":
                    fileType = "图片";
                    break;
                case ".txt":
                    fileType = "文本";
                    break;
                case ".mp4":
                case ".rmvb":
                case ".avi":
                case ".wmv":
                case ".3gp":
                case ".f4v":
                case ".rm":
                    fileType = "视频";
                    break;
                default:
                    fileType = "其他";
                    break;
            }
            return fileType;
        }
           

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            int checkCount = 0;
            int FileCategoryId = Request.QueryString["FileCategoryId"].ToInt32();
            for (int i = 0; i < rptFiles.Items.Count; i++)
            {
                CheckBox chSinger = rptFiles.Items[i].FindControl("chSinger") as CheckBox;
                if (chSinger.Checked)
                {
                    Literal ltlFileName = rptFiles.Items[i].FindControl("ltlFileName") as Literal;
                    Literal ltlFilePaths = rptFiles.Items[i].FindControl("ltlFilePaths") as Literal;
                    Sys_FileDetails singerDetails = new Sys_FileDetails();
                    singerDetails.FileDetailsName = ltlFileName.Text;
                    singerDetails.FileDetailsPath = ltlFilePaths.Text;
                    singerDetails.FileCategoryId = FileCategoryId;
                    objFileDetailsBLL.Insert(singerDetails);
                    checkCount++;
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
        }
    }
}