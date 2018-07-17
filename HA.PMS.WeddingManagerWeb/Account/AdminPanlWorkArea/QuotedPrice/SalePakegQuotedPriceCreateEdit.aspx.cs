using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class SalePakegQuotedPriceCreateEdit : SystemPage
    {
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        Employee ObjEmployeeBLL = new Employee();
        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 报价单类别业务逻辑
        /// </summary>
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();

        /// <summary>
        /// 报价单主表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;


        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = ObjQuotedPriceBLL.GetByKind(Request["Kind"].ToInt32(), 2).QuotedID;
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            if (!IsPostBack)
            {
                BinderData();

                var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                var ObjEmPloyeeModel = ObjEmployeeBLL.GetByID(ObjUopdateModel.EmpLoyeeID);

                //报价单名
                txtQuotedTitle.Text = ObjUopdateModel.QuotedTitle;

            }
        }


        public string GetQuotedID()
        {
            return ObjQuotedPriceBLL.GetByKind(Request["Kind"].ToInt32(), 2).QuotedID.ToString();
        }

        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            txtAggregateAmount.Text = ObjquotedModel.AggregateAmount + string.Empty;
            txtEarnestMoney.Text = ObjquotedModel.EarnestMoney + string.Empty;
            txtRealAmount.Text = ObjquotedModel.RealAmount + string.Empty;
            hideAggregateAmount.Value = ObjquotedModel.AggregateAmount + string.Empty;

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


        public string GetKindImage(object Kind)
        {
            var ObjImageList = ObjQuotedPriceBLL.GetImageByKind(QuotedID, Kind.ToString().ToInt32(), 1);
            string ImageList = string.Empty;
            foreach (var ObjImage in ObjImageList)
            {
                ImageList += "<img alt='' src='" + ObjImage.FileAddress + "' />";
            }
            //<img alt="" src="../../Images/Appraise/3.gif" />
            return ImageList;
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
            this.repfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0);
            this.repfirst.DataBind();
            BinderCuseomerDate();
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
            var ObjItemList = new List<FL_QuotedPriceItems>();
            //获取二级项目
            var DataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                //ObjtxtPrice.Enabled = true;
                var NewList = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }
            //else
            //{
            //获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3);
                if (ItemList.Count == 0)
                {
                    //ObjtxtPrice.Enabled = true;
                    ObjItemList.Add(ObjItem);
                }
                else
                {
                    ItemList[0].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                    //ObjItemList.Add(ObjItem);
                }
            }
            //}
            //ObjItemList.Reverse();
            ObjRep.DataSource = ObjItemList;
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

                FL_QuotedPriceItems ObjCategoryForQuotedPriceModel = new FL_QuotedPriceItems();

                var ObjCategoryList = ObjQuotedCatgoryBLL.GetinKey(ObjList);

                //临时保存到数据库
                foreach (var ObjCategorItem in ObjCategoryList)
                {
                    //先查询是否已经添加 已经添加就只做修改
                    var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjCategorItem.QCKey, 1);
                    if (ObjExistModel == null)
                    {
                        ObjCategoryForQuotedPriceModel = new FL_QuotedPriceItems();
                        ObjCategoryForQuotedPriceModel.CategoryID = ObjCategorItem.QCKey;
                        ObjCategoryForQuotedPriceModel.CategoryName = ObjCategorItem.Title;
                        ObjCategoryForQuotedPriceModel.ParentCategoryName = ObjCategorItem.Title;
                        ObjCategoryForQuotedPriceModel.ParentCategoryID = ObjCategorItem.Parent;
                        ObjCategoryForQuotedPriceModel.ItemLevel = 1;
                        ObjCategoryForQuotedPriceModel.OrderID = OrderID;
                        ObjCategoryForQuotedPriceModel.IsDelete = false;
                        ObjCategoryForQuotedPriceModel.IsSvae = false;
                        ObjCategoryForQuotedPriceModel.IsChange = false;
                        ObjCategoryForQuotedPriceModel.QuotedID = QuotedID;
                        ObjCategoryForQuotedPriceModel.PurchasePrice = 0;
                        ObjCategoryForQuotedPriceModel.Requirement = string.Empty;

                        ObjCategoryForQuotedPriceModel.Quantity = 1;
                        ObjCategoryForQuotedPriceModel.Productproperty = 0;
                        ObjCategoryForQuotedPriceModel.Unit = string.Empty;
                        ObjCategoryForQuotedPriceModel.ServiceContent = ObjCategorItem.Title;

                        ObjCategoryForQuotedPriceModel.UnitPrice = 0;

                        ObjCategoryForQuotedPriceModel.PurchasePrice = 0;
                        ObjCategoryForQuotedPriceModel.IsFirstMake = 0;

                        ObjCategoryForQuotedPriceModel.Productproperty = ObjCategorItem.Productproperty;
                        ObjQuotedPriceItemsBLL.Insert(ObjCategoryForQuotedPriceModel);
                    }
                    else
                    {
                        ObjExistModel.IsDelete = false;
                        ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    }
                }
                SaveallChange();
                BinderData();
                JavaScriptTools.AlertWindow("保存类别成功!", Page);
            }
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

                FL_QuotedPriceItems ObjModel;

                //临时保存
                foreach (var Objitem in CategoryList)
                {
                    //根据父亲级别查询二类 有 但是肯定只有一个
                    var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, Objitem.QCKey, 2);
                    if (ObjExistModel == null)
                    {
                        ObjModel = new FL_QuotedPriceItems();
                        ObjModel.ItemLevel = 2;
                        ObjModel.OrderID = OrderID;
                        ObjModel.CategoryName = Objitem.Title;
                        ObjModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.Parent).Title;
                        ObjModel.CategoryID = Objitem.QCKey;
                        ObjModel.ParentCategoryID = Objitem.Parent;
                        ObjModel.IsDelete = false;
                        ObjModel.IsSvae = false;
                        ObjModel.IsChange = false;
                        ObjModel.QuotedID = QuotedID;
                        ObjModel.Requirement = "";
                        //ObjModel.Productproperty = Objitem.Productproperty;
                        ObjModel.Productproperty = 0;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Requirement = string.Empty;

                        ObjModel.Unit = string.Empty;
                        ObjModel.ServiceContent = Objitem.Title;

                        ObjModel.UnitPrice = 0;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Quantity = 1;
                        ObjModel.IsFirstMake = 0;

                        ObjQuotedPriceItemsBLL.Insert(ObjModel);

                        CreateProduct(Objitem.QCKey);
                    }
                    else
                    {
                        ObjExistModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.Parent).Title;
                        ObjExistModel.IsDelete = false;
                        ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    }
                }
                SaveallChange();
                BinderData();
            }
        }


        /// <summary>
        /// 创建单个产品
        /// </summary>
        /// <param name="QcKey"></param>
        private void CreateProduct(int QcKey)
        {
            QuotedProduct ObjQuotedProduct = new QuotedProduct();
            AllProducts ObjProductBLL = new AllProducts();
            var Keys = ObjQuotedProduct.GetByQcKey(QcKey).Keys;

            if (Keys.Length == 0)
            {
                return;
            }
            var KeyList = Keys.Split(',');
            var CountSource = ObjQuotedProduct.GetByQcKey(QcKey).ProductCount;

            for (int z = 0; z < KeyList.Length; z++)
            {
                var ObjProductModel = ObjProductBLL.GetByID(KeyList[z].ToInt32());
                FL_QuotedPriceItems ObjSetModel = new FL_QuotedPriceItems();
                ObjSetModel = new FL_QuotedPriceItems();
                ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(QcKey).Title;
                ObjSetModel.CategoryID = QcKey;
                ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(QcKey).Parent).Title;
                ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(QcKey).Parent;
                ObjSetModel.ProductID = ObjProductModel.Keys;
                ObjSetModel.IsDelete = false;
                ObjSetModel.IsSvae = false;
                ObjSetModel.ItemLevel = 3;
                ObjSetModel.Requirement = "";
                ObjSetModel.IsChange = false;
                ObjSetModel.Remark = ObjProductModel.Remark;
                ObjSetModel.QuotedID = QuotedID;
                ObjSetModel.UnitPrice = ObjProductModel.SalePrice;
                ObjSetModel.Classification = ObjProductModel.Classification;
                if (ObjProductModel.Specifications != null)
                {
                    if (ObjProductModel.Specifications.Length < 75)
                    {
                        ObjSetModel.Specifications = ObjProductModel.Specifications;
                    }
                    else
                    {
                        ObjSetModel.Specifications = ObjProductModel.Specifications.Substring(0, 75);
                    }
                }
                ObjSetModel.Unit = ObjProductModel.Unit;
                ObjSetModel.RowType = ObjProductModel.Type;

                //if (ObjProductModel.Type == 3)
                //{
                //    ObjSetModel.Quantity = ObjProductModel.Count;
                //    ObjSetModel.Subtotal = ObjProductModel.Count * ObjSetModel.UnitPrice;
                //}
                //else
                //{
                //    ObjSetModel.Quantity = 1;
                //    ObjSetModel.Subtotal = 1 * ObjSetModel.UnitPrice; ;
                //}
                if (CountSource != null)
                {
                    try
                    {
                        ObjSetModel.Quantity = CountSource.Split(',')[z].ToInt32();
                    }
                    catch
                    {
                        ObjSetModel.Quantity = 1;
                    }
                }
                else
                {
                    ObjSetModel.Quantity = 1;
                }
                ObjSetModel.Subtotal = ObjSetModel.Quantity * ObjSetModel.UnitPrice; ;
                ObjSetModel.SupplierName = ObjProductModel.SupplierName;
                ObjSetModel.Productproperty = ObjProductModel.Productproperty;
                ObjSetModel.PurchasePrice = ObjProductModel.PurchasePrice;
                ObjSetModel.Requirement = ObjProductModel.Specifications;
                ObjSetModel.Productproperty = null;
                ObjSetModel.IsFirstMake = 0;


                ObjSetModel.OrderID = OrderID;
                ObjSetModel.ServiceContent = ObjProductModel.ProductName;
                ObjQuotedPriceItemsBLL.Insert(ObjSetModel);
            }

        }


        /// <summary>
        /// 生成并保存三级产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateThired_Click(object sender, EventArgs e)
        {
            var CGKey = this.hideThirdValue.Value.Split(',');
            decimal SalePrice = 0;
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
                FL_QuotedPriceItems ObjSetModel = new FL_QuotedPriceItems();

                foreach (var ObjProduct in ProductList)
                {
                    //根据父亲级别查询二类 有 但是肯定只有一个
                    //var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByProductID(QuotedID, ObjProduct.Keys, 3);
                    //if (ObjExistModel == null)
                    //{
                    ObjSetModel = new FL_QuotedPriceItems();
                    ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                    ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                    ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                    ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent;
                    ObjSetModel.ProductID = ObjProduct.Keys;
                    ObjSetModel.IsDelete = false;
                    ObjSetModel.IsSvae = false;
                    ObjSetModel.ItemLevel = 3;
                    ObjSetModel.Requirement = "";
                    ObjSetModel.IsChange = false;
                    ObjSetModel.Remark = ObjProduct.Remark;
                    ObjSetModel.QuotedID = QuotedID;
                    ObjSetModel.UnitPrice = ObjProduct.SalePrice;

                    if (ObjProduct.Specifications != null)
                    {
                        if (ObjProduct.Specifications.Length < 75)
                        {
                            ObjSetModel.Specifications = ObjProduct.Specifications;
                        }
                        else
                        {
                            ObjSetModel.Specifications = ObjProduct.Specifications.Substring(0, 75);
                        }
                    }
                    ObjSetModel.Unit = ObjProduct.Unit;
                    ObjSetModel.RowType = ObjProduct.Type;
                    if (ObjProduct.Type == 3)
                    {
                        ObjSetModel.Quantity = ObjProduct.Count;
                        ObjSetModel.Subtotal = ObjProduct.Count * ObjSetModel.UnitPrice;
                    }
                    else
                    {
                        ObjSetModel.Quantity = 1;
                        ObjSetModel.Subtotal = 1 * ObjSetModel.UnitPrice; ;
                    }
                    ObjSetModel.SupplierName = ObjProduct.SupplierName;
                    ObjSetModel.Productproperty = ObjProduct.Productproperty;
                    ObjSetModel.PurchasePrice = ObjProduct.PurchasePrice;
                    ObjSetModel.Requirement = ObjProduct.Specifications;
                    ObjSetModel.Productproperty = null;
                    ObjSetModel.IsFirstMake = 0;

                    ObjSetModel.OrderID = OrderID;
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjQuotedPriceItemsBLL.Insert(ObjSetModel);


                    SalePrice += ObjSetModel.UnitPrice.Value * ObjSetModel.Quantity.Value;

                }

                txtAggregateAmount.Text = (txtAggregateAmount.Text.ToDecimal() + SalePrice) + "";
                hideAggregateAmount.Value = (txtAggregateAmount.Text.ToDecimal() + SalePrice) + "";
                txtRealAmount.Text = (txtRealAmount.Text.ToDecimal() + SalePrice) + "";
                SaveallChange();
                BinderData();

            }
        }


        /// <summary>
        /// 保存报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            SaveallChange();

            //btnSaveChecks.Visible = true;
            //btnfinish.Visible = false;
            JavaScriptTools.AlertWindow("保存成功!", Page);
        }


        /// <summary>
        /// 保存所有
        /// </summary>
        private void SaveallChange()
        {
            for (int P = 0; P < repfirst.Items.Count; P++)
            {

                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_QuotedPriceItems ObjItem;
                decimal? ItemAmoutMoney = 0;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.IsDelete = false;
                    ObjItem.ServiceContent = ((TextBox)ObjrepList.Items[I].FindControl("txtProductName")).Text;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.UnitPrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    ObjItem.IsChange = false;
                    ObjItem.IsDelete = false;
                    ObjItem.IsSvae = true;
                    ItemAmoutMoney += ObjItem.Subtotal;
                    ObjQuotedPriceItemsBLL.Update(ObjItem);

                }
                //保存分项合计
                HiddenField ObjHiddKey = (HiddenField)repfirst.Items[P].FindControl("hideKey");
                ObjItem = new FL_QuotedPriceItems();
                ObjItem = ObjQuotedPriceItemsBLL.GetByID(ObjHiddKey.Value.ToInt32());
                ObjItem.ItemSaleAmount = ((TextBox)repfirst.Items[P].FindControl("txtSaleItem")).Text.ToDecimal();
                ObjItem.ItemAmount = ItemAmoutMoney;
                ItemAmoutMoney = 0;
                ObjQuotedPriceItemsBLL.Update(ObjItem);
                //更新报价单主体
            }

            SaveQuotedPrice();
            BinderData();
        }


        private void SaveQuotedPrice()
        {

            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.AggregateAmount = hideAggregateAmount.Value.ToDecimal();
            ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
            ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjUopdateModel.Remark = txtRemark.Text;
            //if (ObjUopdateModel.QuotedTitle == null || ObjUopdateModel.QuotedTitle == string.Empty)
            //{
            ObjUopdateModel.QuotedTitle = txtQuotedTitle.Text;
            //}
            ObjUopdateModel.IsFirstCreate = true;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChecks_Click(object sender, EventArgs e)
        {
            SaveallChange();
            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.AggregateAmount = hideAggregateAmount.Value.ToDecimal();
            ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
            ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjUopdateModel.IsChecks = false;
            ObjUopdateModel.CheckState = 2;
            ObjUopdateModel.ChecksTitle = "已经提交审核";
            ObjUopdateModel.ChecksContent = "";
            ObjUopdateModel.Remark = txtRemark.Text;
            Department ObjDepartmentBLL = new Department();
            ObjUopdateModel.ChecksEmployee = ObjDepartmentBLL.GetByID(ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).DepartmentID).DepartmentManager;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
            BinderData();
            JavaScriptTools.AlertWindow("成功提交到部门审核人处!", Page);

        }


        /// <summary>
        /// 确认此订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnfinish_Click(object sender, EventArgs e)
        {
            btnSaveChange_Click(sender, e);
            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.Remark = txtRemark.Text;
            ObjUopdateModel.IsDispatching = 3;

            ObjQuotedPriceBLL.Update(ObjUopdateModel);

            JavaScriptTools.AlertWindow("保存成功!", Page);


        }




        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var ObjItem = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)e.Item.FindControl("hidePriceKey")).Value.ToInt32());
            if (ObjItem != null)
            {
                ObjQuotedPriceItemsBLL.Delete(ObjItem);
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
                SaveQuotedPrice();
                SaveallChange();
            }
        }


        /// <summary>
        /// 将最佳拍档导入报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnCreateOther_Click(object sender, EventArgs e)
        {

        }

    }
}