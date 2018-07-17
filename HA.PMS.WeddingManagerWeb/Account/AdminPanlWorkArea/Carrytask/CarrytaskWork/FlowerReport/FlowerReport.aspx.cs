using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using System.IO;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.FlowerReport
{

    public partial class FlowerReport : SystemPage
    {
        Statement ObjStatementBLL = new Statement();

        Supplier ObjSupplierBLL = new Supplier();
        SupplierType ObjSuplierTypeBLL = new SupplierType();

        Employee ObjEmployeeBLL = new Employee();

        FourGuardian ObjFourGuardianBLL = new FourGuardian();

        Flower ObjFlowerBLL = new Flower();
        CostSum ObjCostBLL = new CostSum();

        int RowType = 8;
        int DispatchingID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        #region 绑定数据方法
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var DataList = ObjFlowerBLL.GetByDispatchingID(DispatchingID).Where(C => C.BuyType == 0);
            lblSumMoney.Text = DataList.Where(C => C.SaleSumPrice != null).ToList().Sum(C => C.CostSumPrice.Value).ToString();
            //lblSumMoney.Text = DataList.Sum(C => C.CostSumPrice.Value).ToString();
            this.repFlowerPlanning.DataBind(DataList);
        }
        #endregion

        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            #region 上传凭据
            string FileAddress = string.Empty;
            string FileName = string.Empty;
            if (UploadFile.HasFile)
            {
                FileName = UploadFile.FileName.ToString();
                FileAddress = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/ReceiptImages/" + FileName;
                if (System.IO.Directory.Exists(Server.MapPath("/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/ReceiptImages/")))
                {
                    UploadFile.SaveAs(Server.MapPath(FileAddress));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/ReceiptImages/"));
                    UploadFile.SaveAs(Server.MapPath(FileAddress));
                }
            }
            #endregion

            int result = ObjFlowerBLL.Insert(new FL_FlowerCost()
            {
                CreateDate = DateTime.Now,

                SaleSumPrice = txtSaleSumPrice.Text.ToDecimal(),
                FLowername = txtFlosername.Text,
                Quantity = txtQuantity.Text.ToInt32(),
                DispatchingID = DispatchingID,
                CostPrice = txtCostPrice.Text.ToDecimal(),
                CreateEmployeeID = User.Identity.Name.ToInt32(),
                SalePrice = txtSalePrice.Text.ToDecimal(),
                CostSumPrice = txtQuantity.Text.ToInt32() * (txtCostPrice.Text).ToDecimal(),
                CustomerID = Request["CustomerID"].ToInt32(),
                Node = txtNode.Text,
                Unite = txtUnite.Text,
                UploadImage = FileAddress,
                ImageName = FileName,
                BuyType = 0
            });
            if (result == 0)
            {
                JavaScriptTools.AlertWindowAndLocation(txtFlosername.Text.ToString() + "已经存在,请重新输入!", Request.Url.ToString(), Page);
            }

            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndLocation("添加完毕!", Request.Url.ToString(), Page);
                BinderData();
            }
            SaveCostSum();
        }
        #endregion

        #region 绑定 事件触发
        /// <summary>
        /// 触发 修改 删除事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repFlowerPlanning_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    var UpdateModel = ObjFlowerBLL.GetByID((e.Item.FindControl("hideKey") as HiddenField).Value.ToInt32());
                    FL_FlowerCost ObjFlowerModels = ObjFlowerBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    var DataList = ObjFlowerBLL.GetByDispatchingID(DispatchingID).Where(C => C.BuyType == 0).ToList();
                    FL_CostSum ObjCostModel = ObjCostBLL.GetByCheckID(UpdateModel.FLowername, DispatchingID, RowType);
                    if (DataList.Count == 1)
                    {
                        if (ObjCostModel != null)
                        {
                            ObjCostBLL.Delete(ObjCostModel);
                        }
                    }
                    if (DataList.Count > 1)
                    {
                        ObjCostModel.Content = ObjCostModel.Content.Replace(ObjFlowerModels.FLowername, "");
                        ObjCostModel.Sumtotal -= ObjFlowerModels.CostSumPrice;
                        ObjCostBLL.Update(ObjCostModel);        //修改成本明细
                    }
                    if (ObjFlowerModels.UploadImage != string.Empty)        //上传了图片 就要删除图片
                    {
                        File.Delete(Server.MapPath(ObjFlowerModels.UploadImage));
                    }
                    ObjFlowerBLL.Delete(ObjFlowerBLL.GetByID(e.CommandArgument.ToString().ToInt32()));
                    JavaScriptTools.AlertWindow("删除完毕!", Page);
                    break;
                case "Edit":
                    var ObjFlowerModel = ObjFlowerBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    ObjFlowerModel.FLowername = (e.Item.FindControl("txtFLowername") as TextBox).Text;
                    ObjFlowerModel.CostPrice = (e.Item.FindControl("txtCostPrice") as TextBox).Text.ToDecimal();
                    ObjFlowerModel.Unite = (e.Item.FindControl("txtUnite") as TextBox).Text;
                    ObjFlowerModel.SalePrice = (e.Item.FindControl("txtSalePrice") as TextBox).Text.ToDecimal();
                    ObjFlowerModel.Quantity = (e.Item.FindControl("txtQuantity") as TextBox).Text.ToDecimal();
                    ObjFlowerModel.CostSumPrice = ObjFlowerModel.CostPrice * ObjFlowerModel.Quantity;

                    ObjFlowerModel.Node = (e.Item.FindControl("txtNode") as TextBox).Text;

                    ObjFlowerBLL.Update(ObjFlowerModel);
                    JavaScriptTools.AlertWindow("修改完毕!", Page);
                    break;
            }

            BinderData();
            SaveCostSum();
        }
        #endregion


        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Redirect("FlowerReportPrint.aspx?DispatchingID=" + DispatchingID + "&buytype=0");
        }

        #region 保存成本方法
        public void SaveCostSum()
        {
            var DataList = ObjFlowerBLL.GetByDispatchingID(DispatchingID).Where(C => C.BuyType == 0);
            if (DataList.Count() > 0)
            {
                var CostList = ObjCostBLL.GetByDispatchingID(DispatchingID, 8);
                foreach (var item in DataList)
                {
                    if (CostList.Count == 0)      //不存在  就新增
                    {
                        Insert(item);
                    }

                    if (CostList.Count > 0)     //存在的 就修改
                    {
                        foreach (var items in CostList)
                        {
                            FL_CostSum CostModel = ObjCostBLL.GetByID(items.CostSumId);
                            CostModel.Name = "花艺";
                            if (!(items.Content.Contains(item.FLowername)))
                            {
                                CostModel.Content += item.FLowername.ToString() + " , ";
                                CostModel.CategoryName += item.FLowername.ToString() + "花艺总成本";
                            }
                            CostModel.Sumtotal = item.CostSumPrice;
                            CostModel.RowType = RowType;
                            CostModel.DispatchingID = DispatchingID;
                            CostModel.CustomerId = Request["CustomerID"].ToInt32();
                            ObjCostBLL.Update(CostModel);
                        }
                    }
                }
                InsertStatement();
            }
        }
        #endregion

        #region 点击保存成本
        protected void btnSaveCost_Click(object sender, EventArgs e)
        {
            SaveCostSum();
            JavaScriptTools.AlertWindow("保存成功", Page);
        }
        #endregion

        #region 新增消费成本
        public void Insert(FL_FlowerCost item)
        {
            FL_CostSum CostModel = new FL_CostSum();
            CostModel.Name = item.FLowername.ToString();
            CostModel.Content += item.Node.ToString() + " , "; ;
            CostModel.CategoryName += item.FLowername.ToString() + "花艺总成本";
            CostModel.Sumtotal += item.CostSumPrice;
            CostModel.ActualSumTotal += item.CostSumPrice;
            CostModel.RowType = RowType;
            CostModel.DispatchingID = DispatchingID;
            CostModel.CustomerId = Request["CustomerID"].ToInt32();
            CostModel.Advance = "";
            CostModel.ShortCome = "";
            CostModel.Evaluation = 6;
            CostModel.State = 0;
            CostModel.CreateDate = DateTime.Now.ToString().ToDateTime();
            CostModel.OrderID = Request["OrderID"].ToInt32();
            CostModel.QuotedID = Request["QuotedID"].ToInt32();
            CostModel.EmployeeID = User.Identity.Name.ToInt32();
            ObjCostBLL.Insert(CostModel);

            item.CostSumId = CostModel.CostSumId;
            ObjFlowerBLL.Update(item);
        }
        #endregion

        #region 保存到结算表中
        public void InsertStatement()
        {

            var DatasList = ObjCostBLL.GetByDispatchingID(DispatchingID);
            foreach (var item in DatasList)
            {
                #region 结算表

                FL_Statement ObjStatementModel = new FL_Statement();

                ObjStatementModel.Name = item.Name;
                ObjStatementModel.TypeID = -4;
                ObjStatementModel.TypeName = "花艺";
                ObjStatementModel.RowType = 8;

                ObjStatementModel.CustomerID = Request["CustomerID"].ToInt32();
                ObjStatementModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjStatementModel.CreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                ObjStatementModel.DispatchingID = Request["DispatchingID"].ToInt32();
                ObjStatementModel.OrderId = Request["OrderID"].ToInt32();
                ObjStatementModel.QuotedId = Request["QuotedID"].ToInt32();
                ObjStatementModel.Remark = "";
                ObjStatementModel.Finishtation = "";
                ObjStatementModel.SumTotal = item.Sumtotal;
                ObjStatementModel.PayMent = 0;
                ObjStatementModel.NoPayMent = item.Sumtotal;
                FL_Statement StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, item.Name);
                if (StatementModel != null)    //已经存在
                {
                    StatementModel.Name = ObjStatementModel.Name;               //名称
                    StatementModel.TypeID = ObjStatementModel.TypeID;           //类型ID
                    StatementModel.TypeName = ObjStatementModel.TypeName;       //类型名称
                    StatementModel.RowType = ObjStatementModel.RowType;         //供应商类别
                    StatementModel.SumTotal = ObjStatementModel.SumTotal;       //金额
                    StatementModel.NoPayMent = ObjStatementModel.NoPayMent;     //未付款
                    ObjStatementBLL.Update(StatementModel);                 //修改更新
                }
                else
                {
                    ObjStatementBLL.Insert(ObjStatementModel);
                }

                #endregion
            }
        }
        #endregion
    }
}