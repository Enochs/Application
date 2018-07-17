using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.CS;
using System.Web.UI.HtmlControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class OrderCost : SystemPage
    {
        CostforOrder ObjOrderCostBLL = new CostforOrder();


        //派工单项目产品
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        //派工产品
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        //满意度
        DispathingSatisfaction ObjSatisfictionBLL = new DispathingSatisfaction();

        //设计单
        Designclass ObjDesignClassBLL = new Designclass();

        /// <summary>
        /// 获取各类的价格
        /// </summary>
        QuotedPriceForType ObjForTypeBLL = new QuotedPriceForType();

        CostforOrder ObjCostForOrderBLL = new CostforOrder();

        //客户表
        Customers ObjCustomersBLL = new Customers();

        //成本表
        CostSum ObjCostSumBLL = new CostSum();

        //供应商
        Supplier ObjSupplierBLL = new Supplier();

        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 四大金刚
        /// </summary>
        FourGuardian ObjFourGuardianBLL = new FourGuardian();


        SupplierType ObjSuplierTypeBLL = new SupplierType();

        //结算
        Statement ObjStatementBLL = new Statement();

        //前期设计
        EarlyDesigner ObjDesignBLL = new EarlyDesigner();


        Report ObjReportBLL = new Report();

        //执行设计
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        string Type = "";
        int DispatchingID;
        int CustomerID;

        string OrderColumnName = "CostSumId";
        int PageSize = 100;
        int PageIndex = 1;
        int SourceCount = 0;


        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            if (!IsPostBack)
            {
                Insert();       //生成总体评价 保存的时候 进行修改
                SaveDesignCost("Design", "");           //设计单项目
                var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32());

                if (ObjQuotedModel != null && ObjQuotedModel.DesignerEmployee != null)
                {
                    SaveDesignCost("Add", "Designer", ObjQuotedModel.DesignerEmployee.ToString().ToInt32());           //设计师
                }

                //销售费用
                SaveSaleCost();

                BinderData();

                var Model = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
                if (Model != null)
                {
                    if (Model.EvalState == 1 || Model.State == 206)
                    {
                        tr_AddMaterial.Visible = false;
                        t_AddOther.Visible = false;
                        tr_AddPerson.Visible = false;
                        tr_AddSale.Visible = false;
                        btnSavePerson.Visible = false;          //人员保存
                        btnSaveMaterials.Visible = false;       //物料保存
                        btnSaveOrher.Visible = false;           //其他保存
                        btnSaveSale.Visible = false;            //销售保存
                        btnSaveEvaulation.Visible = false;      //总评保存

                        SetTextBoxEnalbe(this, true);
                        GetVisible(repEmployeeCost);      //确认完结 隐藏TextBox  显示Label
                        GetVisible(repSupplierCost);
                        GetVisible(repOtherCost);
                    }
                }


            }
        }
        #endregion

        #region 遍历所有控件  禁用
        /// <summary>  
        /// 界面中所有的TextBox DropDownList，并设置读写属性  
        /// </summary>  
        void SetTextBoxEnalbe(System.Web.UI.Control control, bool enable)
        {
            if (control is TextBox)
            {
                (control as TextBox).Enabled = enable;
                (control as TextBox).ReadOnly = true;
            }
            else if (control.HasControls())
            {
                foreach (System.Web.UI.Control s in control.Controls)
                {
                    SetTextBoxEnalbe(s, enable);
                }
            }


            if (control is DropDownList)
            {
                (control as DropDownList).Enabled = false;
            }
            else if (control.HasControls())
            {
                foreach (System.Web.UI.Control s in control.Controls)
                {
                    SetTextBoxEnalbe(s, enable);
                }
            }
        }
        #endregion

        #region 确认完结之后  显示Label 隐藏TextBox
        public void GetVisible(Repeater Rep)
        {
            if (Rep.Items.Count > 0)
            {
                for (int i = 0; i < Rep.Items.Count; i++)
                {
                    var ObjItem = Rep.Items[i];
                    TextBox txtPayMent = ObjItem.FindControl("txtPayMent") as TextBox;
                    TextBox txtSumTotal = ObjItem.FindControl("txtSumTotal") as TextBox;
                    Label lblPayMent = ObjItem.FindControl("lblPayMent") as Label;
                    Label lblSumTotal = ObjItem.FindControl("lblSumTotal") as Label;
                    txtPayMent.Visible = false;
                    txtSumTotal.Visible = false;
                    lblPayMent.Visible = true;
                    lblSumTotal.Visible = true;
                }
            }
        }
        #endregion

        #region 数据绑定
        private void BinderData()
        {
            int CustomerID = Request["CustomerID"].ToInt32();
            FL_Customers CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            Type = Request["Type"].ToString();
            if (CustomerModel.State == 206)
            {
                int DispathingID = Request["DispatchingID"].ToInt32();
                int OrderID = Request["OrderID"].ToInt32();
                int QuotedID = Request["QuotedID"].ToInt32();
                btn_OrderFinish.Visible = false;

                if (Type != "Details")
                {
                    Response.Redirect("OrderCost.aspx?DispatchingID=" + DispathingID + "&CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&Type=Details&NeedPopu=1");
                }
            }

            int DisID = Request["DispatchingID"].ToInt32();

            //供应商成本 来自供应商明细产品表

            //打通酒店佣金渠道


            var m_sale = ObjCostSumBLL.GetByDispatchingID(DisID).Where(C => C.RowType == 12 && C.Name == "酒店佣金").FirstOrDefault();
            if (m_sale != null)
            {
                var m_report = ObjReportBLL.GetByCustomerID(CustomerID);
                m_sale.Sumtotal = m_report.PayMoney == null ? 0 : m_report.PayMoney;
                m_sale.ActualSumTotal = m_report.PayMoney == null ? 0 : m_report.PayMoney;
                m_sale.PayMent = m_report.PayMoney == null ? 0 : m_report.PayMoney;
                m_sale.NoPayMent = 0;
                ObjReportBLL.Update(m_report);
            }


            List<FL_CostSum> DataList = ObjCostSumBLL.GetByDispatchingID(DisID);

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7));  //执行团队 5  内部人员 4  手动添加  7

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 8 || C.RowType == 10).OrderBy(C => C.CostSumId));        //物料 1      库房 2      新购买 3    系统默认添加的设计师 6   手动添加 8  设计单 10

            repOtherCost.DataBind(DataList.Where(C => C.RowType == 9 || C.RowType == 11));              //其他

            repSaleMoney.DataBind(DataList.Where(C => C.RowType == 12));            //销售成本


            //人员 物料 其他 销售价 成本价 毛利率
            GetFinishAmount(3);     //销售价
            GetCostAmounts(3);      //成本价
            GetProfitRate();        //毛利率

            DeleteIsVisible(repEmployeeCost, 1);      //人员
            DeleteIsVisible(repSupplierCost, 2);      //物料
            DeleteIsVisible(repOtherCost, 3);         //其他

            lblSumMoney.Text = DataList.Sum(C => C.ActualSumTotal).ToString();            //合计

            List<FL_DispathingSatisfaction> ObjSacList = ObjSatisfictionBLL.GetByDispatchingID(DisID);      //总评
            rptSatisfaction.DataBind(ObjSacList);

        }

        #endregion

        #region 添加设计清单成本
        /// <summary>
        /// 添加成本
        /// </summary>
        public void SaveDesignCost(string Type, string SaveType, int Designer = 1)
        {
            //设计清单 10
            FL_CostSum CostSum = new FL_CostSum();

            if (Type == "Design")
            {
                var DataList = ObjCostSumBLL.GetCostSumForDesigner(Request["CustomerID"].ToInt32(), "");
                foreach (var item in DataList)
                {
                    CostSum = new FL_CostSum();

                    var DesignClassModel = ObjDesignClassBLL.GetByCustomerId(CustomerID);           //获取设计单各项产品
                    if (DesignClassModel.Count > 0)           //数量大于0  说明有后期设计单
                    {
                        var Model = ObjCostSumBLL.GetByCheckID(GetSupplierName(item.Supplier), Request["DispatchingID"].ToInt32(), 10);
                        if (Model == null)
                        {
                            CostSum.Name = GetSupplierName(item.Supplier);
                            CostSum.Content = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                            CostSum.CategoryName = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                            CostSum.Sumtotal = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                            CostSum.ActualSumTotal = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                            CostSum.PayMent = 0;
                            CostSum.NoPayMent = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
                            CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                            CostSum.ShortCome = "";
                            CostSum.Advance = "";
                            CostSum.OrderID = Request["OrderID"].ToInt32();
                            CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
                            CostSum.CustomerId = Request["CustomerID"].ToInt32();
                            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
                            CostSum.RowType = 10;
                            CostSum.Evaluation = 6;
                            CostSum.EmployeeID = User.Identity.Name.ToInt32();
                            ObjCostSumBLL.Insert(CostSum);
                        }
                        else
                        {
                            Model.Name = GetSupplierName(item.Supplier);
                            Model.Content = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                            Model.CategoryName = GetTitle(item.CustomerID.ToString().ToInt32(), item.Supplier.ToString().ToInt32());
                            Model.Sumtotal = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                            Model.ActualSumTotal = ObjDesignClassBLL.GetBySupplierID(Request["CustomerID"].ToInt32(), item.Supplier.ToString()).Sum(C => C.TotalPrice).ToString().ToDecimal();
                            Model.DispatchingID = Request["DispatchingID"].ToInt32();
                            ObjCostSumBLL.Update(Model);
                        }
                    }
                }

                BinderData();
            }
            else if (Type == "Add")
            {
                var CostSumModel = ObjCostSumBLL.GetByCheckID(GetEmployeeName(Designer), Request["DispatchingID"].ToInt32(), 6);

                CostSum = new FL_CostSum();
                CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
                CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                CostSum.ShortCome = "";
                CostSum.Advance = "";
                CostSum.OrderID = Request["OrderID"].ToInt32();
                CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
                CostSum.CustomerId = Request["CustomerID"].ToInt32();

                if (SaveType == "Person")           //人员
                {
                    if (ddlPersonType.SelectedItem.Text == "内部人员")
                    {
                        CostSum.Name = Text1.Text.Trim().ToString();
                    }
                    else if (ddlPersonType.SelectedItem.Text == "四大金刚")
                    {
                        CostSum.Name = txtSuppName.Text.Trim().ToString();
                    }
                    CostSum.RowType = 7;
                    CostSum.Content = txtContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtSumtotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtSumtotal.Text.Trim().ToString().ToDecimal();
                }
                else if (SaveType == "Material")            //物料
                {
                    if (ddlPersonTypes.SelectedItem.Text == "内部人员")
                    {
                        CostSum.Name = txtInPerson.Text.Trim().ToString();
                    }
                    else if (ddlPersonTypes.SelectedItem.Text == "供应商")
                    {
                        CostSum.Name = txtSupplier.Text.Trim().ToString();
                    }
                    CostSum.RowType = 8;
                    CostSum.Content = txtMaterialContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtMaterialContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtMaterialSumTotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtMaterialSumTotal.Text.Trim().ToString().ToDecimal();
                }
                else if (SaveType == "Other")               //其他
                {
                    if (ddlOtherType.SelectedItem.Text == "内部人员")
                    {
                        CostSum.Name = txtOtherEmployee.Text.Trim().ToString();
                    }
                    else if (ddlOtherType.SelectedItem.Text == "供应商")
                    {
                        CostSum.Name = txtSuppliers.Text.Trim().ToString();
                    }
                    CostSum.RowType = 9;
                    CostSum.Content = txtOtherContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtOtherContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtOtherSumTotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtOtherSumTotal.Text.Trim().ToString().ToDecimal();
                }
                //else if (SaveType == "Designer")        //设计师的默认添加  
                //{
                //    var Model = ObjCostSumBLL.GetByCheckID(GetEmployeeName(Designer), Request["DispatchingID"].ToInt32(), 6);
                //    if (Model != null)
                //    {
                //        Model.Name = GetEmployeeName(Designer);
                //        Model.RowType = 6;
                //        Model.Content = "执行设计(后期设计)";
                //        Model.CategoryName = "执行设计(后期设计)";
                //        Model.Sumtotal = ((ObjForTypeBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.MPrice).ToString().ToDecimal()) / 100).ToString().ToDecimal();
                //        Model.ActualSumTotal = ((ObjForTypeBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.MPrice).ToString().ToDecimal()) / 100).ToString().ToDecimal();
                //        ObjCostSumBLL.Update(Model);
                //        return;
                //    }
                //    else
                //    {
                //        CostSum.Name = GetEmployeeName(Designer);
                //        CostSum.RowType = 6;
                //        CostSum.Content = "执行设计(后期设计)";
                //        CostSum.CategoryName = "执行设计(后期设计)";
                //        CostSum.Sumtotal = ((ObjForTypeBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.MPrice).ToString().ToDecimal()) / 100).ToString().ToDecimal();
                //        CostSum.ActualSumTotal = ((ObjForTypeBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Sum(C => C.MPrice).ToString().ToDecimal()) / 100).ToString().ToDecimal();
                //    }
                //}
                else if (SaveType == "Sale")               //其他
                {
                    CostSum.Name = txtSaleTitle.Text.Trim().ToString();

                    CostSum.RowType = 12;
                    CostSum.Content = txtSaleContent.Text.Trim().ToString();
                    CostSum.CategoryName = txtSaleContent.Text.Trim().ToString();
                    CostSum.Sumtotal = txtSaleSumTotal.Text.Trim().ToString().ToDecimal();
                    CostSum.ActualSumTotal = txtSaleSumTotal.Text.Trim().ToString().ToDecimal();
                }


                CostSum.Evaluation = 6;
                CostSum.EmployeeID = User.Identity.Name.ToInt32();
                CostSum.Remark = "";
                CostSum.PayMent = 0;
                CostSum.NoPayMent = CostSum.ActualSumTotal;
                ObjCostSumBLL.Insert(CostSum);
            }

            InsertStatement();

        }

        #region 获取设计单的各项名称
        /// <summary>
        /// 获取名称
        /// </summary>
        public string GetTitle(int CustomerID, int Supplier)
        {
            var DataList = ObjDesignClassBLL.GetByCustomerId(CustomerID).Where(C => C.Supplier.ToInt32() == Supplier).ToList();
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

        #region 删除功能

        protected void repOtherCost_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            FL_CostSum ObjCostSumModel = ObjCostSumBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            ObjCostSumBLL.Delete(ObjCostSumModel);
            ObjStatementBLL.Delete(ObjCostSumModel.CostSumId);

            BinderData();
            JavaScriptTools.AlertWindow("删除成功!", Page);
        }
        #endregion

        #region 新增  酒店佣金  税金( 销售费用)
        protected void SaveSaleCost()
        {

            List<string> ObjSaleList = new List<string>();
            ObjSaleList.Add("酒店佣金");
            ObjSaleList.Add("税金");

            foreach (var Objitem in ObjSaleList)
            {
                var Model = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, Objitem);
                if (Model == null)
                {
                    ObjCostSumBLL.Insert(new DataAssmblly.FL_CostSum()
                    {
                        Name = Objitem,
                        CategoryName = Objitem,
                        Content = Objitem,
                        Sumtotal = 0,
                        ActualSumTotal = 0,
                        OrderID = Request["OrderID"].ToInt32(),
                        QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32()),
                        CustomerId = Request["CustomerID"].ToInt32(),
                        DispatchingID = Request["DispatchingID"].ToInt32(),
                        RowType = 12,
                        Evaluation = 6,
                        Advance = "",
                        ShortCome = "",
                        CreateDate = DateTime.Now,
                        EmployeeID = User.Identity.Name.ToInt32(),
                        Remark = ""
                    });
                }
            }
            BinderData();
        }
        #endregion

        #region 内部评价
        ///作内部评价
        ///

        #region 总评价     如果不存在就新增
        public void Insert()
        {
            //总体评价
            DispatchingID = Request["DispatchingID"].ToInt32();
            FL_DispathingSatisfaction SacModel = new FL_DispathingSatisfaction();
            SacModel.DispatchingID = DispatchingID;
            SacModel.SaEvaluationId = 6;
            SacModel.SatisfactionName = "总体满意度";
            SacModel.SatisfactionContent = "";
            SacModel.SatisfactionRemark = "";

            SacModel.EvaluationId = 6;
            SacModel.EvaluationName = "总体评价";
            SacModel.EvaluationContent = "";
            SacModel.EvaluationRemark = "";
            ObjSatisfictionBLL.Insert(SacModel);

            //流程执行
            FL_DispathingSatisfaction SacModel1 = new FL_DispathingSatisfaction();
            SacModel1.DispatchingID = DispatchingID;
            SacModel1.SaEvaluationId = 6;
            SacModel1.SatisfactionName = "总体执行";
            SacModel1.SatisfactionContent = "";
            SacModel1.SatisfactionRemark = "";

            SacModel1.EvaluationId = 6;
            SacModel1.EvaluationName = "流程执行";
            SacModel1.EvaluationContent = "";
            SacModel1.EvaluationRemark = "";
            ObjSatisfictionBLL.Insert(SacModel1);

            //场景布置
            FL_DispathingSatisfaction SacModel2 = new FL_DispathingSatisfaction();
            SacModel2.DispatchingID = DispatchingID;
            SacModel2.SaEvaluationId = 6;
            SacModel2.SatisfactionName = "总体布置";
            SacModel2.SatisfactionContent = "";
            SacModel2.SatisfactionRemark = "";

            SacModel2.EvaluationId = 6;
            SacModel2.EvaluationName = "场景布置";
            SacModel2.EvaluationContent = "";
            SacModel2.EvaluationRemark = "";
            ObjSatisfictionBLL.Insert(SacModel2);
        }
        #endregion

        #region 点击保存  分项保存价格
        /// <summary>
        /// 保存事件
        /// </summary>
        protected void btnSavePri_Click(object sender, EventArgs e)
        {
            //
            DispatchingID = Request["DispatchingID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            Button btn = sender as Button;
            if (btn.ID == "btnSavePerson")
            {
                UpdateCost(repEmployeeCost);
            }
            else if (btn.ID == "btnSaveMaterials")
            {
                UpdateCost(repSupplierCost);
            }
            else if (btn.ID == "btnSaveOrher")
            {
                UpdateCost(repOtherCost);
            }
            else if (btn.ID == "btnSaveSale")
            {
                UpdateCost(repSaleMoney);
            }
            else if (btn.ID == "btnSaveEvaulation")
            {
                UpdateSactisfaction(rptSatisfaction, sender);
            }


            BinderData();
            JavaScriptTools.AlertWindowAndLocation("修改成功!", this.Page.Request.Url.ToString(), Page);

        }
        #endregion

        #region 修改 (人员  物料  其他的内部评价)
        public void UpdateCost(Repeater rptItem)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                DropDownList ddlEvalution = currentItem.FindControl("ddlEvaluation") as DropDownList;
                TextBox txtContent = currentItem.FindControl("txtContent") as TextBox;
                TextBox txtRemark = currentItem.FindControl("txtRemark") as TextBox;
                TextBox txtPrice = currentItem.FindControl("txtSumTotal") as TextBox;
                TextBox txtPayMent = currentItem.FindControl("txtPayMent") as TextBox;
                HiddenField CostSumId = currentItem.FindControl("HiddenValue") as HiddenField;

                int value = CostSumId.Value.ToInt32();
                FL_CostSum CostSumModel = ObjCostSumBLL.GetByID(value);
                if (rptItem.ID == "repSaleMoney")
                {
                    CostSumModel.Evaluation = 6;
                }
                else
                {
                    CostSumModel.Evaluation = ddlEvalution.SelectedValue.ToInt32();
                }
                CostSumModel.Content = txtContent.Text.Trim().ToString();
                CostSumModel.Sumtotal = txtPrice.Text.ToString().ToDecimal();
                CostSumModel.ActualSumTotal = txtPrice.Text.ToString().ToDecimal();
                CostSumModel.PayMent = txtPayMent.Text.Trim().ToDecimal();
                CostSumModel.NoPayMent = (CostSumModel.ActualSumTotal - CostSumModel.PayMent).ToString().ToDecimal();
                CostSumModel.Remark = txtRemark.Text.ToString();
                CostSumModel.State = 1;         //说明已经评价
                ObjCostSumBLL.Update(CostSumModel);


                var StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, CostSumModel.Name);
                if (StatementModel != null)
                {
                    StatementModel.PrePayMent = txtPayMent.Text.ToString().ToDecimal();
                    StatementModel.NoPayMent = (StatementModel.SumTotal - StatementModel.PayMent - StatementModel.PrePayMent).ToString().ToDecimal();
                    ObjStatementBLL.Update(StatementModel);
                }
                else
                {
                    InsertStatement();
                }
            }

        }
        #endregion

        #region 修改总体评价 总体满意度
        public void UpdateSactisfaction(Repeater rptItem, object sender)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                DropDownList ddlEvalution = currentItem.FindControl("ddlEvaluation") as DropDownList;
                DropDownList ddlSaticfaction = currentItem.FindControl("ddlSaticfaction") as DropDownList;
                TextBox txtSumEvauluation = currentItem.FindControl("txtSumEvauluation") as TextBox;
                TextBox txtSumEvauluationRemark = currentItem.FindControl("txtSumEvauluationRemark") as TextBox;
                TextBox txtSumSaticfaction = currentItem.FindControl("txtSumSaticfaction") as TextBox;
                TextBox txtSumSaticfactionRemark = currentItem.FindControl("txtSumSaticfactionRemark") as TextBox;
                HiddenField SacId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = SacId.Value.ToInt32();

                FL_DispathingSatisfaction SacModel = ObjSatisfictionBLL.GetByID(value);
                SacModel.SaEvaluationId = ddlSaticfaction.SelectedValue.ToInt32();
                SacModel.SatisfactionContent = txtSumSaticfaction.Text.ToString();
                SacModel.SatisfactionRemark = txtSumSaticfactionRemark.Text.ToString();

                SacModel.EvaluationId = ddlEvalution.SelectedValue.ToInt32();
                SacModel.EvaluationContent = txtSumEvauluation.Text.ToString();
                SacModel.EvaluationRemark = txtSumEvauluationRemark.Text.ToString();
                SacModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjSatisfictionBLL.Update(SacModel);


                Button btn = sender as Button;
                if (btn.ID == "btn_OrderFinish")
                {
                    var SatisfictionModel = ObjSatisfictionBLL.GetByEvalationName("总体评价", DispatchingID);
                    var ObjCustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());  //更新顾客总体评价
                    ObjCustomerModel.EvaluationId = SatisfictionModel.EvaluationId;      //总体评价
                    ObjCustomerModel.UpdateEvaluTime = DateTime.Now.ToString().ToDateTime();    //评价时间
                    ObjCustomerModel.EvalState = 1;
                    ObjCustomersBLL.Update(ObjCustomerModel);
                }
            }
        }
        #endregion

        #region 点击确定事件(没有使用   已经和项目完结合并一起了)
        /// <summary>
        /// 确定
        /// </summary> 
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            UpdateCost(repEmployeeCost);
            UpdateCost(repSupplierCost);
            UpdateCost(repOtherCost);
            UpdateSactisfaction(rptSatisfaction, sender);
            //操作日志
            CreateHandle();
            JavaScriptTools.AlertWindowAndLocation("确认提交成功!", this.Page.Request.Url.ToString(), Page);
        }
        #endregion

        #endregion

        #region 项目完结
        /// <summary>
        /// 完结
        /// </summary>
        protected void btn_OrderFinish_Click(object sender, EventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            var CustomerModel = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            CustomerModel.State = 206;
            CustomerModel.EvalState = 1;
            CustomerModel.FinishOver = true;
            ObjCustomersBLL.Update(CustomerModel);

            //成本价格保存
            UpdateCost(repEmployeeCost);        //人员
            UpdateCost(repSupplierCost);        //物料
            UpdateCost(repOtherCost);           //其他
            UpdateCost(repSaleMoney);         //销售费用
            UpdateSactisfaction(rptSatisfaction, sender);

            //项目完结
            CreateHandle();

            JavaScriptTools.AlertWindowAndLocation("操作成功,项目成功完成", this.Page.Request.Url.ToString(), Page);

        }
        #endregion

        #region 保存结算表
        public void InsertStatement()
        {
            DispatchingID = Request["DispatchingID"].ToInt32();

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

                        if (ObjSupplierModel != null)
                        {
                            ObjStatementModel.TypeID = ObjSupplierModel.CategoryID;
                            ObjStatementModel.SupplierID = ObjSupplierModel.SupplierID;
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
                        var SupplyModel = ObjSuplierTypeBLL.GetByID(ObjStatementModel.TypeID);
                        if (SupplyModel != null)
                        {
                            ObjStatementModel.TypeName = SupplyModel.TypeName;
                        }
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
                        else
                        {
                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = 0;
                            ObjStatementModel.TypeID = -2;
                            ObjStatementModel.TypeName = "外部人员";
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
                    case 7:         //内部人员/四大金刚 (手动添加)  人员
                        var PersonModel = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModel == null)
                        {
                            var model = ObjFourGuardianBLL.GetByName(item.Name);
                            if (model != null)
                            {
                                ObjStatementModel.SupplierID = model.GuardianId;
                            }
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
                    case 8:         //内部人员/供应商 (手动添加)   物料
                        var PersonModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModels == null)
                        {
                            if (!string.IsNullOrEmpty(item.Name))
                            {
                                var model = ObjSupplierBLL.GetByName(item.Name);
                                if (model != null)
                                {
                                    ObjStatementModel.SupplierID = model.SupplierID;
                                }
                            }
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
                    case 9:         //内部人员/供应商 (手动添加)   其他
                        var OtherModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (OtherModels == null)            //不是人员  是供应商
                        {
                            var SupplierModel = ObjSupplierBLL.GetByName(item.Name);
                            if (SupplierModel != null)
                            {
                                ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                                ObjStatementModel.RowType = 1;
                            }
                            else
                            {
                                ObjStatementModel.SupplierID = 1;
                                ObjStatementModel.RowType = 5;
                            }
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
                ObjStatementModel.Year = ObjCustomersBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Year;
                ObjStatementModel.Month = ObjCustomersBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).Partydate.Value.Month;


                FL_Statement StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, item.Name);
                if (StatementModel != null)    //已经存在
                {
                    StatementModel.Name = ObjStatementModel.Name;               //名称
                    StatementModel.SupplierID = ObjStatementModel.SupplierID;   //供应商ID
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

        #region 获取人员 物料 其他的销售价
        /// <summary>
        /// Type类型 1.人员  2.物料 3.其他
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="TypeSource"></param>
        /// <returns></returns>

        public void GetFinishAmount(int Type)
        {
            decimal FinishAmount = 0;

            int OrderID = GetOrderIdByCustomerID(Request["CustomerID"].ToInt32());
            var ObjQuotedPriceList = ObjForTypeBLL.GetByOrderID(OrderID);

            if (ObjQuotedPriceList.Count > 0)       //单独存人员 物料 其他价格的表有数据  直接读取显示
            {
                for (int i = 1; i <= Type; i++)
                {
                    if (i == 1)
                    {
                        lblPersonSale.Text = ObjQuotedPriceList.Sum(C => C.PPrice).ToString().ToDecimal().ToString("f2");
                        //FinishAmount = ObjQuotedPriceList.Sum(C => C.PPrice).ToString().ToDecimal();
                    }
                    else if (i == 2)
                    {
                        lblMaterialSale.Text = ObjQuotedPriceList.Sum(C => C.MPrice).ToString().ToDecimal().ToString("f2");
                        //FinishAmount = ObjQuotedPriceList.Sum(C => C.MPrice).ToString().ToDecimal();
                    }
                    else if (i == 3)
                    {
                        lblQuotedOtherSale.Text = ObjQuotedPriceList.Sum(C => C.OPrice).ToString().ToDecimal().ToString("f2");
                        // FinishAmount = ObjQuotedPriceList.Sum(C => C.OPrice).ToString().ToDecimal();
                    }
                }
            }
            else            //如果不存在  就获取产品表的各个产品的总和
            {
                FinishAmount = ObjQuotedPriceItemsBLL.GetByOrdersID(OrderID).Where(C => C.Type == Type).Sum(C => C.Subtotal).ToString().ToDecimal(); ;
            }
        }
        #endregion

        #region 获取成本价
        /// <summary>
        /// 成本价
        /// </summary>
        public void GetCostAmounts(int Type)
        {
            //decimal CostAmount = 0;

            int DisID = Request["DispatchingID"].ToInt32();
            List<FL_CostSum> DataList = ObjCostSumBLL.GetByDispatchingID(DisID);

            if (DataList.Count > 0)       //单独存人员 物料 其他价格的表有数据  直接读取显示
            {
                for (int i = 1; i <= Type; i++)
                {
                    if (i == 1)
                    {
                        var PersonList = DataList.Where(C => C.RowType == 4 || C.RowType == 5 || C.RowType == 7).ToList();
                        lblPersonCost.Text = PersonList.Sum(C => C.Sumtotal).ToString().ToDecimal().ToString("f2");
                        //CostAmount = PersonList.Sum(C => C.Sumtotal).ToString().ToDecimal();
                    }
                    else if (i == 2)
                    {
                        var MaterialList = DataList.Where(C => C.RowType == 1 || C.RowType == 2 || C.RowType == 3 || C.RowType == 6 || C.RowType == 8 || C.RowType == 10).ToList();
                        lblMaterialCost.Text = MaterialList.Sum(C => C.Sumtotal).ToString().ToDecimal().ToString("f2");
                        //CostAmount = MaterialList.Sum(C => C.Sumtotal).ToString().ToDecimal();
                    }
                    else if (i == 3)
                    {
                        var OtherList = DataList.Where(C => C.RowType == 9 || C.RowType == 11);
                        lblQuotedOtherCost.Text = OtherList.Sum(C => C.Sumtotal).ToString().ToDecimal().ToString("f2");
                        //CostAmount = OtherList.Sum(C => C.Sumtotal).ToString().ToDecimal();
                    }
                }
            }

        }
        #endregion

        #region 获取毛利率
        /// <summary>
        /// 成本价
        /// </summary>
        public void GetProfitRate()
        {
            //人员毛利率
            if (lblPersonSale.Text.ToDecimal() > 0)
            {
                lblPersonRate.Text = ((lblPersonSale.Text.ToDecimal() - lblPersonCost.Text.ToDecimal()) / lblPersonSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblPersonRate.Text = "0.00%";
            }

            //物料毛利率
            if (lblMaterialSale.Text.ToDecimal() > 0)
            {
                lblMaterialRate.Text = ((lblMaterialSale.Text.ToDecimal() - lblMaterialCost.Text.ToDecimal()) / lblMaterialSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblMaterialRate.Text = "0.00%";
            }

            //其他毛利率
            if (lblQuotedOtherSale.Text.ToDecimal() > 0)
            {
                lblQuotedOtherRate.Text = ((lblQuotedOtherSale.Text.ToDecimal() - lblQuotedOtherCost.Text.ToDecimal()) / lblQuotedOtherSale.Text.ToDecimal()).ToString("0.00%");
            }
            else
            {
                lblQuotedOtherRate.Text = "0.00%";
            }

        }
        #endregion

        #region 获取供应商名称(设计清单)
        /// <summary>
        /// 获取供应商名称
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public string GetSupplierName(object Source)
        {
            int SupplierID = Source.ToString().ToInt32();
            var Model = ObjSupplierBLL.GetByID(SupplierID);
            if (Model != null)
            {
                return Model.Name;
            }
            return "";
        }
        #endregion

        #region 保存成本明细(手动添加)
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button btnSave = (sender as Button);
            if (btnSave.ID == "btnSave")
            {
                SaveDesignCost("Add", "Person");
            }
            else if (btnSave.ID == "btnSaveMaterial")
            {
                SaveDesignCost("Add", "Material");
            }
            else if (btnSave.ID == "btnSaveOther")
            {
                SaveDesignCost("Add", "Other");
            }
            else if (btnSave.ID == "btnSaveSaleTitle")
            {
                SaveDesignCost("Add", "Sale");
            }
            Response.Redirect(this.Page.Request.Url.ToString());


        }
        #endregion

        #region 保存 读取 四大金刚名称(隐藏)
        /// <summary>
        /// 保存
        /// </summary>  
        protected void btnFourGuardianSave_Click(object sender, EventArgs e)
        {
            int GuraianID = hideSuppID.Value.ToInt32();
            var Model = ObjFourGuardianBLL.GetByID(GuraianID);
            if (Model != null)
            {
                txtSuppName.Text = Model.GuardianName.ToString();
            }
        }
        #endregion

        #region 保存 读取 供应商(隐藏)
        /// <summary>
        /// 保存供应商
        /// </summary>
        protected void btnSavesupperSave_Click(object sender, EventArgs e)
        {
            int SupplierID = HideSupplier.Value.ToInt32();
            var Model = ObjSupplierBLL.GetByID(SupplierID);
            if (Model != null)
            {
                txtSupplier.Text = Model.Name.Trim().ToString();
            }
        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32());
            HandleModel.HandleContent = "派工管理-执行中订单,客户姓名:" + Model.Bride + "/" + Model.Groom + ",项目确认完结";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 4;     //派工管理
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 不是派工人 就不能看见价格
        protected string StatuHideViewInviteInfo()
        {
            //return GetTypes() == true ? "style='display:none'" : string.Empty;
            return string.Empty;
        }
        #endregion

        #region 隐藏删除
        /// <summary>
        /// 隐藏删除
        /// </summary>  
        public void DeleteIsVisible(Repeater rep, int Type)
        {
            for (int i = 0; i < rep.Items.Count; i++)
            {
                var ObjItem = rep.Items[i];
                int RowType = (ObjItem.FindControl("HideRowType") as HiddenField).Value.ToString().ToInt32();
                int EvalState = GetEvalState(Request["CustomerID"].ToInt32());
                if (Type == 1)
                {
                    if (RowType != 7 || EvalState == 1)           //不是手动添加的人员和已评价的   就不能删除 lbtnDelete
                    {
                        (ObjItem.FindControl("lbtnDelete") as LinkButton).Visible = false;
                    }
                }
                else if (Type == 2)
                {
                    if (RowType != 8 || EvalState == 1)           //不是手动添加的物料和已评价的    就不能删除
                    {
                        (ObjItem.FindControl("lbtnDelete") as LinkButton).Visible = false;
                    }
                }
                else if (Type == 3)
                {
                    if (RowType != 9 || EvalState == 1)           //不是手动添加的其它和已评价的    就不能删除
                    {
                        (ObjItem.FindControl("lbtnDelete") as LinkButton).Visible = false;
                    }
                }
            }
        }
        #endregion

        #region 获取客户的状态
        /// <summary>
        /// 获取状态
        /// </summary>
        public int GetState(object Source)
        {
            if (Source != null)
            {
                int CustomerID = Source.ToString().ToInt32();
                var Model = ObjCustomersBLL.GetByID(CustomerID);
                if (Model != null)
                {
                    return Model.State.ToString().ToInt32();
                }
            }
            return 0;
        }
        #endregion

        #region 获取已支付或未支付的金额
        /// <summary>
        /// 获取支付的金额
        /// </summary>
        public string GetPayNoMent(object Source, int Type)
        {
            string name = Source.ToString();
            var DataList = ObjStatementBLL.GetListByDispatchingID(DispatchingID, name);
            if (Type == 1)                  //已支付
            {
                return DataList.Sum(C => C.PayMent).ToString();
            }
            else if (Type == 2)             //未支付
            {
                return DataList.Sum(C => C.NoPayMent).ToString();
            }
            return "";
        }
        #endregion
    }
}