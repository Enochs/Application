using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceImageUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
            //this.ServerFileUpLoad.PostServer = "";
            Response.Cookies["FinishFloder"].Value = "QuotedFile";
            Session["PostServerUri"] = "/AdminPanlWorkArea/Control/FileServer.aspx?QuotedID=" + Request["QuotedID"] + "&Kind=" + Request["Kind"] + "&Typer=1";
        }

        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            this.ServerFileUpLoad.PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx?QuotedID=" + Request["QuotedID"] + "&Kind=" + Request["Kind"] + "&Typer=1";
            base.OnInit(e);
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            //HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
            
            //if (Directory.Exists(Server.MapPath("/AdminPanlWorkArea/FileManager/QuotedPrice/")))
            //{
            //    var FileAddress = "/AdminPanlWorkArea/FileManager/QuotedPrice/" + upfile.FileName;
            //    FL_QuotedPricefileManager ObjFileModel = new FL_QuotedPricefileManager();
            //    ObjFileModel.CreateDate = DateTime.Now;
            //    ObjFileModel.FileAddress = FileAddress;
            //    ObjFileModel.Filename = upfile.FileName;
            //    ObjFileModel.QuotedID = Request["QuotedID"].ToInt32();
            //    ObjFileModel.SortOrder = 1;
            //    ObjFileModel.KindID = Request["Kind"].ToInt32();
            //    ObjFileModel.Type = 1;
            //    ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjFileModel);
            //    upfile.SaveAs(Server.MapPath(FileAddress) + upfile.FileName);
            //}
            //else
            //{
            //    Directory.CreateDirectory(Server.MapPath("/AdminPanlWorkArea/FileManager/QuotedPrice/"));
            //    var FileAddress = "/AdminPanlWorkArea/FileManager/QuotedPrice/" + upfile.FileName;
            //    FL_QuotedPricefileManager ObjFileModel = new FL_QuotedPricefileManager();
            //    ObjFileModel.CreateDate = DateTime.Now;
            //    ObjFileModel.FileAddress = FileAddress;
            //    ObjFileModel.Filename = upfile.FileName;
            //    ObjFileModel.QuotedID = Request["QuotedID"].ToInt32();
            //    ObjFileModel.KindID = Request["Kind"].ToInt32();
            //    ObjFileModel.Type = 1;
            //    ObjFileModel.SortOrder = 1;
            //    ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjFileModel);
            //    upfile.SaveAs(Server.MapPath(FileAddress) + upfile.FileName);
            //}
            //JavaScriptTools.AlertAndClosefancybox("上传成功",this.Page);
        }


        /// <summary>
        /// 保存图片 刷新报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindowAndReaload("保存完毕！", Page);
        }
    }
}