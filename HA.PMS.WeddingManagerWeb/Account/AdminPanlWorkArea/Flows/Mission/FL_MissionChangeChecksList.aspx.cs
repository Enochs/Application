using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionChangeChecksList : SystemPage
    {
        /// <summary>
        /// 明细操作
        /// </summary>
        MissionDetailed objMissionDetailsedBLL = new MissionDetailed();


        /// <summary>
        /// 分组操作
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            ObjParameterList.Add(new ObjectParameter("ChecksEmployee", User.Identity.Name.ToInt32()));
            ObjParameterList.Add(new ObjectParameter("IsDelete", false));
            //ObjParameterList.Add(new ObjectParameter("IsOver", false));
            //ObjParameterList.Add(new ObjectParameter("IsLook", false));
            ObjParameterList.Add(new ObjectParameter("ChecksState", 2));
            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            rptMission.DataSource = objMissionDetailsedBLL.GetMissionDetailedByWhere(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount, ObjParameterList);
            CtrPageIndex.RecordCount = sourceCount;
            rptMission.DataBind();
            lblMissionCount.Text = rptMission.Items.Count + string.Empty;
        }

        protected void rptMission_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int missionDetailedId = e.CommandArgument.ToString().ToInt32();


                FL_MissionDetailed fl_MissionDetailed = new FL_MissionDetailed();
                fl_MissionDetailed.DetailedID = missionDetailedId;
                objMissionDetailsedBLL.Delete(fl_MissionDetailed);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}