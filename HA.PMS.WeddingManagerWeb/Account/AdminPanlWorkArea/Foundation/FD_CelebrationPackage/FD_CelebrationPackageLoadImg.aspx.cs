
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.20
 Description:套系上传页面创建页面
 History:修改日志

 Author:杨洋
 date:2013.3.20
 version:好爱1.0
 description:修改描述
 
 
 
 */
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    public partial class FD_CelebrationPackageLoadImg : System.Web.UI.Page
    {
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
        CelebrationPackageImage objCelebrationPackageImageBLL = new CelebrationPackageImage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PackageID = Convert.ToInt32(Request.QueryString["PackageID"]);
                if (PackageID == 0)
                {
                    Response.Write("请你先保存新增套系内容！！！");
                    Response.End();
                }
                else
                {
                    ViewState["PackageID"] = PackageID;
                }
            }
            else
            {
                if (this.SaveImages())
                {
                   // JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
                    JavaScriptTools.AlertAndClosefancyboxNoRenovate("添加成功", this.Page);
                }

            }
        }

        private Boolean SaveImages()
        {
            /**/
            ///'遍历File表单元素 
            HttpFileCollection files = HttpContext.Current.Request.Files;

            try
            {
                for (int iFile = 0; iFile < files.Count; iFile++)
                {
                    /**/
                    ///'检查文件扩展名字 
                    HttpPostedFile postedFile = files[iFile];
                    string fileName, fileExtension;
                    fileName = System.IO.Path.GetFileName(postedFile.FileName);
                    if (fileName != "")
                    {
                        fileExtension = System.IO.Path.GetExtension(fileName);

                        postedFile.SaveAs(Server.MapPath("~/Files/CelebrationPackage/") +
                            Path.GetFileName(fileName));

                        FD_CelebrationPackageImage fD_CelebrationPackageImage = new FD_CelebrationPackageImage()
                        {
                            ImageUrl = "~/Files/CelebrationPackage/" + Path.GetFileName(fileName),
                            IsDelete = false,
                            //算出目前最大的PackageID
                            PackageId =Convert.ToInt32(Request.QueryString["PackageID"])
                        };
                        objCelebrationPackageImageBLL.Insert(fD_CelebrationPackageImage);
                    }
                }

                return true;
            }
            catch (System.Exception Ex)
            {

                return false;
            }
        }
    }
}