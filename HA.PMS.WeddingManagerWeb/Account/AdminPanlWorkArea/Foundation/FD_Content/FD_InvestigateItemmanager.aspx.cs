using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_InvestigateItemmanager : SystemPage
    {

        CustomerReturnVisit ObjCustomerReturnVisitBLL = new CustomerReturnVisit();
        InviteReturnState ObjInviteStateBLL = new InviteReturnState();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }

        }

        #region 保存信息
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ObjCustomerReturnVisitBLL.AddReturnItem(new DataAssmblly.FD_InvestigateItem() { IsDelete = false, ItemTitle = txtTitle.Text, SortOrder = 0 });
            Response.Redirect(Request.Url.ToString());
        }
        #endregion

        #region 保存状态
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Saves_Click(object sender, EventArgs e)
        {
            ObjInviteStateBLL.Insert(new DataAssmblly.FD_InviteReturnState() { Name = txtNames.Text.ToString(), CreateEmployee = User.Identity.Name.ToInt32(), CreateDate = DateTime.Now.ToString().ToDateTime() });
            Response.Redirect(Request.Url.ToString());
        }
        #endregion

        #region 回访信息 删除 修改
        /// <summary>
        /// 回访信息 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int Id = e.CommandArgument.ToString().ToInt32();
            var InviteModel = ObjCustomerReturnVisitBLL.GetReturnItemByID(Id);
            int result = 0;
            if (e.CommandName == "Delete")
            {
                result = ObjCustomerReturnVisitBLL.DeleteReturnItem(InviteModel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }

            }
            else if (e.CommandName == "Edit")
            {
                TextBox txtTitle = e.Item.FindControl("txtName") as TextBox;
                InviteModel.ItemTitle = txtTitle.Text.ToString();
                result = ObjCustomerReturnVisitBLL.UpdateReturnItem(InviteModel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
            }
            DataBinder();

        }
        #endregion

        #region 回访状态 删除修改
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repItemState_ItemCommand(object source, RepeaterCommandEventArgs e)
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
        #endregion

        #region 绑定信息 状态
        /// <summary>
        /// 绑定
        /// </summary>
        public void DataBinder()
        {
            this.repItem.DataSource = ObjCustomerReturnVisitBLL.GetReturnItemByall();
            repItem.DataBind();

            this.repItemState.DataSource = ObjInviteStateBLL.GetByAll();
            repItemState.DataBind();
        }
        #endregion

    }
}