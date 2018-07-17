using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Personal
{
    public partial class UpdatePassWord : SystemPage
    {
        #region 页面初始化


        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Employee ObjEmployeeBLL = new Employee();
            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            string OldPass = Model.PassWord;
            string InputOldPass = txtOldPass.Text.Trim().ToString().MD5Hash();
            string NewPass = txtNewPass.Text.Trim().ToString().MD5Hash();
            if (InputOldPass != OldPass)
            {
                JavaScriptTools.AlertWindow("原密码输入不正确", Page);
                return;
            }
            else if (OldPass == NewPass)
            {
                JavaScriptTools.AlertWindow("密码不能设置为当前密码", Page);
                return;
            }
            else if (NewPass == "123456".MD5Hash())
            {
                JavaScriptTools.AlertWindow("密码不能设置为123456", Page);
            }
            Model.PassWord = NewPass;
            int result = ObjEmployeeBLL.Update(Model);

            if (result > 0)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                HttpCookie cok = Request.Cookies["LoginCookie"];
                if (cok != null)
                {
                    cok.Expires = DateTime.Now.AddDays(-1);
                    Response.AppendCookie(cok);
                }
                Response.Write("<script>alert('密码已经修改,请重新登录');window.parent.parent.document.location ='/Account/LoginOut.aspx';</script>");
            }
            else
            {
                JavaScriptTools.AlertWindow("修改失败,请稍候再试...", Page);
            }
        }
    }
}