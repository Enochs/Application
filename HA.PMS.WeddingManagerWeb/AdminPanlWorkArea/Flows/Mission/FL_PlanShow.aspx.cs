using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_PlanShow : SystemPage
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


            var ObjMissionModel = ObjMissionManagerBLL.GetByID(MissionsID);
            lblEmployee.Text = GetEmployeeName(ObjMissionModel.EmployeeID);
            lblMInssiontitle.Text = ObjMissionModel.MissionTitle;
            //if (Request["DetailedID"] != null)
            //{
            //    List<FL_MissionDetailed> ObjDatalist = new List<FL_MissionDetailed>();
            //    ObjDatalist.Add(ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32()));
            //    this.rptMission.DataSource=ObjDatalist;
            //    this.rptMission.DataBind();
            //    return;
            //}
        }

    }
}