using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.FlowerReport
{
    public partial class FlowerImageList : SystemPage
    {

        BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        Flower ObjFLowerBLL = new Flower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        protected void BinderData()
        {
            var query = ObjFLowerBLL.GetByAll().Where(C => C.Flowerkey == Request["FlowerID"].ToString().ToInt32()).ToList();
            if (query.Count > 0)
            {
                repfilelist.DataSource = query;
                repfilelist.DataBind();
            }
        }

        protected void repfilelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int fileID = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName == "DownLoad")    /*下载*/
            {
                //1.获取文件虚拟路径
                string fileLocalPath = GetServerPath(ObjFLowerBLL.GetByID(fileID).UploadImage);
                //2.下载
                try
                {
                    IOTools.DownLoadFile(Server.MapPath(fileLocalPath), ObjFLowerBLL.GetByID(fileID).ImageName);
                }
                catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败！该文件可能以被移除", Page); }
            }
            if (e.CommandName == "Delete")    /*删除*/
            {
                //1.删除文件
                System.IO.File.Delete(Server.MapPath(ObjFLowerBLL.GetByID(fileID).UploadImage));
                //2.删除记录
                var Model = ObjFLowerBLL.GetByID(Request["FlowerID"].ToString().ToInt32());
                Model.ImageName = string.Empty;
                Model.UploadImage = string.Empty;
                int result = ObjFLowerBLL.Update(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请稍候再试...", Page);
                }

                BinderData();
            }
        }
        protected string GetServerPath(object source)
        {
            //获取程序根目录
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            string imagesurl = (source + string.Empty).Replace(tmpRootDir, "");
            return imagesurl = "/" + imagesurl.Replace(@"\", @"/");
        }
    }
}