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

        Employee ObjEmployeeBLL = new Employee();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
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
            objParmList.Add("CheckState", "1,2", NSqlTypes.IN);

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
            objParmList.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);

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


        #region 只有管理层才能审核
        /// <summary>
        /// 审核前提
        /// </summary> 
        public string IsVisible()
        {
            int EmployeeID = User.Identity.Name.ToInt32();
            var EmployeeModel = ObjEmployeeBLL.GetByID(EmployeeID);
            if (EmployeeModel.EmployeeTypeID == 3)          //普通员工不能审核
            {
                return "style='display:none;'";
            }
            return "";

        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion

    }
}