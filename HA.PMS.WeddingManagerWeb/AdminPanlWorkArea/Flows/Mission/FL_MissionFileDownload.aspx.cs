using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionFileDownload : SystemPage
    {
        MissionFile ObjMissionFileBLL = new MissionFile();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.repMissionResualt.DataSource = ObjMissionFileBLL.GetByMission(Request["DetailedID"].ToInt32());
                this.repMissionResualt.DataBind();

            }
        }

        protected void repMissionResualt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int FileID = e.CommandArgument.ToString().ToInt32();
            var FileModel = ObjMissionFileBLL.GetByID(FileID);
            //.下载
            try
            {
                IOTools.DownLoadFile(Server.MapPath(FileModel.FileAddress), FileModel.FileName);
            }
            catch (Exception ex) { JavaScriptTools.AlertWindow("下载失败！该文件可能已被移除", Page); }
        }
    }
}