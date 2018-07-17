/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.20
 Description:PPT管理页面
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
using System.Data.Objects;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_PPTWarehouse
{
    public partial class FD_PPTWareHouseManager : SystemPage
    {
        PPTWarehouse objPPTWarehouse = new PPTWarehouse();
        ImageType objImageTypeBLL = new ImageType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        protected void DataDropDownList() 
        {
            ddlImageType.DataSource = objImageTypeBLL.GetByAll();
            ddlImageType.DataTextField = "TypeName";
            ddlImageType.DataValueField = "TypeId";
            ddlImageType.DataBind();
            ddlImageType.Items.Add(new ListItem("请选择", "0"));
            ddlImageType.SelectedIndex = ddlImageType.Items.Count - 1;
        }
        /// <summary>
        /// 绑定主体数据源
        /// </summary>
        protected void DataBinder() 
        {
            FD_PPTWarehouseFDImageType fD_PPTWarehouseFDImageType = new DataAssmblly.FD_PPTWarehouseFDImageType();
            fD_PPTWarehouseFDImageType.ImageTypeId = ddlImageType.SelectedValue.ToInt32();


            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            if (ddlImageType.SelectedValue.ToInt32() != 0)
            {
                ObjParameterList.Add(new ObjectParameter("ImageTypeId", fD_PPTWarehouseFDImageType.ImageTypeId));
            }

             
            #region 分页页码
            int startIndex = PPTPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objPPTWarehouse.GetbyParameter(ObjParameterList.ToArray(), PPTPager.PageSize, PPTPager.CurrentPageIndex, out resourceCount);
            PPTPager.RecordCount = resourceCount;

            rptPPTManager.DataSource = query;
            rptPPTManager.DataBind();
         
            #endregion

        
        }

        protected void rptPPTManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int PPTId = e.CommandArgument.ToString().ToInt32();
                //创建PPT类
                HA.PMS.DataAssmblly.FD_PPTWarehouse pptWarehouse = objPPTWarehouse.GetByID(PPTId);

                objPPTWarehouse.Delete(pptWarehouse);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void PPTPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}