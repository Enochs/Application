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
    public partial class FD_GuardianImageOper : SystemPage
    {
        GuardianImage objGuardianImageBLL = new GuardianImage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                ViewState["load"] = "'" + EncodeBase64("/AdminPanlWorkArea/Foundation/FD_FourGuardian/SaveCeleToDB") + "'";
            }
        }

        protected void DataBinder()
        {
            int GuardianId = Request.QueryString["GuardianId"].ToInt32();
            ViewState["GuardianId"] = GuardianId;
            #region 分页页码
            int startIndex = ImagePager.StartRecordIndex;
            int resourceCount = 0;
            var query = objGuardianImageBLL.GetByIndex(GuardianId, ImagePager.PageSize, ImagePager.CurrentPageIndex, out resourceCount);
            ImagePager.RecordCount = resourceCount;

            rptImg.DataSource = query;
            rptImg.DataBind();


            #endregion
        }

        protected void ImagePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptImg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int GuardianImageID = (e.CommandArgument + string.Empty).ToInt32();

            if (e.CommandName=="Delete")
            {
                FD_GuardianImage imgs = objGuardianImageBLL.GetByID(GuardianImageID);
                if (File.Exists(Server.MapPath(imgs.ImagePath)))
                {
                    File.Delete(Server.MapPath(imgs.ImagePath));
                }
 
                objGuardianImageBLL.Delete(imgs);
                DataBinder();
                JavaScriptTools.AlertWindow("删除成功", this.Page);
            }
        }
    }
}