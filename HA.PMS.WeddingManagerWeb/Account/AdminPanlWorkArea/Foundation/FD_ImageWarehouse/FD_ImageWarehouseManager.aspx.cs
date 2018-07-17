/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:图片管理页面
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
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse
{
    public partial class FD_ImageWarehouseManager : SystemPage
    {
        ImageWarehouse objImageWarehouseBLL = new ImageWarehouse();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                ViewState["load"] = "'" + EncodeBase64("/AdminPanlWorkArea/Foundation/FD_ImageWarehouse/SaveCeleToDB") + "'";
                DataBinder();
            }
        }
      
              /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder() 
        {
           //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
           // #region 分页页码
           // int startIndex = ImagePager.StartRecordIndex;
           // int resourceCount = 0;
           // var query = objImageWarehouseBLL.GetbyParameter(ObjParameterList.ToArray(), ImagePager.PageSize, ImagePager.CurrentPageIndex, out resourceCount);
           // ImagePager.RecordCount = resourceCount;

           // rptImageWarehouse.DataSource = query;
           // rptImageWarehouse.DataBind();
            
          //  #endregion

 
        }

        protected void rptImageWarehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int ImageID = e.CommandArgument.ToString().ToInt32();
                
                //创建产品类
                HA.PMS.DataAssmblly.FD_ImageWarehouse fD_ImageWarehouseImageType = new HA.PMS.DataAssmblly.FD_ImageWarehouse()
                {
                    ImageID = ImageID
                };
                objImageWarehouseBLL.Delete(fD_ImageWarehouseImageType);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void ImagePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        
        
    }
}