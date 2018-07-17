using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport
{
    public partial class StatementManager : HA.PMS.Pages.SystemPage
    {
        /// <summary>
        /// 结算表
        /// </summary>
        Statement ObjStatementBLL = new Statement();

        /// <summary>
        /// 四大金刚
        /// </summary>
        GuardianType ObjGuardianTypeBLL = new GuardianType();

        FourGuardian ObjFourGuardBLL = new FourGuardian();

        /// <summary>
        /// 供应商类型
        /// </summary>
        Supplier ObjSupplierBLL = new Supplier();

        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeBLL = new Employee();

        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 支付记录
        /// </summary>
        StatementPayFor ObjPayMentBLL = new StatementPayFor();

        int SourceCount = 0;
        string OrderByColumnName = "CreateDate";
        string DateRanger = "";

        int currentpage = 1;

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["currentpage"] = currentpage;
                DataBinder(1);
            }
        }
        #endregion

        #region 数据绑定方法
        /// <summary>
        /// 加载
        /// </summary>
        public void DataBinder(int Type)
        {
            List<PMSParameters> pars = new List<PMSParameters>();

            //供应商点击查看跳转过来 查看相应的数据
            if (Request["RowType"] != null)
            {
                if (Request["RowType"].ToInt32() == 1)        //供应商
                {
                    Supplier ObjSupllierBLL = new Supplier();
                    int SupplierID = Request["SupplierID"].ToInt32();
                    txtName.Text = ObjSupllierBLL.GetByID(SupplierID).Name;

                    pars.Add("RowType", 1, NSqlTypes.Equal);
                    pars.Add("SupplierID", SupplierID, NSqlTypes.Equal);
                }
                else if (Request["RowType"].ToInt32() == 4)     //四大金刚
                {
                    //GuradianFile ObjGuardBLL = new GuradianFile();
                    FourGuardian ObjGuardBLL = new FourGuardian();
                    int GuardianID = Request["GuardianID"].ToInt32();
                    txtName.Text = ObjGuardBLL.GetByID(GuardianID).GuardianName;

                    pars.Add("RowType", 4, NSqlTypes.Equal);
                    pars.Add("SupplierID", GuardianID, NSqlTypes.Equal);
                }
                else if (Request["RowType"].ToInt32() == 5)     //内部人员
                {
                    Employee ObjEmployeeBLL = new Employee();
                    int EmployeeID = Request["EmployeeID"].ToInt32();
                    txtName.Text = ObjEmployeeBLL.GetByID(EmployeeID).EmployeeName;

                    pars.Add("RowType", 5, NSqlTypes.Equal);
                    pars.Add("SupplierID", EmployeeID, NSqlTypes.Equal);
                }
                else if (Request["RowType"].ToInt32() == 13)     //外部人员
                {

                    txtName.Text = Request["Name"].ToString();


                }
            }


            //供应商名称
            pars.Add(txtName.Text.Trim().ToString() != string.Empty, "Name", txtName.Text.Trim().ToString(), NSqlTypes.LIKE);

            //责任人
            CstmNameSelector.AppandTo(pars);

            //婚期

            if (Request["SupplierID"] != null)
            {
                if (Type == 1)
                {
                    if (DatePartyDate.IsNotBothEmpty)           //有一个不为空
                    {
                        pars.Add(DatePartyDate.IsNotBothEmpty, "PartyDate", DatePartyDate.StartoEnd, NSqlTypes.DateBetween);

                    }
                    else
                    {
                        DateRanger = Request["DateRanger"].ToString();
                        DatePartyDate.Start = DateRanger.Split(',')[0].ToString().ToDateTime();
                        DatePartyDate.End = DateRanger.Split(',')[1].ToString().ToDateTime();
                        pars.Add("PartyDate", DateRanger, NSqlTypes.DateBetween);
                    }
                }
                else
                {
                    pars.Add(DatePartyDate.IsNotBothEmpty, "PartyDate", DatePartyDate.StartoEnd, NSqlTypes.DateBetween);
                }
            }
            else
            {
                pars.Add(DatePartyDate.IsNotBothEmpty, "PartyDate", DatePartyDate.StartoEnd, NSqlTypes.DateBetween);
            }

            //付款是否完成
            if (ddLPayState.SelectedValue.ToInt32() == 1)       //未完成
            {
                pars.Add("NoPayMent", 0, NSqlTypes.Greaterthan); ;
            }
            else if (ddLPayState.SelectedValue.ToInt32() == 2)      //已完成
            {
                pars.Add("NoPayMent", 0, NSqlTypes.Equal); ;
            }

            var DataList = ObjStatementBLL.GetByAllParameter(pars, OrderByColumnName, CtrPageIndex.PageSize, ViewState["currentpage"].ToString().ToInt32(), out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            rptStatement.DataBind(DataList);
            txtPrePayMent.Text = "";
            int index = 0;
            foreach (var item in DataList)
            {
                if (item.NoPayMent == 0)
                {
                    index += 1;
                }
            }
            if (DataList.Count == index)
            {
                lbtnPayAll.Visible = false;
            }

            #region 本页  本期 合计

            //本页合计
            lblPageSumTotal.Text = DataList.Sum(C => C.SumTotal).ToString();
            lblPagePayMent.Text = DataList.Sum(C => C.PayMent).ToString();
            lblPageNoPayment.Text = DataList.Sum(C => C.NoPayMent).ToString();

            //本期合计
            var DataLists = ObjStatementBLL.GetByAllParameter(pars, OrderByColumnName, 10000, 1, out SourceCount);
            lblAllSumTotal.Text = DataLists.Sum(C => C.SumTotal).ToString();
            lblAllPayMent.Text = DataLists.Sum(C => C.PayMent).ToString();
            lblAllNoPayMent.Text = DataLists.Sum(C => C.NoPayMent).ToString();
            #endregion
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            ViewState["currentpage"] = CtrPageIndex.CurrentPageIndex;
            DataBinder(2);          //不根据传入的时间进行查询
        }
        #endregion

        #region 查找事件
        /// <summary>
        /// 点击查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLook_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            DataBinder(2);          //不根据传入的时间进行查询
        }
        #endregion

        #region 绑定完成事件
        /// <summary>
        /// 绑定完成 是否还能付款
        /// </summary> 
        protected void rptStatement_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField HideStateID = e.Item.FindControl("HideStateID") as HiddenField;
            int Id = HideStateID.Value.ToInt32();
            var ObjStatementModel = ObjStatementBLL.GetByID(Id);

            if (ObjStatementModel.SumTotal == ObjStatementModel.PayMent + ObjStatementModel.PrePayMent)
            {
                TextBox txtPayFor = e.Item.FindControl("txtPayFor") as TextBox;
                TextBox txtContent = e.Item.FindControl("txtContent") as TextBox;
                Label lblPayFors = e.Item.FindControl("lblPayFors") as Label;
                lblPayFors.Text = "付款完成";
                lblPayFors.Visible = true;
                txtContent.Visible = false;
                txtPayFor.Visible = false;
            }
        }
        #endregion

        decimal PrePayMent = 0;

        #region 保存付款
        /// <summary>
        /// 付款
        /// </summary>
        protected void btnSavePre_Click(object sender, EventArgs e)
        {

            LinkButton button = (sender as LinkButton);
            if (button.Text == "保存")
            {
                for (int i = 0; i < rptStatement.Items.Count; i++)
                {
                    var ObjItem = rptStatement.Items[i];
                    PrePayMent += (ObjItem.FindControl("txtPayFor") as TextBox).Text.ToString().ToDecimal();
                    txtPrePayMent.Text = PrePayMent.ToString();
                }
            }
            else if (button.Text == "支付")
            {
                if (txtPrePayMent.Text.ToInt32() > 0 && txtPrePayMent.Text != "")
                {
                    for (int i = 0; i < rptStatement.Items.Count; i++)
                    {
                        var ObjItem = rptStatement.Items[i];
                        TextBox txtPayFor = (ObjItem.FindControl("txtPayFor") as TextBox);
                        TextBox txtContent = (ObjItem.FindControl("txtContent") as TextBox);
                        decimal PayMent = txtPayFor.Text.ToString().ToDecimal();

                        int StatementID = (ObjItem.FindControl("HideStateID") as HiddenField).Value.ToInt32();
                        FL_Statement ObjStatementModel = ObjStatementBLL.GetByID(StatementID);

                        if (ObjStatementModel != null)
                        {
                            if (txtPayFor.Text.ToDecimal() > ObjStatementModel.NoPayMent)
                            {
                                JavaScriptTools.AlertWindow("您的支付金额不能大于未付款金额", Page);
                                return;
                            }
                            ObjStatementModel.PayMent += PayMent;       //已付款
                            ObjStatementModel.NoPayMent = (ObjStatementModel.SumTotal - ObjStatementModel.PayMent - ObjStatementModel.NoPayMent).ToString().ToDecimal();      //未付款
                            int result = ObjStatementBLL.Update(ObjStatementModel);      //更新 更改
                            if (result > 0)
                            {
                                if (PayMent > 0)
                                {
                                    InsertPayMent(PayMent, txtContent.Text, ObjStatementModel);      //支付记录
                                }
                            }

                            if (i == rptStatement.Items.Count - 1)
                            {
                                //JavaScriptTools.AlertWindow("支付成功", Page);
                                DataBinder(2);
                            }
                        }
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("付款金额不能为0,请输入正确的付款金额！", Page);
                }

            }

            //txtPrePayMent.Text = "0";
            //txtPrePayMent.Visible = false;
            //btnSavePre.Visible = false;
            //button.Text = "保存";

        }
        #endregion

        #region 记录 支付记录
        /// <summary>
        /// 支付记录
        /// </summary>    
        public void InsertPayMent(decimal PayMent, string Content, FL_Statement ObjStatementModel)
        {
            FL_StatementPayFor PayModel = new FL_StatementPayFor();
            PayModel.PayMoney = PayMent;
            PayModel.PayDate = DateTime.Now;
            PayModel.Content = Content;
            PayModel.CustomerID = ObjStatementModel.CustomerID;
            PayModel.OrderID = ObjStatementModel.OrderId;
            PayModel.QuotedID = ObjStatementModel.QuotedId;
            PayModel.SupplierID = ObjStatementModel.SupplierID;
            PayModel.SupplierName = ObjStatementModel.Name;
            PayModel.RowType = ObjStatementModel.RowType;
            PayModel.CreateDate = DateTime.Now;
            PayModel.CreateEmployee = User.Identity.Name.ToInt32();
            ObjPayMentBLL.Insert(PayModel);
        }
        #endregion


        #region 付款所有
        /// <summary>
        /// 付款
        /// </summary>
        protected void lbtnPayAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptStatement.Items.Count; i++)
            {
                var DataItem = rptStatement.Items[i];
                int StatementID = (DataItem.FindControl("HideStateID") as HiddenField).Value.ToInt32();
                TextBox txtContent = (DataItem.FindControl("txtContent") as TextBox);
                //结算表
                FL_Statement Model = ObjStatementBLL.GetByID(StatementID);
                Model.PayMent += Model.NoPayMent;
                Model.NoPayMent = Model.SumTotal - Model.PayMent;
                ObjStatementBLL.Update(Model);

                //支付记录
                if (Model.NoPayMent > 0)
                {
                    InsertPayMent(Model.NoPayMent.ToString().ToDecimal(), txtContent.Text, Model);
                }

                if (i == rptStatement.Items.Count - 1)
                {
                    //JavaScriptTools.AlertWindow("支付成功", Page);
                    DataBinder(2);
                }
            }
            //Response.Redirect(Page.Request.Url.ToString());

        }
        #endregion

        #region 重置条件
        /// <summary>
        /// 重置
        /// </summary>
        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.Page.Request.Url.ToString());
        }
        #endregion

        #region 是否已完成全部付款
        /// <summary>
        /// 全部付款
        /// </summary>
        /// <returns></returns>
        public string GetAllPayState(object Source, object Source1)
        {
            int CustomerID = Source.ToString().ToInt32();
            string Name = Source1.ToString();
            var Model = ObjStatementBLL.GetByCustomerID(CustomerID, Name);
            if (Model != null)
            {
                if (Model.NoPayMent == 0)       //没有付款的金额为0 说明已完成所有付款 
                {
                    return "已完成";
                }
                else
                {
                    return "未完成";
                }
            }
            return "";
        }
        #endregion

        #region 获取银行信息
        /// <summary>
        /// 获取银行信息
        /// </summary>
        /// <param name="Type">1.代表 银行名称  2.代表银行帐号</param>
        public string GetBankInfo(object Source, object Source1, int Type)
        {
            int RowType = 0;
            int ID = 0;
            if (Source != null)
            {
                RowType = Source.ToString().ToInt32();          //1.供应商    4.四大金刚    5.内部人员
            }

            if (Source1 != null)
            {
                ID = Source1.ToString().ToInt32();                //供应商ID SupplierID
            }

            if (Type == 1)
            {
                if (RowType == 1)
                {
                    var Model = ObjSupplierBLL.GetByID(ID);
                    if (Model != null)
                    {
                        return Model.BankName != null ? Model.BankName.Trim().ToString() : "";
                    }
                }
                else if (RowType == 4)
                {
                    var Model = ObjFourGuardBLL.GetByID(ID);
                    if (Model != null)
                    {
                        return Model.BankName != null ? Model.BankName.Trim().ToString() : "";
                    }
                }
                else if (RowType == 5)
                {
                    var Model = ObjEmployeBLL.GetByID(ID);
                    if (Model != null)
                    {
                        return Model.BankName != null ? Model.BankName.Trim().ToString() : "";
                    }
                }
            }
            else if (Type == 2)
            {
                if (RowType == 1)
                {
                    var Model = ObjSupplierBLL.GetByID(ID);
                    if (Model != null)
                    {
                        return Model.AccountInformation != null ? Model.AccountInformation.Trim().ToString() : "";
                    }
                }
                else if (RowType == 4)
                {
                    var Model = ObjFourGuardBLL.GetByID(ID);
                    if (Model != null)
                    {
                        return Model.BankName != null ? Model.AccountInformation.Trim().ToString() : "";
                    }
                }
                else if (RowType == 5)
                {
                    var Model = ObjEmployeBLL.GetByID(ID);
                    if (Model != null)
                    {
                        return Model.BankCard != null ? Model.BankCard.Trim().ToString() : "";
                    }
                }
            }


            return "";
        }
        #endregion


        public decimal GetPrePayMnet(object Source)
        {
            if (Source != null)
            {
                decimal PrePayMent = Source.ToString().ToDecimal();
                return PrePayMent;
            }
            return 0;
        }
    }
}