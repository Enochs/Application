using HA.PMS.BLLAssmblly.FD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
//临时注释 修改报价单
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceEdit : System.Web.UI.Page
    {
        ///// <summary>
        ///// 类别业务逻辑
        ///// </summary>
        //Category OjbCategoryBLL = new Category();
 

        ///// <summary>
   
        ///// <summary>
        ///// 报价单主体表
        ///// </summary>
        //HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();


        ///// <summary>
        ///// 产品
        ///// </summary>
        //Productcs ObjProductcsBLL = new Productcs();

        //int QuotedID = 0;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    QuotedID = Request["QuotedID"].ToInt32();
        //    if (!IsPostBack)
        //    {

        //        BinderData();
        //    }
        //}
        ///// <summary>
        ///// 删除大类报价单 联动删除
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="e"></param>
        //protected void reppgfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{

        //}


        ///// <summary>
        ///// 绑定预览报价单
        ///// </summary>
        //private void BinderData()
        //{

        //    this.reppgfirst.DataSource = ObjCategoryForQuotedPriceBLL.GetByQuotedID(QuotedID);
        //    this.reppgfirst.DataBind();
 
        //}

        ///// <summary>
        ///// 绑定二级报价单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void reppgfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{

        //    //HiddenField ObjhideKey = (HiddenField)e.Item.FindControl("hideCategoryID");
        //    //var ObjSecondList = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32(), QuotedID);
        //    //if (ObjSecondList.Count == 0)
        //    //{
        //    //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repCGFirst");
        //    //    ObjrepSecond.DataSource = ObjCategoryForQuotedPriceBLL.GetByQuotedID(QuotedID);
        //    //    ObjrepSecond.DataBind();
        //    //}
        //    //else
        //    //{
        //    //    //var ObjSecondCGList = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32());
        //    //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repCgSecondList");
        //    //    ObjrepSecond.DataSource = ObjSecondList;
        //    //    ObjrepSecond.DataBind();

        //    //}
        //}

        ///// <summary>
        ///// 绑定第二级获取第三级
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void repCgSecondList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    //repThiredList
        //    //HiddenField ObjhideKey = (HiddenField)e.Item.FindControl("hideThiredCategoryID");
        //    //var ObjSecondList = ObjProductForQuotedPriceBLL.GetByParentCategoryID(ObjhideKey.Value.ToInt32(), QuotedID);
        //    //if (ObjSecondList.Count == 0)
        //    //{
        //    //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repThiredList");
        //    //    ObjrepSecond.DataSource = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32(), QuotedID);
        //    //    ObjrepSecond.DataBind();
        //    //}
        //    //else
        //    //{
        //    //    //var ObjSecondCGList = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32());
        //    //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repProduct");
        //    //    ObjrepSecond.DataSource = ObjSecondList;
        //    //    ObjrepSecond.DataBind();

        //    //}
        //    //hideThiredCategoryID
        //}


        ///// <summary>
        ///// 根据类型ID获取类型名称
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <returns></returns>
        //public string GetProductByID(object Key)
        //{
        //    return ObjProductcsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
        //}



        ///// <summary>
        ///// 生辰产品一级分类
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnStarFirstpg_Click(object sender, EventArgs e)
        //{

        //    var CGKey = this.hidePgValue.Value.Split(',');
        //    if (CGKey.Length > 0)
        //    {
        //        int[] ObjList = new int[CGKey.Length];
        //        int i = 0;
        //        foreach (string Key in CGKey)
        //        {

        //            ObjList[i] = Key.ToInt32();
        //            i++;
        //        }
        //        this.reppgfirst.DataSource = OjbCategoryBLL.GetinList(ObjList);
        //        this.reppgfirst.DataBind();

        //    }
        //}

        ///// <summary>
        ///// 生成产品二级分类
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnCreateSecond_Click(object sender, EventArgs e)
        //{
        //    var CGKey = this.hideSecondValue.Value.Split(',');
        //    if (CGKey.Length > 0)
        //    {

        //        int[] ObjList = new int[CGKey.Length];
        //        int i = 0;
        //        foreach (string Key in CGKey)
        //        {

        //            ObjList[i] = Key.ToInt32();
        //            i++;
        //        }


        //        for (int P = 0; P < reppgfirst.Items.Count; P++)
        //        {
        //            HiddenField ObjHideKey = (HiddenField)reppgfirst.Items[P].FindControl("hideCategoryID");
        //            if (ObjHideKey.Value == hideSecondCategoryID.Value)
        //            {
        //                Repeater ObjRepfirst = (Repeater)reppgfirst.Items[P].FindControl("repCGFirst");
        //                Repeater ObjRepSecondList = (Repeater)reppgfirst.Items[P].FindControl("repCgSecondList");
        //                var CategoryList = OjbCategoryBLL.GetinList(ObjList);
        //                int N = 0;
        //                List<FL_ProductitemForQuotedPrice> ObjBinderList = new List<FL_ProductitemForQuotedPrice>();
        //                FL_ProductitemForQuotedPrice ObjModel;
        //                ObjRepfirst.Visible = false;
        //                foreach (var Objitem in CategoryList)
        //                {
        //                    ObjModel = new FL_ProductitemForQuotedPrice();
        //                    ObjModel.CategoryName = CategoryList[N].CategoryName;
        //                    ObjModel.CategoryID = CategoryList[N].CategoryID;
        //                    ObjBinderList.Add(ObjModel);

        //                    N++;
        //                }

        //                ObjRepSecondList.DataSource = ObjBinderList;
        //                ObjRepSecondList.DataBind();
        //            }
        //        }
        //        //hideSecondCategoryID

        //        //this.reppgfirst.DataSource = OjbCategoryBLL.GetinList(ObjList);
        //        //this.reppgfirst.DataBind();

        //    }
        //}

        ///// <summary>
        ///// 生成产品级报价单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnCreateThired_Click(object sender, EventArgs e)
        //{
        //    var CGKey = this.hideThirdValue.Value.Split(',');
        //    if (CGKey.Length > 0)
        //    {

        //        int[] ObjKeyList = new int[CGKey.Length];
        //        int i = 0;
        //        foreach (string Key in CGKey)
        //        {

        //            ObjKeyList[i] = Key.ToInt32();
        //            i++;
        //        }


        //        for (int P = 0; P < reppgfirst.Items.Count; P++)
        //        {

        //            Repeater ObjRepSecondList = (Repeater)reppgfirst.Items[P].FindControl("repCgSecondList");
        //            for (int T = 0; T < ObjRepSecondList.Items.Count; T++)
        //            {
        //                HiddenField ObjHideKey = (HiddenField)ObjRepSecondList.Items[T].FindControl("hideThiredCategoryID");
        //                if (ObjHideKey.Value == hideSecondCategoryID.Value)
        //                {
        //                    Repeater ObjrepProduct = (Repeater)ObjRepSecondList.Items[T].FindControl("repProduct");
        //                    List<FL_ProductForQuotedPrice> ObjSourceList = new List<FL_ProductForQuotedPrice>();
        //                    var ProductList = ObjProductcsBLL.GetinKeyList(ObjKeyList);
        //                    FL_ProductForQuotedPrice ObjSetModel = new FL_ProductForQuotedPrice();
        //                    foreach (var ObjProduct in ProductList)
        //                    {
        //                        ObjSetModel.ProductID = ObjProduct.ProductID;
        //                        ObjSourceList.Add(ObjSetModel);
        //                    }
        //                    ObjrepProduct.DataSource = ObjSourceList;
        //                    ObjrepProduct.DataBind();
        //                    return;
        //                }
        //            }

        //        }
        //    }
        //}




 

        //protected void hideSecondValue_ValueChanged(object sender, EventArgs e)
        //{

        //}




        ///// <summary>
        ///// 保存创建的报价单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSaveallChange_Click(object sender, EventArgs e)
        //{
        //    //FL_QuotedPrice ObjQuotedPriceModel = new FL_QuotedPrice();
        //    //ObjQuotedPriceModel.CustomerID = Request["CustomerID"].ToInt32();
        //    //ObjQuotedPriceModel.IsDelete = false;
        //    //ObjQuotedPriceModel.OrderID = 11;// Request["OrderID"].ToInt32();
        //    //ObjQuotedPriceModel.CategoryName = string.Empty;
        //    //var ObjQuteKey = ObjQuotedPriceBLL.Insert(ObjQuotedPriceModel);
        //    //if (ObjQuteKey > 0)
        //    //{

        //    //    //生成大类报价单
        //    //    FL_CategoryForQuotedPrice ObjCategoryForQuotedPrice;
        //    //    for (int P = 0; P < reppgfirst.Items.Count; P++)
        //    //    {
        //    //        HiddenField ObjhideCategoryID = (HiddenField)reppgfirst.Items[P].FindControl("hideCategoryID");
        //    //        //循环创建大类报价单
        //    //        Repeater ObjRepfirst = (Repeater)reppgfirst.Items[P].FindControl("repCGFirst");
        //    //        for (int F = 0; F < ObjRepfirst.Items.Count; F++)
        //    //        {
        //    //            ObjCategoryForQuotedPrice = new FL_CategoryForQuotedPrice();
        //    //            ObjCategoryForQuotedPrice.CategoryID = ObjhideCategoryID.Value.ToInt32();
        //    //            ObjCategoryForQuotedPrice.CategoryName = OjbCategoryBLL.GetByID(ObjCategoryForQuotedPrice.CategoryID).CategoryName;
        //    //            ObjCategoryForQuotedPrice.ImageUrl = string.Empty;
        //    //            ObjCategoryForQuotedPrice.IsDelete = false;
        //    //            ObjCategoryForQuotedPrice.Unit = ((TextBox)ObjRepfirst.Items[F].FindControl("txtUnitPrice")).Text;
        //    //            ObjCategoryForQuotedPrice.ServiceContent = ((TextBox)ObjRepfirst.Items[F].FindControl("txtServiceContent")).Text;
        //    //            ObjCategoryForQuotedPrice.Requirement = ((TextBox)ObjRepfirst.Items[F].FindControl("txtRequirement")).Text;
        //    //            ObjCategoryForQuotedPrice.UnitPrice = ((TextBox)ObjRepfirst.Items[F].FindControl("txtUnitPrice")).Text.ToDecimal();
        //    //            ObjCategoryForQuotedPrice.Quantity = ((TextBox)ObjRepfirst.Items[F].FindControl("txtQuantity")).Text.ToDecimal();
        //    //            ObjCategoryForQuotedPrice.Subtotal = ((TextBox)ObjRepfirst.Items[F].FindControl("txtSubtotal")).Text.ToDecimal();
        //    //            ObjCategoryForQuotedPrice.Remark = ((TextBox)ObjRepfirst.Items[F].FindControl("txtRemark")).Text;
        //    //            ObjCategoryForQuotedPrice.QuotedID = ObjQuteKey;
        //    //            ObjCategoryForQuotedPriceBLL.Insert(ObjCategoryForQuotedPrice);
        //    //        }

        //    //        //开始创建二类报价单
        //    //        Repeater ObjSecondList = (Repeater)reppgfirst.Items[P].FindControl("repCgSecondList");
        //    //        for (int S = 0; S < ObjSecondList.Items.Count; S++)
        //    //        {

        //    //            HiddenField ObjhideThiredCategoryID = (HiddenField)ObjSecondList.Items[S].FindControl("hideThiredCategoryID");
        //    //            var CategoryID = ObjhideThiredCategoryID.Value.ToInt32();
        //    //            //找到二类报价单
        //    //            Repeater ObjRepThiredList = (Repeater)ObjSecondList.Items[S].FindControl("repThiredList");
        //    //            FL_ProductitemForQuotedPrice ObjProductitemForQuotedPrice;
        //    //            if (ObjRepThiredList.Items.Count > 0)
        //    //            {
        //    //                for (int T = 0; T < ObjRepThiredList.Items.Count; T++)
        //    //                {
        //    //                    ObjProductitemForQuotedPrice = new FL_ProductitemForQuotedPrice();

        //    //                    ObjProductitemForQuotedPrice.CategoryID = ObjhideThiredCategoryID.Value.ToInt32();
        //    //                    ObjProductitemForQuotedPrice.CategoryName = OjbCategoryBLL.GetByID(ObjProductitemForQuotedPrice.CategoryID).CategoryName;
        //    //                    ObjProductitemForQuotedPrice.ParentCategoryID = ObjhideCategoryID.Value.ToInt32();
        //    //                    ObjProductitemForQuotedPrice.ImageUrl = string.Empty;
        //    //                    ObjProductitemForQuotedPrice.IsDelete = false;
        //    //                    ObjProductitemForQuotedPrice.Unit = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtUnitPrice")).Text;
        //    //                    ObjProductitemForQuotedPrice.ServiceContent = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtServiceContent")).Text;
        //    //                    ObjProductitemForQuotedPrice.Requirement = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtRequirement")).Text;
        //    //                    ObjProductitemForQuotedPrice.UnitPrice = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtUnitPrice")).Text.ToDecimal();
        //    //                    ObjProductitemForQuotedPrice.Quantity = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtQuantity")).Text.ToDecimal();
        //    //                    ObjProductitemForQuotedPrice.Subtotal = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtSubtotal")).Text.ToDecimal();
        //    //                    ObjProductitemForQuotedPrice.Remark = ((TextBox)ObjRepThiredList.Items[T].FindControl("txtRemark")).Text;
        //    //                    ObjProductitemForQuotedPrice.QuotedID = ObjQuteKey;
        //    //                    ObjProductitemForQuotedPriceBLL.Insert(ObjProductitemForQuotedPrice);
        //    //                }
        //    //            }

        //    //            //找到产品报价单
        //    //            //生成报价单
        //    //            Repeater ObjrepProductList = (Repeater)ObjSecondList.Items[S].FindControl("repProduct");
        //    //            if (ObjrepProductList.Items.Count > 0)
        //    //            {
        //    //                FL_ProductForQuotedPrice ObjProductForQuotedPriceModel = new FL_ProductForQuotedPrice();
        //    //                //循环升生成报价单
        //    //                for (int U = 0; U < ObjrepProductList.Items.Count; U++)
        //    //                {
        //    //                    ObjProductForQuotedPriceModel = new FL_ProductForQuotedPrice();
        //    //                    ObjProductForQuotedPriceModel.CategoryID = ObjhideThiredCategoryID.Value.ToInt32();
        //    //                    ObjProductForQuotedPriceModel.CategoryName = OjbCategoryBLL.GetByID(ObjProductForQuotedPriceModel.CategoryID).CategoryName;
        //    //                    ObjProductForQuotedPriceModel.ParentCategoryID = ObjhideCategoryID.Value.ToInt32();
        //    //                    ObjProductForQuotedPriceModel.ImageUrl = string.Empty;
        //    //                    ObjProductForQuotedPriceModel.IsDelete = false;
        //    //                    ObjProductForQuotedPriceModel.Unit = ((TextBox)ObjrepProductList.Items[U].FindControl("txtUnitPrice")).Text;
        //    //                    ObjProductForQuotedPriceModel.ServiceContent = ((TextBox)ObjrepProductList.Items[U].FindControl("txtServiceContent")).Text;
        //    //                    ObjProductForQuotedPriceModel.Requirement = ((TextBox)ObjrepProductList.Items[U].FindControl("txtRequirement")).Text;
        //    //                    ObjProductForQuotedPriceModel.UnitPrice = ((TextBox)ObjrepProductList.Items[U].FindControl("txtUnitPrice")).Text.ToDecimal();
        //    //                    ObjProductForQuotedPriceModel.Quantity = ((TextBox)ObjrepProductList.Items[U].FindControl("txtQuantity")).Text.ToDecimal();
        //    //                    ObjProductForQuotedPriceModel.Subtotal = ((TextBox)ObjrepProductList.Items[U].FindControl("txtSubtotal")).Text.ToDecimal();
        //    //                    ObjProductForQuotedPriceModel.Remark = ((TextBox)ObjrepProductList.Items[U].FindControl("txtRemark")).Text;
        //    //                    ObjProductForQuotedPriceModel.QuotedID = ObjQuteKey;
        //    //                    ObjProductForQuotedPriceModel.ProductID = ((HiddenField)ObjrepProductList.Items[U].FindControl("hideProductID")).Value.ToInt32();
        //    //                    ObjProductForQuotedPriceBLL.Insert(ObjProductForQuotedPriceModel);
        //    //                }
        //    //            }

        //    //        }

        //    //    }

        //    //}

        //    //Response.Redirect("QuotedPriceShow.aspx?QuotedID=" + ObjQuteKey);
        //}
    }
}