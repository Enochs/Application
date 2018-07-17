using HA.PMS.BLLAssmblly.SysTarget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget
{
    public partial class FL_TargetUpdate : System.Web.UI.Page
    {
        Target objTargetBLL = new Target();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ObjModel = objTargetBLL.GetByID(Request["TargetID"].ToInt32());
                txtTitle.Text = ObjModel.TargetTitle;
                txtRemark.Text = ObjModel.Remark;
                txtTitle.Enabled = false;
                ddltype.Items.FindByValue(ObjModel.TargetType.ToString()).Selected = true;

                try
                {
                    ddlSysChannel1.Items.FindByValue(ObjModel.ChannelID.ToString()).Selected = true;
                   
                }
                catch
                { 
                  }
            }



        }



        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            var ObjModel = objTargetBLL.GetByID(Request["TargetID"].ToInt32());
            ObjModel.ChannelID = ddlSysChannel1.SelectedValue.ToInt32();
            ObjModel.Remark = txtRemark.Text;
            ObjModel.ChannelName = ddlSysChannel1.SelectedItem.Text;
            ObjModel.TargetType = ddltype.SelectedValue.ToInt32();
            objTargetBLL.Update(ObjModel);
            JavaScriptTools.AlertAndClosefancybox("Sucess", Page);
            
        }
    }
}