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
    public partial class FL_MissionSumupManager : SystemPage
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
                BinderData();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();
            ObjParameterList.Add("EmpLoyeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            ObjParameterList.Add("IsDelete", false, NSqlTypes.Equal);
            //ObjParameterList.Add("IsCheck", true, NSqlTypes.Equal);
            ObjParameterList.Add("Type", "61,81", NSqlTypes.NumBetween);

            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            repMissionResualt.DataSource = ObjMissionManagerBLL.GetAllMissionByParameter(ObjParameterList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount);
            CtrPageIndex.RecordCount = sourceCount;
            repMissionResualt.DataBind();

        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnCreatenew_Click(object sender, EventArgs e)
        {
            Response.Redirect("FL_MissionSumupManagerCreateEdit.aspx");
        }
    }
}