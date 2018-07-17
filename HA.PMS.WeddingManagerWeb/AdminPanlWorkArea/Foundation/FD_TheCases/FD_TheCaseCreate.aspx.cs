using System;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class FD_TheCaseCreate : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FD_TheCase ObjTheCaseModel = new FD_TheCase();
            ObjTheCaseModel.CaseName = txtCaseName.Text.Trim();
            ObjTheCaseModel.CaseStyle = txtCaseStyle.Text.Trim();
            ObjTheCaseModel.CaseHotel = txtCaseHotel.Text.Trim();
            ObjTheCaseModel.CaseDetails = txtCaseDetails.Text.Trim();
            ObjTheCaseModel.CaseGroom = txtCaseGroom.Text.Trim();
            ObjTheCaseModel.CaseOrder = 100;

            FD_CaseFile ObjCaseFileModel = new FD_CaseFile();
            ObjCaseFileModel.CaseFileName = txtCaseName.Text.Trim();
            ObjCaseFileModel.FileType = 2;
            

            string savaPath = string.Format("/Files/TheCase/TheCaseImg/{0}/{1}/", DateTime.Now.Year, DateTime.Now.Month);//案例图片保存目录路径
            string thumbPath = string.Format("{0}TheThumb/", savaPath);//缩略图保存目录路径
            string[] fileExtArray = { ".jpeg", ".jpg", ".png", "gif", ".bmp" };//支持上传文件类型
            float intThumbImageWidth = 480, intThumbImageHeight = 320;//缩略图大小

            if (flLoad.HasFile||flLoad.FileName!=string.Empty)
            {
                string fileExt = System.IO.Path.GetExtension(flLoad.FileName).ToLower();
                if (fileExtArray.Contains(fileExt))
                {
                    if (!Directory.Exists(Server.MapPath(savaPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(savaPath));
                    }
                    if (!Directory.Exists(Server.MapPath(thumbPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(thumbPath));
                    }

                    //封面图片
                    ObjTheCaseModel.CasePath = savaPath + Guid.NewGuid() + fileExt;
                    //保存封面图片到硬盘
                    flLoad.SaveAs(Server.MapPath(ObjTheCaseModel.CasePath));
                    //缩略图文件路径
                    ObjTheCaseModel.CaseSmallPath = thumbPath + Guid.NewGuid().ToString() + fileExt;

                    //并且如果上传的图片大小符合指定要求，剪切上传图片到缩略图地址。
                    if (new ImageToools().ToProportionThumbnailImage(Server.MapPath(ObjTheCaseModel.CasePath), Server.MapPath(ObjTheCaseModel.CaseSmallPath), intThumbImageWidth, intThumbImageHeight))
                    {
                        new TheCase().Insert(ObjTheCaseModel);

                        //保存案例图片路径
                        string CaseFilePath = savaPath + Guid.NewGuid() + fileExt;
                        //将封面图片添加到案例图片
                        ObjCaseFileModel.CaseFileName = Path.GetFileName(flLoad.FileName);
                        ObjCaseFileModel.CaseFilePath = CaseFilePath;
                        File.Copy(Server.MapPath(ObjTheCaseModel.CasePath), Server.MapPath(CaseFilePath), true);
                        ObjCaseFileModel.CaseId = ObjTheCaseModel.CaseID;
                        new CaseFile().Insert(ObjCaseFileModel);

                        JavaScriptTools.AlertWindow("保存成功！", this.Page);
                        ClearTextBox();
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("为了图片能正常显示，请上传像素为大于 480*320 的图片，最佳图片宽高比例为 3：2", this.Page);
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("该文件不是有效的图片格式，有效图片格式为： .jpeg .jpg .png .gif .bmp", this.Page);
                }

            }
            else
            {
                JavaScriptTools.AlertWindow("请先上传封面图片！", this.Page);
            }
        }

        private void ClearTextBox()
        {
            txtCaseDetails.Text = string.Empty;
            txtCaseGroom.Text = string.Empty;
            txtCaseHotel.Text = string.Empty;
            txtCaseName.Text = string.Empty;
            txtCaseStyle.Text = string.Empty;
        }
    }
}