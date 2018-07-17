using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.Account
{
    public partial class LoginOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();


            HttpCookie cok = Request.Cookies["LoginCookie"];
            if (cok != null)
            {
                //if (!CheckBox1.Checked)
                //{
                // cok.Values.Remove("userid");//移除键值为userid的值
                //}
                //else
                //{
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                //}
                Response.AppendCookie(cok);
                Session.Abandon();
            }


        }
    }
}