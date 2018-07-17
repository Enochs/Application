using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class OrderDirectCostCreate : SystemPage
    {
        int DispatchingID = 0;
        int EmployeeID = 0;
        int CustomerID = 0;
        int OrderID = 0;
        int CostKey = 0;


        /// <summary>
        /// 获取产品的价格
        /// </summary>
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        Cost ObjCostBLL = new Cost();

        /// <summary>
        /// 成本
        /// </summary>
        CostSum ObjCostSumBLL = new CostSum();

        /// <summary>
        /// 获取各类的价格
        /// </summary>
        QuotedPriceForType ObjForTypeBLL = new QuotedPriceForType();


        DispathingSatisfaction ObjSatisfictionBLL = new DispathingSatisfaction();       //满意度

        /// <summary>
        /// 派工
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();

        /// <summary>
        /// 设计ad
        /// </summary>
        Designclass ObjDesignClassBLL = new Designclass();

        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomerBLL = new Customers();


        ///订单成本明细
        OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();

        /// <summary>
        /// 结算表
        /// </summary>
        Statement ObjStatementBLL = new Statement();

        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 供应商
        /// </summary>
        Supplier ObjSupplierBLL = new Supplier();

        /// <summary>
        /// 供应商类型
        /// </summary>
        SupplierType ObjSupplierTypeBLL = new SupplierType();

        /// <summary>
        /// 四大金刚
        /// </summary>
        FourGuardian ObjFourGuardianBLL = new FourGuardian();

        HA.PMS.BLLAssmblly.Flow.OrderAppraise ObjOrderAppraiseBLL = new HA.PMS.BLLAssmblly.Flow.OrderAppraise();

        /// <summary>
        /// 报价单审核
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        DispatchingEmployeeManager ObjDispatchingEmployeeManagerBLL = new DispatchingEmployeeManager();

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();

            if (!IsPostBack)
            {

                //策划报价
                var QuotedModel = ObjQuotedPriceBLL.GetOnlyFirstByOrderID(OrderID);
                if (QuotedModel != null)
                {
                    hideTotal.Value = QuotedModel.FinishAmount.ToString();
                    txtTotal.Text = QuotedModel.FinishAmount.ToString();
                    if (hideTotal.Value == "")
                    {
                        hideTotal.Value = "1";
                    }
                }
                //销售费用
                SaveSaleCost();
                //修改设计清单

                var ObjQuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
                if (ObjQuotedModel != null && ObjQuotedModel.DesignerEmployee != null)
                {
                    ModifyOrAddDesigner(ObjQuotedModel.DesignerEmployee.ToString().ToInt32());
                }
                //数据绑定
                BinderData();


                //var ObjUpdateModel = ObjCostBLL.GetByOrderID(OrderID);
                //ObjUpdateModel.CustomerID = CustomerID;
                //ObjCostBLL.Update(ObjUpdateModel);

            }
        }
        #endregion

        #region MyRegion

        public string GetEmpLoyeeAppraise(object EmoployeeID)
        {
            var ObjModel = ObjOrderAppraiseBLL.GetByEmployeeID(EmoployeeID.ToString().ToInt32(), OrderID);
            if (ObjModel != null)
            {
                return ObjModel.PointTitle;
            }
            return string.Empty;
        }

        public class KeyClassEquers : IEqualityComparer<KeyClass>
        {

            public bool Equals(KeyClass x, KeyClass y)
            {
                if (x.Key == y.Key && x.KeyName == y.KeyName)
                    return true;
                else
                    return false;
            }
            public int GetHashCode(KeyClass obj)
            {
                return 0;
            }


        }
        public class KeyClass
        {
            public int Key { get; set; }

            public string KeyName { get; set; }
        }


        ///// <summary>
        ///// 根据类型ID获取类型名称
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <returns></returns>
        //public string GetProductByID(object Key)
        //{
        //    if (Key != null)
        //    {
        //        return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}


        ///// <summary>
        ///// 根据类型ID获取类型名称
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <returns></returns>
        //public string GetCategoryByID(object Key)
        //{
        //    if (Key != null)
        //    {
        //        return OjbCategoryBLL.GetByID(Key.ToString().ToInt32()).Title;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}



        public string GetBorderStyle(object IsNewAdd)
        {
            if (IsNewAdd != null)
            {
                if (IsNewAdd.ToString() != string.Empty && IsNewAdd.ToString() == "True")
                {
                    return "style='background-color:#c7face;'";
                }
            }

            return string.Empty;
        }

        #endregion

        #region 数据绑定


        private void BinderData()
        {
            var DataList = ObjCostSumBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());

            var ObjDataList = ObjOrderfinalCostBLL.GetByCustomerID(CustomerID);

            //供应商成本 来自供应商明细产品表

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7));  //执行团队 5  内部人员 4  手动添加  7

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 8 || C.RowType == 10).OrderBy(C => C.CostSumId));        //物料 1      库房 2      新购买 3    系统默认添加的设计师 6   手动添加 8  设计单 10

            //rptDesignClass.DataBind(DataList.Where(C => C.RowType == 10));              //设计单

            repOtherCost.DataBind(DataList.Where(C => C.RowType == 9 || C.RowType == 11));  //其他

            repSaleMoney.DataBind(DataList.Where(C => C.RowType == 12));            //销售成本

            List<FL_DispathingSatisfaction> ObjSacList = ObjSatisfictionBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());      //总体评价
            if (ObjSacList.Count > 0)           //存在总评
            {
                rptSatisfaction.DataBind(ObjSacList);
            }
            else            //有可能没有总评  就需要自己创建
            {
                Insert();
            }

            string DesignFinishAmount = DataList.Where(C => C.RowType == 10).Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString("f2");          //设计清单金额
            txtCost.Text = DataList.Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString("f2");
            txtMaoLi.Text = (txtTotal.Text.ToDecimal() - txtCost.Text.ToDecimal()).ToString();
            if (txtTotal.Text.ToDecimal() > 0)
            {
                txtProfitMargin.Text = (txtMaoLi.Text.ToDecimal() / txtTotal.Text.ToDecimal()).ToString("0.00%").ToString();
            }

            //人员 物料 其他 销售价 成本价 毛利率
            GetFinishAmount(3);     //销售价
            GetCostAmounts(3);      //成本价
            GetProfitRate();        //毛利率

            //隐藏删除
            DeleteIsVisible(repEmployeeCost, 1);      //人员
            DeleteIsVisible(repSupplierCost, 2);      //物料
            DeleteIsVisible(repOtherCost, 3);         //其他

        }
        #endregion

        #region 总评价     如果不存在就新增
        public void Insert()
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            FL_DispathingSatisfaction SacModel = new FL_DispathingSatisfaction();
            SacModel.DispatchingID = DispatchingID;
            SacModel.SaEvaluationId = 6;
            SacModel.SatisfactionName = "总体满意度";
            SacModel.SatisfactionContent = "";
            SacModel.SatisfactionRemark = "";

            SacModel.EvaluationId = 6;
            SacModel.EvaluationName = "总体评价";
            SacModel.EvaluationContent = "";
            SacModel.EvaluationRemark = "";

            ObjSatisfictionBLL.Insert(SacModel);
        }
        #endregion

        #region 确认成本明细
        /// <summary>
        /// 确认成本明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            var ObjUpdateModel = ObjCostBLL.GetByOrderID(OrderID);
            ObjUpdateModel.IsLock = true;
            ObjCostBLL.Update(ObjUpdateModel);

            //保存操作日志
            CreateHandle();
            //SaveeRepItem(repOther);
            JavaScriptTools.AlertWindowAndLocation("确认成功!", "OrderDirectCostShow.aspx?OrderID=" + OrderID + "&DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID, Page);
        }
        #endregion

        #region 新增  酒店佣金  税金( 销售费用)
        protected void SaveSaleCost()
        {

            List<string> ObjSaleList = new List<string>();
            ObjSaleList.Add("酒店佣金");
            ObjSaleList.Add("税金");

            foreach (var Objitem in ObjSaleList)
            {
                var Model = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, Objitem);
                if (Model == null)
                {
                    ObjCostSumBLL.Insert(new DataAssmblly.FL_CostSum()
                    {
                        Name = Objitem,
                        CategoryName = Objitem,
                        Content = Objitem,
                        Sumtotal = 0,
                        ActualSumTotal = 0,
                        OrderID = Request["OrderID"].ToInt32(),
                        QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32()),
                        CustomerId = Request["CustomerID"].ToInt32(),
                        DispatchingID = Request["DispatchingID"].ToInt32(),
                        RowType = 12,
                        Evaluation = 6,
                        Advance = "",
                        ShortCome = "",
                        CreateDate = DateTime.Now,
                        EmployeeID = User.Identity.Name.ToInt32(),
                        Remark = ""
                    });
                }
            }
            BinderData();
        }
        #endregion

        #region 删除功能
        /// <summary>
        /// 删除
        /// </summary>    
        protected void repOtherCost_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            FL_CostSum ObjCostSumModel = ObjCostSumBLL.GetByID(e.CommandArgument.ToString().ToInt32());     //查到数据
            ObjCostSumBLL.Delete(ObjCostSumModel);              //从成本表中进行删除

            BinderData();
            JavaScriptTools.AlertWindow("删除成功!", Page);
        }
        #endregion

        #region 通用修改
        public void UpdateCost(Repeater rptItem)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                Label lblSumtotal = currentItem.FindControl("lblPlanSumtotal") as Label;
                TextBox txtActualSumtotal = currentItem.FindControl("txtActualSumtotal") as TextBox;
                TextBox txtRemark = currentItem.FindControl("txtRemark") as TextBox;
                TextBox txtPayMent = currentItem.FindControl("txtPayMent") as TextBox;
                Label lblNoPayMent = currentItem.FindControl("lblNoPayMent") as Label;
                HiddenField CostSumId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = CostSumId.Value.ToInt32();
                FL_CostSum CostSumModel = ObjCostSumBLL.GetByID(value);


                if (CostSumModel.RowType == 6 || CostSumModel.RowType == 9)
                {
                    Label lblContent = currentItem.FindControl("lblContent") as Label;
                    CostSumModel.Content = lblContent.Text.ToString();
                }
                if (txtPayMent.Text == "")
                {
                    JavaScriptTools.AlertWindow("支付金额不能为空", Page);
                    return;
                }
                else if (txtPayMent.Text.ToDecimal() < 0)
                {
                    JavaScriptTools.AlertWindow("输入金额不能小于0", Page);
                    return;
                }
                else if (txtPayMent.Text.ToDecimal() > txtActualSumtotal.Text.ToDecimal())
                {
                    JavaScriptTools.AlertWindow("支付金额不能大于未支付金额", Page);
                    return;
                }

                CostSumModel.Sumtotal = lblSumtotal.Text.ToDecimal();
                CostSumModel.ActualSumTotal = txtActualSumtotal.Text.ToDecimal();
                CostSumModel.PayMent = txtPayMent.Text.Trim().ToDecimal();
                CostSumModel.NoPayMent = (CostSumModel.ActualSumTotal - CostSumModel.PayMent).ToString().ToDecimal();
                if (rptItem != repSaleMoney)        //销售费用 没有 优点缺点
                {

                }
                else
                {
                    Label lblCategoryName = currentItem.FindControl("lblCategoryName") as Label;
                    CostSumModel.Content = lblCategoryName.Text.Trim().ToString();
                    CostSumModel.CategoryName = lblCategoryName.Text.Trim().ToString();
                }
                CostSumModel.State = 2;
                CostSumModel.Remark = txtRemark.Text.Trim().ToString();
                CostSumModel.OrderID = Request["OrderID"].ToInt32();
                CostSumModel.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
                CostSumModel.CustomerId = Request["CustomerID"].ToInt32();
                CostSumModel.DispatchingID = Request["DispatchingID"].ToInt32();
                if (txtRemark != null)
                {
                    CostSumModel.Remark = txtRemark.Text == null ? "" : txtRemark.Text.ToString();
                }
                ObjCostSumBLL.Update(CostSumModel);



                var StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, CostSumModel.Name);
                if (StatementModel != null)
                {
                    StatementModel.PrePayMent = txtPayMent.Text.ToString().ToDecimal();
                    StatementModel.NoPayMent = (StatementModel.SumTotal - StatementModel.PayMent - StatementModel.PrePayMent).ToString().ToDecimal();
                    ObjStatementBLL.Update(StatementModel);
                }
                else
                {
                    InsertStatement();
                }
            }

        }
        #endregion

        #region 点击 统一全部保存

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            UpdateCost(repEmployeeCost);        //人员
            UpdateCost(repSupplierCost);        //物料
            UpdateCost(repOtherCost);           //其他
            UpdateCost(repSaleMoney);         //销售费用

            //成本表
            var ObjUpdateModel = ObjCostBLL.GetByOrderID(OrderID);
            if (ObjUpdateModel != null)
            {

                ObjUpdateModel.OrderID = Request["OrderID"].ToInt32();
                ObjUpdateModel.Cost = txtCost.Text.ToDecimal();
                ObjUpdateModel.CustomerID = CustomerID;
                ObjUpdateModel.ProfitMargin = txtProfitMargin.Text.Replace("%", "").ToDecimal();
                ObjUpdateModel.TotalAmount = hideTotal.Value.ToDecimal();
                ObjCostBLL.Update(ObjUpdateModel);
            }
            else
            {
                ObjUpdateModel = new FL_Cost();
                ObjUpdateModel.OrderID = Request["OrderID"].ToInt32();
                ObjUpdateModel.Cost = txtCost.Text.ToDecimal();
                ObjUpdateModel.CustomerID = CustomerID;
                ObjUpdateModel.CreateEmpLoyee = GetQuotedEmployee(CustomerID).ToInt32();
                ObjUpdateModel.CreateDate = DateTime.Now;
                ObjUpdateModel.ProfitMargin = txtProfitMargin.Text.Replace("%", "").ToDecimal();
                ObjUpdateModel.TotalAmount = hideTotal.Value.ToDecimal();
                ObjCostBLL.Insert(ObjUpdateModel);
            }

            BinderData();
            JavaScriptTools.AlertWindow("保存完毕", Page);
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
                        ObjStatementModel.TypeName = ObjSupplierTypeBLL.GetByID(ObjStatementModel.TypeID).TypeName;
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
                            var Model = ObjFourGuardianBLL.GetByName(item.Name);
                            if (Model != null)
                            {
                                ObjStatementModel.SupplierID = ObjFourGuardianBLL.GetByName(item.Name).GuardianId;
                                ObjStatementModel.RowType = 4;
                            }
                            else
                            {
                                ObjStatementModel.SupplierID = 0;
                                ObjStatementModel.RowType = 5;      //分到人员中   属于外部人员
                            }
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
                        if (PersonModels == null)           //不是内部人员
                        {
                            if (ObjSupplierBLL.GetByName(item.Name) != null)        //是供应商
                            {
                                ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                                ObjStatementModel.RowType = 1;
                            }
                            else            //手动录入
                            {
                                ObjStatementModel.SupplierID = 0;
                                ObjStatementModel.RowType = 5;
                            }
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
                ObjStatementModel.PrePayMent = item.PayMent;
                ObjStatementModel.NoPayMent = item.ActualSumTotal - item.PayMent;
                ObjStatementModel.CostSumId = item.CostSumId;
                ObjStatementModel.Year = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Year;
                ObjStatementModel.Month = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Month;


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


                    StatementModel.PrePayMent = ObjStatementModel.PrePayMent;         //已付款
                    StatementModel.NoPayMent = StatementModel.SumTotal - StatementModel.PrePayMent;
                    ObjStatementBLL.Update(StatementModel);                     //修改更新
                }
                else
                {
                    if (item.RowType != 12)
                    {
                        ObjStatementBLL.Insert(ObjStatementModel);
                    }
                }

                #endregion
            }
        }
        #endregion

        #region 获取人员 物料 其他的销售价
        /// <summary>
        /// Type类型 1.人员  2.物料 3.其他
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="TypeSource"></param>
        /// <returns></returns>

        public void GetFinishAmount(int Type)
        {
            decimal FinishAmount = 0;

            int OrderID = Request["OrderID"].ToInt32();
            var ObjQuotedPriceList = ObjForTypeBLL.GetByOrderID(OrderID);

            if (ObjQuotedPriceList.Count > 0)       //单独存人员 物料 其他价格的表有数据  直接读取显示
            {
                for (int i = 1; i <= Type; i++)
                {
                    if (i == 1)
                    {
                        lblPersonSale.Text = ObjQuotedPriceList.Sum(C => C.PPrice).ToString().ToDecimal().ToString("f2");
                        FinishAmount = ObjQuotedPriceList.Sum(C => C.PPrice).ToString().ToDecimal();
                    }
                    else if (i == 2)
                    {
                        lblMaterialSale.Text = ObjQuotedPriceList.Sum(C => C.MPrice).ToString().ToDecimal().ToString("f2");
                        FinishAmount = ObjQuotedPriceList.Sum(C => C.MPrice).ToString().ToDecimal();
                    }
                    else if (i == 3)
                    {
                        lblQuotedOtherSale.Text = ObjQuotedPriceList.Sum(C => C.OPrice).ToString().ToDecimal().ToString("f2");
                        FinishAmount = ObjQuotedPriceList.Sum(C => C.OPrice).ToString().ToDecimal();
                    }
                }
            }
            else            //如果不存在  就获取产品表的各个产品的总和
            {
                FinishAmount = ObjQuotedPriceItemsBLL.GetByOrdersID(OrderID).Where(C => C.Type == Type).Sum(C => C.Subtotal).ToString().ToDecimal(); ;
            }
        }
        #endregion

        #region 获取成本价
        /// <summary>
        /// 成本价
        /// </summary>
        public void GetCostAmounts(int Type)
        {
            decimal CostAmount = 0;

            int CustomerID = Request["CustomerID"].ToInt32();
            var DataList = ObjCostSumBLL.GetByCustomerID(CustomerID);

            for (int i = 1; i <= Type; i++)
            {
                if (i == 1)                  //人员
                {
                    CostAmount = DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
                    lblPersonCost.Text = CostAmount.ToString("f2");
                }
                else if (i == 2)             //物料
                {
                    CostAmount = DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 10 || C.RowType == 8).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
                    lblMaterialCost.Text = CostAmount.ToString("f2");
                }
                else if (i == 3)             //其他
                {
                    CostAmount = DataList.Where(C => C.RowType == 11 || C.RowType == 9).Sum(C => C.ActualSumTotal).ToString().ToDecimal();
                    lblQuotedOtherCost.Text = CostAmount.ToString("f2");
                }
            }

        }
        #endregion

        #region 获取毛利率
        /// <summary>
        /// 成本价
        /// </summary>
        public void GetProfitRate()
        {
            //人员毛利率
            if (lblPersonSale.Text.ToDecimal() > 0)
            {
                lblPersonRate.Text = ((lblPersonSale.Text.ToDecimal() - lblPersonCost.Text.ToDecimal()) / lblPersonSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblMaterialRate.Text = "0.00%";
            }

            //物料毛利率
            if (lblMaterialSale.Text.ToDecimal() > 0)
            {
                lblMaterialRate.Text = ((lblMaterialSale.Text.ToDecimal() - lblMaterialCost.Text.ToDecimal()) / lblMaterialSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblMaterialRate.Text = "0.00%";
            }

            //其他毛利率
            if (lblQuotedOtherSale.Text.ToDecimal() > 0)
            {
                lblQuotedOtherRate.Text = ((lblQuotedOtherSale.Text.ToDecimal() - lblQuotedOtherCost.Text.ToDecimal()) / lblQuotedOtherSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblQuotedOtherRate.Text = "0.00%";
            }
        }
        #endregion

        #region 手动 添加 人员  物料 其他成本

        public void InsertCostSum(string Type, string SaveType)
        {
            FL_CostSum CostSum = new FL_CostSum();
            if (Type == "Add")
            {
                CostSum = new FL_CostSum();

                CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
                CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                CostSum.ShortCome = "";
                CostSum.Advance = "";
                CostSum.OrderID = Request["OrderID"].ToInt32();
                CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
                CostSum.CustomerId = Request["CustomerID"].ToInt32();

                if (SaveType == "Person")           //人员
                {
                    if (ddlPersonType.SelectedItem.Text == "内部人员")
                    {
                        CostSum.RowType = 7;
                        CostSum.Name = Text1.Text.Trim().ToString();
                    }
                    else if (ddlPersonType.SelectedItem.Text == "四大金刚")
                    {
                        CostSum.RowType = 7;
                        CostSum.Name = txtSuppName.Text.Trim().ToString();
                    }
                    else if (ddlPersonType.SelectedItem.Text == "手动录入")
                    {
                        CostSum.RowType = 5;
                        CostSum.Name = txtPersonName.Text.Trim().ToString();
                    }

                    CostSum.Content = txtContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtSumtotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtSumtotal.Text.Trim().ToString().ToDecimal();
                }
                else if (SaveType == "Material")            //物料
                {
                    if (ddlPersonTypes.SelectedItem.Text == "内部人员")
                    {
                        CostSum.RowType = 8;
                        CostSum.Name = txtInPerson.Text.Trim().ToString();
                    }
                    else if (ddlPersonTypes.SelectedItem.Text == "供应商")
                    {
                        CostSum.RowType = 8;
                        CostSum.Name = txtSupplier.Text.Trim().ToString();
                    }
                    else if (ddlPersonTypes.SelectedItem.Text == "手动录入")
                    {
                        CostSum.RowType = 5;
                        CostSum.Name = txtMaterial.Text.Trim().ToString();
                    }


                    CostSum.Content = txtMaterialContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtMaterialContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtMaterialSumTotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtMaterialSumTotal.Text.Trim().ToString().ToDecimal();
                }
                else if (SaveType == "Other")               //其他
                {
                    if (ddlOtherType.SelectedItem.Text == "内部人员")
                    {
                        CostSum.Name = txtOtherEmployee.Text.Trim().ToString();
                    }
                    else if (ddlOtherType.SelectedItem.Text == "供应商")
                    {
                        CostSum.Name = txtSuppliers.Text.Trim().ToString();
                    }
                    CostSum.RowType = 9;
                    CostSum.Content = txtOtherContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtOtherContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtOtherSumTotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtOtherSumTotal.Text.Trim().ToString().ToDecimal();
                }
                else if (SaveType == "Sale")                //销售费用
                {
                    CostSum.Name = txtSaleTitle.Text.Trim().ToString();
                    CostSum.RowType = 12;
                    CostSum.Content = txtSaleContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtSaleContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtSaleSumTotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtSaleSumTotal.Text.Trim().ToString().ToDecimal();
                }

                CostSum.PayMent = 0;
                CostSum.NoPayMent = CostSum.ActualSumTotal;
                CostSum.Evaluation = 6;
                CostSum.EmployeeID = User.Identity.Name.ToInt32();
                CostSum.Remark = "";
                ObjCostSumBLL.Insert(CostSum);
            }
        }
        #endregion

        #region 保存人员 物料 其他  手动添加(点击保存按钮)
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button btnSave = (sender as Button);
            if (btnSave.ID == "btnSave")
            {
                InsertCostSum("Add", "Person");
            }
            else if (btnSave.ID == "btnSaveMaterial")
            {
                InsertCostSum("Add", "Material");
            }
            else if (btnSave.ID == "btnSaveOther")
            {
                InsertCostSum("Add", "Other");
            }
            else if (btnSave.ID == "btnSaveSaleTitle")
            {
                InsertCostSum("Add", "Sale");
            }
            InsertStatement();
            Response.Redirect(this.Page.Request.Url.ToString());


        }
        #endregion

        #region 保存 读取 四大金刚名称(隐藏)
        /// <summary>
        /// 保存
        /// </summary>  
        protected void btnFourGuardianSave_Click(object sender, EventArgs e)
        {
            int GuraianID = hideSuppID.Value.ToInt32();
            var Model = ObjFourGuardianBLL.GetByID(GuraianID);
            if (Model != null)
            {
                txtSuppName.Text = Model.GuardianName.ToString();
            }
        }
        #endregion

        #region 保存 读取 供应商(隐藏)
        /// <summary>
        /// 保存供应商
        /// </summary>
        protected void btnSavesupperSave_Click(object sender, EventArgs e)
        {
            int SupplierID = HideSupplier.Value.ToInt32();
            var Model = ObjSupplierBLL.GetByID(SupplierID);
            if (Model != null)
            {
                txtSupplier.Text = Model.Name.Trim().ToString();
            }
        }
        #endregion

        #region 修改或增加  设计清单 合为一条数据
        /// <summary>
        /// 设计清单的修改/增加
        /// </summary>
        public void ModifyOrAddDesigner(int Designer = 1)
        {
            //设计清单 10
            FL_CostSum CostSum = new FL_CostSum();
            var DataList = ObjCostSumBLL.GetCostSumForDesigner(Request["CustomerID"].ToInt32(), "");
            foreach (var item in DataList)
            {
                CostSum = new FL_CostSum();
                string name = GetSupplierName(item.Supplier);
                int DisID = Request["DispatchingID"].ToInt32();
                var Model = ObjCostSumBLL.GetByCheckID(name, DisID, 10);
                if (Model == null)
                {
                    CostSum.Name = GetSupplierName(item.Supplier);
                    CostSum.Content = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                    CostSum.CategoryName = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                    CostSum.Sumtotal = ObjDesignClassBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.TotalPrice).ToString().ToDecimal(); ;
                    CostSum.ActualSumTotal = ObjDesignClassBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                    CostSum.PayMent = 0;
                    CostSum.NoPayMent = CostSum.ActualSumTotal;
                    CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
                    CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                    CostSum.ShortCome = "";
                    CostSum.Advance = "";
                    CostSum.OrderID = Request["OrderID"].ToInt32();
                    CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
                    CostSum.CustomerId = Request["CustomerID"].ToInt32();
                    CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
                    CostSum.RowType = 10;
                    CostSum.Evaluation = 6;
                    CostSum.EmployeeID = User.Identity.Name.ToInt32();
                    ObjCostSumBLL.Insert(CostSum);
                }
                else
                {
                    if (Model.State != 2)           //不等于2   就没有手动修改过 可以自动生成
                    {
                        Model.Name = GetSupplierName(item.Supplier);
                        Model.Content = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                        Model.CategoryName = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                        Model.Sumtotal = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                        Model.ActualSumTotal = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                        Model.DispatchingID = Request["DispatchingID"].ToInt32();
                        ObjCostSumBLL.Update(Model);
                    }
                }
            }

            if (Designer != 1)
            {
                var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
                var CostSumModel = ObjCostSumBLL.GetByCheckID(GetEmployeeName(Designer), Request["DispatchingID"].ToInt32(), 6);
                if (CostSumModel != null && QuotedModel.CheckState != 3)
                {
                    //成本修改
                    //CostSumModel.Name = GetEmployeeName(Designer);
                    //CostSumModel.RowType = 6;
                    //CostSumModel.Content = "执行设计(后期设计)";
                    //CostSumModel.CategoryName = "执行设计(后期设计)";
                    //CostSumModel.Sumtotal = ((ObjForTypeBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.MPrice).ToString().ToDecimal()) / 100).ToString().ToDecimal();
                    //CostSumModel.ActualSumTotal = ((ObjForTypeBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.MPrice).ToString().ToDecimal()) / 100).ToString().ToDecimal();
                    //ObjCostSumBLL.Update(CostSumModel);

                    //报价单 审核状态
                    QuotedModel.CheckState = 3;
                    ObjQuotedPriceBLL.Update(QuotedModel);

                }
            }
            BinderData();

        }
        #endregion

        #region 获取供应商名称(设计清单)
        /// <summary>
        /// 获取供应商名称
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public string GetSupplierName(object Source)
        {
            int SupplierID = Source.ToString().ToInt32();
            var Model = ObjSupplierBLL.GetByID(SupplierID);
            if (Model != null)
            {
                return Model.Name;
            }
            return "";
        }
        #endregion

        #region 获取设计单的各项名称
        /// <summary>
        /// 获取名称
        /// </summary>
        public string GetTitle(int CustomerID, int Supplier)
        {
            var DataList = ObjDesignClassBLL.GetByCustomerId(CustomerID).Where(C => C.Supplier.ToInt32() == Supplier).ToList();
            string titles = "";
            int index = 0;
            foreach (var item in DataList)
            {
                if (DataList.Count == index)
                {
                    titles += item.Title;
                }
                else
                {
                    titles += item.Title + ",";
                    index++;
                }
            }
            return titles;
        }
        #endregion

        #region 判断三大类(人员 物料 其他)是否有数据

        public string IsHide(object Source)
        {
            int Type = Source.ToString().ToInt32();
            var DataList = ObjCostSumBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());
            List<FL_CostSum> GetList = new List<FL_CostSum>();
            if (Type == 1)
            {
                if (DataList != null)
                {
                    GetList = DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 3).ToList();
                }
            }
            else if (Type == 2)
            {
                if (DataList != null)
                {
                    GetList = DataList.Where(C => C.RowType == 11).ToList();
                }
            }
            else if (Type == 3)
            {
                if (DataList != null)
                {
                    GetList = DataList.Where(C => C.RowType == 11).ToList();
                }
            }

            if (GetList.Count == 0 || GetList == null)
            {
                return "style='display:none;'";
            }
            return "";

        }

        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            var Model = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "财务管理,客户姓名:" + Model.Bride + "/" + Model.Groom + ",确认成本明细！";
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 7;     //财务管理
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 隐藏删除
        /// <summary>
        /// 隐藏删除
        /// </summary>  
        public void DeleteIsVisible(Repeater rep, int Type)
        {
            for (int i = 0; i < rep.Items.Count; i++)
            {
                var ObjItem = rep.Items[i];
                int RowType = (ObjItem.FindControl("HideRowType") as HiddenField).Value.ToString().ToInt32();
                //int EvalState = GetEvalState(Request["CustomerID"].ToInt32());
                if (Type == 1)
                {
                    if (RowType != 7)           //不是手动添加的人员和已评价的   就不能删除 lbtnDelete
                    {
                        (ObjItem.FindControl("lbtnDelete") as LinkButton).Visible = false;
                    }
                }
                else if (Type == 2)
                {
                    if (RowType != 8)           //不是手动添加的物料和已评价的    就不能删除
                    {
                        (ObjItem.FindControl("lbtnDelete") as LinkButton).Visible = false;
                    }
                }
                else if (Type == 3)
                {
                    if (RowType != 9)           //不是手动添加的其它和已评价的    就不能删除
                    {
                        (ObjItem.FindControl("lbtnDelete") as LinkButton).Visible = false;
                    }
                }
            }
        }
        #endregion

        #region 获取已支付或未支付的金额
        /// <summary>
        /// 获取支付的金额
        /// </summary>
        public string GetPayNoMent(object Source, int Type)
        {
            string name = Source.ToString();
            var DataList = ObjStatementBLL.GetListByDispatchingID(DispatchingID, name);
            if (Type == 1)                  //已支付
            {
                return DataList.Sum(C => C.PayMent).ToString();
            }
            else if (Type == 2)             //未支付
            {
                return DataList.Sum(C => C.NoPayMent).ToString();
            }
            return "";
        }
        #endregion

        #region 获取QuotedID
        /// <summary>
        /// 获取QuotedID
        /// </summary>
        public int GetQuotedID(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                return Model.QuotedID;
            }
            return 0;
        }
        #endregion

        #region 分项保存
        /// <summary>
        /// 分项保存
        /// </summary>B
        protected void btnSavePerson_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.ID == "btnSavePerson")
            {
                UpdateCost(repEmployeeCost);
            }
            else if (btn.ID == "btnSaveMaterials")
            {
                UpdateCost(repSupplierCost);
            }
            else if (btn.ID == "btnSaveOthers")
            {
                UpdateCost(repOtherCost);
            }
            JavaScriptTools.AlertWindow("修改成功", Page);
        }
        #endregion
    }
}