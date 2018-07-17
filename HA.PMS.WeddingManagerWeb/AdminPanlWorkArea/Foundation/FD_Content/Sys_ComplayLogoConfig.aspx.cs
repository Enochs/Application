using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class Sys_ComplayLogoConfig : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgLogo.Src = "~/Files/Logo/Logo.jpg";

            }
        }

        protected void btnFileLoad_Click(object sender, EventArgs e)
        {
            string savaPath = "~/Files/Logo/";
            string fileExt = "";
            if (fuLoadFile.HasFile)
            {

                fileExt = System.IO.Path.GetExtension(fuLoadFile.FileName);

                if (fileExt == ".jpg" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".png")
                {

                    try
                    {
                        //先删除之前的LOGO图片
                        FileInfo infos = new FileInfo(Server.MapPath(savaPath) + "Logo.jpg");
                        infos.Delete();
                        savaPath = Server.MapPath(savaPath) + "Logo" + ".jpg";
                        fuLoadFile.SaveAs(savaPath);



                        JavaScriptTools.RegisterJsCodeSource("alert('上传成功');window.parent.document.location=window.parent.document.location", this.Page);


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("你上传图片格式有误", this.Page);
                }
            }
        }


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            HandleModel.HandleContent = "基础信息设置-上传Logo";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 13;     //基础信息设置
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

    }
}