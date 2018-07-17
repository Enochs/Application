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
    public partial class FL_MoneyRateSpanConfig : SystemPage
    {
       // FL_MoneyRateSpan
        MoneyRateSpan objMoneyRateSpanBLL = new MoneyRateSpan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {

            rptDegree.DataSource = objMoneyRateSpanBLL.GetByAll();
            rptDegree.DataBind();

        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        protected string GetPirceString(object source, int indexs)
        {
            string[] MoneyValue = (source + string.Empty).Split('-');
            return MoneyValue[indexs];
        }
        protected void rptDegree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int id = (e.CommandArgument + string.Empty).ToInt32();
                TextBox txtName = e.Item.FindControl("txtName") as TextBox;
                TextBox txtName2 = e.Item.FindControl("txtName2") as TextBox;
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtName2.Text))
                {
                    decimal starPrice = txtName.Text.Trim().ToDecimal();
                    decimal endPrice = txtName2.Text.Trim().ToDecimal();
                    if (endPrice <= starPrice)
                    {
                        txtName.Focus();
                        txtName2.Focus();
                        JavaScriptTools.AlertWindow("起始利润率不能大于或者等于截止利润率", this.Page);

                    }
                    else
                    {

                        FL_MoneyRateSpan assess = objMoneyRateSpanBLL.GetByID(id);
                        assess.RateName = txtName.Text + "-" + txtName2.Text;
                        assess.RateValue = txtName.Text + "-" + txtName2.Text;
                        int result = objMoneyRateSpanBLL.Update(assess);
                        PromptStr(result);
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("该项不能为空", this.Page);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text) && !string.IsNullOrEmpty(txtNewName2.Text))
            {

                decimal starPrice = txtNewName.Text.Trim().ToDecimal();
                decimal endPrice = txtNewName2.Text.Trim().ToDecimal();
                if (endPrice <= starPrice)
                {
                    txtNewName.Focus();
                    txtNewName2.Focus();
                    JavaScriptTools.AlertWindow("起始利润率不能大于或者等于截止利润率", this.Page);
                }
                else
                {
                    FL_MoneyRateSpan assess = new FL_MoneyRateSpan();
                    assess.CreateEmployee = User.Identity.Name.ToInt32();
                    assess.CreateTime = DateTime.Now;
                    assess.RateName = txtNewName.Text + "到" + txtNewName2.Text;
                    assess.RateValue = txtNewName.Text + "-" + txtNewName2.Text;
                    int result = objMoneyRateSpanBLL.Insert(assess);
                    PromptStr(result);
                }

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
            txtNewName2.Text = "";
            DataBinder();
            JavaScriptTools.AlertWindow(promptStr, this.Page);

        }
    }
}