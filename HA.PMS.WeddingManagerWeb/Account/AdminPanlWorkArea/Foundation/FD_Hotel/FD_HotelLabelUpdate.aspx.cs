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
    public partial class FD_HotelLabelUpdate : SystemPage
    {
        HotelLabel objHotelLabelBLL = new HotelLabel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int HotelLabelID = Request.QueryString["HotelLabelID"].ToInt32();
                FD_HotelLabel singerQuery= objHotelLabelBLL.GetByID(HotelLabelID);
                if (singerQuery!=null)
                {
                    txtLabel.Text = singerQuery.HotelLabelName;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int HotelLabelID = Request.QueryString["HotelLabelID"].ToInt32();
            FD_HotelLabel singerQuery = objHotelLabelBLL.GetByID(HotelLabelID);
            singerQuery.HotelLabelName = txtLabel.Text;
            objHotelLabelBLL.Update(singerQuery);
            JavaScriptTools.AlertAndClosefancybox("修改成功",this.Page);
        }
    }
}