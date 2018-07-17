using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class QuotedPriceShow : System.Web.UI.UserControl
    {
        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

   

        
        /// <summary>
        /// 报价单主体表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        int QuotedID = 0;

        /// <summary>
        /// 产品
        /// </summary>
        Productcs ObjProductcsBLL = new Productcs();
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            if (!IsPostBack)
            {

                BinderData();
            }
        }
        /// <summary>
        /// 删除大类报价单 联动删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void reppgfirst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }


        /// <summary>
        /// 绑定预览报价单
        /// </summary>
        private void BinderData()
        {

            //this.reppgfirst.DataSource = ObjCategoryForQuotedPriceBLL.GetByQuotedID(QuotedID);
            //this.reppgfirst.DataBind();

        }

        /// <summary>
        /// 绑定二级报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void reppgfirst_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //HiddenField ObjhideKey = (HiddenField)e.Item.FindControl("hideCategoryID");
            //var ObjSecondList = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32(), QuotedID);
            //if (ObjSecondList.Count == 0)
            //{
            //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repCGFirst");
            //    ObjrepSecond.DataSource = ObjCategoryForQuotedPriceBLL.GetByQuotedID(QuotedID);
            //    ObjrepSecond.DataBind();
            //}
            //else
            //{
            //    //var ObjSecondCGList = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32());
            //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repCgSecondList");
            //    ObjrepSecond.DataSource = ObjSecondList;
            //    ObjrepSecond.DataBind();

            //}
        }

        /// <summary>
        /// 绑定第二级获取第三级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repCgSecondList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //repThiredList
            //HiddenField ObjhideKey = (HiddenField)e.Item.FindControl("hideThiredCategoryID");
            //var ObjSecondList = ObjProductForQuotedPriceBLL.GetByParentCategoryID(ObjhideKey.Value.ToInt32(), QuotedID);
            //if (ObjSecondList.Count == 0)
            //{
            //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repThiredList");
            //    ObjrepSecond.DataSource = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32(), QuotedID);
            //    ObjrepSecond.DataBind();
            //}
            //else
            //{
            //    //var ObjSecondCGList = ObjProductitemForQuotedPriceBLL.GetByParentID(ObjhideKey.Value.ToInt32());
            //    Repeater ObjrepSecond = (Repeater)e.Item.FindControl("repProduct");
            //    ObjrepSecond.DataSource = ObjSecondList;
            //    ObjrepSecond.DataBind();

            //}
            //hideThiredCategoryID
        }

    }
}