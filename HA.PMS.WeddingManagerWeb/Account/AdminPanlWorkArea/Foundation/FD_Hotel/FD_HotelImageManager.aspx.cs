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
    public partial class FD_HotelImageManager : SystemPage
    {
        HotelImg objHotelImgBLL = new HotelImg();

        Hotel ObjHotelBLL = new Hotel();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        protected void DataBinder()
        {
            int HotelId = Request.QueryString["HotelId"].ToInt32();
     
            int startIndex = ImgListPager.StartRecordIndex;
            //如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。

            int resourceCount = 0;

            var query = objHotelImgBLL.GetByIndex(HotelId, ImgListPager.PageSize, ImgListPager.CurrentPageIndex, out resourceCount); ;

            ImgListPager.RecordCount = resourceCount;

            rptImg.DataSource = query;
            rptImg.DataBind();


        }

        protected void rptImg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int HotelImgId = e.CommandArgument.ToString().ToInt32();
            FD_HotelImg imgs = objHotelImgBLL.GetByID(HotelImgId);
            if (e.CommandName=="Delete")
            {
                 if (File.Exists(Server.MapPath( imgs.HotelImagePath)))
                 {
                     File.Delete(Server.MapPath(imgs.HotelImagePath));
                 }
               
                 objHotelImgBLL.Delete(imgs);
                 JavaScriptTools.AlertWindow("删除成功",this.Page);
                 DataBinder();
            }
            else if (e.CommandName == "SetOut")
            {
                HiddenField HotelID = (e.Item.FindControl("HideHotelID") as HiddenField);
                HA.PMS.DataAssmblly.FD_Hotel Hotel = ObjHotelBLL.GetByID(HotelID.Value.ToInt32());
                Hotel.HotelImagePath = imgs.HotelImagePath.ToString();
                int result = ObjHotelBLL.Update(Hotel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("封面设置成功", this.Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("设置失败,请稍候再试...", this.Page);
                }
            }
        }

        protected void ImgListPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}