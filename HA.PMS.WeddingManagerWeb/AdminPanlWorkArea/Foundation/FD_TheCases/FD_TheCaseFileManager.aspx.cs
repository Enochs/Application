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
using System.Data.Objects;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class FD_TheCaseFileManager : SystemPage
    {
        HA.PMS.BLLAssmblly.FD.CaseFile ObjCaseFileBLL = new HA.PMS.BLLAssmblly.FD.CaseFile();
        HA.PMS.BLLAssmblly.FD.TheCase ObjTheCaseBLL = new HA.PMS.BLLAssmblly.FD.TheCase();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int CaseID = Request.QueryString["CaseID"].ToInt32();
                int FileType = Request.QueryString["FileType"].ToInt32();
                ViewState["load"] = "'" + EncodeBase64("/AdminPanlWorkArea/Foundation/FD_TheCases/SaveCeleToDB") + "'";
                ViewState["CaseID"] = CaseID;
                if (FileType == 2)
                {
                    phImg.Visible = true;
                    phMovie.Visible = false;
                }
                else
                {
                    phImg.Visible = false;
                    phMovie.Visible = true;
                }
                DataBinder();
            }
        }
        /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder()
        {
            int FileType = Request.QueryString["FileType"].ToInt32();
            int CaseID = Request.QueryString["CaseID"].ToInt32();
            int startIndex = TheCaseFilePager.StartRecordIndex;
            //如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。

            int resourceCount = 0;

            var query = ObjCaseFileBLL.GetByIndex(FileType, CaseID, TheCaseFilePager.PageSize, TheCaseFilePager.CurrentPageIndex, out resourceCount);

            // FD_CaseFile TheSameFile = query.Where(C => C.CaseFilePath == ObjTheCaseBLL.GetByID(CaseID).CasePath).FirstOrDefault();
            //
            // query.Remove(TheSameFile);
            //
            // query.Insert(0, TheSameFile);

            TheCaseFilePager.RecordCount = resourceCount;

            rptTheCaseFile.DataSource = query;
            rptTheCaseFile.DataBind();


        }

        protected void TheCaseFilePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptTheCaseFile_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int CaseFileId = e.CommandArgument.ToString().ToInt32();

            if (e.CommandName == "Delete")
            {
                HA.PMS.DataAssmblly.FD_CaseFile fD_CaseFile = new HA.PMS.DataAssmblly.FD_CaseFile()
                {
                    CaseFileId = CaseFileId
                };
                if (File.Exists(Server.MapPath(fD_CaseFile.CaseFilePath)))  //删除服务器里的文件
                {
                    File.Delete(Server.MapPath(fD_CaseFile.CaseFilePath));
                }
                ObjCaseFileBLL.Delete(fD_CaseFile);
                //删除之后重新绑定数据源

                int FileType = Request.QueryString["FileType"].ToInt32();
                int CaseID = Request.QueryString["CaseID"].ToInt32();
                DataBinder();
            }
            //设置封面
            else if (e.CommandName == "Set")
            {
                var ObjCaseFileModel = ObjCaseFileBLL.GetByID(CaseFileId);
                var ObjTheCaseModel = ObjTheCaseBLL.GetByID(ObjCaseFileModel.CaseId);

                ObjTheCaseModel.CasePath = ObjCaseFileModel.CaseFilePath;
                ObjTheCaseModel.CaseSmallPath = string.Format("/Files/TheCase/TheCaseImg/{0}/{1}/TheThumb/{2}{3}",
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    Guid.NewGuid().ToString(),
                    System.IO.Path.GetExtension(ObjCaseFileModel.CaseFilePath));


                ObjTheCaseBLL.Update(ObjTheCaseModel);

                ImageToools imgsTop = new ImageToools();
                float intThumbImageWidth = 480, intThumbImageHeight = 320;
                //zoom the image save to hard disk
                bool IsSaveedThumbSuccess = imgsTop.ToProportionThumbnailImage(Server.MapPath(ObjTheCaseModel.CasePath), Server.MapPath(ObjTheCaseModel.CaseSmallPath), intThumbImageWidth, intThumbImageHeight);
                JavaScriptTools.AlertWindow("设置成功！", Page);

            }
        }

        protected void btnReflush_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }
    }
}