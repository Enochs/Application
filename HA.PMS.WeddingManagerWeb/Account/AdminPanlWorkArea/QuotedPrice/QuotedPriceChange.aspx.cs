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
using HA.PMS.BLLAssmblly.Sys;
using System.Text;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceChange : SystemPage
    {
        /// <summary>
        /// 报价单类别业务逻辑
        /// </summary>
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();
 

     
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
        int CustomerID = 0;

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
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            QuotedID = Request["QuotedID"].ToInt32();
        
            if (Request["QuotedID"] != null)
            {

               
                var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);




                switch (ObjQuotedModel.IsDispatching)
                {
                    case 1:
                        btnSaveChange.Visible = true;
                        //btnSaveChecks.Visible = true;
                        break;
                    case 2:
                        //btnSaveChange.Visible = true;
                        //btnPrint.Visible = true;
                        break;
                    case 3:
                        btnSaveChange.Visible = true;
                        btnfinish.Visible = true;
                        btnPrint.Visible = true;
                        break;
                    case 4:
                        // btnfinish.Visible = true;
                        btnPrint.Visible = true;
                        break;
                    default:
                        btnSaveChange.Visible = true;
                        //btnSaveChecks.Visible = true;
                        break;
                }
            }
            else
            {
                var CustomerModel=ObjCustomersBLL.GetByID(CustomerID);

                QuotedID = ObjQuotedPriceBLL.Insert(new FL_QuotedPrice()
                {
                    ParentQuotedID = Request["ParentID"].ToInt32(),
                    OrderID = OrderID,
                    IsChecks = false,
                    IsDelete = false,
                    CategoryName = "变更报价单",
                    EmpLoyeeID = User.Identity.Name.ToInt32(),
                    Dispatching = 0,
                    IsDispatching=0,
                    IsFirstCreate = false,
                    ChecksTitle="未提交",
                    HaveFile=false,
                    StarDispatching=false,
                    QuotedTitle=string.Format("{0}{1}",CustomerModel.Bride,CustomerModel.PartyDate.Value.ToString("yyyyMMdd")),
                    CreateDate=DateTime.Now,
                    CustomerID = Request["CustomerID"].ToInt32()
                });
                Response.Redirect("QuotedPriceChange.aspx?QuotedID=" + QuotedID + "&CustomerID=" + Request["CustomerID"] + "&ParentID=" + Request["ParentID"] + "&OrderID=" + Request["OrderID"] + "&quotedPriceChangeCount=" + Request["quotedPriceChangeCount"]);
                JavaScriptTools.AlertWindow("报价单主体已经创建,并且自动保存",Page);
            }

            if (!IsPostBack)
            {
                BinderData();
                 
                //报价单名
              
                //if (txtQuotedTitle.Text != string.Empty)
                //{
                //    txtQuotedTitle.Enabled = false;
                //}

                //if (Request["IsFinish"].ToInt32() == 2)
                //{
                //    btnSaveChecks.Visible = false;
                //    btnPrint.Visible = true;
                //    btnSaveChange.Visible = false;
                //    btnfinish.Visible = true;
                //    return;
                //}

                //if (ObjUopdateModel.IsDispatching == 3)
                //{
                //    btnPrint.Visible = true;
                //    btnSaveChecks.Visible = false;
                //    btnSaveChange.Visible = false;
                //    btnfinish.Visible = false;

                //}
                //if (ObjUopdateModel.CheckState == 2)
                //{
                //    btnSaveChange.Visible = false;
                //    btnSaveChecks.Visible = false;
                //    btnfinish.Visible = false;
                //}
                //if (ObjUopdateModel.IsChecks.Value)
                //{
                //    btnSaveChange.Visible = true;
                //    btnSaveChecks.Visible = false;
                //    btnfinish.Visible = false;
                //}
            }
        }

        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            var ObjCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            //lblCoder.Text = OrderID.ToString();
            //lblCustomerName.Text = ObjCustomerModel.Groom;
            //lblHotel.Text = ObjCustomerModel.Wineshop;
            //lblPartyDate.Text = GetShortDateString(ObjCustomerModel.PartyDate);
            //lblPhone.Text = ObjCustomerModel.GroomCellPhone;
            //lblTyper.Text = "套系";
            //lblTimerSpan.Text = "时段";

            var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            //txtAggregateAmount.Text = ObjquotedModel.AggregateAmount + string.Empty;
            //txtEarnestMoney.Text = ObjquotedModel.EarnestMoney + string.Empty;
            //txtRealAmount.Text = ObjquotedModel.RealAmount + string.Empty;
            //hideAggregateAmount.Value = ObjquotedModel.AggregateAmount + string.Empty;

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
            

            this.repfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0);
            this.repfirst.DataBind();
            BinderCuseomerDate();

            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);

            var ObjparentModel = ObjQuotedPriceBLL.GetByID(ObjUopdateModel.ParentQuotedID);
            if (ObjparentModel != null)
            {
                lblAllMoney.Text = ObjparentModel.FinishAmount.ToString();
                hideFinishAmount.Value = ObjparentModel.FinishAmount.ToString();
            }
            //txtFinishAmount.Text = ObjUopdateModel.FinishAmount + string.Empty;
            //if (txtFinishAmount.Text ==string.Empty)
            //{
            //    txtFinishAmount.Text = "0";
            //}


            if (ObjUopdateModel != null)
            {

                txtQuotedTitle.Enabled = false;
                txtLessenedAmount.Text = ObjUopdateModel.LessenedAmount.ToString();
                txtAddedAmount.Text = ObjUopdateModel.AddedAmount.ToString();
             //   lblAllMoney.Text = ObjUopdateModel.FinishAmount.ToString();
                txtAggregateAmount.Text = ObjUopdateModel.FinishAmount.ToString();
                txtQuotedTitle.Text = ModifyQuotedTitle(ObjUopdateModel.QuotedTitle);
                txtRemark.Text = ObjUopdateModel.Remark;

            }
        }
        
        /// <summary>
        /// 增加字符串末尾"_"后的数字
        /// </summary>
        /// <param name="strQuotedTitle">被更改的字符串</param>
        /// <returns></returns>
        private string ModifyQuotedTitle(string quotedTitle)
        {
            //订单修改次数
            int intQuotedChangeTimes = 0;
            //分解订单  订单格式 :新娘&新人20130890_001
            string[] strArr = quotedTitle.Split('_');
            //最后一个‘-’的位置
            int lastIndex = quotedTitle.LastIndexOf('_');

            if (!string.IsNullOrEmpty(Request["quotedPriceChangeCount"].ToString()))
            {
                intQuotedChangeTimes = Convert.ToInt32(Request["quotedPriceChangeCount"].ToString());
            }
            //如果至少个一个‘_’，即strArr数组长度大于等于2
            if (strArr.Length > 1)
            {  
                return quotedTitle.Substring(0, lastIndex) + string.Format("_{0}", (intQuotedChangeTimes + 1).ToString("000"));
            }
            else
            {
                return quotedTitle + string.Format("_{0}", (intQuotedChangeTimes + 1).ToString("000"));
            }
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
                    ItemList[ItemList.Count - 1].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                    //ObjItemList.Add(ObjItem);
                }
            }
            //}
            ObjItemList.Reverse();
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

                        ObjCategoryForQuotedPriceModel.Unit = string.Empty;
                        ObjCategoryForQuotedPriceModel.ServiceContent = ObjCategorItem.Title;

                        ObjCategoryForQuotedPriceModel.UnitPrice = 0;

                        ObjCategoryForQuotedPriceModel.PurchasePrice = 0;
                        ObjCategoryForQuotedPriceModel.Quantity = 0;

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
                    //var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, Objitem.QCKey, 2);
                    //if (ObjExistModel == null)
                    //{
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
                        ObjModel.Productproperty = Objitem.Productproperty;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Requirement = string.Empty;

                        ObjModel.Unit = string.Empty;
                        ObjModel.ServiceContent = Objitem.Title;

                        ObjModel.UnitPrice = 0;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Quantity = 0;

                        ObjQuotedPriceItemsBLL.Insert(ObjModel);
                    //}
                    //else
                    //{
                    //    ObjExistModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.Parent).Title;
                    //    ObjExistModel.IsDelete = false;
                    //    ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    //}
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
                FL_QuotedPriceItems ObjSetModel = new FL_QuotedPriceItems();
                foreach (var ObjProduct in ProductList)
                {
                    //根据父亲级别查询二类 有 但是肯定只有一个
                    var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByProductID(QuotedID, ObjProduct.Keys, 3);
                    if (ObjExistModel == null)
                    {
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
                        ObjSetModel.Specifications = ObjProduct.Specifications;
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
                        ObjSetModel.Requirement = ObjProduct.Explain;
                        //ObjSetModel.PartyDay = lblPartyDate.Text.ToDateTime();
                        //ObjSetModel.TimerSpan = lblTimerSpan.Text;
                        ObjSetModel.OrderID = OrderID;
                        ObjSetModel.ServiceContent = ObjProduct.ProductName;
                        ObjQuotedPriceItemsBLL.Insert(ObjSetModel);
                    }
                    else
                    {
                        ObjSetModel.ItemLevel = 3;
                        ObjExistModel.IsDelete = false;
                        //ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                        //ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                        //ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                        //ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent;
                        //ObjSetModel.CategoryName = OjbCategoryBLL.GetByID(ObjProduct.ProjectCategory).CategoryName;
                        //ObjExistModel.ParentCategoryName = OjbCategoryBLL.GetByID(ObjProduct.ProductCategory).CategoryName;
                        ObjQuotedPriceItemsBLL.Update(ObjExistModel);
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
            SaveQuotedPrice();
            SaveallChange();
            BinderData();

            JavaScriptTools.AlertWindowAndLocation("保存成功!",Request.Url.ToString(), Page);
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

  
 
        }


        private void SaveQuotedPrice()
        {

            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.AggregateAmount = txtAddedAmount.Text.ToDecimal();
            //ObjUopdateModel.RealAmount = txtFinishAmount.Text.ToDecimal();
            //ObjUopdateModel.EarnestMoney = txtFinishAmount.Text.ToDecimal();
            ObjUopdateModel.FinishAmount = txtAggregateAmount.Text.ToDecimal();

            ObjUopdateModel.LessenedAmount = txtLessenedAmount.Text.ToDecimal();
            ObjUopdateModel.AddedAmount = txtAddedAmount.Text.ToDecimal();

            ObjUopdateModel.Remark = txtRemark.Text;
            if (ObjUopdateModel.QuotedTitle == null || ObjUopdateModel.QuotedTitle == string.Empty)
            {
                ObjUopdateModel.QuotedTitle = txtQuotedTitle.Text;
            }
            if (ObjUopdateModel.IsDispatching == 0)
            {
                ObjUopdateModel.IsDispatching = 1;
            }
            ObjUopdateModel.IsFirstCreate = true;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
            var ObjParentUopdateModel = ObjQuotedPriceBLL.GetByID(ObjUopdateModel.ParentQuotedID);
            ObjParentUopdateModel.FinishAmount += txtAggregateAmount.Text.ToDecimal();
            ObjParentUopdateModel.AddedAmount = 0;
            ObjParentUopdateModel.LessenedAmount = 0;
            ObjQuotedPriceBLL.Update(ObjParentUopdateModel);
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
            //ObjUopdateModel.AggregateAmount = hideAggregateAmount.Value.ToDecimal();
            //ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
            //ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjUopdateModel.IsChecks = false;
            ObjUopdateModel.CheckState = 2;
            ObjUopdateModel.ChecksTitle = "已经提交审核";
            ObjUopdateModel.ChecksContent = "";
            ObjUopdateModel.IsDispatching = 2;
            ObjUopdateModel.Remark = txtRemark.Text;
            Department ObjDepartmentBLL = new Department();
            Employee ObjEmployeeBLL = new Employee();
            ObjUopdateModel.ChecksEmployee = ObjDepartmentBLL.GetByID(ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).DepartmentID).DepartmentManager;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);

            SaveQuotedPrice();
            BinderData();
            JavaScriptTools.CloseWindow("成功提交到部门审核人处!", Page);
            btnSaveChecks.Visible = false;
        }

        /// <summary>
        /// 确认此订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnfinish_Click(object sender, EventArgs e)
        {
            //btnSaveChange_Click(sender, e);
            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.Remark = txtRemark.Text;
            ObjUopdateModel.IsDispatching = 4;
            ObjUopdateModel.StarDispatching = false;
            ObjUopdateModel.IsChecks = true;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
            CreateCelebrationProductItem(ObjUopdateModel.EmpLoyeeID);
            JavaScriptTools.CloseWindow("保存成功!", Page);
   
        }
        /// <summary>
        /// 创建执行表
        /// </summary>
        private void CreateCelebrationProductItem(int? EmpLoyeeID)
        {
            Celebration ObjCelebrationBLL = new Celebration();

            var ObjCelModel = ObjCelebrationBLL.GetByQuotedID(QuotedID);
            var CelebrationID = ObjCelebrationBLL.Insert(new FL_Celebration()
            {

                CustomerID = CustomerID,
                OrderID = Request["OrderID"].ToInt32(),
                IsDelete = false,
                ParentCelebrationID = 0,
                QuotedID = QuotedID,

                QuotedEmpLoyee = EmpLoyeeID
            });

            CelebrationProductItem ObjCelebrationProductItemBLL = new CelebrationProductItem();

            //一级类别
            var ObjChangeFirstList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 1);

            //二级类别
            var ObjChangeSecondList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 2);

            //三级产品类别
            var ObjProductList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 3);

            //添加一级

            FL_CelebrationProductItem ObjCategoryforDispatching = new FL_CelebrationProductItem();
            foreach (var ObjFirstCategory in ObjChangeFirstList)
            {
                ObjCategoryforDispatching = new FL_CelebrationProductItem();

                ObjCategoryforDispatching.CategoryID = ObjFirstCategory.CategoryID;
                ObjCategoryforDispatching.CategoryName = ObjFirstCategory.CategoryName;
                ObjCategoryforDispatching.ParentCategoryID = ObjFirstCategory.ParentCategoryID;
                if (ObjFirstCategory.ParentCategoryID != 0)
                {
                    ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjFirstCategory.ParentCategoryID).Title;
                }
                else
                {
                    ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjFirstCategory.CategoryID).Title;
                }
                ObjCategoryforDispatching.ParentCelebrationID = 0;
                ObjCategoryforDispatching.ParentQuotedID = ObjFirstCategory.ParentQuotedID;
                ObjCategoryforDispatching.ProductID = ObjFirstCategory.ProductID;
                ObjCategoryforDispatching.ItemLevel = 1;
                ObjCategoryforDispatching.Unit = ObjFirstCategory.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjFirstCategory.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjFirstCategory.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjFirstCategory.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjFirstCategory.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjFirstCategory.Quantity;
                ObjCategoryforDispatching.Subtotal = ObjFirstCategory.Subtotal;
                ObjCategoryforDispatching.Remark = ObjFirstCategory.Remark;
                ObjCategoryforDispatching.RowType = ObjFirstCategory.RowType;
                ObjCategoryforDispatching.IsChecks = false;
                ObjCategoryforDispatching.NewAdd = false;
                ObjCategoryforDispatching.QuotedID = QuotedID;
                ObjCategoryforDispatching.CelebrationID = CelebrationID;
                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCategoryforDispatching.PurchasePrice = ObjFirstCategory.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjFirstCategory.Productproperty;
                ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);
            }

            foreach (var ObjSecondItem in ObjChangeSecondList)
            {
                ObjCategoryforDispatching = new FL_CelebrationProductItem();
                ObjCategoryforDispatching.CategoryID = ObjSecondItem.CategoryID;
                ObjCategoryforDispatching.CategoryName = ObjSecondItem.CategoryName;

                ObjCategoryforDispatching.ParentCategoryID = ObjSecondItem.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjSecondItem.ParentCategoryID).Title;
                ObjCategoryforDispatching.ParentCelebrationID = 0;
                ObjCategoryforDispatching.ParentQuotedID = ObjSecondItem.ParentQuotedID;
                ObjCategoryforDispatching.ProductID = ObjSecondItem.ProductID;
                ObjCategoryforDispatching.ItemLevel = 2;
                ObjCategoryforDispatching.Unit = ObjSecondItem.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjSecondItem.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjSecondItem.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjSecondItem.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjSecondItem.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjSecondItem.Quantity;
                ObjCategoryforDispatching.Subtotal = ObjSecondItem.Subtotal;
                ObjCategoryforDispatching.Remark = ObjSecondItem.Remark;
                ObjCategoryforDispatching.IsChecks = false;
                ObjCategoryforDispatching.NewAdd = false;
                ObjCategoryforDispatching.RowType = ObjSecondItem.RowType;
                ObjCategoryforDispatching.CelebrationID = CelebrationID;
                ObjCategoryforDispatching.QuotedID = QuotedID;
                ObjCategoryforDispatching.PurchasePrice = ObjSecondItem.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjSecondItem.Productproperty;
                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);

            }
            //添加三级
            foreach (var ObjThiredItem in ObjProductList)
            {

                ObjCategoryforDispatching = new FL_CelebrationProductItem();

                ObjCategoryforDispatching.CategoryID = ObjThiredItem.CategoryID;
                ObjCategoryforDispatching.CategoryName = ObjThiredItem.CategoryName;
                ObjCategoryforDispatching.ParentCategoryID = ObjThiredItem.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjThiredItem.ParentCategoryID).Title;
                ObjCategoryforDispatching.ParentCelebrationID = 0;
                ObjCategoryforDispatching.ParentQuotedID = ObjThiredItem.ParentQuotedID;
                ObjCategoryforDispatching.ProductID = ObjThiredItem.ProductID;
                ObjCategoryforDispatching.ItemLevel = 3;
                ObjCategoryforDispatching.Unit = ObjThiredItem.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjThiredItem.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjThiredItem.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjThiredItem.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjThiredItem.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjThiredItem.Quantity;
                ObjCategoryforDispatching.Subtotal = ObjThiredItem.Subtotal;
                ObjCategoryforDispatching.Remark = ObjThiredItem.Remark;
                ObjCategoryforDispatching.IsChecks = false;
                ObjCategoryforDispatching.NewAdd = false;
                ObjCategoryforDispatching.RowType = ObjThiredItem.RowType;
                ObjCategoryforDispatching.CelebrationID = CelebrationID;
                ObjCategoryforDispatching.QuotedID = QuotedID;

                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCategoryforDispatching.PurchasePrice = ObjThiredItem.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjThiredItem.Productproperty;
                ObjCategoryforDispatching.SupplierName = ObjThiredItem.SupplierName;
                ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);
            }
            JavaScriptTools.AlertWindow("确认成功", Page);
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
     
                SaveallChange();
            }
        }

    }
}