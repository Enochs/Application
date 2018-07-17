/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:上传图片页面
 History:修改日志

 Author:杨洋
 date:2013.3.17
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
using System.IO;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse
{
    public partial class FD_ImageWarehouseCreate : SystemPage
    {
        
            ImageType objImageType = new ImageType();
            ImageWarehouse objImageWarehouseBLL = new ImageWarehouse();
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
                ddlImageType.DataSource = objImageType.GetByAll();
                ddlImageType.DataTextField = "TypeName";
                ddlImageType.DataValueField = "TypeId";
                ddlImageType.DataBind();
            }

        
    }
}