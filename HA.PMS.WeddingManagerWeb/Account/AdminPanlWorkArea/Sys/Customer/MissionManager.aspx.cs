using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer
{
    public partial class MissionManager : SystemPage
    {
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();

        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        private void BinderData()
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
            if (txtEmployeeName.Text != string.Empty)
            {
                var ObjMOdel = ObjEmployeeBLL.GetByName(txtEmployeeName.Text);
                ObjParameterList.Add(new ObjectParameter("EmployeeID", ObjMOdel.EmployeeID));
            }
            int startIndex = CtrPageIndex.StartRecordIndex;
            int sourceCount = 0;
            rptMission.DataSource = ObjMissionDetailedBLL.GetMissionDetailedByWhere(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out sourceCount, ObjParameterList);
            CtrPageIndex.RecordCount = sourceCount;
            rptMission.DataBind();
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptMission_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            MissionFile ObjMissionFileBLL=new MissionFile();
            int DetailedID = int.Parse(e.CommandArgument.ToString());
            var ObjMissionFileList = ObjMissionFileBLL.GetByMission(DetailedID);
            foreach(var C in ObjMissionFileList)
            {
                ObjMissionFileBLL.Delete(C);
            }
            var ObjItem = ObjMissionDetailedBLL.GetByID(DetailedID);
            ObjMissionDetailedBLL.Delete(ObjItem);
            BinderData();
            JavaScriptTools.AlertWindow("删除成功!", Page);

        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}