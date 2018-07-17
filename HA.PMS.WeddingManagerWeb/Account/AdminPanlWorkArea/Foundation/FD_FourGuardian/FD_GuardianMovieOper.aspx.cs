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
using System.IO;
using System.Xml;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class FD_GuardianMovieOper : SystemPage
    {
        GuardianMovie objGuardianMovieBLL = new GuardianMovie();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        protected void DataBinder()
        {
            int GuardianId = Request.QueryString["GuardianId"].ToInt32();
            ViewState["GuardianId"] = GuardianId;
            #region 分页页码
            int startIndex = MoviePager.StartRecordIndex;
            int resourceCount = 0;
            var query = objGuardianMovieBLL.GetByIndex(GuardianId, MoviePager.PageSize, MoviePager.CurrentPageIndex, out resourceCount);
            MoviePager.RecordCount = resourceCount;

            rptMovie.DataSource = query;
            rptMovie.DataBind();


            #endregion
        }


        protected void MoviePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptMovie_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int GuradinMovieID = (e.CommandArgument + string.Empty).ToInt32();

            if (e.CommandName == "Delete")
            {
                FD_GuardianMovie movies = objGuardianMovieBLL.GetByID(GuradinMovieID);
                objGuardianMovieBLL.Delete(movies);
                DataBinder();
                JavaScriptTools.AlertWindow("删除成功", this.Page);
            }
        }
    }
}