using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceDispatchingUpdate : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 配置文件
        /// </summary>
        SysConfig ObjSysConfigBLL = new SysConfig();

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        StorehouseSourceProduct storehouseSourceProductBLL = new StorehouseSourceProduct();
        /// <summary>
        /// 报价单
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        /// <summary>
        /// 部门
        /// </summary>
        Department ObjDepartmentBLL = new Department();


        Employee ObjEmployeeBLL = new Employee();


        /// <summary>
        /// 各项合计
        /// </summary>
        QuotedPriceForType ObjForTypeBLL = new QuotedPriceForType();

        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int QuotedEmployee = 0;
        string Type = "";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            QuotedEmployee = Request["QuotedEmployee"].ToInt32();
            if (!IsPostBack)
            {

                var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                int CheckEmployee = ObjDepartmentBLL.GetByID(ObjEmployeeBLL.GetByID(ObjQuotedModel.EmpLoyeeID).DepartmentID).DepartmentManager.ToString().ToInt32();
                ObjQuotedModel.ChecksEmployee = CheckEmployee;
                ObjQuotedPriceBLL.Update(ObjQuotedModel);


                BinderData();
            }
        }
        #endregion

        #region 数据绑定 BinderData
        /// <summary>
        /// 绑定
        /// </summary>
        public void BinderData()
        {

            QuotedID = Request["QuotedID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();

            var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);

            //数据绑定
            var ObjList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0);
            this.repfirst.DataSource = ObjList;
            this.repfirst.DataBind();


            //判断登陆人是否有权限操作


            if (ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeTypeID == 3 && ObjQuotedModel.EmpLoyeeID != User.Identity.Name.ToInt32())      //当前登录员工不是管理层  也不是策划师
            {
                IsVisible();
            }
            else if (ObjQuotedModel.CheckState >= 2 || Request["Type"].ToString() == "Look")        //已通过审核或点解查看
            {
                if (ObjQuotedModel.CheckState == 2)
                {
                    lblCheckNode.Visible = true;            //审核状态(只有通过之后才会显示 提示审核通过)
                }
                IsVisible();
            }


            lblFinishAmount.Text = ObjQuotedModel.FinishAmount.ToString();
            lblCostAmount.Text = ObjQuotedModel.CostFinishAmount == null ? "0" : ObjQuotedModel.CostFinishAmount.ToString();
            txtCheckContent.Text = ObjQuotedModel.ChecksContent == null ? "" : ObjQuotedModel.ChecksContent.ToString();

            var ObjForTypeModel = ObjForTypeBLL.GetByOrderID(OrderID, 0);
            if (ObjForTypeModel != null)
            {
                lblFinishAmount.Text = (ObjForTypeModel.PPrice + ObjForTypeModel.WPrice + ObjForTypeModel.OPrice).ToString().ToDecimal().ToString("f2");
            }
            else
            {
                lblFinishAmount.Text = "0.00";
            }

        }
        #endregion

        #region 外围数据绑定 ItemDataBound
        /// <summary>
        /// 内部Repeater绑定
        /// </summary>
        protected void repfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            var ObjItemList = new List<FL_QuotedPriceItems>();

            var DataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                var NewList = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }

            //获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3);
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
        }
        #endregion

        #region 保存分项
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveItem")
            {
                SaveChange();
            }
        }
        #endregion

        #region 保存 SaveChange方法
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveChange()
        {

            for (int P = 0; P < repfirst.Items.Count; P++)
            {
                Repeater ObjrepList = (Repeater)repfirst.Items[P].FindControl("repdatalist");
                FL_QuotedPriceItems ObjItem;
                decimal? ItemSumMoney = 0;
                decimal? itemSumCost = 0;
                //保存主体
                for (int I = 0; I < ObjrepList.Items.Count; I++)
                {
                    ObjItem = ObjQuotedPriceItemsBLL.GetByID(((HiddenField)ObjrepList.Items[I].FindControl("hidePriceKey")).Value.ToInt32());
                    ObjItem.ImageUrl = string.Empty;
                    ObjItem.Requirement = ((TextBox)ObjrepList.Items[I].FindControl("txtRequirement")).Text;
                    ObjItem.Quantity = ((TextBox)ObjrepList.Items[I].FindControl("txtQuantity")).Text.ToInt32();
                    ObjItem.UnitPrice = ((Label)ObjrepList.Items[I].FindControl("lblSalePrice")).Text.ToDecimal().ToString("f2").ToDecimal();
                    ObjItem.Cost = ((TextBox)ObjrepList.Items[I].FindControl("txtCostPrice")).Text.ToDecimal().ToString("f2").ToDecimal();
                    ObjItem.IsChange = false;
                    ObjItem.IsDelete = false;
                    ObjItem.IsSvae = true;
                    ObjItem.Subtotal = (ObjItem.UnitPrice * ObjItem.Quantity).ToString().ToDecimal().ToString("f2").ToDecimal();
                    ObjItem.CostSubTotal = (ObjItem.Cost == null ? 0 : ObjItem.Cost * ObjItem.Quantity).ToString().ToDecimal().ToString("f2").ToDecimal();
                    ItemSumMoney += ObjItem.Subtotal.ToString().ToDecimal().ToString("f2").ToDecimal();
                    itemSumCost += ObjItem.CostSubTotal.ToString().ToDecimal().ToString("f2").ToDecimal();
                    ObjQuotedPriceItemsBLL.Update(ObjItem);
                }
                //保存分项合计
                HiddenField ObjHiddKey = (HiddenField)repfirst.Items[P].FindControl("hideKey");
                ObjItem = ObjQuotedPriceItemsBLL.GetByID(ObjHiddKey.Value.ToInt32());
                ObjItem.ItemSaleAmount = ((TextBox)repfirst.Items[P].FindControl("txtSaleItem")).Text.ToDecimal().ToString("f2").ToDecimal();
                ObjItem.ItemAmount = ItemSumMoney;
                ObjItem.CostItemAmount = ((TextBox)repfirst.Items[P].FindControl("txtCostItem")).Text.ToDecimal().ToString("f2").ToDecimal();
                ObjItem.ItemCost = itemSumCost;
                ObjQuotedPriceItemsBLL.Update(ObjItem);
                ItemSumMoney = 0;

                //更新报价单主体
                var QuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                QuotedModel.CostFinishAmount = ObjQuotedPriceItemsBLL.GetByOrdersID(Request["OrderID"].ToInt32()).Where(C => C.ItemLevel == 3).Sum(C => C.CostSubTotal);
                QuotedModel.ChecksContent = txtCheckContent.Text.Trim().ToString();
                ObjQuotedPriceBLL.Update(QuotedModel);

                //各类价格
                var DataItemList = ObjQuotedPriceItemsBLL.GetByOrdersID(OrderID);     //报价单
                //各类合计的表
                var ObjForTypeModel = ObjForTypeBLL.GetByOrderID(OrderID, 0);
                ObjForTypeModel.PCostPrice = DataItemList.Where(C => C.Type == 1 && C.IsFirstMake == 0).Sum(C => C.CostSubTotal).ToString().ToDecimal().ToString("f2").ToDecimal();            //人员
                ObjForTypeModel.MCostPrice = DataItemList.Where(C => C.Type == 2 && C.IsFirstMake == 0).Sum(C => C.CostSubTotal).ToString().ToDecimal().ToString("f2").ToDecimal();            //物料
                ObjForTypeModel.OCostprice = DataItemList.Where(C => C.Type == 3 && C.IsFirstMake == 0).Sum(C => C.CostSubTotal).ToString().ToDecimal().ToString("f2").ToDecimal();           //其他
                ObjForTypeModel.CostTotal = (ObjForTypeModel.PCostPrice + ObjForTypeModel.MCostPrice + ObjForTypeModel.OCostprice).ToString().ToDecimal().ToString("f2").ToDecimal();
                ObjForTypeBLL.Update(ObjForTypeModel);
            }
            BinderData();
        }
        #endregion

        #region 点击保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SaveChange();
        }
        #endregion

        #region 通过审核
        /// <summary>
        /// 通过审核
        /// </summary>  
        protected void btn_Pass_Click(object sender, EventArgs e)
        {
            SaveChange();
            var QuotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            QuotedModel.CheckState = 2;
            int result = ObjQuotedPriceBLL.Update(QuotedModel);
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("通过审核", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("审核失败,请稍候再试...", Page);
            }
        }
        #endregion

        #region 获取统计金额
        /// <summary>
        /// 统计金额
        /// </summary>
        public decimal GetMoney(int Type, string Category)
        {
            var ObjForTypeModel = ObjForTypeBLL.GetByOrderID(OrderID, 0);
            if (Type == 1)          //1.销售价
            {
                if (Category == "person")
                {
                    return ObjForTypeModel.PPrice.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "material")
                {
                    var DataItemList = ObjQuotedPriceItemsBLL.GetByOrdersID(OrderID);     //报价单
                    ObjForTypeModel.WPrice = DataItemList.Where(C => C.Type == 2).Sum(C => C.Subtotal).ToString().ToDecimal().ToString("f2").ToDecimal();
                    ObjForTypeBLL.Update(ObjForTypeModel);
                    return ObjForTypeModel.WPrice.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "other")
                {
                    return ObjForTypeModel.OPrice.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "all")
                {
                    return (ObjForTypeModel.PPrice + ObjForTypeModel.WPrice + ObjForTypeModel.OPrice).ToString().ToDecimal().ToString("f2").ToDecimal();
                }
            }
            else if (Type == 2)          //2.成本
            {
                if (Category == "person")
                {
                    return ObjForTypeModel.PCostPrice.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "material")
                {
                    return ObjForTypeModel.MCostPrice.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "other")
                {
                    return ObjForTypeModel.OCostprice.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "all")
                {
                    return ObjForTypeModel.CostTotal.ToString().ToDecimal().ToString("f2").ToDecimal();
                }
            }
            else if (Type == 3)          //3.毛利
            {
                if (Category == "person")
                {
                    return (GetMoney(1, Category).ToString().ToDecimal() - GetMoney(2, Category).ToString().ToDecimal()).ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "material")
                {
                    return (GetMoney(1, Category).ToString().ToDecimal() - GetMoney(2, Category).ToString().ToDecimal()).ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "other")
                {
                    return (GetMoney(1, Category).ToString().ToDecimal() - GetMoney(2, Category).ToString().ToDecimal()).ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                else if (Category == "all")
                {
                    return (GetMoney(1, Category).ToString().ToDecimal() - GetMoney(2, Category).ToString().ToDecimal()).ToString().ToDecimal().ToString("f2").ToDecimal();
                }
                return 0;
            }
            return 0;
        }
        #endregion

        #region 获取毛利率
        /// <summary>
        /// 毛利率
        /// </summary>
        public string GetRates(string Category)
        {
            if (GetMoney(1, Category) != 0)
            {
                return (GetMoney(3, Category).ToString().ToDecimal() / GetMoney(1, Category).ToString().ToDecimal()).ToString("0.00%");
            }

            return "0.00%";

        }
        #endregion

        #region 相应的隐藏功能
        /// <summary>
        /// 隐藏
        /// </summary>
        public void IsVisible()
        {

            for (int i = 0; i < repfirst.Items.Count; i++)
            {
                (repfirst.Items[i].FindControl("btnSaveItem") as Button).Visible = false;
                Repeater repDataList = repfirst.Items[i].FindControl("repdatalist") as Repeater;
                for (int j = 0; j < repDataList.Items.Count; j++)
                {
                    (repDataList.Items[j].FindControl("txtCostPrice") as TextBox).Enabled = false;
                }
            }

            txtCheckContent.Enabled = false;
            btn_Save.Visible = false;
            //btn_Pass.Visible = false;

        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int Type)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            if (Type == 1)
            {
                HandleModel.HandleContent = "策划报价-派工明细,客户姓名:" + Model.Bride + "/" + Model.Groom + ",打回订单";
            }
            else
            {
                HandleModel.HandleContent = "策划报价-派工明细,客户姓名:" + Model.Bride + "/" + Model.Groom + ",确认派工";
            }
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 3;     //策划报价
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 页面状态

        /// <summary>
        /// 隐藏选择产品
        /// </summary>
        /// <returns></returns>
        public string HideSelectProduct(object Level)
        {
            //return string.Empty;
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


        public string GetAvailableCount(object productid, object rowType)
        {
            return Convert.ToInt32(rowType) == 2 ? storehouseSourceProductBLL.GetAvailableCount(Convert.ToInt32(productid), Convert.ToInt32(Request["CustomerID"])).ToString() : string.Empty;
        }



        #endregion

        #region 注释   确认派工/打回订单

        #region 点击确认派工按钮
        /// <summary>
        /// 确认
        /// </summary>
        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            SaveChange();
            QuotedID = Request["QuotedID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            //Report ObjReportBLL = new Report();
            //var ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID, User.Identity.Name.ToInt32());    //修改签约时间
            //ObjReportModel.QuotedDateSucessDate = DateTime.Now;
            //ObjReportBLL.Update(ObjReportModel);

            //操作日志
            CreateHandle(2);        //1.代表打回订单 2.代表确认派工

            string UrlPar = "QuotedID=" + QuotedID + "&OrderID=" + OrderID + "&CustomerID=" + CustomerID;
            JavaScriptTools.AlertWindowAndLocation("保存成功,请选择总调度!", "/AdminPanlWorkArea/QuotedPrice/QuotedPriceSplit/QuotedPriceFinishNextPage.aspx?" + UrlPar, Page);
        }
        #endregion

        #region 打回订单
        /// <summary>
        /// 订单打回
        /// </summary>
        protected void btn_BackOrder_Click(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            ///修改客户状态
            var ObjCustomerModel = ObjCustomerBLL.GetByID(CustomerID);
            ObjCustomerModel.State = (int)CustomerStates.SucessOrder;
            ObjCustomerBLL.Update(ObjCustomerModel);

            //还原订单信息
            FL_QuotedPrice ObjQuotePriceModel = ObjQuotedPriceBLL.GetByQuotedID(QuotedID);
            ObjQuotePriceModel.IsChecks = false;        //未审核
            ObjQuotePriceModel.IsDispatching = 0;       //相当于状态()
            ObjQuotePriceModel.FinishAmount = 0;        //由于日报表原因 所以修改价格
            ObjQuotePriceModel.RealAmount = 0;
            ObjQuotedPriceBLL.Update(ObjQuotePriceModel);

            //修改下单时间为null（或者为空）
            Report ObjReportBLL = new Report();
            SS_Report ObjReportModel = ObjReportBLL.GetByCustomerID(CustomerID);
            WorkReport ObjWorkRportBLL = new WorkReport();
            //修改日报表
            var ObjWorkReportModel = ObjWorkRportBLL.GetEntityByTimeCustomerID(User.Identity.Name.ToInt32(), ObjReportModel.QuotedDateSucessDate.ToString().ToDateTime().ToString("yyyy-MM-dd").ToDateTime());
            ObjWorkReportModel.OrderAmount = 0;
            ObjWorkReportModel.QuotedCheckNum = 0;
            ObjWorkRportBLL.Update(ObjWorkReportModel);

            ObjReportModel.QuotedDateSucessDate = null;
            ObjReportBLL.Update(ObjReportModel);

            //操作日志
            CreateHandle(1);

            JavaScriptTools.AlertWindowAndLocation("打回订单成功", "QuotedPriceListCreateEdit.aspx?SaleEmployee=" + string.Empty + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&CustomerID=" + CustomerID + "&PartyDate=" + ObjQuotedPriceBLL.GetByViewQuotedID(QuotedID).PartyDate, Page);


        }
        #endregion

        #endregion

    }
}