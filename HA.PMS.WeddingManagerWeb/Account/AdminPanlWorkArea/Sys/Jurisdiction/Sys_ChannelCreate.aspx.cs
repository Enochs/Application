using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction
{
    public partial class Sys_ChannelCreate : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            Sys_Channel sys_Channel = new Sys_Channel();
            sys_Channel.ChannelAddress = txtChannelAddress.Text;
            sys_Channel.ChannelName = txtChannelName.Text;
            sys_Channel.CreateDate = DateTime.Now;
            sys_Channel.Parent = Request["Parent"].ToInt32();
            sys_Channel.IsMenu = chkismenu.Checked;
            sys_Channel.OrderCode = txtOrderCode.Text.ToInt32();
            sys_Channel.ChannelGetType = txtChannelGetType.Text;
            sys_Channel.IsPublic = false;
            if (new Channel().Insert(sys_Channel) > 0)
            {
                //JavaScriptTools.AlertWindowAndReload("添加成功", this.Page);
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
        }
    }
}