using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Emnus;
//创建派工明细表 根据报价单创建


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class CreateDispatching :PopuPage
    {

        CelebrationProductItem ObjCelebrationProductItemBLL = new CelebrationProductItem();


        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        //Category OjbCategoryBLL = new Category();

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
        int CategoryID = 0;
        int CelebrationID = 0;

        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            CategoryID = Request["CategoryID"].ToInt32();
            CelebrationID = Request["CelebrationID"].ToInt32();
            if (!IsPostBack)
            {
                
                BinderData();

                if (Request["Dis"] != null)
                {
                   
                    btnSaveChange.Visible = false;
                    
                }
 
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


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            //FL_ProductforDispatching
            this.repfirst.DataSource = ObjCelebrationProductItemBLL.GetByCategoryID(CelebrationID, CategoryID, 1);
            this.repfirst.DataBind();
            

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
            var ObjItemList = new List<FL_CelebrationProductItem>();
            //获取二级项目
            var DataList = ObjCelebrationProductItemBLL.GetByParentCatageID(CelebrationID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                //ObjtxtPrice.Enabled = true;
                var NewList = ObjCelebrationProductItemBLL.GetOnlyByCatageID(CelebrationID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }
            //else
            //{
            //获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjCelebrationProductItemBLL.GetByCategoryID(CelebrationID, ObjItem.CategoryID, 3);
                if (ItemList.Count == 0)
                {
                    //ObjtxtPrice.Enabled = true;
                    ObjItemList.Add(ObjItem);
                }
                else
                {
                    ItemList[ItemList.Count - 1].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                    //ObjItemList.Add(ObjItem);
                }
            }
            //}
            ObjItemList.Reverse();
            ObjRep.DataSource = ObjItemList;
            ObjRep.DataBind();

            //HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            //Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            ////TextBox ObjtxtPrice = (TextBox)e.Item.FindControl("txtSalePrice");
            //var DataList = new List<FL_CelebrationProductItem>();
            ////获取二级项目
            //var NewList = ObjCelebrationProductItemBLL.GetOnlyByCatageID(CelebrationID, ObjHiddCategKey.Value.ToInt32(), 1);
            //DataList.Add(NewList);
            //DataList.AddRange(ObjCelebrationProductItemBLL.GetByParentCatageID(CelebrationID, ObjHiddCategKey.Value.ToInt32()).OrderBy(C => C.CategoryID).ToList());
            ////如果没有二级 则只有一级项目
            ////if (DataList.Count == 0)
            ////{
            ////    //ObjtxtPrice.Enabled = true;
            //    //var NewList = ObjCelebrationProductItemBLL.GetOnlyByCatageID(CelebrationID, ObjHiddCategKey.Value.ToInt32(), 1);
            //    //DataList.Add(NewList);
            ////}
            ////else
            ////{
            //////获取产品级项目
            ////foreach (var ObjItem in DataList)
            ////{
            ////    var ItemList = ObjCelebrationProductItemBLL.GetByCategoryID(CelebrationID, ObjItem.CategoryID, 3);
            ////    if (ItemList.Count == 0)
            ////    {
            ////        //ObjtxtPrice.Enabled = true;
            ////        ObjItemList.Add(ObjItem);
            ////    }
            ////    else
            ////    {
            ////        ItemList[ItemList.Count - 1].ItemLevel = 2;
            ////        ObjItemList.AddRange(ItemList);
            ////        //ObjItemList.Add(ObjItem);
            ////    }
            ////}
            ////}
            ////DataList.Reverse();
            //ObjRep.DataSource = DataList;
            //ObjRep.DataBind();
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

                FL_CelebrationProductItem ObjModel;

                //临时保存
                foreach (var Objitem in CategoryList)
                {
                    //根据父亲级别查询二类 有 但是肯定只有一个
                    var ObjExistModel = ObjCelebrationProductItemBLL.GetOnlyByCatageID(CelebrationID, Objitem.QCKey, 2);
                    if (ObjExistModel == null)
                    {
                        ObjModel = new FL_CelebrationProductItem();
                        ObjModel.CelebrationID = CelebrationID;
                        ObjModel.ItemLevel = 2;
                        ObjModel.OrderID = OrderID;
                        ObjModel.CategoryName = Objitem.Title;
                        ObjModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.Parent).Title;
                        ObjModel.CategoryID = Objitem.QCKey;
                        ObjModel.ParentCategoryID = Objitem.Parent;
  
                        ObjModel.QuotedID = QuotedID;
                        ObjModel.Requirement = "";
                        ObjModel.Productproperty = Objitem.Productproperty;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Requirement = string.Empty;

                        ObjModel.Unit = string.Empty;
                        ObjModel.ServiceContent = Objitem.Title;

                        ObjModel.UnitPrice = 0;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Quantity = 0;
                        ObjModel.NewAdd = true;
   
                        ObjCelebrationProductItemBLL.Insert(ObjModel);
                    }
                    else
                    {
                        //ObjExistModel.ParentCategoryName = OjbCategoryBLL.GetByID(Objitem.ParentID).CategoryName;
                        
                        ObjCelebrationProductItemBLL.Update(ObjExistModel);
                    }
                }
                SaveallChange();
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



                FL_CelebrationProductItem ObjSetModel = new FL_CelebrationProductItem();
                foreach (var ObjProduct in ProductList)
                {


                    //根据父亲级别查询二类 有 但是肯定只有一个
                    var ObjExistModel = ObjCelebrationProductItemBLL.GetOnlyByProductID(CelebrationID, ObjProduct.Keys, 3);
                    if (ObjExistModel == null)
                    {
                        ObjSetModel = new FL_CelebrationProductItem();
                        ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                        ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                        ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                        ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent;
                        ObjSetModel.ProductID = ObjProduct.Keys;
       
                        ObjSetModel.ItemLevel = 3;
                        ObjSetModel.Requirement = "";
                  
                        ObjSetModel.Remark = ObjProduct.Remark;
                        ObjSetModel.QuotedID = QuotedID;
                        ObjSetModel.UnitPrice = ObjProduct.SalePrice;
                        ObjSetModel.Specifications = ObjProduct.Specifications;
                        ObjSetModel.Unit = ObjProduct.Unit;
                        ObjSetModel.RowType = ObjProduct.Type;
                        if (ObjProduct.Type == 3)
                        {
                            ObjSetModel.Quantity = ObjProduct.Count;
                            ObjSetModel.Subtotal = ObjProduct.Count * ObjSetModel.UnitPrice;
                        }
                        ObjSetModel.SupplierName = ObjProduct.SupplierName;
                        ObjSetModel.Productproperty = ObjProduct.Productproperty;
                        ObjSetModel.PurchasePrice = ObjProduct.PurchasePrice;
                        ObjSetModel.Requirement = ObjProduct.Explain;
           
                        ObjSetModel.OrderID = OrderID;
                        ObjSetModel.ServiceContent = ObjProduct.ProductName;
              
                        ObjSetModel.CelebrationID = CelebrationID;
                        ObjSetModel.NewAdd = true;
                        ObjCelebrationProductItemBLL.Insert(ObjSetModel);
                    }
                    else
                    {
                        ObjSetModel.ItemLevel = 3;
                        ObjCelebrationProductItemBLL.Update(ObjExistModel);
                    }


                    //ObjSetModel = new FL_ProductForQuotedPrice();
                    //ObjSetModel.ProductID = ObjProduct.Keys;

                    //ObjSourceList.Add(ObjSetModel);GetByCategoryID(QuotedID, ObjHideKey.Value.ToInt32(),3);
                }
            }
            SaveallChange();
            BinderData();

        }


        /// <summary>
        /// 保存报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            //SaveallChange();
            //BinderData();
        }


        /// <summary>
        /// 保存所有
        /// </summary>
        private void SaveallChange()
        {
            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_CelebrationProductItem ObjItem;
                decimal? ItemAmoutMoney = 0;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjCelebrationProductItemBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.ServiceContent = ((TextBox)ObjrepList.Items[I].FindControl("txtProductName")).Text;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.UnitPrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    ItemAmoutMoney += ObjItem.Subtotal;
                    ObjCelebrationProductItemBLL.Update(ObjItem);

                }
                //保存分项合计
                //HiddenField ObjHiddKey = (HiddenField)repfirst.Items[P].FindControl("hidePriceKey");
                //ObjItem = new FL_CelebrationProductItem();
                //ObjItem = ObjCelebrationProductItemBLL.GetByID(ObjHiddKey.Value.ToInt32());
                //ObjItem.ItemSaleAmount = ((TextBox)repfirst.Items[P].FindControl("txtSaleItem")).Text.ToDecimal();
                //ObjItem.ItemAmount = ItemAmoutMoney;
                //ItemAmoutMoney = 0;
                //ObjCelebrationProductItemBLL.Update(ObjItem);
            }
            JavaScriptTools.AlertWindow("保存成功!", Page);
            BinderData();
        }


     


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //var ObjItem = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)e.Item.FindControl("hidePriceKey")).Value.ToInt32());
            //if (ObjItem != null)
            //{
            //    ObjQuotedPriceItemsBLL.Delete(ObjItem);
            //    BinderData();
            //}
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
               
                SaveallChange();
            }
        }

        protected void btnSaveChange_Click1(object sender, EventArgs e)
        {
            SaveallChange();
        }

    }
}