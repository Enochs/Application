using HA.PMS.Pages;
using System;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionChangeApply : PopuPage
    {

        /// <summary>
        /// 任务变更操作
        /// </summary>
        MissionChange ObjMissionChangeBLL = new MissionChange();

        /// <summary>
        /// 任务明细操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();


        /// <summary>
        /// 任务主体操作
        /// </summary>
        MissionManager ObjMissionManager = new MissionManager();

        /// <summary>
        /// 员工操作
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定任务
        /// </summary>
        private void BinderData()
        {

            var ObjMissionDetailedModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());
            this.lblEmpLoyee.Text = ObjEmployeeBLL.GetByID(ObjMissionDetailedModel.CreateEmployeeID).EmployeeName;
            this.lblFinishStandard.Text = ObjMissionDetailedModel.FinishStandard;
            this.lblMissionName.Text = ObjMissionDetailedModel.MissionName;
            this.lblPlanDate.Text = ObjMissionDetailedModel.PlanDate.ToString();
            this.lblStarDate.Text = ObjMissionDetailedModel.StarDate.ToString();
            this.lbltype.Text = "";
            this.lblWorkNode.Text = ObjMissionDetailedModel.WorkNode;
            if (ObjMissionDetailedModel.ChecksState == 2)
            {
                btnSaveChange.Visible = false;
                txtChangeNode.Enabled = false;
            }
            if (!string.IsNullOrEmpty(ObjMissionDetailedModel.KeyWords))
            {
                string[] keyvalue = ObjMissionDetailedModel.KeyWords.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < keyvalue.Length; i++)
                {
                    if (keyvalue[i].StartsWith("CustomerID=", StringComparison.OrdinalIgnoreCase))
                    {
                        CarrytaskCustomerTitle1.CustomerID = keyvalue[i].Replace("CustomerID=", null).ToInt32();
                        break;
                    }
                }
            }
            else
            {
                CarrytaskCustomerTitle1.Visible = false;
            }
        }


        /// <summary>
        /// 申请变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            var ObjMissionDetailedModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());
            //this.lblEmpLoyee.Text = ObjMissionDetailedModel.EmpLoyeeID.ToString();
            //ObjMissionDetailedModel.FinishStandard= this.lblFinishStandard.Text;
            //ObjMissionDetailedModel.MissionName= this.lblFinishStandard.Text;
            //ObjMissionDetailedModel.PlanDate= this.lblFinishStandard.Text.ToDateTime();
            //ObjMissionDetailedModel.StarDate = this.lblFinishStandard.Text.ToDateTime();
            var DepartmentID = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).DepartmentID;
            Department ObjDepartmentBLL = new Department();
            ObjMissionDetailedModel.ChecksEmployee = ObjDepartmentBLL.GetByID(DepartmentID).DepartmentManager;
            ObjMissionDetailedModel.ChangeNode = txtChangeNode.Text;
            ObjMissionDetailedModel.ChecksState = 2;
            ObjMissionDetailedBLL.Update(ObjMissionDetailedModel);


            ///进入变更
            HA.PMS.DataAssmblly.FL_MissionChange ObjMissionChangeModel = new HA.PMS.DataAssmblly.FL_MissionChange();
            //ObjMissionChangeModel.Attachment = ObjMissionDetailedModel.Attachment;
            //ObjMissionChangeModel.ChangeNode = ObjMissionDetailedModel.ChangeNode;
            //ObjMissionChangeModel.ChannelID = ObjMissionDetailedModel.ChannelID;
            //ObjMissionChangeModel.ChecksChangeNode = ObjMissionDetailedModel.ChecksChangeNode;
            //ObjMissionChangeModel.ChecksDate = ObjMissionDetailedModel.ChecksDate;
            //ObjMissionChangeModel.ChecksEmployee = ObjMissionDetailedModel.ChecksEmployee;
            //ObjMissionChangeModel.ChecksNode = ObjMissionDetailedModel.ChangeNode;
            //ObjMissionChangeModel.ChecksState = ObjMissionDetailedModel.ChecksState;
            //ObjMissionChangeModel.Countdown = ObjMissionDetailedModel.Countdown;
            ObjMissionChangeModel.CreateDate = DateTime.Now;
            ObjMissionChangeModel.DetailedID = ObjMissionDetailedModel.DetailedID;
            ObjMissionChangeModel.MissionID = ObjMissionDetailedModel.MissionID;
            ObjMissionChangeModel.IsChangeMission = true;
            //ObjMissionChangeModel.Emergency = ObjMissionDetailedModel.Emergency;
            //ObjMissionChangeModel.EmpLoyeeID = ObjMissionDetailedModel.EmpLoyeeID;
            //ObjMissionChangeModel.FinishDate = ObjMissionDetailedModel.FinishDate;
            //ObjMissionChangeModel.FinishFile = ObjMissionDetailedModel.FinishFile;
            //ObjMissionChangeModel.FinishNode = ObjMissionDetailedModel.FinishNode;
            //ObjMissionChangeModel.FinishStandard = ObjMissionDetailedModel.FinishStandard;
            //ObjMissionChangeModel.IsDelete = ObjMissionDetailedModel.IsDelete;
            ObjMissionChangeModel.KeyWords = "FL_MissionChange.aspx?DetailedID=" + ObjMissionDetailedModel.DetailedID;
            ObjMissionChangeModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
            ObjMissionChangeModel.ChecksEmployee = ObjEmployeeBLL.GetMineCheckEmployeeID(ObjMissionChangeModel.CreateEmpLoyee);
            ObjMissionChangeModel.MissionName = ObjMissionDetailedModel.MissionName;
            ObjMissionChangeModel.MissionType = 1;//dangrenwu单个任务
            //ObjMissionChangeModel.MissionName = ObjMissionDetailedModel.MissionName;
            //ObjMissionChangeModel.SortOrder = 1;
            //ObjMissionChangeModel.StarDate = ObjMissionDetailedModel.StarDate;
            //ObjMissionChangeModel.WorkNode = ObjMissionDetailedModel.WorkNode;
            ObjMissionChangeBLL.Insert(ObjMissionChangeModel);
            JavaScriptTools.AlertWindowAndLocation("提交成功", "FL_MissionMananger.aspx", Page);
            btnSaveChange.Visible = false;

            FL_Message ObjMessageModel = new FL_Message();
            Message objMessageBLL = new Message();

            ObjMessageModel.EmployeeID = ObjMissionDetailedModel.ChecksEmployee;

            ObjMessageModel.MissionID = ObjMissionDetailedModel.MissionID;
            ObjMessageModel.IsDelete = false;
            ObjMessageModel.IsLook = false;
            ObjMessageModel.Message = ObjEmployeeBLL.GetByID(ObjMissionDetailedModel.EmpLoyeeID).EmployeeName + "的" + ObjMissionDetailedModel.MissionName + " 任务申请变更。 录入时间为：" + DateTime.Now;
            ObjMessageModel.MessAgeTitle = "任务变更申请";
            ObjMessageModel.KeyWords = "FL_MissionChange.aspx?ChangeID=" + ObjMissionChangeModel.ChangeID;
            ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
            objMessageBLL.Insert(ObjMessageModel);
            // this.lblWorkNode.Text = ObjMissionDetailedModel.WorkNode;
        }
    }
}