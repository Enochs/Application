using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport
{
    public partial class SupplierReport : SystemPage
    {

        int DispatchingID = 0;
        int EmployeeID = 0;

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
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

            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            List<KeyClass> ObjListSupplier = new List<KeyClass>();


            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1, EmployeeID);
            foreach (var Objitem in ObjProdustList)
            {
                ObjListSupplier.Add(new KeyClass() { Key = Objitem.SupplierID.Value, KeyName = Objitem.SupplierName });
            }
            ObjListSupplier = ObjListSupplier.Distinct(new KeyClassEquers()).ToList();
            repTypeList.DataSource = ObjListSupplier;
            repTypeList.DataBind();
            for (int i = 0; i < repTypeList.Items.Count; i++)
            {
                HiddenField objHideKey = repTypeList.Items[i].FindControl("hideKey") as HiddenField;
                if (objHideKey != null)
                {
                    Repeater ObjRep = repTypeList.Items[i].FindControl("repProductList") as Repeater;
                    int Key = objHideKey.Value.ToInt32();
                    var DataSource = ObjProdustList.Where(C => C.SupplierID == Key);
                    Label Objlbl = (repTypeList.Items[i].FindControl("lblSumTotal") as Label);
                    decimal TotalPrice = 0;
                    foreach (var ObjItem in DataSource)
                    {
                        TotalPrice += ObjItem.Subtotal.Value;
                    }
                    Objlbl.Text = TotalPrice.ToString();
                    ObjRep.DataSource = DataSource;
                    ObjRep.DataBind();
                }
            }


        }

        protected void repTypeList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            if (e.CommandName == "ExporttoExcel")
            {
                Customers ObjCustomersBLL = new Customers();

                Label objlbl = e.Item.FindControl("lblKeyName") as Label;
                var objDataList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1, EmployeeID).Where(C => C.SupplierName == e.CommandArgument.ToString());

                objDataList.Reverse();
                StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplilyProductModel.xml"));
                System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();


                string ObjTempletContent = Objreader.ReadToEnd();
                Objreader.Close();
                if (objDataList.Count() > 0)
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

            if (e.CommandName == "SaveCost")
            {
                Repeater ObjRep = e.Item.FindControl("repProductList") as Repeater;

                decimal SumPrice=0;
                int RowType=0;
                //ProeuctKey
                for (int i = 0; i < ObjRep.Items.Count; i++)
                {
                    var UpdateModel = ObjProductforDispatchingBLL.GetByID((ObjRep.Items[i].FindControl("hideKey") as HiddenField).Value.ToInt32());
                    UpdateModel.UnitPrice = (ObjRep.Items[i].FindControl("txtUnitPrice") as TextBox).Text.ToDecimal();
                    UpdateModel.Quantity = (ObjRep.Items[i].FindControl("txtQuantity") as TextBox).Text.ToInt32();
                    UpdateModel.Subtotal = (ObjRep.Items[i].FindControl("txtSubtotal") as TextBox).Text.ToDecimal();
                    UpdateModel.Remark = (ObjRep.Items[i].FindControl("txtRemark") as TextBox).Text;
                    ObjProductforDispatchingBLL.Update(UpdateModel);
                    SumPrice += UpdateModel.Subtotal.Value;
                    RowType=UpdateModel.RowType.Value;
                }
                CostforOrder ObjCost = new CostforOrder();
                ObjCost.Insert(new DataAssmblly.FL_CostforOrder() { 
                DispatchingID=DispatchingID,
                CustomerID=Request["CustomerID"].ToInt32(),
                CreateDate=DateTime.Now,
                Lock=false,
                CreateEmpLoyee=User.Identity.Name.ToInt32(),
                Name=(e.Item.FindControl("lblKeyName") as Label).Text,
                Node="",
                PlanCost = SumPrice,
                RowType = RowType,
                FinishCost = (e.Item.FindControl("txtFinishCost") as TextBox).Text.ToDecimal(),
                LockDate=DateTime.Now.AddYears(-50),
                LockEmployee=-1,
                OrderID=-1,
                
                });

                string NamesList = string.Empty;
                for (int i = 0; i < repTypeList.Items.Count; i++)
                {
                    NamesList += ((Label)repTypeList.Items[i].FindControl("lblKeyName")).Text+",";
       
                }


                ObjCost.DeleteforNull(NamesList.Trim(',').Split(','),1);

            }
        }


       


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }






    }
}