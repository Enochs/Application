using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.SystemConfig
{
    public partial class Sys_SysConfig : SystemPage
    {
        SysConfig ObjSysConfigBLL = new SysConfig();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

            repSysConfig.DataSource = ObjSysConfigBLL.Getbyall();
            repSysConfig.DataBind();
        }


        /// <summary>
        /// 启用或者停用
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repSysConfig_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Star")
            {

                var ObjUpdateMOdel = ObjSysConfigBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjUpdateMOdel.IsClose = false;

                ObjSysConfigBLL.Update(ObjUpdateMOdel);
                BinderData();
            }
            else
            {
                var ObjUpdateMOdel = ObjSysConfigBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjUpdateMOdel.IsClose = true;

                ObjSysConfigBLL.Update(ObjUpdateMOdel);
                BinderData();
            }
        }


    }
}