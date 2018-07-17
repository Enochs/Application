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
    public partial class FL_NewMission : SystemPage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断是否 为管理员 为管理员且任务为统筹任务时才能下达任务
                hideManagerValue.Value = ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()).ToString();
                DataBinder();
            }
        }

        /// <summary>
        /// 绑定明细表
        /// </summary>
        protected void DataBinder()
        {

            rptMission.Visible = true;
            //repMissionResualt.Visible = false;
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("EmployeeID", User.Identity.Name.ToInt32()));
            ObjParameterList.Add(new ObjectParameter("IsDelete", false));
            ObjParameterList.Add(new ObjectParameter("IsOver", false));
            ObjParameterList.Add(new ObjectParameter("IsLook", false));
            ObjParameterList.Add(new ObjectParameter("ChecksState", 3));
            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            rptMission.DataSource = objMissionDetailsedBLL.GetMissionDetailedByWhere(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount, ObjParameterList);
            CtrPageIndex.RecordCount = sourceCount;
            rptMission.DataBind();
            lblMissionCount.Text = rptMission.Items.Count + string.Empty;

        }

        //判断是否为婚礼统筹任务 暂未结束的才能继续分派
        public string GetPlanStyle(string MissionType, string IsOver)
        {
            if (hideManagerValue.Value == "True" && MissionType == "8" && IsOver == "False")
            {
                return string.Empty;
            }
            else
            {
                return "style=\"display:none;\"";
            }

        }

        protected void rptMission_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int missionDetailedId = e.CommandArgument.ToString().ToInt32();
                var UpdateModel = objMissionDetailsedBLL.GetByID(missionDetailedId);
                UpdateModel.MissionState = 1;
                objMissionDetailsedBLL.Update(UpdateModel);
                WarningMessage ObjWarningMessageBLL = new WarningMessage();
                FL_WarningMessage ObjWareMessage = new FL_WarningMessage();
                Employee ObjEmployeeBLL = new Employee();
                ObjWareMessage.CreateDate = DateTime.Now;
                ObjWareMessage.EmpLoyeeID = UpdateModel.EmpLoyeeID;
                ObjWareMessage.IsLook = false;
                ObjWareMessage.MessAgeTitle = "任务已拒绝";
                ObjWareMessage.ResualtAddress = string.Empty;
                ObjWareMessage.managerEmployee = ObjEmployeeBLL.GetMineCheckEmployeeID(UpdateModel.EmpLoyeeID);
                ObjWareMessage.FinishKey = UpdateModel.MissionID;
                ObjWareMessage.State = 3;
                ObjWareMessage.CustomerID = 0;
                ObjWarningMessageBLL.Insert(ObjWareMessage);

                FL_Message ObjMessageModel = new FL_Message();
                Message objMessageBLL = new Message();

                ObjMessageModel.EmployeeID = UpdateModel.CreateEmployeeID;

                ObjMessageModel.MissionID = UpdateModel.MissionID;
                ObjMessageModel.IsDelete = false;
                ObjMessageModel.IsLook = false;
                ObjMessageModel.Message = "任务:" + UpdateModel.MissionName + "；被拒绝! 任务已被拒绝。 时间为：" + DateTime.Now;
                ObjMessageModel.MessAgeTitle = "任务:" + UpdateModel.MissionName+"；被拒绝!";
                ObjMessageModel.KeyWords = "FL_MissionAppraise.aspx?DetailedID=" + UpdateModel.DetailedID;
                ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                objMessageBLL.Insert(ObjMessageModel);

                JavaScriptTools.AlertWindow("操作完毕！任务已拒绝，警告消息已经发送给任务发起人:" + ObjEmployeeBLL.GetByID(UpdateModel.CreateEmployeeID).EmployeeName, Page);
                //FL_MissionDetailed fl_MissionDetailed = new FL_MissionDetailed();
                //fl_MissionDetailed.DetailedID = missionDetailedId;
                //objMissionDetailsedBLL.Delete(fl_MissionDetailed);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptMission_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ObjButtom = (LinkButton)e.Item.FindControl("lkbtnDelete");
                var UpdateModel = ((FL_MissionDetailed)e.Item.DataItem);
                if (UpdateModel != null)
                {
                    if (UpdateModel.Type <= 3 || UpdateModel.MissionState==1)
                    {
                        ObjButtom.CssClass += "RemoveClass";
                    }
                }

            }
        }

    }
}