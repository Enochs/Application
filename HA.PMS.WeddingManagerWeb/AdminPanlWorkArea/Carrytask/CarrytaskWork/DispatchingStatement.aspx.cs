using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class DispatchingStatement : SystemPage
    {
        /// <summary>
        /// 结算表
        /// </summary>
        Statement ObjStatementBLL = new Statement();
        int DispatchingID;

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>  
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToString().ToInt32();
            if (!IsPostBack)
            {
                var DataList = ObjStatementBLL.GetByDispatchingID(DispatchingID);
                repStatement.DataBind(DataList);
            }
        }
        #endregion
    }
}