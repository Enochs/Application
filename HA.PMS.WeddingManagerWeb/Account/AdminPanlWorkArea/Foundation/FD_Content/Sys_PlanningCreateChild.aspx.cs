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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class Sys_PlanningCreateChild : SystemPage
    {
        HA.PMS.BLLAssmblly.Sys.WeddingPlanning objWeddingBLL = new BLLAssmblly.Sys.WeddingPlanning();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int CategoryId = Request.QueryString["CategoryId"].ToInt32();
            //获取最新的顶级节点sortOrder值 
            string newSortOrder = objWeddingBLL.QuerySortOrderChild(CategoryId);


            Sys_WeddingPlanning wedding = new Sys_WeddingPlanning()
            {
                IsDelete = false,

                PlanningName = txtCategoryName.Text,
                SortOrder = newSortOrder,
                ParentID = CategoryId


            };




            int result = objWeddingBLL.Insert(wedding);
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
