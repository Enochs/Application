using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using System.Data.Objects;
using System.Web.UI.HtmlControls;
using HA.PMS.BLLAssmblly.Report;

///预定成功的客户 
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class SucessOrder : SystemPage
    {
        Customers ObjCustomerBLL = new Customers();
        Order ObjOrderBLL = new Order();
        Employee ObjEmployeeBLL = new Employee();
        MissionManager ObjMissManagerBLL = new MissionManager();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        QuotedCollectionsPlan ObjPlanBLL = new QuotedCollectionsPlan();

        #region 页面加载
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 获取订单金额
        /// <summary>
        /// 订单金额
        /// </summary>
        public string GetQuotedFinishMoney(object Key)
        {
            var ObjModel = ObjQuotedPriceBLL.GetByOrderId(Key.ToString().ToInt32());
            if (ObjModel != null)
            {
                return ObjModel.FinishAmount.ToString();
            }
            else
            {
                return "暂未填写";
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();
            //状态
            objParmList.Add("State", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingChecksQuotedPrice + "," + (int)CustomerStates.DoingCarrytask, NSqlTypes.IN);
            //objParmList.Add("State", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingQuotedPrice, NSqlTypes.IN);
            //顾问
            MyManager.GetEmployeePar(objParmList);
            //婚期
            if (ddlTimeSpan.SelectedValue.ToInt32() == 0)
            {
                objParmList.Add("PartyDate", MainDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            //订单时间
            else if (ddlTimeSpan.SelectedValue.ToInt32() == 1)
            {
                objParmList.Add("LastFollowDate", MainDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //新人姓名
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.LIKE);
            }

            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "LastFollowDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            //合计
            var ObjDataList = ObjOrderBLL.GetByWhereParameter(objParmList, "LastFollowDate", 1000000, 1, out SourceCount);
            GetMoneySumTotal(ObjDataList);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();



        }
        #endregion


        #region 合计
        /// <summary>
        /// 总的合计(本期)
        /// </summary>
        /// <param name="DataList"></param>

        public void GetMoneySumTotal(List<View_GetOrderCustomers> DataList)
        {
            lblSumRealityall.Text = ObjOrderBLL.GetAllRealityAmount(DataList);
        }
        #endregion


        #region 保存所有数据

        /// <summary>
        /// 保存所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {
            HiddenField ObjEmpLoyeeHide;
            HiddenField ObjCustomerHide;
            HtmlInputText ObjEmpLoyeeName;


            for (int i = 0; i < repCustomer.Items.Count; i++)
            {

                ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");
                ObjEmpLoyeeName = (HtmlInputText)repCustomer.Items[i].FindControl("txtEmpLoyee");
                if (ObjEmpLoyeeName.Value != string.Empty)
                {


                    ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");

                    ///录入报价单基础
                    FL_QuotedPrice ObjQuotedPriceModel = new FL_QuotedPrice();

                    //判断是否有报价单
                    ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(ObjCustomerHide.Value.ToInt32());
                    if (ObjQuotedPriceModel == null)
                    {
                        ObjQuotedPriceModel = new FL_QuotedPrice();
                        ObjQuotedPriceModel.CustomerID = ObjCustomerHide.Value.ToInt32();
                        ObjQuotedPriceModel.IsDelete = false;
                        ObjQuotedPriceModel.OrderID = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).OrderID;// Request["OrderID"].ToInt32();
                        ObjQuotedPriceModel.CategoryName = "开始制作报价单";
                        ObjQuotedPriceModel.EmpLoyeeID = ObjEmpLoyeeHide.Value.ToInt32();
                        ObjQuotedPriceModel.IsChecks = false;
                        ObjQuotedPriceModel.CreateDate = DateTime.Now;
                        ObjQuotedPriceModel.IsFirstCreate = false;
                        ObjQuotedPriceModel.OrderCoder = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).OrderCoder;
                        ObjQuotedPriceModel.ParentQuotedID = 0;
                        ObjQuotedPriceModel.IsDispatching = 0;
                        ObjQuotedPriceModel.FinishAmount = 0;
                        ObjQuotedPriceModel.NextFlowDate = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).LastFollowDate;
                        ObjQuotedPriceModel.StarDispatching = false;
                        ObjQuotedPriceModel.FileCheck = 1;

                        ObjQuotedPriceModel.EarnestMoney = 0;
                        var ObjQuteKey = ObjQuotedPriceBLL.Insert(ObjQuotedPriceModel);
                        var ObjUpdateModel = ObjCustomerBLL.GetByID(ObjQuotedPriceModel.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.SucessOrder;//开始制作报价单

                        ObjCustomerBLL.Update(ObjUpdateModel);


                        ///修改统计信息s
                        Report ObjReportBLL = new Report();
                        SS_Report ObjReportModel = new SS_Report();
                        ObjReportModel = ObjReportBLL.GetByCustomerID(ObjQuotedPriceModel.CustomerID.Value, ObjQuotedPriceModel.EmpLoyeeID);

                        ObjReportModel.QuotedCreateDate = DateTime.Now;
                        ObjReportModel.QuotedEmployee = ObjQuotedPriceModel.EmpLoyeeID;
                        ObjReportBLL.Update(ObjReportModel);
                        ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.Value, 1, (int)MissionTypes.Quoted, DateTime.Now, ObjQuotedPriceModel.EmpLoyeeID.Value, "?QuotedID=" + ObjQuotedPriceModel.QuotedID + "&OrderID=" + ObjQuotedPriceModel.OrderID + "&FlowOrder=1&CustomerID=" + ObjQuotedPriceModel.CustomerID + "&PartyDate=" + ObjCustomerBLL.GetByID(ObjQuotedPriceModel.CustomerID).PartyDate.ToString(), "QuotedPriceWorkPanel", ObjQuotedPriceModel.EmpLoyeeID.Value, ObjQuotedPriceModel.QuotedID);
                    }
                    else
                    {

                        ObjQuotedPriceModel.CustomerID = ObjCustomerHide.Value.ToInt32();
                        ObjQuotedPriceModel.IsDelete = false;
                        ObjQuotedPriceModel.OrderID = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).OrderID;// Request["OrderID"].ToInt32();
                        ObjQuotedPriceModel.CategoryName = "开始制作报价单";
                        ObjQuotedPriceModel.EmpLoyeeID = ObjEmpLoyeeHide.Value.ToInt32();
                        ObjQuotedPriceModel.IsChecks = false;
                        ObjQuotedPriceModel.IsDispatching = 0;
                        // ObjQuotedPriceModel.CreateDate = DateTime.Now;
                        ObjQuotedPriceModel.IsFirstCreate = false;
                        ObjQuotedPriceModel.OrderCoder = ObjOrderBLL.GetbyCustomerID(ObjQuotedPriceModel.CustomerID.Value).OrderCoder;
                        ObjQuotedPriceModel.ParentQuotedID = 0;

                        ObjQuotedPriceModel.StarDispatching = false;
                        ObjQuotedPriceModel.FileCheck = 1;

                        var ObjQuteKey = ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                        var ObjUpdateModel = ObjCustomerBLL.GetByID(ObjQuotedPriceModel.CustomerID);
                        ObjUpdateModel.State = (int)CustomerStates.SucessOrder;//开始制作报价单
                        ObjCustomerBLL.Update(ObjUpdateModel);
                        ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.Value, 1, (int)MissionTypes.Quoted, DateTime.Now, ObjQuotedPriceModel.EmpLoyeeID.Value, "?QuotedID=" + ObjQuotedPriceModel.QuotedID + "&OrderID=" + ObjQuotedPriceModel.OrderID + "&FlowOrder=1&CustomerID=" + ObjQuotedPriceModel.CustomerID + "&PartyDate=" + ObjCustomerBLL.GetByID(ObjQuotedPriceModel.CustomerID).PartyDate.ToString(), "QuotedPriceWorkPanel", ObjQuotedPriceModel.EmpLoyeeID.Value, ObjQuotedPriceModel.QuotedID);
                    }

                }
            }
            BinderData();
            JavaScriptTools.AlertWindow("保存成功!", this.Page);
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页 上一页/下一页
        /// </summary>    
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>  
        protected void btnserch_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion

        #region 获取定金
        /// <summary>
        /// 定金
        /// </summary>
        public decimal GetRealityAmount(object Source)
        {
            int OrderID = Source.ToString().ToInt32();
            var m_plan = ObjPlanBLL.GetOrderDate(OrderID);
            if (m_plan != null)
            {
                return m_plan.RealityAmount.ToString().ToDecimal();
            }
            return 0;
        }
        #endregion



        decimal RealitAmount = 0;
        #region 会员标志显示
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            View_GetOrderCustomers Model = e.Item.DataItem as View_GetOrderCustomers;
            RealitAmount += GetRealityAmount(Model.OrderID);
            lblSumRealitypage.Text = RealitAmount.ToString();

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