using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch
{
    public partial class WarningMessageforEmployee :SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {
            WarningMessage ObjWarningMessageBLL = new WarningMessage();
            int SourceCount = 0;
            this.rptMission.DataSource = ObjWarningMessageBLL.GetByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, Request["EmployeeID"].ToInt32());
            CtrPageIndex.RecordCount = SourceCount;
            this.rptMission.DataBind();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        protected bool IsNullOrEmpty(object obj)
        {
            if (obj != null)
            {
                return string.IsNullOrEmpty(obj.ToString().Trim());
            }
            return true;
        }
    }
}