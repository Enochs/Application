using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissioninDoing : SystemPage
    {
        /// <summary>
        /// 明细操作
        /// </summary>
        MissionDetailed objMissionDetailsedBLL = new MissionDetailed();


        /// <summary>
        /// 分组操作
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        Employee ObjEmployeeBLL = new Employee();

        int SourceCount = 0;
        string SortName = "CreateDate";

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                IsManager();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定明细表
        /// </summary>
        protected void DataBinder()
        {

            rptMission.Visible = true;
            //repMissionResualt.Visible = false;
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();

            ObjParameterList.Add("EmployeeID", User.Identity.Name.ToInt32(),NSqlTypes.Equal);

            ObjParameterList.Add("IsDelete", false,NSqlTypes.Bit);
            ObjParameterList.Add("IsOver", false, NSqlTypes.Bit);
            ObjParameterList.Add("IsLook", true, NSqlTypes.Bit);
            ObjParameterList.Add("MissionState", "0,2",NSqlTypes.IN);
            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            rptMission.DataSource = objMissionDetailsedBLL.GetAllByParameter(ObjParameterList,SortName,CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount);
            CtrPageIndex.RecordCount = sourceCount;
            rptMission.DataBind();

        }
        #endregion

        #region 数据绑定  删除方法
        /// <summary>
        /// 删除 / 完成
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptMission_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int DetailedId = e.CommandArgument.ToString().ToInt32();
            HiddenField hideEmpLoyeeID = e.Item.FindControl("hideEmpLoyeeID") as HiddenField;
            LinkButton lbtnChange = e.Item.FindControl("btnChangeUpdate") as LinkButton;

            if (hideEmpLoyeeID.Value.ToInt32() == 0 || lbtnChange.Text == "保存")        //没有选择责任人改派 就是修改信息
            {
                if (e.CommandName == "Change")
                {

                    Label lblWorkNode = e.Item.FindControl("lblWorkNode") as Label;
                    Label lblFinishStandard = e.Item.FindControl("lblFinishStandard") as Label;
                    Label lblStartDate = e.Item.FindControl("lblStartDate") as Label;
                    Label lblPlanDate = e.Item.FindControl("lblPlanDate") as Label;

                    TextBox txtWorkNode = e.Item.FindControl("txtWorkNode") as TextBox;
                    TextBox txtFinishStandard = e.Item.FindControl("txtFinishStandard") as TextBox;
                    TextBox txtStartDate = e.Item.FindControl("txtStartDatee") as TextBox;
                    TextBox txtPlanDate = e.Item.FindControl("txtPlanDate") as TextBox;


                    lblWorkNode.Visible = false;
                    lblFinishStandard.Visible = false;
                    lblStartDate.Visible = false;
                    lblPlanDate.Visible = false;

                    txtWorkNode.Visible = true;
                    txtFinishStandard.Visible = true;
                    txtStartDate.Visible = true;
                    txtPlanDate.Visible = true;

                    if (lbtnChange.Text == "变更")
                    {
                        lbtnChange.Text = "保存";
                    }
                    else if (lbtnChange.Text == "保存")
                    {
                        lbtnChange.Text = "变更";
                        var MissionDetailsModel = objMissionDetailsedBLL.GetByID(DetailedId);
                        MissionDetailsModel.WorkNode = txtWorkNode.Text.Trim().ToString();
                        MissionDetailsModel.FinishStandard = txtFinishStandard.Text.Trim().ToString();
                        MissionDetailsModel.StarDate = txtStartDate.Text.ToDateTime();
                        MissionDetailsModel.PlanDate = txtPlanDate.Text.ToDateTime();
                        if (hideEmpLoyeeID.Value.ToString().ToInt32() != 0)
                        {
                            MissionDetailsModel.EmpLoyeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
                        }
                        int result = objMissionDetailsedBLL.Update(MissionDetailsModel);

                        var MissionModel = ObjMissionManagerBLL.GetByID(MissionDetailsModel.MissionID);
                        if (hideEmpLoyeeID.Value.ToString().ToInt32() != 0)
                        {
                            MissionModel.EmployeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
                            MissionModel.DepartmentID = ObjEmployeeBLL.GetDepartmentID(MissionModel.EmployeeID.ToString().ToInt32());
                        }
                        else
                        {
                            MissionModel.EmployeeID = MissionDetailsModel.EmpLoyeeID;
                        }
                        result += ObjMissionManagerBLL.Update(MissionModel);
                        if (result == 2)
                        {
                            JavaScriptTools.AlertWindow("变更成功", Page);
                        }

                        DataBinder();
                    }
                }
            }
            else if (hideEmpLoyeeID.Value.ToInt32() != 0)            //选择了责任人就是直接改派
            {
                Employee ObjEmployeeBLL = new Employee();
                if (hideEmpLoyeeID.Value.ToInt32() != 0)
                {
                    var MissionDetailsModel = objMissionDetailsedBLL.GetByID(DetailedId);
                    MissionDetailsModel.EmpLoyeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
                    int result = objMissionDetailsedBLL.Update(MissionDetailsModel);

                    var MissionModel = ObjMissionManagerBLL.GetByID(MissionDetailsModel.MissionID);
                    MissionModel.EmployeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
                    MissionModel.DepartmentID = ObjEmployeeBLL.GetDepartmentID(MissionModel.EmployeeID.ToString().ToInt32());
                    result += ObjMissionManagerBLL.Update(MissionModel);
                    string EmployeeName = GetEmployeeName(hideEmpLoyeeID.Value.ToString().ToInt32());
                    if (result == 2)
                    {
                        JavaScriptTools.AlertWindow("改派责任人成功,请联系" + EmployeeName + " 查看是否改派成功", Page);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("变更失败,请稍候再试...", Page);
                    }
                }
                DataBinder();
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 上一页/下一页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 是否是部门主管
        /// <summary>
        /// 主管才拥有任务变更这个功能
        /// </summary>
        public void IsManager()
        {
            int EmployeeID = User.Identity.Name.ToInt32();
            bool IsTrue = ObjEmployeeBLL.IsManager(EmployeeID);
            if (IsTrue == false)             //不是主管 就隐藏任务变更
            {
                for (int i = 0; i < rptMission.Items.Count; i++)
                {
                    var ObjItem = rptMission.Items[i];
                    //LinkButton lbtnSave = ObjItem.FindControl("btnSaveEmployeeID") as LinkButton;
                    LinkButton lbtnChange = ObjItem.FindControl("btnChangeUpdate") as LinkButton;
                    //lbtnSave.Visible = false;
                    lbtnChange.Visible = false;
                }
            }
        }
        #endregion

        #region 当任务超期之后  变成红色
        /// <summary>
        /// 变色
        /// </summary>
        public string ChangeColor(object Source)
        {
            int DetailsID = Source.ToString().ToInt32();
            var Model = objMissionDetailsedBLL.GetByID(DetailsID);
            if (Model.MissionState == 0 || Model.MissionState == 2)
            {
                if (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime() > Model.PlanDate)
                {
                    if (Model.MissionState == 0)
                    {
                        Model.MissionState = 2;
                        objMissionDetailsedBLL.Update(Model);
                    }
                    if (Model.MissionState == 2)
                    {
                        return "style='color:red;'";
                    }
                }
                else if (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime() < Model.PlanDate)
                {
                    Model.MissionState = 0;
                    objMissionDetailsedBLL.Update(Model);
                }
            }
            return "";
        }
        #endregion

        #region 计划隐藏按钮
        /// <summary>
        /// 隐藏
        /// </summary>
        public string IsVisible(object Source)
        {
            int DetailsID = Source.ToString().ToInt32();
            var Model = objMissionDetailsedBLL.GetByID(DetailsID);
            if (GetMissiontypeName(Model.MissionType).Contains("计划"))   //员工的任务是计划类型的
            {
                return "style='display:none;'";
            }
            return "";
        }
        #endregion

    }
}