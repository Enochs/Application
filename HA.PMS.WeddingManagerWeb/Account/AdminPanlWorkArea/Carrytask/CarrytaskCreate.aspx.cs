using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.HtmlControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskCreate : SystemPage
    {
        //财务成本明细 
        OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();

        MissionManager ObjMissManagerBLL = new MissionManager();
        /// <summary>
        /// 
        /// </summary>
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();


        Employee ObjEmployeeBLL = new Employee();

        /// <summary> 
        /// 执行表主表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        Dispatching ObjDispatchingBLL = new Dispatching();


        /// <summary>
        /// 执行表图片
        /// </summary>
        DispatchingImage ObjDispatchingImageBLL = new DispatchingImage();
        int DispatchingID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int CategoryID = 0;
        int State = 0;
        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            CategoryID = Request["CategoryID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }



        #region 页面状态

        /// <summary>
        /// 隐藏选择项目
        /// </summary>
        /// <returns></returns>
        public string HideSelectItem(object Level)
        {
            if (Level != null)
            {
                if (Level.ToString() == "0")
                {
                    return "style='color:#0b0fee;'";
                }
                else
                {
                    return "style='display:none;'";
                }
            }
            else
            {
                return "style='display:none;'";
            }

        }


        /// <summary>
        /// 隐藏选择产品
        /// </summary>
        /// <returns></returns>
        public string HideSelectProduct(object Level)
        {
            if (Level != null)
            {
                if (Level.ToString() == "2")
                {
                    return string.Empty; //"style='color:#0b0fee;'";
                }
                else
                {
                    return "style='display:none;'";
                }
            }
            else
            {
                return "style='display:none;'";
            }
        }


        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetProductByID(object Key)
        {
            if (Key != null)
            {
                return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion


        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.repfirst.DataSource = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, CategoryID, 1);
            this.repfirst.DataBind();

            var ObjDispatchingModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, CategoryID, 1);
            if (ObjDispatchingModel != null)
            {
                hideFirstEmpLoyeeID.Value = ObjDispatchingModel.EmployeeID.ToString();
                if (ObjDispatchingModel.EmployeeID > 0)
                {
                    txtFirstEmpLoyeeItem.Value = ObjEmployeeBLL.GetByID(ObjDispatchingModel.EmployeeID).EmployeeName;
                    hideOldEmployeeID.Value = ObjDispatchingModel.EmployeeID.ToString();
                }
                if (User.Identity.Name.ToInt32() == ObjDispatchingModel.EmployeeID)
                {
                    lblSelectTitle.Text = "选择其他人";
                }
                else
                {
                    lblSelectTitle.Text = "改派";
                }
            }

            //BinderCuseomerDate();
        }



        public string GetCatgoryEmpLoyeeName(object CatgoryID, object ItemLevel)
        {
            var ObjDispatchingModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, CatgoryID.ToString().ToInt32(), 2);
            if (ObjDispatchingModel != null)
            {
                hideFirstEmpLoyeeID.Value = ObjDispatchingModel.EmployeeID.ToString();
                if (ObjDispatchingModel.EmployeeID > 0)
                {
                    return ObjEmployeeBLL.GetByID(ObjDispatchingModel.EmployeeID).EmployeeName;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取产品或者服务
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public string GetProdtcrOrService(object Source)
        {
            return Source.ToString();
        }

        /// <summary>
        /// 绑定二级数据
        /// </summary\=
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            //TextBox ObjtxtPrice = (TextBox)e.Item.FindControl("txtSalePrice");
            //var ObjItemList = new List<FL_ProductforDispatching>();
            var DataList = new List<FL_ProductforDispatching>();
            //获取二级项目
            var NewList = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 1);
            DataList.Add(NewList);
            DataList.AddRange(ObjProductforDispatchingBLL.GetByParentCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32()).OrderBy(C => C.CategoryID).ToList());

            //ObjItemList.Reverse();
            ObjRep.DataSource = DataList;
            ObjRep.DataBind();
            //ObjRep.DataSource = DataList;
            //ObjRep.DataBind();


            //Repeater ObjRepProduct = (Repeater)e.Item.FindControl("repProductList");
            //ObjRepProduct.DataSource=

        }


        #endregion


        #region 新增项目
        /// <summary>
        /// 生成且保存一级大类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStarFirstpg_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 生成并保存二级大类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateSecond_Click(object sender, EventArgs e)
        {
            var CGKey = this.hideSecondValue.Value.Split(',');
            if (CGKey.Length > 0)
            {
                //获取选择的ID

                int[] ObjList = new int[CGKey.Length];
                int i = 0;
                foreach (string Key in CGKey)
                {

                    ObjList[i] = Key.ToInt32();
                    i++;
                }


                //查询出选择的类型
                var CategoryList = ObjQuotedCatgoryBLL.GetinKey(ObjList);

                FL_ProductforDispatching ObjModel;

                //临时保存
                foreach (var Objitem in CategoryList)
                {
                    //根据父亲级别查询二类 有 但是肯定只有一个
                    var ObjExistModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, Objitem.QCKey, 2);
                    if (ObjExistModel == null)
                    {

                        ObjModel = new FL_ProductforDispatching();
                        ObjModel.ItemLevel = 2;
                        ObjModel.OrderID = OrderID;
                        ObjModel.CustomerID = CustomerID;
                        ObjModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.QCKey).Title;
                        ObjModel.CategoryID = Objitem.QCKey;
                        ObjModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(Objitem.QCKey).Parent).Title;
                        ObjModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(Objitem.QCKey).Parent;
                        var ParentCatogry = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjModel.ParentCategoryID, 1);
                        ObjModel.IsDelete = false;
                        ObjModel.IsSvae = false;
                        ObjModel.Quantity = 1;
                        ObjModel.PurchasePrice = 0;
                        ObjModel.Requirement = "";
                        ObjModel.ParentDispatchingID = ObjDispatchingBLL.GetByID(DispatchingID).ParentDispatchingID;
                        ObjModel.DispatchingID = DispatchingID;
                        ObjModel.CreateEmployee = User.Identity.Name.ToInt32();
                        ObjModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjModel.Productproperty = Objitem.Productproperty;
                        ObjModel.Subtotal = ObjModel.Quantity * ObjModel.PurchasePrice; ;
                        ObjModel.Remark = "";
                        ObjModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjModel.SupplierName = ParentCatogry.SupplierName;
                        ObjModel.SupplierID = ParentCatogry.SupplierID;
                        ObjProductforDispatchingBLL.Insert(ObjModel);
                    }
                    else
                    {
                        //ObjExistModel.ParentCategoryName = OjbCategoryBLL.GetByID(Objitem.ParentID).CategoryName;
                        ObjExistModel.IsDelete = false;
                        ObjProductforDispatchingBLL.Update(ObjExistModel);
                    }
                }
                SavePageChange();
                BinderData();
            }
        }


        /// <summary>
        /// 生成并保存三级产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateThired_Click(object sender, EventArgs e)
        {

            //财务成本主体
            Cost ObjCostBLL = new Cost();



            FL_OrderfinalCost ObjFinalCostModel;
            var CGKey = this.hideThirdValue.Value.Split(',');
            if (CGKey.Length > 0)
            {

                int[] ObjKeyList = new int[CGKey.Length];
                int i = 0;
                foreach (string Key in CGKey)
                {

                    ObjKeyList[i] = Key.ToInt32();
                    i++;
                }

                var ProductList = ObjAllProductsBLL.GetByKeysList(ObjKeyList);



                FL_ProductforDispatching ObjSetModel = new FL_ProductforDispatching();
                foreach (var ObjProduct in ProductList)
                {


                    //根据父亲级别查询二类 有 但是肯定只有一个
                    //var ObjExistModel = ObjProductforDispatchingBLL.GetOnlyByProductID(DispatchingID, ObjProduct.Keys, 3);
                    //if (ObjExistModel == null)
                    //{
                    ObjSetModel = new FL_ProductforDispatching();
                    ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                    ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                    ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                    ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent;
                    ObjSetModel.ProductID = ObjProduct.Keys;
                    ObjSetModel.OrderID = OrderID;
                    ObjSetModel.CustomerID = CustomerID;
                    ObjSetModel.IsDelete = false;
                    ObjSetModel.IsSvae = false;
                    ObjSetModel.ItemLevel = 3;
                    ObjSetModel.UnitPrice = ObjProduct.SalePrice;
                    ObjSetModel.Specifications = ObjProduct.Specifications;
                    ObjSetModel.Unit = ObjProduct.Unit;
                    ObjSetModel.RowType = ObjProduct.Type;
                    ObjSetModel.Productproperty = ObjProduct.Productproperty;
                    if (ObjSetModel.RowType == 3)
                    {
                        ObjSetModel.Quantity = ObjProduct.Count.Value;
                        ObjSetModel.Subtotal = ObjSetModel.Quantity * ObjProduct.PurchasePrice;
                    }
                    else
                    {
                        ObjSetModel.Quantity = 1;
                        ObjSetModel.Subtotal = ObjSetModel.Quantity * ObjProduct.PurchasePrice;
                    }
                    ObjSetModel.NewAdd = true;
                    ObjSetModel.Requirement = ObjProduct.Explain;

                    ObjSetModel.PurchasePrice = ObjProduct.PurchasePrice;
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjSetModel.DispatchingID = DispatchingID;
                    ObjSetModel.CreateEmployee = User.Identity.Name.ToInt32();
                    ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjSetModel.Productproperty = ObjProduct.Productproperty;
                    ObjSetModel.RowType = ObjProduct.Type;


                    ObjSetModel.SupplierName = ObjProduct.SupplierName;
                    Supplier ObjSupplierBLL = new Supplier();

                    var ObjSUpplierModel = ObjSupplierBLL.GetByName(ObjSetModel.SupplierName);
                    if (ObjSUpplierModel == null)
                    {
                        ObjSetModel.SupplierID = 0;
                    }
                    else
                    {
                        ObjSetModel.SupplierID = ObjSUpplierModel.SupplierID;
                    }


                    ObjSetModel.ParentDispatchingID = ObjDispatchingBLL.GetByID(DispatchingID).ParentDispatchingID;
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjSetModel.Remark = ObjProduct.Remark;
                    ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();

                    ObjProductforDispatchingBLL.Insert(ObjSetModel);


                    #region 财务成本明细

                    //属于供应商的产品
                    if (ObjProduct.SupplierName != null && ObjProduct.SupplierName != "库房")
                    {
                        ObjFinalCostModel = new FL_OrderfinalCost();
                        ObjFinalCostModel.KindType = 1;
                        ObjFinalCostModel.IsDelete = false;
                        ObjFinalCostModel.CostKey = ObjCostBLL.GetByCustomerID(CustomerID).CostKey;
                        ObjFinalCostModel.CreateDate = DateTime.Now;
                        ObjFinalCostModel.CustomerID = CustomerID;
                        ObjFinalCostModel.CellPhone = string.Empty;
                        ObjFinalCostModel.InsideRemark = string.Empty;

                        if (ObjSetModel.ParentDispatchingID == 0)
                        {

                            ObjFinalCostModel.KindID = ObjSetModel.DispatchingID;
                        }
                        else
                        {
                            ObjFinalCostModel.KindID = ObjSetModel.ParentDispatchingID.Value;
                        }
                        ObjFinalCostModel.KindID = DispatchingID;
                        ObjFinalCostModel.ServiceContent = ObjProduct.SupplierName;
                        ObjFinalCostModel.PlannedExpenditure = ObjProduct.PurchasePrice.Value;
                        ObjFinalCostModel.ActualExpenditure = 0;
                        ObjFinalCostModel.Expenseaccount = string.Empty;

                        ObjFinalCostModel.ActualWorkload = string.Empty;
                        ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                    }


                    //专业团队
                    if (ObjProduct.Productproperty == 0)
                    {
                        ObjFinalCostModel = new FL_OrderfinalCost();
                        ObjFinalCostModel.KindType = 0;
                        ObjFinalCostModel.IsDelete = false;
                        ObjFinalCostModel.CostKey = ObjCostBLL.GetByCustomerID(CustomerID).CostKey;
                        ObjFinalCostModel.CreateDate = DateTime.Now;
                        ObjFinalCostModel.CustomerID = CustomerID;
                        ObjFinalCostModel.CellPhone = string.Empty;
                        ObjFinalCostModel.InsideRemark = string.Empty;

                        if (ObjSetModel.ParentDispatchingID == 0)
                        {

                            ObjFinalCostModel.KindID = ObjSetModel.DispatchingID;
                        }
                        else
                        {
                            ObjFinalCostModel.KindID = ObjSetModel.ParentDispatchingID.Value;
                        }
                        ObjFinalCostModel.ServiceContent = ObjProduct.ProductName;

                        ObjFinalCostModel.PlannedExpenditure = ObjProduct.PurchasePrice.Value;
                        ObjFinalCostModel.ActualExpenditure = 0;
                        ObjFinalCostModel.Expenseaccount = string.Empty;

                        ObjFinalCostModel.ActualWorkload = string.Empty;
                        ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                    }
                    #endregion
                    //}
                    //else
                    //{
                    //    ObjSetModel.ItemLevel = 3;
                    //    ObjExistModel.IsDelete = false;
                    //    ObjProductforDispatchingBLL.Update(ObjExistModel);
                    //}


                    //ObjSetModel = new FL_ProductForQuotedPrice();
                    //ObjSetModel.ProductID = ObjProduct.Keys;

                    //ObjSourceList.Add(ObjSetModel);GetByCategoryID(QuotedID, ObjHideKey.Value.ToInt32(),3);
                }
            }
            SavePageChange();
            //SaveQuotedPrice();
            BinderData();

        }



        public string GetKindImage(object Kind)
        {
            var ObjImageList = ObjDispatchingImageBLL.GetByKind(DispatchingID, Kind.ToString().ToInt32());
            string ImageList = string.Empty;
            foreach (var ObjImage in ObjImageList)
            {
                ImageList += "<img alt='' src='" + ObjImage.FileAddress + "' />";
            }
            //<img alt="" src="../../Images/Appraise/3.gif" />
            return ImageList;
        }
        /// <summary>
        /// 保存报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            SaveallChange();
            BinderData();
        }

        /// <summary>
        /// 单步保存页面所有更改数据
        /// </summary>
        private void SavePageChange()
        {

            int FirstEmpLoyeeID = hideFirstEmpLoyeeID.Value.ToInt32();

            var ObjFirstModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, CategoryID, 1);
            ObjFirstModel.EmployeeID = FirstEmpLoyeeID;

            ObjProductforDispatchingBLL.Update(ObjFirstModel);

            for (int P = 0; P < repfirst.Items.Count; P++)
            {

                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_ProductforDispatching ObjItem;
                string SuppName = string.Empty;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    HiddenField ObjHideCatogry = ObjrepList.Items[I].FindControl("hideCategoryID") as HiddenField;
                    HiddenField ObjHideCatgoryEmpLoyeeID = ObjrepList.Items[I].FindControl("hideNewEmpLoyeeID") as HiddenField;

                    //if (ObjHideCatgoryEmpLoyeeID.Value.ToInt32() > 0)
                    //{
                    //    var UpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHideCatogry.Value.ToInt32(), 2);
                    //    UpdateModel.EmployeeID = ObjHideCatgoryEmpLoyeeID.Value.ToInt32();
                    //    ObjProductforDispatchingBLL.Update(UpdateModel);
                    //}

                    //var UpdateList= ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, ObjItem.ItemLevel);
                    //foreach (var objUpdateModel in UpdateList)
                    //{ 

                    //}
                    Supplier ObjSupplierBLL = new Supplier();
                    //当点击的是一级分类指定供应商时的处理 (State=0)

                    if (hideOldEmployeeID.Value != hideFirstEmpLoyeeID.Value)
                    {
                        ObjItem.EmployeeID = hideFirstEmpLoyeeID.Value.ToInt32();

                    }

                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    //ObjItem.IsGet = true;

                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.IsDelete = false;
                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    //ObjItem.IsGet = true;

                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.IsDelete = false;
                    ObjItem.ServiceContent = ((TextBox)ObjrepList.Items[I].FindControl("txtProductName")).Text;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                    ObjItem.PurchasePrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    //  hideSuppID
                    ObjItem.PlannedCompletionTime = DateTime.Now;
                    ObjItem.FinishDate = DateTime.Now;
                    ObjProductforDispatchingBLL.Update(ObjItem);
                    var CostModel = ObjOrderfinalCostBLL.GetByPremissionnalName(ObjItem.ServiceContent, DispatchingID);
                    if (CostModel != null)
                    {
                        CostModel.PlannedExpenditure = ObjItem.PurchasePrice.Value;
                        ObjOrderfinalCostBLL.Update(CostModel);

                    }



                    ///当选择产品为供应商时 就向库房增加产品
                    ///当供应商不是上一个供应商时则删除上次提交的产品
                    ///仅为产品时才添加进入供应商产品库

                    Productcs ObjProductcsBLL = new Productcs();
                    Category ObjCategoryBLL = new Category();
                    if (ObjItem.ItemLevel == 3 && ObjSupplierBLL.GetByName(ObjItem.SupplierName) != null)
                    {
                        //待录入数据
                        int ItemSupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                        string ItemProductName = ObjItem.ServiceContent;//产品名称
                        decimal? ItemSalePrice = ((Label)ObjrepList.Items[I].FindControl("lblUnitPrice")).Text.ToDecimal();//销售价格
                        decimal? ItemProductPrice = ObjItem.PurchasePrice.Value; //产品成本价
                        int ItemCount = ObjItem.Quantity; //数量
                        int ItemCategoryID = ObjItem.CategoryID.Value;//分类

                        //获取供应商提供的该产品
                        var ObjProductcsQueryModel = ObjProductcsBLL.GetByAll().Where(C => C.SupplierID.Equals(ItemSupplierID) && C.ProductName.Equals(ItemProductName) && C.SalePrice.Equals(ItemSalePrice)).FirstOrDefault();
                        //待录入数据
                        int UnSignInCategoryID = 0;
                        //int UnSignInSupplierID = 0;
                        var ObjCategoryQueryModel = ObjCategoryBLL.GetByAll().Where(C => C.CategoryName.Contains("待分配产品")).FirstOrDefault();
                        //如果没有该实体对象，则创建“待分配产品”或“待分配供应商”
                        if (ObjCategoryQueryModel == null)
                        {
                            UnSignInCategoryID = ObjCategoryBLL.Insert(new FD_Category { CategoryName = "待分配产品", ParentID = 0, SortOrder = "9999", IsDelete = false });
                        }
                        else
                        {
                            UnSignInCategoryID = ObjCategoryQueryModel.CategoryID;
                        }
                        //如果该供应商没有提供该产品，就添加到带分配供应商产品。
                        if (ObjProductcsQueryModel == null)
                        {
                            //获取隐藏待分配实体对象

                            //var ObjSupplierQueryModel = ObjSupplierBLL.GetByAll().Where(C => C.Name.Contains("待分配供应商")).FirstOrDefault();


                            // if (ObjSupplierQueryModel == null)
                            // {
                            //     UnSignInSupplierID = ObjSupplierBLL.Insert(new FD_Supplier { Name = "待分配供应商", CreateEmployeeId = User.Identity.Name.ToInt32(), IsDelete = false });
                            // }
                            // else
                            // {
                            //     UnSignInSupplierID = ObjSupplierQueryModel.SupplierID;
                            // }

                            FD_Product ObjProductInsertModel = new FD_Product();
                            ObjProductInsertModel.CategoryID = UnSignInCategoryID;
                            //ObjProductInsertModel.SupplierID = UnSignInSupplierID;
                            ObjProductInsertModel.SupplierID = ItemSupplierID;
                            ObjProductInsertModel.ProductName = ItemProductName;
                            ObjProductInsertModel.ProductPrice = ItemProductPrice;
                            ObjProductInsertModel.SalePrice = ItemSalePrice;
                            ObjProductInsertModel.Count = 0 - ItemCount;
                            ObjProductInsertModel.IsDelete = false;
                            ObjProductcsBLL.Insert(ObjProductInsertModel);

                        }
                        else
                        {
                            FD_Product ObjProductUpdateModel = new FD_Product();
                            ObjProductUpdateModel.CategoryID = UnSignInCategoryID;
                            ObjProductUpdateModel.SupplierID = ItemSupplierID;
                            ObjProductUpdateModel.ProductName = ItemProductName;
                            ObjProductUpdateModel.ProductPrice = ItemProductPrice;
                            ObjProductUpdateModel.SalePrice = ItemSalePrice;
                            ObjProductUpdateModel.Count = ObjProductcsQueryModel.Count - ItemCount;
                            ObjProductcsBLL.Update(ObjProductUpdateModel);
                        }
                    }

                    if (hideOldEmployeeID.Value != hideFirstEmpLoyeeID.Value)
                    {
                        var ItemUpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjItem.CategoryID, 2);
                        if (ItemUpdateModel != null)
                        {
                            ItemUpdateModel.EmployeeID = ObjItem.EmployeeID;
                            ObjProductforDispatchingBLL.Update(ItemUpdateModel);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 保存所有更改数据
        /// </summary>
        private void SaveallChange()
        {

            int FirstEmpLoyeeID = hideFirstEmpLoyeeID.Value.ToInt32();

            var ObjFirstModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, CategoryID, 1);
            ObjFirstModel.EmployeeID = FirstEmpLoyeeID;

            ObjProductforDispatchingBLL.Update(ObjFirstModel);

            for (int P = 0; P < repfirst.Items.Count; P++)
            {

                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_ProductforDispatching ObjItem;
                string SuppName = string.Empty;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    HiddenField ObjHideCatogry = ObjrepList.Items[I].FindControl("hideCategoryID") as HiddenField;
                    HiddenField ObjHideCatgoryEmpLoyeeID = ObjrepList.Items[I].FindControl("hideNewEmpLoyeeID") as HiddenField;

                    //if (ObjHideCatgoryEmpLoyeeID.Value.ToInt32() > 0)
                    //{
                    //    var UpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHideCatogry.Value.ToInt32(), 2);
                    //    UpdateModel.EmployeeID = ObjHideCatgoryEmpLoyeeID.Value.ToInt32();
                    //    ObjProductforDispatchingBLL.Update(UpdateModel);
                    //}

                    //var UpdateList= ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, ObjItem.ItemLevel);
                    //foreach (var objUpdateModel in UpdateList)
                    //{ 

                    //}
                    Supplier ObjSupplierBLL = new Supplier();
                    //当点击的是一级分类指定供应商时的处理 (State=0)
                    if (State == 0)
                    {
                        //当分类等于二级或者三级的时候则继承一级分类供应商
                        if (ObjItem.ItemLevel == 2 || ObjItem.ItemLevel == 3)
                        {

                            //当为二级的时候处理
                            if (ObjItem.ItemLevel == 2)
                            { 
                                //非库房的才根据上级供应商更改
                                var UpdateModelList = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.ParentCategoryID, 1);

                                //判断父级不为空和库房 才继承父
                                if (UpdateModelList[0].SupplierName != string.Empty && UpdateModelList[0].SupplierName != "库房")
                                {

                                    ObjItem.SupplierName = UpdateModelList[0].SupplierName;
                                    ObjItem.SupplierID = UpdateModelList[0].SupplierID;
                                    ObjItem.RowType = 1;
                                }
                                else 
                                {
                                    //当父为空和库房则选取自身选项
                                    if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房" && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "新购入") 
                                    {
                                        ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                                        ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                                        ObjItem.RowType = 1;
                                    }
                                }
                            }
                            else
                            {
                                //非库房的才根据上级供应商更改
                                var UpdateModelList = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, 2);
                                if (UpdateModelList[0].SupplierName != string.Empty && UpdateModelList[0].SupplierName != "库房")
                                {
                                    //
                                    ObjItem.SupplierName = UpdateModelList[0].SupplierName;
                                    ObjItem.SupplierID = UpdateModelList[0].SupplierID;
                                    ObjItem.RowType = 1;
                                }
                                else
                                {
                                    if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房"&& ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "新购买")
                                    {
                                        ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                                        ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                                        ObjItem.RowType = 1;
                                    }
                                }
                            }
                            //ObjItem.Productproperty = 1;

                        }
                        else
                        {

                            if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房"&& ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "新购买")
                            {
                                ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                                ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                            }
                            else
                            {
                                if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value == "库房")
                                {
                                    ObjItem.RowType = 2;
                                }
                                ObjItem.SupplierID = 0;
                                ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                            }

                        }
                    }
                    else
                    {
                        if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房" && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "新购买")
                        {
                            ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                            ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                        }
                        else
                        {
                            ObjItem.SupplierID = 0;
                            ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                            if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value == "库房")
                            {
                                ObjItem.RowType = 2;
                            }
                        }

                    }
                    if (hideOldEmployeeID.Value != hideFirstEmpLoyeeID.Value)
                    {
                        ObjItem.EmployeeID = hideFirstEmpLoyeeID.Value.ToInt32();

                    }

                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    //ObjItem.IsGet = true;

                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.IsDelete = false;
                    ObjItem.ServiceContent = ((TextBox)ObjrepList.Items[I].FindControl("txtProductName")).Text;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;

                    if (ObjItem.SupplierName != "库房" && ObjItem.SupplierName != string.Empty)
                    {
                        ObjItem.RowType = 1;
                    }
                    ObjItem.PurchasePrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    //  hideSuppID
                    ObjItem.PlannedCompletionTime = DateTime.Now;
                    ObjItem.FinishDate = DateTime.Now;
                    ObjProductforDispatchingBLL.Update(ObjItem);
                    var CostModel = ObjOrderfinalCostBLL.GetByPremissionnalName(ObjItem.ServiceContent, DispatchingID);
                    if (CostModel != null)
                    {
                        CostModel.PlannedExpenditure = ObjItem.PurchasePrice.Value;
                        ObjOrderfinalCostBLL.Update(CostModel);

                    }



                    ///当选择产品为供应商时 就向库房增加产品
                    ///当供应商不是上一个供应商时则删除上次提交的产品
                    ///仅为产品时才添加进入供应商产品库

                    Productcs ObjProductcsBLL = new Productcs();
                    Category ObjCategoryBLL = new Category();
                    if (ObjItem.ItemLevel == 3 && ObjSupplierBLL.GetByName(ObjItem.SupplierName) != null)
                    {
                        //待录入数据
                        int ItemSupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                        string ItemProductName = ObjItem.ServiceContent;//产品名称
                        decimal? ItemSalePrice = ((Label)ObjrepList.Items[I].FindControl("lblUnitPrice")).Text.ToDecimal();//销售价格
                        decimal? ItemProductPrice = ObjItem.PurchasePrice.Value; //产品成本价
                        int ItemCount = ObjItem.Quantity; //数量
                        int ItemCategoryID = ObjItem.CategoryID.Value;//分类

                        //获取供应商提供的该产品
                        var ObjProductcsQueryModel = ObjProductcsBLL.GetByAll().Where(C => C.SupplierID.Equals(ItemSupplierID) && C.ProductName.Equals(ItemProductName) && C.SalePrice.Equals(ItemSalePrice)).FirstOrDefault();
                        //待录入数据
                        int UnSignInCategoryID = 0;
                        //int UnSignInSupplierID = 0;
                        var ObjCategoryQueryModel = ObjCategoryBLL.GetByAll().Where(C => C.CategoryName.Contains("待分配产品")).FirstOrDefault();
                        //如果没有该实体对象，则创建“待分配产品”或“待分配供应商”
                        if (ObjCategoryQueryModel == null)
                        {
                            UnSignInCategoryID = ObjCategoryBLL.Insert(new FD_Category { CategoryName = "待分配产品", ParentID = 0, SortOrder = "9999", IsDelete = false });
                        }
                        else
                        {
                            UnSignInCategoryID = ObjCategoryQueryModel.CategoryID;
                        }
                        //如果该供应商没有提供该产品，就添加到带分配供应商产品。
                        if (ObjProductcsQueryModel == null)
                        {
                            //获取隐藏待分配实体对象

                            //var ObjSupplierQueryModel = ObjSupplierBLL.GetByAll().Where(C => C.Name.Contains("待分配供应商")).FirstOrDefault();


                            // if (ObjSupplierQueryModel == null)
                            // {
                            //     UnSignInSupplierID = ObjSupplierBLL.Insert(new FD_Supplier { Name = "待分配供应商", CreateEmployeeId = User.Identity.Name.ToInt32(), IsDelete = false });
                            // }
                            // else
                            // {
                            //     UnSignInSupplierID = ObjSupplierQueryModel.SupplierID;
                            // }

                            FD_Product ObjProductInsertModel = new FD_Product();
                            ObjProductInsertModel.CategoryID = UnSignInCategoryID;
                            //ObjProductInsertModel.SupplierID = UnSignInSupplierID;
                            ObjProductInsertModel.SupplierID = ItemSupplierID;
                            ObjProductInsertModel.ProductName = ItemProductName;
                            ObjProductInsertModel.ProductPrice = ItemProductPrice;
                            ObjProductInsertModel.SalePrice = ItemSalePrice;
                            ObjProductInsertModel.Count = 0 - ItemCount;
                            ObjProductInsertModel.IsDelete = false;
                            ObjProductcsBLL.Insert(ObjProductInsertModel);

                        }
                        else
                        {
                            FD_Product ObjProductUpdateModel = new FD_Product();
                            ObjProductUpdateModel.CategoryID = UnSignInCategoryID;
                            ObjProductUpdateModel.SupplierID = ItemSupplierID;
                            ObjProductUpdateModel.ProductName = ItemProductName;
                            ObjProductUpdateModel.ProductPrice = ItemProductPrice;
                            ObjProductUpdateModel.SalePrice = ItemSalePrice;
                            ObjProductUpdateModel.Count = ObjProductcsQueryModel.Count - ItemCount;
                            ObjProductcsBLL.Update(ObjProductUpdateModel);
                        }
                    }

                    if (hideOldEmployeeID.Value != hideFirstEmpLoyeeID.Value)
                    {
                        var ItemUpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjItem.CategoryID, 2);
                        if (ItemUpdateModel != null)
                        {
                            ItemUpdateModel.EmployeeID = ObjItem.EmployeeID;
                            ObjProductforDispatchingBLL.Update(ItemUpdateModel);
                        }
                    }
                }
            }
            //保存分项合计
            //HiddenField ObjHiddKey = (HiddenField)repfirst.Items[P].FindControl("hideKey");
            //ObjItem = new FL_ProductforDispatching();
            //ObjItem = ObjQuotedPriceItemsBLL.GetByID(ObjHiddKey.Value.ToInt32());
            //ObjItem.ItemSaleAmount = ((TextBox)repfirst.Items[P].FindControl("txtSaleItem")).Text.ToDecimal();
            //ObjItem.ItemAmount = ItemAmoutMoney;
            //ItemAmoutMoney = 0;
            //ObjQuotedPriceItemsBLL.Update(ObjItem);

            //    //更新报价单主体





            //}
            //JavaScriptTools.AlertWindow("保存成功!", Page);
            BinderData();
        }

        #endregion


        #region 删除修改
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)e.Item.FindControl("hidePriceKey")).Value.ToInt32());
            if (ObjItem != null)
            {

                //Supplier ObjSupplierBLL = new Supplier();
                //ObjItem.SupplierName = ((HtmlInputText)e.Item.FindControl("txtSuppName")).Value;
                //ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;
                ObjProductforDispatchingBLL.Delete(ObjItem);
                BinderData();
            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveItem")
            {

                SavePageChange();
                JavaScriptTools.AlertWindow("保存完毕！", Page);
            }
        }

        /// <summary>
        /// 保存派工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (hideOldEmployeeID.Value != hideFirstEmpLoyeeID.Value)
            {
                //进入订单状态
                DispatchingState ObjDispatchingStateBLL = new DispatchingState();
                FL_DispatchingState ObjispatchingStateModel = new FL_DispatchingState();
                ObjispatchingStateModel.DispatchingID = DispatchingID;
                ObjispatchingStateModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                ObjispatchingStateModel.CreateDate = DateTime.Now;
                ObjispatchingStateModel.IsUse = false;
                ObjispatchingStateModel.State = 1;
                ObjispatchingStateModel.StateEmpLoyee = hideFirstEmpLoyeeID.Value.ToInt32();
                ObjispatchingStateModel.StateItemLevel = 1;
                ObjispatchingStateModel.StateCatgoryID = CategoryID;
                ObjispatchingStateModel.StateParentCatgory = ObjQuotedCatgoryBLL.GetByID(CategoryID).Parent;
                ObjDispatchingStateBLL.Insert(ObjispatchingStateModel);
                Order ObjOrderBLL = new Order();

                //DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1
                ObjMissManagerBLL.WeddingMissionCreate(ObjOrderBLL.GetByID(OrderID).CustomerID.Value, 1, (int)MissionTypes.MyDispatching, DateTime.Now, hideFirstEmpLoyeeID.Value.ToInt32(), "?CustomerID=" + ObjOrderBLL.GetByID(OrderID).CustomerID.Value + "&DispatchingID=" + DispatchingID, MissionChannel.DispatchingManager, hideFirstEmpLoyeeID.Value.ToInt32(), DispatchingID);

                // ObjDispatchingStateBLL.CheckState(DispatchingID, Request["StateKey"].ToInt32(), User.Identity.Name.ToInt32());
                //  ObjDispatchingStateBLL.DeleteforEmployee(Request["StateKey"].ToInt32());

            }
            SavePageChange();


        }


        /// <summary>
        /// 更换产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangeProduct_Click(object sender, EventArgs e)
        {

            var ObjProductModel = ObjAllProductsBLL.GetByID(hideThirdValue.Value.ToInt32());
            var ObjItem = ObjProductforDispatchingBLL.GetByID(hideChange.Value.ToInt32());
            if (ObjItem != null)
            {
                Supplier ObjSupplierBLL = new Supplier();

                ObjItem.SupplierName = ObjProductModel.SupplierName;
                var ObjSUpplierModel = ObjSupplierBLL.GetByName(ObjItem.SupplierName);
                if (ObjSUpplierModel == null)
                {
                    ObjItem.SupplierID = 0;
                }
                else
                {
                    ObjItem.SupplierID = ObjSUpplierModel.SupplierID;
                }

                ObjItem.ProductID = ObjProductModel.Keys;
                ObjItem.UnitPrice = ObjProductModel.SalePrice;
                ObjItem.ServiceContent = ObjProductModel.ProductName;
                ObjItem.Productproperty = ObjProductModel.Productproperty;
                ObjProductforDispatchingBLL.Update(ObjItem);
                BinderData();
            }

        }
        #endregion

        protected void hideFirstEmpLoyeeID_ValueChanged(object sender, EventArgs e)
        {
            btnSaveItem_Click(sender, e);
        }


        /// <summary>
        /// 保存指定供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavesupperSave_Click(object sender, EventArgs e)
        {
            SaveallChange();
        }

        protected void btnSaveSuppertoOther_Click(object sender, EventArgs e)
        {
            State = 1;
            SaveallChange();
        }


    }
}