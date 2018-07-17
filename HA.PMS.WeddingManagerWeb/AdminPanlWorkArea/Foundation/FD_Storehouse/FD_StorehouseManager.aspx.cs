/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.22
 Description:库管管理页面
 History:修改日志

 Author:杨洋
 date:2013.3.22
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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseManager : SystemPage
    {
        Category objCategoryBLL = new Category();
        Storehouse objStorehouseBLL = new Storehouse();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinderDropDown(ddlCategoryProject, 0);
                DataBinder();
                ddlCategoryType.Items.Add(new ListItem("请选择", "0"));
                ddlCategoryType.DataBind();
                ddlCategoryType.SelectedIndex = ddlCategoryType.Items.Count - 1;
            }
        }
        protected void DataBinderDropDown(DropDownList drop, int parentId)
        {
            drop.DataSource = objCategoryBLL.GetByAll().Where(C => C.ParentID == parentId).ToList();
            drop.DataTextField = "CategoryName";
            drop.DataValueField = "CategoryID";
            drop.DataBind();
            drop.Items.Add(new ListItem("请选择", "0"));
            drop.SelectedIndex = drop.Items.Count - 1;
        }

        protected void DataBinder()
        {
            //#region 查询参数
            //FD_StorehouseProduct fD_StorehouseProduct = new FD_StorehouseProduct();

            //fD_StorehouseProduct.CategoryParent = ddlCategoryProject.SelectedValue.ToInt32();
            //fD_StorehouseProduct.CategoryID = ddlCategoryType.SelectedValue.ToInt32();
            //fD_StorehouseProduct.SurplusCount = txtSurplusCount.Text.ToInt32();
            //fD_StorehouseProduct.ProductName = txtProjectName.Text.Trim();
            //fD_StorehouseProduct.SurplusCount=txtSurplusCount.Text.ToInt32();


            //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            //if (!string.IsNullOrEmpty(txtProjectName.Text))
            //{
            //    ObjParameterList.Add(new ObjectParameter("ProductName_LIKE", fD_StorehouseProduct.ProductName));
            //}
            ////项目
            //if (ddlCategoryProject.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("CategoryParent", fD_StorehouseProduct.CategoryParent));
            //}
            ////类别
            //if (ddlCategoryType.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("CategoryID", fD_StorehouseProduct.CategoryID));
            //}

            //if (txtSurplusCount.Text.ToInt32()!=0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("SurplusCount", fD_StorehouseProduct.SurplusCount));
            //}



            ////开始时间
            //DateTime startTime = new DateTime();
            ////如果没有选择结束时间就默认是当前时间

            //DateTime endTime = DateTime.Now;
            //if (!string.IsNullOrEmpty(txtStar.Text))
            //{
            //    startTime = txtStar.Text.ToDateTime();
            //}
            //if (!string.IsNullOrEmpty(txtEnd.Text))
            //{
            //    endTime = txtEnd.Text.ToDateTime();
            //}
            //ObjParameterList.Add(new ObjectParameter("AddTime_between", startTime + "," + endTime));
            //#endregion

            //#region 分页页码
            //int startIndex = StorePager.StartRecordIndex;
            //int resourceCount = 0;
            //var query = objStorehouseBLL.GetbyParameter(ObjParameterList.ToArray(), StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            //StorePager.RecordCount = resourceCount;

            //rptStoreHouse.DataSource = query;
            //rptStoreHouse.DataBind();
            
            //#endregion

        }

        protected void rptStoreHouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int storehouseID = e.CommandArgument.ToString().ToInt32();

                HA.PMS.DataAssmblly.FD_Storehouse fD_Storehouse = new DataAssmblly.FD_Storehouse()
                {
                    StorehouseID = storehouseID

                };

                objStorehouseBLL.Delete(fD_Storehouse);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }
 

        protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parentId = ddlCategoryProject.SelectedValue.ToInt32();
            DataBinderDropDown(ddlCategoryType, parentId);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void StorePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}