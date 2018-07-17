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
namespace HA.PMS.WeddingManagerWeb.Handlers
{
    /// <summary>
    /// BatchLoadFile 的摘要说明
    /// </summary>
    public class BatchLoadFile : IHttpHandler
    {
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
        CelebrationPackageImage objCelebrationPackageImageBLL = new CelebrationPackageImage();
        public void ProcessRequest(HttpContext context)
        {
            int countFile = context.Request.Files.Count;
            
            if (countFile > 0)
            {
             
                for (int i = 0; i < countFile; i++)
                {
                  HttpPostedFile file = context.Request.Files[i];
                  if (file.ContentLength > 0 && file.ContentLength < 1048576|| !string.IsNullOrEmpty(file.FileName) )
                  {
                      //保存文件
                      file.SaveAs(context.Server.MapPath("~/Files/CelebrationPackage/")+Path.GetFileName(file.FileName));
                      FD_CelebrationPackageImage fD_CelebrationPackageImage = new FD_CelebrationPackageImage() 
                      {
                          ImageUrl = "~/Files/CelebrationPackage/" + Path.GetFileName(file.FileName),
                           IsDelete=false,
                           //算出目前最大的PackageID
                          PackageId = context.Request.QueryString["PackageID"].ToInt32()
                      };
                      objCelebrationPackageImageBLL.Insert(fD_CelebrationPackageImage);
                    
                  }
                }
                context.Response.Write("<script>alert('保存图片成功');parent.parent.$.fancybox.close(1);</script>");
               // context.Response.Redirect("~/AdminPanlWorkArea/Foundation/FD_CelebrationPackage/FD_CelebrationPackageCreate.aspx");

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