/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:创建控制添加页面
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

    public partial class Sys_ControlsCreate : OtherPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }
        Controls objControlsBLL = new Controls();
        protected void btnControls_Click(object sender, EventArgs e)
        {
            //构建Sys_Controls 实体控制类
            Sys_Controls Objsys_Controls = new Sys_Controls();

            Objsys_Controls.ConrolKey = txtControlKey.Text;
            Objsys_Controls.BelongByControl = rdolistType.SelectedValue.ToInt32();
            Objsys_Controls.ChannelID = Request["ChannelID"].ToInt32();
            Objsys_Controls.CssClass = txtCssClass.Text;
            Objsys_Controls.ServerType = ddlServerType.SelectedItem.Text;
            Objsys_Controls.ControlName = txtControlTitle.Text;
            Objsys_Controls.IsDelete = false;
            //添加实体对象
            int result = objControlsBLL.Insert(Objsys_Controls);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }
        }
    }
}