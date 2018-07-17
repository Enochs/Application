using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class NoticeUpdate : PopuPage
    {
        Notice ObjNoticeBLL = new Notice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int NoticeKey = Request.QueryString["NoticeKey"].ToInt32();
                CA_Notice ObjNoticeModel = ObjNoticeBLL.GetByID(NoticeKey);



                txtNoticeContent.Text = ObjNoticeModel.NoticeContent;
                txtTitle.Text = ObjNoticeModel.Title;


            }
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            int NoticeKey = Request.QueryString["NoticeKey"].ToInt32();
            CA_Notice ObjNoticeModel = ObjNoticeBLL.GetByID(NoticeKey);


            ObjNoticeModel.NoticeContent = txtNoticeContent.Text;
            ObjNoticeModel.Title = txtTitle.Text;
            ObjNoticeModel.CreateDate = DateTime.Now;
            int result = ObjNoticeBLL.Update(ObjNoticeModel);
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
    }
}