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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class GuardianMoviePlay : SystemPage
    {
        GuardianMovie objGuardianMovieBLL = new GuardianMovie();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int GuradinMovieID = Request.QueryString["GuradinMovieID"].ToInt32();
                FD_GuardianMovie movie=objGuardianMovieBLL.GetByID(GuradinMovieID);
                ltlName.Text = movie.MovieName;
                ViewState["MovieTopImagePath"] = GetImgPath(movie.MovieTopImagePath);
                ViewState["MoviePath"] = GetImgPath(movie.MoviePath);
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

    }
}