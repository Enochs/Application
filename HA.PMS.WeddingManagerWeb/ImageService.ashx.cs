using HA.PMS.DataAssmblly;
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.FilServers
{
    /// <summary>
    /// ImageService 的摘要说明
    /// </summary>
    public class ImageService : IHttpHandler
    {

        //public void ProcessRequest(HttpContext context)
        //{
        //    context.Response.ContentType = "text/plain";
        //    context.Response.Charset = "utf-8";
        //    //图片类型ID
       
        //    HttpPostedFile file = context.Request.Files["Filedata"];

        //    var FileUri =System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"] + Guid.NewGuid().ToString() +"."+file.FileName.Substring(file.FileName.LastIndexOf('.')+1);
        //    string uploadPath = HttpContext.Current.Server.MapPath(FileUri);
        //    //临时文件夹
        //    //string TemplateFloder = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FilesTemporary"]);

        //    if (file != null)
        //    {
        //        if (!Directory.Exists(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"])))
        //        {
        //            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"]));
        //        }
        //        //保存文件

        //        file.SaveAs(uploadPath);
        //        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        //        QuotedPricefileManager ObjQuotedPricefileManagerBLL=new QuotedPricefileManager();
        //        //var FileAddress = uploadPath + file.FileName;
        //        //上传文件夹
        //        //FL_QuotedPricefileManager ObjQuotedPricefileManagerModel = new FL_QuotedPricefileManager();
        //        FL_QuotedPricefileManager ObjFileModel = new FL_QuotedPricefileManager();
        //        ObjFileModel.CreateDate = DateTime.Now;
        //        ObjFileModel.FileAddress = FileUri;
        //        ObjFileModel.Filename = file.FileName;
        //        ObjFileModel.QuotedID = context.Request["QuotedID"].ToInt32();
        //        ObjFileModel.SortOrder = 1;
        //        ObjFileModel.KindID = context.Request["KindID"].ToInt32();
        //        if (context.Request["Kind"].ToInt32() != 0)
        //        {
        //            ObjFileModel.Type = 1;
        //            ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjFileModel);
        //            var ObjQuotedModel=ObjQuotedPriceBLL.GetByID( context.Request["QuotedID"].ToInt32());
        //            ObjQuotedModel.HaveFile = true;
        //            ObjQuotedPriceBLL.Update(ObjQuotedModel);
        //        }
        //        else
        //        {
        //            ObjFileModel = new FL_QuotedPricefileManager();
        //            var ObjquotedfileModel=ObjQuotedPriceBLL.GetQuotedPricefileByQuotedID(context.Request["QuotedID"].ToInt32(), 2);
        //            if (ObjquotedfileModel != null)
        //            {
        //                ObjquotedfileModel.CreateDate = DateTime.Now;
        //                ObjquotedfileModel.FileAddress = FileUri;
        //                ObjquotedfileModel.Filename = file.FileName;
        //                ObjquotedfileModel.QuotedID = context.Request["QuotedID"].ToInt32();
        //                ObjquotedfileModel.SortOrder = 1;
        //                ObjquotedfileModel.KindID = 0;
        //                ObjquotedfileModel.Type = 2;
        //                ObjQuotedPriceBLL.UpdateQuotedPricefileManager(ObjquotedfileModel);
        //                var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(context.Request["QuotedID"].ToInt32());
        //                ObjQuotedModel.HaveFile = true;
        //                ObjQuotedPriceBLL.Update(ObjQuotedModel);
        //            }
        //            else
        //            {
        //                ObjquotedfileModel = new FL_QuotedPricefileManager();
        //                ObjquotedfileModel.CreateDate = DateTime.Now;
        //                ObjquotedfileModel.FileAddress = FileUri;
        //                ObjquotedfileModel.Filename = file.FileName;
        //                ObjquotedfileModel.QuotedID = context.Request["QuotedID"].ToInt32();
        //                ObjquotedfileModel.SortOrder = 1;
        //                ObjquotedfileModel.KindID = 0;
        //                ObjquotedfileModel.Type = 2;
        //                ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjquotedfileModel);
        //                var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(context.Request["QuotedID"].ToInt32());
        //                ObjQuotedModel.HaveFile = true;
        //                ObjQuotedPriceBLL.Update(ObjQuotedModel);
        //            }
        //        }
       
        //       // upfile.SaveAs(Server.MapPath(FileAddress) + upfile.FileName);
        //        //添加对应的图片信息到数据库
        //        //FD_ImageWarehouse fd_ImageWarehouse = new FD_ImageWarehouse()
        //        //{
        //        //    ImageTitle = file.FileName,
        //        //    ImageTypeId = typeId,
        //        //    ImageUrl = "~" + context.Request["folder"] + "/" + file.FileName,
        //        //    IsDelete = false
        //        //};

        //        //objImageWarehouseBLL.Insert(fd_ImageWarehouse);
        //        //返回结果
        //        context.Response.Write(file.FileName);
        //    }
        //    else
        //    {
        //        context.Response.Write("上传失败");
        //    }
        //}

        //public bool IsReusable
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}