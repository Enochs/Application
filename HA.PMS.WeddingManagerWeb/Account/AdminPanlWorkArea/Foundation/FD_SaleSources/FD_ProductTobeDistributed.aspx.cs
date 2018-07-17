using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using System.IO;
using Ionic.Zip;
using System.Text;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_ProductTobeDistributed : SystemPage
    {
        ProductTobeDistributed objProductTobeDistributedBLL = new ProductTobeDistributed();
        Category objCategoryBLL = new Category();
        Productcs objProductsBLL = new Productcs();
        AllProducts objAllProductsBLL = new AllProducts();

        ProductforDispatching objProductforDispatchingBLL = new ProductforDispatching();
        Supplier objSupplierBLL = new Supplier();
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        #region 页面初始化
        /// <summary>
        /// 加载页面
        /// </summary>   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder(sender, e);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DataBinder(object sender, EventArgs e)
        {
            List<ObjectParameter> listParameter = new List<ObjectParameter>();
            listParameter.Add(new ObjectParameter("Type", 3));
            listParameter.Add(new ObjectParameter("IsTobeDistributed", false));

            int startIndex = TobeDistributedPager.StartRecordIndex;
            int resourceCount = 0;

            var query = objAllProductsBLL.GetbyAllProductsParameter(listParameter.ToArray(), TobeDistributedPager.PageSize, TobeDistributedPager.CurrentPageIndex, out resourceCount);
            TobeDistributedPager.RecordCount = resourceCount;
            rptProductTobeDistributed.DataSource = query;
            rptProductTobeDistributed.DataBind();
        }
        #endregion   

        #region 分页
        /// <summary>
        /// 分页  上一页/下一页
        /// </summary> 
        protected void TobeDistributedPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder(sender, e);
        }
        #endregion
    }
}