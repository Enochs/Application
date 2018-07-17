using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs
{
    public partial class SupplierCostList : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender,e);
            }

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BinderData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": CtrPageIndex.CurrentPageIndex = 1; break;
                }
            }

            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add(!string.Empty.Equals(txtSupplierName.Text.Trim()), "SupplierName_LIKE", txtSupplierName.Text.Trim());

            int SourceCount = 0;
            repCustomer.DataBind(new Dispatching().GetSupplierforCarrytaskDistinct(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, paramsList, PartyDateRanger.Start, PartyDateRanger.End));
            //IEnumerable<int> CustomerIDs=new Customers().Where(C=>C.PartyDate>=PartyDateRanger.Start&&C.PartyDate<=PartyDateRanger.End).Select(C=>C.CustomerID);
            //repCustomer.DataBind(new Dispatching().GetPagedProductforDispatchingDistinct(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, true, C => C.DispatchingID, (C, D) => C.CustomerID.Value == D.CustomerID.Value, C => CustomerIDs.Contains(C.CustomerID.Value), paramsList));
            CtrPageIndex.RecordCount = SourceCount;
        }

        /// <summary>
        /// 获取供应商在指定执行订单中的付款总额
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="SupplierName"></param>
        /// <returns></returns>
        protected string GetSupplierFinalCostsByDispatchingID(object DispatchingID, object SupplierName)
        {
            return new OrderfinalCost().GetByCustomerID(GetCustomerByDispatchingID(DispatchingID).CustomerID).Where(C => C.KindType == 1 && C.ServiceContent.Equals(SupplierName) && C.KindID == Convert.ToInt32(DispatchingID)).Sum(C => C.ActualExpenditure).ToString("f2");
        }

        /// <summary>
        /// 根据供应商名称获取供应商 ID
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        protected int GetSupplierID(object Name)
        {
            FD_Supplier fD_Supplier = new Supplier().GetByName(Name + string.Empty);
            return fD_Supplier != null ? fD_Supplier.SupplierID : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        protected FL_Customers GetCustomerByDispatchingID(object DispatchingID)
        {
            return new Customers().GetByID(new Dispatching().GetByID(Convert.ToInt32( DispatchingID)).CustomerID);
        }
    }
}