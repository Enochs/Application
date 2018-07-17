using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class FL_PlannerManager : SystemPage
    {
        PlannerType objTypeBLl = new PlannerType();
        Planner ObjPlannerBLL = new Planner();
        int SourceCount = 0;
        string OrderColumnName = "Sort";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        public void BinderData()
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            var DataList = ObjPlannerBLL.GetAllByParameter(pars, OrderColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            rptPlanner.DataSource = DataList;
            rptPlanner.DataBind();
        }


        #region 获取性别  职位

        public string GetSex(object Source)
        {
            int Sex = Source.ToString().ToString().ToInt32();
            if (Sex == 0)
            {
                return "男";
            }
            else
            {
                return "女";
            }
        }

        public string GetJob(object Source)
        {
            int JobID = Source.ToString().ToInt32();
            var Model = objTypeBLl.GetByID(JobID);
            return Model.TypeName;
        }

        public string GetState(object Source)
        {
            bool IsDelete = Source.ToString().ToBool();
            if (IsDelete == false)
            {
                return "启用";
            }
            else
            {
                return "禁用";
            }
        }

        #endregion

        #region 点击分页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 绑定事件    删除/修改


        protected void rptPlanner_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lbtnDelete")
            {
                int PlannerID = e.CommandArgument.ToString().ToInt32();
                var Model = ObjPlannerBLL.GetByID(PlannerID);
                int result = ObjPlannerBLL.Delete(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请稍候再试...", Page);
                }
                BinderData();
            }
        }
        #endregion
    }
}