using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class SelectCase : System.Web.UI.Page
    {
        TheCase ObjCaseBLL = new TheCase();

        CaseFile objCaseFileBLL = new CaseFile();

        Evaulation ObjEvaulationBLL = new Evaulation();

        int PlannerID = 0;

        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {
            PlannerID = Request["PlannerID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定
        /// </summary> 
        public void BinderData()
        {
            var DataList = ObjCaseBLL.GetByAll();
            var DataLists = ObjEvaulationBLL.GetByPlannerID(PlannerID);
            rptCase.DataSource = DataList;
            rptCase.DataBind();

            for (int i = 0; i < rptCase.Items.Count; i++)
            {
                var DataItem = rptCase.Items[i];
                CheckBox chkCase = DataItem.FindControl("chkCaseName") as CheckBox;
                foreach (var item in DataLists)
                {
                    if (item.EvalTitle == chkCase.Text)
                    {
                        chkCase.Checked = true;
                    }
                }
            }
        }
        #endregion

        #region 点击确认选择

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            FL_Evaulation EvalModel = new FL_Evaulation();
            int result = 0;
            for (int i = 0; i < rptCase.Items.Count; i++)
            {
                var ModelItem = rptCase.Items[i];
                CheckBox check = ModelItem.FindControl("chkCaseName") as CheckBox;
                string chkCaseName = check.Text;

                if (check.Checked == true)
                {
                    EvalModel = ObjEvaulationBLL.GetByName(chkCaseName, PlannerID);
                    if (EvalModel == null)              //不存在 才新增
                    {
                        EvalModel = new FL_Evaulation();
                        int CaseID = (ModelItem.FindControl("HideCaseID") as HiddenField).Value.ToInt32();
                        var FirstData = objCaseFileBLL.GetByCasesID(CaseID).FirstOrDefault();
                        EvalModel.EvalTitle = chkCaseName.ToString();
                        EvalModel.EvalImageName = chkCaseName.ToString();
                        EvalModel.EvalImagePath = FirstData.CaseFilePath.Replace("~", string.Empty);
                        EvalModel.CaseID = CaseID;
                        EvalModel.EvalWorkID = Request["PlannerID"].ToInt32();
                        EvalModel.EvalDescription = "";
                        EvalModel.CreateDate = DateTime.Now;
                        EvalModel.CreateEmployee = User.Identity.Name.ToInt32();
                        EvalModel.Type = 1;
                        EvalModel.IsShow = 1;
                        result = ObjEvaulationBLL.Insert(EvalModel);
                    }
                }
                else if (check.Checked == false)
                {
                    EvalModel = ObjEvaulationBLL.GetByName(chkCaseName, PlannerID);
                    if (EvalModel != null)
                    {
                        result = ObjEvaulationBLL.Delete(EvalModel);
                    }
                }
            }
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("操作成功", Page);
            }
        }
        #endregion
    }
}