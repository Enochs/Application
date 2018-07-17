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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

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



        /// <summary>
        /// 获取隐藏熟悉
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetRemoveClass(object CustomerID)
        {
            var ObjItem = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
            if (ObjItem == null)
            {
                return string.Empty;
            }
            if (ObjItem.IsDispatching >= 2)
            {
                return "RemoveClass";
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();
            //状态
            objParmList.Add("State", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingChecksQuotedPrice + "," + (int)CustomerStates.DoingCarrytask, NSqlTypes.IN);
            //objParmList.Add("State", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingQuotedPrice, NSqlTypes.IN);
            //责任人
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

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

        }



        /// <summary>
        /// 获取隐藏属性 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedRemoveClass(object CustomerID)
        {
            if (CustomerID != null)
            {
                if (CustomerID.ToString() != string.Empty)
                {
                    var ObjQuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
                    if (ObjQuotedModel != null)
                    {
                        if (ObjQuotedModel.IsChecks.Value)
                        {
                            return "RemoveClass";
                        }

                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

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

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected string StatuHideViewInviteInfo()
        {
            return !new Employee().IsManager(User.Identity.Name.ToInt32()) ? "style='display:none'" : string.Empty;
        }


        public decimal GetRealityAmount(object Source)
        {
            int OrderID = Source.ToString().ToInt32();
            var DataList = ObjPlanBLL.GetByOrderID(OrderID);
            return DataList.Sum(C => C.RealityAmount).ToString().ToDecimal();
        }


        public string StatusHideOrder(object Source)
        {
            var Model = ObjOrderBLL.GetByID(Source.ToString().ToInt32());
            if (Model.EmployeeID != User.Identity.Name.ToInt32())
            {
                return "style='display:none;'";
            }
            return "";
        }
    }
}