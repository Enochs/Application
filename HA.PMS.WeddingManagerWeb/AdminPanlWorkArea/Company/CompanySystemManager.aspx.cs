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
using System.Data.Objects;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.PublicTools;
using System.IO;
using System.Threading;
using System.Configuration;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanySystemManager : SystemPage
    {

        CompanySystem objCompanySystemBLL = new CompanySystem();

        Employee ObjEmployeeBLL = new Employee();

        LookURL ObjLookBLL = new LookURL();

        string ImagePath = "~/GenerationFiles/";
        string WebUrl = "";

        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
                {
                    tblWebUrl.Visible = false;
                }
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定

        protected void DataBinder()
        {
            var model = ObjLookBLL.GetByAll().FirstOrDefault();
            if (model != null)
            {
                txtOutURL.Text = model.URL;
                lblOutUrl.Text = model.URL;
                txtKeyID.Text = model.LooksKey.ToString();
                lblKeyID.Text = model.LooksKey.ToString();
            }

            var DataList = objCompanySystemBLL.GetByParentID(0);
            rptKnow.DataSource = DataList.Where(C => C.Type == 1);      //1.公司制度
            rptKnow.DataBind();

            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            if (Model.EmployeeTypeID == 3)
            {
                createKnow.Visible = false;
            }
        }
        #endregion

        #region 父级数据绑定
        /// <summary>
        /// 绑定
        /// </summary>
        protected void rptKnow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField HideSystemId = e.Item.FindControl("HiddenSystemId") as HiddenField;
            int SystemId = HideSystemId.Value.ToInt32();
            Repeater repSystemDetails = e.Item.FindControl("repSystemDetails") as Repeater;
            var DataList = objCompanySystemBLL.GetByParentID(SystemId);
            repSystemDetails.DataSource = objCompanySystemBLL.GetByParentID(SystemId);
            repSystemDetails.DataBind();
            //string conn_strs = ConfigurationManager.AppSettings["PMS_WeddingEntities"].ToString();

            //if (conn_strs == "server=192.168.0.199;uid=sa;pwd=sasa;database=PMS_Wedding")
            //{
            //    foreach (var FileModel in DataList)
            //    {
            //        string fileExtension = FileModel.SystemTitle.Substring(FileModel.SystemTitle.LastIndexOf(".") + 1).ToLower();     //后缀名
            //        string FileNames = FileModel.SystemTitle.ToString();               //存储的中文名称
            //        string savePath = Server.MapPath("~\\GenerationFiles\\" + FileNames.Replace(fileExtension, "html"));    //转中文名称HTML(修改后缀名)
            //        if (!File.Exists(savePath))
            //        {
            //            string fileName = FileModel.SystemURL.Substring(FileModel.SystemURL.LastIndexOf("/") + 1);      //存储的乱码名称路径
            //            string path = Server.MapPath("~" + FileModel.SystemPureRoute) + fileName;         //Word路径 (乱码名称)
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

            LinkButton lbtnDelete = e.Item.FindControl("lkbtnDelete") as LinkButton;
            if (ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeTypeID == 3)
            {
                lbtnDelete.Visible = false;
            }

        }
        #endregion

        #region 父级删除 修改
        /// <summary>
        /// 父级功能
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void rptKnow_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int SystemId = e.CommandArgument.ToString().ToInt32();
            var SystemModel = objCompanySystemBLL.GetByID(SystemId);         //顶级
            if (e.CommandName == "Delete")
            {
                try
                {
                    int result = 0;
                    var FileList = objCompanySystemBLL.GetByParentID(SystemId);    //下级集合

                    if (FileList.Count > 0)         //判断是否有下级  有下级 先删除下级
                    {
                        foreach (var item in FileList)
                        {
                            string fileName = item.SystemTitle.ToString();
                            string fileExtension = item.SystemTitle.Substring(item.SystemTitle.LastIndexOf(".") + 1).ToLower();
                            string urls = Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace(fileExtension, "html"));
                            if (File.Exists(Server.MapPath(item.SystemURL)))
                            {
                                File.Delete(Server.MapPath(item.SystemURL));         //删除Office文件
                                File.Delete(urls);                                 //删除HTML文件
                                if (Directory.Exists(Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace("." + fileExtension, "_files"))))
                                {
                                    string ImageFileName = fileName.Replace("." + fileExtension, "_files");
                                    DeleteHTMLImage(ImageFileName);
                                    Directory.Delete(Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace("." + fileExtension, "_files")));   //删除有图片附件的文件夹
                                }
                            }

                            result += objCompanySystemBLL.Delete(item);  //删除数据库文件
                        }
                    }
                    if (Directory.Exists(Server.MapPath(SystemModel.SystemPureRoute)))
                    {
                        Directory.Delete(Server.MapPath(SystemModel.SystemPureRoute));       //删除文件夹
                    }
                    result += objCompanySystemBLL.Delete(SystemModel);      //删除顶级
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

        #region 子级删除 修改功能
        /// <summary>
        /// 自己功能
        /// </summary>
        protected void repSystemDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int SystemId = e.CommandArgument.ToString().ToInt32();
            var SystemModel = objCompanySystemBLL.GetByID(SystemId);         //子级

            if (e.CommandName == "Delete")
            {
                string fileName = SystemModel.SystemTitle.ToString();
                int result = objCompanySystemBLL.Delete(SystemModel);       //删除数据
                string fileExtension = SystemModel.SystemTitle.Substring(SystemModel.SystemTitle.LastIndexOf(".") + 1).ToLower();
                string urls = Server.MapPath("..\\..\\GenerationFiles\\" + fileName.Replace(fileExtension, "html"));
                if (File.Exists(Server.MapPath(SystemModel.SystemURL)))
                {
                    File.Delete(Server.MapPath(SystemModel.SystemURL));         //删除Office文件

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
                DownLoadFile(SystemModel.SystemURL, SystemModel.SystemTitle);
            }
            else if (e.CommandName == "Look")
            {
                var model = ObjLookBLL.GetByAll().FirstOrDefault();
                if (model != null)
                {
                    var SystemModels = objCompanySystemBLL.GetByID(SystemId);
                    string url = "http://officeweb365.com/o/?i=" + model.LooksKey + "&furl=http://" + lblOutUrl.Text.Trim().ToString() + SystemModels.SystemURL.ToString();
                    Response.Write("<script>window.open('" + url + "')</script>");

                    //Response.Redirect(url, true);
                }
                else
                {
                    JavaScriptTools.AlertWindow("请您先设置外网地址", Page);
                }
            }
            DataBinder();
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

        #region 前方空格

        public string GetItemNbsp()
        {
            return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

        }
        #endregion

        #region 点击刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Url.ToString());
        }
        #endregion

        #region 是否有权限删除 修改

        public string GetJurisdiction()
        {
            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            return Model.EmployeeTypeID == 3 ? "style='display:none'" : string.Empty;
        }
        #endregion

        #region 子级 完成事件 (隐藏)


        protected void repSystemDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
            LinkButton lbtnDownLoad = e.Item.FindControl("lbtnDownLoad") as LinkButton;

            if (ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeTypeID == 3)
            {
                lbtnDelete.Visible = false;
                lbtnDownLoad.Visible = false;
            }
        }
        #endregion

        #region 获取乱码后的文件名


        public string GetSystemName(object Source)
        {
            string SystemUrl = Source.ToString();
            string fileName = SystemUrl.Substring(SystemUrl.LastIndexOf("/") + 1);
            return fileName;
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

        #region 修改外网地址
        /// <summary>
        /// 修改外网地址
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "修改")
            {
                btn.Text = "保存";
                txtOutURL.Visible = true;
                lblOutUrl.Visible = false;
                txtKeyID.Visible = true;
                lblKeyID.Visible = false;

            }
            else if (btn.Text == "保存")
            {
                var LookList = ObjLookBLL.GetByAll();
                string Url = txtOutURL.Text.Trim().ToString();
                int result = 0;
                if (LookList.Count > 0)
                {
                    sys_LookURL Model = ObjLookBLL.GetByAll().FirstOrDefault();
                    Model.LooksKey = txtKeyID.Text.ToString().ToInt32();
                    Model.URL = Url.ToString();
                    result = ObjLookBLL.Update(Model);
                }
                else if (LookList.Count == 0)
                {
                    sys_LookURL Model = new sys_LookURL();
                    Model.LooksKey = txtKeyID.Text.ToString().ToInt32();
                    Model.URL = Url.ToString();
                    result = ObjLookBLL.Insert(Model);
                }
                //if (result > 0)
                //{
                btn.Text = "修改";
                txtOutURL.Visible = false;
                lblOutUrl.Visible = true;
                txtKeyID.Visible = false;
                lblKeyID.Visible = true;
                JavaScriptTools.AlertWindow("保存成功", Page);
                DataBinder();
            }
            //    else
            //    {
            //        JavaScriptTools.AlertWindow("保存失败,请稍候再试...", Page);
            //    }
            //}
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

        #region  获取外网网站
        /// <summary>
        /// 根据制度ID获取
        /// </summary>
        public string GetUrl(object Source)
        {
            int SystemId = Source.ToString().ToInt32();
            var model = ObjLookBLL.GetByAll().FirstOrDefault();
            if (model != null)
            {
                var SystemModels = objCompanySystemBLL.GetByID(SystemId);
                string url = "http://officeweb365.com/o/?i=" + model.LooksKey + "&furl=http://" + lblOutUrl.Text.Trim().ToString() + SystemModels.SystemURL.ToString();
                return url;

                //Response.Redirect(url, true);
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
            if (ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeTypeID == 3)
            {
                return "style='display:none;'";
            }
            return "";

        }

    }
}