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
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class SaveCeleToDB : HandleSaveImg
    {
        GuardianImage objGuardianImageBLL = new GuardianImage();
        public override void SavetoDataBase(string Address, string Filename)
        {

            int urlParId = Request.QueryString["GuardianId"].ToInt32();

            //图片
            FD_GuardianImage imgSinger = new FD_GuardianImage();
            imgSinger.CreateEmployee = User.Identity.Name.ToInt32();
            imgSinger.Createtime = DateTime.Now;
            imgSinger.IsDelete = false;
            imgSinger.FourGuardianId = urlParId;
            imgSinger.ImageName = Filename;
            imgSinger.ImagePath = Address;
            imgSinger.IsDelete = false;
            objGuardianImageBLL.Insert(imgSinger);


        }
    }
}