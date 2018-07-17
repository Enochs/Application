using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member
{
    public partial class CustomerList : SystemPage
    {

        /// <summary>
        /// 订单管理
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 策划报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new HA.PMS.BLLAssmblly.Flow.QuotedPrice();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);

            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            #region 查询参数
            List<PMSParameters> ObjParList = new List<PMSParameters>();

            //新人姓名
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtGroom.Text), "Bride", txtGroom.Text.Trim(), NSqlTypes.LIKE);
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtGroom.Text), "Groom", txtGroom.Text.Trim(), NSqlTypes.ORLike);
            //新人手机
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtGroomCellPhone.Text), "BrideCellPhone", txtGroomCellPhone.Text.Trim(), NSqlTypes.LIKE);

            //婚期
            ObjParList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);

            //责任人
            if (MyManager1.SelectedValue.ToInt32() > 0)
            {
                ObjParList.Add("CustomerID", MyManager1.SelectedValue, NSqlTypes.Equal);
            }

            //酒店
            if (ddlHotel.SelectedValue.ToInt32() > 0)
            {
                ObjParList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);
            }

            //订单日期
            if (DateRanger2.IsNotBothEmpty)
            {
                ObjParList.Add("QuotedCreateDate", DateRanger2.StartoEnd, NSqlTypes.DateBetween);
            }

            //状态 已完成
            ObjParList.Add("State", 206 + "," + 208, NSqlTypes.IN);

            #endregion

            #region 分页绑定
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;

            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(ObjParList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            lblCustomerCount.Text = "客户数量：" + SourceCount.ToString(); ;
            rptCustomer.DataSource = DataList;
            rptCustomer.DataBind();

            #endregion
        }

        #region 获取消费金额
        /// <summary>
        /// 根据OrderID获取消费金额
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>

        public decimal GetRealityAmount(object Source)
        {
            QuotedCollectionsPlan ObjPlanBLL = new QuotedCollectionsPlan();
            int OrderID = Source.ToString().ToInt32();
            var DataList = ObjPlanBLL.GetByOrderID(OrderID);
            return DataList.Sum(C => C.RealityAmount).ToString().ToDecimal();
        }
        #endregion

    }
}