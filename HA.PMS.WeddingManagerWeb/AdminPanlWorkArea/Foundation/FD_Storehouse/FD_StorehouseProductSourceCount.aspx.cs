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
using System.Data.Objects;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseProductSourceCount : System.Web.UI.Page
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            int SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
            FD_StorehouseSourceProduct fd_Storehouse = objStorehouseSourceProductBLL.GetByID(SourceProductId);
            if (fd_Storehouse.SourceCount >= 0)
            {
                fd_Storehouse.SourceCount += txtCount.Text.ToInt32();
            }
            else
            {
                fd_Storehouse.SourceCount = txtCount.Text.ToInt32();
            }
            objStorehouseSourceProductBLL.Update(fd_Storehouse);
            JavaScriptTools.ResponseScript("alert('添加成功');parent.$.fancybox.close(1);$(parent.window.document.getElementById('ContentPlaceHolder1_btnbinderdata')).click();", Page);
        }
    }
}