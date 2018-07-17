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
    public partial class FL_MissionGroupforEdit : SystemPage
    {
        /// <summary>
        /// 任务主体操作
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {

            //repMissionResualt.Visible = false;
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("EmployeeID", User.Identity.Name.ToInt32()));
            ObjParameterList.Add(new ObjectParameter("IsCheck", false));
            ObjParameterList.Add(new ObjectParameter("CheckState", 1));
            ObjParameterList.Add(new ObjectParameter("MissionType_Greaterthan", "9,500"));
            
            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            CtrPageIndex.RecordCount = sourceCount;
            var DataSource = ObjMissionManagerBLL.GetMissionManagerByWhere(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount, ObjParameterList);
            repMissionResualt.DataSource = DataSource;
  
            repMissionResualt.DataBind();

            //MissionChange ObjMissionChangeBLL = new MissionChange();

            //ObjMissionChangeBLL.Delete(new DataAssmblly.FL_MissionChange() { ChangeID = Request["ChangeID"].ToInt32() });

        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}