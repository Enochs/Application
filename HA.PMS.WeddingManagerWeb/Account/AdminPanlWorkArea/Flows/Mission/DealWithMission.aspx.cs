using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class DealWithMission : SystemPage
    {

        HA.PMS.BLLAssmblly.Flow.MissionDetailed ObjDetailsBLL = new BLLAssmblly.Flow.MissionDetailed();
        int DetailsID = 0;

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 点击提交功能
        /// <summary>
        /// 提交
        /// </summary> 
        protected void btnGetSave_Click(object sender, EventArgs e)
        {
            DetailsID = Request["DetailsID"].ToInt32();
            var Model = ObjDetailsBLL.GetByID(DetailsID);
            if (Model != null)
            {
                Model.FinishNode = txtFinishNode.Text.Trim().ToString();
                if (DateTime.Now.ToShortDateString().ToDateTime() >= Model.PlanDate)
                {
                    Model.MissionState = 3;
                }
                else if (DateTime.Now.ToShortDateString().ToDateTime() < Model.PlanDate)
                {
                    Model.MissionState = 1;
                }
                Model.FinishDate = DateTime.Now.ToShortDateString().ToDateTime();

                Model.IsOver = true;
                int result = ObjDetailsBLL.Update(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertAndClosefancybox("处理成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("处理失败,请稍候再试...", Page);
                }
            }
        }
        #endregion

        #region 点击取消
        /// <summary>
        /// 取消
        /// </summary>  
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertAndClosefancybox("是否确认关闭当前窗体？", Page);
        }
        #endregion
    }
}