using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceItemFileList : SystemPage
    {
        BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        protected void BinderData()
        {
            var query = ObjQuotedPriceBLL.GetImageByKind(Request["QuotedID"].ToInt32(), Request["ChangeID"].ToInt32(), 1);
            repfilelist.DataSource = query;
            repfilelist.DataBind();
        }

        protected void repfilelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int fileID = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName == "DownLoad")    /*下载*/
            {
                //1.获取文件虚拟路径
                string fileLocalPath = GetServerPath(ObjQuotedPriceBLL.GetQuotedPricefileByFileID(fileID).FileAddress);
                //2.下载
                try
                {
                    IOTools.DownLoadFile(Server.MapPath(fileLocalPath), ObjQuotedPriceBLL.GetQuotedPricefileByFileID(fileID).Filename);
                }
                catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败！该文件可能以被移除", Page); }
            }
            if (e.CommandName == "Delete")    /*删除*/
            {
                //1.删除文件
                System.IO.File.Delete(Server.MapPath(ObjQuotedPriceBLL.GetQuotedPricefileByFileID(fileID).FileAddress));
                //2.删除记录
                ObjQuotedPriceBLL.DeleteQuotedPricefile(new FL_QuotedPricefileManager() { FileID = fileID });
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