using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedPriceChange : SystemPage
    {
        QuotedSourceItem ObjItemBLL = new QuotedSourceItem();

        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();
        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();
        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 报价单类别业务逻辑
        /// </summary>
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        Dispatching ObjDispatchingBLL = new Dispatching();

        QuotedPriceItems ObjQuotedPriceItemBLL = new QuotedPriceItems();

        /// <summary>
        /// 打折折扣
        /// </summary>
        QuotedPriceForType ObjDiscountBLL = new QuotedPriceForType();


        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        StorehouseSourceProduct storehouseSourceProductBLL = new StorehouseSourceProduct();
        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int FirstMake = 1;
        bool Shows = false;

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            CreateButton();

            if (!IsPostBack)
            {
                if (Request["Type"] == "Look")
                {
                    FirstMake = 1;
                    ButtonVisible(false, true);
                    BinderData(true, 1);

                }
                else
                {
                    BinderData(false);
                }
            }

        }
        #endregion

        #region 数据绑定

        private void BinderData(bool IsShow, int IsFirstsMake = 0)
        {
            if (IsShow == false)        //不是查看历次变更单
            {
                Shows = false;
                bool IsNewInsert = false;       //是否制作新的变更单  默认为false
                var ObjList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).ToList(); ;      //外层绑定
                var MaxFirstMake = ObjList.Max(C => C.IsFirstMake);            //最大值  是多少 就是第几次做变更单  (首次制作 是0  第一次变更为1)
                foreach (var item in ObjList.Where(C => C.IsFirstMake == MaxFirstMake))
                {
                    if (item.IsFinishMake == 1)
                    {
                        IsNewInsert = true;     //要新作变更单
                    }
                }

                if (IsNewInsert == true)            //重新制作新的变更单
                {
                    this.repfirst.DataSource = ObjList.Where(C => C.IsFirstMake == MaxFirstMake + 1);
                    this.repfirst.DataBind();
                }
                else if (IsNewInsert == false)      //不是新做变更单  在原变更单的基础上进行修改
                {
                    this.repfirst.DataSource = ObjList.Where(C => C.IsFirstMake == MaxFirstMake);
                    this.repfirst.DataBind();
                }
            }
            else if (IsShow == true)        //查看变更单
            {
                Shows = true;
                var ObjList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).ToList(); ;      //外层绑定
                this.repfirst.DataSource = ObjList.Where(C => C.IsFirstMake == IsFirstsMake);       //IsFirstsMake是多少 就是第几次变更单
                this.repfirst.DataBind();
            }

            var ObjquotedModel = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, FirstMake).Where(C => C.ParentCategoryID == 0);
            decimal AggregateAmount = 0;
            foreach (var item in ObjquotedModel)
            {
                AggregateAmount += item.ItemAmount.ToString().ToDecimal();
            }
            var Model = ObjquotedModel.FirstOrDefault();
            if (Model != null)
            {
                lblChangeDate.Text = Model.ChangeDate == null ? "暂无" : Model.ChangeDate.ToString().ToDateTime().ToShortDateString();
                lblChangeEmployee.Text = Model.ChangeEmployee == null ? "暂无" : GetEmployeeName(Model.ChangeEmployee);
            }
            else
            {
                lblChangeDate.Text = "暂无";
                lblChangeEmployee.Text = GetEmployeeName(User.Identity.Name.ToInt32());
            }

            var DiscountModel = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
            if (DiscountModel != null)
            {
                if (DiscountModel.IsLock == 1)      //锁定状态
                {
                    btnIsLock.Text = "解锁";
                    txtDiscountPrice.Enabled = false;
                    btnIsLock.ToolTip = "目前加锁状态,不能修改销售价格";
                }
                else if (DiscountModel.IsLock == 0)         //解锁状态
                {
                    btnIsLock.Text = "加锁";
                    txtDiscountPrice.Enabled = true;
                    btnIsLock.ToolTip = "目前解锁状态,可以修改销售价格";
                }
            }
        }
        #endregion

        #region 绑定完成事件 ItemDataBound 绑定二级数据

        protected void repfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Button ObjSaveItem = (Button)e.Item.FindControl("btnSaveItem");
            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            var ObjItemList = new List<FL_QuotedPriceItems>();
            //获取二级项目
            var AllDataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList(); ;
            var MaxFirstMake = 0;
            if (Shows == true)
            {
                MaxFirstMake = FirstMake;
            }
            else if (Shows == false)
            {
                MaxFirstMake = GetMaxFirstMake();
            }
            var DataList = AllDataList.OrderByDescending(C => C.ParentCategoryID).Where(C => C.IsFirstMake == MaxFirstMake).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                var NewList = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }

            ////获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3).Where(C => C.IsFirstMake == MaxFirstMake).ToList();
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

            bool IsShows = true;        //默认显示保存分项
            foreach (var item in ObjItemList)
            {
                if (item.IsFinishMake == 1)
                {
                    IsShows = false; ;

                }
            }
            if (IsShows == false)       //已选择总调度的就不能再次保存
            {
                ObjSaveItem.Visible = false;
            }
            else                //还没选择总调度 可以看见保存分项
            {
                ObjSaveItem.Visible = true;
            }
            ObjRep.DataSource = ObjItemList;
            ObjRep.DataBind();


            //计算折扣价格及人员 其它价格
            var DataItemList = ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32());
            txtMaterialPrice.Text = DataItemList.Where(C => C.Type == 2 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2");

            txtPersonPrice.Text = DataItemList.Where(C => C.Type == 1 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2");
            txtDiscountPrice.Text = DataItemList.Where(C => C.Type == 2 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2");
            txtOtherPrice.Text = DataItemList.Where(C => C.Type == 3 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2");

            var Model = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
            if (Model != null)
            {
                if (Model.MPrice == null || Model.PPrice == null || Model.OPrice == null)
                {
                    Model.MPrice = txtDiscountPrice.Text.Trim().ToDecimal();
                    Model.PPrice = txtPersonPrice.Text.Trim().ToDecimal();
                    Model.OPrice = txtOtherPrice.Text.Trim().ToDecimal();
                    Model.Total = (Model.MPrice == null ? "0".ToDecimal() : Model.MPrice + Model.PPrice + Model.OPrice).ToString().ToDecimal();
                    ObjDiscountBLL.Update(Model);
                }

                //保存价格 方便读取金额
                BinderType();
            }
            else
            {
                //Type默认为1  就是已锁定  不能修改销售价格
                SaveMoneyForType();
                //保存价格 方便读取金额
                BinderType();
            }


        }
        #endregion

        #region 绑定事件ItemCommand 分项保存
        /// <summary>
        /// 绑定
        /// </summary>
        protected void repfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveItem")
            {
                SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());
                SaveallChange();
            }
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
                //    Model = new FL_QuotedPriceForType();
                //    Model.OrderID = Request["OrderID"].ToInt32();
                //    Model.QuotedID = Request["QuotedID"].ToInt32();
                //    Model.CustomerID = Request["CustomerID"].ToInt32();
                //    Model.IsFirstMake = FirstMake;
                //    Model.CreateEmployee = User.Identity.Name.ToInt32();
                //    Model.CreateDate = DateTime.Now;
                //    Model.IsLock = 1;       // 1.代表锁住 不能修改  2.代表解锁  可以修改
                //    ObjDiscountBLL.Insert(Model);
            }
            else
            {
                if (Model.IsLock == 0)      //解锁
                {
                    txtDiscountPrice.Text = Model.MPrice == null ? ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32()).Where(C => C.Type == 2 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2") : Model.MPrice.ToString().ToDecimal().ToString("f2");       //物料价格
                }
                else if (Model.IsLock == 1)    //加锁
                {
                    txtDiscountPrice.Text = ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32()).Where(C => C.Type == 2 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2");
                }
                //txtPersonPrice.Text = Model.PPrice == null || Model.PPrice.ToString() == "0" ? ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32()).Where(C => C.Type == 1 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2") : Model.PPrice.ToString().ToDecimal().ToString("f2");         //人员价格
                //txtOtherPrice.Text = Model.OPrice == null || Model.OPrice.ToString() == "0" ? ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32()).Where(C => C.Type == 3 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2") : Model.OPrice.ToString().ToDecimal().ToString("f2");          //其它价格
                txtRealAmount.Text = Model.Total.ToString().ToDecimal().ToString("f2");                                           //销售总价
                var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByOrderId(Request["OrderID"].ToInt32());
                ObjQuotedPriceModel.FinishAmount = (txtDiscountPrice.Text.ToDecimal() + txtPersonPrice.Text.ToDecimal() + txtOtherPrice.Text.ToDecimal()).ToString("f2").ToDecimal();
                ObjQuotedPriceModel.RealAmount = (txtDiscountPrice.Text.ToDecimal() + txtPersonPrice.Text.ToDecimal() + txtOtherPrice.Text.ToDecimal()).ToString("f2").ToDecimal();
                ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
            }
        }
        #endregion

        #region 保存各级大类

        #region 一级大类
        /// <summary>
        /// 生成且保存一级大类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStarFirstpg_Click(object sender, EventArgs e)
        {

            bool IsFinish = false;      //默认未派工
            QuotedID = Request["QuotedID"].ToInt32();
            int IsFirstMake = 0;
            var Model = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Where(C => C.IsFinishMake >= 1).ToList();
            if (Model.Count > 0)        //第一次跟单后的 加一级大类
            {
                IsFirstMake = Model.Max(C => C.IsFirstMake).ToString().ToInt32();
                foreach (var item in Model)
                {
                    if (item.IsFinishMake == 1)     //已完成
                    {
                        IsFinish = true;
                    }
                }
                if (IsFinish == true)       //第二次跟单 或多次跟单了   如果为false 是同一次增加 一级大类  
                {
                    //IsFirstMake += 1;
                    IsFirstMake = IsFirstMake.ToString().ToInt32() + 1;
                }
            }
            else        //第一次跟单
            {
                IsFirstMake = 1;
            }

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
                    //if (ObjExistModel == null || ObjExistModel.IsFirstMake != 1)
                    //{
                    ObjCategoryForQuotedPriceModel = new FL_QuotedPriceItems();
                    ObjCategoryForQuotedPriceModel.CategoryID = ObjCategorItem.QCKey + IsFirstMake * 10000;
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
                    ObjCategoryForQuotedPriceModel.IsFirstMake = IsFirstMake;
                    ObjCategoryForQuotedPriceModel.IsFinishMake = 0;
                    ObjCategoryForQuotedPriceModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                    ObjCategoryForQuotedPriceModel.ChangeEmployee = User.Identity.Name.ToInt32();

                    ObjQuotedPriceItemsBLL.Insert(ObjCategoryForQuotedPriceModel);
                    //}
                    //else
                    //{
                    //    ObjExistModel.IsDelete = false;
                    //    ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    //}
                }
                SaveallChange();
                BinderData(false);
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
            bool IsFinish = false;      //默认未派工
            QuotedID = Request["QuotedID"].ToInt32();
            int IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32();
            var DataList = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, IsFirstMake);
            if (DataList.Count > 0)         //已经存在  IsFirstMake 不变  在原有基础上 项目累加
            {
                foreach (var item in DataList)
                {
                    if (item.IsFinishMake == 1)        //1 已派工  说明是第三次下单 第二次跟单以上
                    {
                        IsFinish = true;
                    }
                }
                if (IsFinish == true)       //已完成派工
                {
                    IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32() + 1;
                }
                else if (IsFinish == false)         //未派工  项目 产品都会继续累加
                {

                }
            }
            else                //不存在  第一次开始  IsFirstMake 要加 +1
            {
                IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32() + 1;
            }

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
                    //if (ObjExistModel == null || ObjExistModel.IsFirstMake != 1)
                    //{
                    ObjModel = new FL_QuotedPriceItems();
                    ObjModel.ItemLevel = 2;
                    ObjModel.OrderID = OrderID;
                    ObjModel.CategoryName = Objitem.Title;
                    ObjModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.Parent).Title;
                    ObjModel.CategoryID = Objitem.QCKey;
                    ObjModel.ParentCategoryID = Objitem.Parent + IsFirstMake * 10000;
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
                    ObjModel.IsFirstMake = IsFirstMake;
                    ObjModel.IsFinishMake = 0;
                    ObjModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                    ObjModel.ChangeEmployee = User.Identity.Name.ToInt32();

                    ObjQuotedPriceItemsBLL.Insert(ObjModel);

                    CreateProduct(Objitem.QCKey);
                    //}
                    //else
                    //{
                    //    ObjExistModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(Objitem.Parent).Title;
                    //    ObjExistModel.IsDelete = false;
                    //    ObjQuotedPriceItemsBLL.Update(ObjExistModel);
                    //}
                }

                SaveallChange();
                BinderData(false);
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
            bool IsFinish = false;      //默认未派工
            QuotedID = Request["QuotedID"].ToInt32();
            int IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32();
            var DataList = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, IsFirstMake);
            if (DataList.Count > 0)         //已经存在  IsFirstMake 不变  在原有基础上 项目累加
            {
                foreach (var item in DataList)
                {
                    if (item.IsFinishMake == 1)        //1 已派工  说明是第三次下单 第二次跟单以上
                    {
                        IsFinish = true;
                    }
                }
                if (IsFinish == true)       //已完成派工
                {
                    IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32() + 1;
                }
                else if (IsFinish == false)         //未派工  项目 产品都会继续累加
                {

                }
            }
            else                //不存在  第一次开始  IsFirstMake 要加 +1
            {
                IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32() + 1;
            }

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
                ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(QcKey).Parent + IsFirstMake * 10000;
                ObjSetModel.ProductID = ObjProductModel.Keys;
                ObjSetModel.Type = ObjQuotedCatgoryBLL.GetByID(QcKey).Type;
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
                ObjSetModel.PartyDay = ObjCustomersBLL.GetByCustomerID(CustomerID).Partydate;
                ObjSetModel.TimerSpan = ObjCustomersBLL.GetByCustomerID(CustomerID).TimeSpans;
                ObjSetModel.OrderID = OrderID;
                ObjSetModel.ServiceContent = ObjProductModel.ProductName;
                ObjSetModel.IsFirstMake = IsFirstMake;
                ObjSetModel.IsFinishMake = 0;
                ObjSetModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                ObjSetModel.ChangeEmployee = User.Identity.Name.ToInt32();

                ObjQuotedPriceItemsBLL.Insert(ObjSetModel);
            }

        }
        #endregion

        #region 三级产品
        /// <summary>
        /// 生成并保存三级产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateThired_Click(object sender, EventArgs e)
        {
            bool IsFinish = false;      //默认未派工
            QuotedID = Request["QuotedID"].ToInt32();
            int IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32();
            var DataList = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, IsFirstMake);
            if (DataList.Count > 0)         //已经存在  IsFirstMake 不变  在原有基础上 项目累加
            {
                foreach (var item in DataList)
                {
                    if (item.IsFinishMake == 1)        //1 已派工  说明是第三次下单 第二次跟单以上
                    {
                        IsFinish = true;
                    }
                }
                if (IsFinish == true)       //已完成派工
                {
                    IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32() + 1;
                }
                else if (IsFinish == false)         //未派工  项目 产品都会继续累加
                {

                }
            }
            else                //不存在  第一次开始  IsFirstMake 要加 +1
            {
                IsFirstMake = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID).Max(C => C.IsFirstMake).ToString().ToInt32() + 1;
            }

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
                    //根据父亲级别查询三类 有 但是肯定只有一个
                    //var ObjExistModel = ObjQuotedPriceItemsBLL.GetOnlyByProductID(QuotedID, ObjProduct.Keys, 3);
                    //if (ObjExistModel == null)
                    //{
                    ObjSetModel = new FL_QuotedPriceItems();
                    ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Title;
                    ObjSetModel.CategoryID = hideThirdCategoryID.Value.ToInt32();
                    ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent).Title;
                    ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Parent + IsFirstMake * 10000;
                    ObjSetModel.ProductID = ObjProduct.Keys;
                    ObjSetModel.Type = ObjQuotedCatgoryBLL.GetByID(hideThirdCategoryID.Value.ToInt32()).Type;
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
                    ObjSetModel.PartyDay = ObjCustomersBLL.GetByCustomerID(CustomerID).Partydate;
                    ObjSetModel.TimerSpan = ObjCustomersBLL.GetByCustomerID(CustomerID).TimeSpans;
                    ObjSetModel.OrderID = OrderID;
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjSetModel.IsFirstMake = IsFirstMake;
                    ObjSetModel.IsFinishMake = 0;
                    ObjSetModel.ChangeDate = DateTime.Now.ToString().ToDateTime();
                    ObjSetModel.ChangeEmployee = User.Identity.Name.ToInt32();

                    ObjQuotedPriceItemsBLL.Insert(ObjSetModel);


                    SalePrice += ObjSetModel.UnitPrice.Value * ObjSetModel.Quantity.Value;
                }
            }
            SaveallChange();
            BinderData(false);

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
            var ObjChangeSecondList = ObjQuotedPriceItemsBLL.GetByQuotedID(QuotedID, 2); ;

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
        }
        #endregion

        #region 保存报价单
        /// <summary>
        /// 保存报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            SaveallChange();
            JavaScriptTools.AlertWindow("保存成功!", Page);
        }
        #endregion

        #region 分项保存方法
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

            var DataList = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, GetMaxFirstMake());
            foreach (var item in DataList)
            {
                item.ChangeEmployee = User.Identity.Name.ToInt32();
                item.ChangeDate = DateTime.Now.ToString().ToDateTime();
                ObjQuotedPriceItemsBLL.Update(item);
            }

            BinderData(false);
        }
        #endregion

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
        //public string GetRowKey(object Key)
        //{
        //    if (Key != null)
        //    {
        //        return Key.ToString();
        //    }
        //    else
        //    { 

        //    }
        //}


        /// <summary>
        /// 隐藏选择项目
        /// </summary>
        /// <returns></returns>
        public string HideSelectItem(object Level, object IsFinishMake)
        {
            if (Level != null)
            {
                if (Level.ToString() == "0")
                {
                    if (IsFinishMake != null)
                    {
                        if (IsFinishMake.ToString() == "1")
                        {
                            return "style='display:none;'";
                        }
                        else if (IsFinishMake.ToString() == "0")
                        {
                            return string.Empty;
                        }
                    }

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
            return string.Empty;
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

        #region 绑定时间 二级绑定 ItemCommand删除
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var ObjItem = ObjQuotedPriceItemsBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                if (ObjItem != null)
                {
                    ObjQuotedPriceItemsBLL.Delete(ObjItem);
                }
                SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());
            }
            BinderData(false);
        }
        #endregion

        #region 派工任务方法
        /// <summary>
        /// 开始创建派工任务
        /// </summary>
        private void CreateDispatching(int DisID)
        {
            var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DisID);
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

            FL_ProductforDispatching ObjCategoryforDispatching = new FL_ProductforDispatching();
            //添加派工大类

            //一级类别
            var ObjChangeFirstList = ObjQuotedPriceItemBLL.GetByQuotedID(QuotedID, 1);
            int IsFirstMake = ObjChangeFirstList.Max(C => C.IsFirstMake).ToString().ToInt32();
            var objFirstModelList = ObjChangeFirstList.Where(C => C.IsFirstMake == IsFirstMake).ToList();

            //二级类别
            var ObjChangeSecondList = ObjQuotedPriceItemBLL.GetByQuotedID(QuotedID, 2);
            var objSecondModelList = ObjChangeSecondList.Where(C => C.IsFirstMake == IsFirstMake).ToList();

            //三级产品类别
            var ObjProductList = ObjQuotedPriceItemBLL.GetByQuotedID(QuotedID, 3);
            var objThirdModelList = ObjProductList.Where(C => C.IsFirstMake == IsFirstMake).ToList();

            int EmployeeID = ObjProductforDispatchingBLL.GetEmployeeIDByOrderID(OrderID);

            foreach (var ObjFirstCategory in objFirstModelList)
            {

                ObjFirstCategory.IsFinishMake = 1;
                ObjQuotedPriceItemBLL.Update(ObjFirstCategory);


                ObjCategoryforDispatching = new FL_ProductforDispatching();
                ObjCategoryforDispatching.DispatchingID = ObjDispatchingModel.DispatchingID;
                ObjCategoryforDispatching.Unit = ObjFirstCategory.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjFirstCategory.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjFirstCategory.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjFirstCategory.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjFirstCategory.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjFirstCategory.Quantity.ToString().ToInt32();
                ObjCategoryforDispatching.Subtotal = ObjFirstCategory.Subtotal;
                ObjCategoryforDispatching.Remark = ObjFirstCategory.Remark;
                ObjCategoryforDispatching.ParentDispatchingID = ObjDispatchingModel.ParentDispatchingID;
                ObjCategoryforDispatching.CategoryName = ObjFirstCategory.CategoryName;
                ObjCategoryforDispatching.ParentCategoryID = ObjFirstCategory.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjFirstCategory.CategoryName;
                ObjCategoryforDispatching.PurchasePrice = ObjFirstCategory.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjFirstCategory.Productproperty;
                ObjCategoryforDispatching.RowType = ObjFirstCategory.RowType;
                ObjCategoryforDispatching.CreateEmployee = 0;
                ObjCategoryforDispatching.CategoryID = ObjFirstCategory.CategoryID;
                ObjCategoryforDispatching.ItemLevel = 1;
                ObjCategoryforDispatching.SupplierName = ObjFirstCategory.SupplierName;
                ObjCategoryforDispatching.SupplierID = 0;
                ObjCategoryforDispatching.EmployeeID = EmployeeID;
                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCategoryforDispatching.CustomerID = Request["CustomerID"].ToInt32();
                ObjCategoryforDispatching.IsFirstMakes = ObjFirstCategory.IsFirstMake;
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);


            }

            //添加二级产品 类别
            foreach (var ObjSecondItem in objSecondModelList)
            {
                ObjSecondItem.IsFinishMake = 1;
                ObjQuotedPriceItemBLL.Update(ObjSecondItem);


                ObjCategoryforDispatching = new FL_ProductforDispatching();
                ObjCategoryforDispatching.DispatchingID = ObjDispatchingModel.DispatchingID;
                ObjCategoryforDispatching.Unit = ObjSecondItem.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjSecondItem.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjSecondItem.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjSecondItem.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjSecondItem.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjSecondItem.Quantity.ToString().ToInt32();
                ObjCategoryforDispatching.Subtotal = ObjSecondItem.Subtotal;
                ObjCategoryforDispatching.Remark = ObjSecondItem.Remark;
                ObjCategoryforDispatching.ParentDispatchingID = ObjDispatchingModel.ParentDispatchingID;
                ObjCategoryforDispatching.CategoryName = ObjSecondItem.CategoryName;
                ObjCategoryforDispatching.CategoryID = ObjSecondItem.CategoryID;
                ObjCategoryforDispatching.ProductID = ObjSecondItem.ProductID;
                ObjCategoryforDispatching.ParentCategoryID = ObjSecondItem.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjCategoryforDispatching.ParentCategoryID - IsFirstMake * 10000).Title;
                ObjCategoryforDispatching.ItemLevel = 2;
                ObjCategoryforDispatching.PurchasePrice = ObjSecondItem.PurchasePrice;
                ObjCategoryforDispatching.CreateEmployee = 0;
                ObjCategoryforDispatching.Productproperty = ObjSecondItem.Productproperty;
                ObjCategoryforDispatching.EmployeeID = EmployeeID;
                ObjCategoryforDispatching.RowType = ObjSecondItem.RowType;
                ObjCategoryforDispatching.OrderID = OrderID;

                ObjCategoryforDispatching.SupplierName = ObjSecondItem.SupplierName;
                ObjCategoryforDispatching.SupplierID = 0;
                ObjCategoryforDispatching.CustomerID = Request["CustomerID"].ToInt32();
                ObjCategoryforDispatching.IsFirstMakes = ObjSecondItem.IsFirstMake;
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);



            }

            //添加三级产品类别
            foreach (var ObjThiredItem in objThirdModelList)
            {
                ObjThiredItem.IsFinishMake = 1;
                ObjQuotedPriceItemBLL.Update(ObjThiredItem);

                ObjCategoryforDispatching = new FL_ProductforDispatching();
                ObjCategoryforDispatching.DispatchingID = ObjDispatchingModel.DispatchingID;
                ObjCategoryforDispatching.Unit = ObjThiredItem.Unit;
                ObjCategoryforDispatching.ServiceContent = ObjThiredItem.ServiceContent;
                ObjCategoryforDispatching.Requirement = ObjThiredItem.Requirement;
                ObjCategoryforDispatching.ImageUrl = ObjThiredItem.ImageUrl;
                ObjCategoryforDispatching.UnitPrice = ObjThiredItem.UnitPrice;
                ObjCategoryforDispatching.Quantity = ObjThiredItem.Quantity.ToString().ToInt32();
                ObjCategoryforDispatching.Subtotal = ObjThiredItem.PurchasePrice * ObjThiredItem.Quantity;
                ObjCategoryforDispatching.Remark = ObjThiredItem.Remark;
                ObjCategoryforDispatching.ParentDispatchingID = ObjDispatchingModel.ParentDispatchingID;
                ObjCategoryforDispatching.CategoryName = ObjThiredItem.CategoryName;
                ObjCategoryforDispatching.ProductID = ObjThiredItem.ProductID;
                ObjCategoryforDispatching.CategoryID = ObjThiredItem.CategoryID;
                ObjCategoryforDispatching.ParentCategoryID = ObjThiredItem.ParentCategoryID;
                ObjCategoryforDispatching.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjCategoryforDispatching.ParentCategoryID - IsFirstMake * 10000).Title;
                ObjCategoryforDispatching.ItemLevel = 3;
                ObjCategoryforDispatching.CreateEmployee = 0;
                ObjCategoryforDispatching.PurchasePrice = ObjThiredItem.PurchasePrice;
                ObjCategoryforDispatching.Productproperty = ObjThiredItem.Productproperty;
                ObjCategoryforDispatching.RowType = ObjThiredItem.RowType;
                ObjCategoryforDispatching.EmployeeID = EmployeeID;
                ObjCategoryforDispatching.SupplierName = ObjThiredItem.SupplierName;
                ObjCategoryforDispatching.Classification = ObjThiredItem.Classification;
                ObjCategoryforDispatching.OrderID = OrderID;
                ObjCategoryforDispatching.CustomerID = Request["CustomerID"].ToInt32();
                ObjCategoryforDispatching.IsFirstMakes = ObjThiredItem.IsFirstMake;
                ObjCategoryforDispatching.SupplierID = 0;

                //生成三级
                ObjProductforDispatchingBLL.Insert(ObjCategoryforDispatching);


            }

        }
        #endregion

        #region 点击确认  派工
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int CustomerID = Request["CustomerID"].ToInt32();
            var DisModel = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            var ObjQuotedList = ObjQuotedPriceItemsBLL.GetByQuotedsID(QuotedID, FirstMake).Where(C => C.ItemLevel == 3).ToList();
            if (ObjQuotedList.Count > 0)
            {
                if (DisModel != null)
                {
                    int DispacthingID = DisModel.DispatchingID;
                    //保存价格 方便读取金额
                    SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());
                    SaveallChange();
                    CreateDispatching(DispacthingID);    //创建派工任务

                    JavaScriptTools.AlertAndClosefancybox("总派工任务已经下达!", Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("请选择项目", Page);
            }
        }
        #endregion

        #region 点击保存 全局保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SaveMoneyForType(ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake).IsLock.ToString().ToInt32());
            SaveallChange();
        }
        #endregion

        #region 获取最大的IsFirstMake
        /// <summary>
        /// 随时获取最大的IsFirstMake
        /// </summary>
        /// <returns></returns>
        public int GetMaxFirstMake()
        {
            var ObjList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).ToList(); ;      //外层绑定
            var MaxFirstMake = ObjList.Max(C => C.IsFirstMake);            //最大值  是多少 就是第几次做变更单  (首次制作 是0  第一次变更为1)
            return MaxFirstMake.ToString().ToInt32();
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
            if (Request["Type"] == "Look")
            {

            }
            else
            {
                btnCreate.Text = "本次变更单";
                btnCreate.CssClass = "btn btn-success";

                this.div_Button.Controls.Add(btnCreate);
                btnCreate.Click += button_Click;

                FirstMake = MaxIsFirstMake + 1;
            }
            for (int i = 0; i < MaxIsFirstMake; i++)
            {
                btnCreate = new Button();
                btnCreate.Text = "第" + (i + 1) + "次变更单";
                btnCreate.CssClass = "btn btn-success";

                this.div_Button.Controls.Add(btnCreate);
                btnCreate.Click += button_Click;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            if (button != null)
            {
                string text = button.Text.ToString();
                if (text == "本次变更单")
                {
                    BinderData(false);
                    ButtonVisible(true, false);
                }
                else
                {
                    int index = text.Substring(1, 1).ToInt32();
                    FirstMake = index;
                    BinderData(true, FirstMake);
                    HideIndx.Value = FirstMake.ToString();
                    ButtonVisible(false, true);
                }
            }
            else
            {

                FirstMake = 1;
                BinderData(true, FirstMake);
                HideIndx.Value = FirstMake.ToString();
                ButtonVisible(false, true);
            }

        }

        #region 按钮的隐藏显示
        public void ButtonVisible(bool IsVisible, bool NoVisible)
        {
            btnCancel.Visible = IsVisible;
            btnConfirm.Visible = IsVisible;
            SelectPG.Visible = IsVisible;
            btnPrint.Visible = NoVisible;
        }
        #endregion

        #endregion

        #region 获取CategoryId
        public int GetCategoryId(object Source)
        {
            if (Source != null)
            {
                int ChangeId = Source.ToString().ToInt32();
                var Model = ObjQuotedPriceItemsBLL.GetByID(ChangeId);
                if (Model != null)
                {
                    if (Model.ParentCategoryID == 0)
                    {
                        string CategoryId = (Model.CategoryID.ToString().ToDecimal() - (GetMaxFirstMake() * 10000).ToString().ToDecimal()).ToString();
                        return CategoryId.ToInt32();
                    }
                    else
                    {
                        string CategoryId = (Model.ParentCategoryID.ToString().ToDecimal() - (GetMaxFirstMake() * 10000).ToString().ToDecimal()).ToString();
                        return CategoryId.ToInt32();
                    }
                }
            }
            return 0;
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
                objTypeModels.IsFirstMake = FirstMake;
                objTypeModels.CreateEmployee = User.Identity.Name.ToInt32();
                objTypeModels.CreateDate = DateTime.Now;
                objTypeModels.IsLock = 1;
                ObjDiscountBLL.Insert(objTypeModels);
            }
            else
            {
                objTypeModel = ObjDiscountBLL.GetByOrderID(Request["OrderID"].ToInt32(), FirstMake);
                objTypeModel.OrderID = Request["OrderID"].ToInt32();
                objTypeModel.QuotedID = Request["QuotedID"].ToInt32();
                objTypeModel.CustomerID = Request["CustomerID"].ToInt32();

                if (Type == 1)          //加锁
                {
                    objTypeModel.IsLock = 1;
                    objTypeModel.MPrice = ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32()).Where(C => C.Type == 2 && C.IsFirstMake == FirstMake).Sum(C => C.Subtotal).ToString().ToDecimal();
                }
                else if (Type == 0)     //解锁
                {
                    objTypeModel.IsLock = 0;
                    objTypeModel.MPrice = txtDiscountPrice.Text.Trim().ToString().ToDecimal();
                }

                objTypeModel.PPrice = txtPersonPrice.Text.Trim().ToDecimal();
                objTypeModel.OPrice = txtOtherPrice.Text.Trim().ToDecimal();
                objTypeModel.Total = (objTypeModel.MPrice + objTypeModel.PPrice + objTypeModel.OPrice).ToString().ToDecimal();
                objTypeModel.IsFirstMake = FirstMake;
                objTypeModel.CreateEmployee = User.Identity.Name.ToInt32();
                objTypeModel.CreateDate = DateTime.Now;

                ObjDiscountBLL.Update(objTypeModel);
            }

        }
        #endregion


        public string GetIsFirstMake()
        {
            return FirstMake.ToString();
        }

        #region 点击 加锁/解锁按钮事件
        /// <summary>
        /// 点击事件(加锁/解锁)
        /// </summary> 
        protected void btnIsLock_Click(object sender, EventArgs e)
        {
            if (btnIsLock.Text == "加锁")
            {
                btnIsLock.Text = "解锁";
                txtDiscountPrice.Enabled = false;
                SaveMoneyForType(1);
            }
            else if (btnIsLock.Text == "解锁")
            {
                btnIsLock.Text = "加锁";
                txtDiscountPrice.Enabled = true;
                SaveMoneyForType(0);
            }
            BinderData(true, FirstMake);
        }
        #endregion

    }
}