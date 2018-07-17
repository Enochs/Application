using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Office.Interop.Word;
//using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.IO;

namespace HA.PMS.ToolsLibrary
{
    public static class Transformation
    {
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

    }
}
