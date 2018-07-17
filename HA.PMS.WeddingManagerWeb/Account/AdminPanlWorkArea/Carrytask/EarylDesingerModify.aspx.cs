using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class EarylDesingerModify : SystemPage
    {
        EarlyDesigner ObjEarlyDesignerBLL = new EarlyDesigner();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        int QuotedID = 0;
        int CustomerID = 0;
        string Type = "";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            var Model = ObjEarlyDesignerBLL.GetByCustomerID(CustomerID);
            Type = Request["Type"].ToString();
            if (!IsPostBack)
            {
                QuotedID = Request["QuotedID"].ToInt32();
                if (Type == "Look")
                {
                    txtContent.Enabled = false;
                    txtContent.Text = Model.Content.ToString();
                }
                else
                {
                    txtContent.Enabled = true;
                }
            }
        }
        #endregion

        #region 保存功能
        /// <summary>
        /// 点击保存
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var QuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            FL_EarlyDesigner ObjDesingerModel = new FL_EarlyDesigner();
            ObjDesingerModel.Content = txtContent.Text.Trim().ToString();
            ObjDesingerModel.CustomerID = QuotedModel.CustomerID;
            ObjDesingerModel.OrderID = QuotedModel.OrderID;
            ObjDesingerModel.QuotedID = QuotedID;
            ObjDesingerModel.CreateDate = DateTime.Now;
            ObjDesingerModel.CreateEmployeeID = User.Identity.Name.ToInt32();
            ObjDesingerModel.IsOver = true;

            int result = ObjEarlyDesignerBLL.Insert(ObjDesingerModel);
            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndLocation("保存成功", "CarryTaskEarlyList.aspx", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("保存失败,请稍候再试...", Page);
            }
        }
        #endregion
    }
}