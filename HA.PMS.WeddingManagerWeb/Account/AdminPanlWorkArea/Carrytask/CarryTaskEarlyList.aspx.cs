using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarryTaskEarlyList : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        string OrderByName = "PartyDate";
        int SourceCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> pars = new List<PMSParameters>();

            pars.Add("EarlyEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);

            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(pars, OrderByName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            repCustomer.DataBind(DataList);
        }
        #endregion

        #region 绑定事件  保存前期设计
        /// <summary>
        /// 选择前期设计
        /// </summary>
        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                int QuotedID = e.CommandArgument.ToString().ToInt32();
                HiddenField HideEmployeeID = e.Item.FindControl("hideEmpLoyeeID") as HiddenField;
                int EmployeeID = HideEmployeeID.Value.ToString().ToInt32();
                var Model = ObjQuotedPriceBLL.GetByID(QuotedID);
                Model.EarlyEmployee = EmployeeID;
                Model.EarlyState = 1;
                int result = ObjQuotedPriceBLL.Update(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("改派成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("改派失败,请稍候再试...", Page);
                }
            }
        }
        #endregion


        EarlyDesigner ObjEarlyBLL = new EarlyDesigner();
        public string ShowOrHide(object Source, int Type)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjEarlyBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                if (Type == 1)
                {
                    if (Model.IsOver == true)
                    {
                        return "style='display:none;'";
                    }
                }

                if (Type == 2)
                {
                    if (Model.IsOver == null)
                    {
                        return "style='display:none;'";
                    }
                }
            }
            else
            {
                if (Type == 2)
                {
                    return "style='display:none;'";
                }
            }
            return "";
        }
    }
}