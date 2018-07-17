using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.IO;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignclassReports : SystemPage
    {
        int CustomerID = 0;
        int SourceCount = 0;
        string OrderColumnName = "PartyDate";

        //材质
        Material ObjMaterialBLL = new Material();

        //策划报价(执行设计)
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //前期设计
        EarlyDesigner ObjEarlyDesignerBLL = new EarlyDesigner();

        //设计清单
        HA.PMS.BLLAssmblly.Flow.Designclass ObjDesignclassBLL = new BLLAssmblly.Flow.Designclass();

        //成本
        CostSum ObjCostSumBLL = new CostSum();



        /// <summary>
        /// 图片
        /// </summary>
        DesignUpload ObjDesignUploadBLL = new DesignUpload();

        //客户
        Customers ObjCustomerBLL = new Customers();

        Employee ObjEmployeeBLL = new Employee();

        FourGuardian ObjFourGuardianBLL = new FourGuardian();

        //供应商
        Supplier ObjSupplierBLL = new Supplier();

        SupplierType ObjSupplierTypeBLL = new SupplierType();

        Statement ObjStatementBLL = new Statement();


        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerID = Request["CustomerID"].ToInt32();
            if (!IsPostBack)
            {
                DDLBind();

                var DataList = ObjDesignclassBLL.GetByQuotedIDs(Request["QuotedID"].ToInt32()).OrderByDescending(C => C.DesignclassID);
                var Model = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());
                if (Model != null)
                {
                    if (Model.DesignerState == 1 || Model.DesignerState == 2)       //已下派
                    {
                        td_Dispatching.Visible = false;
                        RepDesignlist.Visible = false;
                        tr_TextBoxInsert.Visible = false;
                        th_Save.Visible = false;
                        btnPrint2.Visible = true;
                        divPrint.Visible = true;            //显示
                        divAdd.Visible = false;
                        this.RepDesignlist.DataBind(null);
                    }
                    else                    //未下派
                    {
                        Employee ObjEmployeeBLL = new Employee();
                        if (txtEmpLoyee.Text != "")
                        {
                            hideEmployeesID.Value = ObjEmployeeBLL.GetByName(txtEmpLoyee.Text.Trim().ToString()).EmployeeID.ToString();
                        }

                        td_Dispatching.Visible = true;
                        RepDesignlist.Visible = true;
                        tr_TextBoxInsert.Visible = true;
                        th_Save.Visible = true;
                        btnPrint2.Visible = false;
                        divPrint.Visible = false;           //隐藏
                        divAdd.Visible = true;
                        this.RepDesignlist.DataBind(DataList);
                    }


                    //执行设计
                    if (Model != null)
                    {
                        lblDesignEmployee.Text = GetEmployeeName(Model.DesignerEmployee) == "" ? "暂无" : GetEmployeeName(Model.DesignerEmployee);
                        lblPlanDate.Text = Model.PlanFinishDate.ToString() == "" ? "" : Model.PlanFinishDate.ToString().ToDateTime().ToShortDateString();
                        txtEmpLoyee.Text = GetEmployeeName(Model.DesignerEmployee) == "" ? "暂无" : GetEmployeeName(Model.DesignerEmployee);
                        txtPlanDate.Text = Model.PlanFinishDate.ToString() == "" ? "" : Model.PlanFinishDate.ToString().ToDateTime().ToShortDateString();
                    }


                }
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        public void BinderData()
        {

            var Pars = new List<PMSParameters>();
            Pars.Add("CustomerID", CustomerID, NSqlTypes.Equal);
            var DataList = ObjDesignclassBLL.GetAllByParameter(Pars, OrderColumnName, 100, 1, out SourceCount);
            rptDesignList.DataSource = DataList;            //绑定后期设计
            rptDesignList.DataBind();

            lblTotalPriceSum.Text = DataList.Sum(C => C.TotalPrice).ToString();         //计算合计

            for (int i = 0; i < rptDesignList.Items.Count; i++)
            {
                var ObjItem = rptDesignList.Items[i];
                HiddenField State = (HiddenField)ObjItem.FindControl("HideState");
                if (State.Value.ToInt32() == 2)
                {
                    TextBox txtRealQuantity = (TextBox)ObjItem.FindControl("txtRealQuantity");
                    Button btnConfirm = (Button)ObjItem.FindControl("btn_Confirm");
                    txtRealQuantity.ReadOnly = true;
                    btnConfirm.Visible = false;
                    btnSave.Visible = false;
                }

            }
        }
        #endregion

        #region 点击保存事件(修改数量  统一保存)
        /// <summary>
        /// 保存
        /// </summary>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptDesignList.Items.Count; i++)
            {
                var ObjItem = rptDesignList.Items[i];
                HiddenField DesignId = (HiddenField)ObjItem.FindControl("HideDesignID");
                TextBox txtRealQuantity = (TextBox)ObjItem.FindControl("txtRealQuantity");
                FL_Designclass ObjDesignModel = ObjDesignclassBLL.GetByID(DesignId.Value.ToInt32());
                ObjDesignModel.RealQuantity = txtRealQuantity.Text.ToString() == "" ? 0 : txtRealQuantity.Text.ToString().ToInt32();
                ObjDesignModel.State = 2;
                ObjDesignModel.Evaluation = 6;
                ObjDesignModel.Advance = "";
                ObjDesignModel.ShortCome = "";
                ObjDesignModel.TotalPrice = (ObjDesignModel.RealQuantity * ObjDesignModel.PurchasePrice);
                ObjDesignclassBLL.Update(ObjDesignModel);
            }
            JavaScriptTools.AlertWindow("修改成功", Page);
            BinderData();

        }
        #endregion

        #region 修改数量(单项保存)
        /// <summary>
        /// 修改数量
        /// </summary>    
        protected void rptDesignList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                int DesingId = e.CommandArgument.ToString().ToInt32();
                FL_Designclass ObjDesignModel = ObjDesignclassBLL.GetByID(DesingId);
                ObjDesignModel.State = 2;
                ObjDesignModel.Evaluation = 6;
                ObjDesignModel.Advance = "";
                ObjDesignModel.ShortCome = "";
                ObjDesignModel.RealQuantity = ((TextBox)e.Item.FindControl("txtRealQuantity")).Text.ToString().ToInt32();
                ObjDesignModel.TotalPrice = (ObjDesignModel.RealQuantity * ObjDesignModel.PurchasePrice);
                ObjDesignclassBLL.Update(ObjDesignModel);
            }
            BinderData();
        }
        #endregion

        //可修改
        #region 绑定 做事件(删除 修改)
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RepDesignlist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    ObjDesignclassBLL.Delete(ObjDesignclassBLL.GetByID(e.CommandArgument.ToString().ToInt32()));
                    Response.Redirect(Request.Url.ToString());
                    break;
                case "Edit":
                    var UpdateModel = ObjDesignclassBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    UpdateModel.Material = (e.Item.FindControl("ddlMaterial") as DropDownList).SelectedValue;       //材质
                    UpdateModel.Supplier = (e.Item.FindControl("ddlSupplier") as DropDownList).SelectedValue;       //供应商
                    UpdateModel.Unit = (e.Item.FindControl("txtUnit") as TextBox).Text.ToString();
                    UpdateModel.Spec = (e.Item.FindControl("txtSpec") as TextBox).Text;
                    UpdateModel.PurchaseQuantity = (e.Item.FindControl("txtPurchaseQuantity") as TextBox).Text.ToInt32();
                    UpdateModel.PurchasePrice = (e.Item.FindControl("txtPurchasePrice") as TextBox).Text.ToDecimal();
                    UpdateModel.TotalPrice = UpdateModel.PurchaseQuantity * UpdateModel.PurchasePrice;
                    ObjDesignclassBLL.Update(UpdateModel);
                    Response.Redirect(Request.Url.ToString());
                    break;
            }


        }
        #endregion

        #region 绑定完成事件 显示图片 参考图 Type 2  效果图 Type 1
        /// <summary>
        /// 图片的绑定
        /// </summary>
        protected void RepDesignlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            ///参考图
            Repeater repImages = e.Item.FindControl("repShowImg") as Repeater;
            int DesignClassID = (e.Item.FindControl("HideDesignId") as HiddenField).Value.ToInt32();
            repImages.DataSource = ObjDesignUploadBLL.GetByDesignClassID(DesignClassID, 2);
            repImages.DataBind();


            ///效果图
            //Repeater repShowResultImg = e.Item.FindControl("repShowResultImg") as Repeater;
            //repShowResultImg.DataSource = ObjDesignUploadBLL.GetByDesignClassID(DesignClassID, 1);
            //repShowResultImg.DataBind();

        }
        #endregion

        #region 添加设计清单
        /// <summary>
        ///添加喷绘设计 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            int SupplierId = 1;
            if (ddlSupplierName.SelectedValue.ToInt32() == 0)
            {
                SupplierId = 1;
            }
            else
            {
                SupplierId = ddlSupplierName.SelectedValue.ToInt32();
            }

            if (ddlSupplierName.SelectedValue == "0")
            {
                JavaScriptTools.AlertWindow("请选择供应商", Page);
                return;
            }

            ObjDesignclassBLL.Insert(new FL_Designclass()
            {
                Title = txtTitle.Text,
                Node = txtNode.Text,
                PurchaseQuantity = txtPurchaseQuantity.Text.ToInt32(),
                PurchasePrice = txtPurchasePrice.Text.ToDecimal(),
                RealQuantity = 0,
                Unit = txtUnit.Text,
                Supplier = SupplierId.ToString(),
                TotalPrice = txtPurchaseQuantity.Text.ToInt32() * txtPurchasePrice.Text.ToDecimal(),
                CreateDate = DateTime.Now,
                CreateEmployee = User.Identity.Name.ToInt32(),
                QuotedID = Request["QuotedID"].ToInt32(),
                CustomerID = Request["CustomerID"].ToInt32(),
                State = 0,
                Material = ddlMaterial.SelectedValue,
                Spec = txtSpec.Text

            });
            Response.Redirect(this.Page.Request.Url.ToString());
        }
        #endregion

        //下派后期设计
        #region 确认下派设计师 保存执行设计
        /// <summary>
        /// 下派设计单 选择设计师
        /// </summary>
        protected void btnDispatchingConfirm_Click(object sender, EventArgs e)
        {
            int EmployeeID = hideEmployeesID.Value.ToInt32();
            if (EmployeeID == -1 && txtEmpLoyee.Text != "")
            {
                Employee ObjEmployeeBLL = new Employee();
                EmployeeID = ObjEmployeeBLL.GetByName(txtEmpLoyee.Text.Trim().ToString()).EmployeeID;
            }
            FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());
            if (txtPlanDate.Text == "")
            {
                ObjQuotedPriceModel.PlanFinishDate = DateTime.Today.AddDays(7);
            }
            else
            {
                ObjQuotedPriceModel.PlanFinishDate = txtPlanDate.Text.ToDateTime();
            }
            ObjQuotedPriceModel.DesignerEmployee = EmployeeID;      //选择设计师
            ObjQuotedPriceModel.WorkCreateDate = DateTime.Now.ToShortDateString().ToDateTime();      //选派设计师时间
            ObjQuotedPriceModel.DesignerState = 1;                  //1 .已派设计师 0. null 未派策划师
            ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);



            //操作日志
            CreateHandle(1);
            //保存成本表和结算表
            SaveDesignCost(2, hideEmployeesID.Value.ToString().ToInt32());
            InsertStatement();
            JavaScriptTools.AlertWindowAndLocation("下派设计师成功", Page.Request.Url.ToString(), Page);
        }

        #endregion

        //结算表方法
        #region 保存结算表
        public void InsertStatement()
        {
            int DispatchingID = Request["DispatchingID"].ToInt32();

            var DatasList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);
            foreach (var item in DatasList)
            {
                #region 结算表

                FL_Statement ObjStatementModel = new FL_Statement();
                #region 判断类别
                switch (item.RowType)
                {
                    case 1:         //供应商
                        var ObjSupplierModel = ObjSupplierBLL.GetByName(item.Name);
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.SupplierID = ObjSupplierModel.SupplierID;
                        if (ObjSupplierModel != null)
                        {
                            ObjStatementModel.TypeID = ObjSupplierModel.CategoryID;
                        }
                        else
                        {
                            foreach (var AllItem in ObjSupplierBLL.GetByAll())
                            {
                                if (item.Name.Contains(AllItem.Name) || item.Name.Contains(AllItem.Name))
                                {
                                    ObjStatementModel.TypeID = AllItem.CategoryID;
                                }
                            }
                        }
                        ObjStatementModel.TypeName = ObjSupplierTypeBLL.GetByID(ObjStatementModel.TypeID).TypeName;
                        ObjStatementModel.RowType = 1;

                        break;
                    case 2:         //库房          //显示  因为没有明确的收款人  结算表不方便结算
                        ObjStatementModel.Name = "库房";
                        ObjStatementModel.SupplierName = "库房";
                        ObjStatementModel.TypeID = -1;
                        ObjStatementModel.TypeName = "库房";
                        ObjStatementModel.RowType = 2;

                        break;
                    case 3:         //新购买
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -5;
                        ObjStatementModel.TypeName = "新购买";
                        ObjStatementModel.RowType = 3;

                        break;
                    case 4:         //四大金刚
                        string name = item.Name.Replace("(预定)", "");
                        var ObjFourGuardianModel = ObjFourGuardianBLL.GetByName(name);
                        if (ObjFourGuardianModel == null)
                        {
                            var ObjEmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                            if (ObjEmployeeModel != null)
                            {
                                ObjStatementModel.Name = item.Name;
                                ObjStatementModel.SupplierName = item.Name;
                                ObjStatementModel.SupplierID = ObjEmployeeModel.EmployeeID;
                                ObjStatementModel.TypeID = -2;
                                ObjStatementModel.TypeName = "内部人员";
                                ObjStatementModel.RowType = 5;
                            }
                        }
                        if (ObjFourGuardianModel != null)
                        {
                            GuardianType ObjGuardTypeBLL = new GuardianType();

                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = ObjFourGuardianModel.GuardianId;
                            ObjStatementModel.TypeID = ObjFourGuardianModel.GuardianTypeId.ToString().ToInt32();
                            ObjStatementModel.TypeName = ObjGuardTypeBLL.GetByID(ObjFourGuardianModel.GuardianTypeId).TypeName;
                            ObjStatementModel.RowType = 4;
                        }
                        break;
                    case 5:         //人员
                        var ObjEmployeeModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (ObjEmployeeModels != null)
                        {
                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = ObjEmployeeModels.EmployeeID;
                            ObjStatementModel.TypeID = -2;
                            ObjStatementModel.TypeName = "内部人员";
                            ObjStatementModel.RowType = 5;
                        }
                        break;
                    case 6:         //设计师/工程主管
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -3;
                        ObjStatementModel.TypeName = "设计师";
                        var EmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                        ObjStatementModel.SupplierID = EmployeeModel.EmployeeID;
                        ObjStatementModel.RowType = 5;
                        break;
                    case 7:         //内部人员/四大金刚 (手动添加)
                        var PersonModel = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModel == null)
                        {
                            ObjStatementModel.SupplierID = ObjFourGuardianBLL.GetByName(item.Name).GuardianId;
                            ObjStatementModel.RowType = 4;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = PersonModel.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -4;
                        ObjStatementModel.TypeName = "人员(手动添加)";
                        break;
                    case 8:         //内部人员/供应商 (手动添加)
                        var PersonModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModels == null)
                        {
                            ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                            ObjStatementModel.RowType = 1;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = PersonModels.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -3;
                        ObjStatementModel.TypeName = "物料 (手动添加)";
                        break;
                    case 9:         //内部人员/供应商 (手动添加)
                        var OtherModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (OtherModels == null)
                        {
                            ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                            ObjStatementModel.RowType = 1;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = OtherModels.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -2;
                        ObjStatementModel.TypeName = "其他";
                        break;
                    case 10:         //设计清单(name代表供应商 国色广告)
                        ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -4;
                        ObjStatementModel.TypeName = "设计清单";
                        ObjStatementModel.RowType = 1;
                        break;
                }
                #endregion

                ObjStatementModel.CustomerID = Request["CustomerID"].ToInt32();
                ObjStatementModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjStatementModel.CreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                ObjStatementModel.DispatchingID = Request["DispatchingID"].ToInt32();
                ObjStatementModel.OrderId = Request["OrderID"].ToInt32();
                ObjStatementModel.QuotedId = Request["QuotedID"].ToInt32();
                ObjStatementModel.Remark = "";
                ObjStatementModel.Finishtation = "";
                ObjStatementModel.SumTotal = item.Sumtotal;
                ObjStatementModel.Content = item.Content;
                ObjStatementModel.PayMent = 0;
                ObjStatementModel.NoPayMent = item.Sumtotal;
                ObjStatementModel.CostSumId = item.CostSumId;
                ObjStatementModel.Year = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Year;
                ObjStatementModel.Month = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Month;


                FL_Statement StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, item.Name);
                if (StatementModel != null)    //已经存在
                {
                    StatementModel.Name = ObjStatementModel.Name;               //名称
                    StatementModel.SupplierID = ObjStatementModel.SupplierID;   //供应商ID
                    StatementModel.Content = ObjStatementModel.Content;         //内容(说明)
                    StatementModel.TypeID = ObjStatementModel.TypeID;           //类型ID
                    StatementModel.TypeName = ObjStatementModel.TypeName;       //类型名称
                    StatementModel.RowType = ObjStatementModel.RowType;         //供应商类别
                    StatementModel.SumTotal = ObjStatementModel.SumTotal;       //金额
                    StatementModel.PayMent = 0;                                 //已付款
                    StatementModel.NoPayMent = ObjStatementModel.NoPayMent;     //未付款
                    ObjStatementBLL.Update(StatementModel);                     //修改更新
                }
                else
                {
                    ObjStatementBLL.Insert(ObjStatementModel);
                }

                #endregion
            }
        }
        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 下拉框绑定数据
        /// </summary>
        private void DDLBind()
        {
            //绑定材质
            List<FD_Material> objMaterialModel = ObjMaterialBLL.GetByAll();
            ddlMaterial.DataSource = objMaterialModel;
            ddlMaterial.DataTextField = "MaterialName";
            ddlMaterial.DataValueField = "MaterialId";
            ddlMaterial.DataBind();
            FD_Material material = ObjMaterialBLL.GetByID(ddlMaterial.SelectedValue.ToInt32());
            if (objMaterialModel.Count() == 0)
            {
                txtPurchasePrice.Text = "";
            }
            else
            {
                txtPurchasePrice.Text = material.MaterialUnitPrice.ToString();
            }

            //绑定供应商
            ddlSupplierName.DataSource = ObjSupplierBLL.GetByCategoryId(5);
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataValueField = "SupplierID";
            ddlSupplierName.DataBind();
            ddlSupplierName.Items.Add(new ListItem("请选择", "0"));
            if (ObjSupplierBLL.GetByID(166).Name == "国色广告")
            {
                ddlSupplierName.Items.FindByText("国色广告").Selected = true;
            }
            else
            {
                ddlSupplierName.Items.FindByText("请选择").Selected = true;
            }

        }
        #endregion

        #region 导出Excel
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=PriceManage" + DateTime.Now.Date.ToString("yyyyMMdd") + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.rptDesignList.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();


        }
        #endregion

        #region 添加设计师成本
        /// <summary>
        /// 添加成本
        /// </summary>
        public void SaveDesignCost(int Type, int Designer = 1)
        {
            //FL_CostSum CostSum = new FL_CostSum();

            //CostSum.Name = GetEmployeeName(Designer);
            //CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
            //CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            //CostSum.ShortCome = "";
            //CostSum.Advance = "";
            //CostSum.OrderID = Request["OrderID"].ToInt32();
            //CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
            //CostSum.CustomerId = Request["CustomerID"].ToInt32();
            //CostSum.RowType = 6;
            //CostSum.Sumtotal = 100;
            //CostSum.ActualSumTotal = 100;
            //CostSum.Content = "执行设计(后期设计)";
            //CostSum.CategoryName = "执行设计(后期设计)";
            //CostSum.Evaluation = 6;
            //CostSum.EmployeeID = User.Identity.Name.ToInt32();

            //var Model = ObjCostSumBLL.GetByCheckID(GetEmployeeName(Designer), Request["DispatchingID"].ToInt32(), 6);
            //if (Model == null)          //实体为null   没添加过 (就可以新增 添加)  确保不重复添加
            //{
            //    ObjCostSumBLL.Insert(CostSum);
            //}
            //else if (Model != null)
            //{
            //    ObjCostSumBLL.Update(Model);

            //}
        }

        #region 获取设计单的各项名称
        /// <summary>
        /// 获取名称
        /// </summary>
        public string GetTitle(int CustomerID, int Supplier)
        {
            var DataList = ObjDesignclassBLL.GetByCustomerId(CustomerID).Where(C => C.Supplier.ToInt32() == Supplier).ToList();
            string titles = "";
            int index = 0;
            foreach (var item in DataList)
            {
                if (DataList.Count == index)
                {
                    titles += item.Title;
                }
                else
                {
                    titles += item.Title + ",";
                    index++;
                }
            }
            return titles;
        }
        #endregion

        #endregion

        #region 材质选择变化事件
        /// <summary>
        /// 更改单价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FD_Material> objMaterialModel = ObjMaterialBLL.GetByAll();

            FD_Material material = ObjMaterialBLL.GetByID(ddlMaterial.SelectedValue.ToInt32());
            if (objMaterialModel.Count() == 0)
            {
                txtPurchasePrice.Text = "";
            }
            else
            {
                txtPurchasePrice.Text = material.MaterialUnitPrice.ToString();
            }
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int Type)
        {
            int EmployeeID = hideEmployeesID.Value.ToInt32();
            if (EmployeeID == -1 && txtEmpLoyee.Text != "")
            {
                Employee ObjEmployeeBLL = new Employee();
                EmployeeID = ObjEmployeeBLL.GetByName(txtEmpLoyee.Text.Trim().ToString()).EmployeeID;
            }

            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());

            HandleModel.HandleContent = "婚礼统筹-设计单派单,客户姓名:" + Model.Bride + "/" + Model.Groom + ",选择设计师：" + GetEmployeeName(EmployeeID);


            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 5;     //婚礼统筹
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 获取设计单的状态
        public string GetState()
        {
            int CustomerID = Request["CustomerID"].ToInt32();
            var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                if (Model.DesignerState == 0)
                {
                    return "未派单";
                }
                else if (Model.DesignerState == 1)
                {
                    return "已派单";
                }
                else if (Model.DesignerState == 2)
                {
                    return "已下单";
                }
            }
            return "";
        }

        #endregion

        #region 显示列表绑定完成事件
        /// <summary>
        /// 绑定完成
        /// </summary>
        protected void rptDesignList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Button btnConfirm = e.Item.FindControl("btn_Confirm") as Button;
            TextBox txtRealQuantity = e.Item.FindControl("txtRealQuantity") as TextBox;
            if (Request["Type"].ToString() == "1")
            {
                btnConfirm.Visible = false;
                txtRealQuantity.Visible = false;

            }
        }
        #endregion

        #region 财务查看 隐藏操作
        public string CostVisible()
        {
            if (Request["Type"].ToInt32() == 1)
            {
                return "style='display:none;'";
            }
            return "";
        }
        #endregion

    }
}