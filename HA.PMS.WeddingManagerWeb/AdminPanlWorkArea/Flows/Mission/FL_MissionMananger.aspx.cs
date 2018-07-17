/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.23
 Description:任务管理页面
 History:修改日志
 （客户电话营销）
 Author:杨洋
 date:2013.3.23
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
    public partial class FL_MissionMananger : SystemPage
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
            //if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            //{
            //    ObjParameterList.Add(new ObjectParameter("EmployeeID_CreateEmployeeID_PVP", User.Identity.Name.ToInt32() + "," + User.Identity.Name.ToInt32()));

            //}
            //else
            //{
            ObjParameterList.Add(new ObjectParameter("EmployeeID", User.Identity.Name.ToInt32()));
            // }
            ObjParameterList.Add(new ObjectParameter("IsDelete", false));
          //  ObjParameterList.Add(new ObjectParameter("IsOver", false));
           // ObjParameterList.Add(new ObjectParameter("Type", 9));
            ObjParameterList.Add(new ObjectParameter("PlanDate", DateTime.Now.ToShortDateString().ToDateTime()));
            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            rptMission.DataSource = objMissionDetailsedBLL.GetMissionDetailedByWhere(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount, ObjParameterList);
            CtrPageIndex.RecordCount = sourceCount;
            rptMission.DataBind();
           // lblMissionCount.Text = rptMission.Items.Count + string.Empty;
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





        /// <summary>
        /// 分组绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGroup_Click(object sender, EventArgs e)
        {
            //rptMission.Visible = false;
            //repMissionResualt.Visible = true;
            //repMissionResualt.DataSource = ObjMissionManagerBLL.GetByAll();
            //repMissionResualt.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string GetStateStyle(object State)
        {
            if (State == null)
            {
                return "style='display:none;'";
            }

            if (State != null)
            {
                if (State.ToString() == User.Identity.Name)
                {
                    return string.Empty;
                }
            }
            return "style='display:none;'";
        }
        /// <summary>
        /// 明细查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeatale_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}