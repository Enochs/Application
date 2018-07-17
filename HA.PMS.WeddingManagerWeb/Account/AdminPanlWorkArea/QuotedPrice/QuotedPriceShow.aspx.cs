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
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using HA.PMS.BLLAssmblly.Emnus;


//临时注释 黄晓可 预览报价单
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceShow : SystemPage
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
            CustomerID = Request["CustomerID"].ToInt32();
            string URL = this.Page.Request.Url.ToString();
            string[] urls = URL.Split('?');
            if (urls[1].Contains("OrderID=&QuotedID="))
            {

            }
            else
            {
                QuotedID = Request["QuotedID"].ToInt32() > 0 ? Request["QuotedID"].ToInt32() : ObjQuotedPriceBLL.GetByCustomerID(CustomerID).QuotedID;
                OrderID = Request["OrderID"].ToInt32() > 0 ? Request["OrderID"].ToInt32() : ObjQuotedPriceBLL.GetByCustomerID(CustomerID).OrderID.Value;
            }


            if (Request["Delete"] != null)
            {
                btnloss.Visible = true;
                txtLostNode.Visible = true;
                lblLoseNode.Visible = true;

                btnreturn.Visible = false;
            }
            else
            {
                btnloss.Visible = false;
                txtLostNode.Visible = false;
                lblLoseNode.Visible = false;
            }


            //审核
            if (Request["Check"] != null)
            {
                txtLostNode.Enabled = false;

            }
            if (!IsPostBack)
            {
                var ObjItem = new HA.PMS.BLLAssmblly.Sys.TitleNode().Getbyall();
                if (ObjItem != null)
                {
                    lblTop.Text = ObjItem.NodeTop;
                    lblBottom.Text = ObjItem.NodeButtom;
                }

                if (!(urls[1].Contains("OrderID=&QuotedID=")))
                {
                    BinderData();
                    var ObjUopdateModel = ObjQuotedPriceBLL.GetByID(QuotedID);

                    txtLostNode.Text = ObjUopdateModel.LoseNode;
                    if (ObjUopdateModel.LoseCheck.HasValue)
                    {
                        if (ObjUopdateModel.LoseCheck.Value)
                        {
                            txtLostNode.Enabled = false;
                            btnreturn.Visible = false;
                            btnloss.Visible = false;
                        }
                    }
                }

            }
        }


        /// <summary>
        /// 绑定客户信息
        /// </summary>
        private void BinderCuseomerDate()
        {

            if (Request["OrderID"] != null)
            {
                QuotedPriceForType ObjDicountBLL = new QuotedPriceForType();
                var Model = ObjDicountBLL.GetByOrderID(Request["OrderID"].ToInt32(), 0);
                txtRealAmount.Text = Model.Total.ToString();

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
            this.repfirst.DataSource = ObjQuotedPriceItemsBLL.GetByParentCatageID(QuotedID, 0).Where(C => C.IsFirstMake == 0);
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
            ObjRep.DataSource = ObjItemList.Where(C => C.IsFirstMake == 0);
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
            if (Request["Delete"] != null)
            {
                var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                HA.PMS.BLLAssmblly.Sys.Employee ObjEmployee = new BLLAssmblly.Sys.Employee();
                ObjquotedModel.IsDelete = true;
                ObjquotedModel.LoseCheckEmployee = ObjEmployee.GetMineCheckEmployeeID(User.Identity.Name.ToInt32());
                ObjquotedModel.LoseCheck = false;
                ObjquotedModel.LoseNode = txtLostNode.Text;
                ObjQuotedPriceBLL.Update(ObjquotedModel);

                JavaScriptTools.AlertWindowAndLocation("订单已提交报废，报废审核人为本部门主管!", "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedLoss.aspx", Page);
            }

            if (Request["Check"] != null)
            {
                var ObjquotedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                HA.PMS.BLLAssmblly.Sys.Employee ObjEmployee = new BLLAssmblly.Sys.Employee();
                ObjquotedModel.IsDelete = true;

                ObjquotedModel.LoseCheck = true;

                ObjQuotedPriceBLL.Update(ObjquotedModel);

                JavaScriptTools.AlertWindowAndLocation("订单已报废!", "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedLoss.aspx", Page);
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Customers ObjCustomerBLL = new Customers();
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
            ObjQuotedPriceBLL.Update(ObjQuotePriceModel);

            //删除对应信息
            Dispatching ObjDispathingBLL = new Dispatching();

            ProductforDispatching ObjProductForDispatchingBLL = new ProductforDispatching();
            var DispatchingModel = ObjDispathingBLL.GetByCustomerID(CustomerID);
            if (DispatchingModel != null)
            {
                var ProductList = ObjProductForDispatchingBLL.GetByDispatchingID(DispatchingModel.DispatchingID);
                if (ProductList.Count > 0)
                {
                    foreach (var item in ProductList)
                    {
                        ObjProductForDispatchingBLL.Delete(item);
                    }
                }
                ObjDispathingBLL.Delete(DispatchingModel);
            }



            JavaScriptTools.AlertWindowAndLocation("打回订单成功", "QuotedPriceListCreateEdit.aspx?SaleEmployee=" + ObjQuotePriceModel.SaleEmployee + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&CustomerID=" + CustomerID + "&PartyDate=" + ObjQuotedPriceBLL.GetByViewQuotedID(QuotedID).PartyDate, Page);


        }

    }
}