using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionGoon : SystemPage
    {
        /// <summary>
        /// 操作
        /// </summary>
        MissionDetailed ObjMissionManagerBLL = new MissionDetailed();


        /// <summary>
        /// 频道管理
        /// </summary>
        Channel ObjChannelBLL = new Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            var OjbMissionModel = ObjMissionManagerBLL.GetByID(Request["DetailedID"].ToInt32());
            var ObjChannelModel=ObjChannelBLL.GetByID(OjbMissionModel.ChannelID);
            Response.Redirect(ObjChannelModel.ChannelAddress + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID);

        }
    }
}