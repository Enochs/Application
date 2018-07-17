using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction
{
    public partial class Sys_ChannelUpdate : SystemPage
    {
        Channel ObjChannelBLL = new Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var ChannelModel = ObjChannelBLL.GetByID(Request["ChannelID"].ToInt32());
                txtChannelAddress.Text = ChannelModel.ChannelAddress;
                txtChannelName.Text = ChannelModel.ChannelName;
                txtClass.Text = ChannelModel.StyleSheethem;
                txtgettype.Text = ChannelModel.ChannelGetType;
                txtOrderCode.Text = ChannelModel.OrderCode+string.Empty;
                chkismenu.Checked = ChannelModel.IsMenu.Value;

                if (ChannelModel.Parent != 0)
                {
                    ddlNode.DataSource = ObjChannelBLL.GetByParent(0);
                    ddlNode.DataTextField = "ChannelName";
                    ddlNode.DataValueField = "ChannelID";
                    ddlNode.DataBind();
                    ddlNode.Items.FindByValue(ChannelModel.Parent.ToString()).Selected = true;
                }
                
            }
        }


        /// <summary>
        /// 修改频道信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            var ObjChannelModel = ObjChannelBLL.GetByID(Request["ChannelID"].ToInt32());
            ObjChannelModel.ChannelAddress = txtChannelAddress.Text;
            ObjChannelModel.ChannelName = txtChannelName.Text;
            ObjChannelModel.CreateDate = DateTime.Now;
            ObjChannelModel.IsMenu = chkismenu.Checked;
            ObjChannelModel.StyleSheethem = txtChannelName.Text;
            ObjChannelModel.ChannelGetType = txtgettype.Text;
            if (ObjChannelModel.Parent != 0)
            {
                ObjChannelModel.Parent = ddlNode.SelectedValue.ToInt32();
            }
            
            ObjChannelModel.OrderCode = txtOrderCode.Text.ToInt32();
            ObjChannelBLL.Update(ObjChannelModel);
            JavaScriptTools.AlertAndClosefancybox("修改成功！", this.Page);
        }
    }
}