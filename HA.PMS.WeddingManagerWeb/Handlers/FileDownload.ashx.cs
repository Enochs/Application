using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Ionic.Zip;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
namespace HA.PMS.WeddingManagerWeb.Handlers
{
    /// <summary>
    /// FileDownload 的摘要说明
    /// </summary>
    public class FileDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int DistributedId = context.Request.QueryString["DistributedId"].ToInt32();
            //int DistributedId = context.Request["did"].ToInt32();
        }
        /// <summary>
        /// 单个文件进行下载
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        public void DownLoad(string path, HttpContext context)
        {
            FileInfo fileInfo = new FileInfo(path);

            //防止中文出现乱码
            string filename = HttpUtility.UrlEncode(fileInfo.Name);
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            context.Response.WriteFile(path);


        }
        /// <summary>
        /// 批量下载方法
        /// </summary>
        /// <param name="path"></param>
        public void BatchDownLoad(string path, HttpContext context)
        {
            string[] pathArray = path.Split('*');
            //后四位随机数的生成是为了不与本地文件相冲突
            string timeNow = DateTime.Now.ToString("yyyy年mm月dd日 hh时mm分ss秒") + new Random().Next(1000, 9999).ToString();

            string ConvertName = HttpUtility.UrlEncode(timeNow);
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + ConvertName + ".zip");
            using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))//解决中文乱码题目
            {

                foreach (var item in pathArray)
                {
                    //注意在文件下载的时候，如果有中文名的文件，下载时，会有问题，
                    //问题的原因取决于但是运行的系统编码，通常不会出问题
                    zip.AddFile(item, "www.holdlove.cn");
                    //  zip.AddFile(Server.MapPath("~/" + item), "www.holdlove.cn");
                    //zip.AddFile(HttpUtility.UrlEncode(Server.MapPath("~/"+item)),"www.holdlove.cn");
                }
                zip.Save(context.Response.OutputStream);
            }
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}