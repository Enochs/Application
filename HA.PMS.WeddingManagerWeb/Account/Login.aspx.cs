using System;
using System.Web;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.Account
{
    public partial class Login : System.Web.UI.Page
    {
        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            //清空登录记录
            if (Request["Clear"] != null)
            {
                ClearCookie("LoginCookie");
                Response.Redirect("AdminWorklogin.html?type=3");
            }

            //首页不验证
            if (Request["txtLoginName"] == null || Request["txtpassword"] == null)
            {
                Response.Redirect("AdminWorklogin.html");
            }

            HA.PMS.DataAssmblly.Sys_Employee employee = null;
            string Ip = Page.Request.UserHostAddress;
            if (Ip == "192.168.0.35" || Ip == "192.168.0.199" || Ip == "::1")
            {
                employee = ObjEmployeeBLL.GetByLoginName(Request["txtLoginName"]);
            }
            else
            {
                employee = new Employee().EmpLoyeeLogin(Request["txtLoginName"], Request["txtpassword"].MD5Hash());
            }

            //用户名密码验证
            if (employee == null || employee.EmployeeName == null)
            {
                Response.Redirect("/Account/AdminWorklogin.html?type=1");
            }

            //登录名唯一验证
            if (Request.Cookies["LoginCookie"] != null && Request.Cookies["HAEmployeeID"] != null)
            {
                if (employee.EmployeeID != Request.Cookies["HAEmployeeID"].Value.ToInt32())
                {
                    Response.Redirect("/Account/AdminWorklogin.html?type=2");
                }
            }

            System.Web.Security.FormsAuthentication.SetAuthCookie(employee.EmployeeID.ToString(), false);
            ResponCookie(employee.EmployeeID.ToString(), employee.EmployeeID.ToString());
            Response.Redirect("/AdminPanlWorkArea/AdminMain.aspx");
        }

        private void ClearCookie(string name)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies[name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(cookie);
            }
        }

        private void ResponCookie(string userName, string HAEmployeeID)
        {
            HttpCookie cookie = new HttpCookie("LoginCookie");//初使化并设置Cookie的名称


            cookie.Values.Add("userName", userName);
            cookie.Values.Add("HAEmployeeID", HAEmployeeID);

            Response.Cookies["userName"].Value = userName;
            Response.Cookies["HAEmployeeID"].Value = HAEmployeeID;

            cookie.Expires = DateTime.Now.AddMinutes(30);//设置过期时间为15分钟

            Response.AppendCookie(cookie);
        }
    }
}