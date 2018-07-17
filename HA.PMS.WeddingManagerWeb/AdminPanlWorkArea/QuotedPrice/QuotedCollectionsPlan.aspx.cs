using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;
using HA.PMS.DataAssmblly;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedCollectionsPlan : HA.PMS.Pages.SystemPage
    {
        BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        string DateType = "";
        decimal SumMoney = 0;
        decimal SumNeedMoney = 0;
        decimal FinishMoney = 0;

        View_CustomerQuoted ObjDataModel = new View_CustomerQuoted();

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);

            }
        }
        #endregion

        #region 绑定数据方法
        /// <summary>
        /// 方法
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": CtrPageIndex.CurrentPageIndex = 1; break;
                }
            }

            List<PMSParameters> objParmList = new List<PMSParameters>();

            objParmList.Add("State", "7,29,10,20", NSqlTypes.NotIN);
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                this.MyManager.GetEmployeePar(objParmList, "EmployeeID");     //策划师
            }
            else
            {
                //objParmList.Add("EmployeeID", User.Identity.Name.ToInt32() + ",,int", NSqlTypes.Split);
                objParmList.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            }

            if (MyManager1.SelectedValue.ToInt32() > 0)     //统筹师
            {
                objParmList.Add("OrderEmployee", MyManager1.SelectedValue, NSqlTypes.IN);
            }
            //else
            //{
            //    //objParmList.Add("OrderEmployee", User.Identity.Name.ToInt32() + ",1,int", NSqlTypes.Split);         //注明，1 说明是最后一个
            //    objParmList.Add("OrderEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
            //}


            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);

            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                DateType = "PartyDate";
                objParmList.Add(DateType, DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按店时间查询
            if (ddltimerType.SelectedValue == "2" && DateRanger.IsNotBothEmpty)
            {
                DateType = "OrderCreateDate";
                objParmList.Add(DateType, DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }


            //按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //按新人姓名查询
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtContactMan.Text, NSqlTypes.LIKE);
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.ORLike);
                objParmList.Add("Groom", txtContactMan.Text, NSqlTypes.ORLike);
            }
            BinderSum(objParmList);
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            repCustomer.DataBind(DataList);
            if (DataList.Count == 0)
            {
                lblSumMoneyall.Text = "0.00";
                lblSumFinishMoneyall.Text = "0.00";
                lblSumNeedall.Text = "0.00";
            }
            CtrPageIndex.RecordCount = SourceCount;
            lblSumCustomer.Text = SourceCount.ToString() + "单";

            BindPagerSum();
        }
        #endregion

        #region 获取本期的所有合计

        private void BinderSum(object objParmList)
        {
            var FinishMoney = ObjQuotedPriceBLL.GetFinishMoneyByMonth(DateRanger.Start, DateRanger.End, MyManager.SelectedValue.ToInt32() > 0 ? MyManager.SelectedValue.ToInt32() : User.Identity.Name.ToInt32());
            int SourceCounts = 0;
            List<PMSParameters> parmsList = objParmList as List<PMSParameters>;
            parmsList = objParmList as List<PMSParameters>;
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(parmsList, "PartyDate", 100000, 1, out SourceCounts);

            //订单金额
            lblSumMoneyall.Text = DataList.Where(C => C.FinishAmount != null).Sum(C => C.FinishAmount).ToString().ToDecimal().ToString();  //已收款
            //已收款
            lblSumFinishMoneyall.Text = FinishMoney.ToString();
            //余款
            lblSumNeedall.Text = (lblSumMoneyall.Text.ToDecimal() - FinishMoney).ToString();
        }
        #endregion

        #region 数据绑定完成事件 会员标志
        protected void repCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            ObjDataModel = (View_CustomerQuoted)e.Item.DataItem;
           
            SumMoney += ObjDataModel.FinishAmount == null ? 0 : ObjDataModel.FinishAmount.Value;
            SumNeedMoney += GetOverFinishMoney(ObjDataModel.CustomerID).ToDecimal();
            FinishMoney += GetFinishMoney(ObjDataModel.CustomerID).ToDecimal();
            lblSumMoneyfopage.Text = SumMoney.ToString();
            lblSumneedMoneyfopage.Text = SumNeedMoney.ToString();
            lblFinishMoney.Text = FinishMoney.ToString();

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

        #region 获取本页合计
        /// <summary>
        /// 获取
        /// </summary>
        public void BindPagerSum()
        {
            //if (repCustomer.Items.Count > 0)
            //{
            //    SumMoney += ObjDataModel.FinishAmount.Value;
            //    SumNeedMoney += GetOverFinishMoney(ObjDataModel.CustomerID).ToDecimal();
            //    FinishMoney += GetFinishMoney(ObjDataModel.CustomerID).ToDecimal();
            //    lblSumMoneyfopage.Text = SumMoney.ToString();
            //    lblSumneedMoneyfopage.Text = SumNeedMoney.ToString();
            //    lblFinishMoney.Text = FinishMoney.ToString();
            //}
            //else
            if (repCustomer.Items.Count == 0)
            {
                lblSumMoneyfopage.Text = "0.00";
                lblSumneedMoneyfopage.Text = "0.00";
                lblFinishMoney.Text = "0.00";
            }
        }
        #endregion
    }
}