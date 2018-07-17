using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.CS;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class CompanySystemCreate : SystemPage
    {
        CompanySystem objCompanySystemBLL = new CompanySystem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            CA_CompanySystem sys = new CA_CompanySystem();

            sys.SystemTitle = txtSystemTitle.Text;
            sys.SystemURL = "";
            sys.ParentID = 0;
            sys.IsDelete = false;
            sys.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            sys.CreateEmployee = User.Identity.Name.ToInt32();
            sys.SystemPureRoute = "";
            sys.Remark = "";
            sys.Type = Request["FileType"].ToInt32();

            int result = objCompanySystemBLL.Insert(sys);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }
            btnSave.Enabled = true;

        }
    }
}