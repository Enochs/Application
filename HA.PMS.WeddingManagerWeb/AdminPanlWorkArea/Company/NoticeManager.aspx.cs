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
    public partial class NoticeManager : SystemPage
    {
        Notice ObjNoticeBLL = new Notice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void DataBinder()
        {
            #region 分页页码

            int startIndex = NoticePager.StartRecordIndex;
            int resourceCount = 0;
            var query = ObjNoticeBLL.GetByIndex(NoticePager.PageSize, NoticePager.CurrentPageIndex, out resourceCount);
            NoticePager.RecordCount = resourceCount;

            repNoticelist.DataSource = query;
            repNoticelist.DataBind();


            #endregion
        }
        protected void NoticePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        #region 删除事件
        /// <summary>
        /// 删除通知消息
        /// </summary>
        protected void repNoticelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int key = e.CommandArgument.ToString().ToInt32();
                int result = ObjNoticeBLL.Delete(ObjNoticeBLL.GetByID(key));
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
                }
                DataBinder();
            }
        }
        #endregion
    }
}
