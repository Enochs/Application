using System;
using System.Collections.Generic;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseMarkProductsDetails : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        protected string GetNewAddStyle(object NewAdd)
        {
            return Convert.ToBoolean(NewAdd) ? "style='background-color:#c7face;'" : string.Empty;
        }

        protected void BinderData()
        {
            List<HA.PMS.DataAssmblly.FL_ProductforDispatching> fL_ProductforDispatchingList = new ProductforDispatching().GetMarkProductsByCustomerID(Convert.ToInt32(Request["CustomerID"]));
            repWareHouseList.DataBind(fL_ProductforDispatchingList);
            lblWareSumPrice.Text = fL_ProductforDispatchingList.Sum(C => C.Subtotal.Value).ToString("f2");
        }

        protected void btnExportformWareHouse_Click(object sender, EventArgs e)
        {
            List<HA.PMS.DataAssmblly.FL_ProductforDispatching> fL_ProductforDispatchingList = new ProductforDispatching().GetMarkProductsByCustomerID(Convert.ToInt32(Request["CustomerID"]));

            System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplilyProductModel.xml"));
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            string content = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            
            if (fL_ProductforDispatchingList.Count() > 0)
            {
                foreach (var fL_ProductforDispatching in fL_ProductforDispatchingList)
                {
                    stringBuilder.Append("<Row ss:Height=\"28.5\">\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.ParentCategoryName + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.CategoryName + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.ServiceContent + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.Requirement + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.UnitPrice + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.Quantity + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.Subtotal + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + GetEmployeeName(fL_ProductforDispatching.EmployeeID) + "</Data></Cell>\r\n");
                    stringBuilder.Append(" <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">" + fL_ProductforDispatching.Remark + "</Data></Cell>\r\n");
                    stringBuilder.Append("</Row>");
                }
            }
            content = content.Replace("<=Title>", "库房");

            content = content.Replace("<=Source>", stringBuilder.ToString());
            HA.PMS.ToolsLibrary.IOTools.DownLoadByString(content, "xls");
        }

    }
}