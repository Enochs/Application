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

            repSysConfig.DataSource = ObjSysConfigBLL.GetByType(1);
            repSysConfig.DataBind();

            RepSysConfigs.DataBind(ObjSysConfigBLL.GetByType(2));
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
                ObjUpdateMOdel.IsClose = true;

                ObjSysConfigBLL.Update(ObjUpdateMOdel);
                BinderData();
            }
            else
            {
                var ObjUpdateMOdel = ObjSysConfigBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjUpdateMOdel.IsClose = false;

                ObjSysConfigBLL.Update(ObjUpdateMOdel);
                BinderData();
            }
        }


        public string GetState(object Source)
        {
            bool IsClose = Source.ToString().ToBool();
            if (IsClose == true)
            {
                return "启用";
            }
            else
            {
                return "停用";
            }

        }

        #region 选择责任人
        /// <summary>
        /// 选择
        /// </summary> 
        protected void RepSysConfigs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                HiddenField hideEmployyID = e.Item.FindControl("hideEmpLoyeeID") as HiddenField;
                int EmployeeID = hideEmployyID.Value.ToString().ToInt32();

                int ConfigID = (e.Item.FindControl("HideConfigID") as HiddenField).Value.ToString().ToInt32();
                var ConfigModel = ObjSysConfigBLL.GetByID(ConfigID);
                ConfigModel.CheckEmployeeID = EmployeeID;
                int result = ObjSysConfigBLL.Update(ConfigModel);

                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
            }
            catch
            {
                Response.Redirect(this.Page.Request.Url.ToString());
            }

        }
        #endregion

        #region 特殊人员绑定完成事件
        /// <summary>
        /// 绑定完成
        /// </summary>   
        protected void RepSysConfigs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int ConfigID = (e.Item.FindControl("HideConfigID") as HiddenField).Value.ToString().ToInt32();
            var ConfigModel = ObjSysConfigBLL.GetByID(ConfigID);
            if (ConfigModel != null && ConfigModel.CheckEmployeeID != null)
            {
                TextBox TxtCheckEmployee = e.Item.FindControl("txtEmpLoyee") as TextBox;
                TxtCheckEmployee.Text = GetEmployeeName(ConfigModel.CheckEmployeeID.ToString().ToInt32());
            }
        }
        #endregion


    }
}