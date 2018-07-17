

/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.13
 Description:控制修改页面
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
    public partial class Sys_ControlsUpdate :SystemPage
    {
        Controls objControlsBLL = new Controls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //接收参数
              int controlID =  Request.QueryString["controlID"].ToInt32();

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //后期进行补充
        }
    }
}