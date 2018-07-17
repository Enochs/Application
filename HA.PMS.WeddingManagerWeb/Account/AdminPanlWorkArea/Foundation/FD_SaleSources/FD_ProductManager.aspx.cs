/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:产品管理页面
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
    public partial class FD_ProductManager : SystemPage
    {
        Productcs objProductBLL = new Productcs();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        /// <summary>
        /// 初始化绑定Repeater数据源
        /// </summary>
        protected void DataBinder()
        {
            int sourceCount = 0;
            rptProduct.DataSource = objProductBLL.GetFD_ProductCategorySupplierByIndex(10, 1, out sourceCount);
            rptProduct.DataBind();
        }
        /// <summary>
        /// 该事件提供删除事件操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int productId = e.CommandArgument.ToString().ToInt32();
                //创建产品类
                FD_Product fd_Product = new FD_Product()
                {
                    ProductID = productId
                };
                objProductBLL.Delete(fd_Product);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }
    }
}