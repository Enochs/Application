using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using System.IO;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class ProductList : SystemPage
    {
        int DispatchingID = 0;
        int EmployeeID = 0;
        int CustomerID = 0;

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

        Customers ObjCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }

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

            public string Phone { get; set; }
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
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 绑定供应商数据
        /// </summary>
        private void BinderData()
        {
            AllProducts ObjAllProductsBLL = new AllProducts();
            List<string> ObjSupplyNameList = new List<string>();

            var ObjProductList = new List<FL_ProductforDispatching>();

            ///获取派工单产品
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjDispatchingModel = ObjDispatchingBLL.GetByID(DispatchingID);
            if (ObjDispatchingModel.EmployeeID == User.Identity.Name.ToInt32())
            {
                ObjProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1, 1);
                ObjProductList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 2).ToList());
                ObjProductList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 1).ToList());
            }
            else
            {
                ObjProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, User.Identity.Name.ToInt32(), 1, 1);
                ObjProductList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 2).Where(C => C.EmployeeID == EmployeeID).ToList());
                ObjProductList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 1).Where(C => C.EmployeeID == EmployeeID).ToList());
            }
            //所有产品操作


            if (ObjProductList.Count > 0)
            {
                foreach (var ObjProduct in ObjProductList)
                {
                    ObjSupplyNameList.Add(ObjProduct.SupplierName);
                }
            }

            //取得所有供应商
            ObjSupplyNameList = ObjSupplyNameList.Distinct().ToList();


            //实例化供应商
            List<KeyClass> ObjSupplyList = new List<KeyClass>();

            //循环添加进入绑定类
            foreach (string name in ObjSupplyNameList)
            {
                if (name != string.Empty && name != null)
                {
                    Supplier ObjSupplierBLL = new Supplier();
                    var ObjModel = ObjSupplierBLL.GetByName(name);
                    if (ObjModel != null)
                    {
                        ObjSupplyList.Add(new KeyClass() { Key = 0, KeyName = name, Phone = ObjModel.TelPhone });
                    }
                    else
                    {
                        ObjSupplyList.Add(new KeyClass() { Key = 0, KeyName = name });
                    }
                }
            }

            //绑定供应商
            repTypeList.DataSource = ObjSupplyList;
            repTypeList.DataBind();
            //绑定供应商后获取供应商提供的产品
            for (int i = 0; i < repTypeList.Items.Count; i++)
            {

                //获取产品控件
                Repeater ObjRep = repTypeList.Items[i].FindControl("repProductList") as Repeater;
                //获取供应商名字
                Label objlbl = repTypeList.Items[i].FindControl("lblKeyName") as Label;

                //根据供应商名字获取本人的责任产品
                var objDataList = ObjProductforDispatchingBLL.GetProductBySupplierName(DispatchingID, 1, EmployeeID, 1, objlbl.Text);
                //
                objDataList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 2).Where(C => C.SupplierName == objlbl.Text && C.Productproperty == 1).ToList());
                objDataList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 1).Where(C => C.SupplierName == objlbl.Text && C.Productproperty == 1).ToList());
                objDataList.Reverse();

                decimal SumPrice = 0;
                Label ObjLblSumPrice = repTypeList.Items[i].FindControl("lblPriceSum") as Label;
                foreach (var ObjItem in objDataList)
                {
                    if (ObjItem.PurchasePrice != null)
                    {
                        SumPrice += ObjItem.Subtotal.Value;
                    }
                }
                if (ObjLblSumPrice != null)
                {

                    ObjLblSumPrice.Text = SumPrice.ToString();
                }

                txtPlanCost.Text = GetPlanCostBySupplyName("库房折旧",3);

                if (objDataList.Count > 0)
                {
                    ObjRep.DataSource = objDataList;
                    ObjRep.DataBind();
                }
                else
                {
                    PlaceHolder ph = repTypeList.Items[i].FindControl("phSupplierItem") as PlaceHolder;
                    ph.Visible = false;
                }
            }



            //取得库房
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2, EmployeeID, 1);
            //ObjProdustList.AddRange(ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2, EmployeeID, 0));
            decimal WareSumPrice = 0;

            foreach (var ObjItem in ObjProdustList)
            {
                if (ObjItem.PurchasePrice != null)
                {
                    WareSumPrice += ObjItem.Subtotal.Value;
                }
            }


            lblWareSumPrice.Text = WareSumPrice.ToString();

            repWareHouseList.DataSource = ObjProdustList;
            repWareHouseList.DataBind();


            //获取新购入产品 0 和 1
            var ObjNewProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 3, EmployeeID, 1);
            ObjNewProductList.AddRange(ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 3, EmployeeID, 0));
            WareSumPrice = 0;

            foreach (var ObjItem in ObjNewProductList)
            {
                if (ObjItem.PurchasePrice != null)
                {
                    WareSumPrice += ObjItem.Subtotal.Value;
                }
            }


            lblNewaddSum.Text = WareSumPrice.ToString();

            repNewadd.DataSource = ObjNewProductList;
            repNewadd.DataBind();

        }





        /// <summary>
        /// 绑定供应商供应明细
        /// </summary>
        /// <param name="KindKey"></param>
        /// <param name="Type"></param>
        private void BinderBySupplierID(int? KindKey, int? Type)
        {



            this.repTypeList.DataBind();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 根据供应商 绑定产品列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repTypeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }


        /// <summary>
        /// 保存计划支出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavePlanCost_Click(object sender, EventArgs e)
        {




        }


        /// <summary>
        /// 绑定计划支出
        /// </summary>
        /// <param name="SupplilyName"></param>
        /// <returns></returns>
        public string GetPlanCostBySupplyName(string SupplilyName)
        {
            OrderfinalCost ObjOrderfinalCost = new OrderfinalCost();
            var ObjModel = ObjOrderfinalCost.GetBySupplilyName(SupplilyName, DispatchingID);
            if (ObjModel != null)
            {
                return ObjModel.PlannedExpenditure.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetPlanCostBySupplyName(string SupplilyName, int RowType)
        {
            OrderfinalCost ObjOrderfinalCost = new OrderfinalCost();
            var ObjModel = ObjOrderfinalCost.GetBySupplilyName(SupplilyName, DispatchingID, RowType);
            if (ObjModel != null)
            {
                return ObjModel.PlannedExpenditure.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 保存供应商计划支出到财务
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repTypeList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SavPlanCost")
            {
                OrderfinalCost ObjOrderfinalCost = new OrderfinalCost();
                var ObjModel = ObjOrderfinalCost.GetBySupplilyName(e.CommandArgument.ToString(), DispatchingID);
                if (ObjModel != null)
                {
                    ObjModel.PlannedExpenditure = ((TextBox)e.Item.FindControl("txtPlanCost")).Text.ToDecimal();


                    ObjOrderfinalCost.Update(ObjModel);
                    JavaScriptTools.AlertWindow("已经保存数据到财务处!", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("暂未找到数据!", Page);
                }
            }


            if (e.CommandName == "ExporttoExcel")
            {

                Label objlbl = e.Item.FindControl("lblKeyName") as Label;
                var objDataList = ObjProductforDispatchingBLL.GetProductBySupplierName(DispatchingID, 1, EmployeeID, 1, objlbl.Text);

                objDataList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 2).Where(C => C.SupplierName == objlbl.Text).ToList());
                objDataList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 1).Where(C => C.SupplierName == objlbl.Text).ToList());
                objDataList.Reverse();
                StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplilyProductModel.xml"));
                System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();


                string ObjTempletContent = Objreader.ReadToEnd();
                Objreader.Close();
                if (objDataList.Count > 0)
                {
                    foreach (var ObjDataItem in objDataList)
                    {
                        ObjDataString.Append(" <Row ss:AutoFitHeight=\"0\">\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.ParentCategoryName + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.CategoryName + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.UnitPrice + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Quantity + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Subtotal + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmployeeID) + "</Data></Cell>\r\n");
                        ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                        ObjDataString.Append("</Row>");
                    }
                }

                ObjTempletContent = ObjTempletContent.Replace("<=Source>", ObjDataString.ToString());
                ObjTempletContent = ObjTempletContent.Replace("<=Title>", objlbl.Text);


                var ObjCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
                Order ObjOrderBLL = new Order();
                var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(Request["CustomerID"].ToInt32());
                ObjTempletContent = ObjTempletContent
                    .Replace("<=OrderCoder>", ObjOrderModel.OrderCoder)
                    .Replace("<=Wineshop>", ObjCustomerModel.Wineshop)
                    .Replace("<=PartyDate>", ObjCustomerModel.PartyDate.Value.ToShortDateString())
                    .Replace("<=Groom>", ObjCustomerModel.Groom)
                    .Replace("<=GroomCellPhone>", ObjCustomerModel.GroomCellPhone)
                    .Replace("<=Bride>", ObjCustomerModel.Bride)
                    .Replace("<=BrideCellPhone>", ObjCustomerModel.BrideCellPhone);

                IOTools.DownLoadByString(ObjTempletContent, "xls");
            }
        }


        /// <summary>
        /// 导出领料单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportformWareHouse_Click(object sender, EventArgs e)
        {
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2, EmployeeID, 1);

            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplilyProductModel.xml"));
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();


            string ObjTempletContent = Objreader.ReadToEnd();
            Objreader.Close();
            if (ObjProdustList.Count > 0)
            {
                foreach (var ObjDataItem in ObjProdustList)
                {
                    ObjDataString.Append(" <Row ss:AutoFitHeight=\"0\">\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.ParentCategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.CategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.UnitPrice + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Quantity + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Subtotal + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmployeeID) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>");
                }
            }
            ObjTempletContent = ObjTempletContent.Replace("<=Title>", "库房");

            ObjTempletContent = ObjTempletContent.Replace("<=Source>", ObjDataString.ToString());

            var ObjCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            Order ObjOrderBLL = new Order();
            var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(Request["CustomerID"].ToInt32());
            ObjTempletContent = ObjTempletContent
                .Replace("<=OrderCoder>", ObjOrderModel.OrderCoder)
                .Replace("<=Wineshop>", ObjCustomerModel.Wineshop)
                .Replace("<=PartyDate>", ObjCustomerModel.PartyDate.Value.ToShortDateString())
                .Replace("<=Groom>", ObjCustomerModel.Groom)
                .Replace("<=GroomCellPhone>", ObjCustomerModel.GroomCellPhone)
                .Replace("<=Bride>", ObjCustomerModel.Bride)
                .Replace("<=BrideCellPhone>", ObjCustomerModel.BrideCellPhone);

            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }

        protected void btnEcportforNewadd_Click(object sender, EventArgs e)
        {

            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 3, EmployeeID, 1);

            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplilyProductModel.xml"));
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();


            string ObjTempletContent = Objreader.ReadToEnd();
            Objreader.Close();
            if (ObjProdustList.Count > 0)
            {
                foreach (var ObjDataItem in ObjProdustList)
                {
                    ObjDataString.Append(" <Row ss:AutoFitHeight=\"0\">\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.ParentCategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.CategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.UnitPrice + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Quantity + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Subtotal + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmployeeID) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s65\"><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>");
                }
            }
            ObjTempletContent = ObjTempletContent.Replace("<=Title>", "新购入");

            ObjTempletContent = ObjTempletContent.Replace("<=Source>", ObjDataString.ToString());

            var ObjCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            Order ObjOrderBLL = new Order();
            var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(Request["CustomerID"].ToInt32());
            ObjTempletContent = ObjTempletContent
                .Replace("<=OrderCoder>", ObjOrderModel.OrderCoder)
                .Replace("<=Wineshop>", ObjCustomerModel.Wineshop)
                .Replace("<=PartyDate>", ObjCustomerModel.PartyDate.Value.ToShortDateString())
                .Replace("<=Groom>", ObjCustomerModel.Groom)
                .Replace("<=GroomCellPhone>", ObjCustomerModel.GroomCellPhone)
                .Replace("<=Bride>", ObjCustomerModel.Bride)
                .Replace("<=BrideCellPhone>", ObjCustomerModel.BrideCellPhone);

            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }


        /// <summary>
        /// 保存库房计划支出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavePlanCost_Click1(object sender, EventArgs e)
        {
            OrderfinalCost ObjOrderfinalCostBLL = new OrderfinalCost();
            Cost ObjCostBLL = new Cost();
            var Item = GetPlanCostBySupplyName("库房折旧",3);
            if (Item == string.Empty)
            {

                var ObjFinalCostModel = new FL_OrderfinalCost();
                ObjFinalCostModel.KindType = 3;
                ObjFinalCostModel.IsDelete = false;
                ObjFinalCostModel.CostKey = ObjCostBLL.GetByCustomerID(CustomerID).CostKey;
                ObjFinalCostModel.CreateDate = DateTime.Now;
                ObjFinalCostModel.CustomerID = CustomerID;
                ObjFinalCostModel.CellPhone = string.Empty;
                ObjFinalCostModel.InsideRemark = string.Empty;

                Dispatching ObjDispatchingBLL = new Dispatching();

                var ObjSetModel = ObjDispatchingBLL.GetByID(DispatchingID);
                if (ObjSetModel.ParentDispatchingID == 0)
                {

                    ObjFinalCostModel.KindID = ObjSetModel.DispatchingID;
                }
                else
                {
                    ObjFinalCostModel.KindID = ObjSetModel.ParentDispatchingID.Value;
                }
                ObjFinalCostModel.KindID = DispatchingID;
                ObjFinalCostModel.ServiceContent = "库房折旧";
                ObjFinalCostModel.PlannedExpenditure = txtPlanCost.Text.ToDecimal();
                ObjFinalCostModel.ActualExpenditure = 0;
                ObjFinalCostModel.Expenseaccount = string.Empty;

                ObjFinalCostModel.ActualWorkload = string.Empty;
                ObjOrderfinalCostBLL.Insert(ObjFinalCostModel);
            }
            else
            {

                var PlanModel = ObjOrderfinalCostBLL.GetBySupplilyName("库房折旧", DispatchingID, 3);

                PlanModel.PlannedExpenditure = txtPlanCost.Text.ToDecimal();
                ObjOrderfinalCostBLL.Update(PlanModel);
            }
        }
    }
}