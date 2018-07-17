using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceChangeList : SystemPage
    {

        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();


        Dispatching ObjDispatchingBLL = new Dispatching();

        QuotedPriceItems ObjQuotedPriceItemBLL = new QuotedPriceItems();


        /// <summary>
        /// 派工单操作
        /// </summary>
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        int DispatchingID = 0;
        int OrderID = 0;
        int QuotedID = 0;
        int CustomerID = 0;
        int FirstMake = 1;

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
        {

            DispatchingID = Request["DispatchingID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            QuotedID = Request["QuotedId"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            CreateButton();
            if (!IsPostBack)
            {
                DataBinder(1);
            }
        }
        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void DataBinder(int FirstMake)
        {
            //无类别
            int MineState = 0;
            List<FL_ProductforDispatching> objProductList = ObjProductforDispatchingBLL.GetByMineProductall(User.Identity.Name.ToInt32(), DispatchingID, 1, out MineState).Where(C => C.Classification != null && C.IsFirstMakes == FirstMake).ToList().ToList();
            this.repfirst.DataSource = objProductList;
            this.repfirst.DataBind();
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
            var DataList = new List<FL_ProductforDispatching>();
            //获取二级项目
            var NewList = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 1, FirstMake);
            DataList.Add(NewList);

            //获取三级产品
            DataList.AddRange(ObjProductforDispatchingBLL.GetByParentCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32()).Where(C => C.IsGet == true && C.IsFirstMakes == FirstMake).OrderBy(C => C.CategoryID).ToList());

            Label lblSumMoney = e.Item.FindControl("lblSumMoney") as Label;
            Label lblSumQuantity = e.Item.FindControl("lblSumQuantity") as Label;

            lblSumMoney.Text = (lblSumMoney.Text.ToDecimal() + DataList.Where(C => C.Subtotal != null).ToList().Sum(C => C.Subtotal.Value)).ToString();
            lblSumQuantity.Text = (DataList.Sum(C => C.Quantity).ToString().ToInt32() - 1).ToString();


            ObjRep.DataSource = DataList;
            ObjRep.DataBind();
        }
        #endregion

        #region 动态生成Button 及它的点击事件
        /// <summary>
        /// 动态生成
        /// </summary>
        public void CreateButton()
        {
            int MaxIsFirstMake = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID).Max(C => C.IsFirstMakes).ToString().ToInt32();
            Button btnCreate = new Button();
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
            string text = button.Text.ToString();
            int index = text.Substring(1, 1).ToInt32();
            FirstMake = index;
            DataBinder(FirstMake);

        }
        #endregion



        #region 默认应有功能

        #region 获取图片路径
        /// <summary>
        /// 获取图片
        /// </summary>
        //public string GetKindImage(object Kind)
        //{
        //    var ObjImageList = ObjDispatchingImageBLL.GetByKind(DispatchingID, Kind.ToString().ToInt32());
        //    string ImageList = string.Empty;
        //    foreach (var ObjImage in ObjImageList)
        //    {
        //        ImageList += "<img alt='' src='" + ObjImage.FileAddress + "' />";
        //    }
        //    //<img alt="" src="../../Images/Appraise/3.gif" />
        //    return ImageList;
        //}
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

        #endregion

    }
}