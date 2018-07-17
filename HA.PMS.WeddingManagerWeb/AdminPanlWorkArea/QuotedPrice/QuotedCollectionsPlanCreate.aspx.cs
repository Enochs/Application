using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedCollectionsPlanCreate : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new BLLAssmblly.Flow.QuotedCollectionsPlan();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        HA.PMS.BLLAssmblly.Flow.Order ObjOrderBLL = new BLLAssmblly.Flow.Order();

        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();

        Customers ObjCustomerBLL = new Customers();

        Employee ObjEmployeeBLL = new Employee();
        Report ObjReportBLL = new Report();
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Target ObjTargetBLL = new Target();

        int CustomerID = 0;


        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetShowLoss();
                BinderData();
                if (Request["NeedPopu"].ToString() == "2")
                {
                    tr_modify.Visible = false;
                    btnCreate.Visible = false;
                }
                txtTimer.Text = DateTime.Now.ToShortDateString();
                CustomerID = Request["CustomerID"].ToInt32();
                ShowOrHide(true, false, "修改", 1);

            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var BinderList = ObjQuotedCollectionsPlanBLL.GetByOrderID(Request["OrderID"].ToInt32());
            lblSaleAmount.Text = ObjQuotedPriceBLL.GetByID(Request["QuotedID"].ToInt32()).FinishAmount.ToString().ToDecimal().ToString("0.00");      //合同款
            lblFinishMoney.Text = BinderList.Sum(C => C.RealityAmount).ToString();                                      //已收款
            lblHaveMoney.Text = (lblSaleAmount.Text.ToDecimal() - lblFinishMoney.Text.ToDecimal()).ToString();          //余款

            if (Request["Type"] == "Loss")      //是退款页面   就能看见该选项
            {
                BinderList = BinderList.Where(C => C.Node.Contains("退款") || C.Node.Contains("退订")).ToList();
            }

            this.repDataKist.DataSource = BinderList;
            this.repDataKist.DataBind();

            if (repDataKist.Items.Count > 0)
            {
                Sys_Employee Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
                if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) || (Model.EmployeeID == 77 && Model.EmployeeName == "王卫纲"))     //是管理层  就可以看见修改按钮
                {
                    btn_Updates.Visible = true;
                    btn_SaleAmount.Visible = true;
                }
                else
                {
                    btn_Updates.Visible = false;
                    btn_SaleAmount.Visible = false;
                }

            }
        }
        #endregion

        #region 点击保存事件
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.ToDecimal() == 0)
            {
                JavaScriptTools.AlertWindow("收款金额不能为0", Page);
                return;
            }
            if (Request["Type"] == null)       //财务才能退款退款   
            {
                if (txtAmount.Text.ToDecimal() < 0)
                {
                    JavaScriptTools.AlertWindow("只有财务才能退款,请输入收款金额(正数)", Page);
                    return;
                }
            }

            string Nodes = "";
            string FileAddress = "";
            string FileNames = "";
            if (Request["Type"] == "Loss")      //是退款页面   就能看见该选项
            {
                if (rdoNodes.SelectedValue.ToInt32() == 4)          //退款
                {
                    Nodes = rdoNodes.SelectedItem.Text + " 说明：" + txtNode.Text.Trim().ToString();
                }
                else if (rdoNodes.SelectedValue.ToInt32() == 5)          //退订
                {
                    Nodes = rdoNodes.SelectedItem.Text + " 说明：" + txtNode.Text.Trim().ToString();
                }
            }
            else
            {
                if (rdoNodes.SelectedValue.ToInt32() == 4)      //其他
                {
                    Nodes = rdoNodes.SelectedItem.Text + "--说明：" + txtNode.Text.Trim().ToString(); ;
                }
                else                                     //          订金/中期款/余款
                {
                    Nodes = rdoNodes.SelectedItem.Text;
                }
            }

            if (FileUpload1.HasFile)
            {
                FileAddress = "/AdminPanlWorkArea/QuotedPrice/LossImage/" + Guid.NewGuid().ToString() + ".jpg";
                FileNames = FileUpload1.FileName.ToString();
                if (System.IO.Directory.Exists(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/")))
                {
                    FileUpload1.SaveAs(Server.MapPath(FileAddress));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/"));
                    FileUpload1.SaveAs(Server.MapPath(FileAddress));
                }
            }

            int OrderID = Request["OrderID"].ToInt32();
            var PlanList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID);
            int EmployeeId = 0;
            if (PlanList.Count > 0)
            {
                if (Request["Type"] == "Loss")       //退款   退款金额就该订单的从最后一个收款人的现金流中减掉
                {
                    int PlanId = PlanList.Max(C => C.PlanID).ToString().ToInt32();
                    EmployeeId = ObjQuotedCollectionsPlanBLL.GetByID(PlanId).CreateEmpLoyee.ToString().ToInt32();

                    //修改状态为 退单
                    CustomerID = Request["CustomerID"].ToInt32();
                    var Model = ObjCustomerBLL.GetByID(CustomerID);
                    if (rdoNodes.SelectedValue.ToInt32() == 5)          //退订 需要改变状态
                    {
                        Model.State = 20;
                    }
                    ObjCustomerBLL.Update(Model);

                }
                else
                {
                    EmployeeId = User.Identity.Name.ToInt32();
                }
            }
            else            //第一次收款
            {
                EmployeeId = User.Identity.Name.ToInt32();
            }


            int result = ObjQuotedCollectionsPlanBLL.Insert(new DataAssmblly.FL_QuotedCollectionsPlan()
            {
                OrderID = Request["OrderID"].ToInt32(),
                QuotedID = Request["QuotedID"].ToInt32(),
                RealityAmount = txtAmount.Text.ToDecimal(),
                CollectionTime = txtTimer.Text.ToDateTime().ToShortDateString().ToDateTime(),
                CreateEmpLoyee = User.Identity.Name.ToInt32(),
                AmountEmployee = EmployeeId,
                FinancialEmployee = EmployeeId,
                CreateDate = txtTimer.Text.ToDateTime().ToShortDateString().ToDateTime(),
                Amountmoney = txtAmount.Text.ToDecimal(),
                RowLock = false,
                Node = Nodes,
                MoneyType = rdoMoneytype.SelectedItem.Text,
                State = 0,
                FileName = FileNames.ToString(),
                FileUrl = FileAddress.ToString()

            });

            if (txtTimer.Text.ToDateTime().ToShortDateString().ToDateTime().Year == DateTime.Now.Year)
            {
                GetRealitySumMoney();
            }

            //操作日志  收款
            CreateHandle(1);
            JavaScriptTools.AlertWindowAndLocation("保存成功", Request.Url.ToString(), Page);

        }
        #endregion

        #region 获取现金流 头部显示

        public void GetRealitySumMoney(int CreateEmployee = 2)
        {
            if (CreateEmployee == 2)            //没有传入CreateEmployee   默认就是当前登录人
            {
                CreateEmployee = User.Identity.Name.ToInt32();
            }
            int EmployeeId = CreateEmployee;

            int OrderID = Request["OrderID"].ToInt32();
            var PlanList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID);

            if (PlanList.Count > 0)
            {
                if (Request["Type"] == "Loss")       //退款   退款金额就该订单的从最后一个收款人的现金流中减掉
                {
                    int PlanId = PlanList.Max(C => C.PlanID).ToString().ToInt32();
                    EmployeeId = ObjQuotedCollectionsPlanBLL.GetByID(PlanId).AmountEmployee.ToString().ToInt32();
                }
            }
            else            //第一次收款
            {
                return;
            }


            Sys_Employee employee = ObjEmployeeBLL.GetByID(EmployeeId);

            FL_FinishTargetSum FinishTargetSum = new FL_FinishTargetSum();
            FL_Target target = new FL_Target();

            var FinishTargetList = ObjFinishTargetSumBLL.GetByAll();
            var TargetList = ObjTargetBLL.GetByAll();

            var QuotedCollectionPlanList = ObjQuotedCollectionsPlanBLL.GetByOrderID(Request["OrderID"].ToInt32());

            bool isExists = false;
            List<FL_FinishTargetSum> FinishTargetSumList = new List<FL_FinishTargetSum>();
            foreach (var item in FinishTargetList)
            {
                if (item.EmployeeID == CreateEmployee && item.TargetTitle == "现金流" && item.Year == DateTime.Now.Year)      //判断改用户 现金流是否存在 存在则返回True
                {
                    FinishTargetSumList.Add(item);
                    isExists = true;
                }
            }

            if (isExists == true)       //该用户现金流已经存在  只需修改
            {
                foreach (var item in FinishTargetSumList)
                {
                    if (item.TargetTitle == "现金流" && item.Year == DateTime.Now.Year)
                    {
                        //FinishTargetSum = ObjFinishTargetSumBLL.GetByID(item.FinishKey);
                        if (Request["Type"] == "Loss")       //退款   退款金额就该订单的从最后一个收款人的现金流中减掉
                        {
                            GetUpdateQuotePriceByMonth(item, EmployeeId);
                        }
                        else
                        {
                            GetUpdateQuotePriceByMonth(item, CreateEmployee);
                        }

                        ObjFinishTargetSumBLL.Update(item);
                    }
                }
            }




            if (isExists == false)      //该用户现金流不存在  需要新增
            {
                foreach (var item in TargetList)
                {
                    if (item.TargetTitle == "现金流")
                    {
                        FinishTargetSum = new FL_FinishTargetSum();
                        GetAddQuotePriceByMonth(FinishTargetSum, CreateEmployee);

                        FinishTargetSum.TargetID = item.TargetID;
                        FinishTargetSum.TargetTitle = item.TargetTitle;
                        FinishTargetSum.DepartmentID = employee.DepartmentID;
                        FinishTargetSum.EmployeeID = EmployeeId;
                        FinishTargetSum.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        FinishTargetSum.Year = DateTime.Now.Year;
                        FinishTargetSum.CreateEmployeeID = User.Identity.Name.ToInt32();
                        FinishTargetSum.LastYearCompletionrate = 0;
                        FinishTargetSum.FinishSum = 0;
                        FinishTargetSum.OveryearRate = 0;
                        FinishTargetSum.OverYearFinishSum = 0;
                        FinishTargetSum.LastYearFinishSum = 0;
                        FinishTargetSum.Unite = "个";
                        FinishTargetSum.IsActive = false;

                        ObjFinishTargetSumBLL.Insert(FinishTargetSum);
                    }
                }
            }

            //添加现金流  已经被添加的数据状态就变为1
            int OrderId = Request["OrderID"].ToInt32();
            List<FL_QuotedCollectionsPlan> QCPList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderId);
            foreach (var item in QCPList)
            {
                item.State = 1;
                ObjQuotedCollectionsPlanBLL.Update(item);
            }



        }
        #endregion

        #region 新增现金流

        public void GetAddQuotePriceByMonth(FL_FinishTargetSum FinishTargetSum, int EmployeeId)
        {
            int OrderID = Request["OrderID"].ToInt32();
            FinishTargetSum.MonthPlan1 = 0;
            FinishTargetSum.MonthFinsh1 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 1);
            FinishTargetSum.MonthPlan2 = 0;
            FinishTargetSum.MonthFinish2 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 2);
            FinishTargetSum.MonthPlan3 = 0;
            FinishTargetSum.MonthFinish3 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 3);
            FinishTargetSum.MonthPlan4 = 0;
            FinishTargetSum.MonthFinish4 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 4);
            FinishTargetSum.MonthPlan5 = 0;
            FinishTargetSum.MonthFinish5 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 5);
            FinishTargetSum.MonthPlan6 = 0;
            FinishTargetSum.MonthFinish6 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 6);
            FinishTargetSum.MonthPlan7 = 0;
            FinishTargetSum.MonthFinish7 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 7);
            FinishTargetSum.MonthPlan8 = 0;
            FinishTargetSum.MonthFinish8 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 8);
            FinishTargetSum.MonthPlan9 = 0; ;
            FinishTargetSum.MonthFinish9 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 9);
            FinishTargetSum.MonthPlan10 = 0; ;
            FinishTargetSum.MonthFinish10 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 10);
            FinishTargetSum.MonthPlan11 = 0; ;
            FinishTargetSum.MonthFinish11 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 11);
            FinishTargetSum.MonthPlan12 = 0; ;
            FinishTargetSum.MonthFinish12 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 12);
            FinishTargetSum.Completionrate = 0;
            FinishTargetSum.PlanSum = FinishTargetSum.MonthPlan1 + FinishTargetSum.MonthPlan2 + FinishTargetSum.MonthPlan3 + FinishTargetSum.MonthPlan4 + FinishTargetSum.MonthPlan5 + FinishTargetSum.MonthPlan6 + FinishTargetSum.MonthPlan7 + FinishTargetSum.MonthPlan8 + FinishTargetSum.MonthPlan9 + FinishTargetSum.MonthPlan10 + FinishTargetSum.MonthPlan11 + FinishTargetSum.MonthPlan12;
        }
        #endregion

        #region 修改现金流

        public void GetUpdateQuotePriceByMonth(FL_FinishTargetSum FinishTargetSum, int EmployeeId)
        {
            int OrderID = Request["OrderID"].ToInt32();
            FinishTargetSum.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            FinishTargetSum.MonthFinsh1 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 1);
            FinishTargetSum.MonthFinish2 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 2);
            FinishTargetSum.MonthFinish3 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 3);
            FinishTargetSum.MonthFinish4 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 4);
            FinishTargetSum.MonthFinish5 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 5);
            FinishTargetSum.MonthFinish6 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 6);
            FinishTargetSum.MonthFinish7 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 7);
            FinishTargetSum.MonthFinish8 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 8);
            FinishTargetSum.MonthFinish9 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 9);
            FinishTargetSum.MonthFinish10 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 10);
            FinishTargetSum.MonthFinish11 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 11);
            FinishTargetSum.MonthFinish12 += ObjQuotedCollectionsPlanBLL.GetRealityAmountSum(OrderID, 12);
            FinishTargetSum.Completionrate = 0;
        }
        #endregion

        #region 点击确认生成头部报表
        /// <summary>
        /// 头部报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repDataKist.Items.Count; i++)
            {
                var ObjItem = repDataKist.Items[i];
                Label lblTimes = ObjItem.FindControl("lblCollectionTime") as Label;

                int PlanID = (ObjItem.FindControl("HidePlanId") as HiddenField).Value.ToInt32();
                if (lblTimes.Text.ToDateTime().ToShortDateString().ToDateTime().Year == DateTime.Now.Year)
                {
                    var Model = ObjQuotedCollectionsPlanBLL.GetByID(PlanID);
                    if (Model != null)
                    {
                        GetRealitySumMoney(Model.CreateEmpLoyee.ToString().ToInt32());
                    }
                    else
                    {
                        GetRealitySumMoney();
                    }
                }
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改合同金额
        /// </summary>
        protected void btn_Updates_Click(object sender, EventArgs e)
        {

            Button btnHandle = (sender as Button);
            if (btnHandle.ID == "btn_Updates")
            {
                if (btnHandle.Text == "修改")
                {
                    ShowOrHide(false, true, btnHandle.Text);
                    btnHandle.Text = "确定";
                }
                else if (btnHandle.Text == "确定")
                {
                    ShowOrHide(true, false, btnHandle.Text);
                    btnHandle.Text = "修改";
                }
            }
            else if (btnHandle.ID == "btn_SaleAmount")
            {
                if (btnHandle.Text == "合同金额")
                {
                    lblSaleAmount.Visible = false;
                    txtSaleAmount.Visible = true;
                    txtSaleAmount.Text = lblSaleAmount.Text;
                    btnHandle.Text = "确定";
                }
                else if (btnHandle.Text == "确定")
                {
                    var model = ObjQuotedPriceBLL.GetByQuotedID(Request["QuotedID"].ToInt32());

                    model.FinishAmount = txtSaleAmount.Text.ToString().ToDecimal();
                    model.RealAmount = txtSaleAmount.Text.ToString().ToDecimal();
                    ObjQuotedPriceBLL.Update(model);

                    QuotedPriceForType ObjForTypeBLL = new QuotedPriceForType();
                    var DiscountModel = ObjForTypeBLL.GetByOrderID(Request["OrderID"].ToInt32(), 0);
                    DiscountModel.Total = txtSaleAmount.Text.ToString().ToDecimal();
                    ObjForTypeBLL.Update(DiscountModel);

                    //操作日志  修改合同金额
                    CreateHandle(2);
                    lblSaleAmount.Visible = true;
                    txtSaleAmount.Visible = false;
                    btnHandle.Text = "合同金额";
                    BinderData();
                }
            }
        }
        #endregion

        #region 隐藏或显示
        /// <summary>
        /// 修改每项收款的功能
        /// </summary>
        /// <param name="IsShow"></param>
        /// <param name="IsHide"></param>
        /// <param name="Text"></param>

        public void ShowOrHide(bool IsShow, bool IsHide, string Text, int index = 0)
        {
            for (int i = 0; i < repDataKist.Items.Count; i++)
            {
                var ObjItem = repDataKist.Items[i];
                int PlanId = ((HiddenField)ObjItem.FindControl("HidePlanId")).Value.ToInt32();
                FL_QuotedCollectionsPlan ObjPlanModel = ObjQuotedCollectionsPlanBLL.GetByID(PlanId);

                Label lblCollectionTime = (Label)ObjItem.FindControl("lblCollectionTime");
                Label lblRealityAmount = (Label)ObjItem.FindControl("lblRealityAmount");
                Label lblMoneyType = (Label)ObjItem.FindControl("lblMoneyType");
                Label lblNode = (Label)ObjItem.FindControl("lblNode");
                Button btnDel = (Button)ObjItem.FindControl("btn_del");

                TextBox txtCollectionTime = (TextBox)ObjItem.FindControl("txtCollectionTime");
                TextBox txtRealityAmount = (TextBox)ObjItem.FindControl("txtRealityAmount");
                RadioButtonList rdoMoneytypes = (RadioButtonList)ObjItem.FindControl("rdoMoneytypes");
                RadioButtonList rdoNode = (RadioButtonList)ObjItem.FindControl("rdoNode");

                TextBox txtNode = (TextBox)ObjItem.FindControl("txtNode");
                if (IsShow == false && IsHide == true)            //文本框显示 可以修改
                {
                    lblCollectionTime.Visible = IsShow;
                    lblRealityAmount.Visible = IsShow;
                    lblNode.Visible = IsShow;
                    txtCollectionTime.Visible = IsHide;
                    txtRealityAmount.Visible = IsHide;
                    thHandle.Visible = IsHide;
                    btnDel.Visible = IsHide;
                    if (ObjPlanModel.Node == "定金" || ObjPlanModel.Node == "中期款" || ObjPlanModel.Node == "余款")
                    {
                        rdoNode.Visible = IsHide;
                        if (ObjPlanModel.Node == "定金")
                        {
                            rdoNode.SelectedValue = "1";
                        }
                        else if (ObjPlanModel.Node == "中期款")
                        {
                            rdoNode.SelectedValue = "2";
                        }
                        else if (ObjPlanModel.Node == "余款")
                        {
                            rdoNode.SelectedValue = "3";
                        }
                    }
                    else
                    {
                        txtNode.Visible = IsHide;
                    }
                }
                else if (IsShow == true && IsHide == false)     //修改完成 显示
                {
                    lblCollectionTime.Visible = IsShow;
                    lblRealityAmount.Visible = IsShow;
                    lblNode.Visible = IsShow;
                    txtCollectionTime.Visible = IsHide;
                    txtRealityAmount.Visible = IsHide;
                    txtNode.Visible = IsHide;
                    thHandle.Visible = IsHide;
                    btnDel.Visible = IsHide;
                    decimal OldRealityAmount = ObjPlanModel.RealityAmount.ToString().ToDecimal();
                    if (index == 0)             //点击修改 默认加载时不执行
                    {
                        //有变化  才添加操作日志  修改收款
                        if (ObjPlanModel.RealityAmount.ToString() != txtRealityAmount.Text.ToString())
                        {
                            HandleLog ObjHandleBLL = new HandleLog();
                            sys_HandleLog HandleModel = new sys_HandleLog();
                            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());

                            HandleModel.HandleContent = "策划报价-收款管理-修改收款金额,客户姓名:" + Model.Bride + "/" + Model.Groom + ",修改前收款金额:" +
                                ObjPlanModel.RealityAmount + ",修改金额：" + txtRealityAmount.Text + ",修改前收款时间：" + ObjPlanModel.CollectionTime.ToString().ToDateTime().ToShortDateString() +
                                ",修改后收款时间：" + txtCollectionTime.Text.ToString().ToDateTime().ToShortDateString();

                            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
                            HandleModel.HandleCreateDate = DateTime.Now;
                            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
                            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
                            HandleModel.HandleType = 10;     //收款
                            ObjHandleBLL.Insert(HandleModel);
                        }

                        ObjPlanModel.CollectionTime = txtCollectionTime.Text.ToDateTime().ToShortDateString().ToDateTime();
                        ObjPlanModel.RealityAmount = txtRealityAmount.Text.ToDecimal();
                        ObjPlanModel.Amountmoney = txtRealityAmount.Text.ToDecimal();
                        ObjPlanModel.CollectionTime = txtCollectionTime.Text.Trim().ToDateTime();
                        if (ObjPlanModel.Node == "定金" || ObjPlanModel.Node == "中期款" || ObjPlanModel.Node == "余款")
                        {
                            ObjPlanModel.Node = rdoNode.SelectedItem.Text;
                        }
                        else
                        {
                            ObjPlanModel.Node = txtNode.Text.ToString();
                        }

                        ObjPlanModel.State = 0;
                        ObjQuotedCollectionsPlanBLL.Update(ObjPlanModel);
                        var TargetModel = ObjFinishTargetSumBLL.GetByEmployeeIDTitle(ObjPlanModel.CreateEmpLoyee, ObjPlanModel.CollectionTime.ToString().ToDateTime().Year, "现金流");
                        UpdateTargetSum(TargetModel, OldRealityAmount);

                    }
                }
            }
            if (IsShow == true && IsHide == false)
            {
                BinderData();
            }
        }
        #endregion

        #region 绑定事件 删除
        /// <summary>
        /// 删除
        /// </summary>
        protected void repDataKist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int PlanId = ((HiddenField)e.Item.FindControl("HidePlanId")).Value.ToInt32();
            if (e.CommandName == "Del")
            {
                FL_QuotedCollectionsPlan ObjPlanModel = ObjQuotedCollectionsPlanBLL.GetByID(PlanId);

                var ObjFinishTargetSum = ObjFinishTargetSumBLL.GetByEmployeeIDTitle(ObjPlanModel.CreateEmpLoyee, ObjPlanModel.CollectionTime.ToString().ToDateTime().Year, "现金流");
                int month = ObjPlanModel.CollectionTime.ToString().ToDateTime().Month;
                decimal RealityAmount = ObjPlanModel.RealityAmount.ToString().ToDecimal();
                switch (month)
                {
                    case 1:
                        ObjFinishTargetSum.MonthFinsh1 -= RealityAmount;
                        break;
                    case 2:
                        ObjFinishTargetSum.MonthFinish2 -= RealityAmount;
                        break;
                    case 3:
                        ObjFinishTargetSum.MonthFinish3 -= RealityAmount;
                        break;
                    case 4:
                        ObjFinishTargetSum.MonthFinish4 -= RealityAmount;
                        break;
                    case 5:
                        ObjFinishTargetSum.MonthFinish5 -= RealityAmount;
                        break;
                    case 6:
                        ObjFinishTargetSum.MonthFinish6 -= RealityAmount;
                        break;
                    case 7:
                        ObjFinishTargetSum.MonthFinish7 -= RealityAmount;
                        break;
                    case 8:
                        ObjFinishTargetSum.MonthFinish8 -= RealityAmount;
                        break;
                    case 9:
                        ObjFinishTargetSum.MonthFinish9 -= RealityAmount;
                        break;
                    case 10:
                        ObjFinishTargetSum.MonthFinish10 -= RealityAmount;
                        break;
                    case 11:
                        ObjFinishTargetSum.MonthFinish11 -= RealityAmount;
                        break;
                    case 12:
                        ObjFinishTargetSum.MonthFinish12 -= RealityAmount;
                        break;
                }
                ObjFinishTargetSumBLL.Update(ObjFinishTargetSum);

                HandleLog ObjHandleBLL = new HandleLog();
                sys_HandleLog HandleModel = new sys_HandleLog();
                var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());

                HandleModel.HandleContent = "策划报价-收款管理-删除收款,客户姓名:" + Model.Bride + "/" + Model.Groom + ",删除本条记录,收款金额：" + ObjPlanModel.RealityAmount;
                HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
                HandleModel.HandleCreateDate = DateTime.Now;
                HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
                HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
                HandleModel.HandleType = 10;     //收款
                ObjHandleBLL.Insert(HandleModel);

                int result = ObjQuotedCollectionsPlanBLL.Delete(ObjPlanModel);
            }
            BinderData();
        }
        #endregion

        #region 修改价格 保存现金流
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateTargetSum(FL_FinishTargetSum TargetModel, decimal OldRealityAmount)
        {
            int OrderID = Request["OrderID"].ToInt32();
            FL_QuotedCollectionsPlan ObjPlanModel = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID).Where(C => C.State == 0).FirstOrDefault();
            int month = ObjPlanModel.CollectionTime.Value.Month;
            decimal RealityAmount = ObjPlanModel.RealityAmount.ToString().ToDecimal();

            switch (month)
            {
                case 1:
                    TargetModel.MonthFinsh1 = TargetModel.MonthFinsh1 - OldRealityAmount + RealityAmount;
                    break;
                case 2:
                    TargetModel.MonthFinish2 = TargetModel.MonthFinish2 - OldRealityAmount + RealityAmount;
                    break;
                case 3:
                    TargetModel.MonthFinish3 = TargetModel.MonthFinish3 - OldRealityAmount + RealityAmount;
                    break;
                case 4:
                    TargetModel.MonthFinish4 = TargetModel.MonthFinish4 - OldRealityAmount + RealityAmount;
                    break;
                case 5:
                    TargetModel.MonthFinish5 = TargetModel.MonthFinish5 - OldRealityAmount + RealityAmount;
                    break;
                case 6:
                    TargetModel.MonthFinish6 = TargetModel.MonthFinish6 - OldRealityAmount + RealityAmount;
                    break;
                case 7:
                    TargetModel.MonthFinish7 = TargetModel.MonthFinish7 - OldRealityAmount + RealityAmount;
                    break;
                case 8:
                    TargetModel.MonthFinish8 = TargetModel.MonthFinish8 - OldRealityAmount + RealityAmount;
                    break;
                case 9:
                    TargetModel.MonthFinish9 = TargetModel.MonthFinish9 - OldRealityAmount + RealityAmount;
                    break;
                case 10:
                    TargetModel.MonthFinish10 = TargetModel.MonthFinish10 - OldRealityAmount + RealityAmount;
                    break;
                case 11:
                    TargetModel.MonthFinish11 = TargetModel.MonthFinish11 - OldRealityAmount + RealityAmount;
                    break;
                case 12:
                    TargetModel.MonthFinish12 = TargetModel.MonthFinish12 - OldRealityAmount + RealityAmount;
                    break;
            }
            ObjFinishTargetSumBLL.Update(TargetModel);

            ObjPlanModel.State = 1;
            ObjQuotedCollectionsPlanBLL.Update(ObjPlanModel);
        }
        #endregion

        #region 退款选项  退款的页面才能看见
        public void GetShowLoss()
        {
            if (Request["Type"] == "Loss")      //是退款页面   就能看见该选项
            {
                rdoNodes.Items.Clear();
                rdoNodes.Items.Insert(0, new ListItem { Text = "退款", Value = "4" });
                rdoNodes.Items.Insert(1, new ListItem { Text = "退订", Value = "5" });
                rdoNodes.Items.FindByValue("4").Selected = true;
                tr_getMoney.Visible = false;
                tr_LoseMoney.Visible = true;
            }

        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int Type)
        {

            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            if (Type == 1)
            {
                HandleModel.HandleContent = "策划报价-收款管理-正常收款,客户姓名:" + Model.Bride + "/" + Model.Groom + ",收款金额:" + txtAmount.Text;
            }
            else if (Type == 2)
            {
                HandleModel.HandleContent = "策划报价-收款管理-修改合同金额,客户姓名:" + Model.Bride + "/" + Model.Groom + ",合同金额:" + txtSaleAmount.Text;
            }


            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 10;     //收款
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        protected string GetImgPath(object source)
        {
            string newPath = (source + string.Empty);
            return newPath.Replace("~", "/.");
        }

        #region Repeater数据绑定完成事件  没上传图片 就隐藏图片控件
        /// <summary>
        /// 完成事件
        /// </summary>
        protected void repDataKist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FL_QuotedCollectionsPlan Model = (FL_QuotedCollectionsPlan)e.Item.DataItem;
            if (Model.FileUrl == "" || Model.FileUrl == null)
            {
                Image img = e.Item.FindControl("img") as Image;
                img.Visible = false;
            }

        }
        #endregion


    }
}