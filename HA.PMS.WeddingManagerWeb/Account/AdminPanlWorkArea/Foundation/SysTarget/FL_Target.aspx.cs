 
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget
{
    public partial class FL_Target : SystemPage
    {

        Target ObjTargetBLL = new Target();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定目标
        /// </summary>
        private void BinderData()
        {
            repTarget.DataSource = ObjTargetBLL.GetByAll();
            repTarget.DataBind();
        }
    }
}