using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_DeliveryScheduleDetails : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderSupplierType();
                BinderData(sender,e);
            }
        }

        /// <summary>
        /// 绑定供应商类型
        /// </summary>
        protected void BinderSupplierType()
        {
            ddlSupplierType.DataSource = new SupplierType().GetByAll();
            ddlSupplierType.DataTextField = "TypeName";
            ddlSupplierType.DataValueField = "SupplierTypeId";
            ddlSupplierType.DataBind();
            ddlSupplierType.Items.Insert(0, "");
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
                    case "btnquery": DeliveryPager.CurrentPageIndex = 1; break;
                }
            }

            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add(!string.IsNullOrEmpty(txtSupplierName.Text), "Name_LIKE", txtSupplierName.Text.Trim());
            paramsList.Add(ddlSupplierType.SelectedValue.ToInt32() > 0, "CategoryID", ddlSupplierType.SelectedValue.ToInt32());
            paramsList.Add(new ObjectParameter("IsDelete", false));
            paramsList.Add(CreateDateRanger.IsNotBothEmpty, "StarDate_between", CreateDateRanger.Start, CreateDateRanger.End);

            int startIndex = DeliveryPager.StartRecordIndex;
            int resourceCount = 0;
            rptTestSupplier.DataBind(new Supplier().GetSupplierbyParameter(paramsList.ToArray(), DeliveryPager.PageSize, DeliveryPager.CurrentPageIndex, out resourceCount));
            DeliveryPager.RecordCount = resourceCount;
        }


        /// <summary>
        /// 获取供应商产品类别名称
        /// </summary>
        /// <param name="SupplierTypeId"></param>
        /// <returns></returns>
        protected string GetSupplierTypeName(object SupplierTypeId)
        {
            FD_SupplierType fD_SupplierType = new SupplierType().GetByID(Convert.ToInt32(SupplierTypeId));
            return fD_SupplierType != null ? fD_SupplierType.TypeName : string.Empty;
        }

        /// <summary>
        /// 获取供应商产品种类个数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        protected int GetProductCount(object SupplierID)
        {
            return new Supplier().GetProductCount(Convert.ToInt32(SupplierID));
        }

        /// <summary>
        /// 获取供应商所有产品的总供货次数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        protected int GetProductsUsedTimes(object SupplierID)
        {
            return new Supplier().GetProductsUsedTimes(Convert.ToInt32(SupplierID));
        }

        /// <summary>
        /// 获取供应商差错次数。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        protected int GetErroStateCount(object SupplierID)
        {
            return new Supplier().GetErroStateCount(Convert.ToInt32(SupplierID));
        }

        /// <summary>
        /// 获取供应商评价满意度字符串
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public string GetAveragePoint(object SupplierID)
        {
            return new Supplier().GetAveragePoint(Convert.ToInt32(SupplierID));
        }

        /// <summary>
        /// 获取供应商结算额或只获取当前月的结算额。
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="IsCurrentMonth">为 true 时，获取所有，为 false 是只获取当前月。</param>
        /// <returns></returns>
        public string GetOrderfinalCost(object SupplierID, bool IsCurrentMonth)
        {
            if (IsCurrentMonth)
            {
                return new Supplier().GetOrderfinalCost(Convert.ToInt32(SupplierID), C => true).ToString("f2");
            }
            else
            {
                return new Supplier().GetOrderfinalCost(Convert.ToInt32(SupplierID), C => C.CreateDate >= MonthStart && C.CreateDate <= MonthEnd).ToString("f2");
            }
        } 
    }
}