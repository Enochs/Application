using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
//临时注释：添加修改报价单
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceCreateEdit : PopuPage
    {

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

     


        /// <summary>
        /// 报价单主体表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();


        /// <summary>
        /// 产品
        /// </summary>
        Productcs ObjProductcsBLL = new Productcs();



        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();
        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        int QuotedID = 0;
        int OrderID = 0;


        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();
        /// <summary>
        /// 初始化 多为修改绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();

            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.reppgfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0);
            this.reppgfirst.DataBind();
            var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            //this.txtAggregateAmount.Text = ObjQuotedPriceModel.AggregateAmount+string.Empty;
            //this.txtEarnestMoney.Text = ObjQuotedPriceModel.EarnestMoney+string.Empty;
            //this.txtRealAmount.Text = ObjQuotedPriceModel.RealAmount + string.Empty;
        }



        #region 三级生成事件
        /// <summary>
        /// 生辰产品一级分类
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
                var ObjCategoryList = OjbCategoryBLL.GetinList(ObjList);

                //临时保存到数据库
                foreach (var ObjCategorItem in ObjCategoryList)
                {
                    //先查询是否已经添加 已经添加就只做修改
                    var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjCategorItem.CategoryID, 1);
                    if (ObjExistModel == null)
                    {
                        ObjCategoryForQuotedPriceModel = new FL_QuotedPriceItems();
                        ObjCategoryForQuotedPriceModel.CategoryID = ObjCategorItem.CategoryID;
                        ObjCategoryForQuotedPriceModel.CategoryName = ObjCategorItem.CategoryName;
                        ObjCategoryForQuotedPriceModel.ParentCategoryID = 0;
                        ObjCategoryForQuotedPriceModel.ItemLevel = 1;
                        ObjCategoryForQuotedPriceModel.OrderID = OrderID;
                        ObjCategoryForQuotedPriceModel.IsDelete = false;
                        ObjCategoryForQuotedPriceModel.IsSvae = false;
                        ObjCategoryForQuotedPriceModel.IsChange = false;
                        ObjCategoryForQuotedPriceModel.QuotedID = QuotedID;
                        ObjQuotedPriceItemsBLL.Insert(ObjCategoryForQuotedPriceModel);
                    }
                    else
                    {
                        ObjExistModel.IsDelete = false;
                        ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    }
                }

                this.reppgfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0);
                this.reppgfirst.DataBind();

            }
        }

        /// <summary>
        /// 生成产品二级分类
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


                for (int P = 0; P < reppgfirst.Items.Count; P++)
                {
                    HiddenField ObjHideKey = (HiddenField)reppgfirst.Items[P].FindControl("hideCategoryID");
                    if (ObjHideKey.Value == hideSecondCategoryID.Value)
                    {
                        Repeater ObjRepfirst = (Repeater)reppgfirst.Items[P].FindControl("repCGFirst");
                        Repeater ObjRepSecondList = (Repeater)reppgfirst.Items[P].FindControl("repCgSecondList");

                        //查询出选择的类型
                        var CategoryList = OjbCategoryBLL.GetinList(ObjList);

                        FL_QuotedPriceItems ObjModel;
                        ObjRepfirst.Visible = false;

                        //临时保存
                        foreach (var Objitem in CategoryList)
                        {
                            //根据父亲级别查询二类 有 但是肯定只有一个
                            var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, Objitem.CategoryID, 2);
                            if (ObjExistModel == null)
                            {
                                ObjModel = new FL_QuotedPriceItems();
                                ObjModel.ItemLevel = 2;
                                ObjModel.OrderID = OrderID;
                                ObjModel.CategoryName = Objitem.CategoryName;
                                ObjModel.CategoryID = Objitem.CategoryID;
                                ObjModel.ParentCategoryID = Objitem.ParentID;
                                ObjModel.IsDelete = false;
                                ObjModel.IsSvae = false;
                                ObjModel.IsChange = false;
                                ObjModel.QuotedID = QuotedID;
                                ObjQuotedPriceItemsBLL.Insert(ObjModel);
                            }
                            else
                            {
                                ObjExistModel.IsDelete = false;
                                ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                            }


                        }

                        ObjRepSecondList.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHideKey.Value.ToInt32());
                        ObjRepSecondList.DataBind();
                    }
                }
                //hideSecondCategoryID

                //this.reppgfirst.DataSource = OjbCategoryBLL.GetinList(ObjList);
                //this.reppgfirst.DataBind();

            }
        }

        /// <summary>
        /// 生成产品级报价单 
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


                for (int P = 0; P < reppgfirst.Items.Count; P++)
                {

                    Repeater ObjRepSecondList = (Repeater)reppgfirst.Items[P].FindControl("repCgSecondList");
                    for (int T = 0; T < ObjRepSecondList.Items.Count; T++)
                    {
                        HiddenField ObjHideKey = (HiddenField)ObjRepSecondList.Items[T].FindControl("hideThiredCategoryID");
                        HiddenField ObjParentCategoryID = (HiddenField)ObjRepSecondList.Items[T].FindControl("hiddParentCategoryID");
                        if (ObjHideKey.Value == hideThirdCategoryID.Value)
                        {
                            Repeater ObjrepProduct = (Repeater)ObjRepSecondList.Items[T].FindControl("repProduct");
                            List<FL_ProductForQuotedPrice> ObjSourceList = new List<FL_ProductForQuotedPrice>();
                            var ProductList = ObjAllProductsBLL.GetByKeysList(ObjKeyList);



                            FL_QuotedPriceItems ObjSetModel = new FL_QuotedPriceItems();
                            foreach (var ObjProduct in ProductList)
                            {


                                //根据父亲级别查询二类 有 但是肯定只有一个
                                var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByProductID(QuotedID, ObjProduct.Keys, 3);
                                if (ObjExistModel == null)
                                {
                                    ObjSetModel = new FL_QuotedPriceItems();
                                    ObjSetModel.CategoryName = ObjProduct.ProductName;
                                    ObjSetModel.CategoryID = ObjHideKey.Value.ToInt32();
                                    ObjSetModel.ParentCategoryID = ObjParentCategoryID.Value.ToInt32();
                                    ObjSetModel.ProductID = ObjProduct.Keys;
                                    ObjSetModel.IsDelete = false;
                                    ObjSetModel.IsSvae = false;
                                    ObjSetModel.ItemLevel = 3;
                                    ObjSetModel.IsChange = false;
                                    ObjSetModel.QuotedID = QuotedID;
                                    ObjQuotedPriceItemsBLL.Insert(ObjSetModel);
                                }
                                else
                                {
                                    ObjSetModel.ItemLevel = 3;
                                    ObjExistModel.IsDelete = false;
                                    ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                                }

                                //ObjSetModel = new FL_ProductForQuotedPrice();
                                //ObjSetModel.ProductID = ObjProduct.Keys;

                                //ObjSourceList.Add(ObjSetModel);GetByCategoryID(QuotedID, ObjHideKey.Value.ToInt32(),3);
                            }
                            ObjrepProduct.DataSource = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjHideKey.Value.ToInt32(), 3);
                            ObjrepProduct.DataBind();
                            return;
                        }
                    }

                }
            }
        }

        #endregion




        /// <summary>
        /// 删除大类报价单 联动删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void reppgfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }


        /// <summary>
        /// 循环查找一级分类下的repCGFirst 绑定一级分类报价信息
        /// 如果有二级分类信息则一级报价信息不显示hideCategoryID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void reppgfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField ObjHideKey = (HiddenField)e.Item.FindControl("hideCategoryID");
            Repeater ObjRepfirst = (Repeater)e.Item.FindControl("repCGFirst");
            Repeater ObjRepSecondList = (Repeater)e.Item.FindControl("repCgSecondList");

            //先根据一级分类查询二级
            var ObjSecondList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHideKey.Value.ToInt32());
            if (ObjSecondList.Count > 0)
            {


                ObjRepSecondList.DataSource = ObjSecondList;
                ObjRepSecondList.DataBind();


            }
            else
            {
                //无二级分类肯定就只有一级分类
                List<FL_QuotedPriceItems> ObjList = new List<FL_QuotedPriceItems>();

              
                //BindList.Add(new FL_CategoryForQuotedPrice(
                //    )
                //    {
                //        CategoryID = ObjHideKey.Value.ToInt32()
                //    });
                ObjList.Add(ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHideKey.Value.ToInt32(), 1));
                ObjRepfirst.DataSource = ObjList;
                ObjRepfirst.DataBind();
            }


        }



        /// <summary>
        /// 绑定第三级别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repCgSecondList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField ObjHideKey = (HiddenField)e.Item.FindControl("hideThiredCategoryID");
            Repeater ObjrepThiredList = (Repeater)e.Item.FindControl("repThiredList");
            Repeater ObjrepProduct = (Repeater)e.Item.FindControl("repProduct");

            //if (ObjProductForQuotedPriceBLL.GetByCategoryID(ObjHideKey.Value.ToInt32(), QuotedID).Count== 0)
            //{ 
            var ExistList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjHideKey.Value.ToInt32(),3);
            if (ExistList.Count == 0)
            {
                List<FL_QuotedPriceItems> ObjList = new List<FL_QuotedPriceItems>();
                ObjrepThiredList.DataSource = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjHideKey.Value.ToInt32(),2);
                ObjrepThiredList.DataBind();
            }
            else
            {

                ObjrepProduct.DataSource = ExistList;
                ObjrepProduct.DataBind();
            }

        }



        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetProductByID(object Key)
        {
            return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
        }

        protected void hideSecondValue_ValueChanged(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 保存创建的报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveallChange_Click(object sender, EventArgs e)
        {
            //FL_QuotedPrice ObjQuotedPriceModel = new FL_QuotedPrice();
            //ObjQuotedPriceModel.CustomerID = Request["CustomerID"].ToInt32();
            //ObjQuotedPriceModel.IsDelete = false;
            //ObjQuotedPriceModel.OrderID = 11;// Request["OrderID"].ToInt32();
            //ObjQuotedPriceModel.CategoryName = string.Empty;
            var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjQuotedPriceModel.AggregateAmount = txtAggregateAmount.Text.ToDecimal();
            ObjQuotedPriceModel.RealAmount = txtRealAmount.Text.ToDecimal();
            ObjQuotedPriceModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
            var ObjQuteKey = QuotedID;
            if (ObjQuteKey > 0)
            {

                //生成大类报价单
                FL_QuotedPriceItems ObjCategoryForQuotedPrice;
                for (int P = 0; P < reppgfirst.Items.Count; P++)
                {
                    HiddenField ObjhideCategoryID = (HiddenField)reppgfirst.Items[P].FindControl("hideCategoryID");
                    //循环创建大类报价单
                    Repeater ObjRepfirst = (Repeater)reppgfirst.Items[P].FindControl("repCGFirst");
                    if (ObjRepfirst.Items.Count > 0)
                    {
                        for (int F = 0; F < ObjRepfirst.Items.Count; F++)
                        {

                            ObjCategoryForQuotedPrice = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjRepfirst.Items[F].FindControl("hidePriceKey")).Value.ToInt32());
                            ObjCategoryForQuotedPrice.CategoryID = ObjhideCategoryID.Value.ToInt32();
                            ObjCategoryForQuotedPrice.CategoryName = OjbCategoryBLL.GetByID(ObjCategoryForQuotedPrice.CategoryID).CategoryName;
                            ObjCategoryForQuotedPrice.ImageUrl = string.Empty;
                            ObjCategoryForQuotedPrice.IsDelete = false;
                            ObjCategoryForQuotedPrice.Unit = ((TextBox)ObjRepfirst.Items[F].FindControl("txtUnit")).Text;
                            ObjCategoryForQuotedPrice.Specifications = ((TextBox)ObjRepfirst.Items[F].FindControl("txtspecifications")).Text;
                            ObjCategoryForQuotedPrice.ServiceContent = ((TextBox)ObjRepfirst.Items[F].FindControl("txtServiceContent")).Text;
                            ObjCategoryForQuotedPrice.Requirement = ((TextBox)ObjRepfirst.Items[F].FindControl("txtRequirement")).Text;
                            ObjCategoryForQuotedPrice.UnitPrice = ((TextBox)ObjRepfirst.Items[F].FindControl("txtUnitPrice")).Text.ToDecimal();
                            ObjCategoryForQuotedPrice.Quantity = ((TextBox)ObjRepfirst.Items[F].FindControl("txtQuantity")).Text.ToInt32();
                            ObjCategoryForQuotedPrice.Subtotal = ((TextBox)ObjRepfirst.Items[F].FindControl("txtSubtotal")).Text.ToDecimal();
                            ObjCategoryForQuotedPrice.Remark = ((TextBox)ObjRepfirst.Items[F].FindControl("txtRemark")).Text;
                            ObjCategoryForQuotedPrice.QuotedID = ObjQuteKey;
                            ObjCategoryForQuotedPrice.IsChange = false;
                            ObjCategoryForQuotedPrice.IsDelete = false;
                            ObjCategoryForQuotedPrice.IsSvae = true;
                            ObjQuotedPriceItemsBLL.Update(ObjCategoryForQuotedPrice);
                        }
                    }
                    else
                    {
                        ObjCategoryForQuotedPrice = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)reppgfirst.Items[P].FindControl("hidePriceKey")).Value.ToInt32());
                        ObjCategoryForQuotedPrice.IsDelete = false;
                        ObjCategoryForQuotedPrice.IsSvae = true;
                        ObjQuotedPriceItemsBLL.Update(ObjCategoryForQuotedPrice);
                    }

                    //开始创建二类报价单
                    Repeater ObjSecondList = (Repeater)reppgfirst.Items[P].FindControl("repCgSecondList");
                    for (int S = 0; S < ObjSecondList.Items.Count; S++)
                    {

                        HiddenField ObjhideThiredCategoryID = (HiddenField)ObjSecondList.Items[S].FindControl("hideThiredCategoryID");
                        var CategoryID = ObjhideThiredCategoryID.Value.ToInt32();
                        //找到二类报价单
                        Repeater ObjRepThiredList = (Repeater)ObjSecondList.Items[S].FindControl("repThiredList");
                        FL_QuotedPriceItems ObjProductitemForQuotedPrice;
                        if (ObjRepThiredList.Items.Count > 0)
                        {
                            for (int T = 0; T < ObjRepThiredList.Items.Count; T++)
                            {

                                ObjProductitemForQuotedPrice = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjRepThiredList.Items[T].FindControl("hiddDispatchingID")).Value.ToInt32());

                                //ObjProductitemForQuotedPrice.CategoryID = ObjhideThiredCategoryID.Value.ToInt32();
                                //ObjProductitemForQuotedPrice.CategoryName = OjbCategoryBLL.GetByID(ObjProductitemForQuotedPrice.CategoryID).CategoryName;
                                //ObjProductitemForQuotedPrice.ParentCategoryID = ObjhideCategoryID.Value.ToInt32();
                                ObjProductitemForQuotedPrice.ImageUrl = string.Empty;
                                ObjProductitemForQuotedPrice.IsDelete = false;
                                ObjProductitemForQuotedPrice.Unit = ((TextBox)ObjRepfirst.Items[T].FindControl("txtUnit")).Text;
                                ObjProductitemForQuotedPrice.Specifications = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtspecifications")).Text;
                                ObjProductitemForQuotedPrice.ServiceContent = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtServiceContent")).Text;
                                ObjProductitemForQuotedPrice.Requirement = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtRequirement")).Text;
                                ObjProductitemForQuotedPrice.UnitPrice = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtUnitPrice")).Text.ToDecimal();
                                ObjProductitemForQuotedPrice.Quantity = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtQuantity")).Text.ToInt32();
                                ObjProductitemForQuotedPrice.Subtotal = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtSubtotal")).Text.ToDecimal();
                                ObjProductitemForQuotedPrice.Remark = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtRemark")).Text;
                                ObjProductitemForQuotedPrice.QuotedID = ObjQuteKey;
                                ObjProductitemForQuotedPrice.IsSvae = true;
                                ObjProductitemForQuotedPrice.IsDelete = false;
                                ObjProductitemForQuotedPrice.IsChange = false;
                                ObjQuotedPriceItemsBLL.Update(ObjProductitemForQuotedPrice);
                            }
                        }
                        else
                        {
                            ObjProductitemForQuotedPrice = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjSecondList.Items[P].FindControl("hiddDispatchingID")).Value.ToInt32());
                            ObjProductitemForQuotedPrice.IsDelete = false;
                            ObjProductitemForQuotedPrice.IsSvae = true;
                            ObjQuotedPriceItemsBLL.Update(ObjProductitemForQuotedPrice);
                        }

                        //找到产品报价单
                        //生成报价单
                        Repeater ObjrepProductList = (Repeater)ObjSecondList.Items[S].FindControl("repProduct");
                        if (ObjrepProductList.Items.Count > 0)
                        {
                            FL_QuotedPriceItems ObjProductForQuotedPriceModel = new FL_QuotedPriceItems();
                            //循环升生成报价单
                            for (int U = 0; U < ObjrepProductList.Items.Count; U++)
                            {

                                //hiddProductKey
                                ObjProductForQuotedPriceModel = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjrepProductList.Items[U].FindControl("hiddProductKey")).Value.ToInt32());
                                ObjProductForQuotedPriceModel.CategoryID = ObjhideThiredCategoryID.Value.ToInt32();
                                ObjProductForQuotedPriceModel.CategoryName = OjbCategoryBLL.GetByID(ObjProductForQuotedPriceModel.CategoryID).CategoryName;
                                ObjProductForQuotedPriceModel.ParentCategoryID = ObjhideCategoryID.Value.ToInt32();
                                ObjProductForQuotedPriceModel.ImageUrl = string.Empty;
                                ObjProductForQuotedPriceModel.IsDelete = false;
                                ObjProductForQuotedPriceModel.Unit = ((TextBox)ObjrepProductList.Items[U].FindControl("txtUnit")).Text;
                                ObjProductForQuotedPriceModel.Specifications = ((TextBox)ObjrepProductList.Items[U].FindControl("txtspecifications")).Text;
                                ObjProductForQuotedPriceModel.ServiceContent = ((TextBox)ObjrepProductList.Items[U].FindControl("txtServiceContent")).Text;
                                ObjProductForQuotedPriceModel.Requirement = ((TextBox)ObjrepProductList.Items[U].FindControl("txtRequirement")).Text;
                                ObjProductForQuotedPriceModel.UnitPrice = ((TextBox)ObjrepProductList.Items[U].FindControl("txtUnitPrice")).Text.ToDecimal();
                                ObjProductForQuotedPriceModel.Quantity = ((TextBox)ObjrepProductList.Items[U].FindControl("txtQuantity")).Text.ToInt32();
                                ObjProductForQuotedPriceModel.Subtotal = ((TextBox)ObjrepProductList.Items[U].FindControl("txtSubtotal")).Text.ToDecimal();
                                ObjProductForQuotedPriceModel.Remark = ((TextBox)ObjrepProductList.Items[U].FindControl("txtRemark")).Text;
                                ObjProductForQuotedPriceModel.QuotedID = ObjQuteKey;
                                ObjProductForQuotedPriceModel.IsChange = false;
                                ObjProductForQuotedPriceModel.IsSvae = true;
                                ObjProductForQuotedPriceModel.ProductID = ((HiddenField)ObjrepProductList.Items[U].FindControl("hideProductID")).Value.ToInt32();
                                ObjQuotedPriceItemsBLL.Update(ObjProductForQuotedPriceModel);
                            }
                        }
                    }
                }
            }
            OrderGuardian ObjOrderGuardianBLL = new OrderGuardian();
            var ObjGuardianIDList = hideFoureGingang.Value.Split(',');
            foreach (var ObjItem in ObjGuardianIDList)
            {
                FL_OrderGuardian ObjOrderGuardianModel = new FL_OrderGuardian();
                ObjOrderGuardianModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                ObjOrderGuardianModel.EmpLoyee = User.Identity.Name.ToInt32();
                ObjOrderGuardianModel.GuardianId = ObjItem.ToInt32();
                ObjOrderGuardianModel.OrderID = ObjQuotedPriceModel.OrderID;
                ObjOrderGuardianBLL.Insert(ObjOrderGuardianModel);
            }


            var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjQuotedModel.IsFirstCreate = true;
            ObjQuotedModel.IsChecks = false;
            ObjQuotedModel.CheckState = 2;
            ObjQuotedPriceBLL.Update(ObjQuotedModel);
            JavaScriptTools.AlertWindow("保存成功！", this.Page);
            Response.Redirect("QuotedPriceList.aspx");
        }


        /// <summary>
        /// 确认定金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveNoemoney_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 保存并提交审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChecks_Click(object sender, EventArgs e)
        {
            btnSaveallChange_Click(sender, e);

        }
    }
}