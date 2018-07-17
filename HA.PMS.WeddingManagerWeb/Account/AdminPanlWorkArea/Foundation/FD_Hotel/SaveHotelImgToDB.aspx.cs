using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class SaveHotelImgToDB : HandleSaveImg
    {
        HotelImg objHotelImgBLL=new HotelImg();
        BanquetHallImg objBanquetHallImgBLL = new BanquetHallImg();
        public override void SavetoDataBase(string Address, string Filename)
        {
            string urlPar = Request.QueryString.GetKey(0);
            int urlParId =Request.QueryString[urlPar].ToInt32();
            if (urlPar=="HotelId")
            {
                FD_HotelImg imgs = new FD_HotelImg();
                imgs.HotelImagePath = Address;
                imgs.HotelId = urlParId;
                imgs.HotelImageName = Filename;
               
                objHotelImgBLL.Insert(imgs);
            }
            else
            {
                FD_BanquetHallImg ban = new FD_BanquetHallImg();
                ban.BanquetHallID = urlParId;
                ban.BanquetHallImgName = Filename;
                ban.BanquetHallPath = Address;
                objBanquetHallImgBLL.Insert(ban);
            }
           
        }
    }
}