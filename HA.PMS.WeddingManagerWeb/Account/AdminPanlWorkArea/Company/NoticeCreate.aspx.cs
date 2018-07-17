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
    public partial class NoticeCreate : PopuPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Notice ObjNoticeBLL = new Notice();
            CA_Notice ObjNoticeModel = new CA_Notice();
            ObjNoticeModel.CreateDate = DateTime.Now;
            ObjNoticeModel.EmpLoyeeID = User.Identity.Name.ToInt32();
            ObjNoticeModel.NoticeContent = txtContent.Text;
            ObjNoticeModel.Title = txtTitle.Text;
            JavaScriptTools.AlertAndClosefancybox("添加成功",Page);
            ObjNoticeBLL.Insert(ObjNoticeModel);
            
        }
    }
}