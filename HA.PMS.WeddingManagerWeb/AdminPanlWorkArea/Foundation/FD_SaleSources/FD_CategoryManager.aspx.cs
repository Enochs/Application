/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:产品类别管理页面
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
    public partial class FD_CategoryManager : SystemPage
    {
        Category objCategoryBLL = new Category();

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder()
        {
            var query = objCategoryBLL.GetByAll();
            List<FD_Category> parentList = query.Where(C => C.ParentID == 0 && !C.CategoryName.Contains("待分配产品")).ToList();

            rptCategory.DataSource = parentList;
            rptCategory.DataBind();
        }
        #endregion

        #region 外层绑定完成
        /// <summary>
        /// 完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void rptCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FD_Category category = (e.Item.DataItem) as FD_Category;


            for (int j = 0; j < rptCategory.Items.Count; j++)
            {
                Repeater RptData = rptCategory.Items[j].FindControl("rptChildCategory") as Repeater;
                for (int i = 0; i < RptData.Items.Count; i++)
                {
                    LinkButton lbtnDelete = RptData.Items[i].FindControl("lbtnDelete") as LinkButton;
                    Label lblStateShow = RptData.Items[i].FindControl("lblStateShow") as Label;
                    int CategoryId = lbtnDelete.CommandArgument.ToInt32();
                    var Model = objCategoryBLL.GetByID(CategoryId);
                    if (Model.IsShow == true)
                    {
                        lbtnDelete.Text = "隐藏";
                        lblStateShow.Text = "当前状态:显示";
                    }
                    else if (Model.IsShow == false)
                    {
                        lbtnDelete.Text = "显示";
                        lblStateShow.Text = "当前状态:隐藏";
                    }
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptChildCategory = e.Item.FindControl("rptChildCategory") as Repeater;
                var childQuery = objCategoryBLL.GetByAll().Where(C => C.ParentID == category.CategoryID).ToList();
                if (childQuery.Count != 0)
                {
                    if (rptChildCategory != null)
                    {
                        rptChildCategory.DataSource = childQuery;
                        rptChildCategory.DataBind();
                    }
                }
                Literal ltlCreateChild = e.Item.FindControl("ltlCreateChild") as Literal;

                if (category.ParentID == 0)
                {
                    ltlCreateChild.Text = "<a href='#' class='btn btn-primary  btn-mini' onclick='ShowCreateChildWindows(" + category.CategoryID + ",this);'>创建下级</a>";
                }

            }
        }
        #endregion

        #region 内部Repeater 删除事件
        /// <summary>
        /// 删除
        /// </summary>  
        protected void rptChildCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int CategoryId = e.CommandArgument.ToString().ToInt32();
                //创建产品类
                LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
                Label lblStateShow = e.Item.FindControl("lblStateShow") as Label;
                var Model = objCategoryBLL.GetByID(CategoryId);
                if (lbtnDelete.Text == "隐藏")
                {
                    Model.IsShow = false;
                }
                else if (lbtnDelete.Text == "显示")
                {
                    Model.IsShow = true;
                }
                objCategoryBLL.Update(Model);
                //删除之后重新绑定数据源
                JavaScriptTools.AlertWindow("修改成功!", Page);
                DataBinder();
            }
        }
        #endregion

    }
}