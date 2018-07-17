using HA.PMS.BLLAssmblly.CA;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class NoticeContent : PopuPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Notice ObjNoticeBLL = new Notice();
                var BinderModel = ObjNoticeBLL.GetByID(Request["NoticeKey"].ToInt32());
                this.lblContent.Text = BinderModel.NoticeContent;
                this.lbltitle.Text = BinderModel.Title;
            }

        }
    }
}