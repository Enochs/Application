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
using Insus.NET;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class QuptedCollectionsPlanList : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        HA.PMS.BLLAssmblly.Flow.Order ObjOrderBLL = new BLLAssmblly.Flow.Order();
        HA.PMS.BLLAssmblly.Flow.Customers ObjCustomerBll = new BLLAssmblly.Flow.Customers();

        double QuotedFinishSum = 0;     //婚礼总金额
        double RealitySum = 0;          //收款金额
        double OverFinishSum = 0;       //余额
        List<View_GetCostPlan> ObjDataList = null;
        List<View_GetCostPlan> ObjAllDataList = null;
        string OrderByName = "PartyDate";
        int SourceCount = 0;
        List<PMSParameters> objParmList = new List<PMSParameters>();


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
            Repeater1.Visible = false;
            Repeater1.DataSource = ObjQuotedPriceBLL.GetByAll().Take(1).ToList();
            Repeater1.DataBind();
        }
        #endregion

        #region 获取是否锁定
        /// <summary>
        /// 获取是否锁定
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetLock(object OrderID)
        {

            var ObjItems = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID.ToString().ToInt32());
            if (ObjItems.Count > 0)
            {
                string ReturnValue = "True";
                foreach (var ObjItem in ObjItems)
                {
                    if (ObjItem.RowLock == false)
                    {
                        return "False";
                    }
                }
                return ReturnValue;
            }
            else
            {
                return "True";
            }
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
        public void GetPageMoneySum(List<View_GetCostPlan> source)
        {
            if (source != null)
            {
                //收款金额
                double RealitySum = source.Sum(C => C.RealityAmount).ToString().ToDouble();

                lblSumRealityAmountPage.Text = RealitySum.ToString("f2");               //收款金额
            }
        }
        #endregion

        #region 本期合计
        /// <summary>
        /// 获取本期金额
        /// </summary>
        public void GetAllMoneySum(List<PMSParameters> parm, int SourceCount)
        {
            int count = 0;
            var ObjDataList = ObjQuotedCollectionsPlanBLL.GetCostPlanByWhere(parm, "CreateDate", SourceCount, 1, out count);
            RealitySum = ObjDataList.Sum(C => C.RealityAmount).ToString().ToDouble();
            lblSumRealityAmountAll.Text = RealitySum.ToString("f2");        //收款金额

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

        #region 导出Excel


        protected void btnExport_Click(object sender, EventArgs e)
        {

            ObjAllDataList = ObjQuotedCollectionsPlanBLL.GetCostPlanByWhere(GetWhere(objParmList), OrderByName, 10000, 1, out SourceCount);
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                var ObjDataItem = Repeater1.Items;
                Repeater RptData = ObjDataItem[i].FindControl("repCustomer") as Repeater;
                RptData.DataBind(ObjAllDataList);
            }

            Repeater1.Visible = true;


            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=收款管理" + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.Repeater1.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region 内部Repeater数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater RptData = e.Item.FindControl("repCustomer") as Repeater;
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            QuotedCollectionsPlan objPlan = new QuotedCollectionsPlan();

            int SourceCount = 0;
            //List<PMSParameters> objParmList = new List<PMSParameters>();

            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "PartyDate";
            }

            //按录入时间查询
            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "CreateDate";
            }

            //收款日期
            if (ddltimerType.SelectedValue == "3" && DateRanger.IsNotBothEmpty)
            {
                objParmList.Add("CollectionTime", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "CollectionTime";
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

            ObjDataList = ObjQuotedCollectionsPlanBLL.GetCostPlanByWhere(objParmList, OrderByName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataBind(ObjDataList);

            GetPageMoneySum(ObjDataList);
            GetAllMoneySum(objParmList, SourceCount);
        }
        #endregion

        #region 获取联系人


        public string GetContactMan(object Source)
        {
            if (Source != null)
            {
                string ContactMan = Source.ToString();
                if (ContactMan.ToString().Length > 8)
                {
                    return ContactMan.ToString().Substring(0, 6) + "...";
                }
                else
                {
                    return ContactMan.ToString();
                }
            }
            return "";
        }
        #endregion

        #region 获取条件方法 GetWhere

        public List<PMSParameters> GetWhere(List<PMSParameters> Pars)
        {
            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                Pars.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "PartyDate";
            }

            //按录入时间查询
            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                Pars.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "CreateDate";
            }

            //收款日期
            if (ddltimerType.SelectedValue == "3" && DateRanger.IsNotBothEmpty)
            {
                Pars.Add("CollectionTime", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                OrderByName = "CollectionTime";
            }

            // 新人姓名
            if (txtBridename.Text != string.Empty)
            {
                Pars.Add("ContactMan", txtBridename.Text, NSqlTypes.LIKE);
            }
            // 联系电话
            if (txtContactPhone.Text != string.Empty)
            {
                Pars.Add("ContactPhone", txtContactPhone.Text, NSqlTypes.StringEquals);
            }
            //责任人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                Pars.Add("CreateEmpLoyee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }
            return Pars;
        }
        #endregion
    }
}