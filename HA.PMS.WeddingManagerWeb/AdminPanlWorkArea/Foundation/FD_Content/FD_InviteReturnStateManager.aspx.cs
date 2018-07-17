using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_InviteReturnStateManager : System.Web.UI.Page
    {
        InviteReturnState ObjInviteStateBLL = new InviteReturnState();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ObjInviteStateBLL.Insert(new DataAssmblly.FD_InviteReturnState() { Name = txtTitle.Text.ToString(),CreateEmployee=User.Identity.Name.ToInt32(),CreateDate=DateTime.Now.ToString().ToDateTime() });
            Response.Redirect(Request.Url.ToString());
        }

        protected void repItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int Id = e.CommandArgument.ToString().ToInt32();
            var InviteModel = ObjInviteStateBLL.GetByID(Id);
            int result = 0;
            if (e.CommandName == "Delete")
            {
                result = ObjInviteStateBLL.Delete(InviteModel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }

            }
            else if (e.CommandName == "Edit")
            {
                TextBox txtTitle = e.Item.FindControl("txtName") as TextBox;
                InviteModel.Name = txtTitle.Text.ToString();
                result = ObjInviteStateBLL.Update(InviteModel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
            }
            DataBinder();

        }

        public void DataBinder()
        {
            this.repItem.DataSource = ObjInviteStateBLL.GetByAll();
            repItem.DataBind();
        }
    }
}