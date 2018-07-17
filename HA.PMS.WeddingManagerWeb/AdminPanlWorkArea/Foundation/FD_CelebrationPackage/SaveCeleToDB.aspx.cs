using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    public partial class SaveCeleToDB : HandleSaveImg
    {
        CelebrationPackageImage objCelebrationPackageImageBLL = new CelebrationPackageImage();

        public override void SavetoDataBase(string Address, string Filename)
        {
            string urlPar = Request.QueryString.GetKey(0);
            int urlParId = Request.QueryString[urlPar].ToInt32();
            if (urlPar == "PackageID")
            {
                FD_CelebrationPackageImage imgs = new FD_CelebrationPackageImage();

                imgs.ImageUrl = Address;
                imgs.PackageId = urlParId;
                imgs.IsDelete = false;

                objCelebrationPackageImageBLL.Insert(imgs);
            }


        }
        public override void SaveFiles(string url)
        {
            int packageId = Request.QueryString["PackageID"].ToInt32();
            //套系图片不能进行上传超过16张
            var query = objCelebrationPackageImageBLL.GetByAll().Where(C => C.PackageId == packageId);
            ///'遍历File表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string NewfileName = string.Empty;
            ///'检查文件扩展名字
            //HttpPostedFile postedFile = files[iFile];
            HttpPostedFile postedFile = Request.Files["Filedata"]; //得到要上传文件
            string fileName, fileExtension, filesize, cadFileNameList;

            string URIAddress = string.Empty;

            cadFileNameList = string.Empty;
            fileName = System.IO.Path.GetFileName(postedFile.FileName.ToString()); //Guid.NewGuid().ToString() +  //得到文件名

            //            fileName = Guid.NewGuid().ToString() + System.IO.Path.GetFileName(postedFile.FileName.ToString()); //得到文件名
            filesize = System.IO.Path.GetFileName(postedFile.ContentLength.ToString()); //得到文件大小


            if (fileName != "")
            {
                fileExtension = System.IO.Path.GetExtension(fileName);//'获取扩展名

                string ServerFloder = string.Empty;

                URIAddress = url + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                ServerFloder = Server.MapPath(URIAddress);
                string Floders = System.Web.HttpContext.Current.Request.MapPath(url);
                //注意：可能要修改你的文件夹的匿名写入权限。

                //ServerFloder = Session["UploadCoder"].ToString() + ServerFloder;
                if (!Directory.Exists(ServerFloder))
                {
                    Directory.CreateDirectory(ServerFloder);
                }


                if (!Directory.Exists(Floders))
                {
                    Directory.CreateDirectory(Floders);
                }

                postedFile.SaveAs(Floders + fileName);

                NewfileName = Guid.NewGuid().ToString() + System.IO.Path.GetFileName(postedFile.FileName.ToString());

                File.Move(Floders + fileName, ServerFloder + NewfileName);
                URIAddress = URIAddress + NewfileName;
            }

            SavetoDataBase(URIAddress, fileName);
        }
    }
}