using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class HandleSaveImg : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------------------------
            //组件设置a.MD5File为2，3时 的实例代码
            if (Request.QueryString["access2008_cmd"] != null && Request.QueryString["access2008_cmd"] == "2")//服务器提交MD5验证后的文件信息进行验证
            {

                Response.Write("0");//返回命令  0 = 开始上传文件， 2 = 不上传文件，前台直接显示上传完成
                Response.End();
            }
            else if (Request.QueryString["access2008_cmd"] != null && Request.QueryString["access2008_cmd"] == "3") //服务器提交文件信息进行验证
            {
                Response.Write("1");//返回命令 0 = 开始上传文件,1 = 提交MD5验证后的文件信息进行验证, 2 = 不上传文件，前台直接显示上传完成
                Response.End();
            }
            //---------------------------------------------------------------------------------------------

            if (Request.Files["Filedata"] != null)//判断是否有文件上传上来
            {
                string urlPar = Request.QueryString.GetKey(0);


                SaveFiles(System.Configuration.ConfigurationManager.AppSettings[urlPar]);
                Response.End();
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="url">保存路径,填写相对路径</param>
        /// <returns></returns>
        public virtual void SaveFiles(string url)
        {
            ///'遍历File表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string NewfileName = string.Empty;
            ///'检查文件扩展名字
            //HttpPostedFile postedFile = files[iFile];
            HttpPostedFile postedFile = Request.Files["Filedata"]; //得到要上传文件
            string fileName, fileExtension, filesize, cadFileNameList;

            string URIAddress = string.Empty;

            cadFileNameList = string.Empty;
            fileName =Replaces(System.IO.Path.GetFileName(postedFile.FileName.ToString())); //Guid.NewGuid().ToString() +  //得到文件名

            //            fileName = Guid.NewGuid().ToString() + System.IO.Path.GetFileName(postedFile.FileName.ToString()); //得到文件名
            filesize = System.IO.Path.GetFileName(postedFile.ContentLength.ToString()); //得到文件大小


            if (fileName != "")
            {
                fileExtension = System.IO.Path.GetExtension(fileName);//'获取扩展名

                string ServerFloder = string.Empty;

                URIAddress = url+ "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                
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

                NewfileName = Replaces(Guid.NewGuid().ToString() + System.IO.Path.GetFileName(postedFile.FileName.ToString()));

                File.Move(Floders + fileName, ServerFloder + NewfileName);
                URIAddress = Replaces(URIAddress + NewfileName);
            }

            SavetoDataBase(URIAddress, Replaces(fileName));
        }



        /// <summary>
        /// 保存到数据库
        /// </summary>
        public virtual void SavetoDataBase(string Address, string Filename)
        {
            string urlPar = Request.QueryString.GetKey(0);
            int urlParId = Request.QueryString[urlPar].ToInt32();
             
        }

        public string Replaces(string Address)
        {
            Address = Address.Replace("+", "");
            Address = Address.Replace("(", "");
            Address = Address.Replace(")", "");
            return Address;
        }
    }
}