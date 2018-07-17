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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class ProductListforWareHouse : SystemPage
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
                    return "style='background-color:#c7face;'";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 绑定供应商数据
        /// </summary>
        private void BinderData()
        {
            ///获取派工单产品

            //所有产品操作
            //AllProducts ObjAllProductsBLL = new AllProducts();
            //List<string> ObjSupplyNameList = new List<string>();
            //var ObjProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1, EmployeeID, 1);
            //ObjProductList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 2).ToList());
            //ObjProductList.AddRange(ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 1).ToList());

            //if (ObjProductList.Count > 0)
            //{
            //    foreach (var ObjProduct in ObjProductList)
            //    {
            //        ObjSupplyNameList.Add(ObjProduct.SupplierName);
            //    }
            //}
            //ObjSupplyNameList = ObjSupplyNameList.Distinct().ToList();

            //List<KeyClass> ObjSupplyList = new List<KeyClass>();
            //foreach (string name in ObjSupplyNameList)
            //{
            //    if (name != string.Empty && name != null)
            //    {
            //        Supplier ObjSupplierBLL = new Supplier();
            //        var ObjModel = ObjSupplierBLL.GetByName(name);
            //        if (ObjModel != null)
            //        {
            //            ObjSupplyList.Add(new KeyClass() { Key = 0, KeyName = name, Phone = ObjModel.TelPhone });
            //        }
            //        else
            //        {
            //            ObjSupplyList.Add(new KeyClass() { Key = 0, KeyName = name });
            //        }
            //    }
            //}


            //取得库房
            //var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2, EmployeeID, 1);
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2, EmployeeID, 1); //ObjProductforDispatchingBLL.GetProductsByCustomerIDAndRowType(Request["CustomerID"].ToInt32(), 2);
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
                    ObjDataString.Append("<Row ss:Height=\"28.5\">\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.ParentCategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.CategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.UnitPrice + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.Quantity + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.Subtotal + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmployeeID) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>");
                }
            }
            ObjTempletContent = ObjTempletContent.Replace("<=Title>", "库房");

            ObjTempletContent = ObjTempletContent.Replace("<=Source>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }


    }
}