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
using HA.PMS.EditoerLibrary;
using System.Web.UI.HtmlControls;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class TaskWork : SystemPage
    {
        Statement ObjStatementBLL = new Statement();

        Supplier ObjSupplierBLL = new Supplier();
        SupplierType ObjSuplierTypeBLL = new SupplierType();

        //财务成本明细
        OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();

        CostforOrder ObjCostForOrderBLL = new CostforOrder();
        /// <summary> 
        /// 调度所有产品
        /// </summary>
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


        /// <summary>
        /// 项目
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemBLL = new QuotedPriceItems();

        CostSum ObjCostSumBLL = new CostSum();

        Repeater RptData = null;

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

        FourGuardian ObjFourGuardianBLL = new FourGuardian();


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
        int QuotedID = 0;
        int CustomerID = 0;
        string ServiceContent = "";

        string OrderColumnName = "CostSumId";
        int PageSize = 100;
        int PageIndex = 1;
        int SourceCount = 0;

        string WorkType = string.Empty;
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

        #region 页面加载
        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            QuotedID = Request["QuotedId"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            WorkType = Request["WorkType"];
            switch (WorkType)
            {
                case "花艺":
                    btn_Flower.Visible = true;
                    btn_Caigou.Visible = false;
                    break;
                case "道具":
                    btn_Flower.Visible = false;
                    btn_Caigou.Visible = true;
                    paretntSelect.Visible = true;
                    break;
                case "灯光":
                    btn_Flower.Visible = false;
                    btn_Caigou.Visible = false;
                    break;
                case "人员":
                    btn_Flower.Visible = false;
                    btn_Caigou.Visible = false;
                    break;
            }
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

                var Model = ObjQuotedPriceBLL.GetByOrderId(Request["OrderID"].ToInt32());
                if (Model != null)
                {
                    if (Model.Director != null || Model.Director.ToString() != "")
                    {
                        txtEmpLoyee.Value = GetEmployeeName(Model.Director);
                    }
                }

                BinderData();
                BindDatasList();
            }
        }
        #endregion

        #region 获取图片路径
        /// <summary>
        /// 获取图片
        /// </summary>
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
        #endregion

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

        #region 隐藏项目
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
                if (Level.ToString() == "1")
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
        #endregion

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

        #region 加载绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            int CusetomerID = Request["CustomerID"].ToInt32();

            var objCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            if (objCustomerModel.State == 206)
            {
                btnSaveItem.Visible = false;
            }


            //有类别
            int MineState = 0;
            List<FL_ProductforDispatching> objProductList = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 1, out MineState).Where(C => C.Classification != null).ToList().Where(C => C.Classification.Contains(WorkType)).ToList();
            foreach (var item in objProductList)
            {
                item.Subtotal = item.PurchasePrice * item.Quantity;
                item.CustomerID = CusetomerID;
                ObjProductforDispatchingBLL.Update(item);
            }

            this.repfirst.DataSource = objProductList;
            this.repfirst.DataBind();
            var ObjDisPatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
            ObjDisPatchingModel.CustomerID = CusetomerID;
            ObjDispatchingBLL.Update(ObjDisPatchingModel);
            //判断是否为总派工人

            if (ObjDisPatchingModel.EmployeeID == User.Identity.Name.ToInt32() || ObjDisPatchingModel.DesignEmployee == User.Identity.Name.ToInt32())
            {
                hideMineState.Value = "1";
                hideFirstEmpLoyeeID.Visible = true;
            }
            else
            {
                hideFirstEmpLoyeeID.Visible = false;
            }

        }
        #endregion

        #region 根据类型获取员工姓名
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
        #endregion

        #region 二级数据绑定
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
            //获取三级产品
            DataList.AddRange(ObjProductforDispatchingBLL.GetByParentCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32()).Where(C => C.IsGet == true && C.Classification.Contains(WorkType)).OrderBy(C => C.CategoryID).ToList());

            Label lblSumMoney = e.Item.FindControl("lblSumMoney") as Label;
            Label lblSumQuantity = e.Item.FindControl("lblSumQuantity") as Label;

            lblSumMoney.Text = (lblSumMoney.Text.ToDecimal() + DataList.Where(C => C.Subtotal != null).ToList().Sum(C => C.Subtotal.Value)).ToString();
            lblSumQuantity.Text = (DataList.Sum(C => C.Quantity).ToString().ToInt32() - 1).ToString();


            ObjRep.DataSource = DataList;
            ObjRep.DataBind();
        }
        #endregion

        /// <summary>
        /// 生成且保存一级大类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStarFirstpg_Click(object sender, EventArgs e)
        {

        }


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
                        CreateProduct(Objitem.QCKey);
                    }
                    else
                    {

                        ObjExistModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjExistModel.TaskEmpLoyee = User.Identity.Name.ToInt32();
                        //ObjExistModel.ParentCategoryName = OjbCategoryBLL.GetByID(Objitem.ParentID).CategoryName;
                        ObjExistModel.IsDelete = false;
                        ObjProductforDispatchingBLL.Update(ObjExistModel);
                        CreateProduct(Objitem.QCKey);
                    }
                }


            }

            BinderData();
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
                FL_ProductforDispatching ObjSetModel = new FL_ProductforDispatching();

                ObjSetModel.CategoryName = ObjQuotedCatgoryBLL.GetByID(QcKey).Title;
                ObjSetModel.CategoryID = QcKey;
                ObjSetModel.ParentCategoryName = ObjQuotedCatgoryBLL.GetByID(ObjQuotedCatgoryBLL.GetByID(QcKey).Parent).Title;
                ObjSetModel.ParentCategoryID = ObjQuotedCatgoryBLL.GetByID(QcKey).Parent;
                ObjSetModel.ProductID = ObjProductModel.Keys;
                ObjSetModel.IsDelete = false;
                ObjSetModel.IsSvae = false;
                ObjSetModel.ItemLevel = 3;
                ObjSetModel.IsGet = true;
                ObjSetModel.UnitPrice = ObjProductModel.SalePrice;
                ObjSetModel.Specifications = ObjProductModel.Specifications;
                ObjSetModel.Unit = ObjProductModel.Unit;
                ObjSetModel.RowType = ObjProductModel.Type;
                if (ObjSetModel.RowType == 3)
                {
                    ObjSetModel.Quantity = ObjProductModel.Count.Value;
                    ObjSetModel.Subtotal = ObjSetModel.Quantity * ObjProductModel.PurchasePrice;
                }
                else
                {
                    ObjSetModel.Quantity = 1;
                    ObjSetModel.Subtotal = ObjSetModel.Quantity * ObjProductModel.PurchasePrice;
                }
                ObjSetModel.NewAdd = true;
                ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();

                ObjSetModel.TaskEmpLoyee = User.Identity.Name.ToInt32();
                ObjSetModel.DispatchingID = DispatchingID;
                ObjSetModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjSetModel.Productproperty = ObjProductModel.Productproperty;
                ObjSetModel.Requirement = string.Empty;
                ObjSetModel.Remark = ObjProductModel.Remark;
                ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();
                ObjSetModel.ServiceContent = ObjProductModel.ProductName;
                ObjSetModel.PurchasePrice = ObjProductModel.PurchasePrice;
                ObjSetModel.SupplierName = ObjProductModel.SupplierName;
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

                ObjSetModel.Classification = WorkType;
                ObjSetModel.OrderID = OrderID;
                ObjSetModel.CustomerID = CustomerID;
                ObjProductforDispatchingBLL.Insert(ObjSetModel);

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
            //财务成本主体
            Cost ObjCostBLL = new Cost();

            var CGKey = this.hideThirdValue.Value.Split(',');
            int ThridId = hideThirdCategoryID.Value.ToInt32();
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
                    ObjSetModel.IsGet = true;
                    ObjSetModel.Classification = WorkType;

                    ObjSetModel.ItemLevel = 3;
                    ObjSetModel.UnitPrice = ObjProduct.SalePrice;

                    ObjSetModel.PurchasePrice = ObjProduct.PurchasePrice;
                    ObjSetModel.Quantity = 1;
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
                    //ObjSetModel.Requirement = string.Empty;
                    ObjSetModel.Requirement = ObjProduct.Specifications;
                    ObjSetModel.Remark = ObjProduct.Remark;
                    ObjSetModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjSetModel.ServiceContent = ObjProduct.ProductName;
                    ObjSetModel.PurchasePrice = ObjProduct.PurchasePrice;
                    ObjSetModel.SupplierName = ObjProduct.SupplierName;
                    //ObjSetModel.ParentDispatchingID = ObjDispatchingBLL.GetByID(DispatchingID).ParentDispatchingID;
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

                    #endregion
                }
            }

            //SaveQuotedPrice();
            BinderData();

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
        }
        #endregion

        #region 保存报价单 调用方法
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
                    ServiceContent = ObjItem.ServiceContent;
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
                    ObjItem.PurchasePrice = ((TextBox)ObjrepList.Items[I].FindControl("txtPurchasePrice")).Text.ToDecimal();
                    ObjItem.UnitPrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = (ObjItem.Quantity * ObjItem.PurchasePrice).ToString().ToDecimal();
                    //ObjItem.Subtotal = ((TextBox)ObjrepList.Items[I].FindControl("txtSubtotal")).Text.ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    ObjItem.PlannedCompletionTime = DateTime.Now;
                    ObjItem.FinishDate = DateTime.Now;
                    ObjItem.SupplierName = ((TextBox)ObjrepList.Items[I].FindControl("txtSuppName")).Text.Trim().ToString();

                    ObjProductforDispatchingBLL.Update(ObjItem);
                    if (ObjItem.RowType == null)
                    {
                        ObjItem.RowType = 1;
                    }
                    if (ObjHideCatgoryEmpLoyeeID != null)
                    {
                        if (ObjHideCatgoryEmpLoyeeID.Value.ToInt32() > 0)
                        {
                            var ItemUpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjItem.CategoryID, 2);
                            ItemUpdateModel.EmployeeID = ObjHideCatgoryEmpLoyeeID.Value.ToInt32();
                            ObjProductforDispatchingBLL.Update(ItemUpdateModel);
                        }
                    }

                    var CostModel = ObjOrderfinalCostBLL.GetByPremissionnalName(ObjItem.ServiceContent, DispatchingID);
                    if (CostModel != null)
                    {
                        CostModel.PlannedExpenditure = ObjItem.PurchasePrice.Value;
                        ObjOrderfinalCostBLL.Update(CostModel);
                    }
                }
            }
            BinderData();
            BindDatasList();
            for (int i = 1; i <= 5; i++)
            {
                SetRowType(i);
            }
        }
        #endregion

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
                    //            ObjItem.SupplierName = ((TextBox)ObjrepList.Items[I].FindControl("txtSuppName")).Text.Trim().ToString();
                    //            //ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;
                    //            ObjItem.RowType = 2;
                    //        }
                    //        //ObjItem.Productproperty = 1;

                    //    }
                    //    else
                    //    {
                    //if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房")
                    //{
                    //    ObjItem.SupplierName = ((TextBox)ObjrepList.Items[I].FindControl("txtSuppName")).Text.Trim().ToString();

                    //    ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                    //}
                    ////        else
                    ////        {
                    //if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value == "库房")
                    //{
                    //    ObjItem.RowType = 2;
                    //    ObjItem.SupplierID = 0;
                    //    ObjItem.SupplierName = "库房";
                    //}

                    //if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房" && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "供应商")
                    //{
                    //    //ObjItem.RowType = 2;
                    //    //ObjItem.SupplierID = 0;
                    //    //ObjItem.SupplierName = "库房";
                    //}

                    //        }

                    //    }
                    //}
                    //else
                    //{
                    //    if (((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != string.Empty && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != null && ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value != "库房")
                    //    {
                    //        ObjItem.SupplierName = ((TextBox)ObjrepList.Items[I].FindControl("txtSuppName")).Text.Trim().ToString();

                    //        ObjItem.SupplierID = ObjSupplierBLL.GetByName(ObjItem.SupplierName).SupplierID;//供应商 ID
                    //    }
                    //    else
                    //    {
                    //        ObjItem.SupplierID = 0;
                    //        ObjItem.SupplierName = ((TextBox)ObjrepList.Items[I].FindControl("txtSuppName")).Text.Trim().ToString();
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
                    //ObjItem.SupplierName = ((TextBox)ObjrepList.Items[I].FindControl("txtSuppName")).Text.Trim().ToString();
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
            BindDatasList();

        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SaveaPageChange();
                var ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)e.Item.FindControl("hidePriceKey")).Value.ToInt32());
                if (ObjItem != null)
                {
                    ObjProductforDispatchingBLL.Delete(ObjItem);
                }
                for (int i = 1; i <= 5; i++)
                {
                    SetRowType(i);
                }
                Response.Redirect(this.Page.Request.Url.ToString());
            }
            else if (e.CommandName == "Save")
            {
                int ProeuctKey = e.CommandArgument.ToString().ToInt32();
                var ObjItem = ObjProductforDispatchingBLL.GetByID(ProeuctKey);

                HiddenField ObjHideCatogry = e.Item.FindControl("hideCategoryID") as HiddenField;
                HiddenField ObjHideCatgoryEmpLoyeeID = e.Item.FindControl("hideNewEmpLoyeeID") as HiddenField;


                ObjItem.Updatetime = DateTime.Now;
                ObjItem.ImageUrl = string.Empty;
                ObjItem.IsDelete = false;
                ObjItem.NewAdd = true;
                ObjItem.Updatetime = DateTime.Now;

                ObjItem.ServiceContent = ((TextBox)e.Item.FindControl("txtProductName")).Text;
                ObjItem.Requirement = ((TextBox)e.Item.FindControl("txtRequirement")).Text;
                ObjItem.PurchasePrice = ((TextBox)e.Item.FindControl("txtPurchasePrice")).Text.ToDecimal();
                ObjItem.UnitPrice = ((TextBox)e.Item.FindControl("txtSalePrice")).Text.ToDecimal();
                ObjItem.Quantity = ((TextBox)e.Item.FindControl("txtQuantity")).Text.ToInt32();
                ObjItem.Subtotal = (ObjItem.Quantity * ObjItem.PurchasePrice).ToString().ToDecimal();
                ObjItem.Remark = ((TextBox)e.Item.FindControl("txtRemark")).Text;
                ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                ObjItem.PlannedCompletionTime = DateTime.Now;
                ObjItem.FinishDate = DateTime.Now;
                ObjItem.SupplierName = ((TextBox)e.Item.FindControl("txtSuppName")).Text.Trim().ToString();

                ObjProductforDispatchingBLL.Update(ObjItem);
                if (ObjItem.RowType == null)
                {
                    ObjItem.RowType = 1;
                }
                if (ObjHideCatgoryEmpLoyeeID != null)
                {
                    if (ObjHideCatgoryEmpLoyeeID.Value.ToInt32() > 0)
                    {
                        var ItemUpdateModel = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjItem.CategoryID, 2);
                        ItemUpdateModel.EmployeeID = ObjHideCatgoryEmpLoyeeID.Value.ToInt32();
                        ObjProductforDispatchingBLL.Update(ItemUpdateModel);
                    }
                }

            }
            BinderData();
            BindDatasList();
            for (int i = 1; i <= 5; i++)
            {
                SetRowType(i);
            }


        }
        #endregion

        #region Repeater 事件保存数据
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
            SaveaPageChange();      //保存
            CostSumInsert();        //保存成本表 (里面也已经保存结算表)
            JavaScriptTools.AlertWindow("保存完毕!", Page);
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

        #region 选择供应商...四种类型

        /// <summary>
        /// 保存指定供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavesupperSave_Click(object sender, EventArgs e)
        {
            SetRowType(1);
        }


        /// <summary>
        /// 指定选择项为库房
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetWarhouse_Click(object sender, EventArgs e)
        {

            SetRowType(2);
        }


        /// <summary>
        /// 指定为责任人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSuppertoOther_Click(object sender, EventArgs e)
        {
            SetRowType(3);

        }
        /// <summary>
        /// 设定为新购入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetBuy_Click(object sender, EventArgs e)
        {
            SetRowType(4);
        }
        #endregion

        #region 四大金刚
        /// <summary>
        /// 指定四大金刚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnFourGuardianSave_Click(object sender, EventArgs e)
        {
            SetRowType(5);
        }
        #endregion

        #region 指定人员(五种)
        /// <summary>
        /// 指定责任单位 主要以Rowtype区分 1供应商 2 库房 6责任人 4新购入 4四大金刚
        /// </summary>
        /// <param name="Type"></param>
        private void SetRowType(int Type)
        {
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

                    //人员里面 所有都分配至执行团队
                    if (ObjItem.ParentCategoryID == 190 || ObjItem.ParentCategoryID == 191 || ObjItem.ParentCategoryID == 300 || ObjItem.CategoryID == 190 || ObjItem.CategoryID == 191 || ObjItem.CategoryID == 300)
                    {
                        if (ObjItem.SupplierName != "库房" && ObjItem.SupplierName != "" && ObjItem.RowType != 4)
                        {
                            ObjItem.RowType = 5;
                            ObjProductforDispatchingBLL.Update(ObjItem);
                        }
                    }

                    var IsCheck = (ObjrepList.Items[I].FindControl("CheckItem") as CheckBox).Checked;
                    if (!IsCheck)
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
                    ObjItem.PurchasePrice = ((TextBox)ObjrepList.Items[I].FindControl("txtSalePrice")).Text.ToDecimal();
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.Subtotal = (ObjItem.PurchasePrice * ObjItem.Quantity.ToString().ToDecimal()).ToString().ToDecimal();
                    ObjItem.Remark = ((TextBox)ObjrepList.Items[I].FindControl("txtRemark")).Text;
                    ObjItem.TaskEmpLoyee = User.Identity.Name.ToInt32();
                    ObjItem.PlannedCompletionTime = DateTime.Now;
                    ObjItem.FinishDate = DateTime.Now;
                    //ObjItem.SupplierName == ((HtmlInputText)ObjrepList.Items[I].FindControl("txtSuppName")).Value;

                    //人员 3、5   物料 1、2、4   其他 11
                    switch (Type)
                    {
                        case 1:         //供应商
                            ObjItem.SupplierID = hideSuppID.Value.ToInt32();
                            var ObjSupplierModel = ObjSupplierBLL.GetByID(ObjItem.SupplierID);
                            ObjItem.SupplierName = ObjSupplierModel.Name;
                            ObjItem.RowType = 1;
                            break;
                        case 2:         //库房
                            ObjItem.SupplierID = -1;
                            ObjItem.SupplierName = "库房";
                            ObjItem.RowType = 5;

                            break;
                        case 3:         //人员
                            ObjItem.SupplierID = hideSuppID.Value.ToInt32();
                            ObjItem.SupplierName = ObjEmployeeBLL.GetByID(ObjItem.SupplierID).EmployeeName;
                            ObjItem.RowType = 5;
                            break;
                        case 4:         //新购入
                            ObjItem.SupplierID = -1;
                            ObjItem.SupplierName = "新购入";
                            break;
                        case 5:         //四大金刚
                            ObjItem.SupplierID = hideSuppID.Value.ToInt32();
                            GuardianType ObjGuardTypeBLL = new GuardianType();
                            FD_FourGuardian ObjFourGuardianModel = ObjFourGuardianBLL.GetByID(ObjItem.SupplierID);
                            ObjItem.SupplierName = ObjFourGuardianModel.GuardianName;
                            ObjItem.PurchasePrice = ObjFourGuardianModel.CooperationPrice;
                            ObjItem.Subtotal = ObjItem.Quantity * ObjItem.PurchasePrice;
                            ObjItem.RowType = 4;
                            break;
                    }

                    if (ObjItem.ParentCategoryID == 196 || ObjItem.CategoryID == 196)      //其他  单独给一个RowType
                    {
                        ObjItem.RowType = 11;
                    }

                    ObjProductforDispatchingBLL.Update(ObjItem);
                }
            }

            BinderData();
            BindDatasList();
            CostSumInsert();
        }
        #endregion

        #region 复制  四种类型的复制
        /// <summary>
        /// 复制到另一个类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnCppy_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "复制到花艺":
                    CopyDate("花艺", 194);
                    break;

                case "复制到道具":
                    CopyDate("道具", 192);
                    break;

                case "复制到灯光":
                    CopyDate("灯光", 193);
                    break;

                case "复制到人员":
                    CopyDate("人员", 191);
                    break;
            }
        }
        #endregion

        #region 拷贝数据
        /// <summary>
        /// 拷贝数据到指定的类别下
        /// </summary>
        private void CopyDate(string ClassFire, int Pid)
        {

            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                //int ParentEmpLoyeeID = 0;
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_ProductforDispatching ObjItem;
                var InserModel = new FL_ProductforDispatching();
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    var IsCheck = (ObjrepList.Items[I].FindControl("CheckItem") as CheckBox).Checked;
                    if (IsCheck)
                    {
                        ObjItem = ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                        if (ObjItem != null)
                        {
                            if (ObjItem.ItemLevel == 3)
                            {
                                InserModel = new FL_ProductforDispatching();

                                InserModel = ObjItem;

                                ObjItem.Classification = ClassFire;
                                InserModel.Classification = ClassFire;
                                ObjProductforDispatchingBLL.Insert(InserModel);

                                var ObjUpdateModel = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.ParentCategoryID, 1).FirstOrDefault();

                                ObjUpdateModel.Classification = ObjUpdateModel.Classification.Replace(ClassFire, "");
                                ObjUpdateModel.Classification += ClassFire;
                                ObjProductforDispatchingBLL.Update(ObjUpdateModel);
                                //ObjProductforDispatchingBLL.Delete(ObjProductforDispatchingBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32()));
                                //ObjItem.Classification = ClassFire;
                                //ObjItem.ParentCategoryID = Pid;
                                //ObjProductforDispatchingBLL.Update(ObjItem);
                            }
                        }
                    }
                }
            }

            JavaScriptTools.AlertWindowAndLocation("已将所选产品复制到" + ClassFire, Request.Url.ToString(), Page);
            BinderData();
            BindDatasList();
        }
        #endregion

        #region 分配供应商之后  底部列表显示

        public void BindDatasList()
        {
            if (WorkType != "人员")
            {
                List<FL_ProductforDispatching> ProductList = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3, WorkType);
                var td = (from C in ProductList group C by new { C.SupplierName } into g select g);
                List<Person> list = new List<Person>();
                Person p = new Person();

                foreach (var item in td)
                {
                    p = new Person();
                    p.Sname = item.Key.SupplierName.ToString();
                    list.Add(p);
                }

                rptProduct.DataSource = list;
                rptProduct.DataBind();
            }
        }

        protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSupplyName = e.Item.FindControl("lblSupplyName") as Label;
            RptData = e.Item.FindControl("rptDataList") as Repeater;
            List<FL_ProductforDispatching> ProductList1 = ObjProductforDispatchingBLL.GetForSupplierName(lblSupplyName.Text.Trim().ToString(), WorkType, DispatchingID, 3);
            if (WorkType != "人员")
            {
                Label lblSumMoney = e.Item.FindControl("lblSumMoney") as Label;
                Label lblSumQuantity = e.Item.FindControl("lblSumQuantity") as Label;

                lblSumMoney.Text = (lblSumMoney.Text.ToDecimal() + ProductList1.Where(C => C.Subtotal != null).ToList().Sum(C => C.Subtotal.Value)).ToString();
                lblSumQuantity.Text = ProductList1.Sum(C => C.Quantity).ToString();

                RptData.DataSource = ProductList1;
                RptData.DataBind();
            }

        }

        #region 导出


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=PriceManage" + DateTime.Now.Date.ToString("yyyyMMdd") + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.rptProduct.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();


        }
        #endregion



        #endregion

        #region 成本表中的新增 和结算表的新增 (修改)


        public void CostSumInsert()
        {
            List<CostSumTs> DataLists = ObjCostForOrderBLL.GetCostSums(DispatchingID);  //存的供应商的名称 总价格(存储过程实现的)

            #region 删除不存在的供应商 两个表  1.成本表 2.结算表

            List<PMSParameters> pars = new List<PMSParameters>();
            foreach (var item in DataLists)
            {
                //当供应商改变后 ，该供应商可能就不存在  是最后一个  就需删除
                pars.Add("Name", item.Name, NSqlTypes.StringNotIN);     //不包含已存在的供应商  NotIn
            }

            pars.Add("DispatchingID", DispatchingID, NSqlTypes.Equal);
            pars.Add("RowType", 6, NSqlTypes.NotEquals);        //设计师 自动添加
            pars.Add("RowType", 7, NSqlTypes.NotEquals);        //(物料) 手动添加人员
            pars.Add("RowType", 8, NSqlTypes.NotEquals);        //(花艺) 手动添加花艺
            pars.Add("RowType", 9, NSqlTypes.NotEquals);        //(其他) 手动添加其他
            pars.Add("RowType", 10, NSqlTypes.NotEquals);        //设计清单

            List<FL_CostSum> NotCostSum = ObjCostSumBLL.GetAllByparameter(pars, OrderColumnName, PageSize, PageIndex, ref SourceCount);
            if (NotCostSum.Count > 0)
            {
                foreach (var NItem in NotCostSum)
                {
                    if (!(NItem.Name.Contains("(预定)")))
                    {
                        ObjCostSumBLL.Delete(NItem);
                        ObjStatementBLL.Delete(NItem.DispatchingID.ToString().ToInt32(), NItem.Name.ToString());
                    }
                }
            }

            //合计为0的 将产品中的CostSumID修改为NULL
            List<FL_ProductforDispatching> ProForDis1 = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3);
            foreach (var ProDisItem in ProForDis1)
            {
                if (ProDisItem.PurchasePrice == 0 && ProDisItem.Subtotal == 0)
                {
                    ProDisItem.CostSumId = null;
                    ObjProductforDispatchingBLL.Update(ProDisItem);
                }
            }

            #endregion

            FL_CostSum cost = new FL_CostSum();

            foreach (var item in DataLists)
            {
                //一个供应商 同一个单子 可能会负责多个产品  查出供应商的集合(第三级) 根据供应商的名称来查询
                var Dlist = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3, "", item.Name).Where(C => C.Subtotal > 0 && C.PurchasePrice > 0 && C.Quantity >= 1).ToList();
                int index = 0;
                foreach (var it in Dlist)
                {
                    //判断该供应商是否存在   因为是算出的总和  所以在成本表中  一个供应商 只能出现一次
                    cost = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, item.Name);

                    if (cost == null || cost.RowType == 6)       //为null  说明供应商还没有添加到成本表中  属于新增   6.代表设计师   如果dj或vj也是设计师  也可正常添加
                    {
                        cost = new FL_CostSum();
                        cost.Name = item.Name;
                        cost.Sumtotal = item.Sumtotal;
                        cost.ActualSumTotal = item.Sumtotal;
                        cost.PayMent = 0;
                        cost.NoPayMent = item.Sumtotal;
                        cost.RowType = it.RowType;
                        cost.Evaluation = 6;        //6代表 未评价
                        cost.DispatchingID = DispatchingID.ToString().ToInt32();
                        cost.Advance = "";
                        cost.ShortCome = "";
                        cost.CustomerId = Request["CustomerID"].ToInt32();
                        cost.CreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                        cost.OrderID = Request["OrderID"].ToInt32();
                        cost.QuotedID = Request["QuotedID"].ToInt32();
                        cost.EmployeeID = User.Identity.Name.ToInt32();     //创建人
                        cost.Remark = "";
                        cost.Content = it.ServiceContent;
                        cost.CategoryName = it.ServiceContent;
                        ObjCostSumBLL.Insert(cost);
                    }
                    else if (cost != null)      //供应商存在  说明该供应商有多个成品  把该产品的价格加入到该供应商的合计价格中去
                    {
                        if (index == 0)     //第一次清空内容
                        {
                            cost.RowType = it.RowType;
                            cost.CategoryName = "";
                            cost.Content = "";
                            cost.CategoryName += it.ServiceContent;
                            cost.Content += it.ServiceContent;
                            cost.Sumtotal = item.Sumtotal;
                            cost.ActualSumTotal = item.Sumtotal; //不能修改实际支出 因为财务那边修改之后  这边保存一遍  实际支出就会改变
                        }
                        else
                        {
                            cost.RowType = it.RowType;
                            if (cost.Content.Length >= 10)
                            {
                                cost.Content = it.ServiceContent;      //各项产品
                                cost.CategoryName = it.ServiceContent;     //各项产品
                            }
                            else
                            {
                                cost.Content += "," + it.ServiceContent;      //各项产品
                                cost.CategoryName += "," + it.ServiceContent;     //各项产品
                            }

                        }
                        ObjCostSumBLL.Update(cost);
                        index++;
                    }

                    //修改产品表中的CostSumId  要和成本表中相对应
                    FL_ProductforDispatching ProForDis = ObjProductforDispatchingBLL.GetByID(it.ProeuctKey);
                    ProForDis.CostSumId = cost.CostSumId;
                    ObjProductforDispatchingBLL.Update(ProForDis);
                }
            }

            InsertStatement();      //结算表数据填充(新增/修改)
        }
        #endregion

        #region 保存结算表
        public void InsertStatement()
        {
            var DatasList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);
            foreach (var item in DatasList)
            {
                #region 结算表

                FL_Statement ObjStatementModel = new FL_Statement();
                #region 判断类别
                switch (item.RowType)
                {
                    case 1:         //供应商   
                        var ObjSupplierModel = ObjSupplierBLL.GetByName(item.Name);
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.SupplierID = ObjSupplierModel.SupplierID;
                        if (ObjSupplierModel != null)
                        {
                            ObjStatementModel.TypeID = ObjSupplierModel.CategoryID;
                        }
                        else
                        {
                            foreach (var AllItem in ObjSupplierBLL.GetByAll())
                            {
                                if (item.Name.Contains(AllItem.Name) || item.Name.Contains(AllItem.Name))
                                {
                                    ObjStatementModel.TypeID = AllItem.CategoryID;
                                }
                            }
                        }
                        ObjStatementModel.TypeName = ObjSuplierTypeBLL.GetByID(ObjStatementModel.TypeID).TypeName;
                        ObjStatementModel.RowType = 1;

                        break;
                    case 2:         //库房          //显示  因为没有明确的收款人  结算表不方便结算
                        ObjStatementModel.Name = "库房";
                        ObjStatementModel.SupplierName = "库房";
                        ObjStatementModel.TypeID = -1;
                        ObjStatementModel.TypeName = "库房";
                        ObjStatementModel.RowType = 2;

                        break;
                    case 3:         //新购买
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -5;
                        ObjStatementModel.TypeName = "新购买";
                        ObjStatementModel.RowType = 3;

                        break;
                    case 4:         //四大金刚
                        string name = item.Name.Replace("(预定)", "");
                        var ObjFourGuardianModel = ObjFourGuardianBLL.GetByName(name);
                        if (ObjFourGuardianModel == null)
                        {
                            var ObjEmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                            if (ObjEmployeeModel != null)
                            {
                                ObjStatementModel.Name = item.Name;
                                ObjStatementModel.SupplierName = item.Name;
                                ObjStatementModel.SupplierID = ObjEmployeeModel.EmployeeID;
                                ObjStatementModel.TypeID = -2;
                                ObjStatementModel.TypeName = "内部人员";
                                ObjStatementModel.RowType = 5;
                            }
                        }
                        if (ObjFourGuardianModel != null)
                        {
                            GuardianType ObjGuardTypeBLL = new GuardianType();

                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = ObjFourGuardianModel.GuardianId;
                            ObjStatementModel.TypeID = ObjFourGuardianModel.GuardianTypeId.ToString().ToInt32();
                            ObjStatementModel.TypeName = ObjGuardTypeBLL.GetByID(ObjFourGuardianModel.GuardianTypeId).TypeName;
                            ObjStatementModel.RowType = 4;
                        }
                        break;
                    case 5:         //人员
                        var ObjEmployeeModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (ObjEmployeeModels != null)
                        {
                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = ObjEmployeeModels.EmployeeID;
                            ObjStatementModel.TypeID = -2;
                            ObjStatementModel.TypeName = "内部人员";
                            ObjStatementModel.RowType = 5;
                        }
                        else
                        {
                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = 0;
                            ObjStatementModel.TypeID = -2;
                            ObjStatementModel.TypeName = "外部人员";
                            ObjStatementModel.RowType = 5;
                        }
                        break;
                    case 6:         //设计师/工程主管
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -3;
                        ObjStatementModel.TypeName = "设计师";
                        var EmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                        ObjStatementModel.SupplierID = EmployeeModel.EmployeeID;
                        ObjStatementModel.RowType = 5;
                        break;
                    case 7:         //内部人员/四大金刚 (手动添加)
                        var PersonModel = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModel == null)
                        {
                            ObjStatementModel.SupplierID = ObjFourGuardianBLL.GetByName(item.Name).GuardianId;
                            ObjStatementModel.RowType = 4;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = PersonModel.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -4;
                        ObjStatementModel.TypeName = "人员(手动添加)";
                        break;
                    case 8:         //内部人员/供应商 (手动添加)
                        var PersonModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModels == null)
                        {
                            ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                            ObjStatementModel.RowType = 1;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = PersonModels.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -3;
                        ObjStatementModel.TypeName = "物料 (手动添加)";
                        break;
                    case 9:         //内部人员/供应商 (手动添加)
                        var OtherModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (OtherModels == null)
                        {
                            ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                            ObjStatementModel.RowType = 1;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = OtherModels.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -2;
                        ObjStatementModel.TypeName = "其他(手动添加)";
                        break;
                    case 10:         //设计清单(name代表供应商 国色广告)
                        ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -4;
                        ObjStatementModel.TypeName = "设计清单";
                        ObjStatementModel.RowType = 1;
                        break;
                }
                #endregion

                ObjStatementModel.CustomerID = Request["CustomerID"].ToInt32();
                ObjStatementModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjStatementModel.CreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                ObjStatementModel.DispatchingID = Request["DispatchingID"].ToInt32();
                ObjStatementModel.OrderId = Request["OrderID"].ToInt32();
                ObjStatementModel.QuotedId = Request["QuotedID"].ToInt32();
                ObjStatementModel.Remark = "";
                ObjStatementModel.Finishtation = "";
                ObjStatementModel.SumTotal = item.ActualSumTotal;
                ObjStatementModel.Content = item.Content;
                ObjStatementModel.PayMent = 0;
                ObjStatementModel.NoPayMent = item.ActualSumTotal;
                ObjStatementModel.CostSumId = item.CostSumId;
                ObjStatementModel.Year = ObjCustomersBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Year;
                ObjStatementModel.Month = ObjCustomersBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Month;


                FL_Statement StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, item.Name);
                if (StatementModel != null)    //已经存在
                {
                    var DataList = ObjCostSumBLL.GetByDispatchingIDNames(DispatchingID, item.Name);
                    StatementModel.Name = ObjStatementModel.Name;               //名称
                    StatementModel.SupplierID = ObjStatementModel.SupplierID;   //供应商ID
                    StatementModel.TypeID = ObjStatementModel.TypeID;           //类型ID
                    StatementModel.TypeName = ObjStatementModel.TypeName;       //类型名称
                    StatementModel.RowType = ObjStatementModel.RowType;         //供应商类别

                    if (DataList.Count >= 2)
                    {
                        StatementModel.SumTotal = DataList.Sum(C => C.ActualSumTotal);       //金额
                    }
                    else
                    {
                        StatementModel.SumTotal = ObjStatementModel.SumTotal;       //金额
                    }


                    StatementModel.PayMent = ObjStatementModel.PayMent;         //已付款
                    if (ObjStatementModel.PayMent == 0)             //从未支付过说明 未支付等于结算金额
                    {
                        StatementModel.NoPayMent = StatementModel.SumTotal;
                    }
                    else
                    {
                        StatementModel.NoPayMent = ObjStatementModel.NoPayMent;     //未付款
                    }
                    ObjStatementBLL.Update(StatementModel);                     //修改更新
                }
                else
                {
                    if (ObjStatementModel.SupplierID == null)
                    {
                        string name = ObjStatementModel.Name;
                    }
                    if (item.RowType != 12 || ObjStatementModel.Name != null)
                    {
                        ObjStatementBLL.Insert(ObjStatementModel);
                    }
                }

                #endregion
            }
        }
        #endregion

        #region 获取 QuotedID  ChangeId
        public string GetQuotedID(object Source)
        {
            int DispatchingID = Source.ToString().ToInt32();
            var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
            int QuotedId = ObjDispatchingModel.QuotedID.ToString().ToInt32();
            return QuotedId.ToString();

        }

        /// <summary>
        /// 获取ChangeID
        /// </summary>
        public string GetChangeId(object Source)
        {
            if (Source != null && Source.ToString().ToInt32() > 0)
            {
                DispatchingID = Request["DispatchingID"].ToInt32();
                int ProductId = Source.ToString().ToInt32();
                var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
                int OrderId = ObjDispatchingModel.OrderID.ToString().ToInt32();
                var QuotedPriceModel = ObjQuotedPriceItemBLL.GetByProductIDOrder(OrderId, ProductId, 3);
                if (QuotedPriceModel != null)
                {
                    return QuotedPriceModel.ChangeID.ToString();
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        #endregion

        #region 判断是否有上传图片
        /// <summary>
        /// 返回大于0 说明有上传图片
        /// </summary>
        /// <param name="Source">DispatchingID</param>
        /// <param name="Sources">ProductID</param>
        /// <returns></returns>  
        public int GetByQuoted(object Source, object Sources)
        {
            if (Sources != null && Sources.ToString().ToInt32() > 0)
            {

                int DisID = Source.ToString().ToInt32();
                int ProductId = Sources.ToString().ToInt32();
                var DataList = ObjQuotedPriceBLL.GetImageByKind(GetQuotedID(DisID).ToInt32(), GetChangeId(ProductId).ToInt32(), 1);
                if (DataList.Count > 0)
                {
                    int result = ObjQuotedPriceBLL.GetImageByKind(GetQuotedID(DisID).ToInt32(), GetChangeId(ProductId).ToInt32(), 1).Count;
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
        #endregion

        #region 屏蔽  无用


        #region 添加至CostForOrder
        //添加 CostForOrder
        public void InsertCostforOrder(FL_ProductforDispatching ObjProduct)
        {
            if (ObjProduct.Subtotal >= 1 && ObjProduct.PurchasePrice > 0)
            {
                FL_CostforOrder cost = new FL_CostforOrder()
                {
                    DispatchingID = DispatchingID,
                    CustomerID = Request["CustomerID"].ToInt32(),
                    CreateDate = DateTime.Now,
                    Lock = false,
                    CreateEmpLoyee = User.Identity.Name.ToInt32(),
                    //Name = WorkType.ToString() + "总成本",
                    Name = ObjProduct.SupplierName,
                    Node = ObjProduct.CategoryName,
                    PlanCost = ObjProduct.Subtotal.ToString().ToDecimal(),
                    RowType = ObjProduct.RowType.ToString().ToInt32(),
                    FinishCost = ObjProduct.Subtotal.ToString().ToDecimal(),
                    LockDate = DateTime.Now.AddYears(-50),
                    LockEmployee = -1,
                    OrderID = OrderID,
                };
                ObjCostForOrderBLL.Insert(cost);
            }
        }
        #endregion

        #region 判断 添加 还是修改

        public void AddsOrUpdateCost(FL_ProductforDispatching ObjItem)
        {
            List<FL_CostforOrder> ObjDisList = ObjCostForOrderBLL.GetByDispatchingID(DispatchingID);
            foreach (var item in ObjDisList)
            {
                ObjCostForOrderBLL.Delete(item);
            }

            var ObjUpdateModelList = ObjCostForOrderBLL.GetByRowTypes(ObjItem.RowType.ToString().ToInt32(), DispatchingID, ObjItem.SupplierName, ObjItem.CategoryName);
            if (ObjUpdateModelList.Count > 0)       //大于0  证明该项存在  只需修改   否则新增
            {
                foreach (var ObjItems in ObjUpdateModelList)
                {
                    ObjItems.Name = ObjItem.SupplierName;
                    ObjItems.PlanCost = ObjItem.Subtotal.ToString().ToDecimal();
                    ObjItems.FinishCost = ObjItem.Subtotal.ToString().ToDecimal();
                    ObjCostForOrderBLL.Update(ObjItems);
                }
            }
            else
            {
                InsertCostforOrder(ObjItem);
            }

        }
        #endregion


        #region 算出总成本
        /// <summary>
        /// 计算成本
        /// </summary>
        /// <param name="ObjItem"></param>

        //public void InsertCostSum()
        //{

        //    FL_CostSum cost = new FL_CostSum();
        //    FL_Statement ObjStatementModel = new FL_Statement();
        //    List<PMSParameters> pars = new List<PMSParameters>();
        //    List<CostSumTs> DataLists = ObjCostForOrderBLL.GetCostSums(DispatchingID);

        //    foreach (var item in DataLists)
        //    {
        //        var Dlist = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3, "", item.Name).Where(C => C.Subtotal > 0 && C.PurchasePrice > 0 && C.Quantity >= 1).ToList();
        //        int index = 0;
        //        foreach (var it in Dlist)
        //        {
        //            if (it.CostSumId == null || it.CostSumId == 0)          //不存在  就新增
        //            {
        //                cost = new FL_CostSum();
        //                ObjStatementModel = new FL_Statement();
        //            }
        //            else if (it.CostSumId != null)                      //存在  查找出该项 进行修改
        //            {
        //                //cost = ObjCostSumBLL.GetByID(it.CostSumId);
        //                var DataList = ObjCostSumBLL.GetByDispatchingIDNames(DispatchingID, item.Name);
        //                if (DataList.Count >= 2)        //出现两个 说明出错  删除之后 重新添加
        //                {
        //                    //修改产品表的CostSumID
        //                    List<FL_ProductforDispatching> ProductList = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID);     //修改产品表
        //                    foreach (var pitem in ProductList)
        //                    {
        //                        pitem.CostSumId = null;
        //                        ObjProductforDispatchingBLL.Update(pitem);
        //                    }
        //                    //删除成本表
        //                    List<FL_CostSum> CostList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);
        //                    foreach (var costitem in CostList)
        //                    {
        //                        ObjCostSumBLL.Delete(costitem);
        //                    }

        //                    //删除结算表
        //                    List<FL_Statement> StateMentList = ObjStatementBLL.GetByDispatchingID(DispatchingID);
        //                    foreach (var Statement in StateMentList)
        //                    {
        //                        ObjStatementBLL.Delete(Statement);
        //                    }

        //                    cost = new FL_CostSum();
        //                }
        //                else        //正常情况  只需修改
        //                {
        //                    cost = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, item.Name);
        //                }
        //            }


        //            if (cost != null)
        //            {
        //                cost.RowType = it.RowType;
        //                cost.Name = item.Name;
        //                cost.Sumtotal = item.Sumtotal;
        //                cost.ActualSumTotal = item.Sumtotal;
        //                cost.DispatchingID = DispatchingID.ToString().ToInt32();
        //                cost.CustomerId = Request["CustomerID"].ToInt32();
        //                cost.OrderID = Request["OrderID"].ToInt32();
        //                cost.QuotedID = Request["QuotedID"].ToInt32();
        //                cost.EmployeeID = User.Identity.Name.ToInt32();

        //                if (it.CostSumId == null || it.CostSumId == 0)           //新增  所以CostSumId为null  修改产品中CostSumId
        //                {

        //                    cost.CategoryName += it.ServiceContent + " - ";     //类型名称
        //                    cost.Content += it.ServiceContent + ",";            //内容

        //                    if (ObjCostSumBLL.GetByName(item.Name) == null)       //该供应商名不存在  就新增
        //                    {
        //                        cost.Evaluation = 6;
        //                        cost.CreateDate = DateTime.Now.ToString().ToDateTime();
        //                        cost.Advance = "";
        //                        cost.ShortCome = "";
        //                        ObjCostSumBLL.Insert(cost);
        //                    }

        //                    if (ObjCostSumBLL.GetByName(item.Name) != null)       //因为算合计  该供应商名称已存在  就覆盖修改
        //                    {
        //                        FL_CostSum CsModel = ObjCostSumBLL.GetByCheckID(item.Name, DispatchingID);
        //                        if (CsModel != null)            //DispatchingID对应存在
        //                        {
        //                            if (index == 0)     //第一次清空内容
        //                            {
        //                                CsModel.CategoryName = "";
        //                                CsModel.Content = "";
        //                            }

        //                            CsModel.CategoryName += it.ServiceContent + " - ";
        //                            CsModel.Content += it.ServiceContent + ",";
        //                            cost.CostSumId = CsModel.CostSumId;
        //                            CsModel.Sumtotal = cost.Sumtotal;
        //                            CsModel.ActualSumTotal = cost.ActualSumTotal;
        //                            ObjCostSumBLL.Update(CsModel);
        //                        }
        //                        else                        //DispatchingID对应不存在
        //                        {
        //                            cost.CreateDate = DateTime.Now.ToString().ToDateTime();
        //                            cost.Evaluation = 6;
        //                            cost.Advance = "";
        //                            cost.ShortCome = "";
        //                            ObjCostSumBLL.Insert(cost);
        //                        }
        //                    }
        //                }

        //                if (it.CostSumId != null)            //修改   有值  就说明存在
        //                {
        //                    if (index == 0)     //第一次清空内容
        //                    {
        //                        cost.CategoryName = "";
        //                        cost.Content = "";
        //                    }

        //                    cost.CategoryName += it.ServiceContent + " - ";
        //                    cost.Content += it.ServiceContent + ",";

        //                    if (ObjCostSumBLL.GetByID(it.CostSumId) != null)
        //                    {
        //                        cost.Sumtotal = cost.Sumtotal;
        //                        cost.ActualSumTotal = cost.ActualSumTotal;
        //                        ObjCostSumBLL.Update(cost);
        //                    }

        //                    if (ObjCostSumBLL.GetByID(it.CostSumId) == null)
        //                    {
        //                        cost.CreateDate = DateTime.Now.ToString().ToDateTime();
        //                        ObjCostSumBLL.Insert(cost);
        //                    }
        //                }
        //                FL_ProductforDispatching ProForDis = ObjProductforDispatchingBLL.GetByID(it.ProeuctKey);
        //                ProForDis.CostSumId = cost.CostSumId;
        //                ObjProductforDispatchingBLL.Update(ProForDis);
        //            }
        //            index++;
        //        }
        //    }

        //    //InsertStatement();



        //    //合计为0的 将产品中的CostSumID修改为NULL
        //    List<FL_ProductforDispatching> ProForDis1 = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3);
        //    foreach (var ProDisItem in ProForDis1)
        //    {
        //        if (ProDisItem.PurchasePrice == 0 && ProDisItem.Subtotal == 0)
        //        {
        //            ProDisItem.CostSumId = null;
        //            ObjProductforDispatchingBLL.Update(ProDisItem);
        //        }
        //    }


        //}
        #endregion
        #endregion


        //派工之下的列表显示需要此类的帮助
        public class Person
        {
            public string Sname { get; set; }
        }

        #region 重置价格为0
        /// <summary>
        /// 重置价格
        /// </summary>
        protected void btnClearCost_Click(object sender, EventArgs e)
        {

            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");

                if (ObjrepList != null)
                {
                    //保存主体
                    for (int I = 0; I < ObjrepList.Items.Count; I++)
                    {
                        TextBox txtprices = ObjrepList.Items[I].FindControl("txtPurchasePrice") as TextBox;
                        if (txtprices != null)
                        {
                            txtprices.Text = "0.00";
                        }
                    }
                }
            }
        }
        #endregion

        #region 改派工程主管
        /// <summary>
        /// 改派工程主管
        /// </summary>
        protected void btnSaveDesigner_Click(object sender, EventArgs e)
        {
            int EmployeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
            var DataList = ObjProductforDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());
            var Model = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
            var QuotedModel = ObjQuotedPriceBLL.GetByOrderId(Request["OrderID"].ToInt32());
            int result = 0;
            if (Model != null || QuotedModel != null)
            {
                SaveDesignCost(QuotedModel, EmployeeID);
                if (Model != null)
                {
                    Model.Director = EmployeeID;
                    result = ObjDispatchingBLL.Update(Model);
                    if (DataList.Count > 0)
                    {
                        foreach (var item in DataList)
                        {
                            item.DirectorEmployee = EmployeeID;
                            ObjProductforDispatchingBLL.Update(item);
                        }
                    }
                }
                if (QuotedModel != null)
                {
                    QuotedModel.Director = EmployeeID;
                    result += ObjQuotedPriceBLL.Update(QuotedModel);
                }

            }
            if (result >= 2)
            {
                JavaScriptTools.AlertWindow("成功分派工程主管", Page);
            }


        }
        #endregion

        #region 添加工程主管成本
        /// <summary>
        /// 添加成本
        /// </summary>
        public void SaveDesignCost(FL_QuotedPrice QuotedModel, int Designer = 1)
        {
            FL_CostSum CostSum = new FL_CostSum();

            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
            CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            CostSum.ShortCome = "";
            CostSum.Advance = "";
            CostSum.OrderID = Request["OrderID"].ToInt32();
            CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
            CostSum.CustomerId = Request["CustomerID"].ToInt32();
            CostSum.Name = GetEmployeeName(Designer);
            CostSum.RowType = 6;
            CostSum.Content = "分配的工程主管";
            CostSum.CategoryName = "分配的工程主管";
            CostSum.Sumtotal = 100;
            CostSum.ActualSumTotal = 100;
            CostSum.PayMent = 0;
            CostSum.NoPayMent = 100;
            CostSum.Evaluation = 6;
            CostSum.EmployeeID = User.Identity.Name.ToInt32();

            var Model = ObjCostSumBLL.GetByCheckID(GetEmployeeName(Designer), Request["DispatchingID"].ToInt32(), 6);       //工程主管(6 也可代表设计师)
            if (Model == null)          //实体为null   没添加过 (就可以新增 添加)  确保不重复添加
            {
                if (QuotedModel.Director != null || QuotedModel.Director.ToString() != "")          //不是null 或者不等于空  就说明已经存过了
                {
                    if (QuotedModel.Director != Designer)       //之前的工程主管和现在选择的工程主管不一样  就是修改
                    {
                        CostSum = ObjCostSumBLL.GetByCheckID(GetEmployeeName(QuotedModel.Director), Request["DispatchingID"].ToInt32(), 6);
                        CostSum.Name = GetEmployeeName(Designer);
                        ObjCostSumBLL.Update(CostSum);
                    }
                }
                else                        //第一次增加
                {
                    ObjCostSumBLL.Insert(CostSum);
                }
            }
        }

        #endregion

    }
}