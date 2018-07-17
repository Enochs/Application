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
    public partial class FD_StorehouseScrapLogCreate : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();

        /// <summary>
        /// 报损记录表
        /// </summary>
        StorehouseScrapLog ObjStorehouseScrapLogBLL = new StorehouseScrapLog();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                txtScrapDate.Text = DateTime.Today.ToString("yyyy/M/d");
                int SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
                FD_StorehouseSourceProduct fd_Storehouse = objStorehouseSourceProductBLL.GetByID(SourceProductId);

                lblProductName.Text = fd_Storehouse.SourceProductName;
            }
        }


        /// <summary>
        /// 保存报损记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var InsertModel = new FD_StorehouseScrapLog();
            InsertModel.Reason = txtReason.Text;
            InsertModel.CreateDate = DateTime.Now;
            InsertModel.CreateEmployee = User.Identity.Name.ToInt32();
            InsertModel.ScrapDate = txtScrapDate.Text.ToDateTime();
            InsertModel.ScrapSum = txtScrapSum.Text.ToDecimal();
            InsertModel.SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
            InsertModel.ScrapEmpLoyee = txtScrapEmpLoyee.Text;

            ObjStorehouseScrapLogBLL.Insert(InsertModel);
            JavaScriptTools.ResponseScript("alert('报损成功');parent.$.fancybox.close(1);$(parent.window.document.getElementById('ContentPlaceHolder1_btnbinderdata')).click();", Page);
        }
    }
}