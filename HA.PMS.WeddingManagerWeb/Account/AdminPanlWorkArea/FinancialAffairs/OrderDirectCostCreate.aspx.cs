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


        Cost ObjCostBLL = new Cost();

        CostSum ObjCostSumBLL = new CostSum();

        Employee ObjEmployeeBLL = new Employee();


        Dispatching ObjDispatchingBLL = new Dispatching();

        Designclass ObjDesignClassBLL = new Designclass();
        Customers ObjCustomerBLL = new Customers();


        ///订单成本明细
        OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();

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
                var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);

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
                BinderData();

                var ObjUpdateModel = ObjCostBLL.GetByOrderID(OrderID);
                ObjUpdateModel.CustomerID = CustomerID;
                ObjCostBLL.Update(ObjUpdateModel);



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

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 3));  //执行团队 4

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1));        //供应商 1

            rptStore.DataBind(DataList.Where(C => C.RowType == 2));        //库房 2

            repSaleMoney.DataBind(DataList.Where(C => C.RowType == 6));  // 酒店佣金 顾问提成等 四项

            repBuyCost.DataBind(DataList.Where(C => C.RowType == 7));  //采购物料

            repFlowerCost.DataBind(DataList.Where(C => C.RowType == 8));  //花艺单

            repOther.DataBind(DataList.Where(C => C.RowType == 9));   //其他

            List<FL_Designclass> List = ObjDesignClassBLL.GetByCustomerId(Request["CustomerID"].ToInt32());
            rptDesignClass.DataBind(List);

            txtCost.Text = DataList.Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString();
            txtMaoLi.Text = (txtTotal.Text.ToDecimal() - txtCost.Text.ToDecimal()).ToString();
            if (txtTotal.Text.ToDecimal() > 0)
            {
                txtProfitMargin.Text = (txtMaoLi.Text.ToDecimal() / txtTotal.Text.ToDecimal()).ToString("0.00%").ToString();
            }

        }
        #endregion

        #region 清空
        public void Emptys()
        {
            txtServiceContent.Text = String.Empty;
            txtInsideRemark.Text = String.Empty;
            txtPlannedExpenditure.Text = String.Empty;
            txtActualExpenditure.Text = String.Empty;
            txtAdvances.Text = String.Empty;
            txtShortComes.Text = String.Empty;
        }
        #endregion

        #region 点击 统一全部保存

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            UpdateCost(repEmployeeCost);
            UpdateCost(repSupplierCost);
            UpdateCost(rptStore);
            UpdateCost(repBuyCost);
            UpdateCost(repFlowerCost);
            UpdateCost(repOther);
            UpdateCost(repSaleMoney);
            UpdateDesign();

            var ObjUpdateModel = ObjCostBLL.GetByOrderID(OrderID);
            ObjUpdateModel.Cost = txtCost.Text.ToDecimal();
            ObjUpdateModel.CustomerID = CustomerID;
            ObjUpdateModel.ProfitMargin = txtProfitMargin.Text.Replace("%", "").ToDecimal();
            ObjUpdateModel.TotalAmount = hideTotal.Value.ToDecimal();
            ObjCostBLL.Update(ObjUpdateModel);

            BinderData();
            JavaScriptTools.AlertWindow("保存完毕", Page);

        }
        #endregion

        #region 其他  修改 保存
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RepItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                FL_CostSum ObjCostSumModel = ObjCostSumBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjCostSumBLL.Delete(ObjCostSumModel);
                BinderData();
                JavaScriptTools.AlertWindow("删除成功!", Page);
            }
            else if (e.CommandName == "SaveItem")
            {
                UpdateCost(repOther);
                BinderData();
                JavaScriptTools.AlertWindow("修改成功", Page);
            }
        }
        #endregion

        #region 其他 添加 保存

        /// <summary>
        /// 其他成本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FL_CostSum CostSum = new FL_CostSum();

            CostSum.Name = txtServiceContent.Text;
            CostSum.Content = txtInsideRemark.Text.ToString();
            CostSum.CategoryName = txtInsideRemark.Text.ToString();
            CostSum.Sumtotal = txtPlannedExpenditure.Text.ToDecimal();
            CostSum.ActualSumTotal = txtActualExpenditure.Text.ToDecimal();
            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
            CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            CostSum.ShortCome = txtShortComes.Text.ToString();
            CostSum.Advance = txtAdvances.Text.ToString();
            CostSum.OrderID = Request["OrderID"].ToInt32();
            CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
            CostSum.CustomerId = Request["CustomerID"].ToInt32();
            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
            CostSum.RowType = 9;
            CostSum.Evaluation = 6;
            CostSum.EmployeeID = User.Identity.Name.ToInt32();
            ObjCostSumBLL.Insert(CostSum);
            JavaScriptTools.AlertWindowAndLocation("添加成功!", Page.Request.Url.ToString(), Page);

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


        #region 修改
        public void UpdateCost(Repeater rptItem)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                TextBox txtSumtotal = currentItem.FindControl("txtPlanSumtotal") as TextBox;
                TextBox txtActualSumtotal = currentItem.FindControl("txtActualSumtotal") as TextBox;
                TextBox txtAdvance = currentItem.FindControl("txtAdvance") as TextBox;
                TextBox txtShortCome = currentItem.FindControl("txtShortCome") as TextBox;
                TextBox txtRemark = currentItem.FindControl("txtRemark") as TextBox;
                HiddenField CostSumId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = CostSumId.Value.ToInt32();
                FL_CostSum CostSumModel = ObjCostSumBLL.GetByID(value);


                if (CostSumModel.RowType == 6 || CostSumModel.RowType == 9)
                {
                    TextBox txtContent = currentItem.FindControl("txtContent") as TextBox;
                    CostSumModel.Content = txtContent.Text.ToString();
                }
                CostSumModel.Sumtotal = txtSumtotal.Text.ToDecimal();
                CostSumModel.ActualSumTotal = txtActualSumtotal.Text.ToDecimal();
                CostSumModel.Advance = txtAdvance.Text.ToString();
                CostSumModel.ShortCome = txtShortCome.Text.ToString();
                if (txtRemark != null)
                {
                    CostSumModel.Remark = txtRemark.Text == null ? "" : txtRemark.Text.ToString();
                }
                ObjCostSumBLL.Update(CostSumModel);
            }

        }
        #endregion

        #region 统一修改四项
        protected void btnSaveallsale_Click(object sender, EventArgs e)
        {
            UpdateCost(repSaleMoney);
        }
        #endregion

        #region 新增 四项
        protected void btnSaveSale_Click(object sender, EventArgs e)
        {
            if (repSaleMoney.Items.Count > 0)
            {
                return;
            }
            List<string> ObjSaleList = new List<string>();
            ObjSaleList.Add("酒店佣金");
            ObjSaleList.Add("顾问提成");
            ObjSaleList.Add("策划提成");
            ObjSaleList.Add("工程执行");
            foreach (var Objitem in ObjSaleList)
            {
                var Model = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, Objitem);
                if (Model == null)
                {
                    ObjCostSumBLL.Insert(new DataAssmblly.FL_CostSum()
                    {
                        Name = Objitem,
                        Content = "",
                        Sumtotal = 0,
                        ActualSumTotal = 0,
                        DispatchingID = Request["DispatchingID"].ToInt32(),
                        RowType = 6,
                        CreateDate = DateTime.Now
                    });
                }
            }
            BinderData();
            JavaScriptTools.AlertWindow("操作成功!", Page);
        }
        #endregion

        #region 设计类清单


        /// <summary>
        /// 修改设计单
        /// </summary>
        public void UpdateDesign()
        {
            for (int i = 0; i < rptDesignClass.Items.Count; i++)
            {
                var currentItem = rptDesignClass.Items[i];
                TextBox txtTotalPrice = currentItem.FindControl("txtTotalPrice") as TextBox;
                TextBox txtNode = currentItem.FindControl("txtNode") as TextBox;
                TextBox txtActualSumtotal = currentItem.FindControl("txtActualSumtotal") as TextBox;
                TextBox txtAdvance = currentItem.FindControl("txtAdvance") as TextBox;
                TextBox txtShortCome = currentItem.FindControl("txtShortCome") as TextBox;
                HiddenField DesignClassId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = DesignClassId.Value.ToInt32();
                FL_Designclass DesignModel = ObjDesignClassBLL.GetByID(value);

                DesignModel.TotalPrice = txtTotalPrice.Text.ToDecimal();
                DesignModel.Node = txtNode.Text.ToString();
                DesignModel.ActualSumTotal = txtActualSumtotal.Text.ToDecimal();
                DesignModel.Advance = txtAdvance.Text.ToString();
                DesignModel.ShortCome = txtShortCome.Text.ToString();
                ObjDesignClassBLL.Update(DesignModel);
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

    }
}