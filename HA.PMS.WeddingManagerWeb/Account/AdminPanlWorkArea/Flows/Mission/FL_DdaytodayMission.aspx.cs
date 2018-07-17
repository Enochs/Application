using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//零时注释 处理日常任务 黄晓可

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_DdaytodayMission : PopuPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderMission();
            }
        }


        /// <summary>
        /// 绑定任务基本信息
        /// </summary>
        private void BinderMission()
        { 
            
        }


        /// <summary>
        /// 处理任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {

        }
    }
}