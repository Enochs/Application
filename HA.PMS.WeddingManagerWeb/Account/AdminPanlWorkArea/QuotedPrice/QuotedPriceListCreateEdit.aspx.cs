using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.SysTarget;
using System.Drawing;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceListCreateEdit : SystemPage
    {

        //IsClose=false
        //QuotedPriceSplit 分离报价单（先做策划 然后做报价 然后报价单回到策划师手中）
        //FlowerSplit  花艺报价单分三步审核 1策划师提交到花艺部负责人 2花艺负责人选择核价负责人和采购

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
        /// 
        /// </summary>
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Target ObjTargetBLL = new Target();

        StorehouseSourceProduct storehouseSourceProductBLL = new StorehouseSourceProduct();

        QuotedPriceForType ObjDiscountBLL = new QuotedPriceForType();

        Order ObjOrderBLL = new Order();

        Report ObjReportBLL = new Report();

        /// <summary>
        /// 报价单主表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;

        int showstate = 0;      //是否显示新增变更单
        int FirstMake = 0;

        SysConfig ObjSysConfig = new SysConfig();

        #region 绑定界面 页面初始化
        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            QuotedID = Request["QuotedID"].ToInt32();
            ViewState["QuotedID"] = QuotedID;
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            CreateButton();         //第几次变更单 可以查看

            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);


            //判断按钮隐藏状态
            switch (ObjUopdateModel.IsDispatching)
            {
                case 1:
                    btnSaveChange.Visible = true;
                    break;
                case 2:
                    break;
                case 3:
                    btnSaveChange.Visible = true;
                    btnfinish.Visible = true;
                    btnPrint.Visible = true;
                    btnPrints.Visible = true;
                    break;
                case 4:
                    btnfinish.Visible = false;
                    btnPrint.Visible = true;
                    btnPrints.Visible = true;
                    break;
                default:
                    btnSaveChange.Visible = true;
                    break;
            }

            if (ObjUopdateModel.HaveFile != null)
            {
                rdoIsHaveFile.Enabled = false;
                if (ObjUopdateModel.HaveFile.Value)
                {
                    rdoIsHaveFile.Items[0].Selected = true;
                }
                else
                {
                    rdoIsHaveFile.Items[1].Selected = true;
                }
            }

            if (ObjUopdateModel.IsChecks == false)
            {
                td_InserChange.Visible = false;
            }
            else
            {
                td_InserChange.Visible = true;
            }

            if (!IsPostBack)
            {

                QuotedID = Request["QuotedID"].ToInt32();
                OrderID = Request["OrderID"].ToInt32();
                CustomerID = Request["CustomerID"].ToInt32();

                //绑定数据
                BinderData();

                //判断花艺分离审核是否开启
                if (!ObjSysConfig.GetPowerByKey("FlowerSplit"))
                {
                    HideFlowerSplit.Value = "1";
                }

                ///判断是否报价单分离草错
                if (!ObjSysConfig.GetPowerByKey("QuotedPriceSplit"))
                {
                    HideSaleSplit.Value = "1";
                }

                //绑定风格
                CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
                CelebrationPackageStyle objCelebrationPackageStyle = new CelebrationPackageStyle();
                ddlfengge.DataSource = objCelebrationPackageStyle.GetByAll();
                ddlfengge.DataTextField = "StyleName";
                ddlfengge.DataValueField = "StyleId";
                ddlfengge.DataBind();
                ddlfengge.Items.Insert(0, new ListItem { Text = "请选择", Value = "0" });
                txtRemark.Text = ObjUopdateModel.Remark;

                var ObjEmPloyeeModel = ObjEmployeeBLL.GetByID(ObjUopdateModel.EmpLoyeeID);
                lblEmpLoyee.Text = ObjEmPloyeeModel.EmployeeName;
                lblCheckContent.Text = ObjUopdateModel.ChecksContent;
                if (lblCheckContent.Text == string.Empty)
                {
                    lblChecksNode.Visible = false;
                }

                this.ddlPackgeName.DataSource = ObjQuotedPriceBLL.GetByType(2).Where(C => C.IsDelete == false).ToList();
                this.ddlPackgeName.DataTextField = "QuotedTitle";
                this.ddlPackgeName.DataValueField = "QuotedID";
                this.ddlPackgeName.DataBind();


                //报价单名


                txtQuotedTitle.Text = ObjUopdateModel.QuotedTitle;
                txtNextFlowDate.Text = ObjUopdateModel.NextFlowDate.ToString().ToDateTime().ToShortDateString();

                if (txtQuotedTitle.Text != string.Empty)
                {
                    txtQuotedTitle.Enabled = false;
                }
                else
                {
                    var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
                    txtQuotedTitle.Text = ObjCustomerModel.Bride + ObjCustomerModel.PartyDate.Value.ToString("yyyyMMdd") + "报价单";
                }
            }

        }
        #endregion

        #region 绑定客户信息

        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (ObjCustomerModel.State == 8)
            {
                ObjCustomerModel.State = 13;        //未跟单状态的改成成功预定
                ObjCustomersBLL.Update(ObjCustomerModel);
            }


            lblCoder.Text = ObjOrderBLL.GetByID(OrderID).OrderCoder;
            txtBrideName.Text = ObjCustomerModel.Bride;
            txtGroomName.Text = ObjCustomerModel.Groom;
            ddlHotel.SelectedItem.Text = ObjCustomerModel.Wineshop;

            if (ObjCustomerModel.PartyDate.Value.Year == 1949)
            {
                if (txtpartyday.Text == "")
                {
                    txtpartyday.Text = "婚期未确定!";
                }
            }
            else
            {
                txtpartyday.Text = ShowPartyDate(ObjCustomerModel.PartyDate);
            }
            txtBridePhone.Text = ObjCustomerModel.BrideCellPhone;
            txtGroomPhone.Text = ObjCustomerModel.GroomCellPhone;
            lblTimerSpan.Text = ObjCustomerModel.TimeSpans;

            var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            txtAggregateAmount.Text = ObjquotedModel.AggregateAmount + string.Empty;
            lblOrderMoney.Text = ObjOrderBLL.GetByID(OrderID).EarnestMoney + string.Empty;
            txtEarnestMoney.Text = ObjquotedModel.EarnestMoney.ToString();
            txtRealAmount.Text = ObjquotedModel.FinishAmount + string.Empty;


            hideAggregateAmount.Value = ObjquotedModel.AggregateAmount + string.Empty;

            txtQuotedTitle.Text = ObjquotedModel.QuotedTitle;
            lblyukuan.Text = GetOverFinishMoney(ObjquotedModel.CustomerID.Value).ToString();

            SS_Report Report = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());
            txtSuccessDates.Text = Report.QuotedDateSucessDate.ToString().ToDateTime().ToShortDateString();
            hideDisState.Value = ObjquotedModel.IsDispatching.ToString();

            #region 修改报价单金额
            var DataList = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32());

            var ObjModel = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());
            ObjModel.FinishAmount = DataList.Sum(C => C.Total).ToString().ToDecimal();
            ObjModel.RealAmount = DataList.Sum(C => C.Total).ToString().ToDecimal();
            ObjQuotedPriceBLL.Update(ObjModel);

            #endregion
        }
        #endregion

        #region 页面状态

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

        public string HideforNoneImage(object Kind)
        {
            var ObjImageList = ObjQuotedPriceBLL.GetImageByKind(QuotedID, Kind.ToString().ToInt32(), 1);
            if (ObjImageList.Count > 0)
            {
                return string.Empty;
            }
            else
            {
                return "style='display: none;'";

            }

        }
        public string GetRowKey(object Key)
        {
            if (Key != null)
            {
                return Key.ToString();
            }
            else
            {
                return "style='display:none;'";
            }
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
                return "style='display:none;'";
            }
        }

        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var ObjList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).Where(C => C.IsFirstMake == 0).ToList();
            this.repfirst.DataSource = ObjList.OrderBy(C => C.CategoryID);
            this.repfirst.DataBind();
            BinderCuseomerDate();

            IsVisible();

        }
        #endregion

        #region 类型加载  读取销售金额
        /// <summary>l
        /// 读取金额
        /// </summary>
        public void BinderType()
        {
            var Model = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);

            if (Model == null)
            {
                Model = new FL_QuotedPriceForType();
                Model.OrderID = Request["OrderID"].ToInt32();
                Model.QuotedID = Request["QuotedID"].ToInt32();
                Model.CustomerID = Request["CustomerID"].ToInt32();
                Model.IsFirstMake = FirstMake;
                Model.CreateEmployee = User.Identity.Name.ToInt32();
                Model.CreateDate = DateTime.Now;
                Model.IsLock = 1;       // 1.代表锁定 不能修改  2.代表解锁  可以修改（不再使用   有策划师自行输入就行了）
                ObjDiscountBLL.Insert(Model);
            }
            else
            {
                txtRealAmount.Text = Model.Total.ToString().ToDecimal().ToString("f2");                                           //销售总价
                var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByOrderId(Request["OrderID"].ToInt32());
                ObjQuotedPriceModel.FinishAmount = Model.Total.ToString().ToDecimal().ToString("f2").ToDecimal();                   //修改报价单中的销售总价
                ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
            }
        }
        #endregion

        #region 获取产品者服务
        /// <summary>
        /// 获取产品或者服务
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
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
            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            var ObjItemList = new List<FL_QuotedPriceItems>();
            //获取二级项目
            var DataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList().Where(C => C.IsFirstMake == 0).ToList();

            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                //ObjtxtPrice.Enabled = true;
                var NewList = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }

            //获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3).Where(C => C.IsFirstMake == 0).ToList();
                if (ItemList.Count == 0)
                {
                    ObjItemList.Add(ObjItem);
                }
                else
                {
                    ItemList[0].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                }
            }

            ObjRep.DataSource = ObjItemList;
            ObjRep.DataBind();


            var DataItemList = ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32());

            var ItemTypeList = DataItemList.Where(C => C.Type == null).ToList();
            var CategoryList = ObjQuotedCatgoryBLL.GetByAll();
            if (ItemTypeList.Count > 0)
            {
                foreach (var item in ItemTypeList)
                {
                    foreach (var CategoryItem in CategoryList)
                    {
                        if (item.CategoryID == CategoryItem.QCKey)
                        {
                            item.Type = CategoryItem.Type;
                            ObjQuotedPriceItemsBLL.Update(item);
                        }
                    }
                }
            }

            txtMaterialPrice.Text = DataItemList.Where(C => C.Type == 2 && C.IsFirstMake == 0).Sum(C => C.Subtotal).ToString();     //物料价格

            txtPersonPrice.Text = DataItemList.Where(C => C.Type == 1 && C.IsFirstMake == 0).Sum(C => C.Subtotal).ToString();       //人员价格
            txtOtherPrice.Text = DataItemList.Where(C => C.Type == 3 && C.IsFirstMake == 0).Sum(C => C.Subtotal).ToString();     //其他价格


            var Model = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());

            if (ObjUopdateModel.IsChecks == true)
            {
                if (Model == null)
                {
                    #region 以前的单子  会执行这一步  或者物料价格为0 的单子 通常来说都是以前的单子
                    if (txtSalePrices.Text == "0" || txtSalePrices.Text == "")        //以前的单子  物料价格肯定为0   就直接显示物料的原价格
                    {
                        txtSalePrices.Text = txtMaterialPrice.Text.ToString();          //物料文本框价格
                        //针对以前做的单子 先生成数据  然后价格赋值
                        BinderType();
                        Model = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
                        Model.PPrice = txtPersonPrice.Text.Trim().ToDecimal();      //人员价格
                        Model.OPrice = txtOtherPrice.Text.Trim().ToDecimal();       //其他价格
                        Model.MPrice = txtSalePrices.Text.Trim().ToDecimal();       //物料价格
                        Model.Total = (Model.MPrice + Model.PPrice + Model.OPrice).ToString().ToDecimal();
                        ObjDiscountBLL.Update(Model);
                        txtRealAmount.Text = Model.Total.ToString();        //销售总价
                        txtSalePrices.Enabled = false;
                    }
                }
                else
                {
                    txtSalePrices.Text = Model.MPrice.ToString();
                    txtRealAmount.Text = Model.Total.ToString();        //销售总价
                    txtSalePrices.Enabled = false;
                }
                #endregion


            }
            else
            {
                if (Model != null)
                {
                    txtSalePrices.Text = Model.MPrice == null ? "0" : Model.MPrice.ToString();       //物料价格

                    Model.MPrice = txtSalePrices.Text.Trim().ToDecimal();
                    Model.PPrice = txtPersonPrice.Text.Trim().ToDecimal();
                    Model.OPrice = txtOtherPrice.Text.Trim().ToDecimal();
                    Model.Total = (Model.MPrice + Model.PPrice + Model.OPrice).ToString().ToDecimal();
                    ObjDiscountBLL.Update(Model);
                }

                //保存价格 方便读取金额
                BinderType();
            }
        }
        #endregion

        #region 三个大类 以及产品的保存

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
                        ObjCategoryForQuotedPriceModel.Type = ObjCategorItem.Type;
                        ObjCategoryForQuotedPriceModel.ItemLevel = 1;
                        ObjCategoryForQuotedPriceModel.OrderID = OrderID;
                        ObjCategoryForQuotedPriceModel.IsDelete = false;
                        ObjCategoryForQuotedPriceModel.IsSvae = false;
                        ObjCategoryForQuotedPriceModel.IsChange = false;
                        ObjCategoryForQuotedPriceModel.QuotedID = QuotedID;
                        ObjCategoryForQuotedPriceModel.PurchasePrice = 0;
                        ObjCategoryForQuotedPriceModel.Requirement = string.Empty;


                        ObjCategoryForQuotedPriceModel.Quantity = 1;
                        ObjCategoryForQuotedPriceModel.Unit = string.Empty;
                        ObjCategoryForQuotedPriceModel.ServiceContent = ObjCategorItem.Title;
                        ObjCategoryForQuotedPriceModel.UnitPrice = 0;
                        ObjCategoryForQuotedPriceModel.PurchasePrice = 0;

                        ObjCategoryForQuotedPriceModel.Productproperty = ObjCategorItem.Productproperty;
                        ObjCategoryForQuotedPriceModel.IsFirstMake = 0;
                        ObjCategoryForQuotedPriceModel.IsFinishMake = 0;
                        ObjCategoryForQuotedPriceModel.CreateDate = DateTime.Now;
                        ObjCategoryForQuotedPriceModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                        ObjCategoryForQuotedPriceModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                        ObjCategoryForQuotedPriceModel.ChangeEmployee = User.Identity.Name.ToInt32();

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
                        ObjModel.Type = Objitem.Type;
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
                        ObjModel.Quantity = 1;
                        ObjModel.IsFirstMake = 0;
                        ObjModel.IsFinishMake = 0;
                        ObjModel.CreateDate = DateTime.Now;
                        ObjModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                        ObjModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                        ObjModel.ChangeEmployee = User.Identity.Name.ToInt32();

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
        #endregion

        #region 创建单个产品
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
                ObjSetModel.Type = ObjQuotedCatgoryBLL.GetByID(QcKey).Type;
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
                ObjSetModel.PartyDay = txtpartyday.Text.ToDateTime();
                ObjSetModel.TimerSpan = lblTimerSpan.Text;
                ObjSetModel.OrderID = OrderID;
                ObjSetModel.ServiceContent = ObjProductModel.ProductName;
                ObjSetModel.IsFirstMake = 0;
                ObjSetModel.IsFinishMake = 0;
                ObjSetModel.CreateDate = DateTime.Now;
                ObjSetModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                ObjSetModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                ObjSetModel.ChangeEmployee = User.Identity.Name.ToInt32();
                ObjQuotedPriceItemsBLL.Insert(ObjSetModel);
            }

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
            var CGKey = this.hideThirdValue.Value.Split(',');
            int ThridId = hideThirdCategoryID.Value.ToInt32();
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
                    ObjSetModel = new FL_QuotedPriceItems();
                    ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                    ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                    ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                    ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent;
                    ObjSetModel.Type = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Type;
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
                    ObjSetModel.PartyDay = txtpartyday.Text.ToDateTime();
                    ObjSetModel.TimerSpan = lblTimerSpan.Text;
                    ObjSetModel.OrderID = OrderID;
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjSetModel.IsFirstMake = 0;
                    ObjSetModel.IsFinishMake = 0;
                    ObjSetModel.CreateDate = DateTime.Now;
                    ObjSetModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                    ObjSetModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                    ObjSetModel.ChangeEmployee = User.Identity.Name.ToInt32();
                    ObjQuotedPriceItemsBLL.Insert(ObjSetModel);


                    SalePrice += ObjSetModel.UnitPrice.Value * ObjSetModel.Quantity.Value;
                }
            }

            txtAggregateAmount.Text = (txtAggregateAmount.Text.ToDecimal() + SalePrice) + "";
            hideAggregateAmount.Value = (txtAggregateAmount.Text.ToDecimal() + SalePrice) + "";
            txtRealAmount.Text = (txtRealAmount.Text.ToDecimal() + SalePrice) + "";
            SaveallChange();
            BinderData();

        }
        #endregion

        #endregion

        #region 点击保存方法
        /// <summary>
        /// 保存报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (!txtpartyday.Text.Contains("婚期未确定") && txtpartyday.Text != string.Empty)
            {
                var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
                ObjCustomerModel.Bride = txtBrideName.Text.Trim().ToString();
                ObjCustomerModel.BrideCellPhone = txtBridePhone.Text.Trim().ToString();
                ObjCustomerModel.Groom = txtGroomName.Text.Trim().ToString();
                ObjCustomerModel.GroomCellPhone = txtGroomPhone.Text.Trim().ToString();
                ObjCustomerModel.Wineshop = ddlHotel.SelectedItem.Text.ToString();
                ObjCustomerModel.PartyDate = txtpartyday.Text.ToDateTime();
                ObjCustomersBLL.Update(ObjCustomerModel);
            }
            //保存金额至另一个表中  方便加载时的读取
            SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());

            SaveallChange();
            JavaScriptTools.AlertWindow("保存成功!", Page);
        }
        #endregion

        #region 保存所有
        /// <summary>
        /// 保存所有
        /// </summary>
        private void SaveallChange()
        {
            decimal IteMoeyMoney = 0;
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

                ObjQuotedPriceItemsBLL.Update(ObjItem);
                if (ObjItem.ItemAmount > 0)
                {
                    IteMoeyMoney += ItemAmoutMoney.Value;
                }
                ItemAmoutMoney = 0;
                //更新报价单主体
            }

            txtAggregateAmount.Text = IteMoeyMoney.ToString();
            SaveQuotedPrice();
            BinderData();
        }
        #endregion

        #region 保存报价单  套系
        /// <summary>
        /// 保存
        /// </summary>
        private void SaveQuotedPrice()
        {
            var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjUopdateModel.AggregateAmount = txtAggregateAmount.Text.ToDecimal();
            ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
            ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
            ObjUopdateModel.Remark = txtRemark.Text;
            //ObjUopdateModel.CreateDate = DateTime.Now;
            ObjUopdateModel.NextFlowDate = txtNextFlowDate.Text.ToDateTime();
            ObjUopdateModel.StarDispatching = false;
            ObjUopdateModel.SaleEmployee = hideSaleEmpLoyeeID.Value.ToInt32();

            if (ddlType.SelectedItem.Text == "套系")
            {
                ObjUopdateModel.PakegName = ddlPackgeName.Text;
            }
            else
            {
                ObjUopdateModel.PakegName = "新建";
            }
            ObjUopdateModel.PakegTyper = ddlfengge.SelectedIndex > 0 ? ddlfengge.SelectedItem.Text : "默认风格";
            if (ObjUopdateModel.IsDispatching == 0)
            {
                ObjUopdateModel.IsDispatching = 1;
            }

            if (ObjUopdateModel.QuotedTitle == null || ObjUopdateModel.QuotedTitle == string.Empty)
            {
                ObjUopdateModel.QuotedTitle = txtQuotedTitle.Text;
            }
            ObjUopdateModel.IsFirstCreate = true;
            ObjQuotedPriceBLL.Update(ObjUopdateModel);
        }
        #endregion

        #region 提交审核
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChecks_Click(object sender, EventArgs e)
        {
            if (rdoIsHaveFile.SelectedItem == null)
            {
                JavaScriptTools.AlertWindow("请选择是否有取件！", Page);
                return;
            }
            else
            {
                if (rdoIsHaveFile.SelectedValue.Equals("0") && chkPhoto.Checked == false && chkAvi.Checked == false)
                {
                    JavaScriptTools.AlertWindow("请勾选取件类型！", Page);
                    return;
                }
                if (repfirst.Items.Count > 0)
                {
                    //先保存页面所有修改
                    SaveallChange();


                    var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                    ObjUopdateModel.AggregateAmount = hideAggregateAmount.Value.ToDecimal();
                    ObjUopdateModel.RealAmount = txtRealAmount.Text.ToDecimal();
                    ObjUopdateModel.EarnestMoney = txtEarnestMoney.Text.ToDecimal();
                    ObjUopdateModel.IsChecks = false;
                    ObjUopdateModel.CheckState = 2;
                    ObjUopdateModel.HaveFile = rdoIsHaveFile.SelectedItem.Text == "有取件" ? true : false;
                    if (HideFlowerSplit.Value == string.Empty)
                    {
                        ObjUopdateModel.FlowerCheck = false;
                        ObjUopdateModel.FlowerCheckEmployee = hideEmpLoyeeID.Value.ToInt32();
                    }

                    ObjUopdateModel.IsDispatching = 2;
                    ObjUopdateModel.ChecksTitle = "已经提交审核";
                    ObjUopdateModel.ChecksContent = "";
                    ObjUopdateModel.IsChecks = false;
                    ObjUopdateModel.Remark = txtRemark.Text;
                    Department ObjDepartmentBLL = new Department();
                    ObjUopdateModel.ChecksEmployee = ObjEmployeeBLL.GetMineCheckEmployeeID(User.Identity.Name.ToInt32()); //ObjDepartmentBLL.(ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).DepartmentID).DepartmentManager;

                    ////总经理不需要审核
                    //if (ObjUopdateModel.ChecksEmployee == User.Identity.Name.ToInt32())
                    //{
                    //    ObjUopdateModel.CheckState = 3;
                    //    ObjUopdateModel.IsDispatching = 3;
                    //    ObjUopdateModel.IsChecks = true;
                    //}

                    //if (GetNeedMamagerByKey("ManagerCheck"))
                    //{

                    //    //店长不需要审核
                    //    if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
                    //    {
                    //        ObjUopdateModel.CheckState = 3;
                    //        ObjUopdateModel.IsDispatching = 3;
                    //        ObjUopdateModel.IsChecks = true;
                    //    }
                    //}

                    //if (GetNeedMamagerByKey("QuotedNoneCheck"))
                    //{
                    //    ObjUopdateModel.CheckState = 3;
                    //    ObjUopdateModel.IsDispatching = 3;
                    //    ObjUopdateModel.IsChecks = true;
                    //}


                    //更新报价单主体
                    ObjQuotedPriceBLL.Update(ObjUopdateModel);
                    BinderData();

                    ////根据管理弹出不同的信息
                    //if (ObjUopdateModel.ChecksEmployee == User.Identity.Name.ToInt32())
                    //{
                    //    JavaScriptTools.CloseWindow("成功提交到部门审核人处!审核人为" + ObjEmployeeBLL.GetByID(ObjUopdateModel.ChecksEmployee).EmployeeName + ",现在可以制作派工明细！", Page);
                    //}
                    //else
                    //{
                    //    JavaScriptTools.CloseWindow("成功提交到部门审核人处!审核人为" + ObjEmployeeBLL.GetByID(ObjUopdateModel.ChecksEmployee).EmployeeName + ",审核通过后可确认修改报价单！", Page);
                    //    MissionManager ObjMissManagerBLL = new MissionManager();
                    //    ObjMissManagerBLL.WeddingMissionCreate(ObjUopdateModel.CustomerID.Value, 1, (int)MissionTypes.QuotedCheck, DateTime.Now, ObjUopdateModel.ChecksEmployee.Value, "?QuotedID=" + ObjUopdateModel.QuotedID + "&OrderID=" + ObjUopdateModel.OrderID + "&FlowOrder=1&CustomerID=" + ObjUopdateModel.CustomerID, MissionChannel.FL_TelemarketingManager, ObjUopdateModel.EmpLoyeeID.Value, ObjUopdateModel.QuotedID);
                    //}



                }
                else
                {
                    JavaScriptTools.AlertWindow("报价单不能为空！", Page);
                }
            }
        }
        #endregion

        #region 确认此订单
        /// <summary>
        /// 确认此订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnfinish_Click(object sender, EventArgs e)
        {

            if (rdoIsHaveFile.SelectedItem == null)
            {
                JavaScriptTools.AlertWindow("请选择是否有取件！", Page);
                return;
            }

            if (txtpartyday.Text == "婚期未确定!")
            {
                JavaScriptTools.AlertWindow("婚期未确定，不能进入下一步!", Page);
                return;
            }
            else
            {
                ////保存金额至另一个表中  方便加载时的读取
                //SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());

                //先保存页面所有修改
                SaveallChange();

                var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);
                ObjCustomerModel.Bride = txtBrideName.Text.Trim().ToString();
                ObjCustomerModel.BrideCellPhone = txtBridePhone.Text.Trim().ToString();
                ObjCustomerModel.Groom = txtGroomName.Text.Trim().ToString();
                ObjCustomerModel.GroomCellPhone = txtGroomPhone.Text.Trim().ToString();
                ObjCustomerModel.Wineshop = ddlHotel.SelectedItem.Text.ToString();
                ObjCustomerModel.PartyDate = txtpartyday.Text.ToDateTime();
                ObjCustomerModel.State = (int)CustomerStates.DoingChecksQuotedPrice;
                ObjCustomersBLL.Update(ObjCustomerModel);

                //btnSaveChange_Click(sender, e);取件
                var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                ObjUopdateModel.Remark = txtRemark.Text;
                ObjUopdateModel.IsDispatching = 4;
                ObjUopdateModel.StarDispatching = false;
                ObjUopdateModel.IsChecks = true;
                ObjUopdateModel.HaveFile = ObjUopdateModel.HaveFile = rdoIsHaveFile.SelectedItem.Text == "有取件" ? true : false;
                ObjQuotedPriceBLL.Update(ObjUopdateModel);

                CreateCelebrationProductItem(ObjUopdateModel.EmpLoyeeID);

                btnSaveChange.Visible = false;

                Report ObjReportBLL = new Report();
                var ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());
                ObjReportModel.QuotedDateSucessDate = DateTime.Now;
                ObjReportModel.QuotedMoney = ObjUopdateModel.FinishAmount;
                ObjReportModel.Partydate = txtpartyday.Text.ToDateTime();
                ObjReportBLL.Update(ObjReportModel);

                MissionDetailed ObjMissionDetauledBLL = new MissionDetailed();
                ObjMissionDetauledBLL.UpdateforFlow((int)MissionTypes.Quoted, Request["QuotedID"].ToInt32(), User.Identity.Name.ToInt32());

                //有取件时录入取件系统
                if (rdoIsHaveFile.SelectedItem.Text == "有取件")
                {
                    HA.PMS.BLLAssmblly.CS.TakeDisk objTakeDiskBLL = new BLLAssmblly.CS.TakeDisk();
                    CS_TakeDisk ObjTakeDiskModel = new CS_TakeDisk();

                    //取件类型为视频
                    if (chkAvi.Checked)
                    {
                        ObjTakeDiskModel.HaveFile = true;
                    }
                    else
                    {
                        ObjTakeDiskModel.HaveFile = false;
                    }
                    //取件类型为照片
                    if (chkPhoto.Checked)
                    {
                        ObjTakeDiskModel.HavePhoto = true;
                    }
                    else
                    {
                        ObjTakeDiskModel.HavePhoto = false;
                    }
                    ObjTakeDiskModel.CustomerID = CustomerID;
                    ObjTakeDiskModel.IsDelete = false;
                    ObjTakeDiskModel.IsCheck = false;

                    ObjTakeDiskModel.State = 0;
                    ObjTakeDiskModel.UpdateEmployee = User.Identity.Name.ToInt32();
                    ObjTakeDiskModel.QuotedEmployee = ObjUopdateModel.EmpLoyeeID.Value;
                    ObjTakeDiskModel.IsFinish = false;


                    ObjTakeDiskModel.OrderID = Request["OrderID"].ToInt32();
                    objTakeDiskBLL.Insert(ObjTakeDiskModel);
                }
                #region 头部报表显示

                int EmployeeId = User.Identity.Name.ToInt32();

                var FinishTargetList = ObjFinishTargetSumBLL.GetByAll();
                var TargetList = ObjTargetBLL.GetByAll();


                bool isExists = false;
                List<FL_FinishTargetSum> FinishTargetSumList = new List<FL_FinishTargetSum>();
                foreach (var item in FinishTargetList)
                {
                    if (item.EmployeeID == EmployeeId && item.TargetTitle == "当期新增订单总额" && item.Year == DateTime.Now.Year)
                    {
                        FinishTargetSumList.Add(item);
                        isExists = true;
                    }
                }

                if (isExists == true)       //存在 则只需修改
                {
                    foreach (var item in FinishTargetSumList)
                    {
                        if (item.TargetTitle == "当期新增订单总额" && item.Year == DateTime.Now.Year)
                        {
                            FL_FinishTargetSum FinishTargetSum = ObjFinishTargetSumBLL.GetByID(item.FinishKey);
                            GetUpdateQuotePriceByMonth(FinishTargetSum, EmployeeId);

                            ObjFinishTargetSumBLL.Update(FinishTargetSum);
                        }
                    }
                }




                if (isExists == false)          //不存在 新增方法
                {
                    foreach (var item in TargetList)
                    {
                        if (item.TargetTitle == "当期新增订单总额")
                        {
                            FL_FinishTargetSum FinishTargetSum = new FL_FinishTargetSum();

                            FinishTargetSum.TargetID = item.TargetID;
                            FinishTargetSum.TargetTitle = item.TargetTitle;
                            FinishTargetSum.DepartmentID = ObjEmployeeBLL.GetDepartmentID(EmployeeId);
                            FinishTargetSum.EmployeeID = EmployeeId;
                            FinishTargetSum.CreateEmployeeID = EmployeeId;

                            GetAddQuotePriceByMonth(FinishTargetSum, EmployeeId);



                            ObjFinishTargetSumBLL.Insert(FinishTargetSum);
                        }
                    }
                }

                #endregion

                //更改TargetState为1 说明销售额中 该条数据已经添加  
                SS_Report ReportModel = ObjReportBLL.GetByCustomerID(CustomerID, EmployeeId);
                ReportModel.TargetState = 1;
                ObjReportBLL.Update(ReportModel);

                //操作日志
                CreateHandle();
                JavaScriptTools.AlertWindow("保存成功！", Page);

                //Response.Write("<script type='text/javascript'>window.close();</script>");
                this.Page.Response.Redirect(this.Page.Request.Url.ToString());

            }


        }
        #endregion

        #region 满意度调查
        public void BindSatisfaction()
        {
            DegreeOfSatisfaction ObjDegreeBLL = new DegreeOfSatisfaction();
            Customers ObjCustomerBLL = new Customers();
            var DataList = ObjCustomerBLL.GetByAll().Where(C => C.PartyDate <= DateTime.Now.ToShortDateString().ToDateTime() && C.PartyDate != DateTime.Now.ToString("1949-10-01").ToDateTime()).ToList();

            foreach (var item in DataList)
            {
                var ObjDegreeModel = ObjDegreeBLL.GetByCustomersID(item.CustomerID);
                if (ObjDegreeModel == null)
                {
                    ObjDegreeBLL.Insert(new CS_DegreeOfSatisfaction()
                    {
                        CustomerID = item.CustomerID,
                        SumDof = "",
                        DofContent = "",
                        DofDate = null,
                        IsDelete = false,
                        DegreeResult = null,
                        State = 0,
                        UpdateTime = DateTime.Now.ToString().ToDateTime(),
                        UpdateEmployeeID = User.Identity.Name.ToInt32(),
                        PlanDate = null,
                    });
                }
            }

        }
        #endregion

        #region 创建执行表

        /// <summary>
        /// 创建执行表
        /// </summary>
        private void CreateCelebrationProductItem(int? EmpLoyeeID)
        {
            Celebration ObjCelebrationBLL = new Celebration();


            var CelebrationID = ObjCelebrationBLL.Insert(new FL_Celebration()
            {

                CustomerID = CustomerID,
                OrderID = Request["OrderID"].ToInt32(),
                IsDelete = false,
                ParentCelebrationID = 0,
                QuotedID = QuotedID,

                QuotedEmpLoyee = EmpLoyeeID
            });
            var ObjCelModel = ObjCelebrationBLL.GetByQuotedID(QuotedID);

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
                ObjCategoryforDispatching.PurchasePrice = ObjThiredItem.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjThiredItem.Productproperty;
                ObjCategoryforDispatching.SupplierName = ObjThiredItem.SupplierName;
                ObjCategoryforDispatching.Classification = ObjThiredItem.Classification;
                ObjCelebrationProductItemBLL.Insert(ObjCategoryforDispatching);
            }

            MissionManager ObjMissManagerBLL = new MissionManager();
            ObjMissManagerBLL.WeddingMissionCreate(ObjCelModel.CustomerID, 1, (int)MissionTypes.Celebration, DateTime.Now, ObjCelModel.QuotedEmpLoyee.Value, "?QuotedID=" + ObjCelModel.QuotedID + "&CustomerID=" + ObjCelModel.CustomerID, MissionChannel.Quoted, ObjCelModel.QuotedEmpLoyee.Value, ObjCelModel.CelebrationID);


            //触发一个制作收款计划给策划师
            string QPlanNodeKey = "?OrderID=" + ObjCelModel.OrderID + "&QuotedID=" + QuotedID + "&CustomerID=" + ObjCelModel.CustomerID + "&NeedPopu=1";
            ObjMissManagerBLL.WeddingMissionCreate(ObjCelModel.CustomerID, 1, (int)MissionTypes.QuotedCollectionsPlan, DateTime.Now, ObjCelModel.QuotedEmpLoyee.Value, QPlanNodeKey, MissionChannel.Quoted, ObjCelModel.QuotedEmpLoyee.Value, ObjCelModel.CelebrationID);
            //   Response.Redirect("DispatchingManager.aspx?QuotedID="+QuotedID+"&NeedPopu=1&OrderID="+OrderID);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var ObjItem = ObjQuotedPriceItemsBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            if (ObjItem != null)
            {

                ObjQuotedPriceItemsBLL.Delete(ObjItem);

                txtRealAmount.Text = (txtRealAmount.Text.ToDecimal() - ObjItem.Subtotal.ToString().ToDecimal()).ToString();
                txtAggregateAmount.Text = (txtAggregateAmount.Text.ToDecimal() - ObjItem.Subtotal.ToString().ToDecimal()).ToString();
                SaveQuotedPrice();
                BinderData();
            }
        }
        #endregion

        #region 保存数据

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
        #endregion

        #region 点击导入 导入套餐#endregion btnInsert_Click

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ObjQuotedPriceItemsBLL.DeleteByQuotedID(QuotedID);
            int SelectQuotedID = ddlPackgeName.SelectedValue.ToInt32();
            //一级类别
            var ObjChangeFirstList = ObjQuotedPriceItemsBLL.GetByQuotedID(ddlPackgeName.SelectedValue.ToInt32(), 1);

            //二级类别
            var ObjChangeSecondList = ObjQuotedPriceItemsBLL.GetByQuotedID(ddlPackgeName.SelectedValue.ToInt32(), 2);

            //三级产品类别
            var ObjProductList = ObjQuotedPriceItemsBLL.GetByQuotedID(ddlPackgeName.SelectedValue.ToInt32(), 3);
            JavaScriptTools.AlertWindow("导入成功", Page);
            ObjQuotedPriceItemsBLL.DeleteByQuotedID(QuotedID);
            foreach (var ObjFirst in ObjChangeFirstList)
            {
                // ObjFirst.ChangeID = 0;
                ObjFirst.QuotedID = QuotedID;
                ObjFirst.OrderID = OrderID;
                ObjFirst.ParentQuotedID = 0;
                ObjFirst.ExistAdd = false;
                ObjFirst.IsChange = false;
                ObjFirst.IsDelete = false;

                ObjQuotedPriceItemsBLL.Insert(ObjFirst);
            }

            foreach (var ObjFirst in ObjChangeSecondList)
            {
                // ObjFirst.ChangeID = 0;
                ObjFirst.QuotedID = QuotedID;
                ObjFirst.OrderID = OrderID;
                ObjFirst.ParentQuotedID = 0;
                ObjFirst.ExistAdd = false;
                ObjFirst.IsChange = false;
                ObjFirst.IsDelete = false;

                ObjQuotedPriceItemsBLL.Insert(ObjFirst);
            }

            foreach (var ObjFirst in ObjProductList)
            {
                //ObjFirst.ChangeID = 0;
                ObjFirst.QuotedID = QuotedID;
                ObjFirst.OrderID = OrderID;
                ObjFirst.ParentQuotedID = 0;
                ObjFirst.ExistAdd = false;
                ObjFirst.IsChange = false;
                ObjFirst.IsDelete = false;

                ObjQuotedPriceItemsBLL.Insert(ObjFirst);
            }

            #region 套餐有图片  同样的这个订单相应的产品也有图片


            FL_QuotedPricefileManager FileModel = new FL_QuotedPricefileManager();
            var DataList = ObjQuotedPriceBLL.GetQuotedPricefileByQuotedID(SelectQuotedID, 1);
            if (DataList.Count > 0)
            {
                var ItemDataList = ObjQuotedPriceItemsBLL.GetByQuotedID(SelectQuotedID);
                foreach (var item in DataList)
                {
                    var DataItem = ObjQuotedPriceItemsBLL.GetByID(item.KindID);


                    #region 图片

                    FL_QuotedPricefileManager ObjFileModel = new FL_QuotedPricefileManager();
                    ObjFileModel.CreateDate = DateTime.Now;
                    ObjFileModel.FileAddress = item.FileAddress;
                    ObjFileModel.Filename = item.Filename;
                    ObjFileModel.QuotedID = QuotedID;
                    ObjFileModel.SortOrder = 1;
                    ObjFileModel.KindID = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).FirstOrDefault(C => C.ServiceContent == DataItem.ServiceContent && C.ItemLevel == 3).ChangeID;
                    ObjFileModel.Type = 1;
                    ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjFileModel);
                    #endregion
                }
            }
            #endregion

            var Model = ObjQuotedPriceBLL.GetByQuotedTitle(ddlPackgeName.SelectedItem.Text);
            txtAggregateAmount.Text = Model.AggregateAmount == null ? "0" : Model.AggregateAmount.ToString();
            txtRealAmount.Text = Model.RealAmount == null ? "0" : Model.RealAmount.ToString();


            BinderData();

        }
        #endregion

        #region MyRegion

        protected void ddlfengge_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ddlPackgeName.DataSource = ObjQuotedPriceBLL.GetByType(2).Where(C=>C.PakegTyper==ddlfengge.SelectedItem.Text);
            //this.ddlPackgeName.DataTextField = "QuotedTitle";
            //this.ddlPackgeName.DataValueField = "QuotedID";
            //this.ddlPackgeName.DataBind();
        }

        public string GetAvailableCount(object productid, object rowType)
        {
            return Convert.ToInt32(rowType) == 2 ? storehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(Request["CustomerID"])).ToString() : string.Empty;
        }



        public string GetChangeSummoney(object ChangeID)
        {
            QuotedSourceItem ObjItemBLL = new QuotedSourceItem();
            return ObjItemBLL.GetByChangeID(ChangeID.ToString().ToInt32()).Sum(C => C.Money).ToString();

        }
        #endregion

        #region 新增订单总额
        public void GetAddQuotePriceByMonth(FL_FinishTargetSum FinishTargetSum, int EmployeeId)
        {
            FinishTargetSum.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            FinishTargetSum.Year = DateTime.Now.Year;
            FinishTargetSum.MonthPlan1 = 0;
            FinishTargetSum.MonthFinsh1 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 1);
            FinishTargetSum.MonthPlan2 = 0;
            FinishTargetSum.MonthFinish2 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 2);
            FinishTargetSum.MonthPlan3 = 0;
            FinishTargetSum.MonthFinish3 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 3);
            FinishTargetSum.MonthPlan4 = 0;
            FinishTargetSum.MonthFinish4 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 4);
            FinishTargetSum.MonthPlan5 = 0;
            FinishTargetSum.MonthFinish5 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 5);
            FinishTargetSum.MonthPlan6 = 0;
            FinishTargetSum.MonthFinish6 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 6);
            FinishTargetSum.MonthPlan7 = 0;
            FinishTargetSum.MonthFinish7 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 7);
            FinishTargetSum.MonthPlan8 = 0;
            FinishTargetSum.MonthFinish8 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 8);
            FinishTargetSum.MonthPlan9 = 0; ;
            FinishTargetSum.MonthFinish9 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 9);
            FinishTargetSum.MonthPlan10 = 0; ;
            FinishTargetSum.MonthFinish10 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 10);
            FinishTargetSum.MonthPlan11 = 0; ;
            FinishTargetSum.MonthFinish11 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 11);
            FinishTargetSum.MonthPlan12 = 0; ;
            FinishTargetSum.MonthFinish12 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 12);
            FinishTargetSum.Completionrate = 0;
            FinishTargetSum.LastYearCompletionrate = 0;
            FinishTargetSum.FinishSum = 0;
            FinishTargetSum.OveryearRate = 0;
            FinishTargetSum.OverYearFinishSum = 0;
            FinishTargetSum.LastYearFinishSum = 0;

            FinishTargetSum.Unite = "个";
            FinishTargetSum.IsActive = false;
            FinishTargetSum.PlanSum = FinishTargetSum.MonthPlan1 + FinishTargetSum.MonthPlan2 + FinishTargetSum.MonthPlan3 + FinishTargetSum.MonthPlan4 + FinishTargetSum.MonthPlan5 + FinishTargetSum.MonthPlan6 + FinishTargetSum.MonthPlan7 + FinishTargetSum.MonthPlan8 + FinishTargetSum.MonthPlan9 + FinishTargetSum.MonthPlan10 + FinishTargetSum.MonthPlan11 + FinishTargetSum.MonthPlan12;
        }
        #endregion

        #region 修改订单总额
        public void GetUpdateQuotePriceByMonth(FL_FinishTargetSum FinishTargetSum, int EmployeeId)
        {
            FinishTargetSum.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            FinishTargetSum.MonthFinsh1 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 1);
            FinishTargetSum.MonthFinish2 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 2);
            FinishTargetSum.MonthFinish3 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 3);
            FinishTargetSum.MonthFinish4 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 4);
            FinishTargetSum.MonthFinish5 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 5);
            FinishTargetSum.MonthFinish6 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 6);
            FinishTargetSum.MonthFinish7 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 7);
            FinishTargetSum.MonthFinish8 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 8);
            FinishTargetSum.MonthFinish9 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 9);
            FinishTargetSum.MonthFinish10 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 10);
            FinishTargetSum.MonthFinish11 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 11);
            FinishTargetSum.MonthFinish12 += ObjReportBLL.GetSumQuotedMoneyByMonth(EmployeeId, CustomerID, 12);
        }
        #endregion

        #region 隐藏
        /// <summary>
        /// 隐藏
        /// </summary>   
        public string ShowOrHide()
        {
            var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByQuotedID(Request["QuotedID"].ToInt32());
            if (ObjQuotedPriceModel.IsChecks == true && ObjQuotedPriceModel.IsDispatching == 4)         // 确认签约的订单
            {
                return "style='display:none;'";             //隐藏添加类别按钮
            }
            return "";
        }

        public void IsVisible()
        {
            var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByQuotedID(Request["QuotedID"].ToInt32());
            if (ObjQuotedPriceModel.IsChecks == true && ObjQuotedPriceModel.IsDispatching == 4)         // 确认签约的订单
            {
                for (int i = 0; i < repfirst.Items.Count; i++)
                {
                    (repfirst.Items[i].FindControl("btnSaveItem") as Button).Visible = false;           //隐藏保存分项
                    Repeater repDataList = repfirst.Items[i].FindControl("repdatalist") as Repeater;
                    for (int j = 0; j < repDataList.Items.Count; j++)
                    {
                        (repDataList.Items[j].FindControl("lnkbtnDelete") as LinkButton).Visible = false;       //隐藏删除按钮
                    }
                }
                txtBrideName.Enabled = false;
                txtBridePhone.Enabled = false;
                txtGroomName.Enabled = false;
                txtGroomPhone.Enabled = false;
                ddlHotel.Enabled = false;
            }
        }
        #endregion

        #region 动态生成Button 及它的点击事件
        /// <summary>
        /// 动态生成
        /// </summary>
        public void CreateButton()
        {
            int MaxIsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Where(C => C.IsFinishMake == 1).Max(C => C.IsFirstMake).ToString().ToInt32();
            Button btnCreate = new Button();



            for (int i = 0; i < MaxIsFirstMake; i++)
            {
                //变更单
                btnCreate = new Button();
                btnCreate.Text = "第" + (i + 1) + "次变更单";
                btnCreate.CssClass = "btn btn-success btn-mini";
                btnCreate.Enabled = false;
                this.div_Change.Controls.Add(btnCreate);

                //变更金额
                Button lblAmount = new Button();
                var DiscountModel = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), i + 1);
                lblAmount.Text = DiscountModel.Total.ToString();
                lblAmount.CssClass = "btn btn-danger btn-mini";
                lblAmount.BackColor = Color.White;
                lblAmount.BorderColor = Color.White;
                lblAmount.ForeColor = Color.Black;
                lblAmount.Enabled = false;

                this.div_ChangeAmount.Controls.Add(lblAmount);


                //变更时间
                Button lblChangeDate = new Button();
                lblChangeDate.Text = DiscountModel.CreateDate.ToString();
                lblChangeDate.CssClass = "btn btn-danger btn-mini";
                lblChangeDate.BackColor = Color.White;
                lblChangeDate.BorderColor = Color.White;
                lblChangeDate.ForeColor = Color.Black;
                lblChangeDate.Enabled = false;
                this.div_ChangeDate.Controls.Add(lblChangeDate);

                //变更人
                Button lblChangeEmployee = new Button();
                lblChangeEmployee.Text = GetEmployeeName(DiscountModel.CreateEmployee.ToString().ToInt32());
                lblChangeEmployee.CssClass = "btn btn-danger btn-mini";
                lblChangeEmployee.BackColor = Color.White;
                lblChangeEmployee.BorderColor = Color.White;
                lblChangeEmployee.ForeColor = Color.Black;
                lblChangeEmployee.Enabled = false;
                this.div_ChangeEmployee.Controls.Add(lblChangeEmployee);

                //查看
                Button lbtnHandle = new Button();
                lbtnHandle.Text = "查看";
                lbtnHandle.CommandArgument = (i + 1).ToString();
                lbtnHandle.CssClass = "btn btn-danger btn-mini";
                this.div_Handle.Controls.Add(lbtnHandle);

                lbtnHandle.Click += button_Click;

                Literal r = new Literal();
                r.Text = "<br/>";
                div_Change.Controls.Add(r);

                Literal s = new Literal();
                s.Text = "<br/>";
                div_ChangeAmount.Controls.Add(s);

                Literal d = new Literal();
                d.Text = "<br/>";
                div_ChangeDate.Controls.Add(d);

                Literal a = new Literal();
                a.Text = "<br/>";
                div_ChangeEmployee.Controls.Add(a);

                Literal l = new Literal();
                l.Text = "<br/>";
                div_Handle.Controls.Add(l);
            }


        }
        private void button_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            Response.Write("<script>window.open('QuotedPriceShowOrPrint.aspx?QuotedID=" + QuotedID + "&OrderID=" + OrderID + "&CustomerID=" + CustomerID + "&IsFirstMake=" + button.CommandArgument.ToString() + "')</script>");
            BinderData();
        }

        #endregion

        #region 点击新增变更单
        protected void btn_ShowInsert_Click(object sender, EventArgs e)
        {
            showstate = 1;
        }
        #endregion

        #region 是否隐藏添加类别
        /// <summary>
        /// 是否隐藏
        /// </summary>
        /// <returns></returns>
        public string InsertShow()
        {
            //if (showstate == 1)
            //{
            //    return "style='display:block;'";
            //}
            //else if (showstate == 0)
            //{
            //    return "style='display:none;'";
            //}
            return "";
        }
        #endregion

        #region 获取QuotedID


        public string GetQuotedID(object Type)
        {
            int Types = Type.ToString().ToInt32();
            if (Types == 1)
            {
                QuotedID = Request["QuotedID"].ToInt32();
                return QuotedID.ToString(); ;
            }
            else if (Types == 2)
            {
                OrderID = Request["OrderID"].ToInt32();
                return OrderID.ToString();
            }
            else if (Types == 3)
            {
                CustomerID = Request["CustomerID"].ToInt32();
                return CustomerID.ToString();
            }
            return "";
        }
        #endregion

        #region 是否拥有变更单的功能
        /// <summary>
        /// 判断 拥有变更单的功能
        /// </summary>
        /// <returns></returns>
        public string IsHaveChange()
        {
            var Model = ObjQuotedPriceBLL.GetByQuotedsID(QuotedID);
            if (Model.EmpLoyeeID == User.Identity.Name.ToInt32())       //当前人是策划师
            {
                return NewInsertChangeHide();
            }
            else            //当前登陆人不是策划师
            {
                return "style='display:none;'";
            }
        }

        public string NewInsertChangeHide()
        {
            bool IsShow = false;        //默认 新增变更单不显示
            var DataList = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, 0);
            foreach (var item in DataList)
            {
                if (item.IsFinishMake == 1)     //等于1  说明已派工   已派工就可以新增变更单了
                {
                    IsShow = true;
                }
            }
            if (IsShow == true)         //已派工可以新增变更单
            {
                return string.Empty;
            }
            else        //未派工 看不见新增变更单这一项
            {
                return "style='display:none;'";
            }
        }
        #endregion

        #region 点击刷新
        /// <summary>
        /// 刷新
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            #region 修改报价单金额
            var DataList = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32());
            if (DataList.Count > 0)
            {
                var ObjModel = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());
                ObjModel.FinishAmount = DataList.Sum(C => C.Total).ToString().ToDecimal();
                ObjModel.RealAmount = DataList.Sum(C => C.Total).ToString().ToDecimal();
                ObjQuotedPriceBLL.Update(ObjModel);

                SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());
            }
            #endregion


            Response.Redirect(this.Page.Request.Url.ToString());
        }
        #endregion

        #region 点击保存 保存签约时间
        /// <summary>
        /// 保存
        /// </summary> 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var ReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());
            ReportModel.QuotedDateSucessDate = txtSuccessDates.Text.ToString().ToDateTime();
            int result = ObjReportBLL.Updates(ReportModel);
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("保存成功", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("保存失败,请稍候再试...", Page);
            }

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

            HandleModel.HandleContent = "策划报价-制作变更报价单,客户姓名:" + txtBrideName.Text.Trim().ToString() + "/" + txtGroomName.Text.Trim().ToString() + ",确认签约";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 3;     //策划报价
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 保存金额 至FL_QuotedPriceForType中
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveMoneyForType(int Type = 1)
        {
            var objTypeModel = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
            if (objTypeModel == null)
            {
                FL_QuotedPriceForType objTypeModels = new FL_QuotedPriceForType();
                objTypeModels.OrderID = Request["OrderID"].ToInt32();
                objTypeModels.QuotedID = Request["QuotedID"].ToInt32();
                objTypeModels.CustomerID = Request["CustomerID"].ToInt32();
                objTypeModels.MPrice = txtSalePrices.Text.Trim().ToDecimal();
                objTypeModels.PPrice = txtPersonPrice.Text.Trim().ToDecimal();
                objTypeModels.OPrice = txtOtherPrice.Text.Trim().ToDecimal();
                objTypeModels.Total = (objTypeModel.MPrice + objTypeModel.PPrice + objTypeModel.OPrice).ToString().ToDecimal();
                objTypeModels.IsFirstMake = FirstMake;
                objTypeModels.CreateEmployee = User.Identity.Name.ToInt32();
                objTypeModels.CreateDate = DateTime.Now;
                objTypeModels.IsLock = 1;
                ObjDiscountBLL.Insert(objTypeModels);
            }
            else
            {
                var Model = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());

                if (Model.IsChecks == true)
                {
                    var DiscountModel = ObjDiscountBLL.GetByOrderID(OrderID, 0);
                    txtRealAmount.Text = DiscountModel.Total.ToString();
                    txtSalePrices.Enabled = false;
                }
                else
                {
                    objTypeModel = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
                    objTypeModel.OrderID = Request["OrderID"].ToInt32();
                    objTypeModel.QuotedID = Request["QuotedID"].ToInt32();
                    objTypeModel.CustomerID = Request["CustomerID"].ToInt32();
                    objTypeModel.IsLock = 0;
                    objTypeModel.MPrice = txtSalePrices.Text.Trim().ToString().ToDecimal();
                    objTypeModel.PPrice = txtPersonPrice.Text.Trim().ToDecimal();
                    objTypeModel.OPrice = txtOtherPrice.Text.Trim().ToDecimal();
                    objTypeModel.Total = (objTypeModel.MPrice + objTypeModel.PPrice + objTypeModel.OPrice).ToString().ToDecimal();
                    objTypeModel.IsFirstMake = FirstMake;
                    objTypeModel.CreateEmployee = User.Identity.Name.ToInt32();
                    objTypeModel.CreateDate = DateTime.Now;
                    ObjDiscountBLL.Update(objTypeModel);

                    //修改策划报价的销售总价
                    Model.FinishState = 0;
                    Model.FinishAmount = objTypeModel.Total.ToString().ToDecimal();
                    ObjQuotedPriceBLL.Update(Model);
                }
            }

        }
        #endregion

    }
}