/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.20
 Description:PPT添加页面
 History:修改日志

 Author:杨洋
 date:2013.3.20
 version:好爱1.0
 description:修改描述
 
 
 
 */
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_PPTWarehouse
{
    public partial class FD_PPTWareHouseCreate : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        protected void BinderData()
        {
            ddlImageType.DataSource = new ImageType().GetByAll();
            ddlImageType.DataTextField = "TypeName";
            ddlImageType.DataValueField = "TypeId";
            ddlImageType.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string[] filterFileExt = { ".ppt", ".pptx", ".dps" };
            string fileExt = System.IO.Path.GetExtension(fuLoadFile.FileName).ToLower();

            if (fuLoadFile.HasFile)
            {
                JavaScriptTools.AlertWindow("请上先传 PPT", this.Page);
                return;
            }
            if (!filterFileExt.Contains(fileExt))
            {
                JavaScriptTools.AlertWindow("你上传PPT格式有误，PPT 格式通常为 .ppt,.pptx,.dps", this.Page);
                return;
            }

            string savaPath = Server.MapPath(string.Concat("~/Files/PPT/", Guid.NewGuid(), System.IO.Path.GetFileNameWithoutExtension(fuLoadFile.FileName), fileExt));
            try
            {
                fuLoadFile.SaveAs(savaPath);
            }
            catch { JavaScriptTools.AlertWindow("上传PPT失败，请重新尝试", Page); return; }
           
            HA.PMS.DataAssmblly.FD_PPTWarehouse pptWarehouse = new HA.PMS.DataAssmblly.FD_PPTWarehouse()
            {
                PPTTitle = txtPPTTitle.Text,
                LoadTime = DateTime.Now,
                PPTUrl = savaPath,
                LoadPerson = User.Identity.Name.ToInt32(),
                LoadLabel = txtLoadLabel.Text,
                ImageTypeId = ddlImageType.SelectedValue.ToInt32(),
                IsDelete = false
            };
            int result = new PPTWarehouse().Insert(pptWarehouse);
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);
            }

        }
    }
}