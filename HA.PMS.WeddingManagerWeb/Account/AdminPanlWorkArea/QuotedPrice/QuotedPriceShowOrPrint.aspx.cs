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
using HA.PMS.BLLAssmblly.FinancialAffairsbll;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceShowOrPrint : Page
    {
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();


        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

        /// <summary>
        /// 客户操作
        /// </summary>
        Customers ObjCustomersBLL = new Customers();


        /// <summary>
        /// 打折折扣
        /// </summary>
        QuotedPriceForType ObjDiscountBLL = new QuotedPriceForType();

        /// <summary>
        /// 报价单主表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        int QuotedID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int Index = 0;

        decimal TotalSum = 0;

        TitleNode ObjTitleNodeBLL = new TitleNode();

        #region 页面加载
        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerID = Request["CustomerID"].ToInt32();
            QuotedID = Request["QuotedID"].ToInt32() > 0 ? Request["QuotedID"].ToInt32() : ObjQuotedPriceBLL.GetByCustomerID(CustomerID).QuotedID;
            OrderID = Request["OrderID"].ToInt32() > 0 ? Request["OrderID"].ToInt32() : ObjQuotedPriceBLL.GetByCustomerID(CustomerID).OrderID.Value;
            Index = Request["IsFirstMake"].ToInt32();
            var ObjItem = ObjTitleNodeBLL.Getbyall();
            if (ObjItem != null)
            {
                lblTop.Text = ObjItem.NodeTop;
                lblBottom.Text = ObjItem.NodeButtom;
            }
            if (!IsPostBack)
            {
                BinderData();

                var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                lblRemark.Text = ObjUopdateModel.Remark.ToString();
                //txtEarnestMoney.Text = GetOverFinishMoney(CustomerID);
            }
        }
        #endregion

        #region 获取余款
        /// <summary>
        /// 获取余款数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetOverFinishMoney(int CustomerID)
        {
            OrderEarnestMoney ObjOrderEarnestMoneyBLL = new OrderEarnestMoney();
            /// <summary>
            /// 收款计划
            /// </summary>
            int CID = CustomerID.ToString().ToInt32();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal EarnestMoney = 0;
            decimal AllMoney = 0;
            decimal FinishAmount = 0;
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CID);
            if (QuotedModel != null)
            {
                EarnestMoney = QuotedModel.EarnestMoney.HasValue ? QuotedModel.EarnestMoney.Value : 0;
                AllMoney = QuotedModel.FinishAmount.HasValue ? QuotedModel.FinishAmount.Value : 0;




                var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);

                FinishAmount += EarnestMoney;
                foreach (var Objitem in ObjList)
                {
                    FinishAmount += Objitem.RealityAmount.Value;
                }
                var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
                if (ObjEorder != null && ObjEorder.EarnestMoney.HasValue)
                {
                    FinishAmount += ObjEorder.EarnestMoney.Value;
                }

            }

            //var OrdemoDEL = ObjOrderBLL.GetbyCustomerID(CID);
            //if (OrdemoDEL != null)
            //{
            //    FinishAmount += OrdemoDEL.EarnestMoney.HasValue ? OrdemoDEL.EarnestMoney.Value : 0;
            //}
            if (AllMoney != null)
            {

                return (AllMoney.ToString().ToDecimal() - FinishAmount).ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 客户信息绑定
        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {
            var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomerID);

            Employee ObjEmployeeBLL = new Employee();

            Order ObjOrderBLL = new Order();
            var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            if (Request["Type"] == null)
            {
                var DicountModel = ObjDiscountBLL.GetByOrderID(OrderID, Request["IsFirstMake"].ToInt32());
                //txtAggregateAmount.Text = DicountModel.Total.ToString();
                if (ObjquotedModel.FinishState == 1)
                {
                    if (DicountModel != null)
                    {
                        txtAggregateAmount.Text = DicountModel.Total.ToString();
                    }
                }
                else
                {
                    txtAggregateAmount.Text = ObjquotedModel.FinishAmount.ToString();
                }
            }
            else
            {
                int Type = Request["Type"].ToInt32();
                var DicountModel = ObjDiscountBLL.GetByOrderID(OrderID, Request["IsFirstMake"].ToInt32());
                var DataList = ObjQuotedPriceItemsBLL.GetByOrdersID(OrderID);
                //2：代表物料  物料要算上折扣价格
                //txtAggregateAmount.Text = DataList.Where(C => C.Type == Type && C.IsFirstMake == Index).Sum(C => C.Subtotal).ToString();
                if (Type == 1)
                {
                    txtAggregateAmount.Text = DicountModel.PPrice.ToString().ToDecimal().ToString("f2");
                }
                else if (Type == 2)
                {
                    txtAggregateAmount.Text = DicountModel.MPrice.ToString().ToDecimal().ToString("f2");
                }
                else if (Type == 3)
                {
                    txtAggregateAmount.Text = DicountModel.WPrice.ToString().ToDecimal().ToString("f2");
                }
                else if (Type == 4)
                {
                    txtAggregateAmount.Text = DicountModel.OPrice.ToString().ToDecimal().ToString("f2");
                }
            }

            hideAggregateAmount.Value = ObjquotedModel.FinishAmount + string.Empty;
            lblNode.Text = ObjquotedModel.Remark;

            var ObjEmPloyeeModel = ObjEmployeeBLL.GetByID(ObjquotedModel.EmpLoyeeID);
            lblQuotedEmployee.Text = ObjEmPloyeeModel.EmployeeName;

            var OrderEmployeeModel = ObjOrderBLL.GetByID(OrderID);
            lblOrderEmployee.Text = ObjEmployeeBLL.GetByID(OrderEmployeeModel.EmployeeID).EmployeeName;
        }
        #endregion

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
                var Model = ObjAllProductsBLL.GetByID(Key.ToString().ToInt32());
                if (Model != null)
                {
                    return Model.ProductName.Trim();
                }
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            else
            {
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
        }



        public string GetNbsp(string Source, int Length)
        {
            string Nbsp = "";
            if (Source != null)
            {
                if (Source.Length < Length)
                {
                    for (int i = 0; i < (Length - Source.Length); i++)
                    {
                        Nbsp += "&nbsp;";
                    }
                    return Source + Nbsp;
                }
            }
            return Source;
        }
        #endregion

        #region 绑定数据 BinderData
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            if (Request["Type"] == null)
            {
                this.repfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).Where(C => C.IsFirstMake == Index);
                this.repfirst.DataBind();
            }
            else
            {
                int Type = Request["Type"].ToInt32();
                this.repfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).Where(C => C.IsFirstMake == Index && C.Type == Type);
                this.repfirst.DataBind();
            }
            BinderCuseomerDate();
        }
        #endregion

        #region 获取产品
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

        #region 二级数据绑定
        /// <summary>
        /// 绑定二级数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var DataItem = (FL_QuotedPriceItems)e.Item.DataItem;
            TotalSum += DataItem.ItemAmount.ToString().ToDecimal();
            HiddenField ObjHiddCategKey = (HiddenField)e.Item.FindControl("hidefirstCategoryID");
            Repeater ObjRep = (Repeater)e.Item.FindControl("repdatalist");
            //TextBox ObjtxtPrice = (TextBox)e.Item.FindControl("txtSalePrice");
            var ObjItemList = new List<FL_QuotedPriceItems>();
            //获取二级项目
            var DataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                var NewList = ObjQuotedPriceItemsBLL.GetOnlyByCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }

            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3);
                if (ItemList.Count == 0)
                {
                    ObjItemList.Add(ObjItem);
                }
                else
                {
                    ItemList[ItemList.Count - 1].ItemLevel = 2;
                    ObjItemList.AddRange(ItemList);
                }
            }
            ObjItemList.Reverse();
            ObjRep.DataSource = ObjItemList.Where(C => C.IsFirstMake == Index);
            ObjRep.DataBind();
            if (Request["Type"] == null)
            {
                ObjRep.DataSource = ObjItemList.Where(C => C.IsFirstMake == Index);
                ObjRep.DataBind();
            }
            else
            {
                int Type = Request["Type"].ToInt32();
                ObjRep.DataSource = ObjItemList.Where(C => C.IsFirstMake == Index && C.Type == Type);
                ObjRep.DataBind();
            }


        }
        #endregion

        #region 导出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.ContentType = "application/vnd.ms-excel";
            //this.EnableViewState = false;
            //btnExport.Visible = false;
            Response.Redirect(Request.Url.ToString().Replace("QuotedPriceShowOrPrint", "QuotedPriceEcport"));
        }
        #endregion


    }
}