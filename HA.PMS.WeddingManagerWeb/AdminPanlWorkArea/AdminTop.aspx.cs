using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Web;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class Admintop : System.Web.UI.Page
    {
        HA.PMS.BLLAssmblly.Sys.BugSystem ObjBugSystemBLL = new BLLAssmblly.Sys.BugSystem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message ObjMessAge = new Message();


                lblSumCount.Text = ObjMessAge.GetMessAgeSumbyLook(User.Identity.Name.ToInt32()) + string.Empty;
                lblBugCount.Text = ObjBugSystemBLL.GetByAll().ToList().Where(C => C.State == 1 || C.State == 2).ToList().Count().ToString();
            }

        }

        protected void lkbtnExit_Click(object sender, EventArgs e)
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
            }
            Response.Write("<script>alert('退出系统成功');window.parent.document.location ='/Account/LoginOut.aspx';</script>");

        }


    }
}