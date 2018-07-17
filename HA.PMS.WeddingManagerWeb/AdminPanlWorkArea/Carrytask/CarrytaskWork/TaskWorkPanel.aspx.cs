using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class TaskWorkPanel : SystemPage
    {
        //派工
        ProductforDispatching ObjProductForDispatchingBLL = new ProductforDispatching();
        //调度
        Dispatching ObjDispatchingBLL = new Dispatching();

        //设计清单
        HA.PMS.BLLAssmblly.Flow.Designclass ObjDesignclassBLL = new BLLAssmblly.Flow.Designclass();
        //成本
        CostSum ObjCostSumBLL = new CostSum();

        //策划报价
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var Model = ObjQuotedPriceBLL.GetByOrderId(Request["OrderID"].ToInt32());
                if (Model != null)
                {
                    if (Model.Director != null || Model.Director.ToString() != "")
                    {
                        //txtEmpLoyee.Value = GetEmployeeName(Model.Director);
                    }
                }
            }
        }
        #endregion


        #region 隐藏变更单(没有做过变更单)
        public string HideChange()
        {
            var ObjList = ObjProductForDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());
            if (ObjList.Where(C => C.IsFirstMakes >= 1 && C.IsFirstMakes != null).ToList().Count >= 1)      //做过变更单
            {
                return string.Empty;
            }
            else            //没做过变更单
            {
                return "style='display:none;'";
            }
        }
        #endregion

        //#region 改派工程主管
        ///// <summary>
        ///// 改派工程主管
        ///// </summary>
        //protected void btnSaveDesigner_Click(object sender, EventArgs e)
        //{
        //    int EmployeeID = hideEmpLoyeeID.Value.ToString().ToInt32();
        //    var DataList = ObjProductForDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32());
        //    var Model = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
        //    var QuotedModel = ObjQuotedPriceBLL.GetByOrderId(Request["OrderID"].ToInt32());
        //    int result = 0;
        //    if (Model != null || QuotedModel != null)
        //    {
        //        SaveDesignCost(QuotedModel, EmployeeID);
        //        if (Model != null)
        //        {
        //            Model.Director = EmployeeID;
        //            result = ObjDispatchingBLL.Update(Model);
        //            if (DataList.Count > 0)
        //            {
        //                foreach (var item in DataList)
        //                {
        //                    item.DirectorEmployee = EmployeeID;
        //                    ObjProductForDispatchingBLL.Update(item);
        //                }
        //            }
        //        }
        //        if (QuotedModel != null)
        //        {
        //            QuotedModel.Director = EmployeeID;
        //            result += ObjQuotedPriceBLL.Update(QuotedModel);
        //        }

        //    }
        //    if (result >= 2)
        //    {
        //        JavaScriptTools.AlertWindow("成功分派工程主管", Page);
        //    }


        //}
        //#endregion



        //#region 添加工程主管成本
        ///// <summary>
        ///// 添加成本
        ///// </summary>
        //public void SaveDesignCost(FL_QuotedPrice QuotedModel, int Designer = 1)
        //{
        //    FL_CostSum CostSum = new FL_CostSum();

        //    CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
        //    CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
        //    CostSum.ShortCome = "";
        //    CostSum.Advance = "";
        //    CostSum.OrderID = Request["OrderID"].ToInt32();
        //    CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
        //    CostSum.CustomerId = Request["CustomerID"].ToInt32();
        //    CostSum.Name = GetEmployeeName(Designer);
        //    CostSum.RowType = 6;
        //    CostSum.Content = "";
        //    CostSum.CategoryName = "";
        //    CostSum.Sumtotal = 100;
        //    CostSum.ActualSumTotal = 100;
        //    CostSum.Evaluation = 6;
        //    CostSum.EmployeeID = User.Identity.Name.ToInt32();

        //    var Model = ObjCostSumBLL.GetByCheckID(GetEmployeeName(Designer), Request["DispatchingID"].ToInt32(), 6);       //工程主管(6 也可代表设计师)
        //    if (Model == null)          //实体为null   没添加过 (就可以新增 添加)  确保不重复添加
        //    {
        //        if (QuotedModel.Director != null || QuotedModel.Director.ToString() != "")          //不是null 或者不等于空  就说明已经存过了
        //        {
        //            if (QuotedModel.Director != Designer)       //之前的工程主管和现在选择的工程主管不一样  就是修改
        //            {
        //                CostSum = ObjCostSumBLL.GetByCheckID(GetEmployeeName(QuotedModel.Director), Request["DispatchingID"].ToInt32(), 6);
        //                CostSum.Name = GetEmployeeName(Designer);
        //                ObjCostSumBLL.Update(CostSum);
        //            }
        //        }
        //        else                        //第一次增加
        //        {
        //            ObjCostSumBLL.Insert(CostSum);
        //        }
        //    }
        //}

        //#endregion
    }
}