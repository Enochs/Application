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
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceChangeDispatching : SystemPage
    {
        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

    
 
        
        CelebrationProductItem ObjCelebrationProductItemBLL = new CelebrationProductItem();


        /// <summary>
        /// 获取四大金刚
        /// </summary>
        FourGuardian ObjFourGuardianBLL = new FourGuardian();

        OrderGuardian OrderGuardianBLL = new OrderGuardian();
        Celebration ObjCelebrationBLL = new Celebration();
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        /// <summary>
        /// 报价单变更操作
        /// </summary>

        /// <summary>
        /// 报价单基础表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //客户ID
        int CustomersID = 0;
        //坤ID
        int QuotedID = 0;
        //类别ID
        int CategoryID = 0;


        Dispatching ObjDispatchingBLL = new Dispatching();
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            CustomersID = Request["CustomersID"].ToInt32();
            CategoryID = Request["CategoryID"].ToInt32();
            if (!IsPostBack)
            {
                //绑定本栏目派工人
                UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
                ObjUserJurisdictionBLL.GetDispatchingByChannelType("DispatchingManager", User.Identity.Name.ToInt32());

                var ObjParList = new List<ObjectParameter>();
                //ObjParList.Add(new ObjectParameter("State", (int)CustomerStates.DoingQuotedPrice));
                //ObjParList.Add(new ObjectParameter("ParentQuotedID", Request["QuotedID"].ToInt32()));
                //ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));

                var DataList = ObjQuotedPriceBLL.GetByParentQuotedID(QuotedID);
                repSortListList.DataSource = DataList;
                repSortListList.DataBind();



                //this.repCatageList.DataSource = DataSource;
                //this.repCatageList.DataBind();
                //    this.reptabContent.DataSource = DataSource;
                //    this.reptabContent.DataBind();

                //    var QuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                //    var JinGnangList = OrderGuardianBLL.GetByOrderID(QuotedModel.OrderID);
                //    List<int> KeyList = new List<int>();
                //    foreach (var ObjJingang in JinGnangList)
                //    {
                //        KeyList.Add(ObjJingang.GuardianId.Value);
                //    }

                //    this.repdatalist.DataSource = ObjFourGuardianBLL.GetByInKeyList(KeyList.ToArray());
                //    this.repdatalist.DataBind();


                //    FL_Dispatching ObjDispatchingModel = new FL_Dispatching();
                //    Dispatching ObjDispatchingBLL = new Dispatching();
                //    //先判断是否已经有总派工信息
                //    //如果有就不添加
                //    if (!ObjCelebrationBLL.IsExistByQuotedID(QuotedID))
                //    {
                //        FL_Celebration ObjCelebrationModel = new FL_Celebration();
                //        ObjCelebrationModel.OrderID = QuotedModel.OrderID.Value;
                //        ObjCelebrationModel.OrderCoder = QuotedModel.OrderCoder;
                //        ObjCelebrationModel.IsDelete = false;
                //        ObjCelebrationModel.CustomerID = QuotedModel.CustomerID.Value;
                //        ObjCelebrationModel.QuotedID = QuotedModel.QuotedID;
                //        ObjCelebrationBLL.Insert(ObjCelebrationModel);
                //        ObjDispatchingModel.CelebrationID = ObjCelebrationModel.CelebrationID;
                //        ObjDispatchingModel.CreateDate = DateTime.Now;
                //        ObjDispatchingModel.UpdateDate = DateTime.Now;
                //        ObjDispatchingModel.IsBegin = false;
                //        ObjDispatchingModel.Isover = false;
                //        ObjDispatchingModel.OrderID = QuotedModel.OrderID.Value;
                //        ObjDispatchingModel.OrderCoder = QuotedModel.OrderCoder;
                //        ObjDispatchingModel.EmployeeID = User.Identity.Name.ToString().ToInt32();
                //        ObjDispatchingBLL.Insert(ObjDispatchingModel);


                //    }
                //    else
                //    {
                //        ObjDispatchingBLL.GetByQuotedID(QuotedID);
                //    }
            }

        }


        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            //MissionManager ObjMissManagerBLL = new MissionManager();

            //var UpdateModel = ObjDispatchingBLL.GetByQuotedID(QuotedID);
            //if (ddlSelectDisEmployee.SelectedItem.Value == "0")
            //{
            //    UpdateModel.EmployeeID = User.Identity.Name.ToString().ToInt32();

            //    ///添加庆典任务到任务列表
            //    FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();
            //    ObjMissManagerBLL.WeddingMissionCreate("总派工", "派工任务", DateTime.Now, UpdateModel.EmployeeID.Value, "?CustomerID=" + UpdateModel.CustomerID.ToString(), MissionChannel.StarOrder);
            //    ObjDispatchingBLL.Update(UpdateModel);
            //}
            //else
            //{

            //    UpdateModel.EmployeeID = hiddeEmpLoyeeID.Value.ToInt32();
            //    ObjDispatchingBLL.Update(UpdateModel);
            //    添加庆典任务到任务列表
            //    FL_MissionDetailed ObjDetailedModel = new FL_MissionDetailed();
            //    ObjMissManagerBLL.WeddingMissionCreate("总派工", "派工任务", DateTime.Now, UpdateModel.EmployeeID.Value, "?CustomerID=" + UpdateModel.CustomerID.ToString(), MissionChannel.StarOrder);
            //    ObjDispatchingBLL.Update(UpdateModel);
            //}

            //CreateDispatching();
        }


        /// <summary>
        /// 开始创建派工任务
        /// </summary>
        //private void CreateCelebration(int ChangeID)
        //{

        //    ///获取执行总表信息
        //    var ObjCelebrationModel = ObjCelebrationBLL.GetByQuotedID(QuotedID);


        //    var ObjCategoryList = ObjCategoryForQuotedPriceBLL.GetByQuotedID(QuotedID).Where(C => C.IsChange == true && C.ChangeID == ChangeID);
        //    var ObjDispatchingModel = ObjDispatchingBLL.GetByQuotedID(QuotedID);
        //    CategoryforDispatching ObjCategoryforDispatchingBLL = new CategoryforDispatching();

        //    ///添加信息
        //    FL_CelebrationProductItem ObjCelebrationProductItemModel = new FL_CelebrationProductItem();
        //    //修改派工状态
        //    FL_QuotedPrice ObjquotedPriceModel = ObjQuotedPriceBLL.GetByQuotedID(QuotedID);
        //    if (ObjquotedPriceModel != null)
        //    {
        //        ObjquotedPriceModel.Dispatching = 1;
        //        ObjQuotedPriceBLL.Update(ObjquotedPriceModel);
        //    }
        //    //添加派工大类
        //    foreach (var ObjFirstCategory in ObjCategoryList)
        //    {
        //        //FL_CategoryforDispatching ObjCategoryforDispatching = new FL_CategoryforDispatching();
        //        ObjCelebrationProductItemModel = new FL_CelebrationProductItem();


        //        ObjCelebrationProductItemModel.Unit = ObjFirstCategory.Unit;
        //        ObjCelebrationProductItemModel.ServiceContent = ObjFirstCategory.Unit;
        //        ObjCelebrationProductItemModel.Requirement = ObjFirstCategory.Requirement;
        //        ObjCelebrationProductItemModel.ImageUrl = ObjFirstCategory.ImageUrl;
        //        ObjCelebrationProductItemModel.UnitPrice = ObjFirstCategory.UnitPrice;
        //        ObjCelebrationProductItemModel.Quantity = ObjFirstCategory.Quantity;
        //        ObjCelebrationProductItemModel.Subtotal = ObjFirstCategory.Subtotal;
        //        ObjCelebrationProductItemModel.Remark = ObjFirstCategory.Remark;
        //        ObjCelebrationProductItemModel.ProductID = 0;
        //        ObjCelebrationProductItemModel.CategoryName = ObjFirstCategory.CategoryName;
        //        ObjCelebrationProductItemModel.ParentCategoryID = 0;
        //        ObjCelebrationProductItemModel.CategoryID = ObjFirstCategory.CategoryID;
        //        ObjCelebrationProductItemBLL.Insert(ObjCelebrationProductItemModel);

        //    }


        //    //添加二级宗派共
        //    FL_ProductitemforDispactching ObjProductitemforDispactchingModel = new FL_ProductitemforDispactching();
        //    ProductitemforDispactching ObjProductitemforDispactchingBLL = new ProductitemforDispactching();
        //    ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        //    var ObjSecondList = ObjProductitemForQuotedPriceBLL.GetByQuotedID(QuotedID).Where(C => C.IsChange == true && C.ChangeID == ChangeID);
        //    foreach (var ObjSecondItem in ObjSecondList)
        //    {
        //        ObjCelebrationProductItemModel = new FL_CelebrationProductItem();
        //        ObjCelebrationProductItemModel.Unit = ObjSecondItem.Unit;
        //        ObjCelebrationProductItemModel.ServiceContent = ObjSecondItem.Unit;
        //        ObjCelebrationProductItemModel.Requirement = ObjSecondItem.Requirement;
        //        ObjCelebrationProductItemModel.ImageUrl = ObjSecondItem.ImageUrl;
        //        ObjCelebrationProductItemModel.UnitPrice = ObjSecondItem.UnitPrice;
        //        ObjCelebrationProductItemModel.Quantity = ObjSecondItem.Quantity;
        //        ObjCelebrationProductItemModel.Subtotal = ObjSecondItem.Subtotal;
        //        ObjCelebrationProductItemModel.Remark = ObjSecondItem.Remark;
        //        ObjCelebrationProductItemModel.ProductID = 0;
        //        ObjCelebrationProductItemModel.CategoryName = ObjSecondItem.CategoryName;
        //        ObjCelebrationProductItemModel.CategoryID = ObjSecondItem.CategoryID;
        //        ObjCelebrationProductItemModel.ParentCategoryID = ObjSecondItem.ParentCategoryID;
        //        //生成三级
        //        var ObjThiredList = ObjProductForQuotedPriceBLL.GetByQuotedID(QuotedID).Where(C => C.IsChange == true && C.ChangeID == ChangeID);
        //        foreach (var ObjThiredItem in ObjThiredList)
        //        {
        //            FL_CelebrationProductItem ObjCelebrationProductItemModelProduct = new FL_CelebrationProductItem();

        //            ObjCelebrationProductItemModelProduct.Unit = ObjThiredItem.Unit;
        //            ObjCelebrationProductItemModelProduct.ServiceContent = ObjThiredItem.Unit;
        //            ObjCelebrationProductItemModelProduct.Requirement = ObjThiredItem.Requirement;
        //            ObjCelebrationProductItemModelProduct.ImageUrl = ObjThiredItem.ImageUrl;
        //            ObjCelebrationProductItemModelProduct.UnitPrice = ObjThiredItem.UnitPrice;
        //            ObjCelebrationProductItemModelProduct.Quantity = ObjThiredItem.Quantity;
        //            ObjCelebrationProductItemModelProduct.Subtotal = ObjThiredItem.Subtotal;
        //            ObjCelebrationProductItemModelProduct.Remark = ObjThiredItem.Remark;
        //            ObjCelebrationProductItemModelProduct.CategoryName = ObjThiredItem.CategoryName;
        //            ObjCelebrationProductItemModelProduct.CategoryID = ObjThiredItem.CategoryID;
        //            ObjCelebrationProductItemModelProduct.ProductID = ObjThiredItem.ProductID;
        //            ObjCelebrationProductItemModelProduct.ParentCategoryID = ObjThiredItem.ParentCategoryID;
        //            ObjCelebrationProductItemModelProduct.ChangeID = ObjThiredItem.ChangeID;

        //            ObjCelebrationProductItemBLL.Insert(ObjCelebrationProductItemModel);

        //        }
        //        ObjCelebrationProductItemBLL.Insert(ObjCelebrationProductItemModel);

        //    }
        //    /// <summary>
        //    /// 客户操作
        //    /// </summary>
        //    Customers ObjCustomersBLL = new Customers();
        //    var ObjModel = ObjCustomersBLL.GetByID(ObjQuotedPriceBLL.GetByID(QuotedID).CustomerID);
        //    ObjModel.State = (int)CustomerStates.StarCarrytask;
        //    ObjCustomersBLL.Update(ObjModel);


        //}


        /// <summary>
        /// 绑定二级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repSortListList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField ObjChanggeHide = (HiddenField)e.Item.FindControl("hideChangeKey");

            Repeater repCatageList = (Repeater)e.Item.FindControl("repCatageList");
            //绑定tab

            var DataSource = ObjQuotedPriceItemsBLL.GetByQuotedID(ObjChanggeHide.Value.ToInt32(), 1);
            repCatageList.DataSource = DataSource;
            repCatageList.DataBind();
        }


        /// <summary>
        /// 保存派工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click1(object sender, EventArgs e)
        {
            //for (int i = 0; i < repSortListList.Items.Count; i++)
            //{
            //    HiddenField ObjChanggeHide = (HiddenField)repSortListList.Items[i].FindControl("hideChangeKey");
            //    CreateCelebration(ObjChanggeHide.Value.ToInt32());
            //}
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var ObjCelModel = ObjCelebrationBLL.GetByQuotedID(QuotedID);
            var ParetnID = ObjCelModel.CelebrationID;
            var CelebrationID = ObjCelebrationBLL.Insert(new FL_Celebration()
            {

                CustomerID = CustomersID,
                OrderID = Request["OrderID"].ToInt32(),
                IsDelete = false,
                ParentCelebrationID = ObjCelModel.CelebrationID,
                QuotedID = QuotedID
            });

            CelebrationProductItem ObjCelebrationProductItemBLL = new CelebrationProductItem();
            for (int i = 0; i < repSortListList.Items.Count; i++)
            {
                HiddenField ObjChanggeHide = (HiddenField)repSortListList.Items[i].FindControl("hideChangeKey");

                //一级类别
                var ObjChangeFirstList = ObjQuotedPriceItemsBLL.GetByQuotedID(ObjChanggeHide.Value.ToInt32(), 1);

                //二级类别
                var ObjChangeSecondList = ObjQuotedPriceItemsBLL.GetByQuotedID(ObjChanggeHide.Value.ToInt32(), 2);

                //三级产品类别
                var ObjProductList = ObjQuotedPriceItemsBLL.GetByQuotedID(ObjChanggeHide.Value.ToInt32(), 3);
       
                //添加一级
 
                FL_CelebrationProductItem ObjCategoryforDispatching = new FL_CelebrationProductItem();
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
    }
}