using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class OrderDirectShow : SystemPage
    {

        int DispatchingID = 0;
        int EmployeeID = 0;
        int CustomerID = 0;
        int OrderID = 0;
        int CostKey = 0;
        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();


        Cost ObjCostBLL = new Cost();


        Employee ObjEmployeeBLL = new Employee();

        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        CostforEmpLoyee ObjCostforEmpLoyeeBLL = new CostforEmpLoyee();

        Dispatching ObjDispatchingBLL = new Dispatching();

        CostforOrder ObjCostforOrderBLL = new CostforOrder();
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            if (!IsPostBack)
            {
                var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);
                //if (ObjDisModel.IsCost != true)
                //{
                //    CostKey = ObjCostBLL.Insert(new FL_Cost()
                //    {
                //        OrderID = OrderID,
                //        CreateDate = DateTime.Now,
                //        CreateEmpLoyee = User.Identity.Name.ToInt32(),
                //        CustomerID = CustomerID
                //    });
                //    CreateData();
                //}
                BinderData();
            }
        }

        #region MyRegion


        public class KeyClassEquers : IEqualityComparer<KeyClass>
        {

            public bool Equals(KeyClass x, KeyClass y)
            {
                if (x.Key == y.Key && x.KeyName == y.KeyName)
                    return true;
                else
                    return false;
            }
            public int GetHashCode(KeyClass obj)
            {
                return 0;
            }


        }
        public class KeyClass
        {
            public int Key { get; set; }

            public string KeyName { get; set; }
        }


        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetProductByID(object Key)
        {
            if (Key != null)
            {
                return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetCategoryByID(object Key)
        {
            if (Key != null)
            {
                return OjbCategoryBLL.GetByID(Key.ToString().ToInt32()).CategoryName;
            }
            else
            {
                return string.Empty;
            }
        }



        public string GetBorderStyle(object IsNewAdd)
        {
            if (IsNewAdd != null)
            {
                if (IsNewAdd.ToString() != string.Empty && IsNewAdd.ToString() == "True")
                {
                    return "style='background-color:#c7face;'";
                }
            }

            return string.Empty;
        }

        #endregion
        private void BinderData()
        {
            this.repEmpLoyeeCostList.DataSource = ObjCostforEmpLoyeeBLL.GetByOrderID(OrderID);
            this.repEmpLoyeeCostList.DataBind();

            AllProducts ObjAllProductsBLL = new AllProducts();
            var ObjProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1);
            List<int> ProductKeyList = new List<int>();
            //获取所有产品ID 用于查询供应商关联
            foreach (var Objitem in ObjProductList)
            {
                if (Objitem.ProductID != null)
                {
                    ProductKeyList.Add(Objitem.ProductID.Value);
                }
            }

            var ObjSupplierProductList = ObjAllProductsBLL.GetGetSupplierProduct(ProductKeyList.ToArray());
            List<KeyClass> ObjSupplierKeyList = new List<KeyClass>();
            //获取所有供应商
            foreach (var ObjSupplieritem in ObjSupplierProductList)
            {
                ObjSupplierKeyList.Add(new KeyClass() { Key = ObjSupplieritem.SupplierID, KeyName = ObjSupplieritem.Name });
            }
            ObjSupplierKeyList = ObjSupplierKeyList.Distinct(new KeyClassEquers()).ToList();


            this.repTypeList.DataSource = ObjSupplierKeyList;
            this.repTypeList.DataBind();


            for (int i = 0; i < repTypeList.Items.Count; i++)
            {
                HiddenField objHideKey = repTypeList.Items[i].FindControl("hideKey") as HiddenField;
                if (objHideKey != null)
                {
                    Repeater ObjRep = repTypeList.Items[i].FindControl("repProductList") as Repeater;

                    var ALLKeys = ObjSupplierProductList.Where(C => C.SupplierID == objHideKey.Value.ToInt32());
                    List<int> ObjLiseKey = new List<int>();
                    foreach (var ObjKey in ALLKeys)
                    {
                        ObjLiseKey.Add(ObjKey.Keys);
                    }

                    // var ObjDispatchingProductList = ObjProductforDispatchingBLL.GetByKeysList(ObjLiseKey.ToArray());

                    ObjRep.DataSource = ObjCostforOrderBLL.GetBySupplierIdAll(objHideKey.Value.ToInt32());
                    ObjRep.DataBind();
                }
            }
        }

        ///// <summary>
        ///// 绑定供应商数据
        ///// </summary>
        //private void CreateData()
        //{
        //    var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);
        //    if (ObjDisModel.IsCost != true)
        //    {
        //        var DispatchingEmpLoyee = ObjEmployeeBLL.GetByEmpLoyeeKeysList(ObjProductforDispatchingBLL.GetEmpLoyeeKeyListDispatchingID(DispatchingID).ToArray());
        //        foreach (var ObjEmployeeItem in DispatchingEmpLoyee)
        //        {
        //            FL_CostforEmpLoyee ObjCostforEmpLoyeeModel = new FL_CostforEmpLoyee();
        //            ObjCostforEmpLoyeeModel.OrderID = OrderID;
        //            ObjCostforEmpLoyeeModel.CustomerID = CustomerID;
        //            ObjCostforEmpLoyeeModel.EmpLoyeeID = ObjEmployeeItem.EmployeeID;
        //            ObjCostforEmpLoyeeModel.CostKey = CostKey;
        //            ObjCostforEmpLoyeeBLL.Insert(ObjCostforEmpLoyeeModel);
        //        }



        //        //所有产品操作
        //        AllProducts ObjAllProductsBLL = new AllProducts();
        //        var ObjProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1);
        //        List<int> ProductKeyList = new List<int>();
        //        //获取所有产品ID 用于查询供应商关联
        //        foreach (var Objitem in ObjProductList)
        //        {
        //            if (Objitem.ProductID != null)
        //            {
        //                ProductKeyList.Add(Objitem.ProductID.Value);
        //            }
        //        }

        //        var ObjSupplierProductList = ObjAllProductsBLL.GetGetSupplierProduct(ProductKeyList.ToArray());
        //        List<KeyClass> ObjSupplierKeyList = new List<KeyClass>();
        //        //获取所有供应商
        //        foreach (var ObjSupplieritem in ObjSupplierProductList)
        //        {
        //            ObjSupplierKeyList.Add(new KeyClass() { Key = ObjSupplieritem.SupplierID, KeyName = ObjSupplieritem.Name });
        //        }
        //        ObjSupplierKeyList = ObjSupplierKeyList.Distinct(new KeyClassEquers()).ToList();


        //        this.repTypeList.DataSource = ObjSupplierKeyList;
        //        this.repTypeList.DataBind();


        //        for (int i = 0; i < repTypeList.Items.Count; i++)
        //        {
        //            HiddenField objHideKey = repTypeList.Items[i].FindControl("hideKey") as HiddenField;
        //            if (objHideKey != null)
        //            {


        //                var ALLKeys = ObjSupplierProductList.Where(C => C.SupplierID == objHideKey.Value.ToInt32());
        //                List<int> ObjLiseKey = new List<int>();
        //                foreach (var ObjKey in ALLKeys)
        //                {
        //                    ObjLiseKey.Add(ObjKey.Keys);
        //                }

        //                var ObjDispatchingProductList = ObjProductforDispatchingBLL.GetByKeysList(ObjLiseKey.ToArray());
        //                foreach (var ObjModelItem in ObjDispatchingProductList)
        //                {
        //                    FL_CostforOrder ObjCostModel = new FL_CostforOrder();
        //                    ObjCostModel.CostKey = CostKey;

        //                    ObjCostModel.SupplierID = objHideKey.Value.ToInt32();
        //                    ObjCostModel.ParentCategoryID = ObjModelItem.ParentCategoryID;
        //                    ObjCostModel.CategoryID = ObjModelItem.CategoryID;
        //                    ObjCostModel.CategoryName = ObjModelItem.CategoryName;
        //                    ObjCostModel.ParentCategoryName = ObjModelItem.ParentCategoryName;
        //                    ObjCostModel.OrderID = OrderID;
        //                    ObjCostModel.CustomerID = CustomerID;
        //                    ObjCostModel.ProductID = ObjModelItem.ProductID;
        //                    ObjCostModel.NewAdd = ObjModelItem.NewAdd;
        //                    ObjCostforOrderBLL.Insert(ObjCostModel);
        //                }
        //                //ObjRep.DataSource = 
        //                //ObjRep.DataBind();
        //            }
        //        }

        //        ObjDisModel.IsCost = true;
        //        ObjDispatchingBLL.Update(ObjDisModel);
        //    }
        //    // //循环供应商绑定
        //    //foreach (var SupplierKey in ObjSupplierKeyList)
        //    //{ 

        //    ////}

        //    ////取得库房
        //    //var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2, EmployeeID);

        //    //repWareHouseList.DataSource = ObjProdustList;
        //    //repWareHouseList.DataBind();
        //}

        protected void repTypeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void repTypeList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //CostforOrder ObjCostforOrderBLL = new CostforOrder();
            //if (e.CommandName == "SaveItem")
            //{
            //    Repeater ObjRep = e.Item.FindControl("repProductList") as Repeater;
            //    for (int i = 0; i < ObjRep.Items.Count; i++)
            //    {
            //        var ObjDataItem = (FL_ProductforDispatching)ObjRep.Items[i].DataItem;
            //        FL_CostforOrder ObjCostModel = new FL_CostforOrder();
            //        ObjCostModel.RealityCost = (ObjRep.Items[i].FindControl("txtRealityCost") as TextBox).Text.ToDecimal();
            //        ObjCostModel.PlanCost = (ObjRep.Items[i].FindControl("txtRealityCost") as TextBox).Text.ToDecimal();
            //        ObjCostModel.InsideRemark = (ObjRep.Items[i].FindControl("txtInsideRemark") as TextBox).Text;
            //        ObjCostModel.SupplierID = (e.Item.FindControl("hideKey") as HiddenField).Value.ToInt32();
            //        ObjCostModel.Rmark = ObjDataItem.Remark;
            //        ObjCostModel.ProductID = ObjDataItem.ProductID;
            //        ObjCostModel.CategoryID = ObjDataItem.CategoryID;
            //        ObjCostModel.ParentCategoryID = ObjCostModel.ParentCategoryID;
            //        ObjCostModel.CreateDate = DateTime.Now;
            //        ObjCostModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
            //        ObjCostModel.CustomerID = CustomerID;
            //        ObjCostModel.OrderID = OrderID;
            //        ObjCostModel.CostKey = CostKey;
            //        ObjCostforOrderBLL.Insert(ObjCostModel);
            //    }
            //}
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEmpLoyeeChange_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repEmpLoyeeCostList.Items.Count; i++)
            {
                FL_CostforEmpLoyee ObjEmployeeModel = (FL_CostforEmpLoyee)repEmpLoyeeCostList.Items[i].DataItem;
                ObjEmployeeModel.InsideRemark = (repEmpLoyeeCostList.Items[i].FindControl("txtInsideRemark") as TextBox).Text;
                ObjEmployeeModel.AccountClass = (repEmpLoyeeCostList.Items[i].FindControl("txtAccountClass") as TextBox).Text;
                ObjEmployeeModel.Cost = (repEmpLoyeeCostList.Items[i].FindControl("txtCost") as TextBox).Text.ToDecimal();
                ObjCostforEmpLoyeeBLL.Update(ObjEmployeeModel);
            }
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < repTypeList.Items.Count; i++)
            //{
            //    Repeater ObjRep = repTypeList.Items[i].FindControl("repProductList") as Repeater;
            //    for (int P = 0; P < ObjRep.Items.Count; P++)
            //    {
            //        FL_CostforOrder ObjCostModel = new FL_CostforOrder();
            //        ObjCostModel = (FL_CostforOrder)ObjRep.Items[P].DataItem;
            //        ObjCostModel.InsideRemark = (repEmpLoyeeCostList.Items[i].FindControl("txtInsideRemark") as TextBox).Text;
            //        ObjCostModel.PlanCost = (repEmpLoyeeCostList.Items[i].FindControl("txtPlanCost") as TextBox).Text.ToDecimal();
            //        ObjCostModel.RealityCost = (repEmpLoyeeCostList.Items[i].FindControl("txtRealityCost") as TextBox).Text.ToDecimal();
            //        ObjCostforOrderBLL.Update(ObjCostModel);
            //    }
            //}
        }


    }
    
}