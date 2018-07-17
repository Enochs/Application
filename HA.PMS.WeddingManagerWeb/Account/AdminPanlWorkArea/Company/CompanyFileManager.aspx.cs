using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.CS;
using System.IO;
using HA.PMS.BLLAssmblly.Sys;
//using Microsoft.Office.Interop.Word;
//using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanyFileManager : SystemPage
    {
        CompanyFile ObjFileBLL = new CompanyFile();

        Employee ObjEmployeeBLL = new Employee();

        LookURL ObjLookBLL = new LookURL();

        string ImagePath = "~/GenerationFiles/";
        int FileNameID = 0;

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>  
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void DataBinder()
        {
            var DataList = ObjFileBLL.GetByParentId(0);
            repCompanyFile.DataSource = DataList;
            repCompanyFile.DataBind();

            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());

            if (Model.EmployeeTypeID == 3)
            {
                CreateSystem.Visible = false;
            }
        }
        #endregion

        #region 前方空格

        public string GetItemNbsp(object ItemLevel)
        {
            if (ItemLevel != null)
            {
                int Count = ItemLevel.ToString().ToInt32();
                string Nbsp = "";
                if (Count == 1)
                {
                    return string.Empty;
                }
                for (int i = 0; i < Count; i++)
                {
                    Nbsp += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                return Nbsp;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 父级绑定  完成事件
        /// <summary>
        /// 绑定
        /// </summary> 
        protected void repCompanyFile_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField HideFileId = e.Item.FindControl("HiddentFileId") as HiddenField;
            FileNameID = HideFileId.Value.ToInt32(); ;
            int FileId = HideFileId.Value.ToInt32();
            Repeater RptFileDetails = e.Item.FindControl("repFileDetails") as Repeater;
            var DataList = ObjFileBLL.GetByParentId(FileId);
            RptFileDetails.DataSource = DataList;
            RptFileDetails.DataBind();


            //string conn_strs = ConfigurationManager.AppSettings["PMS_WeddingEntities"].ToString();
            //if (conn_strs == "server=192.168.0.199;uid=sa;pwd=sasa;database=PMS_Wedding")
            //{
            //    foreach (var FileModel in DataList)
            //    {
            //        string fileExtension = FileModel.FileName.Substring(FileModel.FileName.LastIndexOf(".") + 1).ToLower();     //后缀名
            //        string FileNames = FileModel.FileName.ToString();               //存储的中文名称
            //        string savePath = Server.MapPath("~\\GenerationFiles\\" + FileNames.Replace(fileExtension, "html"));    //转中文名称HTML(修改后缀名)
            //        if (!File.Exists(savePath))
            //        {
            //            string fileName = FileModel.FileURL.Substring(FileModel.FileURL.LastIndexOf("/") + 1);      //存储的乱码名称
            //            string path = Server.MapPath("~" + FileModel.PureRoute) + fileName;         //Word路径 (乱码名称)
            //            if (fileExtension == "doc" || fileExtension == "docx")
            //            {
            //                WordToHtml(path, savePath);
            //            }
            //            else if (fileExtension == "xls" || fileExtension == "xlsx")
            //            {
            //                ExcelToHtml(path, savePath);
            //            }
            //        }
            //    }
            //}


            LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
            if (!ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                lbtnDelete.Visible = false;
            }
        }
        #endregion

        #region 删除功能 顶级
        /// <summary>
        /// 删除顶级
        /// </summary>
        protected void repCompanyFile_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int FileId = e.CommandArgument.ToString().ToInt32();
            var FileModel = ObjFileBLL.GetByID(FileId);         //顶级

            if (e.CommandName == "Delete")
            {
                try
                {
                    int result = 0;
                    var FileList = ObjFileBLL.GetByParentId(FileId);    //下级集合

                    if (FileList.Count > 0)         //判断是否有下级  有下级 先删除下级
                    {
                        foreach (var item in FileList)
                        {
                            string fileName = item.FileName.ToString();
                            string fileExtension = item.FileName.Substring(item.FileName.LastIndexOf(".") + 1).ToLower();
                            string urls = Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace(fileExtension, "html"));
                            if (File.Exists(Server.MapPath(item.FileURL)))
                            {
                                File.Delete(Server.MapPath(item.FileURL));         //删除Office文件
                                File.Delete(urls);                                 //删除HTML文件
                                if (Directory.Exists(Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace("." + fileExtension, "_files"))))
                                {
                                    string ImageFileName = fileName.Replace("." + fileExtension, "_files");
                                    DeleteHTMLImage(ImageFileName);
                                    Directory.Delete(Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace("." + fileExtension, "_files")));   //删除有图片附件的文件夹
                                }
                            }
                            result += ObjFileBLL.Delete(item);  //删除数据库文件
                        }
                    }
                    if (FileModel.PureRoute != null)
                    {
                        if (Directory.Exists(Server.MapPath(FileModel.PureRoute)))
                        {
                            Directory.Delete(Server.MapPath(FileModel.PureRoute));       //删除文件夹
                        }
                    }
                    result += ObjFileBLL.Delete(FileModel);      //删除顶级
                    if (result == FileList.Count + 1)
                    {
                        JavaScriptTools.AlertWindow("删除成功", Page);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
                    }
                }
                catch
                {
                    Response.Redirect(Page.Request.Url.ToString());
                }
            }
            DataBinder();
        }
        #endregion

        #region 删除子级  下载功能
        /// <summary>
        /// 删除
        /// </summary>
        protected void repFileDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
            Employee ObjEmployeeBLL = new Employee();
            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            if (Model.EmployeeTypeID == 3)
            {
                lbtnDelete.Visible = false;
            }
            else
            {
                lbtnDelete.Visible = true;
            }

            int FileId = e.CommandArgument.ToString().ToInt32();
            var FileModel = ObjFileBLL.GetByID(FileId);         //子级

            if (e.CommandName == "Delete")
            {
                string fileName = FileModel.FileName.ToString();
                int result = ObjFileBLL.Delete(FileModel);      //删除子级

                string fileExtension = FileModel.FileName.Substring(FileModel.FileName.LastIndexOf(".") + 1).ToLower();
                string urls = Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace(fileExtension, "html"));
                if (File.Exists(Server.MapPath(FileModel.FileURL)))
                {
                    File.Delete(Server.MapPath(FileModel.FileURL));         //删除Office文件
                    File.Delete(urls);                                 //删除HTML文件
                    if (Directory.Exists(Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace("." + fileExtension, "_files"))))
                    {
                        string ImageFileName = fileName.Replace("." + fileExtension, "_files");
                        DeleteHTMLImage(ImageFileName);
                        Directory.Delete(Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace("." + fileExtension, "_files")));   //删除有图片附件的文件夹
                    }
                }

                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
                }
            }
            else if (e.CommandName == "DownLoad")
            {
                DownLoadFile(FileModel.FileURL, FileModel.FileName);
            }
            else if (e.CommandName == "Look")
            {
                var model = ObjLookBLL.GetByAll().FirstOrDefault();
                if (model != null)
                {
                    var FileModels = ObjFileBLL.GetByID(FileId);
                    string url = "http://officeweb365.com/o/?i=" + model.LooksKey + "&furl=http://" + model.URL + FileModels.FileURL.ToString();
                    Response.Write("<script>window.open('" + url + "')</script>");

                }
                else
                {
                    JavaScriptTools.AlertWindow("请您先设置外网地址", Page);
                }
            }

            DataBinder();
        }
        #endregion

        #region 刷新本页面
        /// <summary>
        /// 点击刷新
        /// </summary>
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.ToString());
        }
        #endregion

        #region 文件下载
        /// <summary>
        /// 使用微软的TransmitFile下载文件
        /// </summary>
        /// <param name="filePath">服务器相对路径</param>
        public void TransmitFile(string filePath)
        {
            try
            {
                filePath = Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();

                    //指定Http Mime格式为压缩包
                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";

                    // Http 协议中有专门的指令来告知浏览器, 本次响应的是一个需要下载的文件. 格式如下:
                    // Content-Disposition: attachment;filename=filename.txt
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(info.FullName));
                    //不指明Content-Length用Flush的话不会显示下载进度   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.TransmitFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }

        }


        public void DownLoadFile(string fileUrl, string fileName)
        {
            string selectName = Server.MapPath(fileUrl);
            string saveFileName = fileName;              //创建一个文件实体，方便对文件操作             
            FileInfo finfo = new FileInfo(selectName);             //清空输出流              
            Response.Clear();
            Response.Charset = "utf-8";
            Response.Buffer = true;              //关闭输出文件编码及类型和文件名              
            this.EnableViewState = false;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + saveFileName);
            //因为保存的文件类型不限，此处类型选择“unknown”             
            Response.ContentType = "application/unknown";
            Response.WriteFile(selectName);             //清空并关闭输出流 
            Response.Flush();
            Response.Close();
            Response.End();
        }

        #endregion

        #region 文件预览
        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="swfFile"></param>
        public void ConvertToSWF(string oldFile, string swfFile)
        {
            System.Diagnostics.Process pc = new System.Diagnostics.Process();
            pc.StartInfo.FileName = @"C:\Program Files\Macromedia\FlashPaper 2\FlashPrinter.exe";
            pc.StartInfo.Arguments = string.Format("{0} -o {1}", oldFile, swfFile);
            pc.StartInfo.CreateNoWindow = true;
            pc.StartInfo.UseShellExecute = false;
            pc.StartInfo.RedirectStandardInput = true;
            pc.StartInfo.RedirectStandardOutput = true;
            pc.StartInfo.RedirectStandardError = true;
            pc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pc.Start();
            pc.WaitForExit();
            pc.Close();
            pc.Dispose();
        }
        #endregion


        #region 隐藏功能   权限  一部分有删除 上传 功能


        public string GetJurisdiction()
        {
            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            return Model.EmployeeTypeID == 3 ? "style='display:none'" : string.Empty;
        }
        #endregion

        #region 子级绑定完成事件  隐藏按钮


        protected void repFileDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
            LinkButton lbtnDownLoad = e.Item.FindControl("lbtnDownLoad") as LinkButton;

            if (!ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                lbtnDelete.Visible = false;
                lbtnDownLoad.Visible = false;
            }
        }
        #endregion

        #region 删除HTML图片


        public void DeleteHTMLImage(string ImageFileName)
        {

            string FilePath = Server.MapPath(ImagePath + ImageFileName);
            if (Directory.Exists(FilePath))
            {
                //获得该目录的信息
                DirectoryInfo ObjDirInfo = new DirectoryInfo(FilePath);
                //获得当前目录以及其所有子目录的文件信息
                FileInfo[] ObjAllFileInfo = ObjDirInfo.GetFiles("*", SearchOption.AllDirectories);
                //遍历所有文件
                foreach (FileInfo ItemFileInfo in ObjAllFileInfo)
                {
                    if (File.Exists(ItemFileInfo.FullName))
                    {
                        File.Delete(ItemFileInfo.FullName);
                    }
                }
            }
            else if (Directory.Exists(FilePath.Replace("_", ".")))
            {
                //获得该目录的信息
                DirectoryInfo ObjDirInfo = new DirectoryInfo(FilePath);
                //获得当前目录以及其所有子目录的文件信息
                FileInfo[] ObjAllFileInfo = ObjDirInfo.GetFiles("*", SearchOption.AllDirectories);
                //遍历所有文件
                foreach (FileInfo ItemFileInfo in ObjAllFileInfo)
                {
                    if (File.Exists(ItemFileInfo.FullName))
                    {
                        File.Delete(ItemFileInfo.FullName);
                    }
                }
            }
        }
        #endregion

        #region 注释


        //#region Word转HTML


        //public static void WordToHtml(string filePath)
        //{
        //    Word.Application word = new Word.Application();
        //    Type wordType = word.GetType();
        //    Word.Documents docs = word.Documents;
        //    Type docsType = docs.GetType();
        //    Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { (object)filePath, true, true });
        //    Type docType = doc.GetType();
        //    string strSaveFileName = filePath.ToLower().Replace(Path.GetExtension(filePath).ToLower(), ".html");
        //    object saveFileName = (object)strSaveFileName;
        //    docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML });
        //    docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
        //    wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
        //    Thread.Sleep(3000);//为了使退出完全，这里阻塞3秒
        //}


        //public static void WordToHtml(string filePath, string htmlFilePaht)
        //{
        //    Word.Application word = new Word.Application();
        //    Type wordType = word.GetType();
        //    Word.Documents docs = word.Documents;
        //    Type docsType = docs.GetType();
        //    Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { (object)filePath, true, true });
        //    Type docType = doc.GetType();
        //    string strSaveFileName = htmlFilePaht;
        //    object saveFileName = (object)strSaveFileName;
        //    docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML });
        //    docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
        //    wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
        //    Thread.Sleep(3000);//为了使退出完全，这里阻塞3秒
        //}
        //#endregion

        //#region Excel转HTML
        //public static void ExcelToHtml(string filePath)
        //{
        //    string str = string.Empty;
        //    Excel.Application oApp = new Excel.Application();
        //    Excel.Workbook oBook = null;
        //    Excel.Worksheet oSheet = null;
        //    Excel.Workbooks oBooks = null;

        //    oBooks = oApp.Application.Workbooks;
        //    oBook = oBooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    oSheet = (Excel.Worksheet)oBook.Worksheets[1];
        //    object htmlFile = filePath.ToLower().Replace(Path.GetExtension(filePath).ToLower(), ".html");
        //    object ofmt = Excel.XlFileFormat.xlHtml;
        //    oBook.SaveAs(htmlFile, ofmt, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //    NAR(oSheet);
        //    GC.Collect();
        //    object osave = false;
        //    oBook.Close(osave, Type.Missing, Type.Missing);
        //    GC.Collect();
        //    NAR(oBook);
        //    GC.Collect();
        //    NAR(oBooks);
        //    GC.Collect();
        //    oApp.Quit();
        //    NAR(oApp);
        //    GC.Collect();
        //    KillProcess("EXCEL");
        //    Thread.Sleep(3000);//保证完全关闭
        //}



        //public static void ExcelToHtml(string filePath, string htmlFilePaht)
        //{
        //    string str = string.Empty;
        //    Microsoft.Office.Interop.Excel.Application oApp = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Workbook oBook = null;
        //    Microsoft.Office.Interop.Excel.Worksheet oSheet = null;
        //    Microsoft.Office.Interop.Excel.Workbooks oBooks = null;

        //    oBooks = oApp.Application.Workbooks;
        //    oBook = oBooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    oSheet = (Excel.Worksheet)oBook.Worksheets[1];
        //    object htmlFile = htmlFilePaht;
        //    object ofmt = Excel.XlFileFormat.xlHtml;
        //    oBook.SaveAs(htmlFile, ofmt, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //    NAR(oSheet);
        //    GC.Collect();
        //    object osave = false;
        //    oBook.Close(osave, Type.Missing, Type.Missing);
        //    NAR(oBook);
        //    GC.Collect();
        //    NAR(oBooks);
        //    GC.Collect();
        //    oApp.Quit();
        //    NAR(oApp);
        //    GC.Collect();
        //    KillProcess("EXCEL");
        //    Thread.Sleep(3000);//保证完全关闭

        //}
        //#endregion

        //#region Excel转HTML 必须方法


        ////依据时间杀灭进程
        //private static void KillProcess(string processName)
        //{
        //    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(processName);
        //    foreach (System.Diagnostics.Process p in process)
        //    {
        //        if (DateTime.Now.Second - p.StartTime.Second > 0 && DateTime.Now.Second - p.StartTime.Second < 5)
        //        {
        //            p.Kill();
        //        }
        //    }
        //}

        ////关闭对象
        //private static void NAR(object o)
        //{
        //    try
        //    {
        //        while (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0) ;
        //    }
        //    catch { }
        //    finally
        //    {
        //        o = null;
        //    }
        //}
        //#endregion

        #endregion

        #region 获取URL
        /// <summary>
        /// 获得URL
        /// </summary>
        public string GetUrl(object Source)
        {
            int FileId = Source.ToString().ToInt32();
            var model = ObjLookBLL.GetByAll().FirstOrDefault();
            if (model != null)
            {
                var FileModels = ObjFileBLL.GetByID(FileId);
                string url = "http://officeweb365.com/o/?i=" + model.LooksKey + "&furl=http://" + model.URL + FileModels.FileURL.ToString();
                return url;
            }
            else
            {
                JavaScriptTools.AlertWindow("请您先设置外网地址", Page);
            }

            return "";

        }
        #endregion

        public string IsHideButton()
        {
            if (!ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                return "style='display:none;'";
            }
            return "";

        }


        #region 测试  点击Click Me


        protected void btnClickMe_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Transformation.WordToHtml("D:\\计算机管理制度.doc", "D:\\计算机管理制度.html");
            //    Transformation.ExcelToHtml("D:\\员工月度报告.xls", "D:\\员工月度报告.html");
            //    JavaScriptTools.AlertWindow("测试成功,请打开文件夹查看", Page);
            //}
            //catch
            //{
            //    JavaScriptTools.AlertWindow("请确认是否有此文件", Page);
            //}
        }
        #endregion
    }
}