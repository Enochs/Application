using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class DetailsDispatchingEvaulation : SystemPage
    {
        CostforOrder ObjOrderCostBLL = new CostforOrder();

        ProductforDispatching ObjProductForDisp = new ProductforDispatching();

        DispathingSatisfaction ObjSatisfictionBLL = new DispathingSatisfaction();       //满意度


        Dispatching ObjDispatchingBLL = new Dispatching();

        CostSum ObjCostSumBLL = new CostSum();
        int DispatchingID = 0;

        #region 页面加载
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DispatchingID = Request["DispatchingID"].ToInt32();
                BinderData();
            }
        }
        #endregion


        #region 加载方法
        private void BinderData()
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            List<FL_CostSum> DataList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5));  //执行团队 4

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1));        //供应商 1

            rptStore.DataBind(DataList.Where(C => C.RowType == 2));        //库房 2

            repBuyCost.DataBind(DataList.Where(C => C.RowType == 7));  //采购物料

            repFlowerCost.DataBind(DataList.Where(C => C.RowType == 8));  //花艺单

            List<FL_DispathingSatisfaction> ObjSacList = ObjSatisfictionBLL.GetByDispatchingID(DispatchingID);
            rptSatisfaction.DataBind(ObjSacList);
        }
        #endregion

        

    }
}