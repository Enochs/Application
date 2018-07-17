using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.HtmlControls;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class MyCarrytaskList : SystemPage
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


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

            //有类别
            int MineState = 0;
            this.repfirst.DataSource = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 1, out MineState);
            this.repfirst.DataBind();
            var ObjDisPatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
            //hideMineState.Value = User.Identity.Name.ToInt32()+string.Empty;
            //if (this.repfirst.Items.Count == 0)
            //{
            //    //有项目
            //    var ObjSecond = ObjProductforDispatchingBLL.GetProductforDispatchingByMine(User.Identity.Name.ToInt32(), DispatchingID, 2, out ObjKeyList).Distinct(new KeyClassEquers());
            //    if (ObjSecond.Count() > 0)
            //    {
            //        hideMineState.Value = "2";
            //        this.repfirst.DataSource = ObjProductforDispatchingBLL.GetByCatogryList(ObjKeyList.ToArray(), DispatchingID).Distinct(new KeyClassEquers());
            //        this.repfirst.DataBind();

            //    }
            //    else
            //    {
            //        hideMineState.Value = "3";
            //        //仅有产品
            //        var ObjThird = ObjProductforDispatchingBLL.GetProductforDispatchingByMine(User.Identity.Name.ToInt32(), DispatchingID, 3, out ObjKeyList).Distinct(new KeyClassEquers());
            //        this.repfirst.DataSource = ObjProductforDispatchingBLL.GetByCatogryList(ObjKeyList.ToArray(), DispatchingID).Distinct(new KeyClassEquers());
            //        this.repfirst.DataBind();
            //    }

            //}

            //判断是否为总派工人
            if (ObjDisPatchingModel.EmployeeID == User.Identity.Name.ToInt32())
            {
                hideMineState.Value = "1";
                txtFirstEmpLoyeeItem.Visible = true;
                hideFirstEmpLoyeeID.Visible = true;
                //firstSelect.Visible = true;
            }
            else
            {
                txtFirstEmpLoyeeItem.Visible = false;
                hideFirstEmpLoyeeID.Visible = false;
                //firstSelect.Visible = false;
            }
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="CatgoryID"></param>
        /// <returns></returns>
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
        /// </summary>
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
            ////获取二级项目
            //var DataList = ObjProductforDispatchingBLL.GetByParentCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            ////如果没有二级 则只有一级项目
            //if (DataList.Count == 0)
            //{
            //    //ObjtxtPrice.Enabled = true;
            //    var NewList = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 1);
            //    DataList.Add(NewList);
            //}
            ////else
            ////{
            ////获取产品级项目
            //foreach (var ObjItem in DataList)
            //{
            //    var ItemList = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, 3);
            //    if (ItemList.Count == 0)
            //    {
            //        //ObjtxtPrice.Enabled = true;
            //        ObjItemList.Add(ObjItem);
            //    }
            //    else
            //    {
            //        ItemList[ItemList.Count - 1].ItemLevel = 2;
            //        ObjItemList.AddRange(ItemList);
            //        //ObjItemList.Add(ObjItem);
            //    }
            //}
            //}
            //ObjItemList.Reverse();
            ObjRep.DataSource = DataList;
            ObjRep.DataBind();
            //ObjRep.DataSource = DataList;
            //ObjRep.DataBind();


            //Repeater ObjRepProduct = (Repeater)e.Item.FindControl("repProductList");
            //ObjRepProduct.DataSource=
        }


        /// <summary>
        /// 生成且保存一级大类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStarFirstpg_Click(object sender, EventArgs e)
        {
            //var CGKey = this.hidePgValue.Value.Split(',');
            //if (CGKey.Length > 0)
            //{
            //    int[] ObjList = new int[CGKey.Length];
            //    int i = 0;
            //    foreach (string Key in CGKey)
            //    {

            //        ObjList[i] = Key.ToInt32();
            //        i++;
            //    }

            //    FL_QuotedPriceItems ObjCategoryForQuotedPriceModel = new FL_QuotedPriceItems();
            //    var ObjCategoryList = OjbCategoryBLL.GetinList(ObjList);

            //    //临时保存到数据库
            //    foreach (var ObjCategorItem in ObjCategoryList)
            //    {
            //        //先查询是否已经添加 已经添加就只做修改
            //        var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjCategorItem.CategoryID, 1);
            //        if (ObjExistModel == null)
            //        {
            //            ObjCategoryForQuotedPriceModel = new FL_QuotedPriceItems();
            //            ObjCategoryForQuotedPriceModel.CategoryID = ObjCategorItem.CategoryID;
            //            ObjCategoryForQuotedPriceModel.CategoryName = ObjCategorItem.CategoryName;
            //            ObjCategoryForQuotedPriceModel.ParentCategoryName = ObjCategorItem.CategoryName;
            //            ObjCategoryForQuotedPriceModel.ParentCategoryID = 0;
            //            ObjCategoryForQuotedPriceModel.ItemLevel = 1;
            //            ObjCategoryForQuotedPriceModel.OrderID = OrderID;
            //            ObjCategoryForQuotedPriceModel.IsDelete = false;
            //            ObjCategoryForQuotedPriceModel.IsSvae = false;
            //            ObjCategoryForQuotedPriceModel.IsChange = false;
            //            ObjCategoryForQuotedPriceModel.QuotedID = QuotedID;
            //            ObjQuotedPriceItemsBLL.Insert(ObjCategoryForQuotedPriceModel);
            //        }
            //        else
            //        {
            //            ObjExistModel.IsDelete = false;
            //            ObjQuotedPriceItemsBLL.Update(ObjExistModel);
            //        }
            //    }
            //    SaveallChange();
            //    SaveQuotedPrice();
            //    BinderData();

            //}
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
                        ObjFinalCostModel.ServiceContent = ObjProduct.SupplierName;
                        ObjFinalCostModel.PlannedExpenditure = ObjProduct.PurchasePrice.Value;
                        ObjFinalCostModel.ActualExpenditure = 0;
                        ObjFinalCostModel.Expenseaccount = string.Empty;

                        ObjFinalCostModel.ActualWorkload = string.Empty;
                        ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
                    }

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
                }
            }

            //SaveQuotedPrice();
            BinderData();

        }


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
                    if (State == 0)
                    {
                        if (ObjItem.ItemLevel == 3)
                        {

                            var UpdateModelList = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, 2);
                            if (UpdateModelList[0].SupplierName != string.Empty && UpdateModelList[0].SupplierName != "库房")
                            {
                                ObjItem.SupplierName = UpdateModelList[0].SupplierName;
                                ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;
                                ObjItem.RowType = 1;
                            }
                            else
                            {
                                ObjItem.SupplierName =((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;
                                //ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;
                                ObjItem.RowType =2;
                            }
                            //ObjItem.Productproperty = 1;

                        }
                        else
                        {
                            if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房")
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
                        if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房")
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
                        var SecondList=ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 2, out MineState);
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
                JavaScriptTools.AlertWindowAndLocation("保存成功!",Request.Url.ToString(), Page);



            }
        }

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
    }
}