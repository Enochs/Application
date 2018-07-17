using System;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Linq;
using System.Collections;
using System.Collections.Generic;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.TheStage.ClassicCase
{
    public partial class ClassicMoviePlay : SystemPage
    {
        CaseFile objCaseFileBLL = new CaseFile();
        List<FD_CaseFile> ObjList = new List<FD_CaseFile>();
        int CaseId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CaseId = Request["CaseID"].ToInt32();
            if (!IsPostBack)
            {
                if (Request["Action"] == "More")
                {
                    if (Request["CaseFileId"].ToInt32() > 0)
                    {
                        int CaseFileId = Request.QueryString["CaseFileId"].ToInt32();
                        FD_CaseFile objCaseFile = objCaseFileBLL.GetByID(CaseFileId);
                        ltlName.Text = objCaseFile.CaseFileName;
                        ViewState["MoviePath"] = GetImgPath(objCaseFile.CaseFilePath);
                        ObjList = objCaseFileBLL.GetByCaseID(CaseId);
                        rptMovieList.DataSource = ObjList;
                        rptMovieList.DataBind();
                    }
                    else
                    {
                        ObjList = objCaseFileBLL.GetByCaseID(CaseId);
                        rptMovieList.DataSource = ObjList;
                        rptMovieList.DataBind();
                        ViewState["MoviePath"] = GetImgPath(ObjList[0].CaseFilePath);
                        ltlName.Text = ObjList[0].CaseFileName;
                    }
                }
                else
                {
                    int CaseFileId = Request.QueryString["CaseFileId"].ToInt32();
                    FD_CaseFile objCaseFile = objCaseFileBLL.GetByID(CaseFileId);
                    ltlName.Text = objCaseFile.CaseFileName;
                    ViewState["MoviePath"] = GetImgPath(objCaseFile.CaseFilePath);
                    //ObjList = objCaseFileBLL.GetByCaseID(objCaseFile.CaseId.ToString().ToInt32());
                    rptMovieList.DataSource = ObjList;
                    rptMovieList.DataBind();
                }
            }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        /// <returns></returns>
        protected string GetImgPath(object source)
        {
            string sourcePath = (source + string.Empty);
            return "../../.." + sourcePath.Replace("~", "");

        }

        protected void rptMovieList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {

        }
    }
}