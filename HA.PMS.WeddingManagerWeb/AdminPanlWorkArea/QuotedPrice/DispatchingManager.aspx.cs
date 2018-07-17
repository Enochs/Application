using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class DispatchingManager : SystemPage
    {

        /// <summary>
        /// 报价单类别业务逻辑
        /// </summary>
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        Employee ObjEmployeeBLL = new Employee();

        Celebration ObjCelebrationBLL = new Celebration();

        CelebrationProductItem ObjItemBLL = new CelebrationProductItem();


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
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            CustomersID = Request["CustomerID"].ToInt32();
            CategoryID = Request["CategoryID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            if (!IsPostBack)
            {

                hiddeEmpLoyeeID.Value = User.Identity.Name.ToInt32().ToString();
                var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                if (ObjQuotedModel != null)
                {
                    if (ObjQuotedModel.Dispatching == 0)
                    {
                        btnSaveChange.Visible = true;
                    }
                }
                if (Request["Change"] != null)
                {

                    Employee ObjEmployeeBLL = new Employee();
                    txtEmpLoyee.Value = ObjEmployeeBLL.GetByID(ObjQuotedPriceBLL.GetByID(ObjQuotedModel.ParentQuotedID).EmpLoyeeID).EmployeeName;

                }
                //绑定本栏目派工人
                //UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
                //ObjUserJurisdictionBLL.GetDispatchingByChannelType("DispatchingManager", User.Identity.Name.ToInt32());

                //绑定tab
                if (Request["QuotedID"] == null)
                {
                    QuotedID = ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).QuotedID;
                }
                else
                {
                    QuotedID = Request["QuotedID"].ToInt32();
                }
                var DataSource = ObjItemBLL.GetByQuotedID(QuotedID, 1);

                this.reptabstitle.DataSource = DataSource;
                this.reptabstitle.DataBind();
                this.reptabContent.DataSource = DataSource;
                this.reptabContent.DataBind();


                FL_Dispatching ObjDispathingModel = new FL_Dispatching();

                //判读是否已导入派工单
                var ObjCelModel = ObjCelebrationBLL.GetByQuotedID(Request["QuotedID"].ToInt32());
                if (ObjCelModel != null)
                {
                    ObjDispathingModel = ObjDispatchingBLL.GetByCelebrationID(ObjCelModel.CelebrationID);
                    if (ObjDispathingModel != null)
                    {
                        Employee ObjEmployeeBLL = new Employee();
                        txtEmpLoyee.Value = ObjEmployeeBLL.GetByID(ObjDispathingModel.EmployeeID).EmployeeName;
                        txtEmpLoyee.Disabled = true;
            

                        hideIsDis.Value = "1";

                    }
                }

                //var QuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                //var JinGnangList = OrderGuardianBLL.GetByOrderID(QuotedModel.OrderID);
                //List<int> KeyList = new List<int>();
                //foreach (var ObjJingang in JinGnangList)
                //{
                //    KeyList.Add(ObjJingang.GuardianId.Value);
                //}

                //this.repdatalist.DataSource = ObjFourGuardianBLL.GetByInKeyList(KeyList.ToArray());
                //this.repdatalist.DataBind();


                //FL_Dispatching ObjDispatchingModel = new FL_Dispatching();
                //Dispatching ObjDispatchingBLL = new Dispatching();
                ////先判断是否已经有总派工信息
                ////如果有就不添加
                //if (!ObjCelebrationBLL.IsExistByQuotedID(QuotedID))
                //{
                //    FL_Celebration ObjCelebrationModel = new FL_Celebration();
                //    ObjCelebrationModel.OrderID = QuotedModel.OrderID.Value;
                //    ObjCelebrationModel.OrderCoder = QuotedModel.OrderCoder;
                //    ObjCelebrationModel.IsDelete = false;
                //    ObjCelebrationModel.CustomerID = QuotedModel.CustomerID.Value;
                //    ObjCelebrationModel.QuotedID = QuotedModel.QuotedID;
                //    ObjCelebrationBLL.Insert(ObjCelebrationModel);
                //    ObjDispatchingModel.CelebrationID = ObjCelebrationModel.CelebrationID;
                //    ObjDispatchingModel.CreateDate = DateTime.Now;
                //    ObjDispatchingModel.UpdateDate = DateTime.Now;
                //    ObjDispatchingModel.IsBegin = false;
                //    ObjDispatchingModel.Isover = false;
                //    ObjDispatchingModel.OrderID = QuotedModel.OrderID.Value;
                //    ObjDispatchingModel.OrderCoder = QuotedModel.OrderCoder;
                //    ObjDispatchingModel.EmployeeID = User.Identity.Name.ToString().ToInt32();
                //    ObjDispatchingBLL.Insert(ObjDispatchingModel);
                //}
                //else
                //{
                //    ObjDispatchingBLL.GetByQuotedID(QuotedID);
                //}
            }

        }

        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            //var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomersID);
            //lblCoder.Text = OrderID.ToString();
            //lblCustomerName.Text = ObjCustomerModel.Groom;
            //lblHotel.Text = ObjCustomerModel.Wineshop;
            //lblPartyDate.Text = GetShortDateString(ObjCustomerModel.PartyDate);
            //lblPhone.Text = ObjCustomerModel.GroomCellPhone;
            //lblTyper.Text = "套系";

        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {


            //可以开始调查
            
            MissionManager ObjMissManagerBLL = new MissionManager();
            var UpdateModel = ObjDispatchingBLL.GetByID(InsertDispatching());


           
            if (txtEmpLoyee.Value == string.Empty)
            {
                UpdateModel.EmployeeID = User.Identity.Name.ToString().ToInt32();

                ///添加庆典任务到任务列表
                FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();

                ObjDispatchingBLL.Update(UpdateModel);
            }
            else
            {

                UpdateModel.EmployeeID = hiddeEmpLoyeeID.Value.ToInt32();

                ///添加庆典任务到任务列表
                FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();

                ObjDispatchingBLL.Update(UpdateModel);
            }


            var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            if (ObjQuotedModel.Dispatching == 0)
            {
                ObjQuotedModel.Dispatching = UpdateModel.DispatchingID;
                ObjQuotedModel.StarDispatching = true;
                ObjQuotedPriceBLL.Update(ObjQuotedModel);
                CreateDispatching(UpdateModel.DispatchingID);
                JavaScriptTools.AlertWindow("保存成功!", Page);
                btnSaveChange.Visible = false;
            }
 
            btnSaveChange.Visible = false;
            btnSavessss.Visible = false;


            string NodeKey = "?StateKey=1&New=1&DispatchingID=" + UpdateModel.DispatchingID + "&CustomerID=" + ObjQuotedModel.CustomerID + "&OrderID=" + ObjQuotedModel.OrderID + "&NeedPopu=1";
            ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedModel.CustomerID.Value, 1, (int)MissionTypes.Dispatching, DateTime.Now, UpdateModel.EmployeeID.Value, NodeKey, MissionChannel.Quoted, UpdateModel.EmployeeID.Value, UpdateModel.DispatchingID);
            var ObjCustomerUpdateModel = ObjCustomersBLL.GetByID(ObjQuotedModel.CustomerID);
            ObjCustomerUpdateModel.State = (int)CustomerStates.NewCarrytask;
            ObjCustomersBLL.Update(ObjCustomerUpdateModel);


            //修改统计信息
            Report ObjReportBLL = new Report();
            SS_Report ObjReportModel = new SS_Report();
            ObjReportModel = ObjReportBLL.GetByCustomerID(CustomersID, ObjQuotedModel.EmpLoyeeID.Value);
            ObjReportModel.State = ObjCustomerUpdateModel.State;
            ObjReportModel.WorkCreateDate = DateTime.Now;

            ObjReportModel.WorkEmployee = UpdateModel.EmployeeID;
            ObjReportBLL.Update(ObjReportModel);


            JavaScriptTools.CloseWindow("总派工任务已经下达!",Page);
            //DoingCarrytask
        }


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
            ObjCostModel.CustomerID = CustomersID;
            ObjCostModel.OrderID = OrderID;
            ObjCostModel.IsLock = false;
            ObjCostModel.TotalAmount = ObjQuotedPriceBLL.GetByID(QuotedID).FinishAmount;
            ObjCostModel.CreateDate = DateTime.Now;
            int CostKey = ObjCostBLL.Insert(ObjCostModel);


            #endregion



            int ParentDispatchingID = 0;
            if (Request["Change"] != null)
            {
                ParentDispatchingID = DisID;
            }
            FL_ProductforDispatching ObjCategoryforDispatching = new FL_ProductforDispatching();
            //添加派工大类

            //一级类别
            var ObjChangeFirstList = ObjItemBLL.GetByQuotedID(QuotedID, 1);

            //二级类别
            var ObjChangeSecondList = ObjItemBLL.GetByQuotedID(QuotedID, 2);

            //三级产品类别
            var ObjProductList = ObjItemBLL.GetByQuotedID(QuotedID, 3);

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
                ObjCategoryforDispatching.EmployeeID = hiddeEmpLoyeeID.Value.ToInt32();
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);
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
                ObjCategoryforDispatching.EmployeeID = hiddeEmpLoyeeID.Value.ToInt32();
                //ObjCategoryforDispatching.ParentDispatchingID
                ObjCategoryforDispatching.RowType = ObjSecondItem.RowType;

                ObjCategoryforDispatching.SupplierName = ObjSecondItem.SupplierName;
                ObjCategoryforDispatching.SupplierID = 0;
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);

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
                ObjCategoryforDispatching.EmployeeID = hiddeEmpLoyeeID.Value.ToInt32();
                ObjCategoryforDispatching.SupplierName = ObjThiredItem.SupplierName;


                #region 财务成本明细
                if (ObjThiredItem.SupplierName != null && ObjThiredItem.SupplierName != "库房")
                {
                    ObjFinalCostModel = new FL_OrderfinalCost();
                    ObjFinalCostModel.KindType = 1;
                    ObjFinalCostModel.IsDelete = false;
                    ObjFinalCostModel.CostKey = CostKey;
                    ObjFinalCostModel.CreateDate = DateTime.Now;
                    ObjFinalCostModel.CustomerID = CustomersID;
                    ObjFinalCostModel.CellPhone = string.Empty;
                    ObjFinalCostModel.InsideRemark = string.Empty;
                    if (Request["ParentDispatchingID"].ToInt32()==0)
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
                    ObjFinalCostModel.CustomerID = CustomersID;
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
            }
            /// <summary>
            /// 客户操作
            /// </summary>
            //Customers ObjCustomersBLL = new Customers();
            //var ObjModel = ObjCustomersBLL.GetByID(ObjQuotedPriceBLL.GetByID(QuotedID).CustomerID);
            //ObjModel.State = (int)CustomerStates.StarCarrytask;
            //ObjCustomersBLL.Update(ObjModel);


            //进入订单状态
            DispatchingState ObjDispatchingStateBLL = new DispatchingState();
            FL_DispatchingState ObjispatchingStateModel = new FL_DispatchingState();
            ObjispatchingStateModel.DispatchingID = ObjDispatchingModel.DispatchingID;
            ObjispatchingStateModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
            ObjispatchingStateModel.CreateDate = DateTime.Now;
            ObjispatchingStateModel.IsUse = false;
            ObjispatchingStateModel.State = 1;
            ObjispatchingStateModel.StateEmpLoyee = hiddeEmpLoyeeID.Value.ToInt32();

            ObjDispatchingStateBLL.Insert(ObjispatchingStateModel);

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
                ObjDispathingModel.EmployeeID = 0;
                ObjDispathingModel.ParentDispatchingID = Request["ParentDispatchingID"].ToInt32();
                ObjDispathingModel.CustomerID = CustomersID;
                if (Request["Change"] != null)
                {
                    ObjDispathingModel.ParentDispatchingID = ObjQuotedPriceBLL.GetByID(Request["Change"].ToString().ToInt32()).Dispatching;
                }
                ObjDispathingModel.CelebrationID = ObjItemBLL.GetByQuotedID(Request["QuotedID"].ToInt32(), 1).First().CelebrationID;
                ObjDispathingModel.QuotedEmpLoyee = ObjCelModel.QuotedEmpLoyee;
                return ObjDispatchingBLL.Insert(ObjDispathingModel);
            }
            else
            {
                return ObjDispathingModel.DispatchingID;
         
            }

        }
    }
}