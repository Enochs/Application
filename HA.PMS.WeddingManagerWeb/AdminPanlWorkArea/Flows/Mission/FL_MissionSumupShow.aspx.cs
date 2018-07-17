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
    public partial class FL_MissionSumupShow : SystemPage
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
                if (ObjBinderModel != null)
                {
                    lblSumup.Text = ObjBinderModel.SumUp;
                    lblTitle.Text = ObjBinderModel.Title;
                    lblup.Text = ObjBinderModel.Counselingadvice;


                    lblAllMissionSum.Text = ObjMissionDetailedBLL.GetCountByEmployeeID(User.Identity.Name.ToInt32()).ToString();
                    lblFinishSum.Text = ObjMissionManagerBLL.GetMissionSum(User.Identity.Name.ToInt32(), 1, "",1).MissiionCount.ToString();
                    lblOverFinishSum.Text = ObjMissionManagerBLL.GetMissionSum(User.Identity.Name.ToInt32(), 3, "",1).MissiionCount.ToString();
                    decimal AllMisionSum = lblAllMissionSum.Text.ToDecimal();
                    decimal FinishSum = lblFinishSum.Text.ToDecimal() + lblOverFinishSum.Text.ToDecimal();
                    if (AllMisionSum > 0)
                    {
                        lblFinishRate.Text = (FinishSum / AllMisionSum).ToString("0.00%");
                    }
                    else
                    {
                        lblFinishRate.Text = "0.00%";
                    }
                }
            }
        }

    }
}