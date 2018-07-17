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
    public partial class CA_WeddingSceneEvaluationResultConfig : SystemPage
    {
        WeddingSceneEvaluationResult objWeddingSceneEvaluationResultBLL = new WeddingSceneEvaluationResult();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {

            rptDegree.DataSource = objWeddingSceneEvaluationResultBLL.GetByAll();
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
                    CA_WeddingSceneEvaluationResult assess = objWeddingSceneEvaluationResultBLL.GetByID(id);
                    assess.EvaluationName = txtName.Text;
                    int result = objWeddingSceneEvaluationResultBLL.Update(assess);
                    PromptStr(result);
                }
                else
                {
                    JavaScriptTools.AlertWindow("该项不能为空", this.Page);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text))
            {
                CA_WeddingSceneEvaluationResult assess = new CA_WeddingSceneEvaluationResult();
                assess.CreateEmployee = User.Identity.Name.ToInt32();
                assess.CreateTime = DateTime.Now;
                assess.EvaluationName = txtNewName.Text;
                int result = objWeddingSceneEvaluationResultBLL.Insert(assess);
                PromptStr(result);
            }
            else
            {
                JavaScriptTools.AlertWindow("该项不能为空", this.Page);
            }

        }

        protected void PromptStr(int result)
        {
            string promptStr = result > 0 ? "操作成功" : "操作失败，请重试";
            txtNewName.Text = "";
            DataBinder();
            JavaScriptTools.AlertWindow(promptStr, this.Page);

        }
    }
}