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
    public partial class FD_TheCaseManager : SystemPage
    {
        TheCase objTheCaseBLL = new TheCase();
        CaseFile objTheCaseFileBLL = new CaseFile();
        int SourceCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder()
        {
            #region 注释
            //int startIndex = TheCasePager.StartRecordIndex;
            ////如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。
            //int resourceCount = 0;
            //var query = objTheCaseBLL.GetByIndex(TheCasePager.PageSize, TheCasePager.CurrentPageIndex, out resourceCount).OrderBy(C => C.CaseOrder);
            #endregion

            List<PMSParameters> pars = new List<PMSParameters>();
            //案例名称
            pars.Add(txtName.Text.Trim() != string.Empty, "CaseName", txtName.Text.Trim().ToString(), NSqlTypes.LIKE);
            //酒店
            pars.Add(txtWineShop.Text.Trim() != string.Empty, "CaseHotel", txtWineShop.Text.Trim().ToString(), NSqlTypes.LIKE);
            //风格
            pars.Add(txtStyle.Text.Trim() != string.Empty, "CaseStyle", txtStyle.Text.Trim().ToString(), NSqlTypes.LIKE);


            var DataList = objTheCaseBLL.GetCaseByParameter(pars, "CaseOrder", TheCasePager.PageSize, TheCasePager.CurrentPageIndex, out SourceCount);

            rptTheCase.DataSource = DataList;
            rptTheCase.DataBind();
            TheCasePager.RecordCount = SourceCount;


        }

        #region 分页
        /// <summary>
        /// 点击分页 上一页 下一页
        /// </summary>
        protected void TheCasePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion


        #region 删除
        /// <summary>
        /// 执行删除功能
        /// </summary>
        protected void rptTheCase_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                int CaseID = e.CommandArgument.ToString().ToInt32();

                HA.PMS.DataAssmblly.FD_TheCase fD_TheCase = objTheCaseBLL.GetByID(CaseID);
                if (File.Exists(Server.MapPath(fD_TheCase.CasePath)))
                {
                    File.Delete(Server.MapPath(fD_TheCase.CasePath));
                }
                //这里暂时只删除图片，不对视频文件进行删除操作，防止用户视频并没有进行备份
                var objCaseFileResult = objTheCaseFileBLL.GetByAll().Where(C => C.CaseId == CaseID && C.FileType == 2).ToList();
                for (int i = 0; i < objCaseFileResult.Count; i++)
                {
                    if (File.Exists(Server.MapPath(objCaseFileResult[i].CaseFilePath)))
                    {
                        File.Delete(Server.MapPath(objCaseFileResult[i].CaseFilePath));
                    }
                    objTheCaseFileBLL.Delete(objCaseFileResult[i]);
                }


                objTheCaseBLL.Delete(fD_TheCase);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary> 
        protected void btnLook_Click(object sender, EventArgs e)
        {
            TheCasePager.CurrentPageIndex = 1;
            DataBinder();
        }
        #endregion
    }
}