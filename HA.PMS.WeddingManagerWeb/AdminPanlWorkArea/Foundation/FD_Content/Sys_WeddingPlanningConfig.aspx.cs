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
    public partial class Sys_WeddingPlanningConfig : SystemPage
    {



        HA.PMS.BLLAssmblly.Sys.WeddingPlanning objWeddingBLL = new BLLAssmblly.Sys.WeddingPlanning();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
               var query=  Request.Form;
            }
        }
        /// <summary>
        /// 初始化绑定数据源
        /// </summary>
        protected void DataBinder()
        {
            var query = objWeddingBLL.GetByAll();
            List<Sys_WeddingPlanning> parentList = query.Where(C => C.ParentID == 0).ToList();

            rptCategory.DataSource = parentList;
            rptCategory.DataBind();
        }

        protected void rptCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int CategoryId = e.CommandArgument.ToString().ToInt32();
                //创建产品类
                Sys_WeddingPlanning fd_Planning = objWeddingBLL.GetByID(CategoryId);
               
                objWeddingBLL.Delete(fd_Planning);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void rptCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Sys_WeddingPlanning category = (e.Item.DataItem) as Sys_WeddingPlanning;



            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptChildCategory = e.Item.FindControl("rptChildCategory") as Repeater;
              
                
                
                var childQuery = objWeddingBLL.GetByAll().Where(C => C.ParentID == category.PlanningID).ToList();
                if (childQuery.Count != 0)
                {
                    rptChildCategory.DataSource = childQuery;
                    rptChildCategory.DataBind();
                    rptChildCategory.ItemCommand += rptChildCategory_ItemCommand;
                }
                Literal ltlCreateChild = e.Item.FindControl("ltlCreateChild") as Literal;

                if (category.ParentID == 0)
                {
                    ltlCreateChild.Text = "<a href='#' class='btn btn-primary  btn-mini' onclick='ShowCreateChildWindows(" + category.PlanningID + ",this);'>创建统筹项目</a>";
                }

            }
        }

        protected void rptChildCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int CategoryId = e.CommandArgument.ToString().ToInt32();
                //创建产品类
                Sys_WeddingPlanning fd_Planning = objWeddingBLL.GetByID(CategoryId);

                objWeddingBLL.Delete(fd_Planning);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }
    }
}
