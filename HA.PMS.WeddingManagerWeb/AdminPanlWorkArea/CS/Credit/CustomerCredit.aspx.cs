using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.CS;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit
{
    public partial class CustomerCredit : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }




        protected void BinderData(object sender, EventArgs e)
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int resourceCount = 0;
            List<PMSParameters> ObjparList = new List<PMSParameters>();
            //状态
            ObjparList.Add("State", 206 + "," + 208, NSqlTypes.IN);
            //新人姓名
            ObjparList.Add(CstmNameSelector.SelectedValue.ToInt32() == 1, "Bride", CstmNameSelector.Text.Trim(), NSqlTypes.LIKE);
            ObjparList.Add(CstmNameSelector.SelectedValue.ToInt32() != 1, "Groom", CstmNameSelector.Text.Trim(), NSqlTypes.LIKE);
            //婚期
            ObjparList.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            //联系电话
            ObjparList.Add(txtBrideCellPhone.Text != string.Empty, "BrideCellPhone", ddlHotel.SelectedItem.Text, NSqlTypes.Equal);
            //酒店
            ObjparList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);
            //身份证号码
            ObjparList.Add(txtCustomerKey.Text != string.Empty, "CustomerKeyID", txtCustomerKey.Text, NSqlTypes.LIKE);

            //渠道
            // paramsList.Add(DateRanger.IsNotBothEmpty, "Channel_LIKE");

            BLLAssmblly.Flow.Customers customersBLL = new BLLAssmblly.Flow.Customers();
            repCustomer.DataSource = customersBLL.GetByWhereParameter(ObjparList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out resourceCount);
            CtrPageIndex.RecordCount = resourceCount;
            repCustomer.DataBind();
        }


        /// <summary>
        /// 获取积分
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetPointtoCustomerID(object CustomerID)
        {
            CreditLog ObjCreditLogBLL = new CreditLog();
            return ObjCreditLogBLL.GetPointforCustomer(CustomerID.ToString().ToInt32()).ToString();

        }
    }
}