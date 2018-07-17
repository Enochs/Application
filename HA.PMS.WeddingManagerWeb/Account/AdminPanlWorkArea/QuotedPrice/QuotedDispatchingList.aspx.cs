using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedDispatchingList : SystemPage
    {        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var objParmList = new List<PMSParameters>();
 

            this.MyManager.GetEmployeePar(objParmList);
 
            objParmList.Add("IsChecks", true, NSqlTypes.Bit);
            objParmList.Add("IsDispatching", 4, NSqlTypes.Equal);
            objParmList.Add("StarDispatching", false, NSqlTypes.Bit);
            objParmList.Add("Dispatching", 0, NSqlTypes.Equal);
 
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount); 
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }


        /// <summary>
        /// 获取邀约负责人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderEmployee(object CustomerID)
        {
            if (CustomerID != null)
            {
                if (CustomerID.ToString() != string.Empty)
                {
                    Employee ObjEmpLoyeeBLL = new Employee();
                    var ObjIntive = ObjOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32());
                    if (ObjIntive != null)
                    {
                        var ObjEmpModel = ObjEmpLoyeeBLL.GetByID(ObjIntive.EmployeeID);
                        if (ObjEmpModel != null)
                        {
                            return ObjEmpModel.EmployeeName;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 获取已付款
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetQuotedDispatchingFinishMoney(object CustomerID)
        {
            OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal FinishAmount = 0;

            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
            FinishAmount += EarnestMoney;

            //获得收款计划的东西
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);
            
            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }

            //定金
            var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            if (ObjEorder.EarnestMoney.HasValue)
            {
                FinishAmount += ObjEorder.EarnestMoney.Value;
            }
            return FinishAmount.ToString();

        }

        /// <summary>

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Create")
            {        /// <summary>
                /// 报价单主体表
                /// </summary>
                /// 

                Celebration ObjCelebrationBLL = new Celebration();
                var ObjCelModel = ObjCelebrationBLL.GetByQuotedID(e.CommandArgument.ToString().ToInt32());

                HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
                var QuotedID = e.CommandArgument.ToString().ToInt32();
                var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                FL_CelebrationProductItem ObjCategoryforDispatching = new FL_CelebrationProductItem();
                QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

                CelebrationProductItem ObjCelebrationProductItemBLL = new CelebrationProductItem();
                var ParetnID = 0;
                var CelebrationID = ObjCelebrationBLL.Insert(new FL_Celebration()
                {
                    IsDelete = false,
                    ParentCelebrationID = 0,

                    QuotedID = e.CommandArgument.ToString().ToInt32()

                });


                //一级类别
                var ObjChangeFirstList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 1);

                //二级类别
                var ObjChangeSecondList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 2);

                //三级产品类别
                var ObjProductList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 3);

                foreach (var ObjFirstCategory in ObjChangeFirstList)
                {
                    ObjCategoryforDispatching = new FL_CelebrationProductItem();

                    ObjCategoryforDispatching.CategoryID = ObjFirstCategory.CategoryID;
                    ObjCategoryforDispatching.CategoryName = ObjFirstCategory.CategoryName;
                    ObjCategoryforDispatching.ParentCategoryID = ObjFirstCategory.ParentCategoryID;
                    ObjCategoryforDispatching.ParentCelebrationID = ParetnID;
                    ObjCategoryforDispatching.ParentQuotedID = ObjFirstCategory.ParentQuotedID;
                    ObjCategoryforDispatching.ProductID = ObjFirstCategory.ProductID;
                    ObjCategoryforDispatching.ItemLevel = 1;
                    ObjCategoryforDispatching.Unit = ObjFirstCategory.Unit;
                    ObjCategoryforDispatching.ServiceContent = ObjFirstCategory.Unit;
                    ObjCategoryforDispatching.Requirement = ObjFirstCategory.Requirement;
                    ObjCategoryforDispatching.ImageUrl = ObjFirstCategory.ImageUrl;
                    ObjCategoryforDispatching.UnitPrice = ObjFirstCategory.UnitPrice;
                    ObjCategoryforDispatching.Quantity = ObjFirstCategory.Quantity;
                    ObjCategoryforDispatching.Subtotal = ObjFirstCategory.Subtotal;
                    ObjCategoryforDispatching.Remark = ObjFirstCategory.Remark;
                    ObjCategoryforDispatching.IsChecks = false;

                    ObjCategoryforDispatching.CelebrationID = CelebrationID;

                    ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);
                }


                foreach (var ObjSecondItem in ObjChangeSecondList)
                {
                    ObjCategoryforDispatching = new FL_CelebrationProductItem();
                    ObjCategoryforDispatching.CategoryID = ObjSecondItem.CategoryID;
                    ObjCategoryforDispatching.CategoryName = ObjSecondItem.CategoryName;
                    ObjCategoryforDispatching.ParentCategoryID = ObjSecondItem.ParentCategoryID;
                    ObjCategoryforDispatching.ParentCelebrationID = ParetnID;
                    ObjCategoryforDispatching.ParentQuotedID = ObjSecondItem.ParentQuotedID;
                    ObjCategoryforDispatching.ProductID = ObjSecondItem.ProductID;
                    ObjCategoryforDispatching.ItemLevel = 2;
                    ObjCategoryforDispatching.Unit = ObjSecondItem.Unit;
                    ObjCategoryforDispatching.ServiceContent = ObjSecondItem.Unit;
                    ObjCategoryforDispatching.Requirement = ObjSecondItem.Requirement;
                    ObjCategoryforDispatching.ImageUrl = ObjSecondItem.ImageUrl;
                    ObjCategoryforDispatching.UnitPrice = ObjSecondItem.UnitPrice;
                    ObjCategoryforDispatching.Quantity = ObjSecondItem.Quantity;
                    ObjCategoryforDispatching.Subtotal = ObjSecondItem.Subtotal;
                    ObjCategoryforDispatching.Remark = ObjSecondItem.Remark;
                    ObjCategoryforDispatching.IsChecks = false;

                    ObjCategoryforDispatching.CelebrationID = CelebrationID;

                    ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);

                }


                //添加三级
                foreach (var ObjThiredItem in ObjChangeSecondList)
                {

                    ObjCategoryforDispatching = new FL_CelebrationProductItem();

                    ObjCategoryforDispatching.CategoryID = ObjThiredItem.CategoryID;
                    ObjCategoryforDispatching.CategoryName = ObjThiredItem.CategoryName;
                    ObjCategoryforDispatching.ParentCategoryID = ObjThiredItem.ParentCategoryID;
                    ObjCategoryforDispatching.ParentCelebrationID = ParetnID;
                    ObjCategoryforDispatching.ParentQuotedID = ObjThiredItem.ParentQuotedID;
                    ObjCategoryforDispatching.ProductID = ObjThiredItem.ProductID;
                    ObjCategoryforDispatching.ItemLevel = 2;
                    ObjCategoryforDispatching.Unit = ObjThiredItem.Unit;
                    ObjCategoryforDispatching.ServiceContent = ObjThiredItem.Unit;
                    ObjCategoryforDispatching.Requirement = ObjThiredItem.Requirement;
                    ObjCategoryforDispatching.ImageUrl = ObjThiredItem.ImageUrl;
                    ObjCategoryforDispatching.UnitPrice = ObjThiredItem.UnitPrice;
                    ObjCategoryforDispatching.Quantity = ObjThiredItem.Quantity;
                    ObjCategoryforDispatching.Subtotal = ObjThiredItem.Subtotal;
                    ObjCategoryforDispatching.Remark = ObjThiredItem.Remark;
                    ObjCategoryforDispatching.IsChecks = false;

                    ObjCategoryforDispatching.CelebrationID = CelebrationID;

                    ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);
                }

            }
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        /// <summary>
        /// 显示更新
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string ShowUpdate(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            if (State.ToString() == "True")
            {
                return string.Empty;

            }
            else
            {
                return "style='display:none'";
            }
        }
    }
}