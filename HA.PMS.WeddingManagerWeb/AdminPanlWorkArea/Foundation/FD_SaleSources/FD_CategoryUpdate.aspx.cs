/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:产品列别修改页面
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
    public partial class FD_CategoryUpdate : SystemPage
    {
        Category ObjCategoryBLL = new Category();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CategoryId = Request.QueryString["CategoryId"].ToInt32();
                FD_Category fd_Category = ObjCategoryBLL.GetByID(CategoryId);
                txtCategoryName.Text = fd_Category.CategoryName;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int CategoryId = Request.QueryString["CategoryId"].ToInt32();
            FD_Category fd_Category = ObjCategoryBLL.GetByID(CategoryId);
            fd_Category.CategoryName = txtCategoryName.Text;
            int result = ObjCategoryBLL.Update(fd_Category);
           
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
    }
}