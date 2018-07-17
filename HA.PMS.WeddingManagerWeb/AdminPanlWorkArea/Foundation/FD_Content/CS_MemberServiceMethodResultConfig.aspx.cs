using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class CS_MemberServiceMethodResultConfig : SystemPage
    {
        //CS_MemberServiceMethodResult
        MemberServiceMethodResult objMemberServiceMethodResultBLL = new MemberServiceMethodResult();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {

            rptDegree.DataSource = objMemberServiceMethodResultBLL.GetByAll();
            rptDegree.DataBind();

        }

        protected void rptDegree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int id = (e.CommandArgument + string.Empty).ToInt32();
                TextBox txtName = e.Item.FindControl("txtName") as TextBox;
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    CS_MemberServiceMethodResult assess = objMemberServiceMethodResultBLL.GetByID(id);
                    assess.ServiceName = txtName.Text;
                    int result = objMemberServiceMethodResultBLL.Update(assess);
                    PromptStr(result);
                }
                else
                {
                    JavaScriptTools.AlertWindow("该项不能为空", this.Page);
                }

            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text))
            {
                if (chkIsSp.Checked && txtTemplete.Text.Trim() == string.Empty)
                {

                    JavaScriptTools.AlertWindow("SP类服务需要模板,请编辑模板!",Page);
                    return;
                }
                

                CS_MemberServiceMethodResult assess = new CS_MemberServiceMethodResult();
                assess.CreateEmployee = User.Identity.Name.ToInt32();
                assess.CreateTime = DateTime.Now;
                assess.ServiceName = txtNewName.Text;
                assess.IsSP = chkIsSp.Checked;
                assess.SpTemplete = txtTemplete.Text;
                int result = objMemberServiceMethodResultBLL.Insert(assess);
                PromptStr(result);
            }
            else
            {
                JavaScriptTools.AlertWindow("服务项目名称不能为空", this.Page);
            }

        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="result"></param>
        protected void PromptStr(int result)
        {
            string promptStr = result > 0 ? "操作成功" : "操作失败，请重试";
            txtNewName.Text = string.Empty;
            txtTemplete.Text = string.Empty;
            DataBinder();
            JavaScriptTools.AlertWindow(promptStr, this.Page);

        }
    }
}