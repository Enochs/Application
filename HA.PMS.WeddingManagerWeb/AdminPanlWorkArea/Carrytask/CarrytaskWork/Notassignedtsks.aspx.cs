using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.HtmlControls;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class Notassignedtsks : SystemPage
    {


        //财务成本明细
        OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();
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
        /// 项目
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemBLL = new QuotedPriceItems();

        /// <summary>
        /// 
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();


        /// <summary>
        /// 执行表图片
        /// </summary>
        DispatchingImage ObjDispatchingImageBLL = new DispatchingImage();
        /// <summary>
        /// 报价单主表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        int DispatchingID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int CategoryID = 0;
        int State = 0;
        List<int> ObjKeyList = new List<int>();

        public class KeyClassEquers : IEqualityComparer<FL_ProductforDispatching>
        {
            public bool Equals(FL_ProductforDispatching x, FL_ProductforDispatching y)
            {
                if (x.DispatchingID == y.DispatchingID)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(FL_ProductforDispatching obj)
            {
                return 0;
            }
        }

        #region 页面初始化
        /// <summary>
        /// 绑定界面
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            CategoryID = Request["CategoryID"].ToInt32();
            if (!IsPostBack)
            {
                if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
                {
                    hideMineNeedState.Value = "0";
                }
                else
                {
                    hideMineNeedState.Value = "1";
                }
                BinderData();
            }
        }
        #endregion

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

        #region 页面状态

        public string HideCreateitemAndProduct(object CategID, object Level)
        {
            if (CategID != null)
            {
                if (CategID.ToString() != string.Empty)
                {
                    var ObjModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, CategID.ToString().ToInt32(), Level.ToString().ToInt32());
                    if (ObjModel != null)
                    {
                        if (ObjModel.EmployeeID.ToString() != User.Identity.Name)
                        {

                            return "style='display:none';";
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return "style='display:none';";
                    }

                }
            }

            return "style='display:none';";
        }

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

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

            //有类别
            int MineState = 0;
            this.repfirst.DataSource = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 1, out MineState);
            this.repfirst.DataBind();

            if (State == 0)
            {
                this.repfirst.DataSource = null;
                this.repfirst.DataBind();
            }
            var ObjDisPatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);

            //判断是否为总派工人
            if (ObjDisPatchingModel.EmployeeID == User.Identity.Name.ToInt32())
            {
                hideMineState.Value = "1";

                hideFirstEmpLoyeeID.Visible = true;
                //firstSelect.Visible = true;
            }
            else
            {

                hideFirstEmpLoyeeID.Visible = false;
                //firstSelect.Visible = false;
            }
        }
        #endregion

        #region 获取员工姓名
        /// <summary>
        /// 获取
        /// </summary>
        public string GetCatgoryEmpLoyeeName(object CatgoryID)
        {
            try
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
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }
        #endregion

        #region 获取产品
        /// <summary>
        /// 获取产品或者服务
        /// </summary>
        public string GetProdtcrOrService(object Source)
        {
            return Source.ToString();
        }
        #endregion

        #region 绑定二级数据
        /// <summary>
        /// 绑定二级数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            var DataList = new List<FL_ProductforDispatching>();

            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");

            //获取二级项目
            var NewList = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 1);
            DataList.Add(NewList);
            var ObjProductList = ObjProductforDispatchingBLL.GetByParentCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32()).Where(C => C.IsGet == null || C.IsGet == false).OrderBy(C => C.CategoryID).ToList();
            if (ObjProductList.Count(C => C.ItemLevel == 3) > 0)
            {
                DataList.AddRange(ObjProductList);
                State = 1;

                ObjRep.DataSource = DataList;
                ObjRep.DataBind();
            }

        }
        #endregion

        #region 保存一级大类
        /// <summary>
        /// 生成且保存一级大类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStarFirstpg_Click(object sender, EventArgs e)
        {
            var CGKey = this.hidePgValue.Value.Split(',');
            if (CGKey.Length > 0)
            {
                int[] ObjList = new int[CGKey.Length];
                int i = 0;
                foreach (string Key in CGKey)
                {

                    ObjList[i] = Key.ToInt32();
                    i++;
                }
                var OjbCategoryBLL = new QuotedCatgory();
                FL_ProductforDispatching ObjCategoryForQuotedPriceModel = new FL_ProductforDispatching();
                var ObjCategoryList = OjbCategoryBLL.GetinKey(ObjList);

                //临时保存到数据库
                foreach (var ObjCategorItem in ObjCategoryList)
                {
                    //先查询是否已经添加 已经添加就只做修改
                    var ObjExistModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjCategorItem.QCKey, 1);
                    if (ObjExistModel == null)
                    {
                        ObjCategoryForQuotedPriceModel = new FL_ProductforDispatching();
                        ObjCategoryForQuotedPriceModel.CategoryID = ObjCategorItem.QCKey;
                        ObjCategoryForQuotedPriceModel.CategoryName = ObjCategorItem.Title;
                        ObjCategoryForQuotedPriceModel.ParentCategoryName = ObjCategorItem.Title;
                        ObjCategoryForQuotedPriceModel.ParentCategoryID = 0;
                        ObjCategoryForQuotedPriceModel.ItemLevel = 1;
                        ObjCategoryForQuotedPriceModel.OrderID = OrderID;
                        ObjCategoryForQuotedPriceModel.IsDelete = false;
                        ObjCategoryForQuotedPriceModel.IsSvae = false;
                        ObjCategoryForQuotedPriceModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjCategoryForQuotedPriceModel.CustomerID = CustomerID;
                        ObjCategoryForQuotedPriceModel.Requirement = "";
                        ObjCategoryForQuotedPriceModel.DispatchingID = DispatchingID;
                        ObjProductforDispatchingBLL.Insert(ObjCategoryForQuotedPriceModel);
                    }
                    else
                    {
                        ObjExistModel.IsDelete = false;
                        ObjProductforDispatchingBLL.Update(ObjExistModel);
                    }
                }
                SaveallChange();

                BinderData();
            }
        }
        #endregion

        #region 保存二级大类
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
                        ObjModel.IsDelete = false;
                        ObjModel.IsSvae = false;
                        ObjModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjModel.TaskEmpLoyee = User.Identity.Name.ToInt32();
                        ObjModel.DispatchingID = DispatchingID;
                        ObjModel.CreateEmployee = User.Identity.Name.ToInt32();
                        ObjModel.Productproperty = 0;
                        ObjModel.Requirement = string.Empty;
                        ObjModel.Quantity = 1;
                        ObjModel.Remark = "";
                        ObjModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjModel.ParentDispatchingID = ObjDispatchingBLL.GetByID(DispatchingID).ParentDispatchingID;
                        ObjProductforDispatchingBLL.Insert(ObjModel);
                    }
                    else
                    {

                        ObjExistModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjExistModel.TaskEmpLoyee = User.Identity.Name.ToInt32();
                        //ObjExistModel.ParentCategoryName = OjbCategoryBLL.GetByID(Objitem.ParentID).CategoryName;
                        ObjExistModel.IsDelete = false;
                        ObjProductforDispatchingBLL.Update(ObjExistModel);
                    }
                }


            }

            BinderData();
        }
        #endregion

        #region 保存三级产品
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
                    ObjSetModel.IsDelete = false;
                    ObjSetModel.IsSvae = false;
                    ObjSetModel.ItemLevel = 3;
                    ObjSetModel.UnitPrice = ObjProduct.SalePrice;
                    ObjSetModel.Specifications = ObjProduct.Specifications;
                    ObjSetModel.Unit = ObjProduct.Unit;
                    ObjSetModel.RowType = ObjProduct.Type;
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
                    ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();

                    ObjSetModel.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    ObjSetModel.DispatchingID = DispatchingID;
                    ObjSetModel.CreateEmployee = User.Identity.Name.ToInt32();
                    ObjSetModel.Productproperty = ObjProduct.Productproperty;
                    ObjSetModel.Requirement = string.Empty;
                    ObjSetModel.Remark = ObjProduct.Remark;
                    ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjSetModel.PurchasePrice = ObjProduct.PurchasePrice;
                    ObjSetModel.SupplierName = ObjProduct.SupplierName;
                    ObjSetModel.ParentDispatchingID = ObjDispatchingBLL.GetByID(DispatchingID).ParentDispatchingID;
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


                    ObjSetModel.OrderID = OrderID;
                    ObjSetModel.CustomerID = CustomerID;
                    ObjProductforDispatchingBLL.Insert(ObjSetModel);

                    #region 财务成本明细
                    //if (ObjProduct.SupplierName != null && ObjProduct.SupplierName != "库房")
                    //{
                    //    ObjFinalCostModel = new FL_OrderfinalCost();
                    //    ObjFinalCostModel.KindType = 1;
                    //    ObjFinalCostModel.IsDelete = false;
                    //    ObjFinalCostModel.CostKey = ObjCostBLL.GetByCustomerID(CustomerID).CostKey;
                    //    ObjFinalCostModel.CreateDate = DateTime.Now;
                    //    ObjFinalCostModel.CustomerID = CustomerID;
                    //    ObjFinalCostModel.CellPhone = string.Empty;
                    //    ObjFinalCostModel.InsideRemark = string.Empty;
                    //    if (ObjSetModel.ParentDispatchingID == 0)
                    //    {

                    //        ObjFinalCostModel.KindID = ObjSetModel.DispatchingID;
                    //    }
                    //    else
                    //    {
                    //        ObjFinalCostModel.KindID = ObjSetModel.ParentDispatchingID.Value;
                    //    }
                    //    ObjFinalCostModel.ServiceContent = ObjProduct.SupplierName;
                    //    ObjFinalCostModel.PlannedExpenditure = ObjProduct.PurchasePrice.Value;
                    //    ObjFinalCostModel.ActualExpenditure = 0;
                    //    ObjFinalCostModel.Expenseaccount = string.Empty;

                    //    ObjFinalCostModel.ActualWorkload = string.Empty;
                    //    ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                    //}

                    //if (ObjProduct.Productproperty == 0)
                    //{
                    //    ObjFinalCostModel = new FL_OrderfinalCost();
                    //    ObjFinalCostModel.KindType = 0;
                    //    ObjFinalCostModel.IsDelete = false;
                    //    ObjFinalCostModel.CostKey = ObjCostBLL.GetByCustomerID(CustomerID).CostKey;
                    //    ObjFinalCostModel.CreateDate = DateTime.Now;
                    //    ObjFinalCostModel.CustomerID = CustomerID;
                    //    ObjFinalCostModel.CellPhone = string.Empty;
                    //    ObjFinalCostModel.InsideRemark = string.Empty;
                    //    if (ObjSetModel.ParentDispatchingID == 0)
                    //    {

                    //        ObjFinalCostModel.KindID = ObjSetModel.DispatchingID;
                    //    }
                    //    else
                    //    {
                    //        ObjFinalCostModel.KindID = ObjSetModel.ParentDispatchingID.Value;
                    //    }
                    //    ObjFinalCostModel.ServiceContent = ObjProduct.ProductName;
                    //    ObjFinalCostModel.PlannedExpenditure = ObjProduct.PurchasePrice.Value;
                    //    ObjFinalCostModel.ActualExpenditure = 0;
                    //    ObjFinalCostModel.Expenseaccount = string.Empty;

                    //    ObjFinalCostModel.ActualWorkload = string.Empty;
                    //    ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                    //}
                    #endregion
                }
            }

            //SaveQuotedPrice();
            BinderData();

        }
        #endregion

        #region 点击保存按钮
        /// <summary>
        /// 保存报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            SaveallChange();
            // BinderData();
        }
        #endregion


        private void SaveaPageChange()
        {

            int FirstEmpLoyeeID = hideFirstEmpLoyeeID.Value.ToInt32();
            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                //int ParentEmpLoyeeID = 0;
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_ProductforDispatching ObjItem;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    if (ObjItem == null)
                    {
                        continue;
                    }
                    HiddenField ObjHideCatogry = ObjrepList.Items[I].FindControl("hideCategoryID") as HiddenField;
                    HiddenField ObjHideCatgoryEmpLoyeeID = ObjrepList.Items[I].FindControl("hideNewEmpLoyeeID") as HiddenField;

                    ObjItem.Updatetime = DateTime.Now;
                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.IsDelete = false;
                    ObjItem.NewAdd = true;
                    ObjItem.Updatetime = DateTime.Now;

                    ObjItem.ServiceContent = ((TextBox)ObjrepList.Items[I].FindControl("txtProductName")).Text;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    ObjItem.PlannedCompletionTime = DateTime.Now;
                    ObjItem.FinishDate = DateTime.Now;
                    ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                    ObjItem.PurchasePrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    ObjProductforDispatchingBLL.Update(ObjItem);


                    if (ObjHideCatgoryEmpLoyeeID.Value.ToInt32() > 0)
                    {
                        var ItemUpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjItem.CategoryID, 2);
                        ItemUpdateModel.EmployeeID = ObjHideCatgoryEmpLoyeeID.Value.ToInt32();
                        ObjProductforDispatchingBLL.Update(ItemUpdateModel);
                    }

                    var CostModel = ObjOrderfinalCostBLL.GetByPremissionnalName(ObjItem.ServiceContent, DispatchingID);
                    if (CostModel != null)
                    {
                        CostModel.PlannedExpenditure = ObjItem.PurchasePrice.Value;
                        ObjOrderfinalCostBLL.Update(CostModel);

                    }



                    /////当选择产品为供应商时 就向库房增加产品
                    /////当供应商不是上一个供应商时则删除上次提交的产品
                    /////仅为产品时才添加进入供应商产品库
                    //Supplier ObjSupplierBLL = new Supplier();
                    //Productcs ObjProductcsBLL = new Productcs();
                    //Category ObjCategoryBLL = new Category();
                    //if (ObjItem.ItemLevel == 3 && ObjSupplierBLL.GetByName(ObjItem.SupplierName) != null)
                    //{
                    //    //待录入数据
                    //    int ItemSupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                    //    string ItemProductName = ObjItem.ServiceContent;//产品名称
                    //    decimal? ItemSalePrice = ObjItem.PurchasePrice.Value; //销售价格
                    //    int ItemCount = ObjItem.Quantity; //数量
                    //    int ItemCategoryID = ObjItem.CategoryID.Value;//分类

                    //    //获取供应商提供的该产品
                    //    var ObjProductcsQueryModel = ObjProductcsBLL.GetByAll().Where(C => C.SupplierID.Equals(ItemSupplierID) && C.ProductName.Equals(ItemProductName) && C.SalePrice.Equals(ItemSalePrice)).FirstOrDefault();

                    //    //如果该供应商没有提供该产品，就添加到带分配供应商产品。
                    //    if (ObjProductcsQueryModel == null)
                    //    {
                    //        //获取隐藏待分配实体对象
                    //        var ObjCategoryQueryModel = ObjCategoryBLL.GetByAll().Where(C => C.CategoryName.Contains("待分配产品")).FirstOrDefault();
                    //        //var ObjSupplierQueryModel = ObjSupplierBLL.GetByAll().Where(C => C.Name.Contains("待分配供应商")).FirstOrDefault();

                    //        //待录入数据
                    //        int UnSignInCategoryID = 0;
                    //        //int UnSignInSupplierID = 0;

                    //        //如果没有该实体对象，则创建“待分配产品”或“待分配供应商”
                    //        if (ObjCategoryQueryModel == null)
                    //        {
                    //            UnSignInCategoryID = ObjCategoryBLL.Insert(new FD_Category { CategoryName = "待分配产品", ParentID = 0, SortOrder = "9999", IsDelete = false });
                    //        }
                    //        else
                    //        {
                    //            UnSignInCategoryID = ObjCategoryQueryModel.CategoryID;
                    //        }
                    //        // if (ObjSupplierQueryModel == null)
                    //        // {
                    //        //     UnSignInSupplierID = ObjSupplierBLL.Insert(new FD_Supplier { Name = "待分配供应商", CreateEmployeeId = User.Identity.Name.ToInt32(), IsDelete = false });
                    //        // }
                    //        // else
                    //        // {
                    //        //     UnSignInSupplierID = ObjSupplierQueryModel.SupplierID;
                    //        // }

                    //        FD_Product ObjProductInsertModel = new FD_Product();
                    //        ObjProductInsertModel.CategoryID = UnSignInCategoryID;
                    //        //ObjProductInsertModel.SupplierID = UnSignInSupplierID;
                    //        ObjProductInsertModel.SupplierID = ItemSupplierID;
                    //        ObjProductInsertModel.ProductName = ItemProductName;
                    //        ObjProductInsertModel.SalePrice = ItemSalePrice;
                    //        ObjProductInsertModel.Count = 0 - ItemCount;
                    //        ObjProductInsertModel.IsDelete = false;
                    //        ObjProductcsBLL.Insert(ObjProductInsertModel);

                    //    }
                    //    else
                    //    {
                    //        FD_Product ObjProductUpdateModel = new FD_Product();
                    //        ObjProductUpdateModel.CategoryID = ItemCategoryID;
                    //        ObjProductUpdateModel.SupplierID = ItemSupplierID;
                    //        ObjProductUpdateModel.ProductName = ItemProductName;
                    //        ObjProductUpdateModel.SalePrice = ItemSalePrice;
                    //        ObjProductUpdateModel.Count = ObjProductcsQueryModel.Count - ItemCount;
                    //        ObjProductcsBLL.Update(ObjProductUpdateModel);
                    //    }
                    //}
                }
            }


        }

        #region 保存所有
        /// <summary>
        /// 保存所有
        /// </summary>
        private void SaveallChange()
        {

            int FirstEmpLoyeeID = hideFirstEmpLoyeeID.Value.ToInt32();
            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                //int ParentEmpLoyeeID = 0;
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_ProductforDispatching ObjItem;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    if (ObjItem == null)
                    {
                        continue;
                    }
                    HiddenField ObjHideCatogry = ObjrepList.Items[I].FindControl("hideCategoryID") as HiddenField;
                    HiddenField ObjHideCatgoryEmpLoyeeID = ObjrepList.Items[I].FindControl("hideNewEmpLoyeeID") as HiddenField;

                    ObjItem.Updatetime = DateTime.Now;
                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.IsDelete = false;
                    ObjItem.NewAdd = true;
                    ObjItem.Updatetime = DateTime.Now;
                    Supplier ObjSupplierBLL = new Supplier();
                    //if (State == 0)
                    //{
                    //    if (ObjItem.ItemLevel == 3)
                    //    {

                    //        var UpdateModelList = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, 2);
                    //        if (UpdateModelList[0].SupplierName != string.Empty && UpdateModelList[0].SupplierName != "库房")
                    //        {
                    //            ObjItem.SupplierName = UpdateModelList[0].SupplierName;
                    //            ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;
                    //            ObjItem.RowType = 1;
                    //        }
                    //        else
                    //        {
                    //            ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                    //            //ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;
                    //            ObjItem.RowType = 2;
                    //        }
                    //        //ObjItem.Productproperty = 1;

                    //    }
                    //    else
                    //    {
                    //        if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房")
                    //        {
                    //            ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                    //            ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                    //        }
                    //        else
                    //        {
                    //            if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value == "库房")
                    //            {
                    //                ObjItem.RowType = 2;
                    //            }
                    //            ObjItem.SupplierID = 0;
                    //            ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                    //        }

                    //    }
                    //}
                    //else
                    //{
                    //    if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房")
                    //    {
                    //        ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                    //        ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                    //    }
                    //    else
                    //    {
                    //        ObjItem.SupplierID = 0;
                    //        ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                    //        if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value == "库房")
                    //        {
                    //            ObjItem.RowType = 2;
                    //        }
                    //    }

                    //}
                    ObjItem.ServiceContent = ((TextBox)ObjrepList.Items[I].FindControl("txtProductName")).Text;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    ObjItem.PlannedCompletionTime = DateTime.Now;
                    ObjItem.FinishDate = DateTime.Now;
                    //ObjItem.SupplierName = ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                    ObjItem.PurchasePrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    if (ObjItem.SupplierName != "库房" && ObjItem.SupplierName != string.Empty)
                    {
                        ObjItem.RowType = 1;
                    }
                    ObjProductforDispatchingBLL.Update(ObjItem);


                    if (ObjHideCatgoryEmpLoyeeID.Value.ToInt32() > 0)
                    {
                        var ItemUpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjItem.CategoryID, 2);
                        ItemUpdateModel.EmployeeID = ObjHideCatgoryEmpLoyeeID.Value.ToInt32();
                        ObjProductforDispatchingBLL.Update(ItemUpdateModel);
                    }

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
                        decimal? ItemSalePrice = ObjItem.PurchasePrice.Value; //销售价格
                        int ItemCount = ObjItem.Quantity; //数量
                        int ItemCategoryID = ObjItem.CategoryID.Value;//分类

                        //获取供应商提供的该产品
                        var ObjProductcsQueryModel = ObjProductcsBLL.GetByAll().Where(C => C.SupplierID.Equals(ItemSupplierID) && C.ProductName.Equals(ItemProductName) && C.SalePrice.Equals(ItemSalePrice)).FirstOrDefault();

                        //如果该供应商没有提供该产品，就添加到带分配供应商产品。
                        if (ObjProductcsQueryModel == null)
                        {
                            //获取隐藏待分配实体对象
                            var ObjCategoryQueryModel = ObjCategoryBLL.GetByAll().Where(C => C.CategoryName.Contains("待分配产品")).FirstOrDefault();
                            //var ObjSupplierQueryModel = ObjSupplierBLL.GetByAll().Where(C => C.Name.Contains("待分配供应商")).FirstOrDefault();

                            //待录入数据
                            int UnSignInCategoryID = 0;
                            //int UnSignInSupplierID = 0;

                            //如果没有该实体对象，则创建“待分配产品”或“待分配供应商”
                            if (ObjCategoryQueryModel == null)
                            {
                                UnSignInCategoryID = ObjCategoryBLL.Insert(new FD_Category { CategoryName = "待分配产品", ParentID = 0, SortOrder = "9999", IsDelete = false });
                            }
                            else
                            {
                                UnSignInCategoryID = ObjCategoryQueryModel.CategoryID;
                            }
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
                            ObjProductInsertModel.SalePrice = ItemSalePrice;
                            ObjProductInsertModel.Count = 0 - ItemCount;
                            ObjProductInsertModel.IsDelete = false;
                            ObjProductcsBLL.Insert(ObjProductInsertModel);

                        }
                        else
                        {
                            FD_Product ObjProductUpdateModel = new FD_Product();
                            ObjProductUpdateModel.CategoryID = ItemCategoryID;
                            ObjProductUpdateModel.SupplierID = ItemSupplierID;
                            ObjProductUpdateModel.ProductName = ItemProductName;
                            ObjProductUpdateModel.SalePrice = ItemSalePrice;
                            ObjProductUpdateModel.Count = ObjProductcsQueryModel.Count - ItemCount;
                            ObjProductcsBLL.Update(ObjProductUpdateModel);
                        }
                    }
                }
            }


            BinderData();

        }
        #endregion

        #region 绑定事件  执行删除功能
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
                ObjProductforDispatchingBLL.Delete(ObjItem);
                BinderData();
            }
        }
        #endregion

        #region 保存数据 ItemCommand
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveItem")
            {
                //保存整个页面

                SaveaPageChange();
            }
            else
            {
                SaveaPageChange();
                //保存派工人
                HiddenField ObjEmployeeID = (HiddenField)e.Item.FindControl("hideSetFirstEmpLoyeeID");
                HiddenField ObjCatogryID = (HiddenField)e.Item.FindControl("hideCatgoryID");
                if (ObjEmployeeID != null)
                {
                    if (ObjEmployeeID.Value.ToInt32() > 0)
                    {
                        DispatchingState ObjDispatchingStateBLL = new DispatchingState();
                        ObjDispatchingStateBLL.SetInsertByEmployeeAndCatogry(User.Identity.Name.ToInt32(), ObjCatogryID.Value.ToInt32(), DispatchingID);
                        FL_DispatchingState ObjispatchingStateModel = new FL_DispatchingState();
                        ObjispatchingStateModel.DispatchingID = DispatchingID;
                        ObjispatchingStateModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                        ObjispatchingStateModel.CreateDate = DateTime.Now;
                        ObjispatchingStateModel.IsUse = false;
                        ObjispatchingStateModel.State = 1;
                        ObjispatchingStateModel.StateEmpLoyee = ObjEmployeeID.Value.ToInt32();
                        ObjispatchingStateModel.StateItemLevel = 1;
                        ObjispatchingStateModel.StateCatgoryID = ObjCatogryID.Value.ToInt32();
                        ObjispatchingStateModel.StateParentCatgory = ObjQuotedCatgoryBLL.GetByID(ObjCatogryID.Value.ToInt32()).Parent;
                        ObjDispatchingStateBLL.Insert(ObjispatchingStateModel);

                        Order ObjOrderBLL = new Order();
                        MissionManager ObjMissManagerBLL = new MissionManager();
                        int MineState = 0;
                        var MainList = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 1, out MineState);
                        var SecondList = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 2, out MineState);
                        var ThirdeList = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 3, out MineState);
                        foreach (var ObiSeModel in SecondList)
                        {
                            ObiSeModel.EmployeeID = ObjispatchingStateModel.StateEmpLoyee;
                            ObiSeModel.TaskEmpLoyee = ObjispatchingStateModel.StateEmpLoyee;
                            ObjProductforDispatchingBLL.Update(ObiSeModel);
                        }


                        foreach (var ObiThModel in ThirdeList)
                        {
                            ObiThModel.EmployeeID = ObjispatchingStateModel.StateEmpLoyee;
                            ObiThModel.TaskEmpLoyee = ObjispatchingStateModel.StateEmpLoyee;
                            ObjProductforDispatchingBLL.Update(ObiThModel);
                        }

                        var ObdateModel = MainList.FirstOrDefault(C => C.CategoryID == ObjCatogryID.Value.ToInt32() && C.ItemLevel == 1);
                        ObdateModel.EmployeeID = ObjispatchingStateModel.StateEmpLoyee;
                        ObdateModel.TaskEmpLoyee = ObjispatchingStateModel.StateEmpLoyee;
                        ObjProductforDispatchingBLL.Update(ObdateModel);
                        //DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1
                        // ObjMissManagerBLL.WeddingMissionCreate(ObjOrderBLL.GetByID(OrderID).CustomerID.Value, 1, (int)MissionTypes.MyDispatching, DateTime.Now, ObjEmployeeID.Value.ToInt32(), "?CustomerID=" + ObjOrderBLL.GetByID(OrderID).CustomerID.Value + "&DispatchingID=" + DispatchingID, MissionChannel.DispatchingManager, ObjEmployeeID.Value.ToInt32(), DispatchingID);

                    }
                }
                JavaScriptTools.AlertWindowAndLocation("保存成功!", Request.Url.ToString(), Page);



            }
        }
        #endregion

        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            SaveaPageChange();
            JavaScriptTools.AlertWindow("保存完毕!", Page);
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

        #region 更换产品
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
                ObjItem.SupplierName = ObjProductModel.SupplierName;
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

        #region 自动分解
        /// <summary>
        /// 自动分解订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSplite_Click(object sender, EventArgs e)
        {
            var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            ObjCustomerModel.State = (int)CustomerStates.DoingCarrytask;
            ObjCustomersBLL.Update(ObjCustomerModel);           //修改客户状态 执行中

            for (int i = 0; i < repfirst.Items.Count; i++)
            {
                Repeater ObjrepList = (Repeater)repfirst.Items[i].FindControl("repdatalist");
                if (ObjrepList != null)
                {
                    for (int I = 0; I < ObjrepList.Items.Count; I++)
                    {
                        var ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                        if (ObjItem == null)
                        {
                            continue;
                        }
                        if (ObjItem.ItemLevel == 3)
                        {
                            if (!string.IsNullOrEmpty(ObjItem.Classification))
                            {
                                var UpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(ObjItem.DispatchingID, ObjItem.ParentCategoryID, 1);

                                //二级
                                var UpdateSecondModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(ObjItem.DispatchingID, ObjItem.CategoryID, 2);
                                //添加
                                if (string.IsNullOrEmpty(UpdateModel.Classification))
                                {
                                    UpdateModel.Classification = "";
                                }
                                if (string.IsNullOrEmpty(UpdateSecondModel.Classification))
                                {
                                    UpdateSecondModel.Classification = "";
                                }

                                if (!UpdateModel.Classification.Contains(ObjItem.Classification))
                                {
                                    UpdateModel.Classification += ObjItem.Classification;
                                }
                                if (!UpdateSecondModel.Classification.Contains(ObjItem.Classification))
                                {
                                    UpdateSecondModel.Classification += ObjItem.Classification;
                                }


                                ObjProductforDispatchingBLL.Update(UpdateModel);

                                ObjItem.IsGet = true;

                                var ClassArry = ObjItem.Classification.Trim(',').Split(',');

                                var NewModel = ObjItem;
                                for (int C = 0; C < ClassArry.Length; C++)
                                {
                                    if (ClassArry[C] != string.Empty)
                                    {


                                        NewModel.Classification = ClassArry[C];
                                        ObjProductforDispatchingBLL.Insert(NewModel);
                                    }
                                }
                                ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                                ObjProductforDispatchingBLL.Delete(ObjItem);
                                //if (ClassArry.Length > 0)
                                //{
                                //    ObjItem.IsGet = true;
                                //    ObjItem.Classification = ClassArry[0];
                                //    ObjProductforDispatchingBLL.Update(ObjItem);
                                //}

                            }

                        }
                    }
                }
            }

            var ObjUpdateModel = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
            ObjUpdateModel.IsBegin = true;
            ObjDispatchingBLL.Update(ObjUpdateModel);

            DispatchingState ObjDispatchingStateBLL = new DispatchingState();
            ObjDispatchingStateBLL.CheckState(Request["DispatchingID"].ToInt32(), Request["StateKey"].ToInt32(), User.Identity.Name.ToInt32());
            JavaScriptTools.AlertWindow("自动分解完毕，已将归属明确的产品分派完毕！", Page);
            BinderData();
            SpliteAll();
        }
        #endregion

        #region 对应分解
        protected void btnFlower_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "花艺":
                    CopyDate("花艺");
                    break;

                case "道具":
                    CopyDate("道具");
                    break;

                case "灯光":
                    CopyDate("灯光");
                    break;

                case "人员":
                    CopyDate("人员");
                    break;
                case "其它":
                    CopyDate("其它");
                    break;
            }


            var ObjCustomerUpdateModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            ObjCustomerUpdateModel.State = (int)CustomerStates.DoingCarrytask;
            ObjCustomersBLL.Update(ObjCustomerUpdateModel);

            var ObjUpdateModel = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
            ObjUpdateModel.IsBegin = true;
            ObjDispatchingBLL.Update(ObjUpdateModel);

            DispatchingState ObjDispatchingStateBLL = new DispatchingState();
            ObjDispatchingStateBLL.CheckState(Request["DispatchingID"].ToInt32(), Request["StateKey"].ToInt32(), User.Identity.Name.ToInt32());

        }

        #endregion

        #region 数据拷贝
        /// <summary>
        /// 拷贝数据到指定的类别下
        /// </summary>
        private void CopyDate(string ClassFire)
        {

            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                //int ParentEmpLoyeeID = 0;
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_ProductforDispatching ObjItem;

                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    var IsCheck = (ObjrepList.Items[I].FindControl("CheckItem") as CheckBox).Checked;
                    if (!IsCheck)
                    {
                        continue;
                    }
                    ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    if (ObjItem == null)
                    {
                        continue;
                    }



                    if (ObjItem.Classification != null)
                    {
                        if (!ObjItem.Classification.Contains(ClassFire))
                        {
                            ModifyParent(ObjItem, ClassFire);
                        }
                    }
                    else if (ObjItem.Classification == null)
                    {
                        ModifyParent(ObjItem, ClassFire);
                    }
                    if (ObjItem.ItemLevel == 3)
                    {
                        ObjItem.Classification += ClassFire;
                        ObjItem.IsGet = true;
                        ObjProductforDispatchingBLL.Update(ObjItem);
                    }

                }
            }

            SpliteAll();
        }
        #endregion


        public void ModifyParent(FL_ProductforDispatching ObjItem, string ClassFire)
        {
            var FirstLevelModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(ObjItem.DispatchingID, ObjItem.ParentCategoryID, 1);
            var SecondLevelModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(ObjItem.DispatchingID, ObjItem.CategoryID, 2);

            FirstLevelModel.Classification += ClassFire;
            FirstLevelModel.IsGet = true;

            SecondLevelModel.Classification += ClassFire;
            SecondLevelModel.IsGet = true;

            ObjProductforDispatchingBLL.Update(FirstLevelModel);
            ObjProductforDispatchingBLL.Update(SecondLevelModel);
        }


        public void SpliteAll()
        {
            List<FL_ProductforDispatching> ObjLevelItemList = ObjProductforDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32(), 3);
            var level = (from C in ObjLevelItemList.Where(C => C.Classification == null) select C.Classification).ToList();
            if (level.Count == 0)
            {
                DispatchingID = Request["DispatchingID"].ToInt32();
                CustomerID = Request["CustomerID"].ToInt32();
                //全部分解完成  创建操作日志
                CreateHandle();

                Response.Redirect("/AdminPanlWorkArea/Carrytask/CarrytaskWork/TaskWorkPanel.aspx?PageNameProductList&DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&QuotedID=" + GetQuotedID(DispatchingID) + "&OrderID=" + GetOrderIdByCustomerID(CustomerID) + "&NeedPopu=1&PageName=ProductList");
            }
            else
            {
                Response.Redirect(Request.Url.ToString());
            }
        }

        #region 获得QuotedId  ChangeID 方法


        public string GetQuotedID(object Source)
        {
            int DispatchingID = Source.ToString().ToInt32();
            var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
            int QuotedId = ObjDispatchingModel.QuotedID.ToString().ToInt32();
            return QuotedId.ToString();

        }


        public string GetChangeId(object Source)
        {
            if (Source != null && Source.ToString().ToInt32() > 0)
            {
                DispatchingID = Request["DispatchingID"].ToInt32();
                int ProductId = Source.ToString().ToInt32();
                var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
                int OrderId = ObjDispatchingModel.OrderID.ToString().ToInt32();
                var ItemSource = ObjQuotedPriceItemBLL.GetByProductIDOrder(OrderId, ProductId, 3);
                if (ItemSource != null)
                {
                    string ChangeId = ObjQuotedPriceItemBLL.GetByProductIDOrder(OrderId, ProductId, 3).ChangeID.ToString();
                    return ChangeId;
                }
            }
            return "";
        }
        #endregion



        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            HandleModel.HandleContent = "派工管理-分接订单,客户姓名:" + Model.Bride + "/" + Model.Groom + ",订单分解,全部分解完成";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 4;     //派工管理
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}