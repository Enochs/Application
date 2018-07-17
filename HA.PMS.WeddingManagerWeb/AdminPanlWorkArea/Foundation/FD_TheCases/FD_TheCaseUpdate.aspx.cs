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
    public partial class FD_TheCaseUpdate : SystemPage
    {
        TheCase ObjTheCaseBLL = new TheCase();
        CaseFile ObjCaseFileBLL = new CaseFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CaseID = Request.QueryString["CaseID"].ToInt32();
                FD_TheCase objCase = ObjTheCaseBLL.GetByID(CaseID);
                if (objCase != null)
                {
                    txtCaseName.Text = objCase.CaseName + string.Empty;
                    txtCaseStyle.Text = objCase.CaseStyle + string.Empty;
                    txtCaseHotel.Text = objCase.CaseHotel + string.Empty;
                    txtCaseDetails.Text = objCase.CaseDetails + string.Empty;
                    txtCaseGroom.Text = objCase.CaseGroom + string.Empty;
                    txtCaseOrder.Text = objCase.CaseOrder.HasValue ? objCase.CaseOrder.Value.ToString() : "100";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FD_TheCase ObjTheCaseModel = ObjTheCaseBLL.GetByID(Request.QueryString["CaseID"].ToInt32());
            ObjTheCaseModel.CaseName = txtCaseName.Text.Trim();
            ObjTheCaseModel.CaseStyle = txtCaseStyle.Text.Trim();
            ObjTheCaseModel.CaseHotel = txtCaseHotel.Text.Trim();
            ObjTheCaseModel.CaseDetails = txtCaseDetails.Text.Trim();
            ObjTheCaseModel.CaseGroom = txtCaseGroom.Text.Trim();
            int IntCaseOrder;//排序号
            ObjTheCaseModel.CaseOrder = int.TryParse(txtCaseOrder.Text, out IntCaseOrder) ? IntCaseOrder : 100;

            FD_CaseFile ObjCaseFileModel = new FD_CaseFile();
            ObjCaseFileModel.CaseFileName = txtCaseName.Text.Trim();
            ObjCaseFileModel.FileType = 2;

            string savaPath = string.Format("/Files/TheCase/TheCaseImg/{0}/{1}/", DateTime.Now.Year, DateTime.Now.Month);
            string thumbPath = string.Format("{0}TheThumb/", savaPath);//缩略图保存目录路径
            string[] fileExtArray = { ".jpeg", ".jpg", ".png", "gif", ".bmp" };//支持上传文件类型
            float intThumbImageWidth = 480, intThumbImageHeight = 320;//缩略图大小


            string fileExt = System.IO.Path.GetExtension(flLoad.FileName).ToLower();

            ImageToools imgsTool = new ImageToools();
            if (!Directory.Exists(Server.MapPath(savaPath)))
            {
                Directory.CreateDirectory(Server.MapPath(savaPath));
            }
            if (!Directory.Exists(Server.MapPath(thumbPath)))
            {
                Directory.CreateDirectory(Server.MapPath(thumbPath));
            }

            string OldCasePath = ObjTheCaseModel.CasePath;
            string OldCaseSmallPath = ObjTheCaseModel.CaseSmallPath;
            
            //如果有上传图片，就作封面图片的修改，没有上传就不作封面的图片修改
            if (flLoad.HasFile)
            {
                if (fileExtArray.Contains(fileExt))
                {
                    //封面图片
                    ObjTheCaseModel.CasePath = savaPath + Guid.NewGuid() + fileExt;
                    //保存封面图片到硬盘
                    flLoad.SaveAs(Server.MapPath(ObjTheCaseModel.CasePath));

                    //缩略图文件路径
                    ObjTheCaseModel.CaseSmallPath = thumbPath + Guid.NewGuid().ToString() + fileExt;
                    //保存缩略图到硬盘
                    if (!imgsTool.ToProportionThumbnailImage(Server.MapPath(ObjTheCaseModel.CasePath), Server.MapPath(ObjTheCaseModel.CaseSmallPath), intThumbImageWidth, intThumbImageHeight))
                    {
                        JavaScriptTools.AlertWindow("为了图片能正常显示，请上传像素为大于 480*320 的图片！，最佳图片宽高比例为 3：2！", this.Page);
                        return;
                    }

                    //保存案例图片路径
                    string CaseFilePath = savaPath + Guid.NewGuid() + fileExt;
                    //将封面图片添加到案例图片
                    ObjCaseFileModel.CaseFileName = Path.GetFileName(flLoad.FileName);
                    ObjCaseFileModel.CaseFilePath = CaseFilePath;
                    File.Copy(Server.MapPath(ObjTheCaseModel.CasePath), Server.MapPath(CaseFilePath), true);
                    ObjCaseFileModel.CaseId = ObjTheCaseModel.CaseID;
                    ObjCaseFileBLL.Insert(ObjCaseFileModel);

                    //删除上次封面图片文件
                    System.IO.File.Delete(Server.MapPath(OldCasePath));
                    //删除上次缩略图图片文件
                    System.IO.File.Delete(Server.MapPath(OldCaseSmallPath));
                }
                else
                {
                    JavaScriptTools.AlertWindow("该文件不是有效的图片格式，有效图片格式为： .jpeg .jpg .png .gif .bmp", this.Page);
                    return;
                }
            }

            ObjTheCaseBLL.Update(ObjTheCaseModel);

            JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);

        }

    }
}