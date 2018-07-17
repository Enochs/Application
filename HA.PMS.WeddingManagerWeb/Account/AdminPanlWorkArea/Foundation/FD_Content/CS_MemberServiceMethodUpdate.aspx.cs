using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class CS_MemberServiceMethodUpdate : System.Web.UI.Page
    {
        //CS_MemberServiceMethodResult
        MemberServiceMethodResult objMemberServiceMethodResultBLL = new MemberServiceMethodResult();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {
            CS_MemberServiceMethodResult assess = objMemberServiceMethodResultBLL.GetByID(int.Parse(Request["Key"]));
            txtTempLeate.Text = assess.SpTemplete;
            txtTyperName.Text = assess.ServiceName;
        }


        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            CS_MemberServiceMethodResult assess = objMemberServiceMethodResultBLL.GetByID(int.Parse(Request["Key"]));
            assess.SpTemplete = txtTempLeate.Text;
            assess.ServiceName = txtTyperName.Text;
            objMemberServiceMethodResultBLL.Update(assess);
            JavaScriptTools.AlertAndClosefancybox("保存成功！", Page);
        }
    }
}