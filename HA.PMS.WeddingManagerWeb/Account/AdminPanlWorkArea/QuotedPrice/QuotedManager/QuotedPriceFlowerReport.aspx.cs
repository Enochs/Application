using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedPriceFlowerReport : SystemPage
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
                var ObjItem = new HA.PMS.BLLAssmblly.Sys.TitleNode().Getbyall();
                if (ObjItem != null)
                {
                    lblTop.Text = ObjItem.NodeTop;
                    lblBottom.Text = ObjItem.NodeButtom;
                }
                BinderData();

                var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);

            }
        }


        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {

            if (Request["OrderID"] != null)
            {

                var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                Order ObjOrderBLL = new Order();
                txtAggregateAmount.Text = ObjquotedModel.AggregateAmount + string.Empty;
                txtEarnestMoney.Text = ObjquotedModel.EarnestMoney + string.Empty;
                txtRealAmount.Text = ObjquotedModel.RealAmount + string.Empty;
                hideAggregateAmount.Value = ObjquotedModel.AggregateAmount + string.Empty;
                lblyukuan.Text = GetOverFinishMoney(ObjquotedModel.CustomerID.Value).ToString();
                lblOrderMoney.Text = ObjOrderBLL.GetByID(OrderID).EarnestMoney + string.Empty;
            }
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
        /// 获取图片
        /// </summary>
        /// <param name="Kind"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 查看状态
        /// </summary>
        /// <param name="Kind"></param>
        /// <returns></returns>
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
            this.repfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0, "2");
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
            var DataList = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, ObjHiddCategKey.Value.ToInt32(), 2,"2").OrderByDescending(C => C.ParentCategoryID).ToList();
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
                var ItemList = ObjQuotedPriceItemsBLL.GetByCategoryID(QuotedID, ObjItem.CategoryID, 3).Where(C=>C.Productproperty==2).ToList();
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
        /// 报废订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnloss_Click(object sender, EventArgs e)
        {
            var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjquotedModel.IsDelete = true;
            ObjQuotedPriceBLL.Update(ObjquotedModel);
            JavaScriptTools.AlertWindowAndLocation("订单已报废!", "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedLoss.aspx", Page);
        }

    }
}