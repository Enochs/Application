using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.TheStage.ClassicCase
{
    public partial class PlannerShow : System.Web.UI.Page
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
            lblPlannerNames.Text = Model.PlannerName.ToString();
            lblTypeNames.Text = ObjTypeBLL.GetByID(Model.PlannerJob).TypeName.ToString();
            //lblPlannerSpecial.Text += Model.PlannerSpecial.ToString();
            lblPlannerJobDescription.Text += Model.PlannerJobDescription.ToString();
            //lblPlannerIntrodution.Text += Model.PlannerIntrodution.ToString();

            List<PMSParameters> pars = new List<PMSParameters>();
            var DataLists = ObjPlannerBLL.GetAllByParameter(pars, OrderByColumnName, 500, 1, out SourceCount);
            dl_PlanerList.DataSource = DataLists;
            dl_PlanerList.DataBind();

            var EvalDataList = ObjEvalBLL.GetByPlannerID(PlannerID).Where(C => C.IsShow == 1).ToList();        //作品列表
            if (EvalDataList.Count == 0)
            {
                tr_NoEval.Visible = true;
                lblNoEval.Visible = true;
                
            }
            dlEval.DataSource = EvalDataList;
            dlEval.DataBind();
        }
        #endregion


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

        public string GetImagePath(object Source)
        {
            string Path = Source.ToString();
            //string url = Server.MapPath("~/Files/TheCase/TheCaseImg/" + Path);
            //return url;
            return Path;
        }
    }
}