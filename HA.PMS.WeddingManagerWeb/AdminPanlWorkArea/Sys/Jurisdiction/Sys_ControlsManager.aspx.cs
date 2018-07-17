
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:创建控制管理页面
 History:修改日志
 
 Author:杨洋
 date:2013.3.13
 version:好爱1.0
 description:修改描述
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class Sys_ControlsManager : OtherPage
    {
        Controls objControlsBLL = new Controls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化绑定信息
                DataBinder();
            }
        }
        /// <summary>
        /// 加载控制信息绑定Repeater控件
        /// </summary>
        private void DataBinder()
        {
            rptControls.DataSource = objControlsBLL.GetByChannel(Request["ChannelID"].ToInt32());
            rptControls.DataBind();
        }
        /// <summary>
        /// 通过ItemCommand事件进行删除操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptControls_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           
            if (e.CommandName == "Delete")
            {
                int controlID = e.CommandArgument.ToString().ToInt32();
                Sys_Controls sys_Controls = new Sys_Controls()
                {
                    ControlID = controlID

                };
                //进行删除操作
                objControlsBLL.Delete(sys_Controls);
                //重新绑定Repeater
                DataBinder();
            }

        }
    }
}