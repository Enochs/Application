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
    public partial class Sys_WeddingUpdate : SystemPage
    {
        HA.PMS.BLLAssmblly.Sys.WeddingPlanning objWeddingBLL = new BLLAssmblly.Sys.WeddingPlanning();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CategoryId = Request.QueryString["CategoryId"].ToInt32();


                Sys_WeddingPlanning fd_Category = objWeddingBLL.GetByID(CategoryId);
                txtCategoryName.Text = fd_Category.PlanningName;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int CategoryId = Request.QueryString["CategoryId"].ToInt32();
            Sys_WeddingPlanning fd_Category = objWeddingBLL.GetByID(CategoryId);
            fd_Category.PlanningName = txtCategoryName.Text;
            int result = objWeddingBLL.Update(fd_Category);

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