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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class FD_BanquetHallManager : SystemPage
    {
        BanquetHall objBanquetHallBLL = new BanquetHall();
        BanquetHallImg objBanquetHallImgBLL = new BanquetHallImg();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["load"] = "'" + EncodeBase64("/AdminPanlWorkArea/Foundation/FD_Hotel/SaveHotelImgToDB") + "'";
                ViewState["HotelId"] = Request.QueryString["HotelId"].ToInt32();
                DataBinder();
            }
        }
        /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder()
        {
            int HotelId = Request.QueryString["HotelId"].ToInt32();
            int startIndex = BanquetHallPager.StartRecordIndex;
        
            int resourceCount = 0;

            var query = objBanquetHallBLL.GetByIndex(HotelId, BanquetHallPager.PageSize, BanquetHallPager.CurrentPageIndex, out resourceCount); ;

            BanquetHallPager.RecordCount = resourceCount;
            rptBanquetHall.DataSource = query;
            rptBanquetHall.DataBind();
        }


        protected void BanquetHallPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptBanquetHall_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int BanquetHallID = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName=="Delete")
            {
               

                var currentBanquetHallImg = objBanquetHallImgBLL.GetByBanquetHallIDAll(BanquetHallID).ToList();
                for (int j = 0; j < currentBanquetHallImg.Count; j++)
                {
                    if (File.Exists(Server.MapPath(currentBanquetHallImg[j].BanquetHallPath)))
                    {
                        File.Delete(Server.MapPath(currentBanquetHallImg[j].BanquetHallPath));
                    }
                    //删除单个宴会厅的图片数据
                    objBanquetHallImgBLL.Delete(currentBanquetHallImg[j]);
                }
                objBanquetHallBLL.Delete(new FD_BanquetHall() { BanquetHallID = BanquetHallID });
                JavaScriptTools.AlertWindow("删除成功",this.Page);
                DataBinder();
            }
        }
    }
}