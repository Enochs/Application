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
using HA.PMS.BLLAssmblly.FD;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse
{
    public partial class SaveCeleToDB : HandleSaveImg
    {
        ImageWarehouse objImageWarehouseBLL = new ImageWarehouse();
        public override void SavetoDataBase(string Address, string Filename)
        {
            //添加对应的图片信息到数据库
            HA.PMS.DataAssmblly. FD_ImageWarehouse fd_ImageWarehouse = new HA.PMS.DataAssmblly. FD_ImageWarehouse()
            {
                ImageTitle = Filename,
                ImageTypeId = 0,
                ImageUrl = Address,
                IsDelete = false
            };

            objImageWarehouseBLL.Insert(fd_ImageWarehouse);
        }
    }
}