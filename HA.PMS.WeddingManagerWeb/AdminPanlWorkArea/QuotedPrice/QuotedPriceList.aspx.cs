using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceList : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        Report ObjReportBLL = new Report();
        Order ObjOrderBLL = new Order();

        Customers ObjCustomerBLL = new Customers();

        string SortName = "CreateDate";

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
        /// 获取预算
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

        #region 获取订金
        /// <summary>
        /// 获取定金
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetPartyBudget(object CustomerID)
        {
            FL_Customers fL_Customers = new Customers().GetByID((CustomerID + string.Empty).ToInt32());
            return object.ReferenceEquals(fL_Customers, null) ? string.Empty : fL_Customers.PartyBudget.ToString();
        }
        #endregion

        #region 获取邀约人
        /// <summary>
        /// 获取邀约负责人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderEmployee(object CustomerID)
        {
            FL_Order fl_Order = new Order().GetbyCustomerID((CustomerID + string.Empty).ToInt32());
            if (!object.ReferenceEquals(fl_Order, null))
            {
                Sys_Employee sys_Employee = new Employee().GetByID(fl_Order.EmployeeID);
                return object.ReferenceEquals(sys_Employee, null) ? string.Empty : sys_Employee.EmployeeName;
            }
            return string.Empty;
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> objParmList = new List<PMSParameters>();

            objParmList.Add("State", "206,29,20,208", NSqlTypes.NotIN);
            //新娘
            if (CstmNameSelector.SelectedValue.ToInt32() == 1 && CstmNameSelector.Text != "")
            {
                objParmList.Add("Bride", CstmNameSelector.Text, NSqlTypes.LIKE);
            }
            //新郎
            else if (CstmNameSelector.SelectedValue.ToInt32() == 2 && CstmNameSelector.Text != "")
            {
                objParmList.Add("Groom", CstmNameSelector.Text, NSqlTypes.LIKE);
            }

            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
            objParmList.Add("IsDelete", false, NSqlTypes.Bit);


            //责任人
            MyManager1.GetEmployeePar(objParmList);


            //按婚期、订单时间、签约时间 查询
            if (ddltimerType.SelectedValue != "-1" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add(ddltimerType.SelectedValue.ToString(), DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

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

            //按婚期 订单排序
            SortName = ddlSort.SelectedValue.ToString();

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, SortName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            foreach (var item in DataList)
            {
                item.OrderNextFollowDate = item.QuotedCreateDate.Value.AddDays(15);
            }

            //将状态为未跟单或者到店的客户状态修改为成功预定
            var StateList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, SortName, 2000, CtrPageIndex.CurrentPageIndex, out SourceCount).Where(C => C.State == 8 || C.State == 9).ToList();
            foreach (var item in StateList)
            {
                //修改客户信息 (状态)
                FL_Customers ObjCustomerModel = ObjCustomerBLL.GetByID(item.CustomerID);
                ObjCustomerModel.State = 13;
                ObjCustomerBLL.Update(ObjCustomerModel);
            }

            repCustomer.DataBind(DataList); //ObjInvtieBLL.GetInviteCustomerByStateIndex(isAdd, tlCustomerID, GetWhereParList, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            lblTotalSums.Text = "客户数量:" + SourceCount.ToString();
        }
        #endregion

        #region 获取订单总金额
        /// <summary>
        /// 根据QuotedID获取订单金额
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>

        public string GetItemAmount(object Source)
        {
            int QuotedID = Source.ToString().ToInt32();
            decimal ItemAmout = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Where(C => C.IsFinishMake == 1 && C.ParentCategoryID == 0 && C.IsFirstMake >= 1).Sum(C => C.ItemAmount).ToString().ToDecimal();      //变更单产品各项价格之和
            ItemAmout += ObjQuotedPriceBLL.GetByID(QuotedID).FinishAmount.ToString().ToDecimal();             //报价单的价格
            return ItemAmout.ToString();
        }
        #endregion

        #region 变色 提示
        /// <summary>
        /// 变色
        /// </summary>
        public string IsChange(object Source, object Source1)
        {
            if (Source != null)
            {
                DateTime date = Source.ToString().ToDateTime();
                bool IsCheck = Source1.ToString().ToBool();
                if (date < DateTime.Now && IsCheck == false)       // 当前时间大于提案时间（过了提案时间 并且没签约就变色）
                {
                    return "style='color:Red;'";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        #endregion

        #region 保存
        /// <summary>
        /// 修改提案时间
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repCustomer_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {

                int OrderID = e.CommandArgument.ToString().ToInt32();

                TextBox txtFollowDate = e.Item.FindControl("txtOrderFollowDate") as TextBox;
                var Model = ObjOrderBLL.GetByID(OrderID);
                Model.NextFlowDate = txtFollowDate.Text.Trim().ToDateTime();
                int result = ObjOrderBLL.Update(Model);

                TextBox txtNextFollowDate = e.Item.FindControl("txtNextFollowDate") as TextBox;
                var ReportModel = ObjReportBLL.GetByCustomerID(Model.CustomerID.ToString().ToInt32());
                ReportModel.QuotedCreateDate = txtNextFollowDate.Text.Trim().ToDateTime();
                result += ObjReportBLL.Updates(ReportModel);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("修改失败,请稍候再试...", Page);
                }
            }
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomerBLL.GetByID(CustomerID);
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