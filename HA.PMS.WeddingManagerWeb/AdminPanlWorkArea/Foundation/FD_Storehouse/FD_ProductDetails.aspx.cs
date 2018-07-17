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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_ProductDetails : SystemPage
    {
        Storehouse objStorehouseBLL = new Storehouse();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                DataBinder();
            }
        }

        
        protected void DataBinder() 
        {
            string orderCoder = Request.QueryString["OrderCoder"];

            #region 查询参数
            FL_DispatchAllProducts fl_Dsipatch = new FL_DispatchAllProducts();
            fl_Dsipatch.OrderCoder = orderCoder;
            fl_Dsipatch.Type = 2;
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("OrderCoder", fl_Dsipatch.OrderCoder));
            ObjParameterList.Add(new ObjectParameter("Type", fl_Dsipatch.Type));
            #endregion

            #region 分页页码
            int startIndex = ProductPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objStorehouseBLL.GetbyFL_DispatchAllProductsParameter(ObjParameterList.ToArray(), ProductPager.PageSize, ProductPager.CurrentPageIndex, out resourceCount);
            ProductPager.RecordCount = resourceCount;

            rptStoreHouse.DataSource = query;
            rptStoreHouse.DataBind();

            #endregion
        }
        protected void ProductPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}