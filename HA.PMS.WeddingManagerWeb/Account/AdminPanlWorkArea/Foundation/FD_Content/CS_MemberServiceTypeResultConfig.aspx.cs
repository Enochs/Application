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
    public partial class CS_MemberServiceTypeResultConfig : SystemPage
    {
       // CS_MemberServiceTypeResult
        MemberServiceTypeResult objMemberServiceTypeResultBLL = new MemberServiceTypeResult();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {

            rptDegree.DataSource = objMemberServiceTypeResultBLL.GetByAll();
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
                    CS_MemberServiceTypeResult assess = objMemberServiceTypeResultBLL.GetByID(id);
                    assess.ServiceTypeName = txtName.Text;
                    int result = objMemberServiceTypeResultBLL.Update(assess);
                    PromptStr(result);
                }
                else
                {
                    JavaScriptTools.AlertWindow("该项不能为空", this.Page);
                }

            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text))
            {
                CS_MemberServiceTypeResult assess = new CS_MemberServiceTypeResult();
                assess.CreateEmployee = User.Identity.Name.ToInt32();
                assess.CreateTime = DateTime.Now;
                assess.ServiceTypeName = txtNewName.Text;
                int result = objMemberServiceTypeResultBLL.Insert(assess);
                PromptStr(result);
            }
            else
            {
                JavaScriptTools.AlertWindow("该项不能为空", this.Page);
            }

        }


        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="result"></param>
        protected void PromptStr(int result)
        {
            string promptStr = result > 0 ? "操作成功" : "操作失败，请重试";
            txtNewName.Text = "";
            DataBinder();
            JavaScriptTools.AlertWindow(promptStr, this.Page);

        }
    }
}