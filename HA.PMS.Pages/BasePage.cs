using System;

namespace HA.PMS.Pages
{
    public abstract class BasePage:System.Web.UI.Page
    {
        protected virtual Int32 EmpLoyeeID
        {
            get
            {
                return Convert.ToInt32(User.Identity.Name);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.AppendStateCookie();
            base.OnLoad(e);
        }

        protected virtual void AppendStateCookie(int minute = 1)
        {
            //初使化并设置Cookie的名称
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("StateCookie");
            //过期时间为15分钟
            cookie.Expires = System.DateTime.Now.AddSeconds(10);
            cookie.Values.Add("userName", "A");
            cookie.Values.Add("HAEmployeeID", "-1");
            //System.Web.HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}
