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
using HA.PMS.BLLAssmblly.Sys;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceChecks : SystemPage
    {
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();


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
            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            if (!IsPostBack)
            {
                BinderData();
                BinderCuseomerDate();
            }
        }


        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            //var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            //lblCoder.Text = OrderID.ToString();
            //lblCustomerName.Text = ObjCustomerModel.Groom;
            //lblHotel.Text = ObjCustomerModel.Wineshop;
            //lblPartyDate.Text = GetShortDateString(ObjCustomerModel.PartyDate);
            //lblPhone.Text = ObjCustomerModel.GroomCellPhone;
            //lblTyper.Text = "套系";
            //lblTimerSpan.Text = "时段";

            var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            if (ObjquotedModel.IsDispatching >= 2)
            {
                if (ObjquotedModel.HaveFile.Value)
                {

                    rdoIsHaveFile.Items[0].Selected = true;

                }
                else
                {
                    rdoIsHaveFile.Items[1].Selected = true;
                }
            }
            if (ObjquotedModel.ParentQuotedID > 0)
            {
                var ParentModel = ObjQuotedPriceBLL.GetByID(ObjquotedModel.ParentQuotedID);
                txtAggregateAmount.Text = ParentModel.AggregateAmount + string.Empty;
                txtEarnestMoney.Text = ParentModel.EarnestMoney + string.Empty;
                txtRealAmount.Text = ObjquotedModel.RealAmount + string.Empty;
                txtChecksContent.Text = ObjquotedModel.ChecksContent;
                lblRemark.Text = ObjquotedModel.Remark;
                lblQuotedTitle.Text = ObjquotedModel.QuotedTitle;
            }
            else
            {
                txtAggregateAmount.Text = ObjquotedModel.AggregateAmount + string.Empty;
                txtEarnestMoney.Text = ObjquotedModel.EarnestMoney + string.Empty;
                txtRealAmount.Text = ObjquotedModel.RealAmount + string.Empty;
                txtChecksContent.Text = ObjquotedModel.ChecksContent;
                lblRemark.Text = ObjquotedModel.Remark;
                lblQuotedTitle.Text = ObjquotedModel.QuotedTitle;
                if (ObjquotedModel.IsChecks.Value)
                {
                    btnSaveallChange.Visible = false;
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
                    ItemList[ItemList.Count - 1].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                    //ObjItemList.Add(ObjItem);
                }
            }
            //}
            ObjItemList.Reverse();
            ObjRep.DataSource = ObjItemList;
            ObjRep.DataBind();
        }


        #region 保存

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
                        ObjModel.Productproperty = Objitem.Productproperty;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Requirement = string.Empty;

                        ObjModel.Unit = string.Empty;
                        ObjModel.ServiceContent = Objitem.Title;

                        ObjModel.UnitPrice = 0;

                        ObjModel.PurchasePrice = 0;
                        ObjModel.Quantity = 0;

                        ObjQuotedPriceItemsBLL.Insert(ObjModel);
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

                    ObjSetModel.PartyDay = ObjCustomersBLL.GetByID(CustomerID).PartyDate; //lblPartyDate.Text.ToDateTime();
                    ObjSetModel.TimerSpan = ObjCustomersBLL.GetByID(CustomerID).TimeSpans; //lblTimerSpan.Text;
                    ObjSetModel.OrderID = OrderID;
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjQuotedPriceItemsBLL.Insert(ObjSetModel);
                    //}
                    //else
                    //{
                    //    ObjSetModel.ItemLevel = 3;
                    //    ObjExistModel.IsDelete = false;
                    //    //ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                    //    //ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                    //    //ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                    //    //ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent;
                    //    //ObjSetModel.CategoryName = OjbCategoryBLL.GetByID(ObjProduct.ProjectCategory).CategoryName;
                    //    //ObjExistModel.ParentCategoryName = OjbCategoryBLL.GetByID(ObjProduct.ProductCategory).CategoryName;
                    //    ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    //}


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

            SaveallChange();
            BinderData();
 
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
      
            ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
            ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjUopdateModel.FinishAmount = txtRealAmount.Text.ToDecimal();
 
            ObjUopdateModel.IsFirstCreate = true;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
        }

        #endregion

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChecks_Click(object sender, EventArgs e)
        {
            
            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.AggregateAmount = txtAggregateAmount.Text.ToDecimal();
            ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
            ObjUopdateModel.FinishAmount = txtRealAmount.Text.ToDecimal();
            ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjUopdateModel.IsChecks = true;
            ObjUopdateModel.CheckState = 3;
            ObjUopdateModel.IsDispatching = 3;
            ObjUopdateModel.ChecksTitle = ddlCheckTitle.SelectedItem.Text;
            ObjUopdateModel.ChecksContent = txtChecksContent.Text;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
            SaveallChange();
            //BinderData();
            Employee ObjEmployeeBLL = new Employee();
            JavaScriptTools.CloseWindow("审核完毕,任务已经下达给" + ObjEmployeeBLL.GetByID(ObjUopdateModel.EmpLoyeeID).EmployeeName+",责任人可以继续修改报价单或者制作执行明细，选择派工人！", Page);
            //Response.Redirect(Request.Url.ToString());
        }


        ///// <summary>
        ///// 确认此订单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnfinish_Click(object sender, EventArgs e)
        //{
        //    btnSaveChange_Click(sender, e);
        //    var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
        //    ObjUopdateModel.Remark = txtRemark.Text;
        //    ObjUopdateModel.IsDispatching = 3;
        //    ObjQuotedPriceBLL.Update(ObjUopdateModel);

        //    JavaScriptTools.AlertWindow("保存成功!", Page);
        //}


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
    }
}