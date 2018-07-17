using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using System.IO;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport
{
    public partial class TeamReport : SystemPage
    {
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        DispatchingEmployeeManager ObjDispatchingEmployeeManagerBLL = new DispatchingEmployeeManager();

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();
        int DispatchingID = 0;
        int CustomerID = 0;
        Customers ObjCustomersBLL = new Customers();
        Employee ObjEmployeeBLL = new Employee();


        //婚礼统筹表
        public class OrderTeam
        {
            public string Title { get; set; }


            public string Name { get; set; }



            public string Phone { get; set; }
        }


        //绑定婚礼统筹表
        private void BinderOrderTeam()
        {
            var BinderList = new List<OrderTeam>();
            BinderList.Add(new OrderTeam() { Title = "督导" });
            BinderList.Add(new OrderTeam() { Title = "副督导" });
            BinderList.Add(new OrderTeam() { Title = "策划" });
            BinderList.Add(new OrderTeam() { Title = "统筹" });
            BinderList.Add(new OrderTeam() { Title = "设计" });
            BinderList.Add(new OrderTeam() { Title = "花艺" });
            BinderList.Add(new OrderTeam() { Title = "道具" });
            BinderList.Add(new OrderTeam() { Title = "灯光" });
            BinderList.Add(new OrderTeam() { Title = "秘书" });
            BinderList.Add(new OrderTeam() { Title = "VJ" });
            BinderList.Add(new OrderTeam() { Title = "DJ" });
            BinderList.Add(new OrderTeam() { Title = "主持" });
            BinderList.Add(new OrderTeam() { Title = "化妆" });
            BinderList.Add(new OrderTeam() { Title = "摄影" });
            BinderList.Add(new OrderTeam() { Title = "摄像" });
            BinderList.Add(new OrderTeam() { Title = "工程主管" });
            this.repOrderTeamList.DataSource = BinderList;
            this.repOrderTeamList.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
                BinderOrderTeam();
                CustomerID = Request["CustomerID"].ToInt32();


            }
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
                var ObjAllProductsModel = ObjAllProductsBLL.GetByID(Key.ToString().ToInt32());
                if (ObjAllProductsModel != null)
                {
                    return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 3, User.Identity.Name.ToInt32());
            decimal SumPrice = 0;

            foreach (var ObjItem in ObjProdustList)
            {
                if (ObjItem.PurchasePrice != null)
                {
                    SumPrice += ObjItem.PurchasePrice.Value;
                }
            }
            //lblMoneySum.Text = SumPrice.ToString();
            //repProductList.DataSource = ObjProdustList;
            //repProductList.DataBind();


            ////绑定四大金刚
            //ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1, User.Identity.Name.ToInt32()).Where(C => C.Classification.Contains("人员")).ToList();
            //repjingang.DataSource = ObjProdustList;
            //repjingang.DataBind();


            this.repWeddingPlanning.DataSource = ObjDispatchingEmployeeManagerBLL.GetEmpLoyeeByDispachingID(DispatchingID);
            this.repWeddingPlanning.DataBind();
        }

        /// <summary>
        /// 保存完成时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < repProductList.Items.Count; i++)
            //{
            //    ((TextBox)repProductList.Items[i].FindControl("txtFinishDate"));
            //}
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByProductproperty(DispatchingID, 3, 0, false);

            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/TeamModel.xml"));
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();


            string ObjTempletContent = Objreader.ReadToEnd();
            Objreader.Close();
            if (ObjProdustList.Count > 0)
            {
                foreach (var ObjDataItem in ObjProdustList)
                {
                    ObjDataString.Append("<Row ss:Height=\"15\">\r\n");

                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.ParentCategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.CategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.UnitPrice + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Quantity + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Subtotal + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmployeeID) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>");
                }
            }
            ObjTempletContent = ObjTempletContent.Replace("<=Title>", "新购入");

            ObjTempletContent = ObjTempletContent.Replace("<=Source>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            FL_DispatchingEmployeeManager ObjInsertModel = new DataAssmblly.FL_DispatchingEmployeeManager();
            ObjInsertModel.DispatchingID = DispatchingID;
            ObjInsertModel.EmployeeName = txtEmployeeName.Text;
            ObjInsertModel.EmployeeType = txtEmployeeType.Text;
            ObjInsertModel.Amount = txtAmount.Text.ToDecimal();
            ObjInsertModel.Bulding = txtBulding.Text;
            ObjInsertModel.TelPhone = txtTelPhone.Text;
            ObjInsertModel.CreateDate = DateTime.Now;
            ObjInsertModel.CreateEmployee = User.Identity.Name.ToString();
            ObjDispatchingEmployeeManagerBLL.Insert(ObjInsertModel);


            txtTelPhone.Text = string.Empty;
            txtEmployeeType.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtBulding.Text = string.Empty;
            txtAmount.Text = string.Empty;
            BinderData();

        }

        /// <summary>
        /// 删除或者修改
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repWeddingPlanning_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                ObjDispatchingEmployeeManagerBLL.Delete(new FL_DispatchingEmployeeManager() { DeJey = e.CommandArgument.ToString().ToInt32() });

            }

            BinderData();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }


        /// <summary>
        /// 保存执行团队实际支出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            //decimal SumPrice = 0;
            //int RowType = 0;

            //ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            //List<FL_CostforOrder> ObjList = new List<FL_CostforOrder>();
            //string NamesList = string.Empty;
            //CostforOrder ObjCost = new CostforOrder();
            //for (int i = 0; i < repProductList.Items.Count; i++)
            //{
            //    var UpdateModel = ObjProductforDispatchingBLL.GetByID((repProductList.Items[i].FindControl("hideKey") as HiddenField).Value.ToInt32());
            //    UpdateModel.UnitPrice = (repProductList.Items[i].FindControl("txtUnitPrice") as TextBox).Text.ToDecimal();
            //    UpdateModel.Quantity = (repProductList.Items[i].FindControl("txtQuantity") as TextBox).Text.ToInt32();
            //    UpdateModel.Subtotal = (repProductList.Items[i].FindControl("txtSubtotal") as TextBox).Text.ToDecimal();
            //    UpdateModel.Remark = (repProductList.Items[i].FindControl("txtRemark") as TextBox).Text;
            //    ObjProductforDispatchingBLL.Update(UpdateModel);
            //    SumPrice += UpdateModel.Subtotal.Value;
            //    RowType = UpdateModel.RowType.Value;


            //    ObjList.Add(new DataAssmblly.FL_CostforOrder()
            //    {
            //        DispatchingID = DispatchingID,
            //        CustomerID = Request["CustomerID"].ToInt32(),
            //        CreateDate = DateTime.Now,
            //        Lock = false,
            //        CreateEmpLoyee = User.Identity.Name.ToInt32(),
            //        Name = GetEmployeeName(UpdateModel.EmployeeID),
            //        Node = GetProductByID(UpdateModel.ProductID),
            //        PlanCost = UpdateModel.Subtotal.Value,
            //        RowType = 4,
            //        FinishCost = UpdateModel.Subtotal.Value,
            //        LockDate = DateTime.Now.AddYears(-50),
            //        LockEmployee = -1,
            //        OrderID = -1,

            //    });
            //    ObjCost.InsertNoneUpdate(ObjList, DispatchingID, 4);



            //    NamesList += GetEmployeeName(UpdateModel.EmployeeID) + ",";



            //}

            //ObjCost.DeleteforNull(NamesList.Trim(',').Split(','), 4);

        }


        /// <summary>
        /// 保存四大金刚成本明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveforjingang_Click(object sender, EventArgs e)
        {
            //decimal SumPrice = 0;
            //int RowType = 0;

            //ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            //List<FL_CostforOrder> ObjList = new List<FL_CostforOrder>();
            //string NamesList = string.Empty;
            //CostforOrder ObjCost = new CostforOrder();
            //for (int i = 0; i < repjingang.Items.Count; i++)
            //{
            //    var UpdateModel = ObjProductforDispatchingBLL.GetByID((repjingang.Items[i].FindControl("hideKey") as HiddenField).Value.ToInt32());
            //    UpdateModel.UnitPrice = (repjingang.Items[i].FindControl("txtUnitPrice") as TextBox).Text.ToDecimal();
            //    UpdateModel.Quantity = (repjingang.Items[i].FindControl("txtQuantity") as TextBox).Text.ToInt32();
            //    UpdateModel.Subtotal = (repjingang.Items[i].FindControl("txtSubtotal") as TextBox).Text.ToDecimal();
            //    UpdateModel.Remark = (repjingang.Items[i].FindControl("txtRemark") as TextBox).Text;
            //    ObjProductforDispatchingBLL.Update(UpdateModel);
            //    SumPrice += UpdateModel.Subtotal.Value;
            //    RowType = UpdateModel.RowType.Value;


            //    ObjList.Add(new DataAssmblly.FL_CostforOrder()
            //    {
            //        DispatchingID = DispatchingID,
            //        CustomerID = Request["CustomerID"].ToInt32(),
            //        CreateDate = DateTime.Now,
            //        Lock = false,
            //        CreateEmpLoyee = User.Identity.Name.ToInt32(),
            //        Name = GetProductByID(UpdateModel.EmployeeID),
            //        Node = UpdateModel.ServiceContent,
            //        PlanCost = UpdateModel.Subtotal.Value,
            //        RowType = 6,
            //        FinishCost = UpdateModel.Subtotal.Value,
            //        LockDate = DateTime.Now.AddYears(-50),
            //        LockEmployee = -1,
            //        OrderID = -1,

            //    });
            //    ObjCost.InsertNoneUpdate(ObjList, DispatchingID, 6);
            //    NamesList += GetEmployeeName(UpdateModel.EmployeeID) + ",";



            //}

            //ObjCost.DeleteforNull(NamesList.Trim(',').Split(','), 6);
        }


        /// <summary>
        /// 绑定数据触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repOrderTeamList_DataBinding(object sender, EventArgs e)
        {
            
        }
        Supplier ObjSupplierBLL = new Supplier();
        protected void repOrderTeamList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ObjModel = ObjProductforDispatchingBLL.GetByName((e.Item.DataItem as OrderTeam).Title);
            if (ObjModel != null)
            {
                switch (ObjModel.RowType)
                {
                    case 1:
               
                        var ObjSupplierModel = ObjSupplierBLL.GetByName(ObjModel.SupplierName);
                        if (ObjSupplierModel != null)
                        {
                            (e.Item.FindControl("lblName") as Label).Text = ObjModel.SupplierName;
                            (e.Item.FindControl("lblPhone") as Label).Text = ObjSupplierModel.CellPhone;
                        }
                        break;
                    case 3:

                        var ObjEmployeeModel = ObjEmployeeBLL.GetByID(ObjModel.SupplierID.ToString().ToInt32());
                        if (ObjEmployeeModel != null)
                        {
                            (e.Item.FindControl("lblName") as Label).Text = ObjModel.SupplierName;
                            (e.Item.FindControl("lblPhone") as Label).Text = ObjEmployeeModel.CellPhone;
                        }
                        break;
                }



            }
        }
    }
}