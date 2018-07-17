using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;

//黄晓可 任务变更
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionChange : PopuPage
    {

        /// <summary>
        /// 详细任务操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();

        /// <summary>
        /// 任务主体
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();


        /// <summary>
        /// 任务变更
        /// </summary>
        MissionChange ObjMissionChangeBLL = new MissionChange();

        /// <summary>
        /// 员工操作
        /// </summary>
        Employee EmployeeBLL = new Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Action"] == "Check")
                {
                     
                 //   txtCheckChangeNode.Enabled = false;
                    lblChangeNode.Enabled = false;
                    lblStarDate.Enabled = false;
                    lblEmpLoyee.Enabled = false;
                    lblPlanDate.Enabled = false;
                    lblMissionName.Enabled = false;
                    lbltype.Enabled = false;
                    lblWorkNode.Enabled = false;
                    lblFinishStandard.Enabled = false;
                }
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var BindModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());
            lblChangeNode.Text = BindModel.ChangeNode;
            lblEmpLoyee.Text = EmployeeBLL.GetByID(BindModel.EmpLoyeeID).EmployeeName;
            lblFinishStandard.Text = BindModel.FinishStandard;
            //lblMissionName.Text = BindModel.MissionName;
            lblPlanDate.Text = BindModel.PlanDate.ToString();
            lblStarDate.Text = BindModel.StarDate.ToString();
            lblWorkNode.Text = BindModel.WorkNode;
            hideEmpLoyeeID.Value = BindModel.EmpLoyeeID + string.Empty;
            lblMissionName.Text = BindModel.MissionName;
            lbltype.Text = string.Empty;
        }


        /// <summary>
        /// 保存任务变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            Message objMessageBLL = new Message();
            var ObjMissionDetailedModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());
            //this.lblEmpLoyee.Text = ObjMissionDetailedModel.EmpLoyeeID.ToString();
            //ObjMissionDetailedModel.FinishStandard = this.lblFinishStandard.Text;
            //ObjMissionDetailedModel.MissionName = this.lblFinishStandard.Text;
            //ObjMissionDetailedModel.PlanDate = this.lblFinishStandard.Text.ToDateTime();
            //ObjMissionDetailedModel.StarDate = this.lblFinishStandard.Text.ToDateTime();
            if (Request["Action"] != "Checks" && Request["Action"] == null)
            {
                ObjMissionDetailedModel.ChecksChangeNode = txtCheckChangeNode.Text;

                ObjMissionDetailedModel.ChecksState = ddlState.SelectedValue.ToInt32();
                ObjMissionDetailedModel.ChangeNode = lblChangeNode.Text;
                ObjMissionDetailedModel.EmpLoyeeID = hideEmpLoyeeID.Value.ToInt32();
                ObjMissionDetailedModel.ChecksDate = DateTime.Now;

            }
            else
            {
                ObjMissionDetailedModel.ChecksState = ddlState.SelectedValue.ToInt32();
            }

            ObjMissionDetailedBLL.Update(ObjMissionDetailedModel);
            //发送消息

            FL_Message ObjMessageModel = new FL_Message();
            ObjMessageModel.EmployeeID = hideEmpLoyeeID.Value.ToInt32();
            ObjMessageModel.MissionID = ObjMissionDetailedModel.MissionID;
            ObjMessageModel.IsDelete = false;
            ObjMessageModel.IsLook = false;
            ObjMessageModel.Message = "部门负责人已经查看审核任务：结果为"+ddlState.SelectedItem.Text+" 备注说明为："+txtCheckChangeNode.Text;
            ObjMessageModel.MessAgeTitle = "任务变更审核";
            //ObjMessageModel.KeyWords = "";
            ObjMessageModel.CreateEmployeename = EmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
            objMessageBLL.Insert(ObjMessageModel);

            MissionChange ObjMissionChangeBLL = new MissionChange();

            ObjMissionChangeBLL.Delete(new DataAssmblly.FL_MissionChange() { ChangeID=Request["ChangeID"].ToInt32()});

            JavaScriptTools.AlertWindowAndLocation("审核完毕！", "/AdminPanlWorkArea/Flows/Mission/FL_MissionChangeChecksList.aspx", Page);
            ///变更日志
            //HA.PMS.DataAssmblly.FL_MissionChange ObjMissionChangeModel = new HA.PMS.DataAssmblly.FL_MissionChange();
            //ObjMissionChangeModel.Attachment = ObjMissionDetailedModel.Attachment;
            //ObjMissionChangeModel.ChangeNode = ObjMissionDetailedModel.ChangeNode;
            //ObjMissionChangeModel.ChannelID = ObjMissionDetailedModel.ChannelID;
            //ObjMissionChangeModel.ChecksChangeNode = ObjMissionDetailedModel.ChecksChangeNode;
            //ObjMissionChangeModel.ChecksDate = ObjMissionDetailedModel.ChecksDate;
            //ObjMissionChangeModel.ChecksEmployee = ObjMissionDetailedModel.ChecksEmployee;
            //ObjMissionChangeModel.ChecksNode = ObjMissionDetailedModel.ChangeNode;
            //ObjMissionChangeModel.ChecksState = ObjMissionDetailedModel.ChecksState;
            //ObjMissionChangeModel.Countdown = ObjMissionDetailedModel.Countdown;
            //ObjMissionChangeModel.CreateDate = DateTime.Now;
            //ObjMissionChangeModel.DetailedID = ObjMissionDetailedModel.DetailedID;
            //ObjMissionChangeModel.Emergency = ObjMissionDetailedModel.Emergency;
            //ObjMissionChangeModel.EmpLoyeeID = ObjMissionDetailedModel.EmpLoyeeID;
            //ObjMissionChangeModel.FinishDate = ObjMissionDetailedModel.FinishDate;
            //ObjMissionChangeModel.FinishFile = ObjMissionDetailedModel.FinishFile;
            //ObjMissionChangeModel.FinishNode = ObjMissionDetailedModel.FinishNode;
            //ObjMissionChangeModel.FinishStandard = ObjMissionDetailedModel.FinishStandard;
            //ObjMissionChangeModel.IsDelete = ObjMissionDetailedModel.IsDelete;
            //ObjMissionChangeModel.KeyWords = ObjMissionDetailedModel.KeyWords;
            //ObjMissionChangeModel.MissionName = ObjMissionDetailedModel.MissionName;
            //ObjMissionChangeModel.SortOrder = 1;
            //ObjMissionChangeModel.StarDate = ObjMissionDetailedModel.StarDate;
            //ObjMissionChangeModel.WorkNode = ObjMissionDetailedModel.WorkNode;
            //ObjMissionChangeModel.ChecksState = ddlState.SelectedValue.ToInt32();
            //ObjMissionChangeBLL.Insert(ObjMissionChangeModel);
        }
    }
}