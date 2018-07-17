using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.IO;
namespace HA.PMS.WeddingManagerWeb
{
    /// <summary>
    /// LoadFiles 的摘要说明
    /// </summary>
    public class LoadFiles : IHttpHandler
    {
        ImageWarehouse objImageWarehouseBLL = new ImageWarehouse();
        public void ProcessRequest(HttpContext context)
        {



            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            //图片类型ID
            int typeId = context.Request.QueryString["typeId"].ToInt32();
            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath =
                    HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                //保存文件
                file.SaveAs(uploadPath + file.FileName);
                //添加对应的图片信息到数据库
                FD_ImageWarehouse fd_ImageWarehouse = new FD_ImageWarehouse()
                {
                    ImageTitle = file.FileName,
                    ImageTypeId = typeId,
                    ImageUrl = "~"+context.Request["folder"] +"/"+ file.FileName,
                    IsDelete = false
                };

                objImageWarehouseBLL.Insert(fd_ImageWarehouse);
                //返回结果
                context.Response.Write(file.FileName);
            }
            else
            {
                context.Response.Write("上传失败");
            }
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