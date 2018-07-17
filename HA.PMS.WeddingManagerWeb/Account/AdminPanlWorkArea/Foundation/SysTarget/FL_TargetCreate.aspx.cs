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
    public partial class FL_TargetCreate : System.Web.UI.Page
    {
        Target objTargetBLL = new Target();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            DataAssmblly.FL_Target ObjTargetModel = new DataAssmblly.FL_Target();
            ObjTargetModel.TargetTitle = txtTitle.Text;
            ObjTargetModel.TargetType = ddltype.SelectedValue.ToInt32();
            ObjTargetModel.ChannelID = ddlSysChannel1.SelectedValue.ToInt32();
            ObjTargetModel.Remark = txtRemark.Text;
            ObjTargetModel.ChannelName = ddlSysChannel1.SelectedItem.Text;

            objTargetBLL.Insert(ObjTargetModel);

        }
    }
}