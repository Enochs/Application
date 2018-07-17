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

namespace HA.PMS.WeddingManagerWeb.TheStage.Foundation
{
    public partial class FD_GuardianDetails : Page
    {
        FourGuardian objFourGuardianBLL = new FourGuardian();
        GuardianType objGuardianTypeBLL = new GuardianType();
        GuardianImage objGuardianImageBLL = new GuardianImage();
        GuardianMovie objGuardianMovieBLL = new GuardianMovie();
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int GuardianId = Request.QueryString["GuardianId"].ToInt32();

                HA.PMS.DataAssmblly.FD_FourGuardian four = objFourGuardianBLL.GetByID(GuardianId);
                ViewState["Name"] = four.GuardianName;
                if (System.IO.File.Exists(Server.MapPath(four.HeadImgPath)))
                {
                    imgHead.ImageUrl = four.HeadImgPath;
                }
                else { }
                ltlExplain.Text = four.Explain;
                ViewState["Type"] = GetTypeById(four.GuardianTypeId);
                ltlLeven.Text = GetLevenById(four.GuardianLevenId);
                ltlPrice.Text = four.SalePrice + string.Empty;
                ltlTelPhone.Text = four.CellPhone;
                ltlEmail.Text = four.Email;
                ltlDate.Text = GetDateStr(four.StarTime);

                ltlPersonalDetails.Text = four.PersonalDetails;
                ViewState["leven"] = GetLevenById(four.GuardianLevenId);
                DataBinder();

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

        protected void DataBinder()
        {

            int GuardianId = Request.QueryString["GuardianId"].ToInt32();
            rptImg.DataSource = objGuardianImageBLL.GetByAll().Where(C => C.FourGuardianId == GuardianId);
            rptImg.DataBind();



            rptMovie.DataSource = objGuardianMovieBLL.GetByAll().Where(C => C.FourGuardianId == GuardianId);
            rptMovie.DataBind();
        }
        /// <summary>
        /// 截取时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDateStr(object ObjDatetime)
        {

            if (ObjDatetime != null)
            {
                return ObjDatetime.ToString().ToDateTime().ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 返回等级名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetLevenById(object source)
        {
            int LevenId = (source + string.Empty).ToInt32();
            FD_GuradianLeven gl = objGuradianLevenBLL.GetByID(LevenId);
            return gl.LevenName;
        }
        /// <summary>
        /// 返回风格
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetTypeById(object source)
        {
            int typeId = (source + string.Empty).ToInt32();
            FD_GuardianType types = objGuardianTypeBLL.GetByID(typeId);
            return types.TypeName;

        }
    }
}