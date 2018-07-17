using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class QuptedCollectionsPlanLists : HA.PMS.Pages.SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        HA.PMS.BLLAssmblly.Flow.Order ObjOrderBLL = new BLLAssmblly.Flow.Order();
        HA.PMS.BLLAssmblly.Flow.Customers ObjCustomerBll = new BLLAssmblly.Flow.Customers();

        decimal QuotedFinishSum = 0;     //婚礼总金额
        decimal RealitySum = 0;          //收款金额
        decimal OverFinishSum = 0;       //余额

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BinderData();
            }

        }
        #endregion

        #region Biner 绑定数据
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

            //按录入时间查询
            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            // 新人姓名
            if (txtBridename.Text != string.Empty)
            {
                objParmList.Add("ContactMan", txtBridename.Text, NSqlTypes.LIKE);
            }
            // 联系电话
            if (txtContactPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtContactPhone.Text, NSqlTypes.StringEquals);
            }
            //责任人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("CreateEmpLoyee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);

            }


            var ObjDataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            this.repCustomer.DataBind(ObjDataList);

            GetPageMoneySum(ObjDataList);
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, "PartyDate", 10000, 1, out SourceCount);
            GetAllMoneySum(DataList);

        }
        #endregion

        #region 获取婚礼总金额
        /// <summary>
        /// 获取婚礼总金额
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedFinishMoney(object CustomerID)
        {
            var ReturnModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
            if (ReturnModel != null)
            {
                return ReturnModel.FinishAmount.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region 本页合计


        /// <summary>
        /// 获取本页各种合计
        /// </summary>
        public void GetPageMoneySum(List<View_CustomerQuoted> source)
        {
            //本页婚礼总金额
            double QuotedFinishSum = 0;

            //收款金额
            double RealitySum = 0;

            //本页余额
            double OverFinishSum = 0;

            foreach (var item in source)
            {

                QuotedFinishSum += GetQuotedFinishMoney(item.CustomerID).ToString().ToDouble(); //本页婚礼总金额
                RealitySum += GetFinishMoney(item.CustomerID).ToString().ToDouble();            //获取本页收款金额
                OverFinishSum += GetOverFinishMoney(item.CustomerID).ToString().ToDouble();     //本页婚礼余额
            }

            lblSumQuotedFinishfoPage.Text = QuotedFinishSum.ToString("f2");         //婚礼总金额
            lblSumRealityAmountPage.Text = RealitySum.ToString("f2");               //收款金额
            lblSumOverFinishPage.Text = OverFinishSum.ToString("f2");               //本页余额
        }
        #endregion

        #region 本期合计
        /// <summary>
        /// 获取本期金额
        /// </summary>
        public void GetAllMoneySum(List<View_CustomerQuoted> DataList)
        {

            QuotedFinishSum = ObjQuotedPriceBLL.GetFinishAmountSums(DataList).ToDecimal();           //婚礼总金额
            RealitySum = ObjQuotedPriceBLL.GetFinishMoneys(DataList).ToDecimal();                   //收款金额
            //OverFinishSum = ObjQuotedPriceBLL.GetOverMoneySums(DataList).ToDouble();               //余额
            OverFinishSum = (QuotedFinishSum - RealitySum).ToString().ToDecimal();
            lblSumQuotedFinishfoAll.Text = QuotedFinishSum.ToString("f2");  //婚礼总金额
            lblSumRealityAmountAll.Text = RealitySum.ToString("f2");        //收款金额
            lblSumOverFinishAll.Text = OverFinishSum.ToString("f2");        //余额

        }
        #endregion

        #region 翻页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击查询

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion




        #region 数据绑定执行事件  本页合计
        /// <summary>
        /// 本页合计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repCustomer_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            //本页婚礼总金额
            double QuotedFinishSum = 0;

            //收款金额
            double RealitySum = 0;

            //本页余额
            double OverFinishSum = 0;

            View_CustomerQuoted ObjItem = (View_CustomerQuoted)e.Item.DataItem;
            QuotedFinishSum += GetQuotedFinishMoney(ObjItem.CustomerID).ToString().ToDouble(); //本页婚礼总金额
            RealitySum += GetFinishMoney(ObjItem.CustomerID).ToString().ToDouble();            //获取本页收款金额
            OverFinishSum += GetOverFinishMoney(ObjItem.CustomerID).ToString().ToDouble();     //本页婚礼余额

            lblSumQuotedFinishfoPage.Text = QuotedFinishSum.ToString("f2");         //婚礼总金额
            lblSumRealityAmountPage.Text = RealitySum.ToString("f2");               //收款金额
            lblSumOverFinishPage.Text = OverFinishSum.ToString("f2");               //本页余额

        }
        #endregion

    }
}