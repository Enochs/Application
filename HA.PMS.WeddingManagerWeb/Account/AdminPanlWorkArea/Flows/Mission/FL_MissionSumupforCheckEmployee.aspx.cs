using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionSumupforCheckEmployee : SystemPage
    {

        /// <summary>
        /// 任务详情操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
        /// <summary>
        /// 任务
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        MissionSumup ObjMissionSumupBLL = new MissionSumup();

        Employee ObjEmployeeBLL = new Employee();

        MissionFile ObjMissionFileBLL = new MissionFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ObjBinderModel = ObjMissionSumupBLL.GetByMissionID(Request["MissionID"].ToInt32());
                lblSumUp.Text = ObjBinderModel.SumUp;
                lblTitle.Text = ObjBinderModel.Title;
                

            }
        }


        /// <summary>
        /// 保存辅导意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            var ObjBinderModel = ObjMissionSumupBLL.GetByMissionID(Request["MissionID"].ToInt32());
            ObjBinderModel.Counselingadvice = txtCounselingadvice.Text;
            ObjMissionSumupBLL.Update(ObjBinderModel);
            var ObjUpdateModel = ObjMissionManagerBLL.GetByID(Request["MissionID"].ToInt32());
            ObjUpdateModel.IsAppraise = true;
            ObjMissionManagerBLL.Update(ObjUpdateModel);

            JavaScriptTools.AlertWindowAndLocation("辅导意见保存完毕！", "FL_MissionSumupShow.aspx?MissionID=" + Request["MissionID"], Page);
                
        }
    }
}