using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//该文件已替换为了html，可以删除
namespace HA.PMS.WeddingManagerWeb.TheStage.CompanyIntroduction
{
    public partial class ComanyIntroVideoShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //视频文件路径
                string IntroMovieFilePath = "~/Files/IntroduceVideo/IntroduceVideo.mov";
                ViewState["MoviePath"] ="/../.."+ IntroMovieFilePath.Replace("~", "");
            }
        }
    }
}