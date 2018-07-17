/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:产品创建页面
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_ProductCreate : SystemPage
    {
        Productcs objProductBLL = new Productcs();
        Category objCategoryBLL = new Category();
        Supplier objSupplierBLL = new Supplier();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化添加界面所需的信息
                DataBinder();
            }
        }

        protected void DataBinder()
        {
            ddlCategory.DataSource = objCategoryBLL.GetByAll();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlSupplier.DataSource = objSupplierBLL.GetByAll();
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "SupplierID";
            ddlSupplier.DataBind();
        }
        /// <summary>
        /// 添加产品信息保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FD_Product fd_Product = new FD_Product()
            {
                ProductName = txtProductName.Text,
                SupplierID = ddlSupplier.SelectedValue.ToInt32(),
                CategoryID = ddlCategory.SelectedValue.ToInt32(),
                ProductPrice = txtPrice.Text.ToInt32(),
                SalePrice = txtSalePrice.Text.ToInt32(),
                IsDelete = false,
                Specifications = txtSpecifications.Text,
                Unit = txtUnit.Text,
                Explain = txtExplain.Text,
                Data = txtData.Text,
                Count = txtCount.Text.ToInt32()
            };

            int result = objProductBLL.Insert(fd_Product);
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