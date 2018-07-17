using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskShow : SystemPage
    {
        /// <summary>
        /// 
        /// </summary>
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


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
        int DispatchingID = 0;
        int OrderID = 0;
        int CustomerID = 0;
        int CategoryID = 0;

        /// <summary>
        /// 绑定界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            CategoryID = Request["CategoryID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();


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
            this.repfirst.DataSource = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, CategoryID, 1);
            this.repfirst.DataBind();
            //BinderCuseomerDate();
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
            var ObjItemList = new List<FL_ProductforDispatching>();
            //获取二级项目
            var DataList = ObjProductforDispatchingBLL.GetByParentCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 2).OrderByDescending(C => C.ParentCategoryID).ToList();
            //如果没有二级 则只有一级项目
            if (DataList.Count == 0)
            {
                //ObjtxtPrice.Enabled = true;
                var NewList = ObjProductforDispatchingBLL.GetOnlyByCatageID(DispatchingID, ObjHiddCategKey.Value.ToInt32(), 1);
                DataList.Add(NewList);
            }
            //else
            //{
            //获取产品级项目
            foreach (var ObjItem in DataList)
            {
                var ItemList = ObjProductforDispatchingBLL.GetByCategoryID(DispatchingID, ObjItem.CategoryID, 3);
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
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repdatalist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
  
        }

        protected void repfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
 
 
    }
}