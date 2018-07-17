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
    public partial class FD_BanquetHallUpdate : SystemPage
    {
        Hotel objHotelBLL = new Hotel();
        HotelLabel objHotelLabelBLL = new HotelLabel();
        HotelLabelLog objHotelLabelLogBLL = new HotelLabelLog();
        BanquetHall objBanquetHallBLL = new BanquetHall();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int BanquetHallID = Request.QueryString["BanquetHallID"].ToInt32();
                FD_BanquetHall singerQuery = objBanquetHallBLL.GetByID(BanquetHallID);
                if (singerQuery != null)
                {
                    txtHallName.Text = singerQuery.HallName;
                    txtFloorName.Text = singerQuery.FloorName;
                    txtFloorHeight.Text = singerQuery.FloorHeight.HasValue ? string.Format("{0:f1}", singerQuery.FloorHeight.Value) : string.Empty;
                    txtEmptyHeight.Text = singerQuery.EmptyHigh.HasValue ? string.Format("{0:f1}", singerQuery.EmptyHigh.Value) : string.Empty;
                    txtMeal.Text = singerQuery.Meal.HasValue ? string.Format("{0:f1}", singerQuery.Meal.Value) : string.Empty;
                    txtArea.Text = singerQuery.Area.HasValue ? string.Format("{0:f1}", singerQuery.Area.Value) : string.Empty;
                    txtDeskCount.Text = singerQuery.DeskCount + string.Empty;
                    txtBanquetExplain.Text = singerQuery.BanquetExplain;
                    int HotelId = Request.QueryString["HotelId"].ToInt32();
                    ltlHotel.Text = objHotelBLL.GetByID(HotelId).HotelName;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int BanquetHallID = Request.QueryString["BanquetHallID"].ToInt32();
            FD_BanquetHall singerQuery = objBanquetHallBLL.GetByID(BanquetHallID);

            singerQuery.HallName = txtHallName.Text;
            singerQuery.FloorName = txtFloorName.Text;
            singerQuery.FloorHeight = txtFloorHeight.Text.ToDecimal();
            singerQuery.EmptyHigh = txtEmptyHeight.Text.ToDecimal();
            singerQuery.Meal = txtMeal.Text.ToDecimal();
            singerQuery.Area = txtArea.Text.ToDecimal();
            singerQuery.DeskCount = txtDeskCount.Text.ToInt32();
            singerQuery.BanquetExplain = txtBanquetExplain.Text;
            objBanquetHallBLL.Update(singerQuery);
            JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
        }
    }
}