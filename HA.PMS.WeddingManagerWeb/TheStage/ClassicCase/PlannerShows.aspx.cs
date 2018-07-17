using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.TheStage.ClassicCase
{
    public partial class PlannerShows : System.Web.UI.Page
    {
        Planner ObjPlannerBLL = new Planner();

        PlannerType ObjTypeBLL = new PlannerType();

        Evaulation ObjEvalBLL = new Evaulation();

        int PlannerID = 0;
        string OrderByColumnName = "PlannerID";
        int SourceCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            PlannerID = Request["PlannerID"].ToString().ToInt32();
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        #region 数据加载

        public void DataBinder()
        {
            var Model = ObjPlannerBLL.GetByIDs(PlannerID).FirstOrDefault();
            img_planner.Src = Model.PlannerImagePath;
            lblPlannerName.Text = Model.PlannerName.ToString();
            lblTypeName.Text = ObjTypeBLL.GetByID(Model.PlannerJob).TypeName.ToString();
            ////lblPlannerSpecial.Text += Model.PlannerSpecial.ToString();
            lblPlannerJobDescription.Text += Model.PlannerJobDescription.ToString();
            ////lblPlannerIntrodution.Text += Model.PlannerIntrodution.ToString();

            List<PMSParameters> pars = new List<PMSParameters>();
            var DataLists = ObjPlannerBLL.GetAllByParameter(pars, OrderByColumnName, 500, 1, out SourceCount);
            var EvalDataList = ObjEvalBLL.GetByPlannerID(PlannerID).Where(C => C.IsShow == 1).ToList();        //作品列表
            if (EvalDataList.Count == 0)
            {
                tr_NoEval.Visible = true;
                lblNoEval.Visible = true;

            }
            HideCount.Value = EvalDataList.Count.ToString();
            lblEvalCount.Text = "(总共" + EvalDataList.Count.ToString() + "个案例)";
            dlEval.DataSource = EvalDataList;
            dlEval.DataBind();
            GetCount();
        }
        #endregion


        #region 获取类型名称
        /// <summary>
        /// 获取
        /// </summary>
        public string GetTypeName(object Source)
        {
            int TypeID = Source.ToString().ToInt32();
            var TypeModel = ObjTypeBLL.GetByID(TypeID);
            if (TypeModel != null)
            {
                return TypeModel.TypeName;
            }
            return "";
        }
        #endregion

        #region 标号
        /// <summary>
        /// 标号
        /// </summary>
        public void GetCount()
        {
            for (int i = 0; i < dlEval.Items.Count; i++)
            {
                var DataItem = dlEval.Items[i];
                Label lblTitle = DataItem.FindControl("lblEvalTitle") as Label;
                string title = lblTitle.Text;
                lblTitle.Text = string.Empty;
                int index = i + 1;
                lblTitle.Text = index + "." + title;
            }
        }
        #endregion
    }
}