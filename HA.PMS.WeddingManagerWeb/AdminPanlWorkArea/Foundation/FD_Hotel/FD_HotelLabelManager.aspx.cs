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
    public partial class FD_HotelLabelManager : SystemPage
    {
        HotelLabel objHotelLabelBLL = new HotelLabel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder()
        {

            int startIndex = LabelPager.StartRecordIndex;

            int resourceCount = 0;

            var query = objHotelLabelBLL.GetByIndex(LabelPager.PageSize, LabelPager.CurrentPageIndex, out resourceCount); ;

            LabelPager.RecordCount = resourceCount;
            rptLabel.DataSource = query;
            rptLabel.DataBind();
        }

        protected void LabelPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptLabel_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int id = (e.CommandArgument + string.Empty).ToInt32();
                FD_HotelLabel hotelLabel = new FD_HotelLabel() { HotelLabelID = id };
                objHotelLabelBLL.Delete(hotelLabel);
                JavaScriptTools.AlertWindow("删除成功", this.Page);
                DataBinder();
            }
        }
    }
}