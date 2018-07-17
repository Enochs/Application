using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionGroupCheck : SystemPage
    {
        /// <summary>
        /// 任务详情操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();

        /// <summary>
        /// 任务主体操作
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        int MissionsID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            MissionsID = Request["MissionID"].ToInt32();
            if (!IsPostBack)
            {
                BindChecks();
            }
        }


        /// <summary>
        /// 绑定任务基本信息
        /// </summary>
        private void BindChecks()
        {
           
            if (Request["MissionID"] != null)
            {
                if (!IsPostBack)
                {
                    this.rptMission.DataSource = ObjMissionDetailedBLL.GetbyMissionID(Request["MissionID"].ToInt32());
                    this.rptMission.DataBind();

                    // txtMissiontitle.Text = ObjMissionManagerBLL.GetByID(Request["MissionID"].ToInt32()).MissionTitle;
                }
            }


            var ObjMissionModel= ObjMissionManagerBLL.GetByID(MissionsID);
            lblEmployee.Text =GetEmployeeName(ObjMissionModel.EmployeeID);
            //if (Request["DetailedID"] != null)
            //{
            //    List<FL_MissionDetailed> ObjDatalist = new List<FL_MissionDetailed>();
            //    ObjDatalist.Add(ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32()));
            //    this.rptMission.DataSource=ObjDatalist;
            //    this.rptMission.DataBind();
            //    return;
            //}
        }


        /// <summary>
        /// 保存审核结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            if (txtallCheckNode.Text != string.Empty)
            {
                var ObjDataList = ObjMissionDetailedBLL.GetbyMissionID(MissionsID);
                foreach (var Objitem in ObjDataList)
                {
                    Objitem.ChecksDate = DateTime.Now;
                    Objitem.ChecksState = 3;
                    ObjMissionDetailedBLL.Update(Objitem);
                }

            }
            var ObjUpdateModel = ObjMissionManagerBLL.GetByID(MissionsID);
            ObjUpdateModel.CheckContent = txtallCheckNode.Text;
            ObjUpdateModel.IsCheck = true;
            ObjUpdateModel.CheckState = 3;
            ObjMissionManagerBLL.Update(ObjUpdateModel);


            Employee EmployeeBLL = new Employee();
            FL_Message ObjMessageModel = new FL_Message();
            Message ObjMessageBLL = new Message();
            ObjMessageModel.EmployeeID = ObjUpdateModel.EmployeeID;
            ObjMessageModel.MissionID = ObjUpdateModel.MissionID;
            ObjMessageModel.IsDelete = false;
            ObjMessageModel.IsLook = false;
            ObjMessageModel.Message = "部门负责已经同意计划 备注说明为：" + txtallCheckNode.Text;
            ObjMessageModel.MessAgeTitle = "计划审核通过";
            ObjMessageModel.KeyWords = "FL_MissionGroupforFinish.aspx?MissionID=" + ObjMessageModel.MissionID;
            ObjMessageModel.CreateEmployeename = EmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
            ObjMessageBLL.Insert(ObjMessageModel);
            btnReturn.Visible = false;
            btnSaveChange.Visible = false;
            JavaScriptTools.AlertWindowAndLocation("审核完毕", "FL_MissionChecking.aspx?NeedPopu=1", Page);

            MissionChange ObjMissionChangeBLL = new MissionChange();

            ObjMissionChangeBLL.Delete(new DataAssmblly.FL_MissionChange() { ChangeID = Request["ChangeID"].ToInt32() });
            //FL_MissionDetailed ObjMissionDetaileMdeol;
            //for (int i = 0; i < rptMission.Items.Count; i++)
            //{
            //    ObjMissionDetaileMdeol = ObjMissionDetailedBLL.GetByID(((HiddenField)rptMission.Items[i].FindControl("hiddDetailedID")).Value.ToInt32());
            //    ObjMissionDetaileMdeol.MissionName = ((TextBox)rptMission.Items[i].FindControl("txtMissionName")).Text;
            //    ObjMissionDetaileMdeol.WorkNode = ((TextBox)rptMission.Items[i].FindControl("txtWorkNode")).Text;
            //    ObjMissionDetaileMdeol.FinishStandard = ((TextBox)rptMission.Items[i].FindControl("txtFinishStandard")).Text;
            //    ObjMissionDetaileMdeol.Attachment = ((TextBox)rptMission.Items[i].FindControl("txtAttachment")).Text;
            //    ObjMissionDetaileMdeol.PlanDate = ((TextBox)rptMission.Items[i].FindControl("txtFinishDate")).Text.ToDateTime();
            //    ObjMissionDetaileMdeol.Countdown = ((TextBox)rptMission.Items[i].FindControl("txtCountdown")).Text.ToInt32();
            //    ObjMissionDetaileMdeol.Emergency = ((TextBox)rptMission.Items[i].FindControl("txtEmergency")).Text;
            //    ObjMissionDetaileMdeol.ChecksState = 3;
            //    ObjMissionDetaileMdeol.ChecksNode = ((TextBox)rptMission.Items[i].FindControl("txtChecksNode")).Text;
             
            //    if (ObjMissionDetaileMdeol.ChecksNode == string.Empty)
            //    {
            //        ObjMissionDetaileMdeol.ChecksNode = txtallCheckNode.Text;
            //    }
            //    ObjMissionDetaileMdeol.EmpLoyeeID =User.Identity.Name.ToInt32();
            //    ObjMissionDetailedBLL.Update(ObjMissionDetaileMdeol);
            //}

        }


        /// <summary>
        /// 打回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (txtallCheckNode.Text != string.Empty)
            {
                var ObjDataList = ObjMissionDetailedBLL.GetbyMissionID(MissionsID);
                foreach (var Objitem in ObjDataList)
                {
                    Objitem.ChecksDate = DateTime.Now;
                    Objitem.ChecksState = 4;
                    ObjMissionDetailedBLL.Update(Objitem);
                }
                
            }
            var ObjUpdateModel = ObjMissionManagerBLL.GetByID(MissionsID);
            ObjUpdateModel.CheckContent = txtallCheckNode.Text;
            ObjUpdateModel.IsCheck = false;
            ObjUpdateModel.CheckState = 1;
            Employee EmployeeBLL = new Employee();
            FL_Message ObjMessageModel = new FL_Message();
            Message ObjMessageBLL = new Message();
            ObjMessageModel.EmployeeID = ObjUpdateModel.EmployeeID;
            ObjMessageModel.MissionID = ObjUpdateModel.MissionID;
            ObjMessageModel.IsDelete = false;
            ObjMessageModel.IsLook = false;
            ObjMessageModel.Message = "计划审核未通过 备注说明为：" + txtallCheckNode.Text;
            ObjMessageModel.MessAgeTitle = "计划审核未通过";
            ObjMessageModel.KeyWords = "FL_MissionGroupforFinish.aspx?MissionID="+ObjMessageModel.MissionID;
            ObjMessageModel.CreateEmployeename = EmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
            ObjMessageBLL.Insert(ObjMessageModel);
            ObjMissionManagerBLL.Update(ObjUpdateModel);

            JavaScriptTools.AlertWindowAndLocation("审核完毕", "FL_MissionChecking.aspx?NeedPopu=1", Page);
            btnReturn.Visible = false;
            btnSaveChange.Visible = false;

            MissionChange ObjMissionChangeBLL = new MissionChange();

            ObjMissionChangeBLL.Delete(new DataAssmblly.FL_MissionChange() { ChangeID = Request["ChangeID"].ToInt32() });
            //FL_MissionDetailed ObjMissionDetaileMdeol;
            //for (int i = 0; i < rptMission.Items.Count; i++)
            //{
            //    ObjMissionDetaileMdeol = ObjMissionDetailedBLL.GetByID(((HiddenField)rptMission.Items[i].FindControl("hiddDetailedID")).Value.ToInt32());
            //    ObjMissionDetaileMdeol.MissionName = ((TextBox)rptMission.Items[i].FindControl("txtMissionName")).Text;
            //    ObjMissionDetaileMdeol.WorkNode = ((TextBox)rptMission.Items[i].FindControl("txtWorkNode")).Text;
            //    ObjMissionDetaileMdeol.FinishStandard = ((TextBox)rptMission.Items[i].FindControl("txtFinishStandard")).Text;
            //    ObjMissionDetaileMdeol.Attachment = ((TextBox)rptMission.Items[i].FindControl("txtAttachment")).Text;
            //    ObjMissionDetaileMdeol.PlanDate = ((TextBox)rptMission.Items[i].FindControl("txtFinishDate")).Text.ToDateTime();
            //    ObjMissionDetaileMdeol.Countdown = ((TextBox)rptMission.Items[i].FindControl("txtCountdown")).Text.ToInt32();
            //    ObjMissionDetaileMdeol.Emergency = ((TextBox)rptMission.Items[i].FindControl("txtEmergency")).Text;
            //    ObjMissionDetaileMdeol.ChecksState = 4;
            //    ObjMissionDetaileMdeol.ChecksNode = ((TextBox)rptMission.Items[i].FindControl("txtChecksNode")).Text;
            //    if (ObjMissionDetaileMdeol.ChecksNode == string.Empty)
            //    {
            //        ObjMissionDetaileMdeol.ChecksNode = txtallCheckNode.Text;
            //    }
            //    ObjMissionDetaileMdeol.EmpLoyeeID = User.Identity.Name.ToInt32();
            //    ObjMissionDetailedBLL.Update(ObjMissionDetaileMdeol);
            //}
        }

        protected void rptMission_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}