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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class SaveCeleToDB : HandleSaveImg
    {
        CaseFile objCaseFileBLL = new CaseFile();
        public override void SavetoDataBase(string Address, string Filename)
        {
            string urlPar = Request.QueryString.GetKey(0);
            int urlParId = Request.QueryString[urlPar].ToInt32();
            FD_CaseFile objCaseFile = new FD_CaseFile();
            objCaseFile.CaseFileName = Filename;
            objCaseFile.CaseFilePath = Address;
            objCaseFile.CaseId = urlParId;
            objCaseFile.FileType = 2;
            objCaseFileBLL.Insert(objCaseFile);
        }
    }
}