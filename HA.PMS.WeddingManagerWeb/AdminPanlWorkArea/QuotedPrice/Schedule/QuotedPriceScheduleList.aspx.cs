using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.Schedule
{
    public partial class QuotedPriceScheduleList : SystemPage
    {

        QuotedPriceSchedule ObjScheduleBLL = new QuotedPriceSchedule();
        string OrderByColumnName = "ScheCreateDate";
        int SourceCount = 0;

        decimal guardPrice = 0;
        decimal payment = 0;
        decimal collectionAmount = 0;

        View_QuotedSchedule ObjDataModel = new View_QuotedSchedule();
        List<PMSParameters> pars = new List<PMSParameters>();

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// @author:wp
        /// @datetime:2019-08-31
        /// @desc: 预定婚期
        /// </summary>

        public void DataBinder()
        {

            //商家名称
            if (!string.IsNullOrEmpty(txtSupplierName.Text))
            {
                pars.Add("Name", txtSupplierName.Text.Trim(), NSqlTypes.LIKE);
            }

            //婚期查询
            //pars.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            // 按婚期、订单时间、预定时间 查询
            if (ddltimerType.SelectedValue != "-1" && DateRanger.IsNotBothEmpty)
            {
                pars.Add(ddltimerType.SelectedValue.ToString(), DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按新人姓名查询
            CstmNameSelector.AppandTo(pars);

            //推荐人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                pars.Add("ScheCreateEmployee", MyManager.SelectedValue, NSqlTypes.Equal);
            }

            var DataList = ObjScheduleBLL.GetScheduleByParameter(pars, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            rptSchedule.DataBind(DataList);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// @author:wp
        /// @datetime:2019-08-31
        /// @desc: 点击上一页 下一页 分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 数据绑定完成事件  计算合计  09.28
        /// <summary>
        /// @author:wp
        /// @datetime:2019-08-31
        /// @desc:数据绑定完成事件　计算合计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptSchedule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ObjDataModel = (View_QuotedSchedule)e.Item.DataItem;

            guardPrice += ObjDataModel.ScheOfferPrice == null ? 0 : ObjDataModel.ScheOfferPrice.Value;
            payment += ObjDataModel.ScheCooperatePrice == null ? 0 : ObjDataModel.ScheCooperatePrice.Value;
            collectionAmount += ObjDataModel.ScheDepositPrice == null ? 0 : ObjDataModel.ScheDepositPrice.Value;
            lblScheGuardianPrice.Text = guardPrice.ToString();
            lblSchePayMent.Text = payment.ToString();
            lblScheCollectionAmount.Text = collectionAmount.ToString();

            BinderSum();

        }
        #endregion

        #region 点击查询
        /// <summary>
        /// @author:wp
        /// @datetime:2019-08-31
        /// @desc: 查询
        /// </summary> 
        protected void btnLookFor_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion


        #region 获取本期的所有合计

        private void BinderSum()
        {
            var DataList = ObjScheduleBLL.GetScheduleByParameter(pars, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            //报价
            lblScheGuardianPriceAll.Text = DataList.Where(C => C.ScheOfferPrice != null).Sum(C => C.ScheOfferPrice).ToString().ToDecimal().ToString();  //报价
            //合作价
            lblSchePayMentAll.Text = DataList.Where(C => C.ScheCooperatePrice != null).Sum(C => C.ScheCooperatePrice).ToString().ToDecimal().ToString();  //合作价
            //定金
            lblScheCollectionAmountAll.Text = DataList.Where(C => C.ScheDepositPrice != null).Sum(C => C.ScheDepositPrice).ToString().ToDecimal().ToString();  //定金
        }
        #endregion
    }
}