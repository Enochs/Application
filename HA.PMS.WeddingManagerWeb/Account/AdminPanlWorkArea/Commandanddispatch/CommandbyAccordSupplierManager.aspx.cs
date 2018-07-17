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
    public partial class CommandbyAccordSupplierManager : SystemPage
    {
        Supplier objSupplierBLL = new Supplier();
        SupplierType objSupplierTypeBLL = new SupplierType();
        Productcs objProductsBLL = new Productcs();
        CostforOrder objCostforOrderBLL = new CostforOrder();
        OrderAppraise objOrderAppraiseBLL = new OrderAppraise();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        protected void DataBinder() 
        {
            
            #region 分页页码
            int startIndex = SupplierPager.StartRecordIndex;
            int resourceCount = 0;
            var ObjParList= new List<ObjectParameter>();
            if (txtName.Text != string.Empty)
            { 
                ObjParList.Add(new  ObjectParameter("Name_LIKE",txtName.Text));
            }
            var query = objSupplierBLL.GetSupplierbyParameter(ObjParList.ToArray(), SupplierPager.PageSize, SupplierPager.CurrentPageIndex, out resourceCount);
            SupplierPager.RecordCount = resourceCount;
            rptSupplier.DataSource = query;
            rptSupplier.DataBind();

            #endregion
           
        
        }
        /// <summary>
        /// /当期总结算额
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetRealityCostBySupplierId(object source) 
        {
            int supplierId = (source + string.Empty).ToInt32();
            string[] dateParameter = GetParameterDateTime();

            List<FL_CostforOrder> query = objCostforOrderBLL.GetBySupplierIdAll(supplierId)
                .Where(C => C.CreateDate >= dateParameter[0].ToDateTime() 
                       && C.CreateDate <= dateParameter[1].ToDateTime()).ToList();

            if (query.Count!=0)
            {
                return query.Sum(C => C.FinishCost) + string.Empty;
            }
            return "0";

        }
        /// <summary>
        /// 供应总次数
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDeliveryCount(object source) 
        {
            List<ObjectParameter> listParameter = new List<ObjectParameter>();
            string[] dateParameter = GetParameterDateTime();
            ObjectParameter objDate = new ObjectParameter("FinishDate_between", dateParameter[0].ToDateTime() + "," + dateParameter[1].ToDateTime());
            listParameter.Add(objDate);
            var dispatchProducts = objSupplierBLL.GetbyDispatchParameter(listParameter.ToArray());
            int supplierId = (source + string.Empty).ToInt32();
            var currentProducts = objProductsBLL.GetProductBySupplierID(supplierId);

            
            var result=(from m in currentProducts select m.ProductID).Intersect(from m in dispatchProducts select m.KindID.Value);
            return result.Count()+string.Empty;        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        /// <summary>
        /// 供应商差错次数
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetAppraiseBySupplier(object source) 
        {
            string[] dateParameter = GetParameterDateTime();
            int supplierId = (source + string.Empty).ToInt32();
            var queryAppraise = objOrderAppraiseBLL.GetByAll().Where(C => C.Type == 3&&C.ErroState==1
                &&(C.CreateDate >= dateParameter[0].ToDateTime() && C.CreateDate <= dateParameter[1].ToDateTime())
                ).ToList().Select(C => C.KindID);
               
            var currentProducts = objProductsBLL.GetProductBySupplierID(supplierId);
            var result = (from m in currentProducts select m.ProductID).Intersect(from m in queryAppraise select m.Value);

            return result.Count() + string.Empty;
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

            }
            DateTime chooseDateStar = chooseDateStr[0].ToDateTime();
            DateTime chooseDateEnd = chooseDateStr[1].ToDateTime();
            if (ddlChooseMonth.SelectedValue != "0")
            {
                //加入月份时间
                chooseDateStar = chooseDateStar.AddMonths(ddlChooseMonth.SelectedValue.ToInt32() - 1);

                int year = chooseDateStar.Year;
                int month = chooseDateStar.Month;

                chooseDateEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                chooseDateStr[0] = chooseDateStar + string.Empty;
                chooseDateStr[1] = chooseDateEnd + string.Empty;
            }
            //上年
            chooseDateStr[2] = chooseDateStar.Year - 1 + "-1-1";
            chooseDateStr[3] = chooseDateStar.Year - 1 + "-12-31";
            return chooseDateStr;
        }

        /// <summary>
        /// 某个供应商的满意度
        /// </summary>
        /// <returns></returns>
        protected string GetDegreeBySupplier(object source) 
        {

            List<ObjectParameter> listParameter = new List<ObjectParameter>();
            string[] dateParameter = GetParameterDateTime();
            ObjectParameter objDate = new ObjectParameter("FinishDate_between", dateParameter[0].ToDateTime() + "," + dateParameter[1].ToDateTime());
            listParameter.Add(objDate);
            //求出该时间段所管理的供应商产品信息
            var dispatchProducts = objSupplierBLL.GetbyDispatchParameter(listParameter.ToArray());

            //找到对应供应商Id 的产品
            int supplierId = (source + string.Empty).ToInt32();
            var currentProducts = objProductsBLL.GetProductBySupplierID(supplierId);
            var resultSupplier = (from m in currentProducts select m.ProductID).Intersect(from m in dispatchProducts select m.KindID.Value).ToList();
            //求出相同的产品信息
            List<FL_DispatchAllProducts> resultList = new List<FL_DispatchAllProducts>();

            foreach (var item in resultSupplier)
            {
               var singer= dispatchProducts.Where(C => C.KindID.Value == item).FirstOrDefault();
               if (singer!=null)
               {
                   resultList.Add(singer);
               }
            }

            var notCustomeres= resultList.Where(C => C.CustomerID != null);

            //根据其中的客户ID 然后算出对应的满意度，评价
            var queryDegree = objDegreeOfSatisfactionBLL.GetByAll();
            var result = (from m in queryDegree select m.CustomerID.Value).Intersect(from m in notCustomeres select m.CustomerID.Value);
            int sum=0;
            foreach (var item in result)
            {
                var singer = queryDegree.Where(C => C.CustomerID.Value == item).FirstOrDefault();
                sum += singer == null ? 0 : 1;
            }
            return sum+string.Empty;
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void SupplierPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected string GetSupplierTypeName(object SupplierTypeId)
        {
            FD_SupplierType fD_SupplierType = new SupplierType().GetByID(Convert.ToInt32(SupplierTypeId));
            return fD_SupplierType != null ? fD_SupplierType.TypeName : string.Empty;
        }
    }
}