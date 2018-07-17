using HA.PMS.Pages;
using System;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class SetOfBasicInformationAdminPanel : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/AdminPanlWorkArea/Foundation/FD_Content/Sys_ComplayLogoConfig.aspx");
        }
    }
}