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
using System.Data.Objects;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionList : SystemPage
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
        string SortName = "DetailedID";

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMisssionState.Items.Add(new ListItem("请选择", "-1"));
                ddlMisssionState.Items.Add(new ListItem("按时进行中", "0"));
                ddlMisssionState.Items.Add(new ListItem("按时完成", "1"));
                ddlMisssionState.Items.Add(new ListItem("超时进行中", "2"));
                ddlMisssionState.Items.Add(new ListItem("超时完成", "3"));
                DataBinder();
                IsManager();
            }
        }
        #endregion

        #region 获取任务状态 进行显示
        /// <summary>
        /// 显示
        /// </summary>
        public string GetMissionState(object Source)
        {
            int DetailsID = Source.ToString().ToInt32();
            var Model = objMissionDetailsedBLL.GetByID(DetailsID);
            if (Model.MissionState == 0)
            {
                return "进行中";
            }
            else if (Model.MissionState == 1)
            {
                return "已完成";
            }
            else if (Model.MissionState == 2)
            {
                return "超时进行";
            }
            else if (Model.MissionState == 3)
            {
                return "超时完成";
            }
            return "";
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定明细表
        /// </summary>
        protected void DataBinder()
        {
            rptMission.Visible = true;
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();

            //审核人 为自己  只能看见自己 及自己下派的任务
            ObjParameterList.Add("ChecksEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            //根据员工姓名查询
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                ObjParameterList.Add("EmpLoyeeID", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                ObjParameterList.Add("EmpLoyeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            }

            //根据任务类型查询
            if (ddlMissionType.SelectedItem.Value != "0")
            {
                ObjParameterList.Add("MissionType", ddlMissionType.SelectedValue, NSqlTypes.Equal);
            }


            //判断任务状态 
            if (ddlMisssionState.SelectedValue.ToInt32() >= 0)
            {
                ObjParameterList.Add("MissionState", ddlMisssionState.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //根据完成时间段查询
            if (ddlDateType.SelectedValue != "0")
            {
                if (DateRanger.IsNotBothEmpty)
                {
                    SortName = ddlDateType.SelectedValue.ToString();
                    ObjParameterList.Add(ddlDateType.SelectedValue.ToString(), DateRanger.StartoEnd, NSqlTypes.DateBetween);
                }
            }

            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            rptMission.DataSource = objMissionDetailsedBLL.GetAllByParameter(ObjParameterList, SortName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount);
            CtrPageIndex.RecordCount = sourceCount;
            rptMission.DataBind();

        }
        #endregion

        #region 绑定事件  删除
        /// <summary>
        /// 删除
        /// </summary>
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
                    Label lblPlanDate = e.Item.FindControl("lblPlanDate") as Label;

                    TextBox txtWorkNode = e.Item.FindControl("txtWorkNode") as TextBox;
                    TextBox txtFinishStandard = e.Item.FindControl("txtFinishStandard") as TextBox;
                    TextBox txtPlanDate = e.Item.FindControl("txtPlanDate") as TextBox;


                    lblWorkNode.Visible = false;
                    lblFinishStandard.Visible = false;
                    lblPlanDate.Visible = false;

                    txtWorkNode.Visible = true;
                    txtFinishStandard.Visible = true;
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

        #region 点击查询
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            DataBinder();
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

            return "";
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

        #region 任务绑定完成事件
        protected void rptMission_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            for (int i = 0; i < rptMission.Items.Count; i++)
            {
                var ObjItem = rptMission.Items[i];

                LinkButton lbtnChange = ObjItem.FindControl("btnChangeUpdate") as LinkButton;
                int DetailsId = lbtnChange.CommandArgument.ToInt32();
                var Model = objMissionDetailsedBLL.GetByID(DetailsId);
                if (Model.MissionState == 3 || Model.MissionState == 1)
                {
                    lbtnChange.Visible = false;
                }
                else
                {
                    if (User.Identity.Name.ToInt32() == Model.ChecksEmployee)
                    {
                        lbtnChange.Visible = true;
                    }
                    else
                    {
                        lbtnChange.Visible = false;
                    }
                }
            }
        }
        #endregion
    }
}