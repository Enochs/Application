using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectCase : System.Web.UI.UserControl
    {
        TheCase ObjCaseBLL = new TheCase();

        int PlannerID = 0;

        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {
            PlannerID = Request["PlannerID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定
        /// </summary> 
        public void BinderData()
        {
            var DataList = ObjCaseBLL.GetByAll();
            rptCase.DataSource = DataList;
            rptCase.DataBind();
        }
        #endregion

        #region 点击确认选择

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptCase.Items.Count; i++)
            {
                var ModelItem = rptCase.Items[i];
                CheckBox check = ModelItem.FindControl("chkCaseName") as CheckBox;
                if (check.Checked == true)
                {
                    string chkCaseName = check.Text;
                    txtShowName.Text += chkCaseName.ToString();
                }
            }
        }
        #endregion
    }
}