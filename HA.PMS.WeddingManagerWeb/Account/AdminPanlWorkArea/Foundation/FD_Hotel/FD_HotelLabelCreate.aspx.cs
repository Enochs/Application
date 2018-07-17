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
    public partial class FD_HotelLabelCreate : SystemPage
    {
        HotelLabel objHotelLabelBLL = new HotelLabel();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FD_HotelLabel hotelLabel = new FD_HotelLabel();
            hotelLabel.HotelLabelName = txtLabel.Text;
            objHotelLabelBLL.Insert(hotelLabel);
            JavaScriptTools.AlertAndClosefancybox("操作成功",this.Page);
        }
    }
}