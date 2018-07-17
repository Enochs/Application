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
using System.Text;
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.BLLAssmblly.CS;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch
{
    public partial class CommandbyGatherSupplierManager : SystemPage
    {
        Supplier objSupplierBLL = new Supplier();

        Productcs objProductsBLL = new Productcs();
        CostforOrder objCostforOrderBLL = new CostforOrder();
        OrderAppraise objOrderAppraiseBLL = new OrderAppraise();
        ProductforDispatching objProductforDispatchingBLL = new ProductforDispatching();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }

        /// <summary>
        /// 绑定供应商名称
        /// </summary>
        protected void DataDropDownList()
        {
            ddlSupplierName.DataSource = objSupplierBLL.GetByAll();
            ddlSupplierName.DataValueField = "SupplierID";
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataBind();

        }
        /// <summary>
        /// 根据当前下拉框选择的时间生成对应的相关参数
        /// </summary>
        /// <returns></returns>
        protected string[] GetParameterDateTime()
        {
            string[] chooseDateStr = new string[4];
            if (ddlChooseYear.Items.Count == 0)
            {
                chooseDateStr[0] = (DateTime.Now.Year + "-1-1");

                chooseDateStr[1] = DateTime.Now.Year + "-12-31";
            }
            else
            {
                for (int i = 0; i < ddlChooseYear.SelectedValue.Split(',').Count(); i++)
                {
                    chooseDateStr[i] = ddlChooseYear.SelectedValue.Split(',')[i];
                }
                chooseDateStr[1] = DateTime.Now.Year + "-12-31";
            }
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();

            //上年
            chooseDateStr[2] = chooseDateStar.Year - 1 + "-1-1";
            chooseDateStr[3] = chooseDateStar.Year - 1 + "-12-31";
            return chooseDateStr;
        }

        protected void DataBinder()
        {
            string formatStr = "<td>{0}</td>";
            int supplierId = ddlSupplierName.SelectedValue.ToInt32();

            List<ObjectParameter> listParameter = new List<ObjectParameter>();
            string[] dateParameter = GetParameterDateTime();

            string supplierName = ddlSupplierName.SelectedItem != null ? ddlSupplierName.SelectedItem.Text : string.Empty;

            DateTime star = (dateParameter[0]+"01-1").ToDateTime();
            DateTime end = dateParameter[1].ToDateTime();

            DateTime preStar = DateTime.Now.AddYears(-1);
            DateTime preEnd = DateTime.Now.AddYears(-1);
            #region 供货总次数数据查询
            //所有该供应商的记录
            var objSupplierOper = objProductforDispatchingBLL.GetProductBySupplierName(supplierName);
            //当前日期的供货次数
            var objCurrentSupplierOper = objSupplierOper.Where(C => C.FinishDate >= star && C.FinishDate <= end);
              
            //上年的供货次数
            var objPreSupplierOper = objSupplierOper.Where(C => C.FinishDate >= preStar && C.FinishDate <= preEnd);
                

            //当期供货总次数
            StringBuilder sbDelivery = new StringBuilder();

            #endregion

            
            #region 结算总额

            List<FL_CostforOrder> queryAll = objCostforOrderBLL.GetBySupplierIdAll(supplierId);

            List<FL_CostforOrder> queryCurrentCost = objCostforOrderBLL.GetBySupplierIdAll(supplierId)
                .Where(C => C.CreateDate >= star && C.CreateDate <=end).ToList();
                      


            StringBuilder sbRealityCost = new StringBuilder();

            #endregion

            #region 差错次数
            //所有关于供应商的差错记录
            var objOrderAppraiseResult = objOrderAppraiseBLL.GetAllBySupplierName(supplierName);
            //当前时间的
            var objCurrentAppraiseResult = objOrderAppraiseResult.Where(C => C.CreateDate >= star && C.CreateDate <= end);
            //上年
            var objPreAppraiseResult = objOrderAppraiseResult.Where(C => C.CreateDate >= preStar && C.CreateDate <= preEnd);

            StringBuilder sbAppraise = new StringBuilder();


            #endregion

            
            #region 满意度

            StringBuilder sbDegree = new StringBuilder();
            // 不含时间段的 执行产品信息 dispatchProducts
            //当前相同的产品ID集合
            //所有客户满意度
            var objCustomerAll= objDegreeOfSatisfactionBLL.GetByAll();
            //所有供应商满意度
            var ObjDegreeResult = (from m in objSupplierOper.Where(C => C.CustomerID.HasValue) select m.CustomerID)
                .Intersect(from m in objCustomerAll.Where(C => C.CustomerID.HasValue)
                           select m.CustomerID).ToList();
            
            //当前评价时间 
            var objCurrentDegreeResult = (from m in objCurrentSupplierOper.Where(C => C.CustomerID.HasValue)select m.CustomerID)
                                          
                                          .Intersect(from m in objCustomerAll.Where(C => C.CustomerID.HasValue) 
                                                     select m.CustomerID).ToList();
            //上年评价时间  objPreSupplierOper
            var objPreDegreeResult = (from m in objPreSupplierOper.Where(C => C.CustomerID.HasValue) select m.CustomerID)
                .Intersect(from m in objCustomerAll.Where(C => C.CustomerID.HasValue) select m.CustomerID).ToList();
            //所有评价
            var queryDegree = objDegreeOfSatisfactionBLL.GetByAll();
            var currentYearDegree = queryDegree.Where(C => C.DofDate >= star && C.DofDate < end);
              

            #endregion
            //提取每年十二个月
            for (int i = 1; i <= 12; i++)
            {
                //每月供货总次数
                var singerDelivery = objCurrentSupplierOper.Where(C => C.FinishDate.Value.Month == i);
                if (singerDelivery != null)
                {

                    sbDelivery.AppendFormat(formatStr, singerDelivery.Count());
                }
                else
                {
                    sbDelivery.AppendFormat(formatStr, 0);
                }

                //每月总结算额
                var singerCost = queryCurrentCost.Where(C => C.LockDate.Month == i);

                if (singerCost != null)
                {
                    sbRealityCost.AppendFormat(formatStr, singerCost.Sum(C => C.FinishCost));
                }
                else
                {
                    sbRealityCost.AppendFormat(formatStr, 0);
                }

                //每月的差错记录
                var singerAppraise = objCurrentAppraiseResult.Where(C => C.CreateDate.Value.Month == i);
                if (singerAppraise != null)
                {

                    sbAppraise.AppendFormat(formatStr, singerAppraise.Count());
                }
                else
                {
                    sbAppraise.AppendFormat(formatStr, 0);
                }


                //每月的满意度
                var singerDegree = currentYearDegree.Where(C => C.DofDate.Value.Month == i).ToList();
                List<int> resultDegree = (from m in queryDegree select m.CustomerID.Value)
                    .Intersect(from m in objCurrentSupplierOper.Where(C=>C.CustomerID.HasValue) 
                               select m.CustomerID.Value).ToList();

                if (singerDegree != null)
                {
                    sbDegree.AppendFormat(formatStr, GetSumDofByCustomerID(singerDegree, resultDegree));
                }
                else
                {
                    sbDegree.AppendFormat(formatStr, 0);
                }
            }

            #region 供应总次数

            //当年总计
            sbDelivery.AppendFormat(formatStr, objCurrentSupplierOper.Count());
            //去年总供应
            sbDelivery.AppendFormat(formatStr, objPreSupplierOper.Count());

            //总供应商次数
            //所有产品

            sbDelivery.AppendFormat(formatStr, objSupplierOper.Count());

            ViewState["sbDelivery"] = sbDelivery.ToString();
            #endregion

            #region 结算额
            //当年结算额
            sbRealityCost.AppendFormat(formatStr, queryCurrentCost.Sum(C => C.FinishCost));
            //去年结算额
            var queryPreCost = objCostforOrderBLL.GetBySupplierIdAll(supplierId)
            .Where(C => C.CreateDate >= dateParameter[2].ToDateTime()
                   && C.CreateDate <= dateParameter[3].ToDateTime()).ToList();

            sbRealityCost.AppendFormat(formatStr, queryPreCost.Sum(C => C.FinishCost));
            //总结算额

            sbRealityCost.AppendFormat(formatStr, queryAll.Sum(C => C.FinishCost));
            ViewState["sbRealityCost"] = sbRealityCost.ToString();
            #endregion

            #region 差错记录
            //当年的差错记录

            sbAppraise.AppendFormat(formatStr, objCurrentAppraiseResult.Count());
            //去年的差错记录
            sbAppraise.AppendFormat(formatStr, objPreAppraiseResult.Count());
            //所有差错记录

            sbAppraise.AppendFormat(formatStr, objOrderAppraiseResult.Count());
            ViewState["sbAppraise"] = sbAppraise.ToString();
            #endregion

            #region 满意度

            //当年总的满意度

            sbDegree.AppendFormat(formatStr, GetSumDofByCustomerID(currentYearDegree.ToList(), objCurrentDegreeResult.Select(C => C.Value).ToList()));


            //去年总的满意度
            var preYearDegree = queryDegree.Where(C => C.DofDate >= preStar && C.DofDate <= preEnd);
            sbDegree.AppendFormat(formatStr, GetSumDofByCustomerID(preYearDegree.ToList(), objPreDegreeResult.Select(C => C.Value).ToList()));

            //所有的满意度
            sbDegree.AppendFormat(formatStr, GetSumDofByCustomerID(queryDegree.ToList(), ObjDegreeResult.Select(C=>C.Value).ToList()));
            ViewState["sbDegree"] = sbDegree.ToString();
            #endregion

        }
        

        protected string GetSumDofByCustomerID(List<CS_DegreeOfSatisfaction> queryDegree, List<int> result)
        {
            //int sum = 0;
            //foreach (var item in result)
            //{
            //    var singer = queryDegree.Where(C => C.CustomerID.Value == item).FirstOrDefault();
            //    sum += singer == null ? 0 : singer.SumDof.Value;
            //}
            return string.Empty;

        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}