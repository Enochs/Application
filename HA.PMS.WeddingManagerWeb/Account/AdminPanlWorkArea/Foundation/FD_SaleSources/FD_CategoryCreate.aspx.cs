/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:产品类别创建页面
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
    public partial class FD_CategoryCreate : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        Productcs objProductcsBLL = new Productcs();
        Category objCategoryBLL = new Category();
        SysConfig ObjSysConfig = new SysConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断花艺分离是否开启
                if (!ObjSysConfig.GetPowerByKey("FlowerSplit"))
                {
                    rdolist.Items.RemoveAt(2);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            // 返回一个顶级的最后一个子节点的sortOrder， 
            string newSortOrderChild = objCategoryBLL.QuerySortOderTop();
            FD_Category fd_Category = new FD_Category()
            {

                IsDelete = false,
                CategoryName = txtCategoryName.Text,
                SortOrder = newSortOrderChild,
                ParentID = 0
            };


            //if (rdoProduct.Checked)
            //{
            fd_Category.Productproperty = rdolist.SelectedValue.ToInt32();
            //}
            //else
            //{
            //    fd_Category.Productproperty = 0;
            //}
            int result = objCategoryBLL.Insert(fd_Category);
            //根据返回判断添加的状态
            if (result > 0)
            {
                //自动添加到库房产品里面
                //FD_StorehouseSourceProduct fd_Storehouse = new FD_StorehouseSourceProduct();
                //fd_Storehouse.IsDelete = false;
                //fd_Storehouse.ProductCategory = result;
                ////注意以后如果进行 修改或者删除的操作，
                ////可以根据 当前在这里添加的库房ID字段 区别在库房界面直接添加的库房产品
                //fd_Storehouse.StorehouseSourceID = 0;
                //fd_Storehouse.SourceCount = 0;
                //fd_Storehouse.PutStoreDate = DateTime.Now;
                //fd_Storehouse.ProductState = "新录入";
                //fd_Storehouse.ProductProject = result;
                //fd_Storehouse.OperCount = 0;
                //fd_Storehouse.SaleOrice = 0;
                //fd_Storehouse.PurchasePrice = 0;
                //fd_Storehouse.Specifications = string.Empty;
                //fd_Storehouse.Unit = string.Empty;
                //fd_Storehouse.SourceProductName = txtCategoryName.Text;
                //objStorehouseSourceProductBLL.Insert(fd_Storehouse);
                //自动添加到供应商产品表里面

                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }
        }
    }
}