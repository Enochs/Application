using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Report;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit
{
    public partial class QuotedPriceFinishNextPage : SystemPage
    {


        /// <summary>
        /// 报价单类别业务逻辑
        /// </summary>
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        Employee ObjEmployeeBLL = new Employee();

        Celebration ObjCelebrationBLL = new Celebration();

        CelebrationProductItem ObjItemBLL = new CelebrationProductItem();


        QuotedPriceItems ObjQuotedPriceItemBLL = new QuotedPriceItems();

        /// <summary>
        /// 满意度调查
        /// </summary>
        DegreeOfSatisfaction ObjDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        /// <summary>
        /// 报价单基础表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();
        //客户ID
        int CustomersID = 0;
        //坤ID
        int QuotedID = 0;
        //类别ID
        int CategoryID = 0;

        int OrderID = 0;

        Dispatching ObjDispatchingBLL = new Dispatching();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                hideMissionManager.Value = User.Identity.Name;
                Text1.Text = GetEmployeeName(User.Identity.Name.ToInt32());
                txtMissionManager.Value = ObjEmployeeBLL.GetByID(hideMissionManager.Value.ToInt32()).EmployeeName;
            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            // Dispatching ObjDispatchingBLL = new Dispatching();
            //var ObjModel=ObjQuotedPrice.GetByID(Request["QuotedID"].ToInt32());
            //ObjModel.MissionManagerEmployee = hideMissionManager.Value.ToInt32();
            //ObjQuotedPrice.Update(ObjModel);
            QuotedID = Request["QuotedID"].ToInt32();
            var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            var ObjCustomerUpdateModel = ObjCustomersBLL.GetByID(ObjQuotedModel.CustomerID);
            ObjCustomerUpdateModel.State = (int)CustomerStates.NewCarrytask;    //修改新人状态
            ObjCustomersBLL.Update(ObjCustomerUpdateModel);

            if (hideEmployeeID.Value.ToInt32() > 0)
            {
                CustomersID = Request["CustomersID"].ToInt32();
                OrderID = Request["OrderID"].ToInt32();

                //可以开始调查

                MissionManager ObjMissManagerBLL = new MissionManager();
                var UpdateModel = ObjDispatchingBLL.GetByID(InsertDispatching());
                if (ObjQuotedModel.Dispatching == 0)
                {
                    ObjQuotedModel.Dispatching = UpdateModel.DispatchingID;
                    ObjQuotedModel.StarDispatching = true;
                    ObjQuotedModel.MissionManagerEmployee = hideMissionManager.Value.ToInt32();
                    ObjQuotedPriceBLL.Update(ObjQuotedModel);
                    CreateDispatching(UpdateModel.DispatchingID);
                    JavaScriptTools.AlertWindow("保存成功!", Page);
                    btnSaveChange.Visible = false;
                }

                string NodeKey = "?StateKey=1&New=1&DispatchingID=" + UpdateModel.DispatchingID + "&CustomerID=" + ObjQuotedModel.CustomerID + "&OrderID=" + ObjQuotedModel.OrderID + "&NeedPopu=1";
                ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedModel.CustomerID.Value, 1, (int)MissionTypes.Dispatching, DateTime.Now, UpdateModel.EmployeeID.Value, NodeKey, MissionChannel.Quoted, UpdateModel.EmployeeID.Value, UpdateModel.DispatchingID);

                //修改统计信息
                Report ObjReportBLL = new Report();
                SS_Report ObjReportModel = new SS_Report();
                ObjReportModel = ObjReportBLL.GetByCustomerID(CustomersID, ObjQuotedModel.EmpLoyeeID.Value);
                ObjReportModel.State = ObjCustomerUpdateModel.State;
                ObjReportModel.WorkCreateDate = DateTime.Now;
                ObjReportModel.WorkEmployee = UpdateModel.EmployeeID;
                ObjReportBLL.Update(ObjReportModel);
                JavaScriptTools.CloseWindow("总派工任务已经下达!", Page);
                //DoingCarrytask
            }
        }


        /// <summary>
        /// 创建总派工
        /// </summary> 
        /// <returns></returns>
        private int InsertDispatching()
        {
            FL_Dispatching ObjDispathingModel = new FL_Dispatching();
            var ObjCelModel = ObjCelebrationBLL.GetByQuotedID(Request["QuotedID"].ToInt32());
            ObjDispathingModel = ObjDispatchingBLL.GetByCelebrationID(ObjCelModel.CelebrationID);
            if (ObjDispathingModel == null)
            {
                ObjDispathingModel = new FL_Dispatching();
                ObjDispathingModel.QuotedID = QuotedID;
                ObjDispathingModel.OrderID = OrderID;
                ObjDispathingModel.Isover = false;
                ObjDispathingModel.IsBegin = false;
                ObjDispathingModel.IsAppraise = false;
                ObjDispathingModel.AppraiseOver = false;
                ObjDispathingModel.UpdateDate = DateTime.Now;
                ObjDispathingModel.CreateDate = DateTime.Now;
                ObjDispathingModel.EmployeeID = hideEmployeeID.Value.ToInt32();
                ObjDispathingModel.ParentDispatchingID = Request["ParentDispatchingID"].ToInt32();
                ObjDispathingModel.CustomerID = Request["CustomerID"].ToInt32();
                if (Request["Change"] != null)
                {
                    ObjDispathingModel.ParentDispatchingID = ObjQuotedPriceBLL.GetByID(Request["Change"].ToString().ToInt32()).Dispatching;
                }
                ObjDispathingModel.CelebrationID = ObjItemBLL.GetByQuotedID(Request["QuotedID"].ToInt32(), 1).First().CelebrationID;
                ObjDispathingModel.QuotedEmpLoyee = ObjCelModel.QuotedEmpLoyee;
                int result = ObjDispatchingBLL.Insert(ObjDispathingModel);
                return result;
            }
            else
            {
                return ObjDispathingModel.DispatchingID;

            }

        }



        #region 派工任务
        /// <summary>
        /// 开始创建派工任务
        /// </summary>
        private void CreateDispatching(int DisID)
        {
            var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DisID);
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


            #region 财务成本明细
            //财务成本主体
            Cost ObjCostBLL = new Cost();

            //财务成本明细
            OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();

            FL_OrderfinalCost ObjFinalCostModel;


            FL_Cost ObjCostModel = new FL_Cost();
            ObjCostModel.CustomerID = Request["CustomersID"].ToInt32(); ;
            ObjCostModel.OrderID = OrderID;
            ObjCostModel.IsLock = false;
            ObjCostModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
            ObjCostModel.TotalAmount = ObjQuotedPriceBLL.GetByID(QuotedID).FinishAmount;
            ObjCostModel.CreateDate = DateTime.Now;
            ObjCostModel.IsLock = false;
            int CostKey = ObjCostBLL.Insert(ObjCostModel);


            #endregion



            int ParentDispatchingID = 0;

            FL_ProductforDispatching ObjCategoryforDispatching = new FL_ProductforDispatching();
            //添加派工大类

            //一级类别
            var ObjChangeFirstList = ObjQuotedPriceItemBLL.GetByQuotedID(QuotedID, 1);

            //二级类别
            var ObjChangeSecondList = ObjQuotedPriceItemBLL.GetByQuotedID(QuotedID, 2);

            //三级产品类别
            var ObjProductList = ObjQuotedPriceItemBLL.GetByQuotedID(QuotedID, 3);

            foreach (var ObjFirstCategory in ObjChangeFirstList)
            {

                ObjCategoryforDispatching = new FL_ProductforDispatching();
                ObjCategoryforDispatching.DispatchingID = ObjDispatchingModel.DispatchingID;
                ObjCategoryforDispatching.Unit = ObjFirstCategory.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjFirstCategory.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjFirstCategory.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjFirstCategory.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjFirstCategory.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjFirstCategory.Quantity.ToString().ToInt32();
                ObjCategoryforDispatching.Subtotal = ObjFirstCategory.Subtotal;
                ObjCategoryforDispatching.Remark = ObjFirstCategory.Remark;
                ObjCategoryforDispatching.ParentDispatchingID = ObjDispatchingModel.ParentDispatchingID;
                ObjCategoryforDispatching.CategoryName = ObjFirstCategory.CategoryName;
                ObjCategoryforDispatching.ParentCategoryID = ObjFirstCategory.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjFirstCategory.CategoryName;
                ObjCategoryforDispatching.PurchasePrice = ObjFirstCategory.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjFirstCategory.Productproperty;
                ObjCategoryforDispatching.RowType = ObjFirstCategory.RowType;
                ObjCategoryforDispatching.CreateEmployee = 0;
                ObjCategoryforDispatching.CategoryID = ObjFirstCategory.CategoryID;
                ObjCategoryforDispatching.ItemLevel = 1;
                ObjCategoryforDispatching.SupplierName = ObjFirstCategory.SupplierName;
                ObjCategoryforDispatching.SupplierID = 0;
                ObjCategoryforDispatching.EmployeeID = hideEmployeeID.Value.ToInt32();
                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCategoryforDispatching.CustomerID = Request["CustomerID"].ToInt32();
                ObjCategoryforDispatching.IsFirstMakes = 0;
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);

                ObjFirstCategory.IsFinishMake = 1;
                ObjQuotedPriceItemBLL.Update(ObjFirstCategory);
            }

            //添加二级宗派共
            foreach (var ObjSecondItem in ObjChangeSecondList)
            {
                ObjCategoryforDispatching = new FL_ProductforDispatching();
                ObjCategoryforDispatching.DispatchingID = ObjDispatchingModel.DispatchingID;
                ObjCategoryforDispatching.Unit = ObjSecondItem.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjSecondItem.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjSecondItem.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjSecondItem.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjSecondItem.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjSecondItem.Quantity.ToString().ToInt32();
                ObjCategoryforDispatching.Subtotal = ObjSecondItem.Subtotal;
                ObjCategoryforDispatching.Remark = ObjSecondItem.Remark;
                ObjCategoryforDispatching.ParentDispatchingID = ObjDispatchingModel.ParentDispatchingID;
                ObjCategoryforDispatching.CategoryName = ObjSecondItem.CategoryName;
                ObjCategoryforDispatching.CategoryID = ObjSecondItem.CategoryID;
                ObjCategoryforDispatching.ProductID = ObjSecondItem.ProductID;
                ObjCategoryforDispatching.ParentCategoryID = ObjSecondItem.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjCategoryforDispatching.ParentCategoryID).Title;
                ObjCategoryforDispatching.ItemLevel = 2;
                ObjCategoryforDispatching.PurchasePrice = ObjSecondItem.PurchasePrice;
                ObjCategoryforDispatching.CreateEmployee = 0;
                ObjCategoryforDispatching.Productproperty = ObjSecondItem.Productproperty;
                ObjCategoryforDispatching.EmployeeID = hideEmployeeID.Value.ToInt32();
                //ObjCategoryforDispatching.ParentDispatchingID
                ObjCategoryforDispatching.RowType = ObjSecondItem.RowType;
                ObjCategoryforDispatching.OrderID = OrderID;

                ObjCategoryforDispatching.SupplierName = ObjSecondItem.SupplierName;
                ObjCategoryforDispatching.SupplierID = 0;
                ObjCategoryforDispatching.CustomerID = Request["CustomerID"].ToInt32();
                ObjCategoryforDispatching.IsFirstMakes = 0;
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);

                ObjSecondItem.IsFinishMake = 1;
                ObjQuotedPriceItemBLL.Update(ObjSecondItem);

            }

            foreach (var ObjThiredItem in ObjProductList)
            {
                ObjCategoryforDispatching = new FL_ProductforDispatching();
                ObjCategoryforDispatching.DispatchingID = ObjDispatchingModel.DispatchingID;
                ObjCategoryforDispatching.Unit = ObjThiredItem.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjThiredItem.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjThiredItem.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjThiredItem.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjThiredItem.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjThiredItem.Quantity.ToString().ToInt32();
                ObjCategoryforDispatching.Subtotal = ObjThiredItem.PurchasePrice * ObjThiredItem.Quantity;
                ObjCategoryforDispatching.Remark = ObjThiredItem.Remark;
                ObjCategoryforDispatching.ParentDispatchingID = ObjDispatchingModel.ParentDispatchingID;
                ObjCategoryforDispatching.CategoryName = ObjThiredItem.CategoryName;
                ObjCategoryforDispatching.ProductID = ObjThiredItem.ProductID;
                ObjCategoryforDispatching.CategoryID = ObjThiredItem.CategoryID;
                ObjCategoryforDispatching.ParentCategoryID = ObjThiredItem.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjThiredItem.ParentCategoryID).Title;
                ObjCategoryforDispatching.ItemLevel = 3;
                ObjCategoryforDispatching.CreateEmployee = 0;
                ObjCategoryforDispatching.PurchasePrice = ObjThiredItem.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjThiredItem.Productproperty;
                ObjCategoryforDispatching.RowType = ObjThiredItem.RowType;
                ObjCategoryforDispatching.EmployeeID = hideEmployeeID.Value.ToInt32();
                ObjCategoryforDispatching.SupplierName = ObjThiredItem.SupplierName;
                ObjCategoryforDispatching.Classification = ObjThiredItem.Classification;
                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCategoryforDispatching.CustomerID = Request["CustomerID"].ToInt32();
                ObjCategoryforDispatching.IsFirstMakes = 0;

                #region 财务成本明细
                if (ObjThiredItem.SupplierName != null && ObjThiredItem.SupplierName != "库房")
                {
                    ObjFinalCostModel = new FL_OrderfinalCost();
                    ObjFinalCostModel.KindType = 1;
                    ObjFinalCostModel.IsDelete = false;
                    ObjFinalCostModel.CostKey = CostKey;
                    ObjFinalCostModel.CreateDate = DateTime.Now;
                    ObjFinalCostModel.CustomerID = Request["CustomersID"].ToInt32();
                    ObjFinalCostModel.CellPhone = string.Empty;
                    ObjFinalCostModel.InsideRemark = string.Empty;
                    if (Request["ParentDispatchingID"].ToInt32() == 0)
                    {

                        ObjFinalCostModel.KindID = DisID;
                    }
                    else
                    {
                        ObjFinalCostModel.KindID = Request["ParentDispatchingID"].ToInt32();
                    }
                    ObjFinalCostModel.ServiceContent = ObjThiredItem.SupplierName;
                    ObjFinalCostModel.PlannedExpenditure = 0;
                    ObjFinalCostModel.ActualExpenditure = 0;
                    ObjFinalCostModel.Expenseaccount = string.Empty;

                    ObjFinalCostModel.ActualWorkload = string.Empty;
                    ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                }

                if (ObjThiredItem.Productproperty == 0)
                {
                    ObjFinalCostModel = new FL_OrderfinalCost();
                    ObjFinalCostModel.KindType = 0;
                    ObjFinalCostModel.IsDelete = false;
                    ObjFinalCostModel.CostKey = CostKey;
                    ObjFinalCostModel.CreateDate = DateTime.Now;
                    ObjFinalCostModel.CustomerID = Request["CustomerID"].ToInt32();
                    ObjFinalCostModel.CellPhone = string.Empty;
                    ObjFinalCostModel.InsideRemark = string.Empty;
                    if (Request["ParentDispatchingID"].ToInt32() == 0)
                    {

                        ObjFinalCostModel.KindID = DisID;
                    }
                    else
                    {
                        ObjFinalCostModel.KindID = Request["ParentDispatchingID"].ToInt32();
                    }
                    ObjFinalCostModel.ServiceContent = ObjThiredItem.ServiceContent;

                    ObjFinalCostModel.PlannedExpenditure = 0;
                    ObjFinalCostModel.ActualExpenditure = 0;
                    ObjFinalCostModel.Expenseaccount = string.Empty;

                    ObjFinalCostModel.ActualWorkload = string.Empty;
                    ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                }
                #endregion



                ObjCategoryforDispatching.SupplierID = 0;
                //生成三级
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);

                ObjThiredItem.IsFinishMake = 1;
                ObjQuotedPriceItemBLL.Update(ObjThiredItem);
            }
            /// <summary>
            /// 客户操作
            /// </summary>
            Customers ObjCustomersBLL = new Customers();
            var ObjModel = ObjCustomersBLL.GetByID(ObjQuotedPriceBLL.GetByID(QuotedID).CustomerID);
            ObjModel.State = (int)CustomerStates.NewCarrytask;
            ObjCustomersBLL.Update(ObjModel);


            //进入订单状态
            DispatchingState ObjDispatchingStateBLL = new DispatchingState();
            FL_DispatchingState ObjispatchingStateModel = new FL_DispatchingState();
            ObjispatchingStateModel.DispatchingID = ObjDispatchingModel.DispatchingID;
            ObjispatchingStateModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
            ObjispatchingStateModel.CreateDate = DateTime.Now;
            ObjispatchingStateModel.IsUse = false;
            ObjispatchingStateModel.State = 1;
            ObjDispatchingModel.CustomerID = Request["CustomerID"].ToInt32();
            ObjispatchingStateModel.StateEmpLoyee = hideEmployeeID.Value.ToInt32();

            ObjDispatchingStateBLL.Insert(ObjispatchingStateModel);

        }
        #endregion

    }
}