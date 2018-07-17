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
    public partial class CS_DegreeAssessResultConfig : SystemPage
    {
        DegreeAssessResult objDegreeAssessResultBLL = new DegreeAssessResult();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {

            rptDegree.DataSource = objDegreeAssessResultBLL.GetByAll();
            rptDegree.DataBind();

        }

        protected void rptDegree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = (e.CommandArgument + string.Empty).ToInt32();
            if (e.CommandName == "Edit")
            {
                TextBox txtName = e.Item.FindControl("txtName") as TextBox;
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    CS_DegreeAssessResult assess = objDegreeAssessResultBLL.GetByID(id);
                    assess.AssessName = txtName.Text;
                    int result = objDegreeAssessResultBLL.Update(assess);
                    PromptStr(result);
                }
                else
                {
                    JavaScriptTools.AlertWindow("该项不能为空", this.Page);
                }

            }
            else if (e.CommandName == "Delete")
            {
                CS_DegreeAssessResult assess = objDegreeAssessResultBLL.GetByID(id);
                int result = objDegreeAssessResultBLL.Delete(assess);
                PromptStr(result);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text))
            {
                CS_DegreeAssessResult assess = new CS_DegreeAssessResult();
                assess.CreateEmployee = User.Identity.Name.ToInt32();
                assess.CreateTime = DateTime.Now;
                assess.AssessName = txtNewName.Text;
                int result = objDegreeAssessResultBLL.Insert(assess);
                txtNewName.Text = string.Empty;
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