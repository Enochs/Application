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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class FD_BanquetHallCreate : SystemPage
    {
        Hotel objHotelBLL = new Hotel();

        BanquetHall objBanquetHallBLL = new BanquetHall();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int HotelId = Request.QueryString["HotelId"].ToInt32();
                ltlHotel.Text = objHotelBLL.GetByID(HotelId).HotelName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            FD_BanquetHall singerQuery = new FD_BanquetHall();

            singerQuery.HotelId = Request.QueryString["HotelId"].ToInt32();
            singerQuery.HallName = txtHallName.Text;
            singerQuery.FloorName = txtFloorName.Text;
            singerQuery.FloorHeight = txtFloorHeight.Text.ToDecimal();
            singerQuery.EmptyHigh = txtEmptyHeight.Text.ToDecimal();
            singerQuery.Meal = txtMeal.Text.ToDecimal();
            singerQuery.Area = txtArea.Text.ToDecimal();
            singerQuery.DeskCount = txtDeskCount.Text.ToInt32();
            singerQuery.BanquetExplain = txtBanquetExplain.Text;

            objBanquetHallBLL.Insert(singerQuery);
            JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
        }
    }
}