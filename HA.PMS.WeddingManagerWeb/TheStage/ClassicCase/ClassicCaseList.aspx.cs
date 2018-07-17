using System;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using System.Collections.Generic;
using HA.PMS.BLLAssmblly.Flow;
using System.Web.UI.WebControls;


namespace HA.PMS.WeddingManagerWeb.TheStage.ClassicCase
{
    public partial class ClassicCaseList : System.Web.UI.Page
    {
        TheCase ObjTheCaseBLL = new TheCase();
        CaseFile ObjCaseFileBLL = new CaseFile();

        Planner ObjPlannerBLL = new Planner();

        PlannerType ObjTypeBLL = new PlannerType();
        string OrderByColumnName = "CaseID";
        int SourceCount = 0;
        int PageSize = 5000;
        int PageIndex = 1;

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void DataBinder()
        {
            #region 查询条件
            List<PMSParameters> Pars = new List<PMSParameters>();
            //根据名称查询
            Pars.Add(txtTitle.Text != string.Empty, "CaseName", txtTitle.Text.Trim().ToString(), NSqlTypes.LIKE);
            //根据酒店查询
            Pars.Add(txtHotel.Text != string.Empty, "CaseHotel", txtHotel.Text.Trim().ToString(), NSqlTypes.LIKE);
            //根据风格查询
            Pars.Add(txtStyle.Text != string.Empty, "CaseStyle", txtStyle.Text.Trim().ToString(), NSqlTypes.LIKE);
            #endregion

            var query = ObjTheCaseBLL.GetCaseByParameter(Pars, OrderByColumnName, PageSize, PageIndex, out SourceCount);
            //var query = ObjTheCaseBLL.GetByAll();
            //将案例排序号为空的案例的排序号设置为最大值，目的让其排在最后面
            foreach (FD_TheCase item in query)
            {
                if (item.CaseOrder == null)
                {
                    item.CaseOrder = int.MaxValue;
                }
            }
            var SortedQuery = query.OrderBy(C => C.CaseOrder);
            //封面
            rptCelePackageTop.DataSource = SortedQuery.Take(2);
            rptCelePackageTop.DataBind();



            rptListTree.DataSource = SortedQuery;
            rptListTree.DataBind();

            if (Request["Type"] == null)
            {
                var DataList = ObjPlannerBLL.GetByAll().OrderByDescending(C => C.Sort);
                DataListPlanner.DataSource = DataList;
                DataListPlanner.DataBind();
            }
            else
            {
                int Type = Request["Type"].ToInt32();
                var DataList = ObjPlannerBLL.GetByType(Type).OrderByDescending(C => C.Sort);
                DataListPlanner.DataSource = DataList;
                DataListPlanner.DataBind();
            }

        }
        #endregion

        #region 婚礼作品 点击查询
        /// <summary>
        /// 查询
        /// </summary> 
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 获取类型名称
        /// <summary>
        /// 类型名称
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

        public string GetImgPath(object Source)
        {
            string path = "";
            if (!string.IsNullOrEmpty(Source.ToString()))
            {
                int CaseID = Source.ToString().ToInt32();
                FD_TheCase objTheCase = ObjTheCaseBLL.GetByID(CaseID);
                var query = ObjCaseFileBLL.GetByAll().Where(C => C.FileType == 2 && C.CaseId == CaseID);
                var firstData = query.FirstOrDefault();
                if (objTheCase == null)
                {
                    ViewState["imgTop"] = firstData.CaseFilePath.Replace("~", string.Empty);
                }
                else
                {
                    string paths = objTheCase.CasePath.ToString();
                    ViewState["imgTop"] = objTheCase.CasePath.Replace("~", "/.");
                }
                path = ViewState["imgTop"].ToString();
            }
            return path;
        }

    }
}