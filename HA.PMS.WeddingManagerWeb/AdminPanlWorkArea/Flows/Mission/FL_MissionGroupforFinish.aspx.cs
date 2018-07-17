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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionGroupforFinish : SystemPage
    {
        /// <summary>
        /// 任务详情操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();

        /// <summary>
        /// 任务主体操作
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        int MissionID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
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
            MissionID = Request["MissionID"].ToInt32();
            if (Request["MissionID"] != null)
            {
                this.rptMission.DataSource = ObjMissionDetailedBLL.GetbyMissionID(Request["MissionID"].ToInt32());
                this.rptMission.DataBind();
 
            }

            var ObjMissionModel= ObjMissionManagerBLL.GetByID(MissionID);
            lbltitle.Text = ObjMissionModel.MissionTitle;
            if (ObjMissionModel.CheckState==3)
            {
                lblState.Text = "同意";
                lblCheckEmployee.Text =GetEmployeeName(ObjMissionModel.CheckEmpLoyeeID);
                lblCheckDate.Text = DateTime.Now.ToShortDateString();
                lblCheckContent.Text = ObjMissionModel.CheckContent;
            }


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