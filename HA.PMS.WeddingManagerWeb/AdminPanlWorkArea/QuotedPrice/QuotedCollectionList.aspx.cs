using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedCollectionList : HA.PMS.Pages.SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        HA.PMS.BLLAssmblly.Flow.Order ObjOrderBLL = new BLLAssmblly.Flow.Order();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            QuotedCollectionsPlan objPlan = new QuotedCollectionsPlan();
            int SourceCount = 0;

            var objParmList = new List<PMSParameters>();

            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //收款日期
            if (ddltimerType.SelectedValue == "3" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("CollectionTime", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            // 新人姓名
            if (txtBridename.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtBridename.Text, NSqlTypes.ORLike);
            }

            //联系电话
            if (txtContactPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtContactPhone.Text, NSqlTypes.StringEquals);
            }

            //责任人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("CreateEmpLoyee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            else
            {
                objParmList.Add("CreateEmpLoyee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            }
            var ObjDataList = ObjQuotedCollectionsPlanBLL.GetCostPlanByWhere(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            this.repCustomer.DataBind(ObjDataList);
            lblSumneedMoneyfopage.Text = ObjDataList.Sum(C => C.RealityAmount).ToString();      //本页
            var DataList = ObjQuotedCollectionsPlanBLL.GetCostPlanByWhere(objParmList, "CreateDate", 100000, 1, out SourceCount).ToList();
            lblSumMoneyall.Text = DataList.Sum(C => C.RealityAmount).ToString();        //本期
            GetMoneySumTotal(DataList);
        }
        #endregion

        #region 分页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击查询
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 合计
        /// <summary>
        /// 总的合计(本日  本月 本年)
        /// </summary>
        /// <param name="DataList"></param>

        public void GetMoneySumTotal(List<View_GetCostPlan> DataList)
        {
            //var DataLists = ObjQuotedCollectionsPlanBLL.GetByAll();
            //string MinCollectionTime = DataList.Min(C => C.CollectionTime).GroupBy(C => C.QuotedID).ToString();
            lblMoneySumToday.Text = DataList.Where(C => C.CollectionTime != null && C.CollectionTime.Value.Date == DateTime.Now.Date && C.CollectionTime.Value.Month == DateTime.Now.Month && C.CollectionTime.Value.Year == DateTime.Now.Year).Sum(C => C.RealityAmount).ToString();
            lblMoneySumToMonth.Text = DataList.Where(C => C.CollectionTime != null && C.CollectionTime.Value.Month == DateTime.Now.Month && C.CollectionTime.Value.Year == DateTime.Now.Year).Sum(C => C.RealityAmount).ToString();
            lblMoneySumToYear.Text = DataList.Where(C => C.CollectionTime != null && C.CollectionTime.Value.Year == DateTime.Now.Year).Sum(C => C.RealityAmount).ToString();
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志显示
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