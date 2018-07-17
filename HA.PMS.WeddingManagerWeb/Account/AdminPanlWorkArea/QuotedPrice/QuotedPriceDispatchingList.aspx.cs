using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceDispatchingList : SystemPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["QuotedID"].ToInt32() > 0)
                {
                    new MissionDetailed().UpdateforFlow((int)MissionTypes.Quoted, Request["QuotedID"].ToInt32(), User.Identity.Name.ToInt32());
                }
                BinderData(sender, e);
            }
        }
        #endregion

        #region 获取预算
        /// <summary>
        /// 获取预算 销售总价
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetFinishMoney(object OrderID)
        {

            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();

            var FinishMoney = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID.ToString().ToInt32()).Sum(C => C.RealityAmount);
            return FinishMoney.ToString();
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> objParmList = new List<PMSParameters>();

            //状态  报价单审核中
            objParmList.Add("State", 15, NSqlTypes.Equal);
            objParmList.Add("IsChecks", 1, NSqlTypes.Bit);
            objParmList.Add("IsDispatching", 4, NSqlTypes.Equal);

            //新娘
            if (CstmNameSelector.SelectedValue.ToInt32() == 1)
            {
                objParmList.Add("Bride", CstmNameSelector.Text, NSqlTypes.LIKE);
            }
            //新郎
            else if (CstmNameSelector.SelectedValue.ToInt32() == 2)
            {
                objParmList.Add("Groom", CstmNameSelector.Text, NSqlTypes.LIKE);
            }

            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);

            //责任人
            MyManager1.GetEmployeePar(objParmList);

            //按婚期查询
            objParmList.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            //酒店
            if (ddlHotel.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);
            }
            objParmList.Add("Expr1", false, NSqlTypes.Equal);
            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.QuotedPrice().GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount)); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            lblTotalSums.Text = "客户数量:" + SourceCount.ToString();
        }
        #endregion
    }
}